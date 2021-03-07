// Project:         Daggerfall Tools For Unity
// Copyright:       Copyright (C) 2009-2020 Daggerfall Workshop
// Web Site:        http://www.dfworkshop.net
// License:         MIT License (http://www.opensource.org/licenses/mit-license.php)
// Source Code:     https://github.com/Interkarma/daggerfall-unity
// Original Author: Gavin Clayton (interkarma@dfworkshop.net)
// Contributors:
//
// Notes:
//

using UnityEngine;
using DaggerfallWorkshop.Game.Entity;
using System.Collections.Generic;
using DaggerfallWorkshop.Game.Utility;
using DaggerfallWorkshop.Game.Formulas;
using DaggerfallConnect;

namespace DaggerfallWorkshop.Game.Items
{
    /// <summary>
    /// Built-in loot tables.
    /// Currently just for testing during early implementation.
    /// These approximate the loot tables on page 156 of Daggerfall Chronicles but are
    /// different in several ways (e.g. Chronicles uses "WP" for both "Warm Plant" and "Weapon").
    /// May diverge substantially over time during testing and future implementation.
    /// </summary>
    public static class LootTables
    {
        /// <summary>
        /// Default loot table chance matrices.
        /// Note: Temporary implementation. Will eventually be moved to an external file and loaded as keyed dict.
        /// Note: Many loot tables are defined with a lower chance for magic items in FALL.EXE's tables than is
        /// shown in Daggerfall Chronicles.
        /// These are shown below in the order "Key", "Chronicles chance", "FALL.EXE chance".
        /// E, 5, 3
        /// F, 2, 1
        /// G, 3, 1
        /// H, 2, 1
        /// I, 10, 2
        /// J, 20, 3
        /// K, 5, 3
        /// L, 2, 1
        /// N, 2, 1
        /// O, 3, 2
        /// P, 3, 2
        /// Q, 10, 3
        /// R, 5, 2
        /// S, 20, 3
        /// T, 3, 1
        /// U, 3, 2
        /// </summary>
        public static LootChanceMatrix[] DefaultLootTables = {
            new LootChanceMatrix() {key = "-", MinGold = 0, MaxGold = 0, P1 = 0, P2 = 0, C1 = 0, C2 = 0, C3 = 0, M1 = 0, AM = 0, WP = 0, MI = 0, CL = 0, BK = 0, M2 = 0, RL = 0 },
            new LootChanceMatrix() {key = "A", MinGold = 1, MaxGold = 10, P1 = 0, P2 = 0, C1 = 0, C2 = 0, C3 = 0, M1 = 0, AM = 5, WP = 5, MI = 2, CL = 4, BK = 0, M2 = 2, RL = 0 },
            // Chronicles says B has 10 for Warm Plant and Misc. Monster, but in FALL.EXE it is Temperate Plant and Warm Plant.
            new LootChanceMatrix() {key = "B", MinGold = 0, MaxGold = 0, P1 = 10, P2 = 10, C1 = 0, C2 = 0, C3 = 0, M1 = 0, AM = 0, WP = 0, MI = 0, CL = 0, BK = 0, M2 = 0, RL = 0 },
            new LootChanceMatrix() {key = "C", MinGold = 2, MaxGold = 20, P1 = 10, P2 = 10, C1 = 5, C2 = 5, C3 = 5, M1 = 5, AM = 5, WP = 25, MI = 3, CL = 0, BK = 2, M2 = 2, RL = 2 },
            new LootChanceMatrix() {key = "D", MinGold = 1, MaxGold = 4, P1 = 6, P2 = 6, C1 = 6, C2 = 6, C3 = 6, M1 = 6, AM = 0, WP = 0, MI = 0, CL = 0, BK = 0, M2 = 0, RL = 4 },
            new LootChanceMatrix() {key = "E", MinGold = 20, MaxGold = 80, P1 = 0, P2 = 0, C1 = 0, C2 = 0, C3 = 0, M1 = 0, AM = 10, WP = 10, MI = 3, CL = 4, BK = 2, M2 = 1, RL = 15 },
            new LootChanceMatrix() {key = "F", MinGold = 4, MaxGold = 30, P1 = 2, P2 = 2, C1 = 5, C2 = 5, C3 = 5, M1 = 2, AM = 50, WP = 50, MI = 1, CL = 0, BK = 0, M2 = 3, RL = 0 },
            new LootChanceMatrix() {key = "G", MinGold = 3, MaxGold = 15, P1 = 0, P2 = 0, C1 = 0, C2 = 0, C3 = 0, M1 = 0, AM = 50, WP = 50, MI = 1, CL = 5, BK = 0, M2 = 3, RL = 0 },
            new LootChanceMatrix() {key = "H", MinGold = 2, MaxGold = 10, P1 = 0, P2 = 0, C1 = 0, C2 = 0, C3 = 0, M1 = 0, AM = 0, WP = 100, MI = 1, CL = 2, BK = 0, M2 = 0, RL = 0 },
            // Chronicles is missing "I" but lists its data in table "J." All the tables from here are off by one compared to Chronicles.
            new LootChanceMatrix() {key = "I", MinGold = 0, MaxGold = 0, P1 = 0, P2 = 0, C1 = 0, C2 = 0, C3 = 0, M1 = 0, AM = 0, WP = 0, MI = 2, CL = 0, BK = 0, M2 = 0, RL = 5 },
            new LootChanceMatrix() {key = "J", MinGold = 50, MaxGold = 150, P1 = 0, P2 = 0, C1 = 0, C2 = 0, C3 = 0, M1 = 0, AM = 5, WP = 5, MI = 3, CL = 0, BK = 0, M2 = 0, RL = 0 },
            new LootChanceMatrix() {key = "K", MinGold = 1, MaxGold = 10, P1 = 3, P2 = 3, C1 = 3, C2 = 3, C3 = 3, M1 = 3, AM = 5, WP = 5, MI = 3, CL = 0, BK = 5, M2 = 2, RL = 100 },
            new LootChanceMatrix() {key = "L", MinGold = 1, MaxGold = 20, P1 = 0, P2 = 0, C1 = 3, C2 = 3, C3 = 3, M1 = 3, AM = 50, WP = 50, MI = 1, CL = 75, BK = 0, M2 = 5, RL = 3 },
            new LootChanceMatrix() {key = "M", MinGold = 1, MaxGold = 15, P1 = 1, P2 = 1, C1 = 1, C2 = 1, C3 = 1, M1 = 2, AM = 10, WP = 10, MI = 1, CL = 15, BK = 2, M2 = 3, RL = 1 },
            new LootChanceMatrix() {key = "N", MinGold = 1, MaxGold = 80, P1 = 5, P2 = 5, C1 = 5, C2 = 5, C3 = 5, M1 = 5, AM = 5, WP = 5, MI = 1, CL = 20, BK = 5, M2 = 2, RL = 5 },
            new LootChanceMatrix() {key = "O", MinGold = 5, MaxGold = 20, P1 = 1, P2 = 1, C1 = 1, C2 = 1, C3 = 1, M1 = 1, AM = 10, WP = 15, MI = 2, CL = 0, BK = 0, M2 = 0, RL = 0 },
            new LootChanceMatrix() {key = "P", MinGold = 5, MaxGold = 20, P1 = 5, P2 = 5, C1 = 5, C2 = 5, C3 = 5, M1 = 5, AM = 5, WP = 10, MI = 2, CL = 0, BK = 10, M2 = 5, RL = 0 },
            new LootChanceMatrix() {key = "Q", MinGold = 20, MaxGold = 80, P1 = 2, P2 = 2, C1 = 8, C2 = 8, C3 = 8, M1 = 2, AM = 10, WP = 25, MI = 3, CL = 35, BK = 5, M2 = 3, RL = 0 },
            new LootChanceMatrix() {key = "R", MinGold = 5, MaxGold = 20, P1 = 0, P2 = 0, C1 = 3, C2 = 3, C3 = 3, M1 = 5, AM = 5, WP = 15, MI = 2, CL = 0, BK = 0, M2 = 0, RL = 0 },
            new LootChanceMatrix() {key = "S", MinGold = 50, MaxGold = 125, P1 = 5, P2 = 5, C1 = 5, C2 = 5, C3 = 5, M1 = 15, AM = 10, WP = 10, MI = 3, CL = 0, BK = 5, M2 = 5, RL = 0 },
            new LootChanceMatrix() {key = "T", MinGold = 20, MaxGold = 80, P1 = 0, P2 = 0, C1 = 0, C2 = 0, C3 = 0, M1 = 0, AM = 100, WP = 100, MI = 1, CL = 0, BK = 0, M2 = 0, RL = 0},
            new LootChanceMatrix() {key = "U", MinGold = 7, MaxGold = 30, P1 = 5, P2 = 5, C1 = 5, C2 = 5, C3 = 5, M1 = 10, AM = 10, WP = 10, MI = 2, CL = 0, BK = 2, M2 = 2, RL = 10 },
        };

        /// <summary>
        /// Gets loot matrix by key.
        /// Note: Temporary implementation. Will eventually be moved to an external file and loaded as keyed dict.
        /// </summary>
        /// <param name="key">Key of matrix to get.</param>
        /// <returns>LootChanceMatrix.</returns>
        public static LootChanceMatrix GetMatrix(string key)
        {
            for (int i = 0; i < DefaultLootTables.Length; i++)
            {
                if (DefaultLootTables[i].key == key)
                    return DefaultLootTables[i];
            }

            return DefaultLootTables[0];
        }

        public static bool GenerateBuildingLoot(DaggerfallLoot loot, int locationIndex) // This is for the "cloth piles" that spawn in places like the Dark Brotherhood and Thieves Guild. Private Property is different. 
        {
            int[] traits = { -1, -1, -1 };

            DaggerfallLoot.GenerateBuildingItems(loot.Items, traits);

            return true;
        }

        public static bool GenerateDungeonLoot(DaggerfallLoot loot, int locationIndex)
        {
            DaggerfallLoot.GenerateDungeonItems(loot.Items, locationIndex);

            return true;
        }

        public static DaggerfallUnityItem[] GenerateDungeonLootItems(int dungeonIndex)
        {
            PlayerEntity player = GameManager.Instance.PlayerEntity;
            int playerLuckRaw = player.Stats.LiveLuck;
            int playerLuck = player.Stats.LiveLuck - 50;
            float basicLuckMod = (playerLuck * 0.02f) + 1f;
            float chance = 1f;
            int condModMin = 100;
            int condModMax = 100;
            List<DaggerfallUnityItem> items = new List<DaggerfallUnityItem>();

            // Reseed random
            Random.InitState(items.GetHashCode());

            switch (dungeonIndex)
            {
                case (int)DFRegion.DungeonTypes.Crypt:
                    items.Add(ItemBuilder.CreateGoldPieces((int)Mathf.Floor(Random.Range(15, 30 + 1) * basicLuckMod)));
                    condModMin = Mathf.Clamp((int)Mathf.Floor(10 * basicLuckMod), 1, 100);
                    condModMax = Mathf.Clamp((int)Mathf.Floor(35 * basicLuckMod), 1, 100);
                    AddGems(35, 0.6f, playerLuckRaw, items);
                    AddMagicItems(5, 0.5f, condModMin, condModMax, items);
                    AddMaps(5, 0.4f, items);
                    AddClothing(20, 0.6f, condModMin, condModMax, items);
                    AddMiscDungeonSpecificItems(dungeonIndex, basicLuckMod, condModMin, condModMax, items);
                    break;
                case (int)DFRegion.DungeonTypes.OrcStronghold:
                    items.Add(ItemBuilder.CreateGoldPieces((int)Mathf.Floor(Random.Range(15, 30 + 1) * basicLuckMod)));
                    condModMin = Mathf.Clamp((int)Mathf.Floor(35 * basicLuckMod), 1, 100);
                    condModMax = Mathf.Clamp((int)Mathf.Floor(75 * basicLuckMod), 1, 100);
                    AddArrows(1, 15, basicLuckMod, items);
                    AddWeapons(50, 0.7f, condModMin, condModMax, items);
                    AddArmors(30, 0.6f, condModMin, condModMax, 100, items);
                    AddIngots(60, 0.6f, items);
                    AddGems(5, 0.5f, playerLuckRaw, items);
                    AddMiscDungeonSpecificItems(dungeonIndex, basicLuckMod, condModMin, condModMax, items);
                    break;
                case (int)DFRegion.DungeonTypes.HumanStronghold:
                    items.Add(ItemBuilder.CreateGoldPieces((int)Mathf.Floor(Random.Range(30, 45 + 1) * basicLuckMod)));
                    condModMin = Mathf.Clamp((int)Mathf.Floor(40 * basicLuckMod), 1, 100);
                    condModMax = Mathf.Clamp((int)Mathf.Floor(80 * basicLuckMod), 1, 100);
                    AddArrows(1, 35, basicLuckMod, items);
                    AddWeapons(30, 0.6f, condModMin, condModMax, items);
                    AddArmors(55, 0.55f, condModMin, condModMax, 60, items);
                    AddIngots(35, 0.4f, items);
                    AddGems(10, 0.5f, playerLuckRaw, items);
                    AddMaps(2, 0.5f, items);
                    AddClothing(25, 0.4f, condModMin, condModMax, items);
                    AddMiscDungeonSpecificItems(dungeonIndex, basicLuckMod, condModMin, condModMax, items);
                    break;
                case (int)DFRegion.DungeonTypes.Prison:
                    items.Add(ItemBuilder.CreateGoldPieces((int)Mathf.Floor(Random.Range(0, 15 + 1) * basicLuckMod)));
                    condModMin = Mathf.Clamp((int)Mathf.Floor(15 * basicLuckMod), 1, 100);
                    condModMax = Mathf.Clamp((int)Mathf.Floor(55 * basicLuckMod), 1, 100);
                    chance = 40f;
                    while (Dice100.SuccessRoll((int)chance))
                    {
                        DaggerfallUnityItem Weapon = ItemBuilder.CreateWeapon((Weapons)FormulaHelper.PickOneOfCompact((int)Weapons.Tanto, 4, (int)Weapons.Dagger, 2, (int)Weapons.Shortsword, 1, (int)Weapons.Wakazashi, 1), (WeaponMaterialTypes)FormulaHelper.PickOneOfCompact((int)WeaponMaterialTypes.Iron, 3, (int)WeaponMaterialTypes.Steel, 2, (int)WeaponMaterialTypes.Silver, 1));
                        float condPercentMod = Random.Range(condModMin, condModMax + 1) / 100f;
                        Weapon.currentCondition = (int)Mathf.Ceil(Weapon.maxCondition * condPercentMod);
                        items.Add(Weapon);
                        chance *= 0.6f;
                    }
                    AddArmors(20, 0.6f, condModMin, condModMax, 0, items);
                    AddBooks(10, 0.4f, items, 37);
                    AddClothing(20, 0.4f, condModMin, condModMax, items);
                    AddMiscDungeonSpecificItems(dungeonIndex, basicLuckMod, condModMin, condModMax, items);
                    break;
                case (int)DFRegion.DungeonTypes.DesecratedTemple:
                    items.Add(ItemBuilder.CreateGoldPieces((int)Mathf.Floor(Random.Range(15, 30 + 1) * basicLuckMod)));
                    condModMin = Mathf.Clamp((int)Mathf.Floor(35 * basicLuckMod), 1, 100);
                    condModMax = Mathf.Clamp((int)Mathf.Floor(60 * basicLuckMod), 1, 100);
                    AddPotions(30, 0.5f, items);
                    AddBooks(50, 0.5f, items, 38);
                    AddGems(10, 0.5f, playerLuckRaw, items);
                    AddMagicItems(3, 0.3f, condModMin, condModMax, items);
                    AddPotionRecipes(5, 0.1f, items);
                    AddMiscDungeonSpecificItems(dungeonIndex, basicLuckMod, condModMin, condModMax, items);
                    break;
                case (int)DFRegion.DungeonTypes.Mine:
                    items.Add(ItemBuilder.CreateGoldPieces((int)Mathf.Floor(Random.Range(0, 15 + 1) * basicLuckMod)));
                    AddIngots(40, 0.5f, items);
                    AddGems(35, 0.6f, playerLuckRaw, items);
                    AddMiscDungeonSpecificItems(dungeonIndex, basicLuckMod, condModMin, condModMax, items);
                    break;
                case (int)DFRegion.DungeonTypes.NaturalCave:
                    condModMin = Mathf.Clamp((int)Mathf.Floor(5 * basicLuckMod), 1, 100);
                    condModMax = Mathf.Clamp((int)Mathf.Floor(30 * basicLuckMod), 1, 100);
                    AddClothing(8, 0.8f, condModMin, condModMax, items);
                    AddMiscDungeonSpecificItems(dungeonIndex, basicLuckMod, condModMin, condModMax, items);
                    break;
                case (int)DFRegion.DungeonTypes.Coven:
                    items.Add(ItemBuilder.CreateGoldPieces((int)Mathf.Floor(Random.Range(0, 15 + 1) * basicLuckMod)));
                    condModMin = Mathf.Clamp((int)Mathf.Floor(25 * basicLuckMod), 1, 100);
                    condModMax = Mathf.Clamp((int)Mathf.Floor(40 * basicLuckMod), 1, 100);
                    AddPotions(25, 0.6f, items);
                    AddBooks(20, 0.6f, items, 30);
                    AddMagicItems(6, 0.6f, condModMin, condModMax, items);
                    AddMaps(3, 0.1f, items);
                    AddPotionRecipes(8, 0.5f, items);
                    AddClothing(10, 0.5f, condModMin, condModMax, items);
                    AddMiscDungeonSpecificItems(dungeonIndex, basicLuckMod, condModMin, condModMax, items);
                    break;
                case (int)DFRegion.DungeonTypes.VampireHaunt:
                    items.Add(ItemBuilder.CreateGoldPieces((int)Mathf.Floor(Random.Range(0, 15 + 1) * basicLuckMod)));
                    condModMin = Mathf.Clamp((int)Mathf.Floor(15 * basicLuckMod), 1, 100);
                    condModMax = Mathf.Clamp((int)Mathf.Floor(40 * basicLuckMod), 1, 100);
                    AddMaps(6, 0.5f, items);
                    AddClothing(20, 0.7f, condModMin, condModMax, items);
                    AddMiscDungeonSpecificItems(dungeonIndex, basicLuckMod, condModMin, condModMax, items);
                    break;
                case (int)DFRegion.DungeonTypes.Laboratory:
                    items.Add(ItemBuilder.CreateGoldPieces((int)Mathf.Floor(Random.Range(15, 30 + 1) * basicLuckMod)));
                    condModMin = Mathf.Clamp((int)Mathf.Floor(30 * basicLuckMod), 1, 100);
                    condModMax = Mathf.Clamp((int)Mathf.Floor(55 * basicLuckMod), 1, 100);
                    AddPotions(35, 0.5f, items);
                    AddBooks(50, 0.4f, items, 33);
                    AddMagicItems(8, 0.2f, condModMin, condModMax, items);
                    AddPotionRecipes(10, 0.6f, items);
                    AddMiscDungeonSpecificItems(dungeonIndex, basicLuckMod, condModMin, condModMax, items);
                    break;
                case (int)DFRegion.DungeonTypes.HarpyNest:
                    AddMiscDungeonSpecificItems(dungeonIndex, basicLuckMod, condModMin, condModMax, items);
                    break;
                case (int)DFRegion.DungeonTypes.RuinedCastle:
                    items.Add(ItemBuilder.CreateGoldPieces((int)Mathf.Floor(Random.Range(30, 45 + 1) * basicLuckMod)));
                    condModMin = Mathf.Clamp((int)Mathf.Floor(15 * basicLuckMod), 1, 100);
                    condModMax = Mathf.Clamp((int)Mathf.Floor(50 * basicLuckMod), 1, 100);
                    AddArrows(1, 11, basicLuckMod, items);
                    AddWeapons(25, 0.4f, condModMin, condModMax, items);
                    AddArmors(45, 0.5f, condModMin, condModMax, 50, items);
                    AddBooks(10, 0.5f, items, 28);
                    AddBooks(15, 0.4f, items, 31);
                    AddIngots(25, 0.4f, items);
                    AddGems(15, 0.5f, playerLuckRaw, items);
                    AddMaps(3, 0.2f, items);
                    AddClothing(10, 0.5f, condModMin, condModMax, items);
                    AddMiscDungeonSpecificItems(dungeonIndex, basicLuckMod, condModMin, condModMax, items);
                    break;
                case (int)DFRegion.DungeonTypes.SpiderNest:
                    condModMin = Mathf.Clamp((int)Mathf.Floor(5 * basicLuckMod), 1, 100);
                    condModMax = Mathf.Clamp((int)Mathf.Floor(30 * basicLuckMod), 1, 100);
                    AddClothing(15, 0.7f, condModMin, condModMax, items);
                    AddMiscDungeonSpecificItems(dungeonIndex, basicLuckMod, condModMin, condModMax, items);
                    break;
                case (int)DFRegion.DungeonTypes.GiantStronghold:
                    items.Add(ItemBuilder.CreateGoldPieces((int)Mathf.Floor(Random.Range(0, 15 + 1) * basicLuckMod)));
                    condModMin = Mathf.Clamp((int)Mathf.Floor(15 * basicLuckMod), 1, 100);
                    condModMax = Mathf.Clamp((int)Mathf.Floor(55 * basicLuckMod), 1, 100);
                    AddWeapons(20, 0.5f, condModMin, condModMax, items);
                    AddArmors(20, 0.35f, condModMin, condModMax, 40, items);
                    AddGems(10, 0.5f, playerLuckRaw, items);
                    AddMiscDungeonSpecificItems(dungeonIndex, basicLuckMod, condModMin, condModMax, items);
                    break;
                case (int)DFRegion.DungeonTypes.DragonsDen:
                    items.Add(ItemBuilder.CreateGoldPieces((int)Mathf.Floor(Random.Range(45, 60 + 1) * basicLuckMod)));
                    condModMin = Mathf.Clamp((int)Mathf.Floor(5 * basicLuckMod), 1, 100);
                    condModMax = Mathf.Clamp((int)Mathf.Floor(40 * basicLuckMod), 1, 100);
                    AddArrows(1, 6, basicLuckMod, items);
                    AddWeapons(15, 0.6f, condModMin, condModMax, items);
                    AddArmors(30, 0.5f, condModMin, condModMax, 60, items);
                    AddPotions(15, 0.3f, items);
                    AddIngots(15, 0.5f, items);
                    AddGems(15, 0.3f, playerLuckRaw, items);
                    AddMagicItems(2, 0.5f, condModMin, condModMax, items);
                    AddMaps(3, 0.3f, items);
                    AddMiscDungeonSpecificItems(dungeonIndex, basicLuckMod, condModMin, condModMax, items);
                    break;
                case (int)DFRegion.DungeonTypes.BarbarianStronghold:
                    items.Add(ItemBuilder.CreateGoldPieces((int)Mathf.Floor(Random.Range(15, 30 + 1) * basicLuckMod)));
                    condModMin = Mathf.Clamp((int)Mathf.Floor(30 * basicLuckMod), 1, 100);
                    condModMax = Mathf.Clamp((int)Mathf.Floor(60 * basicLuckMod), 1, 100);
                    AddArrows(1, 25, basicLuckMod, items);
                    AddWeapons(40, 0.6f, condModMin, condModMax, items);
                    AddArmors(30, 0.35f, condModMin, condModMax, 15, items);
                    AddIngots(25, 0.35f, items);
                    AddGems(10, 0.5f, playerLuckRaw, items);
                    AddMaps(3, 0.5f, items);
                    AddClothing(5, 0.6f, condModMin, condModMax, items);
                    AddMiscDungeonSpecificItems(dungeonIndex, basicLuckMod, condModMin, condModMax, items);
                    break;
                case (int)DFRegion.DungeonTypes.VolcanicCaves:
                    AddMiscDungeonSpecificItems(dungeonIndex, basicLuckMod, condModMin, condModMax, items);
                    break;
                case (int)DFRegion.DungeonTypes.ScorpionNest:
                    condModMin = Mathf.Clamp((int)Mathf.Floor(5 * basicLuckMod), 1, 100);
                    condModMax = Mathf.Clamp((int)Mathf.Floor(30 * basicLuckMod), 1, 100);
                    AddClothing(15, 0.7f, condModMin, condModMax, items);
                    AddMiscDungeonSpecificItems(dungeonIndex, basicLuckMod, condModMin, condModMax, items);
                    break;
                case (int)DFRegion.DungeonTypes.Cemetery:
                    items.Add(ItemBuilder.CreateGoldPieces((int)Mathf.Floor(Random.Range(15, 30 + 1) * basicLuckMod)));
                    condModMin = Mathf.Clamp((int)Mathf.Floor(10 * basicLuckMod), 1, 100);
                    condModMax = Mathf.Clamp((int)Mathf.Floor(25 * basicLuckMod), 1, 100);
                    AddGems(25, 0.6f, playerLuckRaw, items);
                    AddMagicItems(2, 0.3f, condModMin, condModMax, items);
                    AddMaps(5, 0.5f, items);
                    AddClothing(25, 0.5f, condModMin, condModMax, items);
                    AddMiscDungeonSpecificItems(dungeonIndex, basicLuckMod, condModMin, condModMax, items);
                    break;
                default:
                    break;
            }

            return items.ToArray();
        }

