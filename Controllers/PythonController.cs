using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PythonResponse : ControllerBase
    {
        // [Route("api/getPythonResponse")]
        [HttpGet(Name = "GetPythonResponse")]
        public IActionResult GetPythonFromString(string pythonCode)
        {
            try
            {
                PythonRunner pyRunner = new PythonRunner();
                string output = pyRunner.RunFromString(pythonCode);
                return Ok(output);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // [Route("api/postPythonFromFile")]
        [HttpPost(Name = "PostPythonFromFile")]
        public async Task<IActionResult> PostPythonFromFile(IFormFile file, string input)
        {
            try
            {
                string output = string.Empty;
                    if (file.Length > 0)
                    {
                        var filePath = Path.GetTempFileName();
                        Console.WriteLine(filePath);

                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await file.CopyToAsync(stream);
                        }
                        PythonRunner pyRunner = new PythonRunner();
                        output = pyRunner.RunFromFile(filePath, input);
                    }

                // Process uploaded files
                // Don't rely on or trust the FileName property without validation.
                return Ok(output);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}