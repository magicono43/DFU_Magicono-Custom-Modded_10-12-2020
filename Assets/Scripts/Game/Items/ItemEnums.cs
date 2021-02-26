// Project:         Daggerfall Tools For Unity
// Copyright:       Copyright (C) 2009-2020 Daggerfall Workshop
// Web Site:        http://www.dfworkshop.net
// License:         MIT License (http://www.opensource.org/licenses/mit-license.php)
// Source Code:     https://github.com/Interkarma/daggerfall-unity
// Original Author: LypyL (lypyl@dfworkshop.net)
// Contributors:    Gavin Clayton (interkarma@dfworkshop.net)
//
// Notes:
//

namespace DaggerfallWorkshop.Game.Items
{
    // This data is primarily for backward compatibility with classic items.
    // Enums should not be changed as this will effect items imported from classic saves.

    // Daggerfall item templates are addressed using two integers forming an index pair:
    // First integer is item group or category.
    // Second integer is index to item template within that group.

    // Enum order follows Daggerfall's item index pairs and have values matching template index.
    // Some items may appear in multiple groups with a different purpose in each group.

    /// <summary>
    /// Base group of item.
    /// </summary>
    public enum ItemGroups
    {
        None = -1,
        Drugs = 0,
        UselessItems1 = 1,
        Armor = 2,
        Weapons = 3,
        MagicItems = 4,
        Artifacts = 5,
        MensClothing = 6,
        Books = 7,
        Furniture = 8,
        UselessItems2 = 9,
        ReligiousItems = 10,
        Maps = 11,
        WomensClothing = 12,
        Paintings = 13,
        Gems = 14,
        MiscPlantIngredients = 15,
        FlowerPlantIngredients = 16,
        FruitPlantIngredients = 17,
        AnimalPartIngredients = 18,
        CreatureIngredients = 19,
        SolventIngredients = 20,
        MetalIngredients = 21,
        Transportation = 22,
        Deeds = 23,
        Jewellery = 24,
        QuestItems = 25,
        MiscItems = 26,
        Currency = 27,
        Biography_Books = 28,
        Faction_Related_Books = 29,
        Fiction_Books = 30,
        History_Lore_Books = 31,
        Humor_Books = 32,
        Instruction_Research_Books = 33,
        Journals_Logs_Books = 34,
        Notes_Letters_Books = 35,
        Plays_Poetry_Riddles_Books = 36,
        Politics_Law_Books = 37,
        Religion_Prophecy_Books = 38,
        Travel_Books = 39,
        Informational_Books = 40,
        No_Topic_Books = 41,
        Tiara_Jewelry = 42,
        Crown_Jewelry = 43,
        Ring_Jewelry = 44,
        Earing_Jewelry = 45,
        Neck_Jewelry = 46,
        Bracelet_Jewelry = 47,
        General_Tools = 48,
        Repair_Tools = 49,
        Musical_Instruments = 50,
        Containers = 51,
        Liquid_Containers = 52,
        Magic_Amplifiers = 53,
        Flavor_Tools = 54,
        Toys = 55,
        Sex_Toys = 56,
        Corpse_Parts = 57,
        Occult_Objects = 58,
        Junk = 59,
    }

    /// <summary>
    /// Weapon material values.
    /// </summary>
    public enum WeaponMaterialTypes
    {
        None        = -1,
        Iron        = 0x0000,
        Steel       = 0x0001,
        Silver      = 0x0002,
        Elven       = 0x0003,
        Dwarven     = 0x0004,
        Mithril     = 0x0005,
        Adamantium  = 0x0006,
        Ebony       = 0x0007,
        Orcish      = 0x0008,
        Daedric     = 0x0009,
    }

    /// <summary>
    /// Armor material values.
    /// </summary>
    public enum ArmorMaterialTypes
    {
        None        = -1,
        Leather     = 0x0000,
        Chain       = 0x0100,
        Chain2      = 0x0103,
        Iron        = 0x0200,
        Steel       = 0x0201,
        Silver      = 0x0202,
        Elven       = 0x0203,
        Dwarven     = 0x0204,
        Mithril     = 0x0205,
        Adamantium  = 0x0206,
        Ebony       = 0x0207,
        Orcish      = 0x0208,
        Daedric     = 0x0209,
    }

