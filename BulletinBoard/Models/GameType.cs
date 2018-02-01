using System.Collections.Generic;

namespace Project.Models
{
    public class GameType
    {
        public string GameTypeId { get; set; }

        public string Name { get; set; }

        public ICollection<GameOffer> GameOffers { get; set; }
    }
}
