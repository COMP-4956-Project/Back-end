using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronPython.Hosting;

namespace Backend
{
    public class PythonRunner
    {

        string output = string.Empty;

        public PythonRunner()
        {
        }

        public string RunFromFile(string path, string input)
        {
            var engine = Python.CreateEngine();
            var scope = engine.CreateScope();

            // Redirect the standard output to capture Python's print statements
            var outputStream = new MemoryStream();
            var inputStream = new MemoryStream(Encoding.Default.GetBytes(input));

            engine.Runtime.IO.SetOutput(outputStream, Encoding.Default);
            engine.Runtime.IO.SetInput(inputStream, Encoding.Default);

            var sourceCode = engine.CreateScriptSourceFromFile(path);
            sourceCode.Execute(scope);

            // Retrieve the captured output
            outputStream.Position = 0; // Reset the stream position
            string capturedOutput;
            using (var reader = new StreamReader(outputStream, Encoding.Default))
            {
                capturedOutput = reader.ReadToEnd();
            }
            return capturedOutput;
        }

        public string RunFromString(string code)
        {
            var engine = Python.CreateEngine();
            var scope = engine.CreateScope();

            // Redirect the standard output to capture Python's print statements
            var stream = new MemoryStream();
            engine.Runtime.IO.SetOutput(stream, Encoding.UTF8);

            var sourceCode = engine.CreateScriptSourceFromString(code);
            sourceCode.Execute(scope);

            // Retrieve the captured output
            stream.Position = 0; // Reset the stream position
            string capturedOutput;
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                capturedOutput = reader.ReadToEnd();
            }
            return capturedOutput;
        }

    }

}