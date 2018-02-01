using System.ComponentModel.DataAnnotations;

namespace Project.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [RegularExpression(@"^([0-9]{9})$", ErrorMessage = "Error Phone number should contain 9 digits")]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }
    }
}
