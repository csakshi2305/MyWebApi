using Microsoft.EntityFrameworkCore;
using MyWebApi.Core.Dto;
using MyWebApi.Core.Model;
using MyWebApi.Infrastructure.Data;
using MyWebApi.Interfaces;


namespace MyWebApi.Infrastructure.Repository
{
    public class CompanyRepository:ICompanyRepository
    {
      
        private readonly ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GetAllActive
        /// </summary>
        /// <returns></returns>
        public IQueryable<Company> GetAllActive()
        {
            return _context.Companies.Where(c => c.IsActive);
        }

        /// <summary>
        /// Count
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<int> Count(IQueryable<Company> query)
        {
            return query.Count();
        }

        /// <summary>
        /// GetById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Company> GetById(int id)
        {
            return await _context.Companies.FirstOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public async Task<int> Add(Company company)
        {
            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();
            return company.Id;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public async Task<int> Update(Company company)
        {
            _context.Companies.Update(company);
            await _context.SaveChangesAsync();
            return company.Id;
        }

        /// <summary>
        /// isExistingName
        /// </summary>
        /// <param name="companyName"></param>
        /// <returns></returns>
        public async Task<bool> isExistingName(string companyName)
        {
            return await _context.Companies.AnyAsync(c => c.CompanyName.Trim() == companyName.Trim());
        }

        /// <summary>
        /// GetAllActiveDepartments
        /// </summary>
        /// <returns></returns>
        public IQueryable<Department> GetAllActiveDepartments()
        {
            return _context.Department.Where(d => d.IsActive);
        }

        /// <summary>
        /// GetAllActiveCompanies
        /// </summary>
        /// <returns></returns>
        public IQueryable<Company> GetAllActiveCompanies()
        {
            return _context.Companies.Where(c => c.IsActive);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public async Task<int> Update(Department department)
        {
            _context.Department.Update(department);
            return await _context.SaveChangesAsync();
        }


        //LINQ


        //2

        //public async Task<List<Company>> GetCompanybyDep(int departmentId)
        //{
        //    var companie= await _context.Companies.Where(c => c.DepartmentId == departmentId).ToListAsync();
        //      return await companie.ToListAsync();
        //}

        public async Task<List<Company>> GetCompanybyDep(int departmentId)
        {
            var companie = from c in _context.Companies where c.DepartmentId == departmentId select c;
            return await companie.ToListAsync();
        }

        //public async Task<List<Company>> GetCompanyCount()
        //{
        //    var getcompany = await _context.Companies.GroupBy(c => c.DepartmentId).Select(g => new Company
        //    {
        //        DepartmentId = g.Key,
        //        CompanyCount = g.Count()
        //    })
        //        .ToListAsync();

        //    return getcompany;
        //}


     //3

        public async Task<List<CompanyLinq>> GetCompanyCount()
        {
            var result = from c in _context.Companies
                         group c by c.DepartmentId into grouped
                         select new CompanyLinq
                         {
                             DepartmentId = grouped.Key,
                            CompanyCount = grouped.Count()
                         };

            return await result.ToListAsync();
        }

        //public async Task<List<CompanyLinqs>> topFive()
        //{

        //    var top = await _context.Companies
        //        .Where(c => c.Department != null)
        //        .OrderByDescending(c => c.Department!.DepartmentName)
        //        .Take(5)
        //        .Select(c => new CompanyLinqs
        //        {
        //            Id = c.Id,
        //            CompanyName = c.CompanyName,
        //            DepartmentName = c.Department!.DepartmentName
        //        })
        //        .ToListAsync();
        //    return top;
        //}

        public async Task<List<CompanyLinqs>> topFive()
        {
            var result = (from c in _context.Companies
                          where c.Department != null
                          orderby c.Department!.DepartmentName descending
                          select new CompanyLinqs
                          {
                              Id = c.Id,
                              CompanyName = c.CompanyName,
                              DepartmentName = c.Department.DepartmentName
                          }).Take(5);

            return await result.ToListAsync();
        }



    }

}