    /// <summary>
    /// Equipment slots available to equip items.
    /// Indices match Daggerfall's legacy equip slots for import.
    /// Some unknowns still need to be resolved.
    /// </summary>
    public enum EquipSlots
    {
        None = -1,
        Amulet0 = 0,            // Amulets / Torcs
        Amulet1 = 1,
        Bracelet0 = 2,          // Bracelets
        Bracelet1 = 3,
        Ring0 = 4,              // Rings
        Ring1 = 5,
        Bracer0 = 6,            // Bracers
        Bracer1 = 7,
        Mark0 = 8,              // Marks
        Mark1 = 9,
        Crystal0 = 10,          // Gems
        Crystal1 = 11,
        Head = 12,              // Helm
        RightArm = 13,          // Right pauldron
        Cloak1 = 14,            // Cloaks
        LeftArm = 15,           // Left pauldron
        Cloak2 = 16,            // Cloaks
        ChestClothes = 17,      // Shirt / Straps / Armband / Eodoric / Tunic / Surcoat / Plain robes / etc.
        ChestArmor = 18,        // Cuirass
        RightHand = 19,         // Right weapon / Two-handed weapon
        Gloves = 20,            // Gauntlets
        LeftHand = 21,          // Left weapon / Shields
        Unknown1 = 22,
        LegsArmor = 23,         // Greaves
        LegsClothes = 24,       // Khajiit suit / Loincloth / Skirt / etc.
        Unknown2 = 25,
        Feet = 26,              // Boots / Shoes / Sandals / etc.
    }

    /// <summary>
    /// Body parts, used for armor value calculations.
    /// </summary>
    public enum BodyParts
    {
        None = -1,
        Head = 0,
        RightArm = 1,
        LeftArm = 2,
        Chest = 3,
        Hands = 4,
        Legs = 5,
        Feet = 6,
    }

    /// <summary>
    /// Poison IDs. The first 8 are found on enemy weapons. The last 4 are created by ingesting drugs.
    /// </summary>
    public enum Poisons
    {
        None = -1,
        Nux_Vomica = 128,
        Arsenic = 129,
        Moonseed = 130,
        Drothweed = 131,
        Somnalius = 132,
        Pyrrhic_Acid = 133,
        Magebane = 134,
        Thyrwort = 135,
        Indulcet = 136,
        Sursum = 137,
        Quaesto_Vil = 138,
        Aegrotat = 139,
    }

    public enum Drugs //checked
    {
        Indulcet = 78,
        Sursum = 79,
        Quaesto_Vil = 80,
        Aegrotat = 81,
        Tobacco = 4417,
        Moon_Sugar = 4418,
        Skooma = 4419,
        Incense_of_Mara = 4420,
        Sleeping_Tree_Sap = 4421,
    }

    public enum UselessItems1 //checked
    {
        Glass_Jar = 82,
        Glass_Bottle = 83,
        Decanter = 84,
        Clay_Jar = 85,
        Small_Sack = 86,
        Large_Sack = 87,
        Quiver = 88,
        Backpack = 89,
        Small_Chest = 90,
        Wine_Rack = 91,
        Large_Chest = 92,
    }

    public enum Armor   //checked
    {
        Cuirass = 102,
        Gauntlets = 103,
        Greaves = 104,
        Left_Pauldron = 105,
        Right_Pauldron = 106,
        Helm = 107,
        Boots = 108,
        Buckler = 109,
        Round_Shield = 110,
        Kite_Shield = 111,
        Tower_Shield = 112,
    }

    public enum Weapons  //checked
    {
        Dagger = 113,
        Tanto = 114,
        Staff = 115,
        Shortsword = 116,
        Wakazashi = 117,
        Broadsword = 118,
        Saber = 119,
        Longsword = 120,
        Katana = 121,
        Claymore = 122,
        Dai_Katana = 123,
        Mace = 124,
        Flail = 125,
        Warhammer = 126,
        Battle_Axe = 127,
        War_Axe = 128,
        Short_Bow = 129,
        Long_Bow = 130,
        Arrow = 131,
    }

