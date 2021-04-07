using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;


namespace CommandAPI.COntrollers{
[Route("api/[controller]")]
[ApiController]
public class CommandsController : ControllerBase 
{

[HttpGet]
public ActionResult<IEnumerable<string>> GetHardCoded(){
    return new string[] {"This", "is","Hard","Coded"};
}

// [HttpGet]
// public ActionResult<IEnumerable<string>> GetHardCoded2(){
//     return new string[] {"This", "is","Hard","Coded2"};
// }
}

}