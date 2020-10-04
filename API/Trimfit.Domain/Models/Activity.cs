using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trimfit.Domain.Abstractions;

namespace Trimfit.Domain.Models
{
    [Table("activity")]
    public class Activity : IDatabaseEntity
    {
        public Activity()
        {
            Category = new List<Category>();
            Employee = new List<Employee>();
        }

        [Key]
        [Column("activity_id")]
        public int Id { get; set; }

        [Column("activity_name")]
        public string Name { get; set; }

        [Column("activity_description")]
        public string Description { get; set; }

        [Column("activity_status")]
        public int Status { get; set; }

        [Column("category_id")]
        public List<int> CategoryId { get; set; }

        [NotMapped]
        public List<Category> Category { get; set; }

        [Column("activity_color")]
        public string Color { get; set; }

        [Column("employee_id")]
        public List<int> EmployeeId { get; set; }

        [NotMapped]
        public List<Employee> Employee { get; set; }
    }
}
