using System.ComponentModel.DataAnnotations;

namespace CoreWebAPI.Model
{
    public class EmployeeModel
    {
        [Required]
        [Key]
        public int EmpId {get; set;}

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string EmpName { get; set; }

        public string City { get; set; }
    }
}
