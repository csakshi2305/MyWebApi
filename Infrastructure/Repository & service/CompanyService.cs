using Microsoft.EntityFrameworkCore;
using MyWebApi.Core.Dto;
using MyWebApi.Core.Model;
using MyWebApi.Interfaces;

namespace MyWebApi.Infrastructure.Repository___service
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly CMap _map;


        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
            _map = new CMap();
        }
        /// <summary>
        /// SaveOrUpdate
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CompanySaveResponse> SaveOrUpdate(PostRequest request)
        {
            CompanySaveResponse response = new CompanySaveResponse();
            
            if (request == null)
            {
                response.Result = CommonEnum.Notfound;
                return response;
            }

            //for existingname

            bool isExisting = await _companyRepository.isExistingName(request.CompanyName);
            if (isExisting)
            {
                response.Result = CommonEnum.NameAlreadyExist; 
                return response;
            }

            if (request.Id == 0) 
            {
                var newCompany = _map.CompanySaveMap(request);
                response.Id = await _companyRepository.Add(newCompany);
                response.Result = CommonEnum.Success;
            }
            else 
            {
                var existingcompany = await _companyRepository.GetAllActive().Where(x => x.Id == request.Id).FirstOrDefaultAsync();
               var companyentity = _map.CompanyUpdateMap(existingcompany, request);
                response.Id = await _companyRepository.Update(companyentity);
                response.Result = CommonEnum.Success;
            }

            return response;
        }


        /// <summary>
        /// GetAllCompany
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        public async Task<PaginatedResponse> GetAllCompany(PaginationRequest request)
        {
            var response = new PaginatedResponse
            {
                PageIndex = request.Index,
                PageSize = request.PageSize
            };

           
            if (request.PageSize <= 0 || request.Index <= 0)
            {
                response.Result = CommonEnum.Notfound; 
                response.PageCount = 0;
                return response;
            }
            //var query = _companyRepository.GetAllActive()
            //    .Include(c => c.Department) 
            //    .AsNoTracking();
            //;



            //method syntax
            var query = _companyRepository.GetAllActive().Where(c => c.IsActive);


            //Query Syntax
            //var query = from company in _companyRepository.GetAllActive()
            //                  where company.IsActive
            //                  select company;




            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(c => c.CompanyName.Contains(request.Search));
            }

            response.TotalRecords = await query.CountAsync();

            
            if (response.TotalRecords == 0)
            {
                response.Result = CommonEnum.Notfound; 
                response.PageCount = 0;
                return response;
            }
                

            response.Data = await query
                .OrderByDescending(x => x.Id)
                .Skip((request.Index - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new PostRequest
                {
                    Id = x.Id,
                    CompanyName = x.CompanyName,
                    DepartmentId = x.DepartmentId,
                  DepartmentName = x.CompanyName != null ? x.Department.DepartmentName : null,
                  
                    IsActive = x.IsActive
                })
                .AsNoTracking()
                .ToListAsync();

          
            response.PageCount = (int)Math.Ceiling((double)response.TotalRecords / request.PageSize);

            response.Result = CommonEnum.Success;
            return response;
        }




        /// <summary>
        /// DeleteCompany
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<DeleteRequest> Delete(DeleteRequest request)
        {
            var company = await _companyRepository.GetById(request.Id);

            if (company == null)
            {
                return new DeleteRequest { Id = request.Id };
            }

            if (request.IsActive == false)
            {
                company.IsActive = false;
                await _companyRepository.Update(company);
            }

            return new DeleteRequest { Id = request.Id };
        }
        /// <summary>
        /// DeleteDepartment
        /// </summary>
        /// <param name="requestDep"></param>
        /// <returns></returns>
     
        public async Task<DeleteRequestDep> DeleteDepartment(DeleteRequestDep requestDep) 
{
    var department = await _companyRepository.GetAllActiveDepartments()
        .FirstOrDefaultAsync(d => d.Id == requestDep.Id);

    if (department == null)
        return new DeleteRequestDep { Id = requestDep.Id, Result = CommonEnum.Notfound };

    if (await _companyRepository.GetAllActiveCompanies()
        .AnyAsync(c => c.DepartmentId == requestDep.Id))
        return new DeleteRequestDep { Id = requestDep.Id, Result = CommonEnum.AlreadyInUse };

    if (!requestDep.IsActive) 
    {
        var updatedDepartment = _map.MapToEntity(requestDep);
        await _companyRepository.Update(updatedDepartment);
    }

    return new DeleteRequestDep { Id = requestDep.Id, Result = CommonEnum.Success };
}


        //LINQ

        public async Task<List<Company>> GetCompanybyDep(int departmentId)
        {
            return await _companyRepository.GetCompanybyDep(departmentId);
        }

        public async Task<List<CompanyLinq>> GetCompanyCount()
        {
            return await _companyRepository.GetCompanyCount();
        }

        public async Task<List<CompanyLinqs>> topFive()
        {
            return await _companyRepository.topFive();
        }
    }
}




