using Azure.Core;
using Microsoft.EntityFrameworkCore;
using MyWebApi.Core.Dto;
using MyWebApi.Core.Model;
using MyWebApi.Interfaces;

namespace MyWebApi.Infrastructure.Repository___service
{
    public class StuffService:IStuffService
    {
        private readonly IStuffRepo _stuffRepo;
        private readonly StuffMapping _stuffMapping;
        private readonly IAPIUserContext _applicationContext;

        public StuffService(IStuffRepo repo, IAPIUserContext applicationContext)
        {
            _stuffRepo = repo;
            _stuffMapping = new StuffMapping();
            _applicationContext = applicationContext;

        }

        public async Task<StuffDeleteRequest> DeleteStuff(StuffDeleteRequest requests)
        {
            var Stuff = await _stuffRepo.GetByIdStuffs(requests.No);
            if (Stuff == null)
            {
                return new StuffDeleteRequest { No = requests.No };
            }
            if (requests.IsActive == false)
            {
                Stuff.IsActive = false;
                await _stuffRepo.UpdateStuffs(Stuff);
            }
            return new StuffDeleteRequest { No = requests.No };
        }

        public async Task<StuffPaginationResponse> GetAllStuff(StuffPaginationRequest requests)
        {
            int userid = _applicationContext.UserId;
            var response = new StuffPaginationResponse
            {
                PageIndex = requests.Index,
                PageSize = requests.PageSize
            };
            if (requests.PageSize <= 0 || requests.Index <= 0)
            {
                response.Result = CommonEnum.Notfound;
                response.PageCount = 0;
                return response;
            }
            var query = _stuffRepo.GetAllActiveStuffs().Where(x=>x.CreatedBy==userid);
            

            if (!string.IsNullOrEmpty(requests.Search))
            {
                query = query.Where(c => c.Dyestuffs.Contains(requests.Search));
            }
            response.TotalRecords = await query.CountAsync();

            if (response.TotalRecords == 0)
            {
                response.Result = CommonEnum.Notfound;
                response.PageCount = 0;
                return response;
            }

            response.Data = await query
                  .OrderByDescending(x => x.No)
                  .Skip((requests.Index - 1) * requests.PageSize)
                  .Take(requests.PageSize)
                  .Select(x => new StuffPostRequest
                  {
                      No=x.No,
                    Dyestuffs=x.Dyestuffs,
                      IsActive = x.IsActive
                  })
                          .AsNoTracking()
                 .ToListAsync();

            response.PageCount = (int)Math.Ceiling((double)response.TotalRecords / requests.PageSize);
            response.Result = CommonEnum.Success;

            return response;
        
        }

        public async  Task<StuffSaveResponse> SaveOrUpdateStuff(StuffPostRequest requests)
        {

            var response = new StuffSaveResponse();

            if (requests == null)
            {
                response.Result = CommonEnum.Notfound;
                return response;
            }

            // Check for existing name
            if (await _stuffRepo.isExistsStuffs(requests.Dyestuffs))
            {
                response.Result = CommonEnum.NameAlreadyExist;
                return response;
            }

            if (requests.No == 0)
            {
                var entity = _stuffMapping.StuffSaveMap(requests, _applicationContext.UserId);
                response.No = await _stuffRepo.AddStuffs(entity);
                response.Result = CommonEnum.Success;
            }
            else
            {
                var existingStuff = await _stuffRepo.GetAllActiveStuffs().FirstOrDefaultAsync(x => x.No == requests.No);
                var entity = _stuffMapping.StuffUpdateMap(existingStuff, requests,_applicationContext.UserId);
                response.No = await _stuffRepo.UpdateStuffs(entity);
                response.Result = CommonEnum.Success;
            }

            return response;
        }

    }
    
}
