using System.ComponentModel.DataAnnotations;

namespace MyWebApi.Core.Model
{
    public class DyeStuffs
    {
        [Key]
        public int No { get; set; } 

        public string Dyestuffs { get; set; }
        public bool IsActive { get; set; } = true;

        public int CreatedBy { get; set; }

      
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public int UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;


    }
}
