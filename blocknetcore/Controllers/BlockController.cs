using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blocknetcore.Controllers
{
    public class BlockController : Controller
    {
        [HttpGet]
        [Route("Blocks")]
        public IActionResult GetBlocks()
        {
            return Ok(Program.BlockLogic.GetBlockChain());
        }

        [HttpPost]
        [Route("MineBlock")]
        public IActionResult MineBlock([FromBody] string blockData)
        {
            var block = Program.BlockLogic.GenerateNextBlock(blockData);
            Program.BlockLogic.AddBlock(block);

            return Ok("Block added:" + JsonConvert.SerializeObject(block));
        }
    }
}
