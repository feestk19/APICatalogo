using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers;

[Route("api/v{version:apiVersion}/teste")]
[ApiController]
[ApiVersion("1.0")]
public class TesteV1Controller : Controller
{
    [HttpGet]
    public string Index()
    {
        return "TesteV1 - GET - Api Versão 1.0";
    }
}
