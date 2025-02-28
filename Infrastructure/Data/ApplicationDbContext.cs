using Microsoft.EntityFrameworkCore;
using MyWebApi.Core.Model;

namespace MyWebApi.Infrastructure.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }
        public virtual DbSet<DyeTypes> DyeTypes { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Construction> Construction { get; set; }
        public virtual DbSet<FabricTypeMaster> FabricTypeMaster { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<UserEmail> Users { get; set; }
        public virtual DbSet<DyeStuffs> DyeStuffs { get; set; }
        public virtual DbSet<IndividualCareSymbol> IndividualCareSymbol { get; set; }
        public virtual DbSet<Symbol_Category_Master> SymbolCategoryMaster { get; set; }
        
    }
}
