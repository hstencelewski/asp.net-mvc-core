using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Models.GameOfferViewModels
{
    public class GameOfferViewModel
    {
        public string GameOfferId { get; set; }
       

        [Display(Name = "Category")]
        public GameCategory GameCategory { get; set; }

        [Display(Name = "Type")]
        public GameType GameType { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [Display(Name = "Year of Release")]
        public int Year { get; set; }

        public int Visits { get; set; }
    }
}
