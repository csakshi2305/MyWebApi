using MyWebApi.Core.Model;

namespace MyWebApi.Core.Dto
{
    public class CategoryDetails
    {
        public int Symbol_Category_Id { get; set; }

        public string Symbol_Category_Name { get; set; }

        public bool IsActive { get; set; } = true;

    }

    public class CountryPaginatedResponse: CommonEnumResponse
    {
        public int TotalRecords { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        public List<CountryPostRequests> Data { get; set; } = new List<CountryPostRequests>();

   
    }
    public class IndividualPaginatedResponse: CommonEnumResponse
    {
        public int TotalRecords { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        public List<IndividualPostRequest> Data { get; set; } = new List<IndividualPostRequest>();

       
    }
    public class CategoryPaginatedResponse : CommonEnumResponse
    {
        public int TotalRecords { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        public List<CategoryPostRequest> Data { get; set; } = new List<CategoryPostRequest>();

      
    }

    public class CategoryPaginationRequest
    {
        public int Index { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Search { get; set; } = "";

    }

    public class CountryPaginationRequest
    {
        public int Index { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Search { get; set; } = "";
    }
    public class IndividualPaginationRequest
    {
        public int Index { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Search { get; set; } = "";
    }

    public class CategoryPostRequest
    {
        public int Symbol_Category_Id { get; set; }

        public string Symbol_Category_Name { get; set; }

        public bool IsActive { get; set; } = true;

    }

    public class IndividualPostRequest
    {
        public int SymbolCode { get; set; }
        public string Name { get; set; }
        public string ImagePathURL { get; set; }

        public string UniqueId { get; set; }

        public string imageName { get; set; }

        public string Description { get; set; }
        public int Symbol_Category_id { get; set; }

        public string Symbol_Category_Name { get; set; }

        public int CountryId { get; set; }

       public string CountryName { get; set; }

        public bool IsActive { get; set; } = true;
    }

    public class IndividualPostRequests
    {
        public int SymbolCode { get; set; }
        public string Name { get; set; }
        public string ImagePathURL { get; set; }

        public string UniqueId { get; set; }


        public string imageName { get; set; }

        public string Description { get; set; }
        public int Symbol_Category_id { get; set; }

        public int CountryId { get; set; }

      

        public bool IsActive { get; set; } = true;
    }

    public class CountryPostRequests
    {
        public int CountryId { get; set; }

        public string CountryName { get; set; }

        public bool IsActive { get; set; } = true;
    }

    public class CategorySaveResponse: CommonEnumResponse
    {
        public int Symbol_Category_Id { get; set; }



    }
    public class CountrySaveResponse: CommonEnumResponse
    {
        public int CountryId { get; set; }

     
    }
    public class IndividualSaveResponse: CommonEnumResponse
    {
        public int SymbolCode { get; set; }
    
       
    }


    public class CategoryDeleteRequest
    {
        public int Symbol_Category_Id { get; set; }
        public bool IsActive { get; set; } = true;

    }

    public class IndividualDeleteRequest
    {
        public int SymbolCode { get; set; }

        public bool IsActive { get; set; } = true;

    }

    public class CountryDeleteRequest
    {
        public int CountryId { get; set; }

        public bool IsActive { get; set; }
    }
}
