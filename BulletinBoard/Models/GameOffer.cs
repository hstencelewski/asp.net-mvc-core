using System;

namespace Project.Models
{
    public class GameOffer
    {
        public string GameOfferId { get; set; }

        public GameCategory GameCategory { get; set; }

        public GameType GameType { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }

        public int Visits { get; set; }
    }
}
