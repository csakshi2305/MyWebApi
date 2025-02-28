using MyWebApi.Core.Model;

namespace MyWebApi.Interfaces
{
    public interface IStuffRepo
    {
        Task<DyeStuffs> GetByIdStuffs(int Id);
        Task<int> AddStuffs(DyeStuffs master);

        Task<int> UpdateStuffs(DyeStuffs master);

        IQueryable<DyeStuffs> GetAllActiveStuffs();

        Task<int> CountStuffs(IQueryable<DyeStuffs> query);

        Task<bool> isExistsStuffs(String name);
    }
}
