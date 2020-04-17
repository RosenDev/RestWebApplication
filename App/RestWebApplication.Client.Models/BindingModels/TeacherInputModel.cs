using System.ComponentModel.DataAnnotations;

namespace RestWebApplication.Client.Models.BindingModels
{
    public class TeacherInputModel
    {
        [Required]
        [StringLength(20,MinimumLength = 3)]
        public string Name { get; set; }

        public int Age { get; set; }

        [Required]
        [StringLength(150,MinimumLength = 13)]
        public string Subject { get; set; }
        
    }
}