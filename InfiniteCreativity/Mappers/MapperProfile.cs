using AutoMapper;
using InfiniteCreativity.Models;
using InfiniteCreativity.Models.Armor;
using InfiniteCreativity.Models.DTO;
using InfiniteCreativity.Models.DTO.Armor;
using InfiniteCreativity.Models.DTO.Game;
using InfiniteCreativity.Models.DTO.Message;
using InfiniteCreativity.Models.DTO.Weapon;
using InfiniteCreativity.Models.Materials;
using InfiniteCreativity.Models.Weapons;

namespace InfiniteCreativity.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Item, ShowItemDTO>().IncludeAllDerived();
            CreateMap<Equippable, ShowEquippableDTO>();
            CreateMap<Stackable, ShowStackableDTO>();

            CreateMap<Weapon, ShowWeaponDTO>();
            CreateMap<Melee, ShowMeleeDTO>();
            CreateMap<Ranged, ShowRangedDTO>();

            CreateMap<Armor, ShowArmorDTO>();
            CreateMap<Head, ShowHeadDTO>();
            CreateMap<Shoulder, ShowShoulderDTO>();
            CreateMap<Chest, ShowChestDTO>();
            CreateMap<Hand, ShowHandDTO>();
            CreateMap<Leg, ShowLegDTO>();
            CreateMap<Boot, ShowBootDTO>();

            CreateMap<Material, ShowMaterialDTO>();

            CreateMap<Player, CreatePlayerDTO>().ReverseMap();
            CreateMap<Player, ShowGamePlayerDTO>();
            CreateMap<Player, ShowWalletDTO>();
            CreateMap<Player, ShowPlayerDTO>();

            CreateMap<Character, CreateCharacterDTO>().ReverseMap();
            CreateMap<Character, ShowCharacterDTO>();
            CreateMap<Character, ShowEquipmentDTO>();
            CreateMap<Character, ShowGameCharacterDTO>();

            CreateMap<Quest, ShowQuestDTO>();

            CreateMap<Listing, ShowListingDTO>();

            CreateMap<Message, ShowMessageDTO>();

            /*CreateMap<Item, ShowMeleeDTO>().ConstructUsing((source, context) => context.Mapper.Map<ShowMeleeDTO>(source as Melee));*/
        }
    }
}
