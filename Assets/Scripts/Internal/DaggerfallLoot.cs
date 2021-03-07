// Project:         Daggerfall Tools For Unity
// Copyright:       Copyright (C) 2009-2020 Daggerfall Workshop
// Web Site:        http://www.dfworkshop.net
// License:         MIT License (http://www.opensource.org/licenses/mit-license.php)
// Source Code:     https://github.com/Interkarma/daggerfall-unity
// Original Author: Gavin Clayton (interkarma@dfworkshop.net)
// Contributors:    Allofich, Hazelnut
// 
// Notes:
//

using UnityEngine;
using DaggerfallWorkshop.Game;
using DaggerfallWorkshop.Game.Items;
using DaggerfallConnect;
using DaggerfallWorkshop.Utility;
using DaggerfallWorkshop.Game.Utility;
using DaggerfallWorkshop.Game.MagicAndEffects;
using DaggerfallWorkshop.Game.Formulas;
using DaggerfallConnect.FallExe;
using DaggerfallWorkshop.Game.Entity;
using System.Collections.Generic;

namespace DaggerfallWorkshop
{
    /// <summary>
    /// Enables a world object to be lootable by player.
    /// </summary>
    public class DaggerfallLoot : MonoBehaviour
    {
        // Dimension of random treasure marker in Daggerfall Units
        // Used to align random icon to surface marker is placed on
        public const int randomTreasureMarkerDim = 40;

        public WorldContext WorldContext = WorldContext.Nothing;
        public LootContainerTypes ContainerType = LootContainerTypes.Nothing;
        public InventoryContainerImages ContainerImage = InventoryContainerImages.Chest;
        public string entityName = string.Empty;
        public int TextureArchive = 0;
        public int TextureRecord = 0;
        public bool playerOwned = false;
        public bool houseOwned = false;
        public bool customDrop = false;         // Custom drop loot is not part of base scene and must be respawned on deserialization
        public bool isEnemyClass = false;
        public int stockedDate = 0;

        ulong loadID = 0;
        ItemCollection items = new ItemCollection();

        public ulong LoadID
        {
            get { return loadID; }
            set { loadID = value; }
        }

        public ItemCollection Items
        {
            get { return items; }
        }

        public static int CreateStockedDate(DaggerfallDateTime date)
        {
            return (date.Year * 1000) + date.DayOfYear;
        }

        /// <summary>
        /// Generates items in the given item collection based on loot table key.
        /// Any existing items will be destroyed.
        /// </summary>
        public static void GenerateEnemyItems(ItemCollection collection, int[] traits, EnemyEntity enemyEnt)
        {
            DaggerfallEntity enemy = enemyEnt as DaggerfallEntity;
            int[] enemyPredefLootTableProperties;
            int[] enemyExtraLootProperties;

            enemyPredefLootTableProperties = EnemyBasics.EnemyPredefLootTableCalculator(enemy, traits);
            enemyExtraLootProperties = EnemyBasics.EnemyExtraLootCalculator(enemy, traits, enemyPredefLootTableProperties);
            EnemyBasics.TraitExtraLootModCalculator(traits, enemyPredefLootTableProperties, enemyExtraLootProperties, out enemyPredefLootTableProperties, out enemyExtraLootProperties);

            DaggerfallUnityItem[] newitems = LootTables.GenerateEnemyLoot(enemy, traits, enemyPredefLootTableProperties, enemyExtraLootProperties);

            FormulaHelper.ModifyFoundLootItems(ref newitems);

            collection.Import(newitems);
        }

        /// <summary>
        /// Generates items in the given item collection based on loot table key.
        /// Any existing items will be destroyed.
        /// </summary>
        public static void GenerateDungeonItems(ItemCollection collection, int dungeonIndex)
        {
            DaggerfallUnityItem[] newitems = LootTables.GenerateDungeonLootItems(dungeonIndex);

            FormulaHelper.ModifyFoundLootItems(ref newitems);

            collection.Import(newitems);
        }

