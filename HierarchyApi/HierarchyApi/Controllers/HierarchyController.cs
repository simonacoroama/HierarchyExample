namespace HierarchyApi.Controllers
{
    using Contracts;
    using Handlers;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class HierarchyController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public HierarchyController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [Microsoft.AspNetCore.Cors.EnableCors("AllowOrigin")]
        [HttpGet(Name = "GetHierarchy")]
        public Hierarchy Get()
        {
            var handler = new GetHierarchyHandler(this.configuration);
            return handler.GetHierarchy();
        }
    }
}