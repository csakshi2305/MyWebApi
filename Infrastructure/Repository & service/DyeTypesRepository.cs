using Microsoft.EntityFrameworkCore;
using MyWebApi.Core.Model;
using MyWebApi.Infrastructure.Data;
using MyWebApi.Interfaces;

namespace MyWebApi.Infrastructure.Repository___service
{
    public class DyeTypesRepository:IDyeTypesRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DyeTypesRepository(ApplicationDbContext context)
        {
            _applicationDbContext = context;
        }

        public async Task<int> AddDyeTypes(DyeTypes master)
        {
            await _applicationDbContext.DyeTypes.AddAsync(master);
            await _applicationDbContext.SaveChangesAsync();
            return master.Id;
        }

        public async Task<int> CountDyeTypes(IQueryable<DyeTypes> query)
        {
            return query.Count();
        }

        public IQueryable<DyeTypes> GetAllActiveDyeTypes()
        {
            return _applicationDbContext.DyeTypes.Where(c=>c.IsActive);
        }

        public async Task<DyeTypes> GetByIdDyeTypes(int Id)
        {
            return await _applicationDbContext.DyeTypes.FirstOrDefaultAsync(c=>c.Id==Id);
        }

     

        public async Task<bool> isExistsDyeTypes(string name)
        {
            return await _applicationDbContext.DyeTypes.AnyAsync(c => c.DyeTypeName.Trim() == name.Trim());
        }

        public async Task<int> UpdateDyeTypes(DyeTypes master)
        {
            _applicationDbContext.DyeTypes.Update(master);
            await _applicationDbContext.SaveChangesAsync();
            return master.Id;
        }
      
        public async Task<DyeTypes> GetMaxDyeTypeAsync()
        {
            return await _applicationDbContext.DyeTypes
                .Where(x => x.IsActive) 
                .OrderByDescending(x => x.DyeTypeCode) 
                .FirstOrDefaultAsync(); 
        }




    }
}