        /// <summary>
        /// Generates items in the given item collection based on loot table key.
        /// Any existing items will be destroyed.
        /// </summary>
        public static void GenerateBuildingItems(ItemCollection collection, int[] traits, EnemyEntity enemyEnt = null)
        {
            DaggerfallEntity enemy = enemyEnt as DaggerfallEntity; // This may not work as i'm expecting, will have to see.
            int[] enemyPredefLootTableProperties;
            int[] enemyExtraLootProperties;

            enemyPredefLootTableProperties = EnemyBasics.EnemyPredefLootTableCalculator(enemy, traits);
            enemyExtraLootProperties = EnemyBasics.EnemyExtraLootCalculator(enemy, traits, enemyPredefLootTableProperties);
            EnemyBasics.TraitExtraLootModCalculator(traits, enemyPredefLootTableProperties, enemyExtraLootProperties, out enemyPredefLootTableProperties, out enemyExtraLootProperties);
            // Now will start actually creating the items based on enemyPredefLootTableProperties and enemyExtraLootProperties, after that is done, likely add the "flavor" items from the traits afterward. 

            DaggerfallUnityItem[] newitems = LootTables.GenerateEnemyLoot(enemy, traits, enemyPredefLootTableProperties, enemyExtraLootProperties);

            FormulaHelper.ModifyFoundLootItems(ref newitems);

            collection.Import(newitems);
        }

        /// <summary>
        /// Randomly add a map
        /// </summary>
        public static void RandomlyAddMap(int chance, ItemCollection collection)
        {
            if (Dice100.SuccessRoll(chance))
            {
                DaggerfallUnityItem map = new DaggerfallUnityItem(ItemGroups.MiscItems, 8);
                collection.AddItem(map);
            }
        }

        /// <summary>
        /// Randomly add a potion
        /// </summary>
        public static void RandomlyAddPotion(int chance, ItemCollection collection)
        {
            if (Dice100.SuccessRoll(chance))
                collection.AddItem(ItemBuilder.CreateRandomPotion());
        }

        /// <summary>
        /// Randomly add a potion recipe
        /// </summary>
        public static void RandomlyAddPotionRecipe(int chance, ItemCollection collection)
        {
            if (Dice100.SuccessRoll(chance))
            {
                List<int> recipeKeys = GameManager.Instance.EntityEffectBroker.GetPotionRecipeKeys();
                int recipeIdx = UnityEngine.Random.Range(0, recipeKeys.Count);
                DaggerfallUnityItem potionRecipe = new DaggerfallUnityItem(ItemGroups.MiscItems, 4) { PotionRecipeKey = recipeKeys[recipeIdx] };
                collection.AddItem(potionRecipe);

                /*for (int i = 0; i < recipeKeys.Count; i++)  // This is simply here for a quick easy testing loop to see the potions and their prices in the Unity Debug window. 
                {
                    DaggerfallUnityItem printThis = new DaggerfallUnityItem(ItemGroups.MiscItems, 4) { PotionRecipeKey = recipeKeys[i] };

                    Debug.LogFormat("Potion Recipe ID: {0}, ||| Potion Name: {1}, ||| Potion Weight: {2}, ||| Potion Value: {3}", recipeKeys[i], printThis.LongName, printThis.weightInKg, printThis.value);
                }*/
            }
        }

        /// <summary>
        /// Randomly add a potion recipe
        /// </summary>
        public static void RandomlyAddPotionRecipe(int chance, List<DaggerfallUnityItem> targetItems)
        {
            if (Dice100.SuccessRoll(chance))
            {
                List<int> recipeKeys = GameManager.Instance.EntityEffectBroker.GetPotionRecipeKeys();
                int recipeIdx = UnityEngine.Random.Range(0, recipeKeys.Count);
                DaggerfallUnityItem potionRecipe = new DaggerfallUnityItem(ItemGroups.MiscItems, 4) { PotionRecipeKey = recipeKeys[recipeIdx] };
                targetItems.Add(potionRecipe);
            }
        }

