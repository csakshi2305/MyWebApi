using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebApi.Core.Model
{
    [Table("SymbolCategoryMaster")]
    public class Symbol_Category_Master
    {
        [Key]
        public int Symbol_Category_Id { get; set; }


        [Required]
         public string Symbol_Category_Name { get; set; }

        public bool IsActive { get; set; } = true;


        [InverseProperty("Symbol_Category_Master")]
        public virtual ICollection<IndividualCareSymbol> IndividualCareSymbol { get; set; }
    }
}
