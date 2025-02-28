namespace MyWebApi.Core.Model
{
    public class DyeTypes
    {
        public int Id { get; set; }
        public string DyeTypeName { get; set; }

        public String? DyeTypeCode { get; set; }

        public bool IsActive { get; set; }=true;
        public int CustomerId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public int UpdatedBy { get; set; }

        public DateTime UpdatedOn{ get; set; } = DateTime.UtcNow;



    }
}
