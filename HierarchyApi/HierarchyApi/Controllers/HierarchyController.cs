using HierarchyApi.Contracts;
using HierarchyApi.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace HierarchyApi.Controllers
{
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