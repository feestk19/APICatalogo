using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers;

[Route("api/v{version:apiVersion}/teste")]
[ApiController]
[ApiVersion("2.0")]
public class TesteV2Controller : Controller
{
    [HttpGet]
    public string Index()
    {
        return "TesteV2 - GET - Api Versão 2.0";
    }
}
