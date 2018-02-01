namespace Project.Models.GameOfferViewModels
{
    public class DeleteGameOfferViewModel
    {
        public string GameOfferId { get; set; }

        public GameCategory GameCategory { get; set; }

        public GameType GameType { get; set; }

        public string Title { get; set; }
    }
}
