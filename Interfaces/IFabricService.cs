using MyWebApi.Core.Dto;

namespace MyWebApi.Interfaces
{
    public interface IFabricService
    {
        Task<FabricPaginatedResponse> GetAllFabric(FabricPaginationRequest requests);

        Task<FabricSaveResponse> SaveOrUpdate(FabricPostRequest requests);
        Task<DeleteFabricRequest> Delete(DeleteFabricRequest requests);


        //for construction

        Task<ConstructionPaginatedResponse> GetAllConstruction(ConstructionPaginationRequest requests);
        Task<ConstructionSaveResponse> SaveOrUpdateConstruction(ConstructionPostRequest requests);
        Task<DeleteConstructionRequest> DeleteConstruction(DeleteConstructionRequest requests);

    }
}