        public static DaggerfallUnityItem[] GenerateEnemyLoot(DaggerfallEntity enemy, int[] traits, int[] predefLootProps, int[] extraLootProps)
        {
            PlayerEntity player = GameManager.Instance.PlayerEntity;
            int level = enemy.Level;
            int[] enemyLootCondMods = EnemyLootConditionCalculator(enemy, traits);
            float condPercentMod = 1f;
            EnemyEntity AITarget = enemy as EnemyEntity;
            List<DaggerfallUnityItem> items = new List<DaggerfallUnityItem>();

            // Reseed random
            Random.InitState(items.GetHashCode());

            // Add gold
            if (predefLootProps[0] > 0)
            {
                items.Add(ItemBuilder.CreateGoldPieces(predefLootProps[0])); // Will have to work on this amount, it feels a bit off, too low. 
            }

            // Arrows
            if (extraLootProps[0] > 0)
            {
                DaggerfallUnityItem arrowPile = ItemBuilder.CreateWeapon(Weapons.Arrow, WeaponMaterialTypes.Iron);
                arrowPile.stackCount = extraLootProps[0];
                items.Add(arrowPile);
            }

            // Random Potions
            if (extraLootProps[1] > 0)
            {
                byte[,] allowedPotions = GetAllowedPotionTypes(AITarget);

                for (int i = 0; i < extraLootProps[1]; i++)
                {
                    items.Add(ItemBuilder.CreateRandomSpecificPotion(allowedPotions, AITarget.Level));
                }
            }

            // Random books
            if (predefLootProps[8] > 0)
            {
                AddBooksBasedOnSubject(AITarget, predefLootProps[8], items);
            }

            // Random Ingots
            float ingotChance = IngotDropChance(AITarget);
            while (Dice100.SuccessRoll((int)ingotChance))
            {
                items.Add(ItemBuilder.CreateRandomIngot(AITarget.Level));
                ingotChance *= 0.5f;
            }

            // Random Gems
            if (extraLootProps[2] > 0)
            {
                for (int i = 0; i < extraLootProps[2]; i++)
                {
                    items.Add(ItemBuilder.CreateRandomGem(level));
                }
            }

            // Random magic items
            if (extraLootProps[3] > 0)
            {
                for (int i = 0; i < extraLootProps[3]; i++)
                {
                    DaggerfallUnityItem magicItem = ItemBuilder.CreateRandomMagicItem(player.Gender, player.Race, level);

                    if (magicItem != null)
                    {
                        condPercentMod = Random.Range(enemyLootCondMods[0], enemyLootCondMods[1] + 1) / 100f;
                        magicItem.currentCondition = (int)Mathf.Ceil(magicItem.maxCondition * condPercentMod);
                    }

                    items.Add(magicItem);
                }
            }

            // Food/Ration Items
            if (extraLootProps[4] > 0)
            {
                // Items not yet implemented. 
            }

            // Light sources
            if (extraLootProps[5] > 0)
            {
                for (int i = 0; i < extraLootProps[5]; i++)
                {
                    if (i == 1 && Dice100.SuccessRoll(15))
                    {
                        items.Add(ItemBuilder.CreateItem(ItemGroups.UselessItems2, (int)UselessItems2.Lantern));
                        DaggerfallUnityItem lampOil = ItemBuilder.CreateItem(ItemGroups.UselessItems2, (int)UselessItems2.Oil);
                        lampOil.stackCount = UnityEngine.Random.Range(0, 4 + 1);
                        items.Add(lampOil);
                        continue;
                    }
                    items.Add(ItemBuilder.CreateItem(ItemGroups.UselessItems2, FormulaHelper.PickOneOfCompact((int)UselessItems2.Candle, 1, (int)UselessItems2.Torch, 1)));
                }
            }

            // Random religious items
            if (extraLootProps[6] > 0)
            {
                for (int i = 0; i < extraLootProps[6]; i++)
                {
                    items.Add(ItemBuilder.CreateRandomReligiousItem(level));
                }
            }

            // Bandages
            if (extraLootProps[7] > 0)
            {
                DaggerfallUnityItem bandages = ItemBuilder.CreateItem(ItemGroups.UselessItems2, (int)UselessItems2.Bandage);
                bandages.stackCount = extraLootProps[7];
                items.Add(bandages);
            }

            // Repair tools
            if (extraLootProps[8] > 0)
            {
                // Items not yet implemented.
            }

            // Drugs
            if (extraLootProps[9] > 0)
            {
                for (int i = 0; i < extraLootProps[9]; i++)
                {
                    items.Add(ItemBuilder.CreateRandomDrug(level));
                }
            }

            // Extra weapon
            if (extraLootProps[10] > 0)
            {
                for (int i = 0; i < extraLootProps[10]; i++)
                {
                    if (i == 1 && AITarget.EntityType == EntityTypes.EnemyClass && AITarget.CareerIndex == (int)ClassCareers.Nightblade)
                    {
                        DaggerfallUnityItem extraWep = ItemBuilder.CreateWeapon((Weapons)FormulaHelper.PickOneOfCompact((int)Weapons.Dagger, 1, (int)Weapons.Tanto, 1, (int)Weapons.Shortsword, 1, (int)Weapons.Longsword, 1), WeaponMaterialTypes.Silver);
                        condPercentMod = Random.Range(enemyLootCondMods[0], enemyLootCondMods[1] + 1) / 100f;
                        extraWep.currentCondition = (int)Mathf.Ceil(extraWep.maxCondition * condPercentMod);
                        continue;
                    }

                    if (traits[2] == (int)MobilePersonalityInterests.Survivalist || traits[2] == (int)MobilePersonalityInterests.Hunter)
                    {
                        DaggerfallUnityItem extraWep = ItemBuilder.CreateWeapon((Weapons)FormulaHelper.PickOneOfCompact((int)Weapons.Short_Bow, 1, (int)Weapons.Long_Bow, 1), FormulaHelper.RandomMaterial(level));
                        condPercentMod = Random.Range(enemyLootCondMods[0], enemyLootCondMods[1] + 1) / 100f;
                        extraWep.currentCondition = (int)Mathf.Ceil(extraWep.maxCondition * condPercentMod);
                        continue;
                    }
                    else
                    {
                        DaggerfallUnityItem extraWep = ItemBuilder.CreateRandomWeapon(level);
                        condPercentMod = Random.Range(enemyLootCondMods[0], enemyLootCondMods[1] + 1) / 100f;
                        extraWep.currentCondition = (int)Mathf.Ceil(extraWep.maxCondition * condPercentMod);
                    }
                }
            }

            // Maps
            if (extraLootProps[11] > 0)
            {
                for (int i = 0; i < extraLootProps[11]; i++)
                {
                    items.Add(new DaggerfallUnityItem(ItemGroups.MiscItems, 8));
                }
            }

            // Potion Recipes
            if (traits[2] == (int)MobilePersonalityInterests.Brewer)
            {
                DaggerfallLoot.RandomlyAddPotionRecipe(100, items); // I'll expand on this later on, probably add another parameter to extra loot method, but for now it's good enough. 
            }

            // Random clothes
            if (predefLootProps[9] > 0)
            {
                AddClothesBasedOnEnemy(player.Gender, player.Race, AITarget, enemyLootCondMods, items); // Will obviously have to change this later on when I add the location specific context variables of this loot system. 
            }

            // Ingredients
            AddIngredientsBasedOnEnemy(AITarget, traits, items);

            // Extra flavor/junk items (mostly based on personality traits, if present)
            if (traits[0] > -1 || traits[1] > -1 || traits[2] > -1)
            {
                PersonalityTraitFlavorItemsGenerator(AITarget, traits, items);
            }

            return items.ToArray();
        }

        public static void AddArrows(int minArrows, int maxArrows, float luckMod, List<DaggerfallUnityItem> targetItems)
        {
            if (Dice100.SuccessRoll(50))
            {
                DaggerfallUnityItem arrowPile = ItemBuilder.CreateWeapon(Weapons.Arrow, WeaponMaterialTypes.Iron);
                arrowPile.stackCount = (int)Mathf.Floor(Random.Range(minArrows, maxArrows + 1) * luckMod);
                targetItems.Add(arrowPile);
            }
        }

        public static void AddWeapons(float chance, float chanceMod, int condModMin, int condModMax, List<DaggerfallUnityItem> targetItems)
        {
            int playerLuck = GameManager.Instance.PlayerEntity.Stats.LiveLuck;

            while (Dice100.SuccessRoll((int)chance))
            {
                DaggerfallUnityItem Weapon = (ItemBuilder.CreateRandomWeapon(-1, -1, playerLuck));
                float condPercentMod = Random.Range(condModMin, condModMax + 1) / 100f;
                Weapon.currentCondition = (int)Mathf.Ceil(Weapon.maxCondition * condPercentMod);
                targetItems.Add(Weapon);
                chance *= chanceMod;
            }
        }

        public static void AddArmors(float chance, float chanceMod, int condModMin, int condModMax, int metalChance, List<DaggerfallUnityItem> targetItems)
        {
            PlayerEntity player = GameManager.Instance.PlayerEntity;
            int playerLuck = player.Stats.LiveLuck;

            while (Dice100.SuccessRoll((int)chance))
            {
                if (Dice100.SuccessRoll(metalChance))
                {
                    DaggerfallUnityItem Armor = ItemBuilder.CreateRandomArmor(player.Gender, player.Race, -1, -1, playerLuck, 2);
                    float condPercentMod = Random.Range(condModMin, condModMax + 1) / 100f;
                    Armor.currentCondition = (int)Mathf.Ceil(Armor.maxCondition * condPercentMod);
                    targetItems.Add(Armor);
                    chance *= chanceMod;
                }
                else
                {
                    DaggerfallUnityItem Armor = ItemBuilder.CreateRandomArmor(player.Gender, player.Race, -1, -1, playerLuck, FormulaHelper.PickOneOf(0, 1));
                    float condPercentMod = Random.Range(condModMin, condModMax + 1) / 100f;
                    Armor.currentCondition = (int)Mathf.Ceil(Armor.maxCondition * condPercentMod);
                    targetItems.Add(Armor);
                    chance *= chanceMod;
                }
            }
        }

        public static void AddPotions(float chance, float chanceMod, List<DaggerfallUnityItem> targetItems)
        {
            byte[,] allowedPotions = new byte[,] { { 1, 1 }, { 1, 1 }, { 1, 1 }, { 1, 1 }, { 1, 1 }, { 1, 1 }, { 1, 1 } };
            int playerLuck = GameManager.Instance.PlayerEntity.Stats.LiveLuck;

            while (Dice100.SuccessRoll((int)chance))
            {
                targetItems.Add(ItemBuilder.CreateRandomSpecificPotion(allowedPotions, -1, playerLuck));
                chance *= chanceMod;
            }
        }

        public static void AddBooks(float chance, float chanceMod, List<DaggerfallUnityItem> targetItems, int bookSubject = -1)
        {
            int playerLuck = GameManager.Instance.PlayerEntity.Stats.LiveLuck;

            while (Dice100.SuccessRoll((int)chance))
            {
                if (bookSubject == -1)
                {
                    targetItems.Add(ItemBuilder.CreateRandomBookOfRandomSubject(-1, playerLuck));
                    chance *= chanceMod;
                }
                else
                {
                    targetItems.Add(ItemBuilder.CreateRandomBookOfSpecificSubject((ItemGroups)bookSubject, -1, playerLuck));
                    chance *= chanceMod;
                }
            }
        }

        public static void AddIngots(float chance, float chanceMod, List<DaggerfallUnityItem> targetItems)
        {
            int playerLuck = GameManager.Instance.PlayerEntity.Stats.LiveLuck;

            while (Dice100.SuccessRoll((int)chance))
            {
                targetItems.Add(ItemBuilder.CreateRandomIngot(-1, -1, playerLuck));
                chance *= chanceMod;
            }
        }

        public static void AddGems(float chance, float chanceMod, int luckMod, List<DaggerfallUnityItem> targetItems)
        {
            while (Dice100.SuccessRoll((int)chance))
            {
                targetItems.Add(ItemBuilder.CreateRandomGem(luckMod));
                chance *= chanceMod;
            }
        }

        public static void AddMagicItems(float chance, float chanceMod, int condModMin, int condModMax, List<DaggerfallUnityItem> targetItems)
        {
            PlayerEntity player = GameManager.Instance.PlayerEntity;
            int playerLuck = player.Stats.LiveLuck;

            while (Dice100.SuccessRoll((int)chance))
            {
                DaggerfallUnityItem magicItem = ItemBuilder.CreateRandomMagicItem(player.Gender, player.Race, -1, -1, playerLuck);
                float condPercentMod = Random.Range(condModMin, condModMax + 1) / 100f;
                magicItem.currentCondition = (int)Mathf.Ceil(magicItem.maxCondition * condPercentMod);
                targetItems.Add(magicItem);
                chance *= chanceMod;
            }
        }

        public static void AddMaps(float chance, float chanceMod, List<DaggerfallUnityItem> targetItems)
        {
            while (Dice100.SuccessRoll((int)chance))
            {
                targetItems.Add(new DaggerfallUnityItem(ItemGroups.MiscItems, 8));
                chance *= chanceMod;
            }
        }

        public static void AddPotionRecipes(float chance, float chanceMod, List<DaggerfallUnityItem> targetItems)
        {
            while (Dice100.SuccessRoll((int)chance))
            {
                DaggerfallLoot.RandomlyAddPotionRecipe(100, targetItems);
                chance *= chanceMod;
            }
        }

        public static void AddClothing(float chance, float chanceMod, int condModMin, int condModMax, List<DaggerfallUnityItem> targetItems)
        {
            PlayerEntity player = GameManager.Instance.PlayerEntity;

            while (Dice100.SuccessRoll((int)chance))
            {
                if (Dice100.SuccessRoll(50))
                {
                    DaggerfallUnityItem Cloths = ItemBuilder.CreateRandomClothing(Genders.Male, player.Race);
                    float condPercentMod = Random.Range(condModMin, condModMax + 1) / 100f;
                    Cloths.currentCondition = (int)Mathf.Ceil(Cloths.maxCondition * condPercentMod);
                    targetItems.Add(Cloths);
                    chance *= chanceMod;
                }
                else
                {
                    DaggerfallUnityItem Cloths = ItemBuilder.CreateRandomClothing(Genders.Female, player.Race);
                    float condPercentMod = Random.Range(condModMin, condModMax + 1) / 100f;
                    Cloths.currentCondition = (int)Mathf.Ceil(Cloths.maxCondition * condPercentMod);
                    targetItems.Add(Cloths);
                    chance *= chanceMod;
                }
            }
        }