    public enum MagicItemSubTypes                   // Not mapped to a specific item template index
    {
        MagicItem,
    }

    public enum ArtifactsSubTypes                   // Mapped to artifact definitions in MAGIC.DEF
    {
        Masque_of_Clavicus = 0,
        Mehrunes_Razor = 1,
        Mace_of_Molag_Bal = 2,
        Hircine_Ring = 3,
        Sanguine_Rose = 4,
        Oghma_Infinium = 5,
        Wabbajack = 6,
        Ring_of_Namira = 7,
        Skull_of_Corruption = 8,
        Azuras_Star = 9,
        Volendrung = 10,
        Warlocks_Ring = 11,
        Auriels_Bow = 12,
        Necromancers_Amulet = 13,
        Chrysamere = 14,
        Lords_Mail = 15,
        Staff_of_Magnus = 16,
        Ring_of_Khajiit = 17,
        Ebony_Mail = 18,
        Auriels_Shield = 19,
        Spell_Breaker = 20,
        Skeletons_Key = 21,
        Ebony_Blade = 22,
    }

    public enum MensClothing  //check
    {
        Straps = 141,
        Armbands = 142,
        Kimono = 143,
        Fancy_Armbands = 144,
        Sash = 145,
        Eodoric = 146,
        Shoes = 147,
        Tall_Boots = 148,
        Boots = 149,
        Sandals = 150,
        Casual_pants = 151,
        Breeches = 152,
        Short_skirt = 153,
        Casual_cloak = 154,
        Formal_cloak = 155,
        Khajiit_suit = 156,
        Dwynnen_surcoat = 157,
        Short_tunic = 158,
        Formal_tunic = 159,
        Toga = 160,
        Reversible_tunic = 161,
        Loincloth = 162,
        Plain_robes = 163,
        Priest_robes = 164,
        Short_shirt = 165,
        Short_shirt_with_belt = 166,
        Long_shirt = 167,
        Long_shirt_with_belt = 168,
        Short_shirt_closed_top = 169,
        Short_shirt_closed_top2 = 170,
        Long_shirt_closed_top = 171,
        Long_shirt_closed_top2 = 172,
        Open_Tunic = 173,
        Wrap = 174,
        Long_Skirt = 175,
        Anticlere_Surcoat = 176,
        Challenger_Straps = 177,
        Short_shirt_unchangeable = 178,
        Long_shirt_unchangeable = 179,
        Vest = 180,
        Champion_straps = 181,
    }

    public enum Books
    {
        Book0 = 277,
        Book1 = 277,
        Book2 = 277,
        Book3 = 277,
    }

    public enum Furniture
    {
        Plain_single_bed = 217,
        Fancy_single_bed = 218,
        Plain_double_bed = 219,
        Fancy_double_bed = 220,
        Large_oak_table = 221,
        Large_cherry_table = 222,
        Large_mahogany_table = 223,
        Large_teak_table = 224,
        Small_oak_table = 225,
        Small_cherry_table = 226,
        Small_mahogany_table = 227,
        Small_teak_table = 228,
        Oak_chair = 229,
        Cherry_chair = 230,
        Mahogany_chair = 231,
        Teak_chair = 232,
        Curtains = 233,
        Fancy_curtains = 234,
        Large_pillow = 235,
        Small_pillow = 236,
        Small_plain_rug = 237,
        Large_plain_rug = 238,
        Small_fine_rug = 239,
        Large_fine_rug = 240,
        Large_tapestry = 241,
        Medium_tapestry = 242,
        Small_tapestry = 243,
        Large_skins = 244,
        Small_skins = 245,
        Tent = 4836,
        Trunk = 4837,
    }

    public enum UselessItems2 //checked
    {
        Torch = 247,
        Lantern = 248,
        Bandage = 249,
        Oil = 252,
        Candle = 253,
        Parchment = 279,
    }

