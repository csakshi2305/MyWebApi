using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebApi.Core.Model
{
    public class FabricTypeMaster
    {
        [Key]
        public int Fabric_id { get; set; }

        public string Fabric_Type { get; set; }

        public bool IsActive { get; set; } = true;


        [InverseProperty("FabricTypeMaster")]
        public virtual ICollection<Construction> Constructions { get; set; }
    }
}