        public static void AddIngredGroupRandomItems(ItemGroups itemGroup, float chance, float chanceMod, List<DaggerfallUnityItem> targetItems, params int[] itemIndices)
        {
            PlayerEntity player = GameManager.Instance.PlayerEntity;
            int playerLuckRaw = player.Stats.LiveLuck;

            while (Dice100.SuccessRoll((int)chance))
            {
                targetItems.Add(ItemBuilder.CreateRandomIngredientOfGroup(itemGroup, -1, -1, playerLuckRaw, itemIndices));
                chance *= chanceMod;
            }
        }

        public static void AddMiscDungeonSpecificItems(int dungeonIndex, float luckMod, int condModMin, int condModMax, List<DaggerfallUnityItem> targetItems)
        {
            switch (dungeonIndex)
            {
                case (int)DFRegion.DungeonTypes.Crypt:
                    AddRandomJewelryItem(30, 0.6f, targetItems, (int)Crown_Jewelry.Gem_Encrusted_Silver_Crown, (int)Crown_Jewelry.Gem_Encrusted_Gold_Crown);
                    AddRandomItemsOfSpecificGroup(ItemGroups.ReligiousItems, 20, 0.4f, targetItems);
                    AddRandomItemsOfSpecificGroup(ItemGroups.Corpse_Parts, 60, 0.5f, targetItems, (int)Corpse_Parts.Animal_Carcass, (int)Corpse_Parts.Animal_Skull, (int)Corpse_Parts.Charred_Bone, (int)Corpse_Parts.Charred_Remains, (int)Corpse_Parts.Charred_Animal_Skull, (int)Corpse_Parts.Charred_Humanoid_Skull);
                    AddSpecificRandomItems(ItemGroups.Containers, 20, 0.1f, targetItems, (int)Containers.Urn);
                    AddSpecificRandomItems(ItemGroups.General_Tools, 20, 0.1f, targetItems, (int)General_Tools.Spade);
                    break;
                case (int)DFRegion.DungeonTypes.OrcStronghold:
                    AddRandomItemsOfSpecificGroup(ItemGroups.Corpse_Parts, 30, 0.3f, targetItems, (int)Corpse_Parts.Humanoid_Skull, (int)Corpse_Parts.Skeletal_Arm, (int)Corpse_Parts.Charred_Remains, (int)Corpse_Parts.Charred_Humanoid_Skull);
                    AddSpecificRandomItems(ItemGroups.Liquid_Containers, 50, 0.6f, targetItems, (int)Liquid_Containers.Empty_Bottle, (int)Liquid_Containers.Wooden_Cup);
                    AddSpecificRandomItems(ItemGroups.Containers, 15, 0.1f, targetItems, (int)Containers.Barrel, (int)Containers.Bucket, (int)Containers.Snuff_Box);
                    AddSpecificRandomItems(ItemGroups.Repair_Tools, 15, 0.5f, targetItems, (int)Repair_Tools.Armorers_Hammer, (int)Repair_Tools.Whetstone);
                    AddSpecificRandomItems(ItemGroups.Flavor_Tools, 30, 0.5f, targetItems, (int)Flavor_Tools.Bellows, (int)Flavor_Tools.Metal_Scoop, (int)Flavor_Tools.Tongs);
                    AddSpecificRandomItems(ItemGroups.Junk, 30, 0.4f, targetItems, (int)Junk.Dirty_Rags, (int)Junk.Broken_Glass);
                    break;
                case (int)DFRegion.DungeonTypes.HumanStronghold:
                    AddRandomJewelryItem(10, 0.1f, targetItems, (int)Crown_Jewelry.Gem_Encrusted_Silver_Crown, (int)Crown_Jewelry.Gem_Encrusted_Gold_Crown, (int)Crown_Jewelry.Gold_Crown, (int)Crown_Jewelry.Silver_Crown);
                    AddRandomItemsOfSpecificGroup(ItemGroups.ReligiousItems, 10, 0.5f, targetItems);
                    AddRandomItemsOfSpecificGroup(ItemGroups.Corpse_Parts, 15, 0.2f, targetItems, (int)Corpse_Parts.Humanoid_Skull, (int)Corpse_Parts.Skeletal_Arm, (int)Corpse_Parts.Charred_Remains, (int)Corpse_Parts.Charred_Humanoid_Skull);
                    AddSpecificRandomItems(ItemGroups.Liquid_Containers, 20, 0.4f, targetItems, (int)Liquid_Containers.Empty_Bottle, (int)Liquid_Containers.Wooden_Cup, (int)Liquid_Containers.Tin_Goblet);
                    AddRandomItemsOfSpecificGroup(ItemGroups.Containers, 20, 0.4f, targetItems, (int)Containers.Lockbox, (int)Containers.Urn);
                    AddRandomItemsOfSpecificGroup(ItemGroups.Repair_Tools, 10, 0.1f, targetItems, (int)Repair_Tools.Charging_Powder);
                    AddRandomItemsOfSpecificGroup(ItemGroups.Flavor_Tools, 25, 0.3f, targetItems, (int)Flavor_Tools.Scythe, (int)Flavor_Tools.Net, (int)Flavor_Tools.Cane);
                    AddSpecificRandomItems(ItemGroups.Junk, 15, 0.5f, targetItems, (int)Junk.Dirty_Rags, (int)Junk.Broken_Glass);
                    break;
                case (int)DFRegion.DungeonTypes.Prison:
                    AddSpecificRandomItems(ItemGroups.ReligiousItems, 25, 0.5f, targetItems, (int)ReligiousItems.Common_symbol, (int)ReligiousItems.Prayer_beads);
                    AddRandomItemsOfSpecificGroup(ItemGroups.Corpse_Parts, 20, 0.5f, targetItems);
                    AddSpecificRandomItems(ItemGroups.Liquid_Containers, 40, 0.6f, targetItems, (int)Liquid_Containers.Empty_Bottle, (int)Liquid_Containers.Wooden_Cup);
                    AddSpecificRandomItems(ItemGroups.Containers, 20, 0.2f, targetItems, (int)Containers.Barrel, (int)Containers.Bucket, (int)Containers.Snuff_Box);
                    AddSpecificRandomItems(ItemGroups.Sex_Toys, 60, 0.4f, targetItems, (int)Sex_Toys.Blindfold, (int)Sex_Toys.Handcuffs);
                    AddSpecificRandomItems(ItemGroups.Flavor_Tools, 35, 0.4f, targetItems, (int)Flavor_Tools.Broom, (int)Flavor_Tools.Wooden_Scoop, (int)Flavor_Tools.Wooden_Shovel, (int)Flavor_Tools.Wooden_Spoon, (int)Flavor_Tools.Wooden_Bowl);
                    AddSpecificRandomItems(ItemGroups.Junk, 45, 0.3f, targetItems, (int)Junk.Dirty_Rags, (int)Junk.Broken_Glass, (int)Junk.Feces);
                    break;
                case (int)DFRegion.DungeonTypes.DesecratedTemple:
                    AddRandomJewelryItem(15, 0.5f, targetItems, (int)Crown_Jewelry.Gem_Encrusted_Silver_Crown, (int)Crown_Jewelry.Gem_Encrusted_Gold_Crown, (int)Crown_Jewelry.Gold_Crown, (int)Crown_Jewelry.Silver_Crown);
                    AddRandomItemsOfSpecificGroup(ItemGroups.ReligiousItems, 45, 0.6f, targetItems);
                    AddRandomItemsOfSpecificGroup(ItemGroups.Occult_Objects, 15, 0.4f, targetItems);
                    AddSpecificRandomItems(ItemGroups.Liquid_Containers, 15, 0.4f, targetItems, (int)Liquid_Containers.Silver_Goblet, (int)Liquid_Containers.Gold_Goblet);
                    AddSpecificRandomItems(ItemGroups.Junk, 15, 0.5f, targetItems, (int)Junk.Masonry_Rubble);
                    break;
                case (int)DFRegion.DungeonTypes.Mine:
                    AddSpecificRandomItems(ItemGroups.General_Tools, 55, 0.6f, targetItems, (int)General_Tools.Pickaxe, (int)General_Tools.Spade, (int)General_Tools.Rope);
                    AddSpecificRandomItems(ItemGroups.Containers, 35, 0.5f, targetItems, (int)Containers.Barrel, (int)Containers.Bucket);
                    AddSpecificRandomItems(ItemGroups.Flavor_Tools, 30, 0.6f, targetItems, (int)Flavor_Tools.Mallet, (int)Flavor_Tools.Metal_Scoop, (int)Flavor_Tools.Wooden_Scoop, (int)Flavor_Tools.Wooden_Shovel);
                    AddSpecificRandomItems(ItemGroups.Junk, 60, 0.4f, targetItems, (int)Junk.Rock_Rubble);
                    AddIngredGroupRandomItems(ItemGroups.MetalIngredients, 65, 0.4f, targetItems);
                    break;
                case (int)DFRegion.DungeonTypes.NaturalCave:
                    AddRandomItemsOfSpecificGroup(ItemGroups.Corpse_Parts, 65, 0.4f, targetItems, (int)Corpse_Parts.Humanoid_Skull, (int)Corpse_Parts.Skeletal_Arm, (int)Corpse_Parts.Charred_Remains, (int)Corpse_Parts.Charred_Humanoid_Skull, (int)Corpse_Parts.Charred_Bone, (int)Corpse_Parts.Charred_Animal_Skull);
                    AddSpecificRandomItems(ItemGroups.Containers, 20, 0.5f, targetItems, (int)Containers.Satchel, (int)Containers.Pouch);
                    AddSpecificRandomItems(ItemGroups.Furniture, 10, 0.2f, targetItems, (int)Furniture.Tent);
                    AddSpecificRandomItems(ItemGroups.Junk, 25, 0.4f, targetItems, (int)Junk.Feces, (int)Junk.Rock_Rubble, (int)Junk.Webbing);
                    AddIngredGroupRandomItems(ItemGroups.MiscPlantIngredients, 45, 0.6f, targetItems, (int)MiscPlantIngredients.Aloe, (int)MiscPlantIngredients.Arrowroot, (int)MiscPlantIngredients.Bamboo, (int)MiscPlantIngredients.Bog_Beacon, (int)MiscPlantIngredients.Garlic, (int)MiscPlantIngredients.Giant_Puffball, (int)MiscPlantIngredients.Ginkgo_leaves, (int)MiscPlantIngredients.Ginseng_Root, (int)MiscPlantIngredients.Mint, (int)MiscPlantIngredients.Onion, (int)MiscPlantIngredients.Palm, (int)MiscPlantIngredients.Pine_branch, (int)MiscPlantIngredients.Root_bulb, (int)MiscPlantIngredients.Summer_Bolete, (int)MiscPlantIngredients.Tinder_Polypore);
                    AddIngredGroupRandomItems(ItemGroups.FruitPlantIngredients, 25, 0.6f, targetItems, (int)FruitPlantIngredients.Banana, (int)FruitPlantIngredients.Cactus, (int)FruitPlantIngredients.Fig, (int)FruitPlantIngredients.Grapes, (int)FruitPlantIngredients.Kiwi, (int)FruitPlantIngredients.Lemon, (int)FruitPlantIngredients.Lime, (int)FruitPlantIngredients.Orange, (int)FruitPlantIngredients.Pear, (int)FruitPlantIngredients.Pomegranate);
                    AddSpecificRandomItems(ItemGroups.SolventIngredients, 25, 0.5f, targetItems, (int)SolventIngredients.Rain_water);
                    AddSpecificRandomItems(ItemGroups.AnimalPartIngredients, 25, 0.5f, targetItems, (int)AnimalPartIngredients.Small_tooth, (int)AnimalPartIngredients.Big_tooth, (int)AnimalPartIngredients.Snake_venom, (int)AnimalPartIngredients.Small_scorpion_stinger, (int)AnimalPartIngredients.Rat_Tail);
                    break;
                case (int)DFRegion.DungeonTypes.Coven:
                    AddRandomItemsOfSpecificGroup(ItemGroups.Occult_Objects, 35, 0.6f, targetItems);
                    AddRandomJewelryItem(20, 0.3f, targetItems, (int)Crown_Jewelry.Gem_Encrusted_Silver_Crown, (int)Crown_Jewelry.Gem_Encrusted_Gold_Crown, (int)Crown_Jewelry.Gold_Crown, (int)Crown_Jewelry.Silver_Crown);
                    AddRandomItemsOfSpecificGroup(ItemGroups.Corpse_Parts, 45, 0.5f, targetItems);
                    AddRandomItemsOfSpecificGroup(ItemGroups.Magic_Amplifiers, 20, 0.4f, targetItems);
                    AddSpecificRandomItems(ItemGroups.Flavor_Tools, 35, 0.3f, targetItems, (int)Flavor_Tools.Brush, (int)Flavor_Tools.Cane, (int)Flavor_Tools.Mirror);
                    AddSpecificRandomItems(ItemGroups.Repair_Tools, 1, 0.1f, targetItems, (int)Repair_Tools.Charging_Powder);
                    AddIngredGroupRandomItems(ItemGroups.CreatureIngredients, 20, 0.5f, targetItems);
                    AddIngredGroupRandomItems(ItemGroups.FruitPlantIngredients, 35, 0.4f, targetItems, (int)FruitPlantIngredients.Banana, (int)FruitPlantIngredients.Cactus, (int)FruitPlantIngredients.Fig, (int)FruitPlantIngredients.Grapes, (int)FruitPlantIngredients.Kiwi, (int)FruitPlantIngredients.Lemon, (int)FruitPlantIngredients.Lime, (int)FruitPlantIngredients.Orange, (int)FruitPlantIngredients.Pear, (int)FruitPlantIngredients.Pomegranate);
                    AddIngredGroupRandomItems(ItemGroups.MiscPlantIngredients, 45, 0.4f, targetItems);
                    AddIngredGroupRandomItems(ItemGroups.FlowerPlantIngredients, 30, 0.5f, targetItems);
                    break;
                case (int)DFRegion.DungeonTypes.VampireHaunt:
                    AddRandomJewelryItem(15, 0.5f, targetItems, (int)Crown_Jewelry.Gem_Encrusted_Silver_Crown, (int)Crown_Jewelry.Gem_Encrusted_Gold_Crown, (int)Crown_Jewelry.Gold_Crown, (int)Crown_Jewelry.Silver_Crown);
                    AddRandomItemsOfSpecificGroup(ItemGroups.Corpse_Parts, 45, 0.5f, targetItems);
                    AddSpecificRandomItems(ItemGroups.Sex_Toys, 35, 0.4f, targetItems, (int)Sex_Toys.Blindfold, (int)Sex_Toys.Handcuffs, (int)Sex_Toys.Gag);
                    AddSpecificRandomItems(ItemGroups.Junk, 25, 0.3f, targetItems, (int)Junk.Dirty_Rags, (int)Junk.Webbing);
                    break;
                case (int)DFRegion.DungeonTypes.Laboratory:
                    AddSpecificRandomItems(ItemGroups.UselessItems2, 60, 0.6f, targetItems, (int)UselessItems2.Parchment);
                    AddSpecificRandomItems(ItemGroups.Liquid_Containers, 60, 0.6f, targetItems, (int)Liquid_Containers.Empty_Bottle);
                    AddSpecificRandomItems(ItemGroups.Flavor_Tools, 60, 0.6f, targetItems, (int)Flavor_Tools.Magnifying_Glass, (int)Flavor_Tools.Metal_Scoop, (int)Flavor_Tools.Quill_And_Ink_Well, (int)Flavor_Tools.Scroll, (int)Flavor_Tools.Spectacles);
                    AddSpecificRandomItems(ItemGroups.Repair_Tools, 1, 0.1f, targetItems, (int)Repair_Tools.Charging_Powder);
                    AddSpecificRandomItems(ItemGroups.Junk, 25, 0.3f, targetItems, (int)Junk.Broken_Glass);
                    AddIngredGroupRandomItems(ItemGroups.MetalIngredients, 20, 0.5f, targetItems);
                    AddSpecificRandomItems(ItemGroups.CreatureIngredients, 25, 0.5f, targetItems, (int)CreatureIngredients.Basilisk_eye, (int)CreatureIngredients.Ectoplasm, (int)CreatureIngredients.Mummy_wrappings, (int)CreatureIngredients.Troll_blood, (int)CreatureIngredients.Wraith_essence, (int)CreatureIngredients.Orcs_blood, (int)CreatureIngredients.Nymph_hair, (int)CreatureIngredients.Gorgon_snake, (int)CreatureIngredients.Bone_Meal, (int)CreatureIngredients.Dreugh_Wax, (int)CreatureIngredients.Imp_Heart);
                    AddSpecificRandomItems(ItemGroups.SolventIngredients, 35, 0.6f, targetItems, (int)SolventIngredients.Rain_water, (int)SolventIngredients.Pure_water, (int)SolventIngredients.Nectar);
                    AddSpecificRandomItems(ItemGroups.AnimalPartIngredients, 20, 0.6f, targetItems, (int)AnimalPartIngredients.Snake_venom, (int)AnimalPartIngredients.Spider_venom);
                    break;
                case (int)DFRegion.DungeonTypes.HarpyNest:
                    AddRandomItemsOfSpecificGroup(ItemGroups.Corpse_Parts, 55, 0.4f, targetItems, (int)Corpse_Parts.Humanoid_Skull, (int)Corpse_Parts.Skeletal_Arm, (int)Corpse_Parts.Charred_Animal_Skull, (int)Corpse_Parts.Charred_Bone, (int)Corpse_Parts.Charred_Humanoid_Skull, (int)Corpse_Parts.Charred_Remains);
                    AddSpecificRandomItems(ItemGroups.Junk, 15, 0.3f, targetItems, (int)Junk.Feces);
                    AddSpecificRandomItems(ItemGroups.CreatureIngredients, 75, 0.7f, targetItems, (int)CreatureIngredients.Harpy_Feather);
                    break;
                case (int)DFRegion.DungeonTypes.RuinedCastle:
                    AddRandomJewelryItem(20, 0.7f, targetItems, (int)Crown_Jewelry.Gem_Encrusted_Silver_Crown, (int)Crown_Jewelry.Gem_Encrusted_Gold_Crown);
                    AddRandomItemsOfSpecificGroup(ItemGroups.ReligiousItems, 15, 0.4f, targetItems);
                    AddRandomItemsOfSpecificGroup(ItemGroups.Corpse_Parts, 20, 0.4f, targetItems);
                    AddRandomItemsOfSpecificGroup(ItemGroups.Liquid_Containers, 20, 0.5f, targetItems, (int)Liquid_Containers.Empty_Bottle, (int)Liquid_Containers.Hip_Flask, (int)Liquid_Containers.Wooden_Cup);
                    AddRandomItemsOfSpecificGroup(ItemGroups.Containers, 15, 0.5f, targetItems, (int)Containers.Lockbox, (int)Containers.Snuff_Box);
                    AddRandomItemsOfSpecificGroup(ItemGroups.Flavor_Tools, 20, 0.5f, targetItems, (int)Flavor_Tools.Scythe, (int)Flavor_Tools.Net, (int)Flavor_Tools.Cane);
                    AddSpecificRandomItems(ItemGroups.Junk, 20, 0.4f, targetItems, (int)Junk.Dirty_Rags, (int)Junk.Broken_Glass, (int)Junk.Masonry_Rubble);
                    break;
                case (int)DFRegion.DungeonTypes.SpiderNest:
                    AddRandomItemsOfSpecificGroup(ItemGroups.Corpse_Parts, 65, 0.4f, targetItems, (int)Corpse_Parts.Charred_Remains, (int)Corpse_Parts.Charred_Humanoid_Skull, (int)Corpse_Parts.Charred_Bone, (int)Corpse_Parts.Charred_Animal_Skull);
                    AddSpecificRandomItems(ItemGroups.Junk, 65, 0.6f, targetItems, (int)Junk.Webbing, (int)Junk.Egg_Sack_Remains);
                    AddSpecificRandomItems(ItemGroups.AnimalPartIngredients, 45, 0.5f, targetItems, (int)AnimalPartIngredients.Spider_venom, (int)AnimalPartIngredients.Rat_Tail);
                    break;
                case (int)DFRegion.DungeonTypes.GiantStronghold:
                    AddRandomItemsOfSpecificGroup(ItemGroups.Corpse_Parts, 45, 0.3f, targetItems);
                    AddSpecificRandomItems(ItemGroups.Liquid_Containers, 30, 0.5f, targetItems, (int)Liquid_Containers.Empty_Bottle);
                    AddSpecificRandomItems(ItemGroups.Junk, 35, 0.6f, targetItems, (int)Junk.Broken_Glass, (int)Junk.Feces);
                    break;
                case (int)DFRegion.DungeonTypes.DragonsDen:
                    AddRandomJewelryItem(15, 0.6f, targetItems, (int)Crown_Jewelry.Gem_Encrusted_Silver_Crown, (int)Crown_Jewelry.Gem_Encrusted_Gold_Crown);
                    AddRandomItemsOfSpecificGroup(ItemGroups.Corpse_Parts, 45, 0.3f, targetItems, (int)Corpse_Parts.Animal_Carcass, (int)Corpse_Parts.Animal_Skull, (int)Corpse_Parts.Bone, (int)Corpse_Parts.Humanoid_Skull, (int)Corpse_Parts.Skeletal_Arm);
                    AddSpecificRandomItems(ItemGroups.Junk, 20, 0.6f, targetItems, (int)Junk.Masonry_Rubble);
                    AddSpecificRandomItems(ItemGroups.CreatureIngredients, 25, 0.5f, targetItems, (int)CreatureIngredients.Dragons_scales, (int)CreatureIngredients.Fairy_dragon_scales);
                    break;
                case (int)DFRegion.DungeonTypes.BarbarianStronghold:
                    AddRandomJewelryItem(25, 0.5f, targetItems, (int)Crown_Jewelry.Gem_Encrusted_Silver_Crown, (int)Crown_Jewelry.Gem_Encrusted_Gold_Crown, (int)Crown_Jewelry.Gold_Crown, (int)Crown_Jewelry.Silver_Crown);
                    AddRandomItemsOfSpecificGroup(ItemGroups.Corpse_Parts, 25, 0.3f, targetItems);
                    AddSpecificRandomItems(ItemGroups.Liquid_Containers, 40, 0.4f, targetItems, (int)Liquid_Containers.Empty_Bottle, (int)Liquid_Containers.Wooden_Cup, (int)Liquid_Containers.Hip_Flask, (int)Liquid_Containers.Tin_Goblet);
                    AddRandomItemsOfSpecificGroup(ItemGroups.Containers, 25, 0.5f, targetItems, (int)Containers.Lockbox, (int)Containers.Urn);
                    AddRandomItemsOfSpecificGroup(ItemGroups.Repair_Tools, 10, 0.1f, targetItems, (int)Repair_Tools.Sewing_Kit, (int)Repair_Tools.Jewelers_Pliers, (int)Repair_Tools.Armorers_Hammer, (int)Repair_Tools.Charging_Powder);
                    AddRandomItemsOfSpecificGroup(ItemGroups.Flavor_Tools, 25, 0.3f, targetItems, (int)Flavor_Tools.Scythe, (int)Flavor_Tools.Cane, (int)Flavor_Tools.Broom, (int)Flavor_Tools.Brush, (int)Flavor_Tools.Inside_Caliper, (int)Flavor_Tools.Magnifying_Glass, (int)Flavor_Tools.Mirror, (int)Flavor_Tools.Outside_Caliper, (int)Flavor_Tools.Painters_Palette, (int)Flavor_Tools.Paint_Brush, (int)Flavor_Tools.Proportional_Divider, (int)Flavor_Tools.Quill_And_Ink_Well, (int)Flavor_Tools.Scroll, (int)Flavor_Tools.Spectacles, (int)Flavor_Tools.Triangle_Ruler);
                    AddSpecificRandomItems(ItemGroups.Junk, 20, 0.5f, targetItems, (int)Junk.Broken_Glass, (int)Junk.Dirty_Rags);
                    break;
                case (int)DFRegion.DungeonTypes.VolcanicCaves:
                    AddRandomItemsOfSpecificGroup(ItemGroups.Corpse_Parts, 30, 0.4f, targetItems, (int)Corpse_Parts.Animal_Carcass, (int)Corpse_Parts.Animal_Skull, (int)Corpse_Parts.Bone, (int)Corpse_Parts.Humanoid_Skull, (int)Corpse_Parts.Skeletal_Arm);
                    AddSpecificRandomItems(ItemGroups.Junk, 35, 0.4f, targetItems, (int)Junk.Rock_Rubble);
                    AddIngredGroupRandomItems(ItemGroups.MetalIngredients, 45, 0.6f, targetItems);
                    break;
                case (int)DFRegion.DungeonTypes.ScorpionNest:
                    AddRandomItemsOfSpecificGroup(ItemGroups.Corpse_Parts, 65, 0.4f, targetItems, (int)Corpse_Parts.Charred_Remains, (int)Corpse_Parts.Charred_Humanoid_Skull, (int)Corpse_Parts.Charred_Bone, (int)Corpse_Parts.Charred_Animal_Skull);
                    AddSpecificRandomItems(ItemGroups.Junk, 55, 0.6f, targetItems, (int)Junk.Egg_Sack_Remains);
                    AddSpecificRandomItems(ItemGroups.AnimalPartIngredients, 50, 0.5f, targetItems, (int)AnimalPartIngredients.Small_scorpion_stinger, (int)AnimalPartIngredients.Giant_scorpion_stinger, (int)AnimalPartIngredients.Rat_Tail);
                    break;
                case (int)DFRegion.DungeonTypes.Cemetery:
                    AddSpecificRandomItems(ItemGroups.Jewellery, 25, 0.6f, targetItems, (int)Jewellery.Bracer, (int)Jewellery.Cloth_amulet);
                    AddRandomItemsOfSpecificGroup(ItemGroups.ReligiousItems, 10, 0.6f, targetItems);
                    AddRandomItemsOfSpecificGroup(ItemGroups.Corpse_Parts, 70, 0.5f, targetItems, (int)Corpse_Parts.Animal_Carcass, (int)Corpse_Parts.Animal_Skull, (int)Corpse_Parts.Charred_Bone, (int)Corpse_Parts.Charred_Remains, (int)Corpse_Parts.Charred_Animal_Skull, (int)Corpse_Parts.Charred_Humanoid_Skull);
                    AddSpecificRandomItems(ItemGroups.Containers, 15, 0.3f, targetItems, (int)Containers.Urn);
                    AddSpecificRandomItems(ItemGroups.General_Tools, 35, 0.1f, targetItems, (int)General_Tools.Spade);
                    AddSpecificRandomItems(ItemGroups.Junk, 15, 0.6f, targetItems, (int)Junk.Webbing);
                    break;
                default:
                    break;
            }
        }

