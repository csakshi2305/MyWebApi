using Microsoft.EntityFrameworkCore;
using MyWebApi.Core.Model;
using MyWebApi.Infrastructure.Data;
using MyWebApi.Interfaces;

namespace MyWebApi.Infrastructure.Repository___service
{
    public class FabricRepository:IFabricRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public FabricRepository(ApplicationDbContext context)
        {
            _applicationDbContext = context;    
        }

        public async Task<int> Add(FabricTypeMaster fabricTypeMaster)
        {
            await _applicationDbContext.FabricTypeMaster.AddAsync(fabricTypeMaster);
            await _applicationDbContext.SaveChangesAsync();
            return fabricTypeMaster.Fabric_id;
        }

        public async Task<int> Add(Construction construction)
        {
            await _applicationDbContext.Construction.AddAsync(construction);
            await _applicationDbContext.SaveChangesAsync();
            return construction.Construction_Id;
        }

        public async Task<int> Count(IQueryable<FabricTypeMaster> query)
        {
          return query.Count();
        }

        public async Task<int> Count(IQueryable<Construction> query)
        {
            return query.Count();
        }

        public IQueryable<FabricTypeMaster> GetAllActive()
        {
            return _applicationDbContext.FabricTypeMaster.Where(c=>c.IsActive);
        }

        public IQueryable<Construction> GetAllActiveConstruction()
        {
            return _applicationDbContext.Construction.Where(d=>d.IsActive);
        }

        //public IQueryable<FabricTypeMaster> GetAllActiveFabrics()
        //{
        //    return _applicationDbContext.FabricTypeMaster.Where(c => c.IsActive);
        //}

        public async Task<FabricTypeMaster?> GetById(int id)
        {
            return await _applicationDbContext.FabricTypeMaster.FirstOrDefaultAsync(c=>c.Fabric_id==id);
        }

        public async Task<Construction?> GetByIdConstruction(int id)
        {
            return await _applicationDbContext.Construction.FirstOrDefaultAsync(c=>c.Construction_Id==id);
        }

        public async Task<bool> isExistingName(string fabric_Type)
        {
            return await _applicationDbContext.FabricTypeMaster.AnyAsync(c => c.Fabric_Type.Trim() == fabric_Type.Trim());
        }

        public async Task<bool> IsExistingType(string constructionType)
        {
            return await _applicationDbContext.Construction.AnyAsync(c => c.Construction_Type.Trim() == constructionType.Trim());
        }

        public async Task<int> Update(FabricTypeMaster fabricTypeMaster)
        {
            _applicationDbContext.FabricTypeMaster.Update(fabricTypeMaster);
            await _applicationDbContext.SaveChangesAsync();
            return fabricTypeMaster.Fabric_id;
        }

        public async Task<int> Update(Construction construction)
        {
            _applicationDbContext.Update(construction);
            return await _applicationDbContext.SaveChangesAsync();
        }
    }
}
