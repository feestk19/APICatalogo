using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers;

[ApiController]
[Route("api/teste")]
[ApiVersion(3)]
[ApiVersion(4)]
public class TesteV3Controller : Controller
{
    [MapToApiVersion(3)]
    [HttpGet]
    public string GetVersion3()
    {
        return "Version3 - GET - Api Versão 3.0";
    }

    [MapToApiVersion(4)]
    [HttpGet]
    public string GetVersion4()
    {
        return "Version4 - GET - Api Versão 4.0";
    }
}
