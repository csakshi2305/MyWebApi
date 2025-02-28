using Microsoft.EntityFrameworkCore;
using MyWebApi.Core.Dto;
using MyWebApi.Core.Model;
using MyWebApi.Infrastructure.Data;
using MyWebApi.Interfaces;

namespace MyWebApi.Infrastructure.Repository___service
{
    public class DyeTypesService : IDyeTypesService
    {
        private readonly IAPIUserContext _applicationContext;
        private readonly IDyeTypesRepository _repository;
        private readonly StuffMapping _stuffMapping;


        public DyeTypesService(IAPIUserContext aPIUserContext, IDyeTypesRepository dyeTypesRepository, StuffMapping stuffMapping)
        {
            _applicationContext = aPIUserContext;
                _repository = dyeTypesRepository;
               _stuffMapping = stuffMapping;
        }


        public async Task<DyeTypeDeleteRequest> DeleteDyeTypes(DyeTypeDeleteRequest requests)
        {
            var Stuff = await _repository.GetByIdDyeTypes(requests.Id);
            if (Stuff == null)
            {
                return new DyeTypeDeleteRequest { Id = requests.Id };
            }
            if (requests.IsActive == false)
            {
                Stuff.IsActive = false;
                await _repository.UpdateDyeTypes(Stuff);
            }
            return new DyeTypeDeleteRequest { Id = requests.Id };
        }

        public async Task<DyeTypePaginationResponse> GetAllDyeTypes(DyeTypePaginationRequest requests)
        {
            int userid = _applicationContext.UserId;
            var response = new DyeTypePaginationResponse
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
            var query = _repository.GetAllActiveDyeTypes().Where(x => x.CreatedBy == userid);


            if (!string.IsNullOrEmpty(requests.Search))
            {
                query = query.Where(c => c.DyeTypeName.Contains(requests.Search));
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
                  .Skip((requests.Index - 1) * requests.PageSize)
                  .Take(requests.PageSize)
                  .Select(x => new DyeTypePostRequest
                  {
                      Id = x.Id,
                      DyeTypeName = x.DyeTypeName,
                      DyeTypeCode=x.DyeTypeCode,
                      IsActive = x.IsActive
                  })
                          .AsNoTracking()
                 .ToListAsync();

            response.PageCount = (int)Math.Ceiling((double)response.TotalRecords / requests.PageSize);
            response.Result = CommonEnum.Success;

            return response;

        }



        public async Task<DyeTypeSaveResponse> SaveOrUpdateDyeTypes(DyeTypePostRequest requests)
        {
            var response = new DyeTypeSaveResponse();

            if (requests == null)
            {
                response.Result = CommonEnum.Notfound;
                return response;
            }

            // Check for existing name
            if (await _repository.isExistsDyeTypes(requests.DyeTypeName))
            {
                response.Result = CommonEnum.NameAlreadyExist;
                return response;
            }

            if (requests.Id == 0)
            {
                var entity = _stuffMapping.DyeTypeSaveMap(requests, _applicationContext.UserId);
                response.Id = await _repository.AddDyeTypes(entity);
                response.Result = CommonEnum.Success;
            }
            else
            {
                var existingStuff = await _repository.GetAllActiveDyeTypes().FirstOrDefaultAsync(x => x.Id == requests.Id);
                var entity = _stuffMapping.DyeTypeUpdateMap(existingStuff, requests, _applicationContext.UserId);
                response.Id = await _repository.UpdateDyeTypes(entity);
                response.Result = CommonEnum.Success;
            }

            return response;
        }


        public async Task<string> GetMaxDyeTypeCode()
        {
            // Get the most recent DyeType
            var maxDyeType = await _repository.GetMaxDyeTypeAsync();

            if (maxDyeType == null || string.IsNullOrEmpty(maxDyeType.DyeTypeCode))
            {
                // If no valid dye type is found, return "DT0001"
                return "DT0001";
            }

            // Extract the numeric part of the DyeTypeCode and increment it
            var lastCode = maxDyeType.DyeTypeCode.Substring(2);  // Exclude "DT"
            if (int.TryParse(lastCode, out int lastNumber))
            {
                return "DT" + (lastNumber + 1).ToString("D4");
            }
            else
            {
                // If there's an issue with the format of the code, return the default code
                return "DT0001";
            }
        }


    }
}

