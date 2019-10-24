using Microsoft.AspNetCore.Mvc;

namespace NetCoreFizzBuzzApi.Controllers {
    
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class ApiController : ControllerBase { }
}