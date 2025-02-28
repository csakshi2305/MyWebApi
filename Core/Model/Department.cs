namespace MyWebApi.Core.Model
{
    public class Department
    {
        public int Id { get; set; }

        public string DepartmentName { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<Company> Companies { get; set; } = new List<Company>();

    }
}
