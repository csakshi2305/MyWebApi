using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Core.Dto;
using MyWebApi.Infrastructure.Repository___service;
using MyWebApi.Interfaces;

namespace MyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class StuffController:ControllerBase
    {
        private readonly IStuffService _stuffService;

        public StuffController(IStuffService service)
        {
            _stuffService= service;
        }


        [HttpPost("SaveStuffs")]
        public async Task<StuffSaveResponse> SaveStuffs([FromBody] StuffPostRequest stuffPostRequest)
        {
            return await _stuffService.SaveOrUpdateStuff(stuffPostRequest);
        }

        [HttpPost("GetAllStuffs")]
        public async Task<StuffPaginationResponse> GetAllStuffs([FromBody] StuffPaginationRequest request)
        {
            return await _stuffService.GetAllStuff(request);
        }

        [HttpPost("deleteStuffs")]
        public async Task<StuffDeleteRequest> deleteStuffs([FromBody] StuffDeleteRequest request)
        {
            return await _stuffService.DeleteStuff(request);
        }
    }
}
