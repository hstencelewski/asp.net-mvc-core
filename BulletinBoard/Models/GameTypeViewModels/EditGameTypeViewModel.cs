using System.ComponentModel.DataAnnotations;
using Project.Helpers.CustomValidators;

namespace Project.Models.GameTypeViewModels
{
    public class EditGameTypeViewModel
    {
        public string GameTypeId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [Capitalized]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}