    public enum General_Tools
    {
        Compass = 4800,
        Rope = 4801,
        Matchbox = 4802,
        Spade = 4803,
        Pickaxe = 4804,
        Fishing_Pole = 4805,
        Fishing_Rod = 4806,
        Smoking_Pipe = 4807,
    }

    public enum Repair_Tools
    {
        Whetstone = 4808,
        Epoxy_Glue = 4809,
        Sewing_Kit = 4810,
        Armorers_Hammer = 4811,
        Jewelers_Pliers = 4812,
        Charging_Powder = 4813,
    }

    public enum Musical_Instruments
    {
        Lute = 4814,
        Hand_Drum = 4815,
        Flute = 4816,
        Oboe = 4817,
        Ney = 4818,
    }

    public enum Containers
    {
        Quiver = 4819,
        Satchel = 4820,
        Pouch = 4821,
        Urn = 4822,
        Bucket = 4823,
        Barrel = 4824,
        Lockbox = 4825,
        Snuff_Box = 4826,
    }

    public enum Liquid_Containers
    {
        Empty_Bottle = 4827,
        Hip_Flask = 4828,
        Wooden_Cup = 4829,
        Tin_Goblet = 4830,
        Silver_Goblet = 4831,
        Gold_Goblet = 4832,
        Gem_Encrusted_Silver_Goblet = 4833,
        Gem_Encrusted_Gold_Goblet = 4834,
    }

    public enum Magic_Amplifiers
    {
        Mark = 137,
        Wand = 140,
        Scepter = 4835,
    }

    public enum Flavor_Tools
    {
        Bell = 261,
        Basket = 4838,
        Bellows = 4839,
        Broom = 4840,
        Brush = 4841,
        Cane = 4842,
        Painters_Palette = 4843,
        Frying_Pan = 4844,
        Hour_Glass = 4845,
        Magnifying_Glass = 4846,
        Mallet = 4847,
        Metal_Scoop = 4848,
        Mirror = 4849,
        Net = 4850,
        Inside_Caliper = 4851,
        Outside_Caliper = 4852,
        Paint_Brush = 4853,
        Proportional_Divider = 4854,
        Quill_And_Ink_Well = 4855,
        Scroll = 4856,
        Scythe = 4857,
        Shears = 4858,
        Spectacles = 4859,
        Spyglass = 4860,
        Tongs = 4861,
        Triangle_Ruler = 4862,
        Trowel = 4863,
        Wooden_Scoop = 4864,
        Wooden_Shovel = 4865,
        Wooden_Spoon = 4866,
    }

    public enum Toys
    {
        Doll = 4867,
        Toy_Horse = 4868,
        Toy_House = 4869,
        Toy_Anvil = 4870,
        Wooden_Puzzle_Box = 4871,
    }

    public enum Sex_Toys
    {
        Blindfold = 4872,
        Gag = 4873,
        Handcuffs = 4874,
        Sex_Toy = 4875,
    }

    public enum Corpse_Parts
    {
        Bone = 4876,
        Skeletal_Arm = 4877,
        Animal_Carcass = 4878,
        Animal_Skull = 4879,
        Humanoid_Skull = 4880,
    }

    public enum Occult_Objects
    {
        Occult_Fetish = 4881,
        Demonic_Figurine = 4882,
    }

    public enum Junk
    {
        Dirty_Rags = 4884,
    }

    public enum ReligiousItems  //checked
    {
        Holy_relic = 55,
        Prayer_beads = 258,
        Rare_symbol = 259,
        Common_symbol = 260,
        Holy_water = 262,
        Talisman = 263,
        Religious_item = 264,
        Small_statue = 265,
        Icon = 267,
        Scarab = 268,
        Holy_candle = 269,
        Holy_dagger = 270,
        Holy_tome = 271,
        Religious_Icon = 4883,
    }

    public enum Maps //checked
    {
        Map = 287,
    }

