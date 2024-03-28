using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApplicationDataLayer.Entities;
using MyApplicationServiceLayer.RequestService;
using MyApplicationServiceLayer.RequestService.Extensions;
using MyApplicationServiceLayer.RequestService.Models;
using MyApplicationServiceLayer.RequestService.PostRequest.Models;
using System.Security.Claims;

namespace MyApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController :  ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        /// <summary>
        /// Get the collection of RequestModels
        /// </summary>
        /// <returns>Collection of RequestModels</returns>
        /// <response code = "200">Returns the collection of RequestModels</response>
        /// <response code = "401">If not authorized</response>
        /// <response code = "403">If not authorized</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpGet]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RequestModel>))]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<RequestModel>?>> GetAll()
        {
            var requestsQry = await _requestService.GetAll();

            var requests = await requestsQry.ToModel().ToListAsync();

            return Ok(requests);
        }

        /// <summary>
        /// Create a request
        /// </summary>
        /// <param name="model"></param>
        /// <returns>New created request</returns>
        /// <response code = "201">Returns the newly created request</response>
        /// <response code = "400">If the new request is null</response>
        /// <response code = "401">If not authorized</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpPost]
        [Authorize(Roles = "user")]
        [ProducesResponseType(201, Type = typeof(Request))]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Request>> Post([FromBody] PostRequestModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Int32.TryParse(userId, out var id))
                throw new ArgumentException("Wrong id format");

            var result = await _requestService.Post(model, id);

            return result == null ? BadRequest() : CreatedAtAction(nameof(Post), result);
        }

        /// <summary>
        /// Edit status for request
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns>Patched request</returns>
        /// <response code = "200">Returns patched request</response>
        /// <response code = "404">If id is null</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpPatch("{id:int}/{status:length(1,20)}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Request))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Request>> EditStatus(int id, RequestStatus status)
        {
            var result = await _requestService.EditStatus(id, status);

            return result == null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// Get the collection of RequestsModel with selected RequestStatus
        /// </summary>
        /// <param name="status"></param>
        /// <returns>Collection of RequestModel</returns>
        /// <response code = "200">Returns the collection of RequestModels</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpGet("{status:length(1,20)}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RequestModel>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<RequestModel>?>> GetByStatus(RequestStatus status)
        {
            var requestsQry = await _requestService.GetByStatus(status);

            var requests = await requestsQry.ToModel().ToListAsync();

            return Ok(requests);
        }
    }
}
