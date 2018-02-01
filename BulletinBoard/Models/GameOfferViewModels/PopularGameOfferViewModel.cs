using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Models.GameOfferViewModels
{
    public class PopularGameOfferViewModel
    {
        public string GameOfferId { get; set; }
        
        [Display(Name = "Category")]
        public GameCategory GameCategory { get; set; }

        [Display(Name = "Type")]
        public GameType GameType { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }

        public int Visits { get; set; }
    }
}
