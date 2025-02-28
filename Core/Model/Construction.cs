using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebApi.Core.Model
{
    public class Construction
    {
        [Key]
        public int Construction_Id { get; set; }

        public string Construction_Type { get; set; }

        public int Fabric_id { get; set; }


        public bool IsActive { get; set; } = true;

        [ForeignKey("Fabric_id")]
        [InverseProperty("Constructions")]
        public virtual FabricTypeMaster FabricTypeMaster { get; set; }

    }
}