        /// <summary>
        /// Called when this loot collection is opened by inventory window
        /// </summary>
        public void OnInventoryOpen()
        {
            //Debug.Log("Loot container opened.");
        }

        /// <summary>
        /// Called when this loot collection is closed by inventory window
        /// </summary>
        public void OnInventoryClose()
        {
            //Debug.Log("Loot container closed.");
        }


        public void StockShopShelf(PlayerGPS.DiscoveredBuilding buildingData)
        {
            stockedDate = CreateStockedDate(DaggerfallUnity.Instance.WorldTime.Now);
            items.Clear();

            DFLocation.BuildingTypes buildingType = buildingData.buildingType;
            int shopQuality = buildingData.quality;
            Game.Entity.PlayerEntity playerEntity = GameManager.Instance.PlayerEntity;
            int playerLuck = playerEntity.Stats.LiveLuck;
            ItemHelper itemHelper = DaggerfallUnity.Instance.ItemHelper;
            byte[] itemGroups = { 0 };

            switch (buildingType)
            {
                case DFLocation.BuildingTypes.Alchemist:
                    float alchChance = 60;
                    float alchChanceMod = 0.75f;
                    while (Dice100.SuccessRoll((int)alchChance))
                    {
                        RandomlyAddPotion(100, items);
                        alchChance *= alchChanceMod;
                    }
                    alchChance = 40;
                    while (Dice100.SuccessRoll((int)alchChance))
                    {
                        RandomlyAddPotionRecipe(100, items);
                        alchChance *= alchChanceMod;
                    }
                    itemGroups = DaggerfallLootDataTables.itemGroupsAlchemist;
                    break;
                case DFLocation.BuildingTypes.Armorer:
                    itemGroups = DaggerfallLootDataTables.itemGroupsArmorer;
                    break;
                case DFLocation.BuildingTypes.Bookseller:
                    itemGroups = DaggerfallLootDataTables.itemGroupsBookseller;
                    break;
                case DFLocation.BuildingTypes.ClothingStore:
                    itemGroups = DaggerfallLootDataTables.itemGroupsClothingStore;
                    break;
                case DFLocation.BuildingTypes.GemStore:
                    itemGroups = DaggerfallLootDataTables.itemGroupsGemStore;
                    break;
                case DFLocation.BuildingTypes.GeneralStore:
                    itemGroups = DaggerfallLootDataTables.itemGroupsGeneralStore;
                    if (Dice100.SuccessRoll(20))
                        items.AddItem(ItemBuilder.CreateItem(ItemGroups.Transportation, (int)Transportation.Horse));
                    if (Dice100.SuccessRoll(30))
                        items.AddItem(ItemBuilder.CreateItem(ItemGroups.Transportation, (int)Transportation.Small_cart));
                    break;
                case DFLocation.BuildingTypes.PawnShop:
                    itemGroups = DaggerfallLootDataTables.itemGroupsPawnShop;
                    break;
                case DFLocation.BuildingTypes.WeaponSmith:
                    itemGroups = DaggerfallLootDataTables.itemGroupsWeaponSmith;
                    break;
            }

            for (int i = 0; i < itemGroups.Length; i += 2) // Alright, that makes more sense to me now at least, from what it seems at least. Odd values are the itemGroup, Even are the chance for that itemGroup, makes much more sense. 
            {
                ItemGroups itemGroup = (ItemGroups)itemGroups[i];
                float chance = itemGroups[i + 1];
                float chanceMod = 0.45f;

                if (itemGroup != ItemGroups.Furniture && itemGroup != ItemGroups.UselessItems1)
                {
                    while (Dice100.SuccessRoll((int)chance)) // I think order will be, roll if item of group is generated, then actually pick the item of said group randomly and then continue from there until loop roll fails. 
                    {
                        DaggerfallUnityItem item = null; // Don't forget to have weapons and armor and such have variable condition values depending on the quality of the store they were bought/generated in.
                        chanceMod = Mathf.Clamp(chanceMod + 0.05f, 0.10f, 0.85f); // Will likely have to tweak this around and see how the results are. Possibly have shop quality modify these chance values as well. 
                        if (itemGroup == ItemGroups.Weapons)
                        {
                            item = ItemBuilder.CreateWeapon((Weapons)Random.Range((int)Weapons.Dagger, (int)Weapons.Arrow + 1), FormulaHelper.RandomMaterial(-1, shopQuality, playerLuck)); // May rework and give weapons different rarity values later.
                        }
                        else if (itemGroup == ItemGroups.Armor)
                        {
                            item = ItemBuilder.CreateArmor(playerEntity.Gender, playerEntity.Race, (Armor)Random.Range((int)Armor.Cuirass, (int)Armor.Tower_Shield + 1), FormulaHelper.RandomArmorMaterial(-1, shopQuality, playerLuck));
                        }
                        else if (itemGroup == ItemGroups.MensClothing)
                        {
                            item = ItemBuilder.CreateMensClothing((MensClothing)Random.Range((int)MensClothing.Straps, (int)MensClothing.Champion_straps + 1), playerEntity.Race);
                            item.dyeColor = ItemBuilder.RandomClothingDye();
                        }
                        else if (itemGroup == ItemGroups.WomensClothing)
                        {
                            item = ItemBuilder.CreateWomensClothing((WomensClothing)Random.Range((int)WomensClothing.Brassier, (int)WomensClothing.Vest + 1), playerEntity.Race);
                            item.dyeColor = ItemBuilder.RandomClothingDye();
                        }
                        else if (itemGroup == ItemGroups.Books)
                        {
                            item = ItemBuilder.CreateRandomBookOfRandomSubject(-1, shopQuality, playerLuck);
                        }
                        else if (itemGroup == ItemGroups.MagicItems)
                        {
                            item = ItemBuilder.CreateRandomMagicItem(playerEntity.Gender, playerEntity.Race, -1, shopQuality, playerLuck);
                        }
                        else if ((int)itemGroup == (int)ItemGroups.Jewellery || ((int)itemGroup >= (int)ItemGroups.Tiara_Jewelry && (int)itemGroup <= (int)ItemGroups.Bracelet_Jewelry))
                        {
                            item = ItemBuilder.CreateRandomJewelryOfRandomSlot(-1, shopQuality, playerLuck);
                        }
                        else
                        {
                            item = ItemBuilder.CreateRandomItemOfItemgroup(itemGroup, -1, shopQuality, playerLuck);
                            if (DaggerfallUnity.Settings.PlayerTorchFromItems && item.IsOfTemplate(ItemGroups.UselessItems2, (int)UselessItems2.Oil))
                                item.stackCount = Random.Range(5, 20 + 1);  // Shops stock 5-20 bottles
                        }
                        items.AddItem(item);

                        chance *= chanceMod; // Likely determine chanceMod by the itemGroup being currently ran. 
                    }

                    // Add any modded items registered in applicable groups
                    int[] customItemTemplates = itemHelper.GetCustomItemsForGroup(itemGroup);
                    for (int j = 0; j < customItemTemplates.Length; j++)
                    {
                        ItemTemplate itemTemplate = itemHelper.GetItemTemplate(itemGroup, customItemTemplates[j]);
                        if (itemTemplate.rarity <= shopQuality)
                        {
                            int stockChance = (int)Mathf.Round(chance * 5 * (21 - itemTemplate.rarity) / 100);
                            if (Dice100.SuccessRoll(stockChance))
                            {
                                DaggerfallUnityItem item = ItemBuilder.CreateItem(itemGroup, customItemTemplates[j]);

                                // Setup specific group stats
                                if (itemGroup == ItemGroups.Weapons)
                                {
                                    WeaponMaterialTypes material = FormulaHelper.RandomMaterial(-1, shopQuality, playerLuck);
                                    ItemBuilder.ApplyWeaponMaterial(item, material);
                                }
                                else if (itemGroup == ItemGroups.Armor)
                                {
                                    ArmorMaterialTypes material = FormulaHelper.RandomArmorMaterial(-1, shopQuality, playerLuck);
                                    ItemBuilder.ApplyArmorSettings(item, playerEntity.Gender, playerEntity.Race, material);
                                }
                                else if (item.TemplateIndex == 810)
                                {
                                    WeaponMaterialTypes material = FormulaHelper.RandomMaterial(-1, shopQuality, playerLuck);
                                    ItemBuilder.ApplyIngotMaterial(item, material);
                                }

                                items.AddItem(item);
                            }
                        }
                    }
                }
            }
        }

