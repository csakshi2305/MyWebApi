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
    public class DyeTypeController:ControllerBase
    {

        private readonly IDyeTypesService _dyeTypesService;

        public DyeTypeController(IDyeTypesService service)
        {
            _dyeTypesService = service;
        }


        [HttpGet("GetNextDyeTypeCode")]
        public async Task<IActionResult> GetNextDyeTypeCode()
        {
            var nextCode = await _dyeTypesService.GetMaxDyeTypeCode();
            return Ok(nextCode);
        }
        [HttpPost("SaveDyetypes")]
        public async Task<DyeTypeSaveResponse> SaveDyetypes([FromBody] DyeTypePostRequest dyeTypePostRequest)
        {
            return await _dyeTypesService.SaveOrUpdateDyeTypes(dyeTypePostRequest);
        }

        [HttpPost("GetAllDyeTypes")]
        public async Task<DyeTypePaginationResponse> GetAllDyeType([FromBody] DyeTypePaginationRequest request)
        {
            return await _dyeTypesService.GetAllDyeTypes(request);
        }

        [HttpPost("deleteDyeTypes")]
        public async Task<DyeTypeDeleteRequest> deleteDyeTypes([FromBody] DyeTypeDeleteRequest request)
        {
            return await _dyeTypesService.DeleteDyeTypes(request);
        }
    }
}
