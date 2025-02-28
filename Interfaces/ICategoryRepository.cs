using MyWebApi.Core.Model;

namespace MyWebApi.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Symbol_Category_Master> GetById(int Id);
        Task<int>Add(Symbol_Category_Master master);

        Task<int>Update(Symbol_Category_Master master);

        IQueryable<Symbol_Category_Master>GetAllActive();

        Task<int> Count(IQueryable<Symbol_Category_Master> query);

        Task<bool> isExist(String name);

        //individual care symbol

        Task<IndividualCareSymbol> GetByIdSymbol(int Id);
        Task<int> AddSymbol(IndividualCareSymbol master);

        Task<int> UpdateSymbol(IndividualCareSymbol master);

        IQueryable<IndividualCareSymbol> GetAllActiveSymbol();

        Task<int> CountSymbol(IQueryable<IndividualCareSymbol> query);

        Task<bool> isExistSymbol(String name);

        //country

        Task<Country> GetByIdCountry(int Id);
        Task<int> AddCountry(Country master);

        Task<int> UpdateCountry(Country master);

        IQueryable<Country> GetAllActiveCountry();

        Task<int> CountCountry(IQueryable<Country> query);

        Task<bool> isExistsCountry(String name);

    }
}
