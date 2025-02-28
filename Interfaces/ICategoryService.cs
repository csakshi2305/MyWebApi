using MyWebApi.Core.Dto;

namespace MyWebApi.Interfaces
{
    public interface ICategoryService
    {

        Task<CategoryPaginatedResponse> GetAllCategory(CategoryPaginationRequest request);
        Task<CategorySaveResponse> SaveOrUpdateCategory(CategoryPostRequest request);
        Task<CategoryDeleteRequest> DeleteCategory(CategoryDeleteRequest request);

        //individualCareSymbol

        Task<IndividualPaginatedResponse> IndividualGetAll(IndividualPaginationRequest request);
        Task<IndividualSaveResponse> IndividualSaveOrUpdate(IndividualPostRequests request);
        Task<IndividualDeleteRequest> IndividualDelete(IndividualDeleteRequest request);

        //country

        Task<CountryPaginatedResponse> CountryGetAll(CountryPaginationRequest request);
        Task<CountrySaveResponse> CountrySaveOrUpdate(CountryPostRequests request);
        Task<CountryDeleteRequest> CountryDelete(CountryDeleteRequest request);
    }
}
