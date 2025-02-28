using Microsoft.EntityFrameworkCore;
using MyWebApi.Core.Model;
using MyWebApi.Infrastructure.Data;
using MyWebApi.Interfaces;

namespace MyWebApi.Infrastructure.Repository___service
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CategoryRepository(ApplicationDbContext context)
        {
            _applicationDbContext = context;
        }

        public async Task<int> Add(Symbol_Category_Master master)
        {

            await _applicationDbContext.SymbolCategoryMaster.AddAsync(master);
            await _applicationDbContext.SaveChangesAsync();
            return master.Symbol_Category_Id;

            
        }

        public async Task<int> AddCountry(Country master)
        {
            await _applicationDbContext.Country.AddAsync(master);
            await _applicationDbContext.SaveChangesAsync();
            return master.CountryId;
        }

        public async Task<int> AddSymbol(IndividualCareSymbol master)
        {
            await _applicationDbContext.IndividualCareSymbol.AddAsync(master);
            await _applicationDbContext.SaveChangesAsync();
            return master.SymbolCode;
        }

        public async Task<int> Count(IQueryable<Symbol_Category_Master> query)
        {
            return query.Count();
        }

        public async Task<int> CountCountry(IQueryable<Country> query)
        {
            return query.Count();
        }

        public async Task<int> CountSymbol(IQueryable<IndividualCareSymbol> query)
        {
            return query.Count();
        }

        public IQueryable<Symbol_Category_Master> GetAllActive()
        {
            return  _applicationDbContext.SymbolCategoryMaster.Where(c=>c.IsActive);
        }

        public IQueryable<Country> GetAllActiveCountry()
        {
            return _applicationDbContext.Country.Where(c => c.IsActive);
        }

        public IQueryable<IndividualCareSymbol> GetAllActiveSymbol()
        {
            return _applicationDbContext.IndividualCareSymbol.Where(c => c.IsActive);
        }

        public async Task<Symbol_Category_Master> GetById(int Id)
        {
            return await _applicationDbContext.SymbolCategoryMaster.FirstOrDefaultAsync(c=>c.Symbol_Category_Id==Id);
        }

        public async Task<Country> GetByIdCountry(int Id)
        {
            return await _applicationDbContext.Country.FirstOrDefaultAsync(c =>c.CountryId == Id);
        }

        public async Task<IndividualCareSymbol> GetByIdSymbol(int Id)
        {
            return await _applicationDbContext.IndividualCareSymbol.FirstOrDefaultAsync(c => c.SymbolCode == Id);
        }

        public async Task<bool> isExist(string name)
        {
            return await _applicationDbContext.SymbolCategoryMaster.AnyAsync(c=>c.Symbol_Category_Name.Trim()==name.Trim());
        }

        public async Task<bool> isExistsCountry(string name)
        {
            return await _applicationDbContext.Country.AnyAsync(c => c.CountryName.Trim() ==name.Trim());
        }

        public async Task<bool> isExistSymbol(string name)
        {
            return await _applicationDbContext.IndividualCareSymbol.AnyAsync(c =>c.Name.Trim()==name.Trim());
        }

        public async Task<int> Update(Symbol_Category_Master master)
        {
            _applicationDbContext.SymbolCategoryMaster.Update(master);
            await _applicationDbContext.SaveChangesAsync();
            return master.Symbol_Category_Id;

        }

        public async Task<int> UpdateCountry(Country master)
        {
            _applicationDbContext.Country.Update(master);
            await _applicationDbContext.SaveChangesAsync();
            return master.CountryId;
        }

        public async Task<int> UpdateSymbol(IndividualCareSymbol master)
        {
            _applicationDbContext.IndividualCareSymbol.Update(master);
            await _applicationDbContext.SaveChangesAsync();
            return master.SymbolCode;
        }
    }
}
