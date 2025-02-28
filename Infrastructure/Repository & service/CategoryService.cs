using Azure.Core;
using Azure;
using Microsoft.EntityFrameworkCore;
using MyWebApi.Core.Dto;
using MyWebApi.Core.Model;
using MyWebApi.Infrastructure.Repository;
using MyWebApi.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Linq;

namespace MyWebApi.Infrastructure.Repository___service
{
    public class CategoryService:ICategoryService
    {

        public readonly ICategoryRepository _categoryRepository;
        private readonly CMap _map;

        public CategoryService(ICategoryRepository repository)
        {
            _categoryRepository = repository;
            _map = new CMap();
        }


        public async Task<CountryDeleteRequest> CountryDelete(CountryDeleteRequest request)
        {
            var countryc= await _categoryRepository.GetByIdCountry(request.CountryId);

            if (countryc == null)
            {
                return new CountryDeleteRequest { CountryId  = request.CountryId };
            }
            if (request.IsActive == false)
            {
                countryc.IsActive = false;
                await _categoryRepository.UpdateCountry(countryc);

            }
            return new CountryDeleteRequest { CountryId = request.CountryId };
        }
        
        /// <summary>
        /// IndividualDelete
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// 

        public async Task<IndividualDeleteRequest> IndividualDelete(IndividualDeleteRequest request)
        {
            var individualCat = await _categoryRepository.GetByIdSymbol(request.SymbolCode);

            if(individualCat==null)
            {
                return new IndividualDeleteRequest { SymbolCode=request.SymbolCode};
            }

            var category = await _categoryRepository.GetById(individualCat.Symbol_Category_id);
            if (category != null && category.IsActive)
            {
               
                throw new InvalidOperationException("Cannot delete");
            }


            if (request.IsActive==false)
            {
                individualCat.IsActive = false;
                await _categoryRepository.UpdateSymbol(individualCat);

            }
            return new IndividualDeleteRequest { SymbolCode=request.SymbolCode};
        }

        /// <summary>
        /// DeleteCategory
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CategoryDeleteRequest> DeleteCategory(CategoryDeleteRequest request)
        {
            var category = await _categoryRepository.GetById(request.Symbol_Category_Id); 
            
            if(category==null)
            {
                return new CategoryDeleteRequest { Symbol_Category_Id=request.Symbol_Category_Id};
            }
            if(request.IsActive==false)
            {
                category.IsActive = false;
                await _categoryRepository.Update(category);
            }
            return new CategoryDeleteRequest { Symbol_Category_Id=request.Symbol_Category_Id};
        }

        /// <summary>
        /// IndividualGetAll
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IndividualPaginatedResponse> IndividualGetAll(IndividualPaginationRequest request)
        {
            var response = new IndividualPaginatedResponse
            {
                PageIndex = request.Index,
                PageSize = request.PageSize
            };
            if (request.PageSize <= 0 || request.Index <= 0)
            {
                response.Result = CommonEnum.Notfound;
                response.PageCount = 0; 
                return response;
            }
            var query = _categoryRepository.GetAllActiveSymbol();

            //if (!string.IsNullOrEmpty(request.Search))
            //{
            //    query = query.Where(c => c.Name.Contains(request.Search));
            //}

            //query = query.Where(c => string.IsNullOrEmpty(request.Search) || c.Name.Contains(request.Search));

            query = query
          .Where(c => c.IsActive)  
          .WhereIf(!string.IsNullOrEmpty(request.Search), c => c.Name.Contains(request.Search));  


            response.TotalRecords = await query.CountAsync();
            if (response.TotalRecords == 0)
            {
                response.Result = CommonEnum.Notfound;
                response.PageCount = 0;
                return response;
            }
            response.Data = await query
               .OrderByDescending(x => x.SymbolCode)
               .Skip((request.Index - 1) * request.PageSize)
               .Take(request.PageSize)
               .Select(x => new IndividualPostRequest
               {
                   SymbolCode = x.SymbolCode,
                   Name = x.Name,
                   ImagePathURL = x.ImagePathURL,
                   UniqueId = x.UniqueId,
                   imageName = x.imageName,
                   Description = x.Description,
                   Symbol_Category_id = x.Symbol_Category_id,
                   Symbol_Category_Name = x.Symbol_Category_id != null ? x.Symbol_Category_Master.Symbol_Category_Name : null,
                    CountryId=x.CountryId,
                   CountryName=x.CountryId!=null ? x.Country.CountryName:null,
                   IsActive = x.IsActive
               })
               .AsNoTracking()
               .ToListAsync();

            response.PageCount = (int)Math.Ceiling((double)response.TotalRecords / request.PageSize);

            response.Result = CommonEnum.Success;
            return response;
        

        }

        /// <summary>
        /// GetAllCategory
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CategoryPaginatedResponse> GetAllCategory(CategoryPaginationRequest request)
        {
            var response = new CategoryPaginatedResponse
            {
                PageIndex = request.Index,
                PageSize = request.PageSize
            };
            if (request.PageSize <= 0 || request.Index <= 0)
            {
                response.Result = CommonEnum.Notfound;
                response.PageCount = 0;
                return response;
            }
            var query = _categoryRepository.GetAllActive();

            //if (!string.IsNullOrEmpty(request.Search))
            //{
            //    query = query.Where(c => c.Symbol_Category_Name.Contains(request.Search));
            //}

            query = query.Where(c => string.IsNullOrEmpty(request.Search) || c.Symbol_Category_Name.Contains(request.Search));

            response.TotalRecords = await query.CountAsync();

            if (response.TotalRecords == 0)
            {
                response.Result = CommonEnum.Notfound;
                response.PageCount = 0;
                return response;
            }
            response.Data = await query
               .OrderByDescending(x => x.Symbol_Category_Id)
               .Skip((request.Index - 1) * request.PageSize)
               .Take(request.PageSize)
               .Select(x => new CategoryPostRequest
               {
                   Symbol_Category_Id = x.Symbol_Category_Id,
                   Symbol_Category_Name = x.Symbol_Category_Name,
                   IsActive = x.IsActive
               })
               .AsNoTracking()
               .ToListAsync();

            response.PageCount = (int)Math.Ceiling((double)response.TotalRecords / request.PageSize);

            response.Result = CommonEnum.Success;
            return response;
        }


