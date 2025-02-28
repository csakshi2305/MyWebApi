using MyWebApi.Core.Model;

namespace MyWebApi.Interfaces
{
    public interface IFabricRepository
    {
        Task<int> Add(FabricTypeMaster fabricTypeMaster);
        Task<int> Update(FabricTypeMaster fabricTypeMaster);
        IQueryable<FabricTypeMaster> GetAllActive();
        Task<bool> isExistingName(string fabric_Type);
        Task<FabricTypeMaster?> GetById(int id);
       Task<int> Count(IQueryable<FabricTypeMaster> query);

        //for construction

            Task<Construction?> GetByIdConstruction(int id);
            Task<int> Add(Construction construction);
            Task<int> Update(Construction construction);
            IQueryable<Construction> GetAllActiveConstruction();
            Task<int> Count(IQueryable<Construction> query);
            Task<bool> IsExistingType(string constructionType);


    }
}
