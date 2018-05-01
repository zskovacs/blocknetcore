using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace blocknetcore.Core
{
    public class Logic
    {
        private Stack<Block> blockChain;

        public Logic()
        {
            blockChain = new Stack<Block>();
            blockChain.Push(GenerateGenesisBlock());
        }

        private Block GenerateGenesisBlock()
        {
            return new Block(0, "0", 1525160403, "genesis block", "7f888024d4a182d61074fcccb7c69af401acfbfff328df8633cec87f669c1e07");
        }

        public void AddBlock(Block block)
        {
            if (IsValidBlock(block, GetLatestBlock()))
                blockChain.Push(block);
        }

        private bool IsValidBlock(Block newBlock, Block prevBlock)
        {
            if (prevBlock.Index + 1 != newBlock.Index)
            {
                Console.WriteLine("INVALID INDEX");
                return false;
            }
            else if (prevBlock.Hash != newBlock.PreviousHash)
            {
                Console.WriteLine("INVALID PREVIOUS HASH");
                return false;
            }
            else if (CalcualteHash(newBlock) != newBlock.Hash)
            {
                Console.WriteLine("INVALID HASH");
                return false;
            }

            return true;
        }

        public Block GenerateNextBlock(string data)
        {
            var previousBlock = GetLatestBlock();
            var nextIndex = previousBlock.Index + 1;
            var nextTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var nextHash = CalcualteHash(nextIndex, previousBlock.Hash, nextTimestamp, data);
            return new Block(nextIndex, previousBlock.Hash, nextTimestamp, data, nextHash);
        }

        private Block GetLatestBlock()
        {
            return blockChain.Peek();
        }

        public string CalcualteHash(Block block)
        {
            return CalcualteHash(block.Index, block.PreviousHash, block.Timestamp, block.Data);
        }

        public string CalcualteHash(int index, string previousHash, long timestamp, string data)
        {
            var bytes = Encoding.UTF8.GetBytes($"{index}{previousHash}{timestamp}{data}");
            var hashstring = new SHA256Managed();
            var hash = hashstring.ComputeHash(bytes);

            var hashString = new StringBuilder();
            foreach (var x in hash)
            {
                hashString.Append(x.ToString("x2"));
            }

            return hashString.ToString();
        }

        public List<Block> GetBlockChain()
        {
            return blockChain.ToList();
        }
    }
}
