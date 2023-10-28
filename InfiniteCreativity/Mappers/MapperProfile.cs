using AutoMapper;
using DTOs;
using DTOs.Game;
using InfiniteCreativity.DTO;
using InfiniteCreativity.DTO.Armor;
using InfiniteCreativity.DTO.Game;
using InfiniteCreativity.DTO.Message;
using InfiniteCreativity.DTO.Weapon;
using InfiniteCreativity.Models.CoreNS;
using InfiniteCreativity.Models.CoreNS.ArmorNs;
using InfiniteCreativity.Models.CoreNS.Materials;
using InfiniteCreativity.Models.CoreNS.Weapons;
using InfiniteCreativity.Models.GameNS;
using InfiniteCreativity.Models.GameNS.Enemys;

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
            CreateMap<Character, ShowGameCharacterDTO>().ForMember(dest => dest.SkillSlots, opt => opt.MapFrom(src => src.SkillSlots.OrderBy(x => x.SlotNumber)));
            CreateMap<Character, ShowCharacterWithStatDTO>();

            CreateMap<GameMapAccessor, ShowGameMapDTO>();
            CreateMap<HexTileDataObject, ShowGameHexTileDataObjectDTO>();

            CreateMap<Enemy, ShowGameEnemyDTO>().IncludeAllDerived();
            CreateMap<Boss, ShowGameBossDTO>();

            CreateMap<Skill, ShowSkillDTO>();
            CreateMap<SkillHolder, ShowSkillHolderDTO>();
            CreateMap<Buff, ShowBuffDTO>();
            CreateMap<Condition, ShowConditionDTO>();

            CreateMap<Battle, ShowBattleStateDTO>();
            CreateMap<BattleParticipant, ShowBattleParticipantDTO>();

            CreateMap<Quest, ShowQuestDTO>();

            CreateMap<Listing, ShowListingDTO>();

            CreateMap<Message, ShowMessageDTO>();

            CreateMap<CharacterSkillSlot, ShowCharacterSkillSlotDTO>();

            /*CreateMap<Item, ShowMeleeDTO>().ConstructUsing((source, context) => context.Mapper.Map<ShowMeleeDTO>(source as Melee));*/
        }
    }
}