        public async Task<CountryPaginatedResponse> CountryGetAll(CountryPaginationRequest request)
        {
            var response = new CountryPaginatedResponse
            {
                PageIndex = request.Index,
                PageSize = request.PageSize
            };
            if (request.PageSize <= 0 || request.Index <= 0)
            {
                response.Result = CommonEnum.Notfound;
                response.PageCount = 0;
                return response;
            }
            var query = _categoryRepository.GetAllActiveCountry();

            //if (!string.IsNullOrEmpty(request.Search))
            //{
            //    query = query.Where(c => c.Symbol_Category_Name.Contains(request.Search));
            //}

            query = query.Where(c => string.IsNullOrEmpty(request.Search) || c.CountryName.Contains(request.Search));

            response.TotalRecords = await query.CountAsync();

            if (response.TotalRecords == 0)
            {
                response.Result = CommonEnum.Notfound;
                response.PageCount = 0;
                return response;
            }
            response.Data = await query
               .OrderByDescending(x => x.CountryId)
               .Skip((request.Index - 1) * request.PageSize)
               .Take(request.PageSize)
               .Select(x => new CountryPostRequests
               {
                   CountryId=x.CountryId,
                   CountryName=x.CountryName,
                   IsActive = x.IsActive
               })
               .AsNoTracking()
               .ToListAsync();

            response.PageCount = (int)Math.Ceiling((double)response.TotalRecords / request.PageSize);

            response.Result = CommonEnum.Success;
            return response;
        }
        



        /// <summary>
        /// IndividualSaveOrUpdate
        /// </summary>
        /// <param name="requests"></param>
        /// <returns></returns>
        public async Task<IndividualSaveResponse> IndividualSaveOrUpdate(IndividualPostRequests requests)
        {
            IndividualSaveResponse response= new IndividualSaveResponse();

            if(requests == null )
            {
                response.Result = CommonEnum.Notfound;
                return response;
            }
            bool exist = await _categoryRepository.isExistSymbol(requests.Name);
            if (exist)
            {
                response.Result = CommonEnum.NameAlreadyExist;
                return response;
            }
            if(requests.SymbolCode==0)
            {
                var newcat= _map.IndividualSaveMap(requests);
                response.SymbolCode = await _categoryRepository.AddSymbol(newcat);
                if(response.SymbolCode>0) // check condition here to success save
                {
                    // entry in new table
                }
                response.Result = CommonEnum.Success;
            }
            else
            {
                var individualexist = await _categoryRepository.GetAllActiveSymbol().Where(x => x.SymbolCode == requests.SymbolCode).FirstOrDefaultAsync();
                var entity = _map.IndividualUpdateMap(individualexist, requests);
                response.SymbolCode = await _categoryRepository.UpdateSymbol(entity);
                response.Result = CommonEnum.Success;
            }
            return response;
         }


        /// <summary>
        /// SaveOrUpdateCategory
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CategorySaveResponse> SaveOrUpdateCategory(CategoryPostRequest request)
        {
            CategorySaveResponse response = new CategorySaveResponse();

            if (request == null )
            {
                response.Result = CommonEnum.Notfound;
                return response;
            }

            //for existingname

            bool isExisting = await _categoryRepository.isExist(request.Symbol_Category_Name);
            if (isExisting)
            {
                response.Result = CommonEnum.NameAlreadyExist;
                return response;
            }
            if (request.Symbol_Category_Id == 0)
            {
                var newCotegory = _map.CategorySaveMap(request);
                response.Symbol_Category_Id = await _categoryRepository.Add(newCotegory);
                response.Result = CommonEnum.Success;
            }
            else
            {
                var existingcategory = await _categoryRepository.GetAllActive().Where(x => x.Symbol_Category_Id == request.Symbol_Category_Id).FirstOrDefaultAsync();
                var entity = _map.CategoryUpdateMap(existingcategory, request);
                response.Symbol_Category_Id = await _categoryRepository.Update(entity);
                response.Result = CommonEnum.Success;
            }

            return response;
        }

        public async Task<CountrySaveResponse> CountrySaveOrUpdate(CountryPostRequests request)
        {
            CountrySaveResponse response = new CountrySaveResponse();

            if (request == null)
            {
                response.Result = CommonEnum.Notfound;
                return response;
            }
            bool isExisting = await _categoryRepository.isExistsCountry(request.CountryName);
            if (isExisting)
            {
                response.Result = CommonEnum.NameAlreadyExist;
                return response;
            }
            if (request.CountryId == 0)
            {
                var newCountry = _map.CountrySaveMap(request);
                response.CountryId = await _categoryRepository.AddCountry(newCountry);
                response.Result = CommonEnum.Success;
            }
            else
            {
                var existingcountry = await _categoryRepository.GetAllActiveCountry().Where(x => x.CountryId == request.CountryId).FirstOrDefaultAsync();
                var entity = _map.CountryUpdateMap(existingcountry, request);
                response.CountryId = await _categoryRepository.UpdateCountry(entity);
                response.Result = CommonEnum.Success;
            }

            return response;
        }
        }
    }

