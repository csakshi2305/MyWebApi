using MyWebApi.Core.Model;

namespace MyWebApi.Core.Dto
{
    public class StuffDetails
    {
    }

    public class StuffPaginationResponse : CommonEnumResponse
    {
        public int TotalRecords { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        public List<StuffPostRequest> Data { get; set; } = new List<StuffPostRequest>();
    }

    public class StuffPaginationRequest
    {
        public int Index { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Search { get; set; } = "";
    }

    public class StuffSaveResponse : CommonEnumResponse
    {
        public int No { get; set; }
    }

    public class StuffPostRequest
    {
        public int No { get; set; }

        public string Dyestuffs { get; set; }

        public bool IsActive { get; set; } = true;
    }

    public class StuffDeleteRequest
    {
        public int No { get; set; }

        public bool IsActive { get; set; }
    }
}