    public enum WomensClothing  //checked
    {
        Brassier = 182,
        Formal_brassier = 183,
        Peasant_blouse = 184,
        Eodoric = 185,
        Shoes = 186,
        Tall_boots = 187,
        Boots = 188,
        Sandals = 189,
        Casual_pants = 190,
        Casual_cloak = 191,
        Formal_cloak = 192,
        Khajiit_suit = 193,
        Formal_eodoric = 194,
        Evening_gown = 195,
        Day_gown = 196,
        Casual_dress = 197,
        Strapless_dress = 198,
        Loincloth = 199,
        Plain_robes = 200,
        Priestess_robes = 201,
        Short_shirt = 202,
        Short_shirt_belt = 203,
        Long_shirt = 204,
        Long_shirt_belt = 205,
        Short_shirt_closed = 206,
        Short_shirt_closed_belt = 207,
        Long_shirt_closed = 208,
        Long_shirt_closed_belt = 209,
        Open_tunic = 210,
        Wrap = 211,
        Long_skirt = 212,
        Tights = 213,
        Short_shirt_unchangeable = 214,
        Long_shirt_unchangeable = 215,
        Vest = 216,
    }

    public enum Paintings                           // DEFEDIT sets subgroup to 255? ... correct group # though
    {
        Painting = 284,
    }

    public enum Gems  //checked
    {
        Ruby = 0,
        Emerald = 1,
        Sapphire = 2,
        Diamond = 3,
        Jade = 4,
        Turquoise = 5,
        Malachite = 6,
        Amber = 7,
        Amethyst = 4000,
        Apatite = 4001,
        Aquamarine = 4002,
        Garnet = 4003,
        Spinel = 4004,
        Topaz = 4005,
        Zircon = 4006,
    }

    public enum MiscPlantIngredients
    {
        Twigs = 8,
        Green_leaves = 9,
        Root_tendrils = 12,
        Root_bulb = 13,
        Pine_branch = 14,
        Ginkgo_leaves = 27,
        Bamboo = 28,
        Palm = 29,
        Aloe = 30,
        Arrowroot = 4400,
        Black_Trumpet = 4401,
        Bog_Beacon = 4402,
        Emetic_Russula = 4403,
        Fly_Amanita = 4404,
        Garlic = 4405,
        Giant_Puffball = 4406,
        Ginseng_Root = 4407,
        Glowing_Mushroom = 4408,
        Green_Elfcup = 4409,
        Beech_Mushrooms = 4410,
        Mint = 4411,
        Onion = 4412,
        Steel_Blue_Entoloma = 4413,
        Stinkhorn = 4414,
        Summer_Bolete = 4415,
        Tinder_Polypore = 4416,
    }

    public enum FlowerPlantIngredients
    {
        Red_Flowers = 10,
        Yellow_Flowers = 11,
        Clover = 18,
        Red_rose = 19,
        Yellow_rose = 20,
        Black_rose = 21,
        White_rose = 22,
        Red_poppy = 23,
        Black_poppy = 24,
        Golden_poppy = 25,
        White_poppy = 26,
        Alkanet_Flower = 4300,
        Columbine_Flower = 4301,
        Fennel_Flower = 4302,
        Flax_Flower = 4303,
        Dragons_Flower = 4304,
        Foxglove_Flower = 4305,
        Wild_Bergamot = 4306,
    }

    public enum FruitPlantIngredients
    {
        Green_berries = 15,
        Red_berries = 16,
        Yellow_berries = 17,
        Fig = 31,
        Cactus = 32,
        Banana = 4500,
        Black_Berries = 4501,
        Blue_Berries = 4502,
        Grapes = 4504,
        Kiwi = 4505,
        Lemon = 4506,
        Lime = 4507,
        Orange = 4508,
        Pear = 4509,
        Pomegranate = 4510,
    }

    public enum AnimalPartIngredients
    {
        Spider_venom = 41,
        Snake_venom = 43,
        Giant_scorpion_stinger = 47,
        Small_scorpion_stinger = 48,
        Big_tooth = 56,
        Medium_tooth = 57,
        Small_tooth = 58,
        Ivory = 76,
        Pearl = 77,
        Rat_Tail = 4100,
        Slaughterfish_Scales = 4101,
        Spider_Eye = 4102,
    }

