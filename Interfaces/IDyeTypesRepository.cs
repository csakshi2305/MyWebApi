using MyWebApi.Core.Model;

namespace MyWebApi.Interfaces
{
    public interface IDyeTypesRepository
    {
        Task<DyeTypes> GetByIdDyeTypes(int Id);
        Task<int> AddDyeTypes(DyeTypes master);

        Task<int> UpdateDyeTypes(DyeTypes master);

        IQueryable<DyeTypes> GetAllActiveDyeTypes();
    
          
      
    Task<int> CountDyeTypes(IQueryable<DyeTypes> query);

        Task<DyeTypes> GetMaxDyeTypeAsync();




   
        Task<bool> isExistsDyeTypes(String name);
    }
}
