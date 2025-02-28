using System.ComponentModel.DataAnnotations.Schema;
using MyWebApi.Core.Model;

namespace MyWebApi.Core.Dto
{
    public class FabricDeatils
    {
        public int Fabric_id { get; set; }

        public string Fabric_Type { get; set; }

        public bool IsActive { get; set; } = true;
    }
    public class FabricPaginatedResponse: CommonEnumResponse
    {
        public int TotalRecords { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        public List<FabricPostRequest> Data { get; set; } = new List<FabricPostRequest>();

      


    }

    public class FabricPostRequest
    {
        public int Fabric_id { get; set; }

        public string Fabric_Type { get; set; }

        public bool IsActive { get; set; } = true;
    }

    public class FabricPaginationRequest
    {
        public int Index { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Search { get; set; } = "";
    }

    public class FabricSaveResponse : CommonEnumResponse
    {
        public int Fabric_id { get; set; }

    }

    public class DeleteFabricRequest
    {
        public int Fabric_id { get; set; }
        public bool IsActive { get; set; } = true;
    }

    //for construction


    public class DeleteConstructionRequest
    {
        public int Construction_Id { get; set; }
        public bool IsActive { get; set; } = true;
    }


    public class ConstructionPostRequest
    {
        public int Construction_Id { get; set; }

        public string Construction_Type { get; set; }

        public int Fabric_id { get; set; }

      //  public String Fabric_Type { get; set; }

        public bool IsActive { get; set; } = true;
    }
    public class ConstructionPostRequests
    {
        public int Construction_Id { get; set; }

        public string Construction_Type { get; set; }

        public int Fabric_id { get; set; }

        public String Fabric_Type { get; set; }

        public bool IsActive { get; set; } = true;
    }
    public class ConstructionPaginatedResponse: CommonEnumResponse
    {
        public int TotalRecords { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        public List<ConstructionPostRequests> Data { get; set; } = new List<ConstructionPostRequests>();

       
    }


    public class ConstructionPaginationRequest
    {
        public int Index { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Search { get; set; } = "";
    }


    public class ConstructionSaveResponse: CommonEnumResponse
    {
        public int Construction_Id { get; set; }

    }


}
