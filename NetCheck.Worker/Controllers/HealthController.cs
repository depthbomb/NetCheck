using Microsoft.AspNetCore.Mvc;

namespace NetCheck.Worker.Controllers;

public class HealthController : Controller
{
    [HttpGet("/ping")]
    public IActionResult Ping()
    {
        return new NoContentResult();
    }
}
