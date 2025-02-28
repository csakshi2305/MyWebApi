using MyWebApi.Core.Model;

namespace MyWebApi.Core.Dto
{
  

    public class Companydetailsdto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

     


    }
    public class Pagination
    {
        public List<Companydetailsdto> Companydetails { get; set; }  
    }


    public class PostRequest
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public int? DepartmentId { get; set; }

        public string DepartmentName { get; set; }


    }
    public class CompanySaveResponse: CommonEnumResponse
    {
        public int Id { get; set; }
    
       
    }



    public class PaginationRequest
    {
        public int Index { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Search { get; set; } = "";
    }
    public class PaginatedResponse: CommonEnumResponse
    {
        public int TotalRecords { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        public List<PostRequest> Data { get; set; } = new List<PostRequest>();

       

       
    }

    public class DeleteRequest
    {
        public int Id { get; set; }
        public bool IsActive { get; set; } = true;

    }

    public class DeleteRequestDep: CommonEnumResponse
    {
        public int Id { get; set; }
        public bool IsActive { get; set; } = true;

   

    }

    public class CompanyResponseDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public int? DepartmentId { get; set; } 
        public string DepartmentName { get; set; } = string.Empty; 
    }

    public class CompanyLinq
    {

        public int? DepartmentId { get; set; }
        public int CompanyCount { get; set; }
    }
   
        public class CompanyLinqs
        {
            public int Id { get; set; }
            public string CompanyName { get; set; } 
            public string DepartmentName { get; set; }
        }
    }

