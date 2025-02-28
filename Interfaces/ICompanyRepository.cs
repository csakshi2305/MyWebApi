using MyWebApi.Core.Model;
using MyWebApi.Core.Dto;


namespace MyWebApi.Interfaces
{
    public interface ICompanyRepository
    {
        Task<Company?> GetById(int id);
        Task<int> Add(Company company);
        Task<int> Update(Company company);
        IQueryable<Company> GetAllActive();
        Task<int> Count(IQueryable<Company> query);
       Task<bool> isExistingName(string companyName);




        IQueryable<Department> GetAllActiveDepartments();
        IQueryable<Company> GetAllActiveCompanies();
        Task<int> Update(Department department);

        //linq

        Task<List<Company>> GetCompanybyDep(int departmentId);

        Task<List<CompanyLinq>> GetCompanyCount();

        Task<List<CompanyLinqs>> topFive(); 



    }

}