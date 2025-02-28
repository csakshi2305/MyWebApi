using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebApi.Core.Model
{
    [Table("Company")]

    public class Company
    {
        public int Id { get; set; }

        public string CompanyName { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        public int? DepartmentId {get;set;}

        public virtual Department? Department { get; set; }

        //public int CompanyCount { get; set; }
    }
}
