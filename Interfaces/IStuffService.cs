using MyWebApi.Core.Dto;

namespace MyWebApi.Interfaces
{
    public interface IStuffService
    {
        Task<StuffPaginationResponse> GetAllStuff(StuffPaginationRequest requests);

        Task<StuffSaveResponse> SaveOrUpdateStuff(StuffPostRequest requests);
        Task<StuffDeleteRequest> DeleteStuff(StuffDeleteRequest requests);
    }
}
