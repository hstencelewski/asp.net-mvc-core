using System.Collections.Generic;

namespace Project.Models
{
    public class GameCategory
    {
        public string GameCategoryId { get; set; }

        public string Name { get; set; }

        public ICollection<GameOffer> GameOffers { get; set; }
    }
}
