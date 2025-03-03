using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApplicationDomain.Entities;
using MyApplicationServiceLayer.ProjectService;
using MyApplicationServiceLayer.ProjectService.Extensions;
using MyApplicationServiceLayer.ProjectService.Models;
using MyApplicationServiceLayer.ProjectService.Post;
using MyApplicationServiceLayer.RequestService.Models;
using System.Collections;

namespace MyApplication.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        /// <summary>
        /// Create a project
        /// </summary>
        /// <param name="model"></param>
        /// <returns>New created project</returns>
        /// <response code = "201">Returns the newly created project</response>
        /// <response code = "400">If the new project is not validated</response>
        /// <response code = "401">If not authorized</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ProjectModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Project>> Post([FromBody] ProjectModel model)
        {
            var project = await _projectService.Post(model);
            var projectModel = project?.ToModel();

            return projectModel == null
                ? BadRequest() 
                : CreatedAtAction(nameof(Post), projectModel);
        }

        /// <summary>
        /// Edit project
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns>Edited project</returns>
        /// <response code = "200">Returns edited project</response>
        /// <response code = "400">If project with this id is not exists</response>
        /// <response code = "401">If not authorized</response>
        /// <response code = "403">If not authorized</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpPut("id")]
        [ProducesResponseType(200, Type = typeof(ProjectModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<ProjectModel>> Edit(int id, [FromBody] ProjectModel model)
        {
            var project = await _projectService.Edit(id, model);
            var projectModel = project?.ToModel();

            return projectModel == null
                ? BadRequest()
                : Ok(projectModel);
        }

        /// <summary>
        /// Get the collection of ProjectModels
        /// </summary>
        /// <returns>Collection of ProjectModels</returns>
        /// <response code = "200">Returns the collection of ProjectModels</response>>
        /// <response code = "401">If not authorized</response>>
        /// <response code = "403">If not authorized</response>>
        /// <response code = "500">Internal server error</response>>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProjectModel>))]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<ProjectModel>>> GetAll()
        {
            var qry = _projectService.GetAll();

            var projects = await qry.ToModel().ToListAsync();

            return Ok(projects);
        }

        /// <summary>
        /// Get a ProjectModel with selcted id
        /// </summary>
        /// <param name="id"></param>
        /// <response code = "200">Returns a ProjectModel</response>>
        /// <response code = "400">If project with selected id is not exists</response>>
        /// <response code = "401">If not authorized</response>>
        /// <response code = "403">If not authorized</response>>
        /// <response code = "500">Internal server error</response>>
        /// <returns>ProjectModel</returns>
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(ProjectModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<ProjectModel>> Get(int id)
        {
            var project = await _projectService.Get(id);

            var projectModel = project?.ToModel();

            return projectModel == null
                ? BadRequest()
                : Ok(projectModel);
        }

        /// <summary>
        /// Delete a project
        /// </summary>
        /// <param name="id"></param>
        /// <response code = "200">Returns deleted ProjectModel</response>>
        /// <response code = "401">If not authorized</response>>
        /// <response code = "403">If not authorized</response>>
        /// <response code = "500">Internal server error</response>>
        /// <returns>Deleted project</returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(200, Type = typeof(ProjectModel))]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<ProjectModel>> Remove(int id)
        {
            var deletedProject = await _projectService.Remove(id);
            var resultModel = deletedProject?.ToModel();

            return resultModel == null
                ? BadRequest()
                : Ok(resultModel);
        }
    }
}
