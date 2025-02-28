using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebApi.Core.Model
{
    public class IndividualCareSymbol
    {
        [Key]
        public int SymbolCode { get; set; }

        public string? Name { get; set; } 
        public string? ImagePathURL { get; set; }
        public string? UniqueId { get; set; }
        public string? imageName { get; set; }
        public string? Description { get; set; }
        public int Symbol_Category_id { get; set; }
        public int CountryId { get; set; }


        public bool IsActive { get; set; } = true;

        [ForeignKey("Symbol_Category_id")]
        [InverseProperty("IndividualCareSymbol")]
        public virtual Symbol_Category_Master Symbol_Category_Master { get; set; }

        [ForeignKey("CountryId")]
        [InverseProperty("IndividualCareSymbols")]
        public virtual Country Country { get; set; }
    }
}
