using Microsoft.EntityFrameworkCore;
using MyWebApi.Core.Dto;
using MyWebApi.Core.Model;
using MyWebApi.Interfaces;

namespace MyWebApi.Infrastructure.Repository___service
{
    public class FabricService:IFabricService
    {
        private readonly IFabricRepository _fabricRepository;
        private readonly CMap _map;

        public FabricService(IFabricRepository repository)
        {
            _fabricRepository = repository;
            _map = new CMap();
        }

        public async Task<DeleteFabricRequest> Delete(DeleteFabricRequest requests)
        {
            var Fabric = await _fabricRepository.GetById(requests.Fabric_id);
            if (Fabric == null)
            {
                return new DeleteFabricRequest { Fabric_id = requests.Fabric_id};
            }
            if (requests.IsActive == false)
            {
                Fabric.IsActive = false;
                await _fabricRepository.Update(Fabric);
            }
            return new DeleteFabricRequest { Fabric_id = requests.Fabric_id };


        }

        public async Task<DeleteConstructionRequest> DeleteConstruction(DeleteConstructionRequest requests)
        {
            var construct = await _fabricRepository.GetByIdConstruction(requests.Construction_Id);
            if (construct == null)
            {
                return new DeleteConstructionRequest { Construction_Id = requests.Construction_Id };
            }
            if(requests.IsActive==false)
            {
                construct.IsActive= false;
                await _fabricRepository.Update(construct);
            }
            return new DeleteConstructionRequest { Construction_Id=requests.Construction_Id};
        }

        public async Task<ConstructionPaginatedResponse> GetAllConstruction(ConstructionPaginationRequest requests)
        {
            var response = new ConstructionPaginatedResponse
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
            var query = _fabricRepository.GetAllActiveConstruction()
            ;

            if (!string.IsNullOrEmpty(requests.Search))
            {
                query = query.Where(c => c.Construction_Type.Contains(requests.Search));
            }
            response.TotalRecords = await query.CountAsync();

            if (response.TotalRecords == 0)
            {
                response.Result = CommonEnum.Notfound;
                response.PageCount = 0;
                return response;
            }

            response.Data=await query
                  .OrderByDescending(x => x.Construction_Id) 
                  .Skip((requests.Index - 1) * requests.PageSize) 
                  .Take(requests.PageSize) 
                  .Select(x => new ConstructionPostRequests
               {
                    Construction_Id = x.Construction_Id,
                    Construction_Type = x.Construction_Type,
                    Fabric_id = x.Fabric_id,
                   
                    Fabric_Type=x.Fabric_id!=null?x.FabricTypeMaster.Fabric_Type:null,
                       IsActive = x.IsActive
               })
                          .AsNoTracking() 
                 .ToListAsync();

            response.PageCount = (int)Math.Ceiling((double)response.TotalRecords / requests.PageSize);
            response.Result = CommonEnum.Success;

            return response;
        }

        public async Task<FabricPaginatedResponse> GetAllFabric(FabricPaginationRequest requests)
        {
            var response = new FabricPaginatedResponse
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
            var query = _fabricRepository.GetAllActive()
                .Include(c => c.Constructions)
                .AsNoTracking();
            ;

            if (!string.IsNullOrEmpty(requests.Search))
            {
                query = query.Where(c => c.Fabric_Type.Contains(requests.Search));
            }
            response.TotalRecords = await query.CountAsync();

            if (response.TotalRecords == 0)
            {
                response.Result = CommonEnum.Notfound;
                response.PageCount = 0;
                return response;
            }
            response.Data = await query
               .OrderByDescending(x => x.Fabric_id)
               .Skip((requests.Index - 1) * requests.PageSize)
               .Take(requests.PageSize)
               .Select(x => new FabricPostRequest
               {
                   Fabric_id = x.Fabric_id,
                 Fabric_Type = x.Fabric_Type,
                   IsActive = x.IsActive
               })
               .AsNoTracking()
               .ToListAsync();

            response.PageCount = (int)Math.Ceiling((double)response.TotalRecords / requests.PageSize);

            response.Result = CommonEnum.Success; 
            return response;


        }

        public async Task<FabricSaveResponse> SaveOrUpdate(FabricPostRequest requests)
        {
            FabricSaveResponse response = new FabricSaveResponse();

            if(requests==null || requests.Fabric_id!=0)
            {
                response.Result= CommonEnum.Notfound;
                return response;
            }
         //for existingname
          bool isExisting = await _fabricRepository.isExistingName(requests.Fabric_Type);
            if(isExisting)
            {
                response.Result = CommonEnum.NameAlreadyExist;
                return response;
             }
          if(requests.Fabric_id==0)
            {
                var newFabric = _map.FabriceSaveMap(requests);
                response.Fabric_id = await _fabricRepository.Add(newFabric);
                response.Result = CommonEnum.Success;
            }
            else
            {
                var existingfabric = await _fabricRepository.GetAllActive().Where(x => x.Fabric_id == requests.Fabric_id).FirstOrDefaultAsync();
                var fabricentity = _map.FabricUpdateMap(existingfabric,requests);
                response.Fabric_id = await _fabricRepository.Update(fabricentity);
                response.Result = CommonEnum.Success;
            }
            return response;
                 }
        public async Task<ConstructionSaveResponse> SaveOrUpdateConstruction(ConstructionPostRequest requests)
                            {
           ConstructionSaveResponse response= new ConstructionSaveResponse();

            if (requests == null || requests.Construction_Id != 0)
            {
                response.Result = CommonEnum.Notfound;
                return response;
            }
            bool isExist = await _fabricRepository.isExistingName(requests.Construction_Type);
            if (isExist)
            {
                response.Result = CommonEnum.NameAlreadyExist;
                return response;
            }
            if (requests.Construction_Id == 0)
            {
                var newcon = _map.ConstructionSaveMap(requests);
                response.Construction_Id = await _fabricRepository.Add(newcon);
              
                response.Result = CommonEnum.Success;
                
            }
            else
            {
                var existingcon = await _fabricRepository.GetAllActiveConstruction()
                    .Where(x => x.Construction_Id == requests.Construction_Id)
            .FirstOrDefaultAsync();

                var constructionEntity = _map.ConstructionUpdateMap(existingcon, requests);
                response.Construction_Id = await _fabricRepository.Update(constructionEntity);
                
                response.Result = CommonEnum.Success;
            }
            return response;
        }
    }
}

