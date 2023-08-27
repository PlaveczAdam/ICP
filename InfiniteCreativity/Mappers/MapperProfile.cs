﻿using AutoMapper;
using InfiniteCreativity.DTO;
using InfiniteCreativity.DTO.Armor;
using InfiniteCreativity.DTO.Game;
using InfiniteCreativity.DTO.Message;
using InfiniteCreativity.DTO.Weapon;
using InfiniteCreativity.Models.CoreNS;
using InfiniteCreativity.Models.CoreNS.ArmorNs;
using InfiniteCreativity.Models.CoreNS.Materials;
using InfiniteCreativity.Models.CoreNS.Weapons;
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
            CreateMap<Character, ShowGameCharacterDTO>();
            CreateMap<Character, ShowCharacterWithStatDTO>();

            CreateMap<Enemy, ShowGameEnemyDTO>();
            CreateMap<Boss, ShowGameBossDTO>();

            CreateMap<Quest, ShowQuestDTO>();

            CreateMap<Listing, ShowListingDTO>();

            CreateMap<Message, ShowMessageDTO>();

            /*CreateMap<Item, ShowMeleeDTO>().ConstructUsing((source, context) => context.Mapper.Map<ShowMeleeDTO>(source as Melee));*/
        }
    }
}
