using Microsoft.AspNetCore.Mvc;
using MyWebApi.Core.Dto;
using MyWebApi.Infrastructure.Repository___service;
using MyWebApi.Interfaces;

namespace MyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FabricController : ControllerBase
    {
        private readonly IFabricService _fabricService;

        public FabricController(IFabricService fabricService)
        {
            _fabricService = fabricService;
        }

        [HttpPost("SaveFabrics")]
        public async Task<FabricSaveResponse> SaveFabric([FromBody] FabricPostRequest fabricPostRequest)
        {
            return await _fabricService.SaveOrUpdate(fabricPostRequest);
        }

        [HttpPost("GetAllFabrics")]
        public async Task<FabricPaginatedResponse> GetAllFabrics([FromBody] FabricPaginationRequest request)
        {
            return await _fabricService.GetAllFabric(request);
        }

        [HttpPost("deleteFabrics")]
        public async Task<DeleteFabricRequest> deleteFabrics([FromBody] DeleteFabricRequest request)
        {
            return await _fabricService.Delete(request);
        }


        //construction


        [HttpPost("SaveConstruction")]
        public async Task<ConstructionSaveResponse> SaveConstruction([FromBody] ConstructionPostRequest constructionPostRequest)
        {
            return await _fabricService.SaveOrUpdateConstruction(constructionPostRequest);
        }


        [HttpPost("GetAllConstruction")]
        public async Task<ConstructionPaginatedResponse> GetAllConstruction([FromBody] ConstructionPaginationRequest constructionPaginationRequest)
        {
            return await _fabricService.GetAllConstruction(constructionPaginationRequest);
        }

        [HttpPost("deleteConstruction")]
        public async Task<DeleteConstructionRequest> deleteConstruction([FromBody] DeleteConstructionRequest deleteConstructionRequest)
        {
            return await _fabricService.DeleteConstruction(deleteConstructionRequest);
        }
    }
}
