using System.ComponentModel.DataAnnotations;
using Project.Helpers.CustomValidators;

namespace Project.Models.GameCategoryViewModels
{
    public class EditGameCategoryViewModel
    {
        public string GameCategoryId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [Capitalized]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}
