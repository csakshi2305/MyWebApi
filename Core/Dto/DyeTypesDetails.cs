using MyWebApi.Core.Model;

namespace MyWebApi.Core.Dto
{
    public class DyeTypesDetails
    {

    }

    public class DyeTypePaginationResponse:CommonEnumResponse
    {
        public int TotalRecords { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        public List<DyeTypePostRequest> Data { get; set; } = new List<DyeTypePostRequest>();
    }

    public class DyeTypePaginationRequest
    {
        public int Index { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Search { get; set; } = "";
    }

    public class DyeTypeSaveResponse:CommonEnumResponse
    {
        public int Id { get; set; }
    }

    public class DyeTypePostRequest
    {
        public int Id{ get; set; }

        public string DyeTypeName { get; set; }

        public string DyeTypeCode { get; set; }

        public bool IsActive { get; set; } = true;
    }

    public class DyeTypeDeleteRequest
    {
        public int Id { get; set; }

        public bool IsActive { get; set; }
    }

  
}
