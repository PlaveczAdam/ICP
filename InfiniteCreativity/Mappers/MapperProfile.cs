using AutoMapper;
using InfiniteCreativity.Models;
using InfiniteCreativity.Models.Armor;
using InfiniteCreativity.Models.DTO;
using InfiniteCreativity.Models.DTO.Armor;
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
            CreateMap<Stackable, ShowStackableDTO>();

            CreateMap<Player, CreatePlayerDTO>().ReverseMap();
            CreateMap<Character, CreateCharacterDTO>().ReverseMap();
            CreateMap<Quest, ShowQuestDTO>();
            CreateMap<Player, ShowWalletDTO>();
            CreateMap<Player, ShowPlayerDTO>();
            CreateMap<Character, ShowCharacterDTO>();
            CreateMap<Character, ShowEquipmentDTO>();

            CreateMap<Listing, ShowListingDTO>();

            CreateMap<Message, ShowMessageDTO>();

            /*CreateMap<Item, ShowMeleeDTO>().ConstructUsing((source, context) => context.Mapper.Map<ShowMeleeDTO>(source as Melee));*/
        }
    }
}
