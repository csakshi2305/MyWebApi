using MyWebApi.Core.Dto;
using MyWebApi.Core.Model;

namespace MyWebApi.Interfaces
{
    public interface IDyeTypesService

    {

        Task<DyeTypePaginationResponse> GetAllDyeTypes(DyeTypePaginationRequest requests);

        Task<DyeTypeSaveResponse> SaveOrUpdateDyeTypes(DyeTypePostRequest requests);
        Task<DyeTypeDeleteRequest> DeleteDyeTypes(DyeTypeDeleteRequest requests);



        Task<string> GetMaxDyeTypeCode();

        }
    }
