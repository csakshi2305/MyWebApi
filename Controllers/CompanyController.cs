using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebApi.Core.Dto;
using MyWebApi.Core.Model;
using MyWebApi.Infrastructure.Data;
using MyWebApi.Interfaces;

namespace MyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly ApplicationDbContext _context;

        public CompanyController(ICompanyService companyService, ApplicationDbContext context)
        {
            _companyService = companyService;
            _context = context;
        }


        [HttpPost("SaveCompany")]
        public async Task<CompanySaveResponse> SaveCompany([FromBody] PostRequest companyRequest)
        {
            return await _companyService.SaveOrUpdate(companyRequest);
        }

        [HttpPost("GetAllCompanies")]
        public async Task<PaginatedResponse> GetAllCompanies([FromBody] PaginationRequest request)
        {
            return await _companyService.GetAllCompany(request);
        }

        [HttpPost("delete")]
        public async Task<DeleteRequest> DeleteCompany([FromBody] DeleteRequest request)
        {
            return await _companyService.Delete(request);
        }


        [HttpPost("DeleteDepartment")]
        public async Task<DeleteRequestDep> DeleteDepartment([FromBody] DeleteRequestDep request)
        {
            
            return await _companyService.DeleteDepartment(request);
        }

        //Linq

        [HttpGet("getdep/{departmentId}")]
        public async Task<List<Company>> GetCompaniesbydep(int departmentId)
        {
            return await _companyService.GetCompanybyDep(departmentId);
        }

        [HttpGet("count")]
        public async Task<List<CompanyLinq>> GetCompaniesCount()
        {
            return await _companyService.GetCompanyCount();
        }


        [HttpGet("top5")]
        public async Task<List<CompanyLinqs>> GetTop5CompaniesQuery()
        {
            return await _companyService.topFive();
        }






























        // [HttpGet("1")]
        // public async Task<IActionResult> GetActiveCompanies()
        // {

        // var active = await (from company in _context.Companies
        //                                   where company.IsActive
        //                                   select company).ToListAsync();


        //    // var active = await _context.Companies.Where(c => c.IsActive).ToListAsync();

        // return Ok(active);
        // }

        // [HttpGet("2/{departmentId}")]
        // public async Task<IActionResult> GetCompaniesByDepartment(int departmentId)
        // {

        // var companies = await (from company in _context.Companies

        //                        where company.DepartmentId == departmentId
        //                  select company).ToListAsync();


        //   //  var companies = await _context.Companies.Where(c => c.DepartmentId == departmentId).ToListAsync();

        //     return Ok(companies);
        // }

        // [HttpGet("3")]
        // public async Task<IActionResult> GetCompanyCountByDepartment()
        // {


        // var count = await _context.Companies
        //     .GroupBy(c => c.DepartmentId)
        //     .Select(g => new { g.departmentid, CompanyCount = g.Count() })
        //     .ToListAsync();

        // return Ok(count);
        // }

        // [HttpGet("4")]
        // public async Task<IActionResult> GetTop5CompaniesByDepartmentName()
        // {

        // var TopCompanies = await (from company in _context.Companies
        //join department in _context.Department on company.DepartmentId equals department.Id
        //         orderby department.DepartmentName descending
        //         select new
        //               {
        //              company.Id,
        //              company.CompanyName,
        //         department.DepartmentName
        //          }).Take(5).ToListAsync();


        // //var TopCompanies = await _context.Companies
        // //        .Include(c => c.Department)
        // //        .OrderByDescending(c => c.Department.DepartmentName)
        // //        .Select(c => new { c.Id, c.CompanyName, c.Department.DepartmentName })
        // //        .Take(5)
        // //        .ToListAsync();

        //     return Ok(TopCompanies);
        // }

    }
}
