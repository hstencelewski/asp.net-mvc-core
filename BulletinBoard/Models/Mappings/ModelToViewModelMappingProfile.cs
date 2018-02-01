using AutoMapper;
using Project.Models.GameCategoryViewModels;
using Project.Models.GameOfferViewModels;
using Project.Models.GameTypeViewModels;

namespace Project.Models.Mappings
{
    /// <summary>
    /// Default model to view-model automapper mapping profile.
    /// </summary>
    public class ModelToViewModelMappingProfile : Profile
    {
        public ModelToViewModelMappingProfile()
        {
            CreateMap<GameType, GameTypeViewModel>();
            CreateMap<GameType, CreateGameTypeViewModel>();
            CreateMap<GameType, DeleteGameTypeViewModel>();
            CreateMap<GameType, DetailsGameTypeViewModel>();
            CreateMap<GameType, EditGameTypeViewModel>();

            CreateMap<GameCategory, GameCategoryViewModel>();
            CreateMap<GameCategory, CreateGameCategoryViewModel>();
            CreateMap<GameCategory, DeleteGameCategoryViewModel>();
            CreateMap<GameCategory, DetailsGameCategoryViewModel>();
            CreateMap<GameCategory, EditGameCategoryViewModel>();

            CreateMap<GameOffer, GameOfferViewModel>();
            CreateMap<GameOffer, PopularGameOfferViewModel>();
            CreateMap<GameOffer, CreateGameOfferViewModel>()
                .ForMember(dest => dest.GameCategoryId,
                    opts => opts.MapFrom(src => src.GameCategory.GameCategoryId))
                .ForMember(dest => dest.GameTypeId,
                    opts => opts.MapFrom(src => src.GameType.GameTypeId));
            CreateMap<GameOffer, DeleteGameOfferViewModel>();
            CreateMap<GameOffer, DetailsGameOfferViewModel>();
            CreateMap<GameOffer, EditGameOfferViewModel>()
                .ForMember(dest => dest.GameCategoryId,
                    opts => opts.MapFrom(src => src.GameCategory.GameCategoryId))
                .ForMember(dest => dest.GameTypeId,
                    opts => opts.MapFrom(src => src.GameType.GameTypeId));
        }
    }
}
