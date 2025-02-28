using Microsoft.EntityFrameworkCore;
using MyWebApi.Core.Model;
using MyWebApi.Infrastructure.Data;
using MyWebApi.Interfaces;

namespace MyWebApi.Infrastructure.Repository___service
{
    public class StuffRepository:IStuffRepo
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public StuffRepository(ApplicationDbContext context)
        {
            _applicationDbContext=context;
        }

        public async  Task<int> AddStuffs(DyeStuffs master)
        {
            await _applicationDbContext.DyeStuffs.AddAsync(master);
            await _applicationDbContext.SaveChangesAsync();
            return master.No;
        }

        public async Task<int> CountStuffs(IQueryable<DyeStuffs> query)
        {
            return query.Count();
        }

        public IQueryable<DyeStuffs> GetAllActiveStuffs()
        {
            return _applicationDbContext.DyeStuffs.Where(c => c.IsActive);
        }

        public async Task<DyeStuffs> GetByIdStuffs(int Id)
        {
            return await _applicationDbContext.DyeStuffs.FirstOrDefaultAsync(c => c.No == Id);
        }

        public async Task<bool> isExistsStuffs(string name)
        {
            return await _applicationDbContext.DyeStuffs.AnyAsync(c => c.Dyestuffs.Trim() == name.Trim());
        }

        public async Task<int> UpdateStuffs(DyeStuffs master)
        {
            _applicationDbContext.DyeStuffs.Update(master);
            await _applicationDbContext.SaveChangesAsync();
            return master.No;
        }
    }
}
