using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trimfit.Models
{
    public class ActivityView
    {
        public int Activity_Id { get; set; }
        public string Activity_Name { get; set; }
        public string Activity_Description { get; set; }
        public int Activity_Status { get; set; }
        public List<int> Category_Id { get; set; }
        public string Activity_Color { get; set; }
        public List<Employee> Employee { get; set; }
    }
}
