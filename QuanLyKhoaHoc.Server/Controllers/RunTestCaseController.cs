using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RunTestCaseController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> RunTestCase([FromQuery] string code)
        {
            try
            {
                var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.IsDynamic).Select(a => a.Location);

                var scriptOptions = ScriptOptions.Default.WithReferences(assemblies).WithImports("System");

                using (var writer = new StringWriter())
                {
                    Console.SetOut(writer);

                    var result = await CSharpScript.EvaluateAsync(code + "IsEven(2)", scriptOptions);

                    var consoleOutput = writer.ToString();

                    Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });

                    return Ok(new { Result = result?.ToString(), ConsoleOutput = consoleOutput });
                }
            }
            catch (CompilationErrorException ex)
            {
                return BadRequest(new { Errors = ex.Diagnostics });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
