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

        public static bool GenerateLoot(DaggerfallLoot loot, int locationIndex)
        {
            string[] lootTableKeys = {
            "K", // Crypt
            "N", // Orc Stronghold
            "N", // Human Stronghold
            "N", // Prison
            "K", // Desecrated Temple
            "M", // Mine
            "M", // Natural Cave
            "Q", // Coven
            "K", // Vampire Haunt
            "U", // Laboratory
            "D", // Harpy Nest
            "N", // Ruined Castle
            "L", // Spider Nest
            "F", // Giant Stronghold
            "S", // Dragon's Den
            "N", // Barbarian Stronghold
            "M", // Volcanic Caves
            "L", // Scorpion Nest
            "N", // Cemetery
            };

            // Get loot table key
            if (locationIndex < lootTableKeys.Length)
            {
                DaggerfallLoot.GenerateItems(lootTableKeys[locationIndex], loot.Items);

                // Randomly add map
                char key = lootTableKeys[locationIndex][0];
                int alphabetIndex = key - 64;

                if (alphabetIndex >= 10 && alphabetIndex <= 15) // between keys J and O
                {
                    int[] mapChances = { 2, 1, 1, 2, 2, 15 };
                    int mapChance = mapChances[alphabetIndex - 10];
                    DaggerfallLoot.RandomlyAddMap(mapChance, loot.Items);
                    DaggerfallLoot.RandomlyAddPotion(4, loot.Items);
                    DaggerfallLoot.RandomlyAddPotionRecipe(2, loot.Items);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Generates an array of items based on loot chance matrix.
        /// </summary>
        /// <param name="matrix">Loot chance matrix.</param>
        /// <param name="playerEntity">Player entity.</param>
        /// <returns>DaggerfallUnityItem array.</returns>
        public static DaggerfallUnityItem[] GenerateRandomLoot(LootChanceMatrix matrix, PlayerEntity playerEntity)
        {
            // Notes: The first part of the DF Chronicles explanation of how loot is generated does not match the released game.
            // It says the chance for each item category is the matrix amount * the level of the NPC. Actual behavior in the
            // released game is (matrix amount * PC level) for the first 4 item categories (temperate plants, warm plants,
            // miscellaneous monster, warm monster), and just matrix amount for the categories after that.
            // The second part of the DF Chronicles explanation (roll repeatedly for items from a category, each time at halved
            // chance), matches the game.
            // In classic, since a 0-99 roll is compared to whether it is less or greater than item chance,
            // even a 0% chance category has a 1/100 chance to appear, and the chance values are effectively
            // 1 higher than what the loot tables show.
            float chance;
            List<DaggerfallUnityItem> items = new List<DaggerfallUnityItem>();

            // Reseed random
            Random.InitState(items.GetHashCode());

            // Random gold
            int goldCount = Random.Range(matrix.MinGold, matrix.MaxGold + 1) * playerEntity.Level;
            if (goldCount > 0)
            {
                items.Add(ItemBuilder.CreateGoldPieces(goldCount));
            }

            // Random weapon
            chance = matrix.WP;
            while (Dice100.SuccessRoll((int)chance))
            {
                items.Add(ItemBuilder.CreateRandomWeapon(playerEntity.Level));
                chance *= 0.5f;
            }

            // Random armor
            chance = matrix.AM;
            while (Dice100.SuccessRoll((int)chance))
            {
                items.Add(ItemBuilder.CreateRandomArmor(playerEntity.Level, playerEntity.Gender, playerEntity.Race));
                chance *= 0.5f;
            }

            // Random ingredients
            RandomIngredient(matrix.C1 * playerEntity.Level, ItemGroups.CreatureIngredients1, items);
            RandomIngredient(matrix.C2 * playerEntity.Level, ItemGroups.CreatureIngredients2, items);
            RandomIngredient(matrix.C3, ItemGroups.CreatureIngredients3, items);
            RandomIngredient(matrix.P1 * playerEntity.Level, ItemGroups.PlantIngredients1, items);
            RandomIngredient(matrix.P2 * playerEntity.Level, ItemGroups.PlantIngredients2, items);
            RandomIngredient(matrix.M1, ItemGroups.MiscellaneousIngredients1, items);
            RandomIngredient(matrix.M2, ItemGroups.MiscellaneousIngredients2, items);

            // Random magic item
            chance = matrix.MI;
            while (Dice100.SuccessRoll((int)chance))
            {
                items.Add(ItemBuilder.CreateRandomMagicItem(playerEntity.Level, playerEntity.Gender, playerEntity.Race));
                chance *= 0.5f;
            }

            // Random clothes
            chance = matrix.CL;
            while (Dice100.SuccessRoll((int)chance))
            {
                items.Add(ItemBuilder.CreateRandomClothing(playerEntity.Gender, playerEntity.Race));
                chance *= 0.5f;
            }

            // Random books
            chance = matrix.BK;
            while (Dice100.SuccessRoll((int)chance))
            {
                items.Add(ItemBuilder.CreateRandomBook());
                chance *= 0.5f;
            }

            // Random religious item
            chance = matrix.RL;
            while (Dice100.SuccessRoll((int)chance))
            {
                items.Add(ItemBuilder.CreateRandomReligiousItem());
                chance *= 0.5f;
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
                items.Add(ItemBuilder.CreateGoldPieces(predefLootProps[0]));
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
                for (int i = 1; i < extraLootProps[1]; i++)
                {
                    items.Add(ItemBuilder.CreateRandomPotion()); // The whole Potion Recipe ID thing is a bit too confusing for me at this moment, so I can't specify what potions should be allowed, will work for now though. 
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
                for (int i = 1; i < extraLootProps[2]; i++)
                {
                    items.Add(ItemBuilder.CreateRandomGem());
                }
            }

            // Random magic items
            if (extraLootProps[3] > 0)
            {
                for (int i = 1; i < extraLootProps[3]; i++)
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
                for (int i = 1; i < extraLootProps[5]; i++)
                {
                    if (i == 1 && Dice100.SuccessRoll(15))
                    {
                        items.Add(ItemBuilder.CreateItem(ItemGroups.UselessItems2, (int)UselessItems2.Lantern));
                        DaggerfallUnityItem lampOil = ItemBuilder.CreateItem(ItemGroups.UselessItems2, (int)UselessItems2.Oil);
                        lampOil.stackCount = UnityEngine.Random.Range(0, 4 + 1);
                        items.Add(lampOil);
                        continue;
                    }
                    items.Add(ItemBuilder.CreateItem(ItemGroups.UselessItems2, PickOneOf((int)UselessItems2.Candle, (int)UselessItems2.Torch)));
                }
            }

            // Random religious items
            if (extraLootProps[6] > 0)
            {
                for (int i = 1; i < extraLootProps[6]; i++)
                {
                    items.Add(ItemBuilder.CreateRandomReligiousItem());
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
                for (int i = 1; i < extraLootProps[9]; i++)
                {
                    items.Add(ItemBuilder.CreateRandomDrug());
                }
            }

            // Extra weapon
            if (extraLootProps[10] > 0)
            {
                for (int i = 1; i < extraLootProps[10]; i++)
                {
                    if (i == 1 && AITarget.EntityType == EntityTypes.EnemyClass && AITarget.CareerIndex == (int)ClassCareers.Nightblade)
                    {
                        DaggerfallUnityItem extraWep = ItemBuilder.CreateWeapon((Weapons)PickOneOf((int)Weapons.Dagger, (int)Weapons.Tanto, (int)Weapons.Shortsword, (int)Weapons.Longsword), WeaponMaterialTypes.Silver);
                        condPercentMod = Random.Range(enemyLootCondMods[0], enemyLootCondMods[1] + 1) / 100f;
                        extraWep.currentCondition = (int)Mathf.Ceil(extraWep.maxCondition * condPercentMod);
                        continue;
                    }

                    if (traits[2] == (int)MobilePersonalityInterests.Survivalist || traits[2] == (int)MobilePersonalityInterests.Hunter)
                    {
                        DaggerfallUnityItem extraWep = ItemBuilder.CreateWeapon((Weapons)PickOneOf((int)Weapons.Short_Bow, (int)Weapons.Long_Bow), FormulaHelper.RandomMaterial(level));
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
                for (int i = 1; i < extraLootProps[11]; i++)
                {
                    items.Add(new DaggerfallUnityItem(ItemGroups.MiscItems, 8));
                }
            }

            // Random clothes
            if (predefLootProps[9] > 0)
            {
                AddClothesBasedOnEnemy(player.Gender, player.Race, AITarget, enemyLootCondMods, items); // Will obviously have to change this later on when I add the location specific context variables of this loot system. 
            }

            // Ingredients
            bool customIngredCheck = TargetedIngredients(AITarget, predefLootProps, items);

            if (!customIngredCheck)
            {
                if (predefLootProps[1] > 0)
                {
                    for (int i = 1; i < predefLootProps[1]; i++)
                        items.Add(ItemBuilder.CreateRandomIngredient(ItemGroups.MiscPlantIngredients));
                }

                if (predefLootProps[2] > 0)
                {
                    for (int i = 1; i < predefLootProps[2]; i++)
                        items.Add(ItemBuilder.CreateRandomIngredient(ItemGroups.FlowerPlantIngredients));
                }

                if (predefLootProps[3] > 0)
                {
                    for (int i = 1; i < predefLootProps[3]; i++)
                        items.Add(ItemBuilder.CreateRandomIngredient(ItemGroups.FruitPlantIngredients));
                }

                if (predefLootProps[4] > 0)
                {
                    for (int i = 1; i < predefLootProps[4]; i++)
                        items.Add(ItemBuilder.CreateRandomIngredient(ItemGroups.AnimalPartIngredients));
                }

                if (predefLootProps[5] > 0)
                {
                    for (int i = 1; i < predefLootProps[5]; i++)
                        items.Add(ItemBuilder.CreateRandomIngredient(ItemGroups.CreatureIngredients));
                }

                if (predefLootProps[6] > 0)
                {
                    for (int i = 1; i < predefLootProps[6]; i++)
                        items.Add(ItemBuilder.CreateRandomIngredient(ItemGroups.SolventIngredients));
                }

                if (predefLootProps[7] > 0)
                {
                    for (int i = 1; i < predefLootProps[7]; i++)
                        items.Add(ItemBuilder.CreateRandomIngredient(ItemGroups.MetalIngredients));
                }
            }

            // Extra flavor/junk items (mostly based on personality traits, if present)
            if (traits[0] > -1 || traits[1] > -1 || traits[2] > -1)
            {
                PersonalityTraitFlavorItemsGenerator(AITarget, traits, items); // Items not yet implemented.
            }

            return items.ToArray();
        }

        public static int PickOneOf(params int[] values) // Pango provided assistance in making this much cleaner way of doing the random value choice part, awesome.
        {
            return values[Random.Range(0, values.Length)];
        }

        #region Private Methods

        static void PersonalityTraitFlavorItemsGenerator(EnemyEntity AITarget, int[] traits, List<DaggerfallUnityItem> targetItems)
        {
            int level = AITarget.Level;

            if (traits[0] > -1 || traits[1] > -1)
            {
                if (traits[0] == (int)MobilePersonalityQuirks.Curious || traits[1] == (int)MobilePersonalityQuirks.Curious)
                {
                    int randRange = Random.Range(1, 3 + 1);
                    for (int i = 1; i < randRange; i++)
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.UselessItems2, PickOneOf(811, 812, 813, 814))); // Item ID will be whatever the respective item IDs are in their respective itemgroup Enum, when finished. 
                }

                if (traits[0] == (int)MobilePersonalityQuirks.Addict || traits[1] == (int)MobilePersonalityQuirks.Addict)
                {
                    int randRange = Random.Range(2, 6 + 1);
                    for (int i = 1; i < randRange; i++)
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.UselessItems2, PickOneOf(811, 812, 813, 814))); // Item ID will be whatever the respective item IDs are in their respective itemgroup Enum, when finished. 
                }

                if (traits[0] == (int)MobilePersonalityQuirks.Hoarder || traits[1] == (int)MobilePersonalityQuirks.Hoarder)
                {
                    int randRange = Random.Range(6, 18 + 1);
                    for (int i = 1; i < randRange; i++)
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.UselessItems2, PickOneOf(811, 812, 813, 814))); // Item ID will be whatever the respective item IDs are in their respective itemgroup Enum, when finished. 
                }

                if (traits[0] == (int)MobilePersonalityQuirks.Vain || traits[1] == (int)MobilePersonalityQuirks.Vain)
                {
                    int randRange = Random.Range(2, 7 + 1);
                    for (int i = 1; i < randRange; i++)
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.UselessItems2, PickOneOf(811, 812, 813, 814))); // Item ID will be whatever the respective item IDs are in their respective itemgroup Enum, when finished. 
                }

                if (traits[0] == (int)MobilePersonalityQuirks.Untrusting || traits[1] == (int)MobilePersonalityQuirks.Untrusting)
                {
                    int randRange = Random.Range(1, 3 + 1);
                    for (int i = 1; i < randRange; i++)
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.UselessItems2, PickOneOf(811, 812, 813, 814))); // Item ID will be whatever the respective item IDs are in their respective itemgroup Enum, when finished.
                    // This won't work the same as the others, since it in theory will be placing existing items into a seperate lock-box inventory type of item, will need a lot of work on this one eventually.
                }

                if (traits[0] == (int)MobilePersonalityQuirks.Sadistic || traits[1] == (int)MobilePersonalityQuirks.Sadistic)
                {
                    int randRange = Random.Range(1, 4 + 1);
                    for (int i = 1; i < randRange; i++)
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.UselessItems2, PickOneOf(811, 812, 813, 814))); // Item ID will be whatever the respective item IDs are in their respective itemgroup Enum, when finished. 
                }

                if (traits[0] == (int)MobilePersonalityQuirks.Romantic || traits[1] == (int)MobilePersonalityQuirks.Romantic)
                {
                    int randRange = Random.Range(2, 4 + 1);
                    for (int i = 1; i < randRange; i++)
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.UselessItems2, PickOneOf(811, 812, 813, 814))); // Item ID will be whatever the respective item IDs are in their respective itemgroup Enum, when finished. 
                }

                if (traits[0] == (int)MobilePersonalityQuirks.Alcoholic || traits[1] == (int)MobilePersonalityQuirks.Alcoholic)
                {
                    int randRange = Random.Range(2, 6 + 1);
                    for (int i = 1; i < randRange; i++)
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.UselessItems2, PickOneOf(811, 812, 813, 814))); // Item ID will be whatever the respective item IDs are in their respective itemgroup Enum, when finished. 
                }
            }

            if (traits[2] > -1)
            {
                if (traits[2] == (int)MobilePersonalityInterests.God_Fearing)
                {
                    int randRange = Random.Range(1, 3 + 1);
                    for (int i = 1; i < randRange; i++)
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.UselessItems2, PickOneOf(811, 812, 813, 814))); // Item ID will be whatever the respective item IDs are in their respective itemgroup Enum, when finished. 
                }

                if (traits[2] == (int)MobilePersonalityInterests.Occultist)
                {
                    int randRange = Random.Range(2, 4 + 1);
                    for (int i = 1; i < randRange; i++)
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.UselessItems2, PickOneOf(811, 812, 813, 814))); // Item ID will be whatever the respective item IDs are in their respective itemgroup Enum, when finished. 
                }

                if (traits[2] == (int)MobilePersonalityInterests.Childish)
                {
                    int randRange = Random.Range(1, 3 + 1);
                    for (int i = 1; i < randRange; i++)
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.UselessItems2, PickOneOf(811, 812, 813, 814))); // Item ID will be whatever the respective item IDs are in their respective itemgroup Enum, when finished. 
                }

                if (traits[2] == (int)MobilePersonalityInterests.Artistic)
                {
                    int randRange = Random.Range(1, 3 + 1);
                    for (int i = 1; i < randRange; i++)
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.UselessItems2, PickOneOf(811, 812, 813, 814))); // Item ID will be whatever the respective item IDs are in their respective itemgroup Enum, when finished. 
                }

                if (traits[2] == (int)MobilePersonalityInterests.Collector)
                {
                    int randRange = Random.Range(4, 11 + 1);
                    for (int i = 1; i < randRange; i++)
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.UselessItems2, PickOneOf(811, 812, 813, 814))); // Item ID will be whatever the respective item IDs are in their respective itemgroup Enum, when finished. 
                }

                if (traits[2] == (int)MobilePersonalityInterests.Survivalist)
                {
                    int randRange = Random.Range(1, 3 + 1);
                    for (int i = 1; i < randRange; i++)
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.UselessItems2, PickOneOf(811, 812, 813, 814))); // Item ID will be whatever the respective item IDs are in their respective itemgroup Enum, when finished. 
                }

                if (traits[2] == (int)MobilePersonalityInterests.Hunter)
                {
                    int randRange = Random.Range(1, 3 + 1);
                    for (int i = 1; i < randRange; i++)
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.UselessItems2, PickOneOf(811, 812, 813, 814))); // Item ID will be whatever the respective item IDs are in their respective itemgroup Enum, when finished. 
                }

                if (traits[2] == (int)MobilePersonalityInterests.Fetishist)
                {
                    int randRange = Random.Range(2, 4 + 1);
                    for (int i = 1; i < randRange; i++)
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.UselessItems2, PickOneOf(811, 812, 813, 814))); // Item ID will be whatever the respective item IDs are in their respective itemgroup Enum, when finished. 
                }

                if (traits[2] == (int)MobilePersonalityInterests.Brewer)
                {
                    int randRange = Random.Range(1, 3 + 1);
                    for (int i = 1; i < randRange; i++)
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.UselessItems2, PickOneOf(811, 812, 813, 814))); // Item ID will be whatever the respective item IDs are in their respective itemgroup Enum, when finished. 
                }

                if (traits[2] == (int)MobilePersonalityInterests.Cartographer)
                {
                    int randRange = Random.Range(1, 3 + 1);
                    for (int i = 1; i < randRange; i++)
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.UselessItems2, PickOneOf(811, 812, 813, 814))); // Item ID will be whatever the respective item IDs are in their respective itemgroup Enum, when finished. 
                }

                if (traits[2] == (int)MobilePersonalityInterests.Fisher)
                {
                    int randRange = Random.Range(1, 3 + 1);
                    for (int i = 1; i < randRange; i++)
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.UselessItems2, PickOneOf(811, 812, 813, 814))); // Item ID will be whatever the respective item IDs are in their respective itemgroup Enum, when finished. 
                }

                if (traits[2] == (int)MobilePersonalityInterests.Diver)
                {
                    int randRange = Random.Range(1, 3 + 1);
                    for (int i = 1; i < randRange; i++)
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.UselessItems2, PickOneOf(811, 812, 813, 814))); // Item ID will be whatever the respective item IDs are in their respective itemgroup Enum, when finished. 
                }

                if (traits[2] == (int)MobilePersonalityInterests.Writer)
                {
                    int randRange = Random.Range(1, 3 + 1);
                    for (int i = 1; i < randRange; i++)
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.UselessItems2, PickOneOf(811, 812, 813, 814))); // Item ID will be whatever the respective item IDs are in their respective itemgroup Enum, when finished. 
                }

                if (traits[2] == (int)MobilePersonalityInterests.Handy)
                {
                    int randRange = Random.Range(1, 3 + 1);
                    for (int i = 1; i < randRange; i++)
                        targetItems.Add(ItemBuilder.CreateItem(ItemGroups.UselessItems2, PickOneOf(811, 812, 813, 814))); // Item ID will be whatever the respective item IDs are in their respective itemgroup Enum, when finished. 
                }
            }

            return;
        }

        static bool TargetedIngredients(EnemyEntity AITarget, int[] predefLootProps, List<DaggerfallUnityItem> targetItems)
        {
            DaggerfallUnityItem ingredients = null;

            if (AITarget.EntityType == EntityTypes.EnemyClass)
            {
                return false;
            }
            else
            {
                switch (AITarget.CareerIndex)
                {
                    case 0:
                    case 3:
                        ingredients = ItemBuilder.CreateItem(ItemGroups.AnimalPartIngredients, (int)AnimalPartIngredients.Small_tooth);
                        ingredients.stackCount = predefLootProps[4];
                        targetItems.Add(ingredients);
                        return true;
                    case 4:
                    case 5:
                        ingredients = ItemBuilder.CreateItem(ItemGroups.AnimalPartIngredients, (int)AnimalPartIngredients.Big_tooth);
                        ingredients.stackCount = predefLootProps[4];
                        targetItems.Add(ingredients);
                        return true;
                    case 6:
                        ingredients = ItemBuilder.CreateItem(ItemGroups.AnimalPartIngredients, (int)AnimalPartIngredients.Spider_venom);
                        ingredients.stackCount = predefLootProps[4];
                        targetItems.Add(ingredients);
                        return true;
                    case 11:
                        for (int i = 1; i < predefLootProps[4]; i++)
                        {
                            if (Dice100.SuccessRoll(95))
                                targetItems.Add(ItemBuilder.CreateItem(ItemGroups.AnimalPartIngredients, (int)AnimalPartIngredients.Small_tooth));
                            else
                                targetItems.Add(ItemBuilder.CreateItem(ItemGroups.AnimalPartIngredients, (int)AnimalPartIngredients.Pearl));
                        }
                        return true;
                    case 20:
                        ingredients = ItemBuilder.CreateItem(ItemGroups.AnimalPartIngredients, (int)AnimalPartIngredients.Giant_scorpion_stinger);
                        ingredients.stackCount = predefLootProps[4];
                        targetItems.Add(ingredients);
                        return true;
                    case 2:
                        for (int i = 1; i < predefLootProps[1]; i++)
                            targetItems.Add(ItemBuilder.CreateRandomIngredient(ItemGroups.MiscPlantIngredients));
                        for (int i = 1; i < predefLootProps[2]; i++)
                            targetItems.Add(ItemBuilder.CreateRandomIngredient(ItemGroups.FlowerPlantIngredients));
                        for (int i = 1; i < predefLootProps[3]; i++)
                        {
                            if (Dice100.SuccessRoll(90))
                                targetItems.Add(ItemBuilder.CreateRandomIngredient(ItemGroups.FruitPlantIngredients));
                            else
                                targetItems.Add(ItemBuilder.CreateItem(ItemGroups.Gems, (int)Gems.Amber));
                        }
                        return true;
                    case 10:
                        for (int i = 1; i < predefLootProps[2]; i++)
                            targetItems.Add(ItemBuilder.CreateRandomIngredient(ItemGroups.FlowerPlantIngredients));
                        ingredients = ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Nymph_hair);
                        ingredients.stackCount = predefLootProps[5];
                        targetItems.Add(ingredients);
                        return true;
                    case 13:
                        ingredients = ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Harpy_Feather);
                        ingredients.stackCount = predefLootProps[5];
                        targetItems.Add(ingredients);
                        return true;
                    case 16:
                        ingredients = ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Giant_blood);
                        ingredients.stackCount = predefLootProps[5];
                        targetItems.Add(ingredients);
                        return true;
                    case 22:
                        ingredients = ItemBuilder.CreateItem(ItemGroups.MiscPlantIngredients, (int)MiscPlantIngredients.Root_tendrils);
                        ingredients.stackCount = predefLootProps[1];
                        targetItems.Add(ingredients);
                        ingredients = ItemBuilder.CreateItem(ItemGroups.MetalIngredients, (int)MetalIngredients.Lodestone);
                        ingredients.stackCount = predefLootProps[7];
                        targetItems.Add(ingredients);
                        return true;
                    case 34:
                        ingredients = ItemBuilder.CreateItem(ItemGroups.AnimalPartIngredients, (int)AnimalPartIngredients.Small_tooth);
                        ingredients.stackCount = predefLootProps[4];
                        targetItems.Add(ingredients);
                        for (int i = 1; i < predefLootProps[5]; i++)
                        {
                            if (Dice100.SuccessRoll(40))
                                targetItems.Add(ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Dragons_scales));
                            else
                                targetItems.Add(ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Fairy_dragon_scales));
                        }
                        return true;
                    case 40:
                        ingredients = ItemBuilder.CreateItem(ItemGroups.AnimalPartIngredients, (int)AnimalPartIngredients.Big_tooth);
                        ingredients.stackCount = predefLootProps[4];
                        targetItems.Add(ingredients);
                        for (int i = 1; i < predefLootProps[5]; i++)
                        {
                            if (Dice100.SuccessRoll(80))
                                targetItems.Add(ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Dragons_scales));
                            else
                                targetItems.Add(ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Fairy_dragon_scales));
                        }
                        return true;
                    case 41:
                        ingredients = ItemBuilder.CreateItem(ItemGroups.AnimalPartIngredients, (int)AnimalPartIngredients.Pearl);
                        ingredients.stackCount = predefLootProps[4];
                        targetItems.Add(ingredients);
                        return true;
                    case 42:
                        for (int i = 1; i < predefLootProps[5]; i++)
                        {
                            if (Dice100.SuccessRoll(35))
                                targetItems.Add(ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Nymph_hair));
                            else
                                targetItems.Add(ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Gorgon_snake));
                        }
                        return true;
                    case 9:
                        ingredients = ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Werewolfs_blood);
                        ingredients.stackCount = predefLootProps[5];
                        targetItems.Add(ingredients);
                        return true;
                    case 14:
                        ingredients = ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Wereboar_tusk);
                        ingredients.stackCount = predefLootProps[5];
                        targetItems.Add(ingredients);
                        return true;
                    case 35:
                        ingredients = ItemBuilder.CreateItem(ItemGroups.MetalIngredients, (int)MetalIngredients.Sulphur);
                        ingredients.stackCount = predefLootProps[7];
                        targetItems.Add(ingredients);
                        return true;
                    case 36:
                        ingredients = ItemBuilder.CreateItem(ItemGroups.MetalIngredients, (int)MetalIngredients.Iron);
                        ingredients.stackCount = predefLootProps[7];
                        targetItems.Add(ingredients);
                        return true;
                    case 37:
                        ingredients = ItemBuilder.CreateItem(ItemGroups.SolventIngredients, (int)SolventIngredients.Ichor);
                        ingredients.stackCount = predefLootProps[6];
                        targetItems.Add(ingredients);
                        return true;
                    case 38:
                        ingredients = ItemBuilder.CreateItem(ItemGroups.SolventIngredients, (int)SolventIngredients.Pure_water);
                        ingredients.stackCount = predefLootProps[6];
                        targetItems.Add(ingredients);
                        return true;
                    case 18:
                        ingredients = ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Ectoplasm);
                        ingredients.stackCount = predefLootProps[5];
                        targetItems.Add(ingredients);
                        return true;
                    case 19:
                        ingredients = ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Mummy_wrappings);
                        ingredients.stackCount = predefLootProps[5];
                        targetItems.Add(ingredients);
                        return true;
                    case 23:
                        for (int i = 1; i < predefLootProps[5]; i++)
                        {
                            if (Dice100.SuccessRoll(70))
                                targetItems.Add(ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Ectoplasm));
                            else
                                targetItems.Add(ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Wraith_essence));
                        }
                        return true;
                    case 28:
                    case 30:
                        ingredients = ItemBuilder.CreateItem(ItemGroups.AnimalPartIngredients, (int)AnimalPartIngredients.Small_tooth);
                        ingredients.stackCount = predefLootProps[4];
                        targetItems.Add(ingredients);
                        for (int i = 1; i < predefLootProps[5]; i++)
                            targetItems.Add(ItemBuilder.CreateRandomIngredient(ItemGroups.CreatureIngredients));
                        return true;
                    case 32:
                    case 33:
                        ingredients = ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Lich_dust);
                        ingredients.stackCount = predefLootProps[5];
                        targetItems.Add(ingredients);
                        return true;
                    case 25:
                        ingredients = ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Daedra_heart);
                        ingredients.stackCount = predefLootProps[5];
                        targetItems.Add(ingredients);
                        ingredients = ItemBuilder.CreateItem(ItemGroups.SolventIngredients, (int)SolventIngredients.Pure_water);
                        ingredients.stackCount = predefLootProps[6];
                        targetItems.Add(ingredients);
                        return true;
                    case 26:
                        ingredients = ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Daedra_heart);
                        ingredients.stackCount = predefLootProps[5];
                        targetItems.Add(ingredients);
                        ingredients = ItemBuilder.CreateItem(ItemGroups.MetalIngredients, (int)MetalIngredients.Sulphur);
                        ingredients.stackCount = predefLootProps[7];
                        targetItems.Add(ingredients);
                        return true;
                    case 27:
                        ingredients = ItemBuilder.CreateItem(ItemGroups.AnimalPartIngredients, (int)AnimalPartIngredients.Big_tooth);
                        ingredients.stackCount = predefLootProps[4];
                        targetItems.Add(ingredients);
                        ingredients = ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Daedra_heart);
                        ingredients.stackCount = predefLootProps[5];
                        targetItems.Add(ingredients);
                        return true;
                    case 29:
                        ingredients = ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Daedra_heart);
                        ingredients.stackCount = predefLootProps[5];
                        targetItems.Add(ingredients);
                        ingredients = ItemBuilder.CreateItem(ItemGroups.SolventIngredients, (int)SolventIngredients.Elixir_vitae);
                        ingredients.stackCount = predefLootProps[6];
                        targetItems.Add(ingredients);
                        return true;
                    case 31:
                        ingredients = ItemBuilder.CreateItem(ItemGroups.CreatureIngredients, (int)CreatureIngredients.Daedra_heart);
                        ingredients.stackCount = predefLootProps[5];
                        targetItems.Add(ingredients);
                        ingredients = ItemBuilder.CreateItem(ItemGroups.SolventIngredients, (int)SolventIngredients.Ichor);
                        ingredients.stackCount = predefLootProps[6];
                        targetItems.Add(ingredients);
                        return true;
                    default:
                        return false;
                }
            }
        }

        static void AddBooksBasedOnSubject(EnemyEntity AITarget, int bookAmount, List<DaggerfallUnityItem> targetItems)
        {
            if (AITarget.EntityType == EntityTypes.EnemyClass)
            {
                switch (AITarget.CareerIndex)
                {
                    case (int)ClassCareers.Mage:
                        for (int i = 1; i < bookAmount; i++)
                            targetItems.Add(ItemBuilder.CreateRandomBookOfSpecificSubject((ItemGroups)PickOneOf(33, 33, 33, 33, 33, 33, 33, 33, 33, 28, 30, 30, 30, 31, 31, 32, 34, 36, 36, 39)));
                        return;
                    case (int)ClassCareers.Spellsword:
                        for (int i = 1; i < bookAmount; i++)
                            targetItems.Add(ItemBuilder.CreateRandomBookOfSpecificSubject((ItemGroups)PickOneOf(33, 33, 33, 33, 33, 33, 28, 28, 30, 30, 30, 30, 30, 31, 31, 32, 34, 36, 36, 39)));
                        return;
                    case (int)ClassCareers.Battlemage:
                        for (int i = 1; i < bookAmount; i++)
                            targetItems.Add(ItemBuilder.CreateRandomBookOfSpecificSubject((ItemGroups)PickOneOf(33, 33, 33, 33, 28, 28, 28, 30, 30, 30, 30, 30, 30, 31, 31, 32, 34, 36, 36, 39)));
                        return;
                    case (int)ClassCareers.Sorcerer:
                        for (int i = 1; i < bookAmount; i++)
                            targetItems.Add(ItemBuilder.CreateRandomBookOfSpecificSubject((ItemGroups)PickOneOf(33, 33, 33, 28, 28, 30, 30, 30, 30, 30, 30, 30, 31, 31, 31, 32, 34, 36, 36, 39)));
                        return;
                    case (int)ClassCareers.Healer:
                    case (int)ClassCareers.Monk:
                        for (int i = 1; i < bookAmount; i++)
                            targetItems.Add(ItemBuilder.CreateRandomBookOfSpecificSubject((ItemGroups)PickOneOf(38, 38, 38, 38, 38, 38, 38, 38, 33, 28, 30, 30, 31, 31, 31, 32, 34, 36, 39, 39)));
                        return;
                    case (int)ClassCareers.Nightblade:
                        for (int i = 1; i < bookAmount; i++)
                            targetItems.Add(ItemBuilder.CreateRandomBookOfSpecificSubject((ItemGroups)PickOneOf(28, 29, 30, 30, 30, 30, 30, 30, 31, 31, 33, 33, 34, 35, 36, 36, 37, 38, 38, 39)));
                        return;
                    case (int)ClassCareers.Bard:
                        for (int i = 1; i < bookAmount; i++)
                            targetItems.Add(ItemBuilder.CreateRandomBookOfSpecificSubject((ItemGroups)PickOneOf(28, 28, 29, 30, 30, 30, 31, 31, 31, 32, 32, 34, 35, 36, 36, 36, 36, 38, 38, 39)));
                        return;
                    case (int)ClassCareers.Assassin:
                        for (int i = 1; i < bookAmount; i++)
                            targetItems.Add(ItemBuilder.CreateRandomBookOfSpecificSubject((ItemGroups)PickOneOf(28, 28, 29, 29, 29, 30, 30, 31, 31, 31, 31, 33, 33, 34, 35, 36, 37, 37, 38, 39)));
                        return;
                    case (int)ClassCareers.Ranger:
                        for (int i = 1; i < bookAmount; i++)
                            targetItems.Add(ItemBuilder.CreateRandomBookOfSpecificSubject((ItemGroups)PickOneOf(28, 28, 29, 30, 30, 31, 31, 31, 32, 33, 33, 33, 33, 34, 35, 36, 38, 39, 39, 39)));
                        return;
                    case (int)ClassCareers.Knight:
                        for (int i = 1; i < bookAmount; i++)
                            targetItems.Add(ItemBuilder.CreateRandomBookOfSpecificSubject((ItemGroups)PickOneOf(28, 28, 28, 30, 30, 30, 31, 31, 31, 36, 36, 36, 36, 37, 37, 38, 38, 38, 38, 39)));
                        return;
                    default:
                        for (int i = 1; i < bookAmount; i++)
                            targetItems.Add(ItemBuilder.CreateRandomBookOfRandomSubject());
                        return;
                }
            }
            else
            {
                switch (AITarget.CareerIndex)
                {
                    case 21:
                        for (int i = 1; i < bookAmount; i++)
                            targetItems.Add(ItemBuilder.CreateRandomBookOfSpecificSubject((ItemGroups)PickOneOf(28, 30, 30, 30, 30, 31, 31, 33, 33, 36, 38, 38, 38, 38, 38, 38, 38, 38, 38, 39)));
                        return;
                    case 32:
                    case 33:
                        for (int i = 1; i < bookAmount; i++)
                            targetItems.Add(ItemBuilder.CreateRandomBookOfSpecificSubject((ItemGroups)PickOneOf(28, 30, 30, 31, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 34, 35, 35, 36, 38, 39)));
                        return;
                    default:
                        for (int i = 1; i < bookAmount; i++)
                            targetItems.Add(ItemBuilder.CreateRandomBookOfRandomSubject());
                        return;
                }
            }
        }

        static void AddClothesBasedOnEnemy(Genders playerGender, Races playerRace, EnemyEntity AITarget, int[] condMods,List<DaggerfallUnityItem> targetItems)
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
                                targetItems.Add(ItemBuilder.CreateMensClothing((MensClothing)PickOneOf((int)MensClothing.Casual_cloak, (int)MensClothing.Casual_cloak, (int)MensClothing.Formal_cloak), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        }
                        else
                        {
                            targetItems.Add(ItemBuilder.CreateWomensClothing(WomensClothing.Priestess_robes, playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                            targetItems.Add(ItemBuilder.CreateRandomShoes(enemyGender, playerRace, condMods[0], condMods[1]));
                            if (Dice100.SuccessRoll(50))
                                targetItems.Add(ItemBuilder.CreateRandomPants(enemyGender, playerRace, condMods[0], condMods[1]));
                            if (Dice100.SuccessRoll(50))
                                targetItems.Add(ItemBuilder.CreateWomensClothing((WomensClothing)PickOneOf((int)WomensClothing.Casual_cloak, (int)WomensClothing.Casual_cloak, (int)WomensClothing.Formal_cloak), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
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
                            targetItems.Add(ItemBuilder.CreateMensClothing((MensClothing)PickOneOf((int)MensClothing.Shoes, (int)MensClothing.Sandals), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                            if (Dice100.SuccessRoll(50))
                                targetItems.Add(ItemBuilder.CreateMensClothing(MensClothing.Casual_cloak, playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        }
                        else
                        {
                            targetItems.Add(ItemBuilder.CreateWomensClothing(WomensClothing.Khajiit_suit, playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                            targetItems.Add(ItemBuilder.CreateWomensClothing((WomensClothing)PickOneOf((int)WomensClothing.Shoes, (int)WomensClothing.Sandals), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                            if (Dice100.SuccessRoll(50))
                                targetItems.Add(ItemBuilder.CreateWomensClothing(WomensClothing.Casual_cloak, playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        }
                        return;
                    case (int)ClassCareers.Monk:
                        if (enemyGender == Genders.Male)
                        {
                            targetItems.Add(ItemBuilder.CreateRandomPants(enemyGender, playerRace, condMods[0], condMods[1]));
                            if (Dice100.SuccessRoll(50))
                                targetItems.Add(ItemBuilder.CreateMensClothing((MensClothing)PickOneOf((int)MensClothing.Sash, (int)MensClothing.Toga, (int)MensClothing.Kimono, (int)MensClothing.Armbands, (int)MensClothing.Vest), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                            if (Dice100.SuccessRoll(50))
                                targetItems.Add(ItemBuilder.CreateMensClothing((MensClothing)PickOneOf((int)MensClothing.Shoes, (int)MensClothing.Sandals), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                            if (Dice100.SuccessRoll(25))
                                targetItems.Add(ItemBuilder.CreateMensClothing(MensClothing.Casual_cloak, playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        }
                        else
                        {
                            targetItems.Add(ItemBuilder.CreateRandomPants(enemyGender, playerRace, condMods[0], condMods[1]));
                            targetItems.Add(ItemBuilder.CreateRandomBra(enemyGender, playerRace, condMods[0], condMods[1]));
                            if (Dice100.SuccessRoll(50))
                                targetItems.Add(ItemBuilder.CreateWomensClothing((WomensClothing)PickOneOf((int)WomensClothing.Shoes, (int)WomensClothing.Sandals), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                            if (Dice100.SuccessRoll(25))
                                targetItems.Add(ItemBuilder.CreateWomensClothing(WomensClothing.Casual_cloak, playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        }
                        return;
                    case (int)ClassCareers.Barbarian:
                        if (enemyGender == Genders.Male)
                        {
                            targetItems.Add(ItemBuilder.CreateRandomShoes(enemyGender, playerRace, condMods[0], condMods[1]));
                            targetItems.Add(ItemBuilder.CreateMensClothing((MensClothing)PickOneOf((int)MensClothing.Short_skirt, (int)MensClothing.Long_Skirt, (int)MensClothing.Loincloth, (int)MensClothing.Wrap), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                            if (Dice100.SuccessRoll(50))
                                targetItems.Add(ItemBuilder.CreateMensClothing((MensClothing)PickOneOf((int)MensClothing.Sash, (int)MensClothing.Armbands, (int)MensClothing.Fancy_Armbands, (int)MensClothing.Straps, (int)MensClothing.Challenger_Straps, (int)MensClothing.Champion_straps), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                            if (Dice100.SuccessRoll(50))
                                targetItems.Add(ItemBuilder.CreateMensClothing(MensClothing.Casual_cloak, playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        }
                        else
                        {
                            targetItems.Add(ItemBuilder.CreateRandomBra(enemyGender, playerRace, condMods[0], condMods[1]));
                            targetItems.Add(ItemBuilder.CreateRandomShoes(enemyGender, playerRace, condMods[0], condMods[1]));
                            targetItems.Add(ItemBuilder.CreateWomensClothing((WomensClothing)PickOneOf((int)WomensClothing.Loincloth, (int)WomensClothing.Wrap, (int)WomensClothing.Long_skirt), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
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
                        targetItems.Add(ItemBuilder.CreateMensClothing((MensClothing)PickOneOf((int)MensClothing.Short_skirt, (int)MensClothing.Long_Skirt, (int)MensClothing.Loincloth, (int)MensClothing.Wrap), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        if (Dice100.SuccessRoll(50))
                            targetItems.Add(ItemBuilder.CreateMensClothing((MensClothing)PickOneOf((int)MensClothing.Straps, (int)MensClothing.Challenger_Straps), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        if (Dice100.SuccessRoll(50))
                            targetItems.Add(ItemBuilder.CreateMensClothing(MensClothing.Casual_cloak, playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        return;
                    case 24:
                        targetItems.Add(ItemBuilder.CreateRandomShoes(Genders.Male, playerRace, condMods[0], condMods[1]));
                        targetItems.Add(ItemBuilder.CreateMensClothing((MensClothing)PickOneOf((int)MensClothing.Short_skirt, (int)MensClothing.Long_Skirt, (int)MensClothing.Loincloth, (int)MensClothing.Wrap), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        if (Dice100.SuccessRoll(50))
                            targetItems.Add(ItemBuilder.CreateMensClothing((MensClothing)PickOneOf((int)MensClothing.Challenger_Straps, (int)MensClothing.Champion_straps), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
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
                                targetItems.Add(ItemBuilder.CreateMensClothing((MensClothing)PickOneOf((int)MensClothing.Boots, (int)MensClothing.Tall_Boots), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        }
                        else
                        {
                            targetItems.Add(ItemBuilder.CreateWomensClothing(WomensClothing.Casual_cloak, playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                            if (Dice100.SuccessRoll(50))
                                targetItems.Add(ItemBuilder.CreateWomensClothing((WomensClothing)PickOneOf((int)WomensClothing.Boots, (int)WomensClothing.Tall_boots), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        }
                        return;
                    case 27:
                        if (playerGender == Genders.Male)
                        {
                            targetItems.Add(ItemBuilder.CreateMensClothing(MensClothing.Loincloth, playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                            targetItems.Add(ItemBuilder.CreateMensClothing((MensClothing)PickOneOf((int)MensClothing.Boots, (int)MensClothing.Tall_Boots), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        }
                        else
                        {
                            targetItems.Add(ItemBuilder.CreateWomensClothing(WomensClothing.Loincloth, playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                            targetItems.Add(ItemBuilder.CreateWomensClothing((WomensClothing)PickOneOf((int)WomensClothing.Boots, (int)WomensClothing.Tall_boots), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        }
                        return;
                    case 29:
                        if (Dice100.SuccessRoll(50))
                            targetItems.Add(ItemBuilder.CreateWomensClothing((WomensClothing)PickOneOf((int)WomensClothing.Eodoric, (int)WomensClothing.Formal_eodoric, (int)WomensClothing.Strapless_dress), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        if (Dice100.SuccessRoll(20))
                            targetItems.Add(ItemBuilder.CreateRandomBra(Genders.Female, playerRace, condMods[0], condMods[1]));
                        return;
                    case 31:
                        targetItems.Add(ItemBuilder.CreateRandomPants(Genders.Male, playerRace, condMods[0], condMods[1]));
                        targetItems.Add(ItemBuilder.CreateMensClothing((MensClothing)PickOneOf((int)MensClothing.Boots, (int)MensClothing.Tall_Boots), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
                        targetItems.Add(ItemBuilder.CreateMensClothing((MensClothing)PickOneOf((int)MensClothing.Casual_cloak, (int)MensClothing.Formal_cloak), playerRace, -1, condMods[0], condMods[1],  ItemBuilder.RandomClothingDye()));
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
                            equipTableProps[0] = 60;
                            equipTableProps[1] = 20;
                            break;
                        case (int)ClassCareers.Spellsword:
                            equipTableProps[0] = 70;
                            equipTableProps[1] = 30;
                            break;
                        case (int)ClassCareers.Battlemage:
                            equipTableProps[0] = 70;
                            equipTableProps[1] = 30;
                            break;
                        case (int)ClassCareers.Sorcerer:
                            equipTableProps[0] = 60;
                            equipTableProps[1] = 20;
                            break;
                        case (int)ClassCareers.Healer:
                            equipTableProps[0] = 65;
                            equipTableProps[1] = 25;
                            break;
                        case (int)ClassCareers.Nightblade:
                            equipTableProps[0] = 80;
                            equipTableProps[1] = 40;
                            break;
                        case (int)ClassCareers.Bard:
                            equipTableProps[0] = 70;
                            equipTableProps[1] = 30;
                            break;
                        case (int)ClassCareers.Burglar:
                            equipTableProps[0] = 60;
                            equipTableProps[1] = 20;
                            break;
                        case (int)ClassCareers.Rogue:
                            equipTableProps[0] = 60;
                            equipTableProps[1] = 20;
                            break;
                        case (int)ClassCareers.Acrobat:
                            equipTableProps[0] = 65;
                            equipTableProps[1] = 25;
                            break;
                        case (int)ClassCareers.Thief:
                            equipTableProps[0] = 60;
                            equipTableProps[1] = 20;
                            break;
                        case (int)ClassCareers.Assassin:
                            equipTableProps[0] = 80;
                            equipTableProps[1] = 40;
                            break;
                        case (int)ClassCareers.Monk:
                            equipTableProps[0] = 65;
                            equipTableProps[1] = 25;
                            break;
                        case (int)ClassCareers.Archer:
                            equipTableProps[0] = 75;
                            equipTableProps[1] = 35;
                            break;
                        case (int)ClassCareers.Ranger:
                            equipTableProps[0] = 70;
                            equipTableProps[1] = 30;
                            break;
                        case (int)ClassCareers.Barbarian:
                            equipTableProps[0] = 60;
                            equipTableProps[1] = 20;
                            break;
                        case (int)ClassCareers.Warrior:
                            equipTableProps[0] = 80;
                            equipTableProps[1] = 40;
                            break;
                        case (int)ClassCareers.Knight:
                            equipTableProps[0] = 85;
                            equipTableProps[1] = 45;
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
                            equipTableProps[0] = 70;
                            equipTableProps[1] = 30;
                            break;
                        case 8:
                            equipTableProps[0] = 55;
                            equipTableProps[1] = 15;
                            break;
                        case 12:
                            equipTableProps[0] = 75;
                            equipTableProps[1] = 35;
                            break;
                        case 15:
                            equipTableProps[0] = 45;
                            equipTableProps[1] = 5;
                            break;
                        case 17:
                            equipTableProps[0] = 45;
                            equipTableProps[1] = 5;
                            break;
                        case 21:
                            equipTableProps[0] = 60;
                            equipTableProps[1] = 20;
                            break;
                        case 23:
                            equipTableProps[0] = 50;
                            equipTableProps[1] = 10;
                            break;
                        case 24:
                            equipTableProps[0] = 80;
                            equipTableProps[1] = 40;
                            break;
                        case 25:
                            equipTableProps[0] = 55;
                            equipTableProps[1] = 15;
                            break;
                        case 26:
                            equipTableProps[0] = 55;
                            equipTableProps[1] = 15;
                            break;
                        case 27:
                            equipTableProps[0] = 55;
                            equipTableProps[1] = 15;
                            break;
                        case 29:
                            equipTableProps[0] = 65;
                            equipTableProps[1] = 25;
                            break;
                        case 31:
                            equipTableProps[0] = 75;
                            equipTableProps[1] = 35;
                            break;
                        case 28:
                        case 30:
                            equipTableProps[0] = 65;
                            equipTableProps[1] = 25;
                            break;
                        case 32:
                        case 33:
                            equipTableProps[0] = 60;
                            equipTableProps[1] = 20;
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
                targetItems.Add(ItemBuilder.CreateRandomIngredient(ingredientGroup));
                chance *= 0.5f;
            }
        }

        #endregion
    }
}
