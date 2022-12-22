using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DssDbDemoApp.Models
{
    [Table("Employees")]
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Department")]
        public int FKDeptId { get; set; }
        public string EmpName { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public Department? Department { get; set; }
    }
    [Table("Departments")]
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string DeptName { get; set; }
        public bool IsActive { get; set;}
        public ICollection<Employee> Employees { get; set; }
    }
}
