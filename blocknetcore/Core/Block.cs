using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blocknetcore.Core
{
    public class Block
    {
        public Block(int index, string previousHash, long timestamp, string data, string hash)
        {
            Index = index;
            PreviousHash = previousHash;
            Timestamp = timestamp;
            Data = data;
            Hash = hash;
        }

        public int Index { get; private set; }
        public string PreviousHash { get; private set; }
        public long Timestamp { get; private set; }
        public string Data { get; private set; }
        public string Hash { get; private set; }
    }
}