        public static void AddSpecificRandomItems(ItemGroups itemGroup, float chance, float chanceMod, List<DaggerfallUnityItem> targetItems, params int[] itemIndices)
        {
            while (Dice100.SuccessRoll((int)chance))
            {
                int itemIndex = itemIndices[Random.Range(0, itemIndices.Length)];
                targetItems.Add(ItemBuilder.CreateItem(itemGroup, itemIndex));
                chance *= chanceMod;
            }
        }

        public static void AddRandomItemsOfSpecificGroup(ItemGroups itemGroup, float chance, float chanceMod, List<DaggerfallUnityItem> targetItems, params int[] itemIndices) // params is extremely handy for my purposes.
        {
            int playerLuck = GameManager.Instance.PlayerEntity.Stats.LiveLuck;

            while (Dice100.SuccessRoll((int)chance))
            {
                targetItems.Add(ItemBuilder.CreateRandomItemOfItemgroup(itemGroup, -1, -1, playerLuck, itemIndices));
                chance *= chanceMod;
            }
        }

        public static void AddRandomJewelryItem(float chance, float chanceMod, List<DaggerfallUnityItem> targetItems, params int[] itemIndices)
        {
            int playerLuck = GameManager.Instance.PlayerEntity.Stats.LiveLuck;

            while (Dice100.SuccessRoll((int)chance))
            {
                targetItems.Add(ItemBuilder.CreateRandomJewelryOfRandomSlot(-1, -1, playerLuck, itemIndices));
                chance *= chanceMod;
            }
        }

        #region Private Methods

        static void PersonalityTraitFlavorItemsGenerator(EnemyEntity AITarget, int[] traits, List<DaggerfallUnityItem> targetItems)
        {
            int level = AITarget.Level;

            if (traits[0] > -1 || traits[1] > -1)
            {
                if (traits[0] == (int)MobilePersonalityQuirks.Curious || traits[1] == (int)MobilePersonalityQuirks.Curious)
                {
                    int randRange = Random.Range(2, 4 + 1);
                    for (int i = 0; i < randRange; i++)
                    {
                        if (Dice100.SuccessRoll(70))
                            targetItems.Add(ItemBuilder.CreateItem(ItemGroups.UselessItems2, (int)UselessItems2.Parchment)); // Not sure best way how to do this part with the different item groups, so hopefully this will work for now. 
                        else
                            targetItems.Add(ItemBuilder.CreateItem(ItemGroups.Flavor_Tools, FormulaHelper.PickOneOfCompact((int)Flavor_Tools.Scroll, 70, (int)Flavor_Tools.Spectacles, 50, (int)Flavor_Tools.Magnifying_Glass, 30)));
                    }
                }

                if (traits[0] == (int)MobilePersonalityQuirks.Addict || traits[1] == (int)MobilePersonalityQuirks.Addict)
                {
                    int randRange = Random.Range(1, 3 + 1);
                    for (int i = 0; i < randRange; i++)
                        targetItems.Add(ItemBuilder.CreateRandomDrug(level, -1, (int)Drugs.Aegrotat, (int)Drugs.Incense_of_Mara, (int)Drugs.Sleeping_Tree_Sap));

                    randRange = Random.Range(1, 3 + 1);
                    for (int i = 0; i < randRange; i++)
                    {
                        if (Dice100.SuccessRoll(40))
                            targetItems.Add(ItemBuilder.CreateItem(ItemGroups.General_Tools, FormulaHelper.PickOneOfCompact((int)General_Tools.Smoking_Pipe, 10, (int)General_Tools.Matchbox, 20)));
                        else if (Dice100.SuccessRoll(40))
                            targetItems.Add(ItemBuilder.CreateItem(ItemGroups.Containers, (int)Containers.Snuff_Box));
                        else
                            targetItems.Add(ItemBuilder.CreateItem(ItemGroups.Flavor_Tools, (int)Flavor_Tools.Metal_Scoop));
                    }
                }

                if (traits[0] == (int)MobilePersonalityQuirks.Nyctophobic || traits[1] == (int)MobilePersonalityQuirks.Nyctophobic)
                {
                    int randRange = Random.Range(1, 2 + 1);
                    for (int i = 0; i < randRange; i++)
                    {
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.General_Tools, (int)General_Tools.Matchbox));
                    }
                }

                if (traits[0] == (int)MobilePersonalityQuirks.Hoarder || traits[1] == (int)MobilePersonalityQuirks.Hoarder)
                {
                    int randRange = Random.Range(6, 11 + 1);
                    for (int i = 0; i < randRange; i++)
                    {
                        if (Dice100.SuccessRoll(70))
                            targetItems.Add(ItemBuilder.CreateRandomItemOfItemgroup(ItemGroups.Flavor_Tools, level, -1));
                        else if (Dice100.SuccessRoll(50))
                            targetItems.Add(ItemBuilder.CreateRandomItemOfItemgroup(ItemGroups.Containers, level, -1, (int)Containers.Barrel, (int)Containers.Lockbox, (int)Containers.Snuff_Box));
                        else if (Dice100.SuccessRoll(50))
                            targetItems.Add(ItemBuilder.CreateRandomItemOfItemgroup(ItemGroups.Liquid_Containers, level, -1, (int)Liquid_Containers.Gem_Encrusted_Silver_Goblet, (int)Liquid_Containers.Gem_Encrusted_Gold_Goblet));
                        else
                            targetItems.Add(ItemBuilder.CreateItem(ItemGroups.Junk, (int)Junk.Dirty_Rags));
                    }
                }

                if (traits[0] == (int)MobilePersonalityQuirks.Vain || traits[1] == (int)MobilePersonalityQuirks.Vain)
                {
                    int randRange = Random.Range(1, 3 + 1);
                    for (int i = 0; i < randRange; i++)
                        targetItems.Add(ItemBuilder.CreateRandomJewelryOfRandomSlot(level, -1, (int)Crown_Jewelry.Gem_Encrusted_Silver_Crown, (int)Crown_Jewelry.Gem_Encrusted_Gold_Crown));

                    randRange = Random.Range(1, 3 + 1);
                    for (int i = 0; i < randRange; i++)
                    {
                        if (Dice100.SuccessRoll(15))
                            targetItems.Add(ItemBuilder.CreateRandomItemOfItemgroup(ItemGroups.Liquid_Containers, level, -1, (int)Liquid_Containers.Empty_Bottle, (int)Liquid_Containers.Wooden_Cup, (int)Liquid_Containers.Hip_Flask));
                    }
                }

