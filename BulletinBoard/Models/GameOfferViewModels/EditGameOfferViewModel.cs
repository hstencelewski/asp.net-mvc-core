using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Project.Helpers.CustomValidators;

namespace Project.Models.GameOfferViewModels
{
    public class EditGameOfferViewModel
    {
        public string GameOfferId { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string GameCategoryId { get; set; }

        [Required]
        [Display(Name = "Type")]
        public string GameTypeId { get; set; }

        [Required]
        [Capitalized]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Capitalized]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Range(1900, 2018, ErrorMessage = "Year must be between 1900 and 2018")]
        [Display(Name = "Year of Release")]
        public int Year { get; set; }

        public IEnumerable<GameCategory> GameCategories { get; set; }

        public IEnumerable<GameType> GameTypes { get; set; }
    }
}
