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
        //[Authorize(Roles = "admin")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RequestModel>))]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<RequestModel>?>> GetAll()
        {
            var requestsQry = await _requestService.GetAll();

            var requests = await requestsQry.ToModel().ToListAsync();

            return Ok(requests);
        }

        /// <summary>
        /// Get a RequestModel with selected id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>RequestModel</returns>
        /// <response code = "200">Returns the RequestModels</response>
        /// <response code = "401">If not authorized</response>
        /// <response code = "403">If not authorized</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpGet("{id:int}")]
        //[Authorize(Roles = "admin")]
        [ProducesResponseType(200, Type = typeof(RequestModel))]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<RequestModel?>> Get(int id)
        {
            var request =  await _requestService.Get(id);

            var requestModel = request?.ToModel();

            return requestModel == null ? BadRequest() : Ok(requestModel);
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
        //[Authorize(Roles = "user")]
        [ProducesResponseType(201, Type = typeof(Request))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
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
        /// <response code = "400">If request with this id is not exists</response>
        /// <response code = "401">If not authorized</response>
        /// <response code = "403">If not authorized</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpPatch("{id}/{status}")]
        //[Authorize(Roles = "admin")]
        [ProducesResponseType(200, Type = typeof(Request))]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
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
        /// <response code = "401">If not authorized</response>
        /// <response code = "403">If not authorized</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpGet("{status}")]
        //[Authorize(Roles = "admin")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RequestModel>))]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<RequestModel>?>> GetByStatus(RequestStatus status)
        {
            var requestsQry = await _requestService.GetByStatus(status);

            var requests = await requestsQry.ToModel().ToListAsync();

            return Ok(requests);
        }
    }
}
