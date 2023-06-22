using AutoMapper;
using InfiniteCreativity.Models;
using InfiniteCreativity.Models.DTO;
using InfiniteCreativity.Models.Weapons;

namespace InfiniteCreativity.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Item, ShowItemDTO>().IncludeAllDerived();
            CreateMap<Weapon, ShowWeaponDTO>();
            CreateMap<Melee, ShowMeleeDTO>();
            CreateMap<Ranged, ShowRangedDTO>();

            CreateMap<Player, CreatePlayerDTO>().ReverseMap();
            CreateMap<Character, CreateCharacterDTO>().ReverseMap();
            CreateMap<Quest, ShowQuestDTO>();
            /*CreateMap<Item, ShowMeleeDTO>().ConstructUsing((source, context) => context.Mapper.Map<ShowMeleeDTO>(source as Melee));*/
        }
    }
}