    public enum CreatureIngredients
    {
        Werewolfs_blood = 33,
        Wereboar_tusk = 34,
        Fairy_dragon_scales = 35,
        Nymph_hair = 36,
        Unicorn_horn = 37,
        Wraith_essence = 38,
        Ectoplasm = 39,
        Ghouls_tongue = 40,
        Troll_blood = 42,
        Gorgon_snake = 44,
        Lich_dust = 45,
        Dragons_scales = 46,
        Mummy_wrappings = 49,
        Giant_blood = 50,
        Basilisk_eye = 51,
        Harpy_Feather = 52,
        Daedra_heart = 53,
        Saints_hair = 54,
        Orcs_blood = 61,
        Bone_Meal = 4200,
        Dreugh_Wax = 4201,
        Fire_Powder = 4202,
        Gargoyle_Horn = 4203,
        Imp_Heart = 4204,
        Permafrost_Shavings = 4205,
        Vampire_Dust = 4206,
    }

    public enum SolventIngredients
    {
        Pure_water = 59,
        Rain_water = 60,
        Elixir_vitae = 62,
        Nectar = 63,
        Ichor = 64,
    }

    public enum MetalIngredients  //Checked
    {
        Mercury = 65,
        Tin = 66,
        Brass = 67,
        Lodestone = 68,
        Sulphur = 69,
        Lead = 70,
        Iron = 71,
        Copper = 72,
        Silver = 73,
        Gold = 74,
        Platinum = 75,
    }

    public enum Transportation  //Checked
    {
        Small_cart = 93,
        Horse = 94,
        Rowboat = 95,
        Large_boat = 96,
        Small_ship = 97,
        Large_Galley = 98,
    }

    public enum Deeds  //checked
    {
        Deed_to_townhouse,
        Deed_to_house,
        Deed_to_manor,
    }

    public enum Jewellery  //checked
    {
        Amulet = 133,
        Bracer = 134,
        Ring = 135,
        Bracelet = 136,
        Torc = 138,
        Cloth_amulet = 139,
    }

    public enum Tiara_Jewelry
    {
        Gold_Tiara = 4600,
        Silver_Tiara = 4601,
        Gold_Ruby_Tiara = 4602,
        Silver_Ruby_Tiara = 4603,
        Gold_Emerald_Tiara = 4604,
        Silver_Emerald_Tiara = 4605,
        Gold_Sapphire_Tiara = 4606,
        Silver_Sapphire_Tiara = 4607,
        Gold_Diamond_Tiara = 4608,
        Silver_Diamond_Tiara = 4609,
        Gold_Garnet_Tiara = 4610,
        Silver_Garnet_Tiara = 4611,
        Gold_Apatite_Tiara = 4612,
        Silver_Apatite_Tiara = 4613,
        Gold_Amethyst_Tiara = 4614,
        Silver_Amethyst_Tiara = 4615,
        Gold_Aquamarine_Tiara = 4616,
        Silver_Aquamarine_Tiara = 4617,
        Gold_Topaz_Tiara = 4618,
        Silver_Topaz_Tiara = 4619,
        Gold_Zircon_Tiara = 4620,
        Silver_Zircon_Tiara = 4621,
        Gold_Spinel_Tiara = 4622,
        Silver_Spinel_Tiara = 4623,
        Gold_Basilisk_Eye_Tiara = 4624,
        Silver_Basilisk_Eye_Tiara = 4625,
    }

    public enum Crown_Jewelry
    {
        Gold_Crown = 4626,
        Silver_Crown = 4627,
        Gem_Encrusted_Gold_Crown = 4628,
        Gem_Encrusted_Silver_Crown = 4629,
    }

