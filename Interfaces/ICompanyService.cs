using MyWebApi.Core.Dto;
using MyWebApi.Core.Model;

namespace MyWebApi.Interfaces
{
    public interface ICompanyService
    {
        Task<PaginatedResponse> GetAllCompany(PaginationRequest request);
        Task<CompanySaveResponse> SaveOrUpdate(PostRequest request);
        Task<DeleteRequest> Delete(DeleteRequest request);

        Task<DeleteRequestDep> DeleteDepartment(DeleteRequestDep requestDep);


        //LINQ

        Task<List<Company>> GetCompanybyDep(int departmentId);

        Task<List<CompanyLinq>> GetCompanyCount();

        Task<List<CompanyLinqs>> topFive();

    }
}
