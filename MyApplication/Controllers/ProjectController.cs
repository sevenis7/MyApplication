using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApplicationDomain.Entities;
using MyApplicationServiceLayer.ProjectService.Models;
using MyApplicationServiceLayer.ProjectService.Post;

namespace MyApplication.Controllers
{
    //[Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IPostProjectService _postProjectService;

        public ProjectController(IPostProjectService postProjectService)
        {
            _postProjectService = postProjectService;
        }

        /// <summary>
        /// Create a project
        /// </summary>
        /// <param name="model"></param>
        /// <returns>New created project</returns>
        /// <response code = "201">Returns the newly created project</response>
        /// <response code = "401">If not authorized</response>
        /// <response code = "400">If the new project is null</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Project))]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Project>> Post([FromBody] ProjectModel model)
        {
            var result = await _postProjectService.Post(model);

            return result == null ? BadRequest() : CreatedAtAction(nameof(Post), result);
        }
    }
}