    public enum Ring_Jewelry
    {
        Gold_Ring = 4700,
        Silver_Ring = 4701,
        Gold_Amethyst_Ring = 4702,
        Silver_Amethyst_Ring = 4703,
        Gold_Apatite_Ring = 4704,
        Silver_Apatite_Ring = 4705,
        Gold_Aquamarine_Ring = 4706,
        Silver_Aquamarine_Ring = 4707,
        Gold_Diamond_Ring = 4708,
        Silver_Diamond_Ring = 4709,
        Gold_Emerald_Ring = 4710,
        Silver_Emerald_Ring = 4711,
        Gold_Garnet_Ring = 4712,
        Silver_Garnet_Ring = 4713,
        Gold_Ruby_Ring = 4714,
        Silver_Ruby_Ring = 4715,
        Gold_Sapphire_Ring = 4716,
        Silver_Sapphire_Ring = 4717,
        Gold_Spinel_Ring = 4718,
        Silver_Spinel_Ring = 4719,
        Gold_Topaz_Ring = 4720,
        Silver_Topaz_Ring = 4721,
        Gold_Zircon_Ring = 4722,
        Silver_Zircon_Ring = 4723,
    }

    public enum Earing_Jewelry
    {
        Amethyst_Earing = 4724,
        Apatite_Earing = 4725,
        Aquamarine_Earing = 4726,
        Diamond_Earing = 4727,
        Emerald_Earing = 4728,
        Garnet_Earing = 4729,
        Ruby_Earing = 4730,
        Sapphire_Earing = 4731,
        Spinel_Earing = 4732,
        Topaz_Earing = 4733,
        Zircon_Earing = 4734,
    }

    public enum Neck_Jewelry
    {
        Gold_Pendant = 4735,
        Amethyst_Pendant = 4736,
        Apatite_Pendant = 4737,
        Aquamarine_Pendant = 4738,
        Diamond_Pendant = 4739,
        Emerald_Pendant = 4740,
        Garnet_Pendant = 4741,
        Ruby_Pendant = 4742,
        Sapphire_Pendant = 4743,
        Spinel_Pendant = 4744,
        Topaz_Pendant = 4745,
        Zircon_Pendant = 4746,
    }

    public enum Bracelet_Jewelry
    {
        Amethyst_Encrusted_Bracelet = 4747,
        Apatite_Encrusted_Bracelet = 4748,
        Aquamarine_Encrusted_Bracelet = 4749,
        Diamond_Encrusted_Bracelet = 4750,
        Emerald_Encrusted_Bracelet = 4751,
        Garnet_Encrusted_Bracelet = 4752,
        Ruby_Encrusted_Bracelet = 4753,
        Sapphire_Encrusted_Bracelet = 4754,
        Spinel_Encrusted_Bracelet = 4755,
        Topaz_Encrusted_Bracelet = 4756,
        Zircon_Encrusted_Bracelet = 4757,
    }

    public enum QuestItems  //checked
    {
        Telescope = 254,
        Scales = 255,
        Globe = 256,
        Skeleton = 257,
        Totem = 280,
        Dead_body = 281,
        Mantella = 282,
        Finger = 283,
    }

    public enum MiscItems  //checked
    {
        Spellbook = 132,
        Soul_trap = 274,
        Letter_of_credit = 275,
        Unused,
        Potion_recipe = 278,
        Dead_Body = 281,
        House_Deed = 285,
        Ship_Deed = 286,
        Map = 287,
    }

    public enum Currency  //checked
    {
        Gold_pieces = 276,
    }

    public enum Biography_Books
    {
        Biography_of_Queen_Barenziah_Vol_I = 59,
        Biography_of_Queen_Barenziah_Vol_II = 60,
        Biography_of_Queen_Barenziah_Vol_III = 89,
        Galerion_the_Mystic = 66,
        King_Edward_Part_I = 100,
        King_Edward_Part_II = 101,
        King_Edward_Part_III = 102,
        King_Edward_Part_IV = 103,
        King_Edward_Part_V = 104,
        King_Edward_Part_VI = 105,
        King_Edward_Part_VII = 106,
        King_Edward_Part_VIII = 107,
        King_Edward_Part_IX = 108,
        King_Edward_Part_X = 109,
        King_Edward_Part_XI = 110,
        King_Edward_Part_XII = 111,
        The_Madness_of_Pelagius = 42,
        The_Real_Barenziah = 43,
        The_Real_Barenziah_Part_II = 44,
        The_Real_Barenziah_Part_III = 45,
        The_Real_Barenziah_Part_IV = 46,
        The_Real_Barenziah_Part_V = 47,
        The_Real_Barenziah_Part_VI = 48,
        The_Real_Barenziah_Part_VII = 49,
        The_Real_Barenziah_Part_VIII = 50,
        The_Real_Barenziah_Part_IX = 51,
        The_Real_Barenziah_Part_X = 52,
    }

