using Microsoft.AspNetCore.Mvc;
using MyApplicationDataLayer.Entities;
using MyApplicationServiceLayer.ComponentService;

namespace MyApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentController : ControllerBase
    {
        private readonly IComponentService _componentService;

        public ComponentController(
            IComponentService componentService)
        {
            _componentService = componentService;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Component>> Get(int id)
        {
            var request = await _componentService.Get(id);

            return request == null ? BadRequest() : Ok(request);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Component>>> GetAll()
        {
            var request = _componentService.GetAll().Result.ToList();

            return request == null ? BadRequest() : Ok(request);
        }
    }
}