                if (traits[0] == (int)MobilePersonalityQuirks.Untrusting || traits[1] == (int)MobilePersonalityQuirks.Untrusting)
                {
                    int randRange = Random.Range(1, 2 + 1);
                    for (int i = 0; i < randRange; i++)
                    {
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.Containers, (int)Containers.Lockbox));
                    }
                    // This won't work the same as the others, since it in theory will be placing existing items into a seperate lock-box inventory type of item, will need a lot of work on this one eventually.
                }

                if (traits[0] == (int)MobilePersonalityQuirks.Sadistic || traits[1] == (int)MobilePersonalityQuirks.Sadistic)
                {
                    int randRange = Random.Range(1, 2 + 1);
                    for (int i = 0; i < randRange; i++)
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.Sex_Toys, FormulaHelper.PickOneOfCompact((int)Sex_Toys.Blindfold, 90, (int)Sex_Toys.Handcuffs, 50, (int)Sex_Toys.Gag, 10)));

                    randRange = Random.Range(1, 2 + 1);
                    for (int i = 0; i < randRange; i++)
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.Flavor_Tools, FormulaHelper.PickOneOfCompact((int)Flavor_Tools.Mallet, 20, (int)Flavor_Tools.Shears, 40, (int)Flavor_Tools.Tongs, 40)));
                }

                if (traits[0] == (int)MobilePersonalityQuirks.Romantic || traits[1] == (int)MobilePersonalityQuirks.Romantic)
                {
                    if (Dice100.SuccessRoll(65))
                        targetItems.Add(ItemBuilder.CreateRandomItemOfItemgroup(ItemGroups.Musical_Instruments, level));

                    targetItems.Add(ItemBuilder.CreateItem(ItemGroups.Flavor_Tools, FormulaHelper.PickOneOfCompact((int)Flavor_Tools.Brush, 90, (int)Flavor_Tools.Mirror, 45, (int)Flavor_Tools.Spyglass, 5)));
                }

                if (traits[0] == (int)MobilePersonalityQuirks.Necrophiliac || traits[1] == (int)MobilePersonalityQuirks.Necrophiliac)
                {
                    int randRange = Random.Range(2, 5 + 1);
                    for (int i = 0; i < randRange; i++)
                        targetItems.Add(ItemBuilder.CreateRandomItemOfItemgroup(ItemGroups.Corpse_Parts));
                }

                if (traits[0] == (int)MobilePersonalityQuirks.Alcoholic || traits[1] == (int)MobilePersonalityQuirks.Alcoholic)
                {
                    if (Dice100.SuccessRoll(60))
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.Liquid_Containers, (int)Liquid_Containers.Hip_Flask));

                    int randRange = Random.Range(3, 6 + 1);
                    for (int i = 0; i < randRange; i++)
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.Liquid_Containers, FormulaHelper.PickOneOfCompact((int)Liquid_Containers.Empty_Bottle, 80, (int)Liquid_Containers.Wooden_Cup, 50, (int)Liquid_Containers.Tin_Goblet, 10)));
                    // Eventually when I start working on food items and such, likely make it so some of these containers will spawn with some sort of alcohol inside them. 
                }
            }

            if (traits[2] > -1)
            {
                if (traits[2] == (int)MobilePersonalityInterests.God_Fearing)
                {
                    int randRange = Random.Range(1, 3 + 1);
                    for (int i = 0; i < randRange; i++)
                        targetItems.Add(ItemBuilder.CreateRandomReligiousItem(level));
                }

                if (traits[2] == (int)MobilePersonalityInterests.Occultist)
                {
                    int randRange = Random.Range(1, 2 + 1);
                    for (int i = 0; i < randRange; i++)
                        targetItems.Add(ItemBuilder.CreateRandomItemOfItemgroup(ItemGroups.Occult_Objects, level));
                }

                if (traits[2] == (int)MobilePersonalityInterests.Childish)
                {
                    int randRange = Random.Range(1, 2 + 1);
                    for (int i = 0; i < randRange; i++)
                        targetItems.Add(ItemBuilder.CreateRandomItemOfItemgroup(ItemGroups.Toys, level));
                }

                if (traits[2] == (int)MobilePersonalityInterests.Artistic)
                {
                    if (Dice100.SuccessRoll(20))
                        targetItems.Add(ItemBuilder.CreateRandomPainting());

                    int randRange = Random.Range(1, 3 + 1);
                    for (int i = 0; i < randRange; i++)
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.Flavor_Tools, FormulaHelper.PickOneOfCompact((int)Flavor_Tools.Paint_Brush, 55, (int)Flavor_Tools.Painters_Palette, 25, (int)Flavor_Tools.Proportional_Divider, 35)));
                }

                if (traits[2] == (int)MobilePersonalityInterests.Collector)
                {
                    int randRange = Random.Range(4, 7 + 1);
                    for (int i = 0; i < randRange; i++)
                    {
                        if (Dice100.SuccessRoll(60))
                            targetItems.Add(ItemBuilder.CreateRandomItemOfItemgroup(ItemGroups.Flavor_Tools, level, -1, (int)Flavor_Tools.Basket, (int)Flavor_Tools.Bellows, (int)Flavor_Tools.Broom, (int)Flavor_Tools.Brush, (int)Flavor_Tools.Cane, (int)Flavor_Tools.Frying_Pan, (int)Flavor_Tools.Metal_Scoop, (int)Flavor_Tools.Net, (int)Flavor_Tools.Paint_Brush, (int)Flavor_Tools.Scythe, (int)Flavor_Tools.Shears, (int)Flavor_Tools.Tongs, (int)Flavor_Tools.Trowel, (int)Flavor_Tools.Wooden_Scoop, (int)Flavor_Tools.Wooden_Shovel, (int)Flavor_Tools.Wooden_Spoon));
                        else if (Dice100.SuccessRoll(50))
                            targetItems.Add(ItemBuilder.CreateRandomItemOfItemgroup(ItemGroups.Containers, level, -1, (int)Containers.Barrel, (int)Containers.Lockbox, (int)Containers.Snuff_Box, (int)Containers.Bucket, (int)Containers.Quiver));
                        else if (Dice100.SuccessRoll(25))
                            targetItems.Add(ItemBuilder.CreateRandomItemOfItemgroup(ItemGroups.Liquid_Containers, level, -1, (int)Liquid_Containers.Empty_Bottle, (int)Liquid_Containers.Wooden_Cup, (int)Liquid_Containers.Hip_Flask, (int)Liquid_Containers.Tin_Goblet));
                        else if (Dice100.SuccessRoll(50))
                            targetItems.Add(ItemBuilder.CreateRandomItemOfItemgroup(ItemGroups.Magic_Amplifiers, level));
                        else
                            targetItems.Add(ItemBuilder.CreateRandomGem(level));
                        // I'll likely change this at some point, I feel like it's too similar to the hoarder for the most part, will see.
                    }
                }

                if (traits[2] == (int)MobilePersonalityInterests.Survivalist)
                {
                    if (Dice100.SuccessRoll(40))
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.General_Tools, (int)General_Tools.Compass));
                    if (Dice100.SuccessRoll(75))
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.General_Tools, (int)General_Tools.Matchbox));
                    if (Dice100.SuccessRoll(55))
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.General_Tools, (int)General_Tools.Rope));
                    if (Dice100.SuccessRoll(50))
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.Containers, (int)Containers.Quiver));
                    if (Dice100.SuccessRoll(60))
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.Furniture, (int)Furniture.Tent));
                }

                if (traits[2] == (int)MobilePersonalityInterests.Hunter)
                {
                    if (Dice100.SuccessRoll(25))
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.Corpse_Parts, FormulaHelper.PickOneOfCompact((int)Corpse_Parts.Animal_Skull, 1, (int)Corpse_Parts.Animal_Carcass, 1)));
                    if (Dice100.SuccessRoll(40))
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.General_Tools, (int)General_Tools.Rope));
                    if (Dice100.SuccessRoll(60))
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.Flavor_Tools, (int)Flavor_Tools.Net));
                    if (Dice100.SuccessRoll(75))
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.Containers, (int)Containers.Quiver));
                    if (Dice100.SuccessRoll(15))
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.Flavor_Tools, (int)Flavor_Tools.Frying_Pan));
                }

                if (traits[2] == (int)MobilePersonalityInterests.Fetishist)
                {
                    int randRange = Random.Range(2, 3 + 1);
                    for (int i = 0; i < randRange; i++)
                        targetItems.Add(ItemBuilder.CreateRandomItemOfItemgroup(ItemGroups.Sex_Toys, level));
                }

                if (traits[2] == (int)MobilePersonalityInterests.Brewer)
                {
                    targetItems.Add(ItemBuilder.CreateItem(ItemGroups.Liquid_Containers, (int)Liquid_Containers.Empty_Bottle));
                }

                if (traits[2] == (int)MobilePersonalityInterests.Cartographer)
                {
                    int randRange = Random.Range(2, 4 + 1);
                    for (int i = 0; i < randRange; i++)
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.Flavor_Tools, FormulaHelper.PickOneOfCompact((int)Flavor_Tools.Inside_Caliper, 60, (int)Flavor_Tools.Outside_Caliper, 60, (int)Flavor_Tools.Quill_And_Ink_Well, 50, (int)Flavor_Tools.Proportional_Divider, 70, (int)Flavor_Tools.Spyglass, 10, (int)Flavor_Tools.Triangle_Ruler, 70, (int)Flavor_Tools.Magnifying_Glass, 10)));
                    randRange = Random.Range(1, 3 + 1);
                    for (int i = 0; i < randRange; i++)
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.UselessItems2, (int)UselessItems2.Parchment));
                    if (Dice100.SuccessRoll(70))
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.General_Tools, (int)General_Tools.Compass));
                }

                if (traits[2] == (int)MobilePersonalityInterests.Fisher)
                {
                    targetItems.Add(ItemBuilder.CreateItem(ItemGroups.General_Tools, FormulaHelper.PickOneOfCompact((int)General_Tools.Fishing_Pole, 50, (int)General_Tools.Fishing_Rod, 5)));
                    if (Dice100.SuccessRoll(75))
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.Flavor_Tools, (int)Flavor_Tools.Net));
                    if (Dice100.SuccessRoll(35))
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.Flavor_Tools, (int)Flavor_Tools.Frying_Pan));
                }

                if (traits[2] == (int)MobilePersonalityInterests.Diver)
                {
                    targetItems.Add(ItemBuilder.CreateItem(ItemGroups.Flavor_Tools, (int)Flavor_Tools.Basket));
                    // Need to add more items for this interest, I don't really have any to choose from atm honestly, oh well.
                }

                if (traits[2] == (int)MobilePersonalityInterests.Writer)
                {
                    targetItems.Add(ItemBuilder.CreateItem(ItemGroups.Flavor_Tools, (int)Flavor_Tools.Quill_And_Ink_Well));
                    int randRange = Random.Range(2, 6 + 1);
                    for (int i = 0; i < randRange; i++)
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.UselessItems2, (int)UselessItems2.Parchment));
                    randRange = Random.Range(1, 3 + 1);
                    for (int i = 0; i < randRange; i++)
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.Flavor_Tools, FormulaHelper.PickOneOfCompact((int)Flavor_Tools.Magnifying_Glass, 20, (int)Flavor_Tools.Spectacles, 30, (int)Flavor_Tools.Scroll, 70)));
                }

                if (traits[2] == (int)MobilePersonalityInterests.Handy)
                {
                    int randRange = Random.Range(1, 3 + 1);
                    for (int i = 0; i < randRange; i++)
                        targetItems.Add(ItemBuilder.CreateRandomItemOfItemgroup(ItemGroups.Repair_Tools, level));
                    randRange = Random.Range(1, 2 + 1);
                    for (int i = 0; i < randRange; i++)
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.Flavor_Tools, FormulaHelper.PickOneOfCompact((int)Flavor_Tools.Mallet, 40, (int)Flavor_Tools.Tongs, 30, (int)Flavor_Tools.Trowel, 20)));
                }
            }

            return;
        }

        public static void AddIngredientsBasedOnEnemy(EnemyEntity AITarget, int[] traits, List<DaggerfallUnityItem> targetItems)
        {
            DaggerfallUnityItem ingredients = null;
            int enemyLevel = AITarget.Level;
            int rolledStackSize = 0;

            if (AITarget.EntityType == EntityTypes.EnemyClass) // I'll likely want to do more work on these for the human enemies, just did it somewhat lazily to get it done here. 
            {
                switch (AITarget.CareerIndex)
                {
                    case (int)ClassCareers.Mage:
                    case (int)ClassCareers.Spellsword:
                    case (int)ClassCareers.Battlemage:
                    case (int)ClassCareers.Sorcerer:
                    case (int)ClassCareers.Healer:
                    case (int)ClassCareers.Nightblade:
                        rolledStackSize = FormulaHelper.PickOneOfCompact(0, 10, 1, 4, 2, 1);
                        for (int i = 0; i < rolledStackSize; i++)
                            targetItems.Add(ItemBuilder.CreateRandomIngredientOfGroup(ItemGroups.MiscPlantIngredients, enemyLevel, -1, (int)MiscPlantIngredients.Giant_Puffball, (int)MiscPlantIngredients.Glowing_Mushroom, (int)MiscPlantIngredients.Onion));
                        rolledStackSize = FormulaHelper.PickOneOfCompact(0, 10, 1, 5, 2, 1);
                        for (int i = 0; i < rolledStackSize; i++)
                            targetItems.Add(ItemBuilder.CreateRandomIngredientOfGroup(ItemGroups.FlowerPlantIngredients, enemyLevel));
                        rolledStackSize = FormulaHelper.PickOneOfCompact(0, 12, 1, 5, 2, 1);
                        for (int i = 0; i < rolledStackSize; i++)
                            targetItems.Add(ItemBuilder.CreateRandomIngredientOfGroup(ItemGroups.FruitPlantIngredients, enemyLevel, -1, (int)FruitPlantIngredients.Banana, (int)FruitPlantIngredients.Grapes, (int)FruitPlantIngredients.Kiwi, (int)FruitPlantIngredients.Lemon, (int)FruitPlantIngredients.Lime, (int)FruitPlantIngredients.Orange, (int)FruitPlantIngredients.Pear, (int)FruitPlantIngredients.Pomegranate));
                        rolledStackSize = FormulaHelper.PickOneOfCompact(0, 30, 1, 6);
                        for (int i = 0; i < rolledStackSize; i++)
                            targetItems.Add(ItemBuilder.CreateRandomIngredientOfGroup(ItemGroups.CreatureIngredients, enemyLevel));
                        rolledStackSize = FormulaHelper.PickOneOfCompact(0, 10, 1, 5, 2, 1);
                        for (int i = 0; i < rolledStackSize; i++)
                            targetItems.Add(ItemBuilder.CreateRandomIngredientOfGroup(ItemGroups.SolventIngredients, enemyLevel));
                        rolledStackSize = FormulaHelper.PickOneOfCompact(0, 20, 1, 4);
                        for (int i = 0; i < rolledStackSize; i++)
                            targetItems.Add(ItemBuilder.CreateRandomIngredientOfGroup(ItemGroups.MetalIngredients, enemyLevel));
                        break;
                    case (int)ClassCareers.Bard:
                        rolledStackSize = FormulaHelper.PickOneOfCompact(0, 12, 1, 4);
                        for (int i = 0; i < rolledStackSize; i++)
                            targetItems.Add(ItemBuilder.CreateRandomIngredientOfGroup(ItemGroups.FlowerPlantIngredients, enemyLevel));
                        rolledStackSize = FormulaHelper.PickOneOfCompact(0, 9, 1, 3);
                        for (int i = 0; i < rolledStackSize; i++)
                            targetItems.Add(ItemBuilder.CreateRandomIngredientOfGroup(ItemGroups.MetalIngredients, enemyLevel));
                        break;
                    case (int)ClassCareers.Burglar:
                        rolledStackSize = FormulaHelper.PickOneOfCompact(1, 12, 2, 7, 3, 1);
                        for (int i = 0; i < rolledStackSize; i++)
                            targetItems.Add(ItemBuilder.CreateRandomIngredientOfGroup(ItemGroups.MetalIngredients, enemyLevel));
                        break;
                    case (int)ClassCareers.Rogue:
                        rolledStackSize = FormulaHelper.PickOneOfCompact(0, 6, 1, 2);
                        for (int i = 0; i < rolledStackSize; i++)
                            targetItems.Add(ItemBuilder.CreateRandomIngredientOfGroup(ItemGroups.MetalIngredients, enemyLevel));
                        break;
                    case (int)ClassCareers.Acrobat:
                        rolledStackSize = FormulaHelper.PickOneOfCompact(0, 12, 1, 4);
                        for (int i = 0; i < rolledStackSize; i++)
                            targetItems.Add(ItemBuilder.CreateRandomIngredientOfGroup(ItemGroups.FlowerPlantIngredients, enemyLevel));
                        break;
                    case (int)ClassCareers.Thief:
                        rolledStackSize = FormulaHelper.PickOneOfCompact(1, 18, 2, 7, 3, 1);
                        for (int i = 0; i < rolledStackSize; i++)
                            targetItems.Add(ItemBuilder.CreateRandomIngredientOfGroup(ItemGroups.MetalIngredients, enemyLevel));
                        break;
                    case (int)ClassCareers.Assassin:
                    case (int)ClassCareers.Archer:
                    case (int)ClassCareers.Ranger:
                        rolledStackSize = FormulaHelper.PickOneOfCompact(0, 8, 1, 4, 2, 1);
                        for (int i = 0; i < rolledStackSize; i++)
                            targetItems.Add(ItemBuilder.CreateRandomIngredientOfGroup(ItemGroups.MiscPlantIngredients, enemyLevel, -1, (int)MiscPlantIngredients.Giant_Puffball, (int)MiscPlantIngredients.Glowing_Mushroom, (int)MiscPlantIngredients.Onion));
                        rolledStackSize = FormulaHelper.PickOneOfCompact(0, 12, 1, 5, 2, 1);
                        for (int i = 0; i < rolledStackSize; i++)
                            targetItems.Add(ItemBuilder.CreateRandomIngredientOfGroup(ItemGroups.FlowerPlantIngredients, enemyLevel));
                        rolledStackSize = FormulaHelper.PickOneOfCompact(0, 18, 1, 5, 2, 1);
                        for (int i = 0; i < rolledStackSize; i++)
                            targetItems.Add(ItemBuilder.CreateRandomIngredientOfGroup(ItemGroups.FruitPlantIngredients, enemyLevel, -1, (int)FruitPlantIngredients.Banana, (int)FruitPlantIngredients.Grapes, (int)FruitPlantIngredients.Kiwi, (int)FruitPlantIngredients.Lemon, (int)FruitPlantIngredients.Lime, (int)FruitPlantIngredients.Orange, (int)FruitPlantIngredients.Pear, (int)FruitPlantIngredients.Pomegranate));
                        rolledStackSize = FormulaHelper.PickOneOfCompact(0, 20, 1, 6);
                        for (int i = 0; i < rolledStackSize; i++)
                            targetItems.Add(ItemBuilder.CreateRandomIngredientOfGroup(ItemGroups.AnimalPartIngredients, enemyLevel));
                        rolledStackSize = FormulaHelper.PickOneOfCompact(0, 10, 1, 5, 2, 1);
                        for (int i = 0; i < rolledStackSize; i++)
                            targetItems.Add(ItemBuilder.CreateRandomIngredientOfGroup(ItemGroups.SolventIngredients, enemyLevel));
                        rolledStackSize = FormulaHelper.PickOneOfCompact(0, 22, 1, 4);
                        for (int i = 0; i < rolledStackSize; i++)
                            targetItems.Add(ItemBuilder.CreateRandomIngredientOfGroup(ItemGroups.MetalIngredients, enemyLevel));
                        break;
                    case (int)ClassCareers.Knight:
                        rolledStackSize = FormulaHelper.PickOneOfCompact(0, 25, 1, 7);
                        for (int i = 0; i < rolledStackSize; i++)
                            targetItems.Add(ItemBuilder.CreateRandomIngredientOfGroup(ItemGroups.CreatureIngredients, enemyLevel, -1, (int)CreatureIngredients.Werewolfs_blood, (int)CreatureIngredients.Fairy_dragon_scales, (int)CreatureIngredients.Nymph_hair, (int)CreatureIngredients.Unicorn_horn, (int)CreatureIngredients.Troll_blood, (int)CreatureIngredients.Dragons_scales, (int)CreatureIngredients.Giant_blood, (int)CreatureIngredients.Harpy_Feather, (int)CreatureIngredients.Saints_hair, (int)CreatureIngredients.Orcs_blood, (int)CreatureIngredients.Bone_Meal, (int)CreatureIngredients.Dreugh_Wax, (int)CreatureIngredients.Fire_Powder, (int)CreatureIngredients.Permafrost_Shavings, (int)CreatureIngredients.Imp_Heart, (int)CreatureIngredients.Gargoyle_Horn));
                        break;
                    case (int)ClassCareers.Monk:
                    case (int)ClassCareers.Barbarian:
                    case (int)ClassCareers.Warrior:
                    default:
                        break;
                }
            }
            else
            {
                switch (AITarget.CareerIndex)
                {
                    case 0:
                        if (Dice100.SuccessRoll(65))
                            targetItems.Add(ItemBuilder.CreateItem(ItemGroups.AnimalPartIngredients, (int)AnimalPartIngredients.Rat_Tail));
                        rolledStackSize = Random.Range(0, 4 + 1);
                        if (rolledStackSize > 0)
                        {
                            ingredients = ItemBuilder.CreateItem(ItemGroups.AnimalPartIngredients, (int)AnimalPartIngredients.Small_tooth);
                            ingredients.stackCount = rolledStackSize;
                            targetItems.Add(ingredients);
                        }
                        break;
                    case 3:
                        rolledStackSize = Random.Range(0, 7 + 1);
                        if (rolledStackSize > 0)
                        {
                            ingredients = ItemBuilder.CreateItem(ItemGroups.AnimalPartIngredients, (int)AnimalPartIngredients.Small_tooth);
                            ingredients.stackCount = rolledStackSize;
                            targetItems.Add(ingredients);
                        }
                        break;
                    case 4:
                    case 5:
                        rolledStackSize = Random.Range(0, 9 + 1);
                        if (rolledStackSize > 0)
                        {
                            ingredients = ItemBuilder.CreateItem(ItemGroups.AnimalPartIngredients, (int)AnimalPartIngredients.Big_tooth);
                            ingredients.stackCount = rolledStackSize;
                            targetItems.Add(ingredients);
                        }
                        break;
                    case 6:
                        rolledStackSize = Random.Range(0, 3 + 1);
                        if (rolledStackSize > 0)
                        {
                            ingredients = ItemBuilder.CreateItem(ItemGroups.AnimalPartIngredients, (int)AnimalPartIngredients.Spider_Eye);
                            ingredients.stackCount = rolledStackSize;
                            targetItems.Add(ingredients);
                        }
                        rolledStackSize = Random.Range(0, 3 + 1);
                        if (rolledStackSize > 0)
                        {
                            ingredients = ItemBuilder.CreateItem(ItemGroups.AnimalPartIngredients, (int)AnimalPartIngredients.Spider_venom);
                            ingredients.stackCount = rolledStackSize;
                            targetItems.Add(ingredients);
                        }
                        break;
                    case 11:
                        if (Dice100.SuccessRoll(10))
                            targetItems.Add(ItemBuilder.CreateItem(ItemGroups.AnimalPartIngredients, (int)AnimalPartIngredients.Pearl));
                        rolledStackSize = Random.Range(0, 6 + 1);
                        if (rolledStackSize > 0)
                        {
                            ingredients = ItemBuilder.CreateItem(ItemGroups.AnimalPartIngredients, (int)AnimalPartIngredients.Small_tooth);
                            ingredients.stackCount = rolledStackSize;
                            targetItems.Add(ingredients);
                        }
                        rolledStackSize = Random.Range(0, 3 + 1);
                        if (rolledStackSize > 0)
                        {
                            ingredients = ItemBuilder.CreateItem(ItemGroups.AnimalPartIngredients, (int)AnimalPartIngredients.Slaughterfish_Scales);
                            ingredients.stackCount = rolledStackSize;
                            targetItems.Add(ingredients);
                        }
                        break;
                    case 20:
                        if (Dice100.SuccessRoll(100))
                            targetItems.Add(ItemBuilder.CreateItem(ItemGroups.AnimalPartIngredients, (int)AnimalPartIngredients.Giant_scorpion_stinger));
                        break;
                    case 1:
                        if (Dice100.SuccessRoll(100))
                            targetItems.Add(ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Imp_Heart)); // Possibly give more loot, maybe. 
                        break;
                    case 2:
                        rolledStackSize = Random.Range(1, 5 + 1);
                        for (int i = 0; i < rolledStackSize; i++)
                            targetItems.Add(ItemBuilder.CreateRandomIngredientOfGroup(ItemGroups.MiscPlantIngredients, enemyLevel, -1, (int)MiscPlantIngredients.Bamboo, (int)MiscPlantIngredients.Palm, (int)MiscPlantIngredients.Aloe, (int)MiscPlantIngredients.Arrowroot, (int)MiscPlantIngredients.Black_Trumpet, (int)MiscPlantIngredients.Garlic, (int)MiscPlantIngredients.Giant_Puffball, (int)MiscPlantIngredients.Ginseng_Root, (int)MiscPlantIngredients.Beech_Mushrooms, (int)MiscPlantIngredients.Mint, (int)MiscPlantIngredients.Onion));
                        rolledStackSize = Random.Range(1, 4 + 1);
                        for (int i = 0; i < rolledStackSize; i++)
                            targetItems.Add(ItemBuilder.CreateRandomIngredientOfGroup(ItemGroups.FlowerPlantIngredients, enemyLevel, -1, (int)FlowerPlantIngredients.Fennel_Flower, (int)FlowerPlantIngredients.Dragons_Flower, (int)FlowerPlantIngredients.Foxglove_Flower, (int)FlowerPlantIngredients.Wild_Bergamot));
                        rolledStackSize = Random.Range(0, 2 + 1);
                        for (int i = 0; i < rolledStackSize; i++)
                            targetItems.Add(ItemBuilder.CreateRandomIngredientOfGroup(ItemGroups.FruitPlantIngredients, enemyLevel, -1, (int)FruitPlantIngredients.Cactus, (int)FruitPlantIngredients.Banana, (int)FruitPlantIngredients.Grapes, (int)FruitPlantIngredients.Kiwi, (int)FruitPlantIngredients.Lemon, (int)FruitPlantIngredients.Lime, (int)FruitPlantIngredients.Orange, (int)FruitPlantIngredients.Pear, (int)FruitPlantIngredients.Pomegranate));
                        if (Dice100.SuccessRoll(20))
                            targetItems.Add(ItemBuilder.CreateItem(ItemGroups.Gems, (int)Gems.Amber));
                        break;
                    case 10:
                        rolledStackSize = Random.Range(1, 4 + 1);
                        for (int i = 0; i < rolledStackSize; i++)
                            targetItems.Add(ItemBuilder.CreateRandomIngredientOfGroup(ItemGroups.FlowerPlantIngredients, enemyLevel));
                        rolledStackSize = FormulaHelper.PickOneOfCompact(1, 5, 2, 3, 3, 1);
                        if (rolledStackSize > 0)
                        {
                            ingredients = ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Nymph_hair);
                            ingredients.stackCount = rolledStackSize;
                            targetItems.Add(ingredients);
                        }
                        break;
                    case 13:
                        rolledStackSize = Random.Range(3, 9 + 1);
                        if (rolledStackSize > 0)
                        {
                            ingredients = ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Harpy_Feather);
                            ingredients.stackCount = rolledStackSize;
                            targetItems.Add(ingredients);
                        }
                        break;
                    case 16:
                        rolledStackSize = Random.Range(1, 5 + 1);
                        if (rolledStackSize > 0)
                        {
                            ingredients = ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Giant_blood);
                            ingredients.stackCount = rolledStackSize;
                            targetItems.Add(ingredients);
                        }
                        break;
                    case 22:
                        rolledStackSize = Random.Range(0, 3 + 1);
                        if (rolledStackSize > 0)
                        {
                            ingredients = ItemBuilder.CreateItem(ItemGroups.MiscPlantIngredients, (int)MiscPlantIngredients.Root_tendrils);
                            ingredients.stackCount = rolledStackSize;
                            targetItems.Add(ingredients);
                        }
                        rolledStackSize = Random.Range(0, 2 + 1);
                        if (rolledStackSize > 0)
                        {
                            ingredients = ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Gargoyle_Horn);
                            ingredients.stackCount = rolledStackSize;
                            targetItems.Add(ingredients);
                        }
                        rolledStackSize = Random.Range(2, 7 + 1);
                        if (rolledStackSize > 0)
                        {
                            ingredients = ItemBuilder.CreateItem(ItemGroups.MetalIngredients, (int)MetalIngredients.Lodestone);
                            ingredients.stackCount = rolledStackSize;
                            targetItems.Add(ingredients);
                        }
                        break;
                    case 34:
                        rolledStackSize = Random.Range(2, 6 + 1);
                        if (rolledStackSize > 0)
                        {
                            ingredients = ItemBuilder.CreateItem(ItemGroups.AnimalPartIngredients, (int)AnimalPartIngredients.Small_tooth);
                            ingredients.stackCount = rolledStackSize;
                            targetItems.Add(ingredients);
                        }
                        rolledStackSize = Random.Range(1, 3 + 1);
                        for (int i = 0; i < rolledStackSize; i++)
                        {
                            if (Dice100.SuccessRoll(35))
                                targetItems.Add(ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Dragons_scales));
                            else
                                targetItems.Add(ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Fairy_dragon_scales));
                        }
                        break;
                    case 40:
                        rolledStackSize = Random.Range(3, 8 + 1);
                        if (rolledStackSize > 0)
                        {
                            ingredients = ItemBuilder.CreateItem(ItemGroups.AnimalPartIngredients, (int)AnimalPartIngredients.Big_tooth);
                            ingredients.stackCount = rolledStackSize;
                            targetItems.Add(ingredients);
                        }
                        rolledStackSize = Random.Range(3, 10 + 1);
                        for (int i = 0; i < rolledStackSize; i++)
                        {
                            if (Dice100.SuccessRoll(75))
                                targetItems.Add(ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Dragons_scales));
                            else
                                targetItems.Add(ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Fairy_dragon_scales));
                        }
                        break;
                    case 41:
                        if (Dice100.SuccessRoll(40))
                            targetItems.Add(ItemBuilder.CreateItem(ItemGroups.AnimalPartIngredients, (int)AnimalPartIngredients.Pearl));
                        rolledStackSize = Random.Range(1, 4 + 1);
                        if (rolledStackSize > 0)
                        {
                            ingredients = ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Dreugh_Wax);
                            ingredients.stackCount = rolledStackSize;
                            targetItems.Add(ingredients);
                        }
                        break;
                    case 42:
                        if (Dice100.SuccessRoll(65))
                            targetItems.Add(ItemBuilder.CreateItem(ItemGroups.AnimalPartIngredients, (int)AnimalPartIngredients.Pearl));
                        rolledStackSize = Random.Range(2, 6 + 1);
                        for (int i = 0; i < rolledStackSize; i++)
                        {
                            if (Dice100.SuccessRoll(40))
                                targetItems.Add(ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Nymph_hair));
                            else
                                targetItems.Add(ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Gorgon_snake));
                        }
                        break;
                    case 7:
                    case 12:
                    case 24:
                        rolledStackSize = Random.Range(1, 2 + 1);
                        if (rolledStackSize > 0)
                        {
                            ingredients = ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Orcs_blood);
                            ingredients.stackCount = rolledStackSize;
                            targetItems.Add(ingredients);
                        }
                        break;
                    case 21:
                        rolledStackSize = FormulaHelper.PickOneOfCompact(0, 8, 1, 3, 2, 1);
                        for (int i = 0; i < rolledStackSize; i++)
                            targetItems.Add(ItemBuilder.CreateRandomIngredientOfGroup(ItemGroups.MiscPlantIngredients, enemyLevel, -1, (int)MiscPlantIngredients.Giant_Puffball, (int)MiscPlantIngredients.Glowing_Mushroom, (int)MiscPlantIngredients.Onion));
                        rolledStackSize = FormulaHelper.PickOneOfCompact(0, 7, 1, 4, 2, 1);
                        for (int i = 0; i < rolledStackSize; i++)
                            targetItems.Add(ItemBuilder.CreateRandomIngredientOfGroup(ItemGroups.FlowerPlantIngredients, enemyLevel));
                        rolledStackSize = FormulaHelper.PickOneOfCompact(0, 5, 1, 4, 2, 1);
                        for (int i = 0; i < rolledStackSize; i++)
                            targetItems.Add(ItemBuilder.CreateRandomIngredientOfGroup(ItemGroups.FruitPlantIngredients, enemyLevel, -1, (int)FruitPlantIngredients.Banana, (int)FruitPlantIngredients.Grapes, (int)FruitPlantIngredients.Kiwi, (int)FruitPlantIngredients.Lemon, (int)FruitPlantIngredients.Lime, (int)FruitPlantIngredients.Orange, (int)FruitPlantIngredients.Pear, (int)FruitPlantIngredients.Pomegranate));
                        rolledStackSize = FormulaHelper.PickOneOfCompact(0, 10, 1, 3);
                        for (int i = 0; i < rolledStackSize; i++)
                            targetItems.Add(ItemBuilder.CreateRandomIngredientOfGroup(ItemGroups.AnimalPartIngredients, enemyLevel, -1, (int)AnimalPartIngredients.Giant_scorpion_stinger, (int)AnimalPartIngredients.Pearl));
                        rolledStackSize = Random.Range(1, 2 + 1);
                        if (rolledStackSize > 0)
                        {
                            ingredients = ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Orcs_blood);
                            ingredients.stackCount = rolledStackSize;
                            targetItems.Add(ingredients);
                        }
                        rolledStackSize = FormulaHelper.PickOneOfCompact(0, 7, 1, 4, 2, 1);
                        for (int i = 0; i < rolledStackSize; i++)
                            targetItems.Add(ItemBuilder.CreateRandomIngredientOfGroup(ItemGroups.SolventIngredients, enemyLevel));
                        rolledStackSize = FormulaHelper.PickOneOfCompact(0, 13, 1, 3);
                        for (int i = 0; i < rolledStackSize; i++)
                            targetItems.Add(ItemBuilder.CreateRandomIngredientOfGroup(ItemGroups.MetalIngredients, enemyLevel));
                        break;
                    case 9:
                        rolledStackSize = Random.Range(1, 4 + 1);
                        if (rolledStackSize > 0)
                        {
                            ingredients = ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Werewolfs_blood);
                            ingredients.stackCount = rolledStackSize;
                            targetItems.Add(ingredients);
                        }
                        break;
                    case 14:
                        rolledStackSize = FormulaHelper.PickOneOfCompact(0, 1, 1, 2, 2, 1);
                        if (rolledStackSize > 0)
                        {
                            ingredients = ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Wereboar_tusk);
                            ingredients.stackCount = rolledStackSize;
                            targetItems.Add(ingredients);
                        }
                        break;
                    case 35:
                        if (Dice100.SuccessRoll(20))
                            targetItems.Add(ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Fire_Powder));
                        rolledStackSize = Random.Range(2, 8 + 1);
                        if (rolledStackSize > 0)
                        {
                            ingredients = ItemBuilder.CreateItem(ItemGroups.MetalIngredients, (int)MetalIngredients.Sulphur);
                            ingredients.stackCount = rolledStackSize;
                            targetItems.Add(ingredients);
                        }
                        break;
                    case 36:
                        rolledStackSize = Random.Range(2, 8 + 1);
                        if (rolledStackSize > 0)
                        {
                            ingredients = ItemBuilder.CreateItem(ItemGroups.MetalIngredients, (int)MetalIngredients.Iron);
                            ingredients.stackCount = rolledStackSize;
                            targetItems.Add(ingredients);
                        }
                        break;
                    case 37:
                        rolledStackSize = Random.Range(1, 2 + 1);
                        if (rolledStackSize > 0)
                        {
                            ingredients = ItemBuilder.CreateItem(ItemGroups.SolventIngredients, (int)SolventIngredients.Ichor);
                            ingredients.stackCount = rolledStackSize;
                            targetItems.Add(ingredients);
                        }
                        break;
                    case 38:
                        if (Dice100.SuccessRoll(15))
                            targetItems.Add(ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Permafrost_Shavings));
                        rolledStackSize = Random.Range(2, 6 + 1);
                        if (rolledStackSize > 0)
                        {
                            ingredients = ItemBuilder.CreateItem(ItemGroups.SolventIngredients, (int)SolventIngredients.Pure_water);
                            ingredients.stackCount = rolledStackSize;
                            targetItems.Add(ingredients);
                        }
                        break;
                    case 15:
                        rolledStackSize = Random.Range(0, 3 + 1);
                        if (rolledStackSize > 0)
                        {
                            ingredients = ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Bone_Meal);
                            ingredients.stackCount = rolledStackSize;
                            targetItems.Add(ingredients);
                        }
                        break;
                    case 18:
                        rolledStackSize = Random.Range(1, 2 + 1);
                        if (rolledStackSize > 0)
                        {
                            ingredients = ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Ectoplasm);
                            ingredients.stackCount = rolledStackSize;
                            targetItems.Add(ingredients);
                        }
                        break;
                    case 19:
                        rolledStackSize = Random.Range(1, 3 + 1);
                        if (rolledStackSize > 0)
                        {
                            ingredients = ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Mummy_wrappings);
                            ingredients.stackCount = rolledStackSize;
                            targetItems.Add(ingredients);
                        }
                        break;
                    case 23:
                        rolledStackSize = Random.Range(1, 4 + 1);
                        for (int i = 0; i < rolledStackSize; i++)
                        {
                            if (Dice100.SuccessRoll(65))
                                targetItems.Add(ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Ectoplasm));
                            else
                                targetItems.Add(ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Wraith_essence));
                        }
                        break;
                    case 28:
                    case 30:
                        if (Dice100.SuccessRoll(100))
                            targetItems.Add(ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Vampire_Dust));
                        rolledStackSize = Random.Range(0, 2 + 1);
                        if (rolledStackSize > 0)
                        {
                            ingredients = ItemBuilder.CreateItem(ItemGroups.AnimalPartIngredients, (int)AnimalPartIngredients.Small_tooth);
                            ingredients.stackCount = rolledStackSize;
                            targetItems.Add(ingredients);
                        }
                        break;
                    case 32:
                    case 33:
                        rolledStackSize = Random.Range(0, 2 + 1);
                        if (rolledStackSize > 0)
                        {
                            ingredients = ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Bone_Meal);
                            ingredients.stackCount = rolledStackSize;
                            targetItems.Add(ingredients);
                        }
                        rolledStackSize = Random.Range(1, 4 + 1);
                        if (rolledStackSize > 0)
                        {
                            ingredients = ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Lich_dust);
                            ingredients.stackCount = rolledStackSize;
                            targetItems.Add(ingredients);
                        }
                        break;
                    case 25:
                        if (Dice100.SuccessRoll(100))
                            targetItems.Add(ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Permafrost_Shavings));
                        if (Dice100.SuccessRoll(100))
                            targetItems.Add(ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Daedra_heart));
                        rolledStackSize = Random.Range(1, 3 + 1);
                        if (rolledStackSize > 0)
                        {
                            ingredients = ItemBuilder.CreateItem(ItemGroups.SolventIngredients, (int)SolventIngredients.Pure_water);
                            ingredients.stackCount = rolledStackSize;
                            targetItems.Add(ingredients);
                        }
                        break;
                    case 26:
                        if (Dice100.SuccessRoll(100))
                            targetItems.Add(ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Fire_Powder));
                        if (Dice100.SuccessRoll(100))
                            targetItems.Add(ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Daedra_heart));
                        rolledStackSize = Random.Range(1, 3 + 1);
                        if (rolledStackSize > 0)
                        {
                            ingredients = ItemBuilder.CreateItem(ItemGroups.MetalIngredients, (int)MetalIngredients.Sulphur);
                            ingredients.stackCount = rolledStackSize;
                            targetItems.Add(ingredients);
                        }
                        break;
                    case 27:
                        rolledStackSize = Random.Range(0, 6 + 1);
                        if (rolledStackSize > 0)
                        {
                            ingredients = ItemBuilder.CreateItem(ItemGroups.AnimalPartIngredients, (int)AnimalPartIngredients.Big_tooth);
                            ingredients.stackCount = rolledStackSize;
                            targetItems.Add(ingredients);
                        }
                        if (Dice100.SuccessRoll(100))
                            targetItems.Add(ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Daedra_heart));
                        break;
                    case 29:
                        if (Dice100.SuccessRoll(100))
                            targetItems.Add(ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Daedra_heart));
                        rolledStackSize = Random.Range(0, 2 + 1);
                        if (rolledStackSize > 0)
                        {
                            ingredients = ItemBuilder.CreateItem(ItemGroups.SolventIngredients, (int)SolventIngredients.Elixir_vitae);
                            ingredients.stackCount = rolledStackSize;
                            targetItems.Add(ingredients);
                        }
                        break;
                    case 31:
                        if (Dice100.SuccessRoll(100))
                            targetItems.Add(ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Daedra_heart));
                        rolledStackSize = Random.Range(0, 3 + 1);
                        if (rolledStackSize > 0)
                        {
                            ingredients = ItemBuilder.CreateItem(ItemGroups.SolventIngredients, (int)SolventIngredients.Ichor);
                            ingredients.stackCount = rolledStackSize;
                            targetItems.Add(ingredients);
                        }
                        break;
                    default:
                        break;
                }
            }

            // Extra ingredients based on personality traits, if present. 
            if (traits[0] > -1 || traits[1] > -1 || traits[2] > -1)
            {
                AddExtraIngredBasedOnTraits(enemyLevel, traits, targetItems); // Items not yet implemented.
            }
        }

        public static void AddExtraIngredBasedOnTraits(int enemyLevel, int[] traits, List<DaggerfallUnityItem> targetItems)
        {
            DaggerfallUnityItem ingredients = null;
            int rolledStackSize = 0;

            if (traits[0] > -1 || traits[1] > -1)
            {
                if (traits[0] == (int)MobilePersonalityQuirks.Romantic || traits[1] == (int)MobilePersonalityQuirks.Romantic)
                {
                    rolledStackSize = 1;
                    for (int i = 0; i < rolledStackSize; i++)
                        targetItems.Add(ItemBuilder.CreateRandomIngredientOfGroup(ItemGroups.FlowerPlantIngredients, enemyLevel, -1, (int)FlowerPlantIngredients.Clover, (int)FlowerPlantIngredients.Alkanet_Flower, (int)FlowerPlantIngredients.Fennel_Flower));
                }
            }

            if (traits[2] > -1)
            {
                if (traits[2] == (int)MobilePersonalityInterests.Collector)
                {
                    rolledStackSize = FormulaHelper.PickOneOfCompact(1, 34, 2, 8, 3, 2);
                    for (int i = 0; i < rolledStackSize; i++)
                        targetItems.Add(ItemBuilder.CreateRandomIngredientOfGroup(ItemGroups.MetalIngredients, enemyLevel));
                }

                if (traits[2] == (int)MobilePersonalityInterests.Survivalist)
                {
                    rolledStackSize = FormulaHelper.PickOneOfCompact(1, 34, 2, 8, 3, 2);
                    for (int i = 0; i < rolledStackSize; i++)
                        targetItems.Add(ItemBuilder.CreateRandomIngredientOfGroup(ItemGroups.FruitPlantIngredients, enemyLevel, -1, (int)FruitPlantIngredients.Cactus));
                }

                if (traits[2] == (int)MobilePersonalityInterests.Hunter)
                {
                    rolledStackSize = FormulaHelper.PickOneOfCompact(1, 34, 2, 8, 3, 2);
                    for (int i = 0; i < rolledStackSize; i++)
                        targetItems.Add(ItemBuilder.CreateRandomIngredientOfGroup(ItemGroups.AnimalPartIngredients, enemyLevel, -1, (int)AnimalPartIngredients.Giant_scorpion_stinger, (int)AnimalPartIngredients.Pearl, (int)AnimalPartIngredients.Slaughterfish_Scales, (int)AnimalPartIngredients.Snake_venom, (int)AnimalPartIngredients.Spider_Eye, (int)AnimalPartIngredients.Spider_venom));
                }

                if (traits[2] == (int)MobilePersonalityInterests.Brewer)
                {
                    rolledStackSize = FormulaHelper.PickOneOfCompact(0, 1, 1, 2);
                    for (int i = 0; i < rolledStackSize; i++)
                        targetItems.Add(ItemBuilder.CreateRandomIngredientOfGroup(ItemGroups.MiscPlantIngredients, enemyLevel));
                    rolledStackSize = FormulaHelper.PickOneOfCompact(0, 1, 1, 1);
                    for (int i = 0; i < rolledStackSize; i++)
                        targetItems.Add(ItemBuilder.CreateRandomIngredientOfGroup(ItemGroups.FlowerPlantIngredients, enemyLevel));
                    rolledStackSize = FormulaHelper.PickOneOfCompact(0, 1, 1, 1);
                    for (int i = 0; i < rolledStackSize; i++)
                        targetItems.Add(ItemBuilder.CreateRandomIngredientOfGroup(ItemGroups.FruitPlantIngredients, enemyLevel));
                    rolledStackSize = FormulaHelper.PickOneOfCompact(1, 12, 2, 1);
                    for (int i = 0; i < rolledStackSize; i++)
                        targetItems.Add(ItemBuilder.CreateRandomIngredientOfGroup(ItemGroups.SolventIngredients, enemyLevel, -1, (int)SolventIngredients.Ichor, (int)SolventIngredients.Elixir_vitae));
                }

                if (traits[2] == (int)MobilePersonalityInterests.Anthophile)
                {
                    rolledStackSize = FormulaHelper.PickOneOfCompact(1, 34, 2, 8, 3, 2);
                    for (int i = 0; i < rolledStackSize; i++)
                        targetItems.Add(ItemBuilder.CreateRandomIngredientOfGroup(ItemGroups.FlowerPlantIngredients, enemyLevel));
                }

                if (traits[2] == (int)MobilePersonalityInterests.Fisher)
                {
                    rolledStackSize = FormulaHelper.PickOneOfCompact(1, 34, 2, 8, 3, 2);
                    if (rolledStackSize > 0)
                    {
                        ingredients = ItemBuilder.CreateItem(ItemGroups.AnimalPartIngredients, (int)AnimalPartIngredients.Slaughterfish_Scales);
                        ingredients.stackCount = rolledStackSize;
                        targetItems.Add(ingredients);
                    }
                }

                if (traits[2] == (int)MobilePersonalityInterests.Diver)
                {
                    rolledStackSize = FormulaHelper.PickOneOfCompact(1, 88, 2, 22, 3, 2);
                    if (rolledStackSize > 0)
                    {
                        ingredients = ItemBuilder.CreateItem(ItemGroups.AnimalPartIngredients, (int)AnimalPartIngredients.Pearl);
                        ingredients.stackCount = rolledStackSize;
                        targetItems.Add(ingredients);
                    }
                }
            }
        }

        static void AddBooksBasedOnSubject(EnemyEntity AITarget, int bookAmount, List<DaggerfallUnityItem> targetItems)
        {
            int level = AITarget.Level;

            if (AITarget.EntityType == EntityTypes.EnemyClass)
            {
                switch (AITarget.CareerIndex)
                {
                    case (int)ClassCareers.Mage:
                        for (int i = 0; i < bookAmount; i++)
                            targetItems.Add(ItemBuilder.CreateRandomBookOfSpecificSubject((ItemGroups)FormulaHelper.PickOneOfCompact(33, 9, 28, 1, 30, 3, 31, 2, 32, 1, 34, 1, 36, 2, 39, 1), level));
                        return;
                    case (int)ClassCareers.Spellsword:
                        for (int i = 0; i < bookAmount; i++)
                            targetItems.Add(ItemBuilder.CreateRandomBookOfSpecificSubject((ItemGroups)FormulaHelper.PickOneOfCompact(33, 6, 28, 2, 30, 5, 31, 2, 32, 1, 34, 1, 36, 2, 39, 1), level));
                        return;
                    case (int)ClassCareers.Battlemage:
                        for (int i = 0; i < bookAmount; i++)
                            targetItems.Add(ItemBuilder.CreateRandomBookOfSpecificSubject((ItemGroups)FormulaHelper.PickOneOfCompact(33, 4, 28, 3, 30, 6, 31, 2, 32, 1, 34, 1, 36, 2, 39, 1), level));
                        return;
                    case (int)ClassCareers.Sorcerer:
                        for (int i = 0; i < bookAmount; i++)
                            targetItems.Add(ItemBuilder.CreateRandomBookOfSpecificSubject((ItemGroups)FormulaHelper.PickOneOfCompact(33, 3, 28, 2, 30, 7, 31, 3, 32, 1, 34, 1, 36, 2, 39, 1), level));
                        return;
                    case (int)ClassCareers.Healer:
                    case (int)ClassCareers.Monk:
                        for (int i = 0; i < bookAmount; i++)
                            targetItems.Add(ItemBuilder.CreateRandomBookOfSpecificSubject((ItemGroups)FormulaHelper.PickOneOfCompact(38, 8, 33, 1, 28, 1, 30, 2, 31, 3, 32, 1, 34, 1, 36, 1, 39, 2), level));
                        return;
                    case (int)ClassCareers.Nightblade:
                        for (int i = 0; i < bookAmount; i++)
                            targetItems.Add(ItemBuilder.CreateRandomBookOfSpecificSubject((ItemGroups)FormulaHelper.PickOneOfCompact(28, 1, 29, 1, 30, 6, 31, 2, 33, 2, 34, 1, 35, 1, 36, 2, 37, 1, 38, 2, 39, 1), level));
                        return;
                    case (int)ClassCareers.Bard:
                        for (int i = 0; i < bookAmount; i++)
                            targetItems.Add(ItemBuilder.CreateRandomBookOfSpecificSubject((ItemGroups)FormulaHelper.PickOneOfCompact(28, 2, 29, 1, 30, 3, 31, 3, 32, 2, 34, 1, 35, 1, 36, 4, 38, 2, 39, 1), level));
                        return;
                    case (int)ClassCareers.Assassin:
                        for (int i = 0; i < bookAmount; i++)
                            targetItems.Add(ItemBuilder.CreateRandomBookOfSpecificSubject((ItemGroups)FormulaHelper.PickOneOfCompact(28, 2, 29, 3, 30, 2, 31, 4, 33, 2, 34, 1, 35, 1, 36, 1, 37, 2, 38, 1, 39, 1), level));
                        return;
                    case (int)ClassCareers.Ranger:
                        for (int i = 0; i < bookAmount; i++)
                            targetItems.Add(ItemBuilder.CreateRandomBookOfSpecificSubject((ItemGroups)FormulaHelper.PickOneOfCompact(28, 2, 29, 1, 30, 2, 31, 3, 32, 1, 33, 4, 34, 1, 35, 1, 36, 1, 38, 1, 39, 3), level));
                        return;
                    case (int)ClassCareers.Knight:
                        for (int i = 0; i < bookAmount; i++)
                            targetItems.Add(ItemBuilder.CreateRandomBookOfSpecificSubject((ItemGroups)FormulaHelper.PickOneOfCompact(28, 3, 30, 3, 31, 3, 36, 4, 37, 2, 38, 4, 39, 1), level));
                        return;
                    default:
                        for (int i = 0; i < bookAmount; i++)
                            targetItems.Add(ItemBuilder.CreateRandomBookOfRandomSubject(level));
                        return;
                }
            }
            else
            {
                switch (AITarget.CareerIndex)
                {
                    case 21:
                        for (int i = 0; i < bookAmount; i++)
                            targetItems.Add(ItemBuilder.CreateRandomBookOfSpecificSubject((ItemGroups)FormulaHelper.PickOneOfCompact(28, 1, 30, 4, 31, 2, 33, 2, 36, 1, 38, 9, 39, 1), level));
                        return;
                    case 32:
                    case 33:
                        for (int i = 0; i < bookAmount; i++)
                            targetItems.Add(ItemBuilder.CreateRandomBookOfSpecificSubject((ItemGroups)FormulaHelper.PickOneOfCompact(28, 1, 30, 2, 31, 1, 33, 10, 34, 1, 35, 2, 36, 1, 38, 1, 39, 1), level));
                        return;
                    default:
                        for (int i = 0; i < bookAmount; i++)
                            targetItems.Add(ItemBuilder.CreateRandomBookOfRandomSubject(level));
                        return;
                }
            }
        }

        static void AddClothesBasedOnEnemy(Genders playerGender, Races playerRace, EnemyEntity AITarget, int[] condMods, List<DaggerfallUnityItem> targetItems)
        {
            Genders enemyGender = AITarget.Gender;

            if (AITarget.EntityType == EntityTypes.EnemyClass)
            {
                switch (AITarget.CareerIndex)
                {
                    case (int)ClassCareers.Mage:
                        if (enemyGender == Genders.Male)
                        {
                            targetItems.Add(ItemBuilder.CreateMensClothing(MensClothing.Plain_robes, playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                            targetItems.Add(ItemBuilder.CreateRandomShoes(enemyGender, playerRace, condMods[0], condMods[1]));
                            if (Dice100.SuccessRoll(50))
                                targetItems.Add(ItemBuilder.CreateRandomPants(enemyGender, playerRace, condMods[0], condMods[1]));
                            if (Dice100.SuccessRoll(50))
                                targetItems.Add(ItemBuilder.CreateMensClothing(MensClothing.Casual_cloak, playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        }
                        else
                        {
                            targetItems.Add(ItemBuilder.CreateWomensClothing(WomensClothing.Plain_robes, playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                            targetItems.Add(ItemBuilder.CreateRandomShoes(enemyGender, playerRace, condMods[0], condMods[1]));
                            if (Dice100.SuccessRoll(50))
                                targetItems.Add(ItemBuilder.CreateRandomPants(enemyGender, playerRace, condMods[0], condMods[1]));
                            if (Dice100.SuccessRoll(50))
                                targetItems.Add(ItemBuilder.CreateWomensClothing(WomensClothing.Casual_cloak, playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        }
                        return;
                    case (int)ClassCareers.Spellsword:
                    case (int)ClassCareers.Battlemage:
                    case (int)ClassCareers.Sorcerer:
                    case (int)ClassCareers.Bard:
                    case (int)ClassCareers.Burglar:
                    case (int)ClassCareers.Rogue:
                    case (int)ClassCareers.Thief:
                    case (int)ClassCareers.Archer:
                    case (int)ClassCareers.Warrior:
                    case (int)ClassCareers.Knight:
                        if (enemyGender == Genders.Male)
                        {
                            targetItems.Add(ItemBuilder.CreateRandomShirt(enemyGender, playerRace, condMods[0], condMods[1]));
                            targetItems.Add(ItemBuilder.CreateRandomPants(enemyGender, playerRace, condMods[0], condMods[1]));
                            targetItems.Add(ItemBuilder.CreateRandomShoes(enemyGender, playerRace, condMods[0], condMods[1]));
                            if (Dice100.SuccessRoll(50))
                                targetItems.Add(ItemBuilder.CreateMensClothing(MensClothing.Casual_cloak, playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        }
                        else
                        {
                            targetItems.Add(ItemBuilder.CreateRandomShirt(enemyGender, playerRace, condMods[0], condMods[1]));
                            targetItems.Add(ItemBuilder.CreateRandomPants(enemyGender, playerRace, condMods[0], condMods[1]));
                            targetItems.Add(ItemBuilder.CreateRandomShoes(enemyGender, playerRace, condMods[0], condMods[1]));
                            if (Dice100.SuccessRoll(50))
                                targetItems.Add(ItemBuilder.CreateRandomBra(enemyGender, playerRace, condMods[0], condMods[1]));
                            if (Dice100.SuccessRoll(50))
                                targetItems.Add(ItemBuilder.CreateWomensClothing(WomensClothing.Casual_cloak, playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        }
                        return;
                    case (int)ClassCareers.Healer:
                        if (enemyGender == Genders.Male)
                        {
                            targetItems.Add(ItemBuilder.CreateMensClothing(MensClothing.Priest_robes, playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                            targetItems.Add(ItemBuilder.CreateRandomShoes(enemyGender, playerRace, condMods[0], condMods[1]));
                            if (Dice100.SuccessRoll(50))
                                targetItems.Add(ItemBuilder.CreateRandomPants(enemyGender, playerRace, condMods[0], condMods[1]));
                            if (Dice100.SuccessRoll(50))
                                targetItems.Add(ItemBuilder.CreateMensClothing((MensClothing)FormulaHelper.PickOneOfCompact((int)MensClothing.Casual_cloak, 2, (int)MensClothing.Formal_cloak, 1), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        }
                        else
                        {
                            targetItems.Add(ItemBuilder.CreateWomensClothing(WomensClothing.Priestess_robes, playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                            targetItems.Add(ItemBuilder.CreateRandomShoes(enemyGender, playerRace, condMods[0], condMods[1]));
                            if (Dice100.SuccessRoll(50))
                                targetItems.Add(ItemBuilder.CreateRandomPants(enemyGender, playerRace, condMods[0], condMods[1]));
                            if (Dice100.SuccessRoll(50))
                                targetItems.Add(ItemBuilder.CreateWomensClothing((WomensClothing)FormulaHelper.PickOneOfCompact((int)WomensClothing.Casual_cloak, 2, (int)WomensClothing.Formal_cloak, 1), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        }
                        return;
                    case (int)ClassCareers.Nightblade:
                    case (int)ClassCareers.Assassin:
                    case (int)ClassCareers.Ranger:
                        if (enemyGender == Genders.Male)
                        {
                            targetItems.Add(ItemBuilder.CreateRandomShirt(enemyGender, playerRace, condMods[0], condMods[1]));
                            targetItems.Add(ItemBuilder.CreateRandomPants(enemyGender, playerRace, condMods[0], condMods[1]));
                            targetItems.Add(ItemBuilder.CreateRandomShoes(enemyGender, playerRace, condMods[0], condMods[1]));
                            targetItems.Add(ItemBuilder.CreateMensClothing(MensClothing.Casual_cloak, playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        }
                        else
                        {
                            targetItems.Add(ItemBuilder.CreateRandomShirt(enemyGender, playerRace, condMods[0], condMods[1]));
                            targetItems.Add(ItemBuilder.CreateRandomPants(enemyGender, playerRace, condMods[0], condMods[1]));
                            targetItems.Add(ItemBuilder.CreateRandomShoes(enemyGender, playerRace, condMods[0], condMods[1]));
                            targetItems.Add(ItemBuilder.CreateWomensClothing(WomensClothing.Casual_cloak, playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                            if (Dice100.SuccessRoll(50))
                                targetItems.Add(ItemBuilder.CreateRandomBra(enemyGender, playerRace, condMods[0], condMods[1]));
                        }
                        return;
                    case (int)ClassCareers.Acrobat:
                        if (enemyGender == Genders.Male)
                        {
                            targetItems.Add(ItemBuilder.CreateMensClothing(MensClothing.Khajiit_suit, playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                            targetItems.Add(ItemBuilder.CreateMensClothing((MensClothing)FormulaHelper.PickOneOf((int)MensClothing.Shoes, (int)MensClothing.Sandals), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                            if (Dice100.SuccessRoll(50))
                                targetItems.Add(ItemBuilder.CreateMensClothing(MensClothing.Casual_cloak, playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        }
                        else
                        {
                            targetItems.Add(ItemBuilder.CreateWomensClothing(WomensClothing.Khajiit_suit, playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                            targetItems.Add(ItemBuilder.CreateWomensClothing((WomensClothing)FormulaHelper.PickOneOf((int)WomensClothing.Shoes, (int)WomensClothing.Sandals), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                            if (Dice100.SuccessRoll(50))
                                targetItems.Add(ItemBuilder.CreateWomensClothing(WomensClothing.Casual_cloak, playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        }
                        return;
                    case (int)ClassCareers.Monk:
                        if (enemyGender == Genders.Male)
                        {
                            targetItems.Add(ItemBuilder.CreateRandomPants(enemyGender, playerRace, condMods[0], condMods[1]));
                            if (Dice100.SuccessRoll(50))
                                targetItems.Add(ItemBuilder.CreateMensClothing((MensClothing)FormulaHelper.PickOneOf((int)MensClothing.Sash, (int)MensClothing.Toga, (int)MensClothing.Kimono, (int)MensClothing.Armbands, (int)MensClothing.Vest), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                            if (Dice100.SuccessRoll(50))
                                targetItems.Add(ItemBuilder.CreateMensClothing((MensClothing)FormulaHelper.PickOneOf((int)MensClothing.Shoes, (int)MensClothing.Sandals), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                            if (Dice100.SuccessRoll(25))
                                targetItems.Add(ItemBuilder.CreateMensClothing(MensClothing.Casual_cloak, playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        }
                        else
                        {
                            targetItems.Add(ItemBuilder.CreateRandomPants(enemyGender, playerRace, condMods[0], condMods[1]));
                            targetItems.Add(ItemBuilder.CreateRandomBra(enemyGender, playerRace, condMods[0], condMods[1]));
                            if (Dice100.SuccessRoll(50))
                                targetItems.Add(ItemBuilder.CreateWomensClothing((WomensClothing)FormulaHelper.PickOneOf((int)WomensClothing.Shoes, (int)WomensClothing.Sandals), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                            if (Dice100.SuccessRoll(25))
                                targetItems.Add(ItemBuilder.CreateWomensClothing(WomensClothing.Casual_cloak, playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        }
                        return;
                    case (int)ClassCareers.Barbarian:
                        if (enemyGender == Genders.Male)
                        {
                            targetItems.Add(ItemBuilder.CreateRandomShoes(enemyGender, playerRace, condMods[0], condMods[1]));
                            targetItems.Add(ItemBuilder.CreateMensClothing((MensClothing)FormulaHelper.PickOneOf((int)MensClothing.Short_skirt, (int)MensClothing.Long_Skirt, (int)MensClothing.Loincloth, (int)MensClothing.Wrap), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                            if (Dice100.SuccessRoll(50))
                                targetItems.Add(ItemBuilder.CreateMensClothing((MensClothing)FormulaHelper.PickOneOf((int)MensClothing.Sash, (int)MensClothing.Armbands, (int)MensClothing.Fancy_Armbands, (int)MensClothing.Straps, (int)MensClothing.Challenger_Straps, (int)MensClothing.Champion_straps), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                            if (Dice100.SuccessRoll(50))
                                targetItems.Add(ItemBuilder.CreateMensClothing(MensClothing.Casual_cloak, playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        }
                        else
                        {
                            targetItems.Add(ItemBuilder.CreateRandomBra(enemyGender, playerRace, condMods[0], condMods[1]));
                            targetItems.Add(ItemBuilder.CreateRandomShoes(enemyGender, playerRace, condMods[0], condMods[1]));
                            targetItems.Add(ItemBuilder.CreateWomensClothing((WomensClothing)FormulaHelper.PickOneOf((int)WomensClothing.Loincloth, (int)WomensClothing.Wrap, (int)WomensClothing.Long_skirt), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                            if (Dice100.SuccessRoll(50))
                                targetItems.Add(ItemBuilder.CreateWomensClothing(WomensClothing.Casual_cloak, playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        }
                        return;
                    default:
                        if (enemyGender == Genders.Male)
                        {
                            targetItems.Add(ItemBuilder.CreateRandomShirt(enemyGender, playerRace, condMods[0], condMods[1]));
                            targetItems.Add(ItemBuilder.CreateRandomPants(enemyGender, playerRace, condMods[0], condMods[1]));
                            targetItems.Add(ItemBuilder.CreateRandomShoes(enemyGender, playerRace, condMods[0], condMods[1]));
                        }
                        else
                        {
                            targetItems.Add(ItemBuilder.CreateRandomShirt(enemyGender, playerRace, condMods[0], condMods[1]));
                            targetItems.Add(ItemBuilder.CreateRandomPants(enemyGender, playerRace, condMods[0], condMods[1]));
                            targetItems.Add(ItemBuilder.CreateRandomShoes(enemyGender, playerRace, condMods[0], condMods[1]));
                            if (Dice100.SuccessRoll(50))
                                targetItems.Add(ItemBuilder.CreateRandomBra(enemyGender, playerRace, condMods[0], condMods[1]));
                        }
                        return;
                }
            }
            else
            {
                switch (AITarget.CareerIndex)
                {
                    case 7:
                    case 12:
                    case 21:
                        targetItems.Add(ItemBuilder.CreateRandomShoes(Genders.Male, playerRace, condMods[0], condMods[1]));
                        targetItems.Add(ItemBuilder.CreateMensClothing((MensClothing)FormulaHelper.PickOneOf((int)MensClothing.Short_skirt, (int)MensClothing.Long_Skirt, (int)MensClothing.Loincloth, (int)MensClothing.Wrap), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        if (Dice100.SuccessRoll(50))
                            targetItems.Add(ItemBuilder.CreateMensClothing((MensClothing)FormulaHelper.PickOneOf((int)MensClothing.Straps, (int)MensClothing.Challenger_Straps), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        if (Dice100.SuccessRoll(50))
                            targetItems.Add(ItemBuilder.CreateMensClothing(MensClothing.Casual_cloak, playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        return;
                    case 24:
                        targetItems.Add(ItemBuilder.CreateRandomShoes(Genders.Male, playerRace, condMods[0], condMods[1]));
                        targetItems.Add(ItemBuilder.CreateMensClothing((MensClothing)FormulaHelper.PickOneOf((int)MensClothing.Short_skirt, (int)MensClothing.Long_Skirt, (int)MensClothing.Loincloth, (int)MensClothing.Wrap), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        if (Dice100.SuccessRoll(50))
                            targetItems.Add(ItemBuilder.CreateMensClothing((MensClothing)FormulaHelper.PickOneOf((int)MensClothing.Challenger_Straps, (int)MensClothing.Champion_straps), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        if (Dice100.SuccessRoll(50))
                            targetItems.Add(ItemBuilder.CreateMensClothing(MensClothing.Formal_cloak, playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        return;
                    case 15:
                    case 17:
                        if (playerGender == Genders.Male)
                        {
                            if (Dice100.SuccessRoll(20))
                                targetItems.Add(ItemBuilder.CreateRandomShirt(enemyGender, playerRace, condMods[0], condMods[1]));
                            if (Dice100.SuccessRoll(20))
                                targetItems.Add(ItemBuilder.CreateRandomPants(enemyGender, playerRace, condMods[0], condMods[1]));
                            if (Dice100.SuccessRoll(20))
                                targetItems.Add(ItemBuilder.CreateRandomShoes(enemyGender, playerRace, condMods[0], condMods[1]));
                        }
                        else
                        {
                            if (Dice100.SuccessRoll(20))
                                targetItems.Add(ItemBuilder.CreateRandomShirt(enemyGender, playerRace, condMods[0], condMods[1]));
                            if (Dice100.SuccessRoll(20))
                                targetItems.Add(ItemBuilder.CreateRandomPants(enemyGender, playerRace, condMods[0], condMods[1]));
                            if (Dice100.SuccessRoll(20))
                                targetItems.Add(ItemBuilder.CreateRandomShoes(enemyGender, playerRace, condMods[0], condMods[1]));
                            if (Dice100.SuccessRoll(10))
                                targetItems.Add(ItemBuilder.CreateRandomBra(enemyGender, playerRace, condMods[0], condMods[1]));
                        }
                        return;
                    case 28:
                        targetItems.Add(ItemBuilder.CreateRandomShirt(Genders.Female, playerRace, condMods[0], condMods[1]));
                        targetItems.Add(ItemBuilder.CreateRandomPants(Genders.Female, playerRace, condMods[0], condMods[1]));
                        targetItems.Add(ItemBuilder.CreateRandomShoes(Genders.Female, playerRace, condMods[0], condMods[1]));
                        targetItems.Add(ItemBuilder.CreateWomensClothing(WomensClothing.Casual_cloak, playerRace, -1, condMods[0], condMods[1], DyeColors.Grey));
                        if (Dice100.SuccessRoll(50))
                            targetItems.Add(ItemBuilder.CreateRandomBra(Genders.Female, playerRace, condMods[0], condMods[1]));
                        return;
                    case 30:
                        targetItems.Add(ItemBuilder.CreateRandomShirt(Genders.Male, playerRace, condMods[0], condMods[1]));
                        targetItems.Add(ItemBuilder.CreateRandomPants(Genders.Male, playerRace, condMods[0], condMods[1]));
                        targetItems.Add(ItemBuilder.CreateRandomShoes(Genders.Male, playerRace, condMods[0], condMods[1]));
                        targetItems.Add(ItemBuilder.CreateMensClothing(MensClothing.Casual_cloak, playerRace, -1, condMods[0], condMods[1], DyeColors.Grey));
                        return;
                    case 32:
                    case 33:
                        if (playerGender == Genders.Male)
                        {
                            targetItems.Add(ItemBuilder.CreateMensClothing(MensClothing.Casual_cloak, playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                            if (Dice100.SuccessRoll(50))
                                targetItems.Add(ItemBuilder.CreateMensClothing((MensClothing)FormulaHelper.PickOneOf((int)MensClothing.Boots, (int)MensClothing.Tall_Boots), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        }
                        else
                        {
                            targetItems.Add(ItemBuilder.CreateWomensClothing(WomensClothing.Casual_cloak, playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                            if (Dice100.SuccessRoll(50))
                                targetItems.Add(ItemBuilder.CreateWomensClothing((WomensClothing)FormulaHelper.PickOneOf((int)WomensClothing.Boots, (int)WomensClothing.Tall_boots), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        }
                        return;
                    case 27:
                        if (playerGender == Genders.Male)
                        {
                            targetItems.Add(ItemBuilder.CreateMensClothing(MensClothing.Loincloth, playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                            targetItems.Add(ItemBuilder.CreateMensClothing((MensClothing)FormulaHelper.PickOneOf((int)MensClothing.Boots, (int)MensClothing.Tall_Boots), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        }
                        else
                        {
                            targetItems.Add(ItemBuilder.CreateWomensClothing(WomensClothing.Loincloth, playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                            targetItems.Add(ItemBuilder.CreateWomensClothing((WomensClothing)FormulaHelper.PickOneOf((int)WomensClothing.Boots, (int)WomensClothing.Tall_boots), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        }
                        return;
                    case 29:
                        if (Dice100.SuccessRoll(50))
                            targetItems.Add(ItemBuilder.CreateWomensClothing((WomensClothing)FormulaHelper.PickOneOf((int)WomensClothing.Eodoric, (int)WomensClothing.Formal_eodoric, (int)WomensClothing.Strapless_dress), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        if (Dice100.SuccessRoll(20))
                            targetItems.Add(ItemBuilder.CreateRandomBra(Genders.Female, playerRace, condMods[0], condMods[1]));
                        return;
                    case 31:
                        targetItems.Add(ItemBuilder.CreateRandomPants(Genders.Male, playerRace, condMods[0], condMods[1]));
                        targetItems.Add(ItemBuilder.CreateMensClothing((MensClothing)FormulaHelper.PickOneOf((int)MensClothing.Boots, (int)MensClothing.Tall_Boots), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        targetItems.Add(ItemBuilder.CreateMensClothing((MensClothing)FormulaHelper.PickOneOf((int)MensClothing.Casual_cloak, (int)MensClothing.Formal_cloak), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        if (Dice100.SuccessRoll(50))
                            targetItems.Add(ItemBuilder.CreateRandomShirt(Genders.Male, playerRace, condMods[0], condMods[1]));
                        return;
                    default:
                        return;
                }
            }
        }

        public static float IngotDropChance(EnemyEntity AITarget)
        {
            int level = AITarget.Level;

            if (AITarget.EntityType == EntityTypes.EnemyClass)
            {
                switch (AITarget.CareerIndex)
                {
                    case (int)ClassCareers.Spellsword:
                    case (int)ClassCareers.Battlemage:
                        return 7.5f + (0.5f * level);
                    case (int)ClassCareers.Burglar:
                    case (int)ClassCareers.Rogue:
                    case (int)ClassCareers.Thief:
                        return 15f + (1.5f * level);
                    case (int)ClassCareers.Archer:
                    case (int)ClassCareers.Barbarian:
                    case (int)ClassCareers.Warrior:
                    case (int)ClassCareers.Knight:
                        return 10f + (1f * level);
                    default:
                        return 0f;
                }
            }
            else
            {
                switch (AITarget.CareerIndex)
                {
                    case 8:
                    case 16:
                        return 2f;
                    case 7:
                        return 5f;
                    case 12:
                        return 10f;
                    case 21:
                        return 3f;
                    case 24:
                        return 20f;
                    case 31:
                        return 40f;
                    default:
                        return 0f;
                }
            }
        }

        public static byte[,] GetAllowedPotionTypes(EnemyEntity AITarget)
        {
            // Index meanings: Row 1 = Potion Types: 0 = healHP, 1 = healStam, 2 = healMana, 3 = cure, 4 = fortify, 5 = utilDef, 6 = utilExplore.
            // Row 2 = Potion Type Rarity: 0 = 1 - 20, 20 = least rare, 1 = most rare.
            byte[,] allowedPotions = new byte[,] { { 1, 20 }, { 1, 20 }, { 1, 20 }, { 1, 20 }, { 1, 20 }, { 1, 20 }, { 1, 20 } };

            if (AITarget.EntityType == EntityTypes.EnemyClass)
            {
                switch (AITarget.CareerIndex)
                {
                    case (int)ClassCareers.Mage:
                        return new byte[,] { { 1, 15 }, { 1, 5 }, { 1, 20 }, { 0, 20 }, { 1, 2 }, { 1, 8 }, { 1, 5 } };
                    case (int)ClassCareers.Spellsword:
                    case (int)ClassCareers.Battlemage:
                        return new byte[,] { { 1, 20 }, { 1, 15 }, { 1, 12 }, { 0, 20 }, { 1, 6 }, { 1, 6 }, { 1, 7 } };
                    case (int)ClassCareers.Sorcerer:
                        return new byte[,] { { 1, 10 }, { 1, 10 }, { 1, 20 }, { 0, 20 }, { 1, 4 }, { 1, 2 }, { 1, 5 } };
                    case (int)ClassCareers.Healer:
                        return new byte[,] { { 1, 20 }, { 1, 5 }, { 1, 10 }, { 1, 20 }, { 0, 20 }, { 1, 7 }, { 1, 2 } };
                    case (int)ClassCareers.Nightblade:
                        return new byte[,] { { 1, 15 }, { 1, 15 }, { 1, 10 }, { 1, 10 }, { 1, 5 }, { 1, 10 }, { 1, 7 } };
                    case (int)ClassCareers.Bard:
                        return new byte[,] { { 1, 10 }, { 1, 15 }, { 1, 5 }, { 0, 20 }, { 1, 20 }, { 1, 5 }, { 1, 14 } };
                    case (int)ClassCareers.Burglar:
                    case (int)ClassCareers.Acrobat:
                    case (int)ClassCareers.Thief:
                        return new byte[,] { { 1, 10 }, { 1, 20 }, { 0, 20 }, { 0, 20 }, { 1, 8 }, { 0, 20 }, { 1, 20 } };
                    case (int)ClassCareers.Rogue:
                        return new byte[,] { { 1, 20 }, { 1, 15 }, { 0, 20 }, { 0, 20 }, { 1, 6 }, { 0, 20 }, { 1, 10 } };
                    case (int)ClassCareers.Assassin:
                        return new byte[,] { { 1, 20 }, { 1, 10 }, { 0, 20 }, { 1, 8 }, { 1, 12 }, { 1, 12 }, { 1, 15 } };
                    case (int)ClassCareers.Monk:
                        return new byte[,] { { 1, 15 }, { 1, 10 }, { 0, 20 }, { 0, 20 }, { 0, 20 }, { 1, 8 }, { 1, 12 } };
                    case (int)ClassCareers.Archer:
                    case (int)ClassCareers.Ranger:
                        return new byte[,] { { 1, 20 }, { 1, 20 }, { 0, 20 }, { 1, 6 }, { 1, 8 }, { 1, 9 }, { 1, 9 } };
                    case (int)ClassCareers.Barbarian:
                        return new byte[,] { { 1, 20 }, { 1, 20 }, { 0, 20 }, { 0, 20 }, { 1, 12 }, { 0, 20 }, { 0, 20 } };
                    case (int)ClassCareers.Warrior:
                    case (int)ClassCareers.Knight:
                        return new byte[,] { { 1, 20 }, { 1, 20 }, { 0, 20 }, { 1, 5 }, { 1, 8 }, { 1, 10 }, { 1, 11 } };
                    default:
                        return allowedPotions;
                }
            }
            else
            {
                switch (AITarget.CareerIndex)
                {
                    case 8:
                        return new byte[,] { { 1, 20 }, { 1, 20 }, { 0, 20 }, { 0, 20 }, { 0, 20 }, { 1, 5 }, { 0, 20 } };
                    case 7:
                    case 12:
                    case 24:
                        return new byte[,] { { 1, 20 }, { 1, 20 }, { 0, 20 }, { 0, 20 }, { 0, 20 }, { 1, 5 }, { 0, 20 } };
                    case 21:
                        return new byte[,] { { 1, 15 }, { 1, 5 }, { 1, 20 }, { 1, 10 }, { 0, 20 }, { 1, 10 }, { 0, 20 } };
                    case 28:
                    case 30:
                        return new byte[,] { { 1, 10 }, { 0, 20 }, { 1, 10 }, { 0, 20 }, { 1, 5 }, { 1, 10 }, { 1, 10 } };
                    default:
                        return allowedPotions;
                }
            }
        }

        public static int[] EnemyLootConditionCalculator(DaggerfallEntity enemy, int[] traits)
        {
            // Index meanings: 0 = minCond%, 1 = maxCond%.
            int[] equipTableProps = { -1, -1 };

            EnemyEntity AITarget = enemy as EnemyEntity;

            if (EnemyEntity.EquipmentUser(AITarget))
            {
                if (AITarget.EntityType == EntityTypes.EnemyClass)
                {
                    switch (AITarget.CareerIndex)
                    {
                        case (int)ClassCareers.Mage:
                            equipTableProps[0] = 20;
                            equipTableProps[1] = 60;
                            break;
                        case (int)ClassCareers.Spellsword:
                            equipTableProps[0] = 30;
                            equipTableProps[1] = 70;
                            break;
                        case (int)ClassCareers.Battlemage:
                            equipTableProps[0] = 30;
                            equipTableProps[1] = 70;
                            break;
                        case (int)ClassCareers.Sorcerer:
                            equipTableProps[0] = 20;
                            equipTableProps[1] = 60;
                            break;
                        case (int)ClassCareers.Healer:
                            equipTableProps[0] = 25;
                            equipTableProps[1] = 65;
                            break;
                        case (int)ClassCareers.Nightblade:
                            equipTableProps[0] = 40;
                            equipTableProps[1] = 80;
                            break;
                        case (int)ClassCareers.Bard:
                            equipTableProps[0] = 30;
                            equipTableProps[1] = 70;
                            break;
                        case (int)ClassCareers.Burglar:
                            equipTableProps[0] = 20;
                            equipTableProps[1] = 60;
                            break;
                        case (int)ClassCareers.Rogue:
                            equipTableProps[0] = 20;
                            equipTableProps[1] = 60;
                            break;
                        case (int)ClassCareers.Acrobat:
                            equipTableProps[0] = 25;
                            equipTableProps[1] = 65;
                            break;
                        case (int)ClassCareers.Thief:
                            equipTableProps[0] = 20;
                            equipTableProps[1] = 60;
                            break;
                        case (int)ClassCareers.Assassin:
                            equipTableProps[0] = 40;
                            equipTableProps[1] = 80;
                            break;
                        case (int)ClassCareers.Monk:
                            equipTableProps[0] = 25;
                            equipTableProps[1] = 65;
                            break;
                        case (int)ClassCareers.Archer:
                            equipTableProps[0] = 35;
                            equipTableProps[1] = 75;
                            break;
                        case (int)ClassCareers.Ranger:
                            equipTableProps[0] = 30;
                            equipTableProps[1] = 70;
                            break;
                        case (int)ClassCareers.Barbarian:
                            equipTableProps[0] = 20;
                            equipTableProps[1] = 60;
                            break;
                        case (int)ClassCareers.Warrior:
                            equipTableProps[0] = 40;
                            equipTableProps[1] = 80;
                            break;
                        case (int)ClassCareers.Knight:
                            equipTableProps[0] = 45;
                            equipTableProps[1] = 85;
                            break;
                        default:
                            return equipTableProps;
                    }
                }
                else
                {
                    switch (AITarget.CareerIndex)
                    {
                        case 7:
                            equipTableProps[0] = 30;
                            equipTableProps[1] = 70;
                            break;
                        case 8:
                            equipTableProps[0] = 15;
                            equipTableProps[1] = 55;
                            break;
                        case 12:
                            equipTableProps[0] = 35;
                            equipTableProps[1] = 75;
                            break;
                        case 15:
                            equipTableProps[0] = 5;
                            equipTableProps[1] = 45;
                            break;
                        case 17:
                            equipTableProps[0] = 5;
                            equipTableProps[1] = 45;
                            break;
                        case 21:
                            equipTableProps[0] = 20;
                            equipTableProps[1] = 60;
                            break;
                        case 23:
                            equipTableProps[0] = 10;
                            equipTableProps[1] = 50;
                            break;
                        case 24:
                            equipTableProps[0] = 40;
                            equipTableProps[1] = 80;
                            break;
                        case 25:
                            equipTableProps[0] = 15;
                            equipTableProps[1] = 55;
                            break;
                        case 26:
                            equipTableProps[0] = 15;
                            equipTableProps[1] = 55;
                            break;
                        case 27:
                            equipTableProps[0] = 15;
                            equipTableProps[1] = 55;
                            break;
                        case 29:
                            equipTableProps[0] = 25;
                            equipTableProps[1] = 65;
                            break;
                        case 31:
                            equipTableProps[0] = 35;
                            equipTableProps[1] = 75;
                            break;
                        case 28:
                        case 30:
                            equipTableProps[0] = 25;
                            equipTableProps[1] = 65;
                            break;
                        case 32:
                        case 33:
                            equipTableProps[0] = 20;
                            equipTableProps[1] = 60;
                            break;
                        default:
                            return equipTableProps;
                    }
                }
            }
            else
            {
                return equipTableProps;
            }

            equipTableProps = TraitLootConditionCalculator(enemy, traits, equipTableProps);

            return equipTableProps;
        }

        public static int[] TraitLootConditionCalculator(DaggerfallEntity enemy, int[] traits, int[] equipTableProps)
        {
            if (traits[0] == (int)MobilePersonalityQuirks.Prepared || traits[1] == (int)MobilePersonalityQuirks.Prepared)
            {
                equipTableProps[0] = (int)Mathf.Clamp(equipTableProps[0] + 15, 1, 95);
                equipTableProps[1] = (int)Mathf.Clamp(equipTableProps[1] + 15, 1, 95);
            }

            if (traits[0] == (int)MobilePersonalityQuirks.Reckless || traits[1] == (int)MobilePersonalityQuirks.Reckless)
            {
                equipTableProps[0] = (int)Mathf.Clamp(equipTableProps[0] - 15, 1, 95);
                equipTableProps[1] = (int)Mathf.Clamp(equipTableProps[1] - 15, 1, 95);
            }

            if (traits[0] == (int)MobilePersonalityQuirks.Cautious || traits[1] == (int)MobilePersonalityQuirks.Cautious)
            {
                equipTableProps[0] = (int)Mathf.Clamp(equipTableProps[0] + 5, 1, 95);
                equipTableProps[1] = (int)Mathf.Clamp(equipTableProps[1] + 5, 1, 95);
            }

            if (traits[0] == (int)MobilePersonalityQuirks.Hoarder || traits[1] == (int)MobilePersonalityQuirks.Hoarder)
            {
                equipTableProps[0] = (int)Mathf.Clamp(equipTableProps[0] - 10, 1, 95);
                equipTableProps[1] = (int)Mathf.Clamp(equipTableProps[1] - 10, 1, 95);
            }

            if (traits[2] == (int)MobilePersonalityInterests.Collector)
            {
                equipTableProps[0] = (int)Mathf.Clamp(equipTableProps[0] + 5, 1, 95);
                equipTableProps[1] = (int)Mathf.Clamp(equipTableProps[1] + 5, 1, 95);
            }

            if (traits[2] == (int)MobilePersonalityInterests.Survivalist)
            {
                equipTableProps[0] = (int)Mathf.Clamp(equipTableProps[0] + 10, 1, 95);
                equipTableProps[1] = (int)Mathf.Clamp(equipTableProps[1] + 10, 1, 95);
            }

            if (traits[2] == (int)MobilePersonalityInterests.Diver)
            {
                equipTableProps[0] = (int)Mathf.Clamp(equipTableProps[0] - 5, 1, 95);
                equipTableProps[1] = (int)Mathf.Clamp(equipTableProps[1] - 5, 1, 95);
            }

            if (traits[2] == (int)MobilePersonalityInterests.Handy)
            {
                equipTableProps[0] = (int)Mathf.Clamp(equipTableProps[0] + 15, 1, 95);
                equipTableProps[1] = (int)Mathf.Clamp(equipTableProps[1] + 15, 1, 95);
            }

            return equipTableProps;
        }

        static void RandomIngredient(float chance, ItemGroups ingredientGroup, List<DaggerfallUnityItem> targetItems)
        {
            while (Dice100.SuccessRoll((int)chance))
            {
                targetItems.Add(ItemBuilder.CreateRandomIngredientOfGroup(ingredientGroup));
                chance *= 0.5f;
            }
        }

        #endregion
    }
}