    public enum Faction_Related_Books
    {
        The_Brothers_of_Darkness = 26,
        Confessions_of_a_Thief = 56,
        Origin_of_the_Mages_Guild = 38,
    }

    public enum Fiction_Books
    {
        The_Arrowshot_Woman = 81,
        The_Asylum_Ball = 39,
        Bankers_Bet = 82,
        Broken_Diamonds = 34,
        Divad_the_Singer = 86,
        A_Dubious_Tale_of_the_Crystal_Tower = 62,
        The_Epic_of_the_Grey_Falcon = 32,
        The_Healers_Tale = 72,
        The_Legend_of_Lovers_Lament = 35,
        The_Light_and_The_Dark = 55,
        Oelanders_Hammer = 7,
        The_Sage = 54,
        The_Story_of_Lyrisius = 37,
        A_Tale_of_Kieran = 1,
    }

    public enum History_Lore_Books
    {
        Brief_History_of_the_Empire_Part_I = 28,
        Brief_History_of_the_Empire_Part_II = 29,
        Brief_History_of_the_Empire_Part_III = 30,
        Brief_History_of_the_Empire_Part_IV = 31,
        The_Fall_of_the_Usurper = 41,
        The_War_Of_Betony_by_Favte = 83,
        The_War_Of_Betony_by_Newgate = 84,
        Fragment_On_Artaeum = 2,
        A_History_of_Daggerfall = 61,
        Maras_Tear = 69,
        The_Pig_Children = 8,
        Redguards_History_and_Heroes = 4,
        The_Wild_Elves = 3,
    }

    public enum Humor_Books
    {
        Jokes = 70,
    }

    public enum Instruction_Research_Books
    {
        Etiquette_With_Rulers = 79,
        The_Faerie = 6,
        Ghraewaj = 20,
        Holidays_of_the_Iliac_Bay = 33,
        Mysticism = 40,
        On_Lycanthropy = 36,
        On_Oblivion = 53,
        A_Scholars_Guide_to_Nymphs = 21,
        Special_Flora_of_Tamriel = 27,
        Vampires_of_the_Iliac_Bay_Part_I = 57,
        Vampires_of_the_Iliac_Bay_Part_II = 58,
    }

    public enum Journals_Logs_Books
    {
        The_MemoryStone = 22,
        Wabbajack = 65,
    }

    public enum Notes_Letters_Books
    {
        Notes_For_Redguard_History = 87,
    }

    public enum Plays_Poetry_Riddles_Books
    {
        Fools_Ebony_Part_the_Oneth = 74,
        Fools_Ebony_Part_the_Twoth = 75,
        Fools_Ebony_Part_the_Threeth = 76,
        Fools_Ebony_Part_the_Fourth = 77,
        Fools_Ebony_Part_the_Fiveth = 78,
        Fools_Ebony_Part_the_Sixth = 88,
        Rude_Song = 68,
    }

    public enum Politics_Law_Books
    {
        Legal_Basics = 25,
    }

    public enum Religion_Prophecy_Books
    {
        Arkay_The_God = 5,
        The_Ebon_Arm = 67,
        The_First_Scroll_of_Baan_Dar = 0,
        Invocation_of_Azura = 80,
        Ius_Animal_God = 71,
        Of_Jephre = 73,
        The_Old_Ways = 9,
        An_Overview_of_Gods_and_Worship = 63,
    }

    public enum Travel_Books
    {
        The_Alikr = 85,
        Wayrest_Jewel_of_the_Bay = 64,
    }

    public enum Informational_Books
    {
        None = -1,
    }

    public enum No_Topic_Books
    {
        None = -1,
    }
}
