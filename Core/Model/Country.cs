using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebApi.Core.Model
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }

        public string CountryName { get; set; }

        public bool IsActive { get; set; } = true;

        [InverseProperty("Country")]
        public virtual ICollection<IndividualCareSymbol> IndividualCareSymbols { get; set; } = new List<IndividualCareSymbol>();
    }
}