        public void StockHouseContainer(PlayerGPS.DiscoveredBuilding buildingData)
        {
            stockedDate = CreateStockedDate(DaggerfallUnity.Instance.WorldTime.Now);
            items.Clear();

            DFLocation.BuildingTypes buildingType = buildingData.buildingType;
            uint modelIndex = (uint) TextureRecord;
            int buildingQuality = buildingData.quality;
            byte[] privatePropertyList = null;
            DaggerfallUnityItem item = null;
            Game.Entity.PlayerEntity playerEntity = GameManager.Instance.PlayerEntity;
            int playerLuck = playerEntity.Stats.LiveLuck;

            if (buildingType < DFLocation.BuildingTypes.House5)
            {
                if (modelIndex >= 2)
                {
                    if (modelIndex >= 4)
                    {
                        if (modelIndex >= 11)
                        {
                            if (modelIndex >= 15)
                            {
                                privatePropertyList = DaggerfallLootDataTables.privatePropertyItemsModels15AndUp[(int)buildingType];
                            }
                            else
                            {
                                privatePropertyList = DaggerfallLootDataTables.privatePropertyItemsModels11to14[(int)buildingType];
                            }
                        }
                        else
                        {
                            privatePropertyList = DaggerfallLootDataTables.privatePropertyItemsModels4to10[(int)buildingType];
                        }
                    }
                    else
                    {
                        privatePropertyList = DaggerfallLootDataTables.privatePropertyItemsModels2to3[(int)buildingType];
                    }
                }
                else
                {
                    privatePropertyList = DaggerfallLootDataTables.privatePropertyItemsModels0to1[(int)buildingType];
                }
                if (privatePropertyList == null)
                    return;
                int randomChoice = Random.Range(0, privatePropertyList.Length);
                ItemGroups itemGroup = (ItemGroups)privatePropertyList[randomChoice];
                int continueChance = 100;
                bool keepGoing = true;
                while (keepGoing)
                {
                    if (itemGroup != ItemGroups.MensClothing && itemGroup != ItemGroups.WomensClothing)
                    {
                        if (itemGroup == ItemGroups.MagicItems)
                        {
                            item = ItemBuilder.CreateRandomMagicItem(playerEntity.Gender, playerEntity.Race, -1, buildingQuality, playerLuck);
                        }
                        else if (itemGroup == ItemGroups.Books)
                        {
                            item = ItemBuilder.CreateRandomBook();
                        }
                        else
                        {
                            if (itemGroup == ItemGroups.Weapons)
                                item = ItemBuilder.CreateRandomWeapon(-1, buildingQuality, playerLuck);
                            else if (itemGroup == ItemGroups.Armor)
                                item = ItemBuilder.CreateRandomArmor(playerEntity.Gender, playerEntity.Race, -1, buildingQuality, playerLuck);
                            else
                            {
                                System.Array enumArray = DaggerfallUnity.Instance.ItemHelper.GetEnumArray(itemGroup);
                                item = new DaggerfallUnityItem(itemGroup, Random.Range(0, enumArray.Length));
                            }
                        }
                    }
                    else
                    {
                        item = ItemBuilder.CreateRandomClothing(playerEntity.Gender, playerEntity.Race);
                    }
                    continueChance >>= 1;
                    if (DFRandom.rand() % 100 > continueChance)
                        keepGoing = false;
                    items.AddItem(item);
                }
            }
        }
    }
}