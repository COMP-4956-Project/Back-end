using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using MVC_Backend_Frontend.Models;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/runPython")]
    public class PythonController : ControllerBase
    {

        [HttpPost]
        public IActionResult PostPythonFromJson([FromBody] BlockList blockInput)
        {
            Console.WriteLine(blockInput);
            try
            {
                PythonRunner pyRunner = new PythonRunner();
                string output = pyRunner.RunFromBlockList(blockInput);
                return Ok(output);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}