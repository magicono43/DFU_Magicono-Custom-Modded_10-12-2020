// Project:         Daggerfall Tools For Unity
// Copyright:       Copyright (C) 2009-2020 Daggerfall Workshop
// Web Site:        http://www.dfworkshop.net
// License:         MIT License (http://www.opensource.org/licenses/mit-license.php)
// Source Code:     https://github.com/Interkarma/daggerfall-unity
// Original Author: Gavin Clayton (interkarma@dfworkshop.net)
// Contributors: InconsolableCellist
//
// Notes:
//

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DaggerfallWorkshop.Game.Entity;
using DaggerfallConnect.FallExe;
using DaggerfallConnect.Arena2;
using DaggerfallWorkshop.Utility;
using DaggerfallWorkshop.Utility.AssetInjection;
using DaggerfallWorkshop.Game.Utility;
using DaggerfallWorkshop.Game.Formulas;

namespace DaggerfallWorkshop.Game.Items
{
    /// <summary>
    /// Generates new items for various game systems.
    /// This helper still under development.
    /// </summary>
    public static class ItemBuilder
    {
        #region Data

        public const int firstFemaleArchive = 245;
        public const int firstMaleArchive = 249;
        private const int chooseAtRandom = -1;

        // This array is used to pick random material values.
        // The array is traversed, subtracting each value from a sum until the sum is less than the next value.
        // Steel through Daedric, or Iron if sum is less than the first value.
        //                                  Iron, Steel, Silver, Elven, Dwarven, Mithril, Adamantium, Ebony, Orcish, Daedric.
        public static readonly short[] materialsByRarity = { 70, 45, 30, 25, 8, 15, 3, 5, 10, 2 };
        //public static readonly short[] materialsByRarity = { 20, 18, 16, 14, 8, 12, 4, 6, 10, 2 };
        //public static readonly short[] materialsByRarity = { 4, 6, 8, 10, 20, 14, 32, 24, 16, 52 };

        // Weight multipliers by material type. Iron through Daedric. Weight is baseWeight * value / 4.
        //static readonly short[] weightMultipliersByMaterial = { 4, 5, 4, 4, 3, 4, 4, 2, 4, 5 };
        public static readonly short[] weightMultipliersByMaterial = { 8, 10, 5, 4, 8, 5, 6, 3, 12, 7, 2, 3 }; // Added two values to the end here, for leather and chain.

        // Value multipliers by material type. Iron through Daedric. Value is baseValue * ( 3 * value).
        //static readonly short[] valueMultipliersByMaterial = { 1, 2, 4, 8, 16, 32, 64, 128, 256, 512 };
        static readonly short[] valueMultipliersByMaterial = { 2, 5, 10, 20, 50, 30, 70, 60, 40, 140 };

        // Condition multipliers by material type. Iron through Daedric. MaxCondition is baseMaxCondition * value / 4.
        //static readonly short[] conditionMultipliersByMaterial = { 4, 6, 6, 8, 12, 16, 20, 24, 28, 32 };
        static readonly short[] conditionMultipliersByMaterial = { 4, 6, 2, 2, 4, 4, 6, 3, 9, 6 };

        // Blunt
        static readonly short[] densityMultipliersByMaterial = { 400, 500, 250, 200, 400, 250, 300, 150, 600, 350 };

        // Slashing/Cutting
        static readonly short[] shearMultipliersByMaterial = { 150, 200, 200, 400, 200, 500, 350, 600, 250, 500 };

        // Piercing
        static readonly short[] fractureMultipliersByMaterial = { 200, 250, 400, 350, 500, 300, 600, 200, 100, 450 };

        // Extra condition/damage from fire if low
        static readonly short[] meltingPointMultipliersByMaterial = { 175, 200, 75, 100, 50, 125, 25, 150, 225, 350 };

        // Extra damage from electrical attacks if high
        static readonly short[] conductivityMultipliersByMaterial = { 100, 150, 225, 50, 200, 25, 175, 75, 125, 100 };

        // Extra condition/damage from ice attacks when high
        static readonly short[] brittlenessMultipliersByMaterial = { 200, 225, 50, 100, 75, 150, 25, 125, 175, 200 };

        // Enchantment point/gold value data for item powers
        static readonly int[] extraSpellPtsEnchantPts = { 0x1F4, 0x1F4, 0x1F4, 0x1F4, 0xC8, 0xC8, 0xC8, 0x2BC, 0x320, 0x384, 0x3E8 };
        static readonly int[] potentVsEnchantPts = { 0x320, 0x384, 0x3E8, 0x4B0 };
        static readonly int[] regensHealthEnchantPts = { 0x0FA0, 0x0BB8, 0x0BB8 };
        static readonly int[] vampiricEffectEnchantPts = { 0x7D0, 0x3E8 };
        static readonly int[] increasedWeightAllowanceEnchantPts = { 0x190, 0x258 };
        static readonly int[] improvesTalentsEnchantPts = { 0x1F4, 0x258, 0x258 };
        static readonly int[] goodRepWithEnchantPts = { 0x3E8, 0x3E8, 0x3E8, 0x3E8, 0x3E8, 0x1388 };
        static readonly int[][] enchantmentPtsForItemPowerArrays = { null, null, null, extraSpellPtsEnchantPts, potentVsEnchantPts, regensHealthEnchantPts,
                                                                    vampiricEffectEnchantPts, increasedWeightAllowanceEnchantPts, null, null, null, null, null,
                                                                    improvesTalentsEnchantPts, goodRepWithEnchantPts};
        static readonly ushort[] enchantmentPointCostsForNonParamTypes = { 0, 0x0F448, 0x0F63C, 0x0FF9C, 0x0FD44, 0, 0, 0, 0x384, 0x5DC, 0x384, 0x64, 0x2BC };

        public enum BodyMorphology
        {
            Argonian = 0,
            Elf = 1,
            Human = 2,
            Khajiit = 3,
        }

        public static DyeColors[] clothingDyes = {
            DyeColors.Blue,
            DyeColors.Grey,
            DyeColors.Red,
            DyeColors.DarkBrown,
            DyeColors.Purple,
            DyeColors.LightBrown,
            DyeColors.White,
            DyeColors.Aquamarine,
            DyeColors.Yellow,
            DyeColors.Green,
        };

        #endregion

        #region Public Methods

        public static DyeColors RandomClothingDye()
        {
            return clothingDyes[UnityEngine.Random.Range(0, clothingDyes.Length)];
        }

        /// <summary>
        /// Creates a generic item from group and template index.
        /// </summary>
        /// <param name="itemGroup">Item group.</param>
        /// <param name="templateIndex">Template index.</param>
        /// <returns>DaggerfallUnityItem.</returns>
        public static DaggerfallUnityItem CreateItem(ItemGroups itemGroup, int templateIndex)
        {
            // Handle custom items
            if (templateIndex > ItemHelper.LastDFTemplate)
            {
                // Allow custom item classes to be instantiated when registered
                Type itemClassType;
                if (DaggerfallUnity.Instance.ItemHelper.GetCustomItemClass(templateIndex, out itemClassType))
                    return (DaggerfallUnityItem)Activator.CreateInstance(itemClassType);
                else
                    return new DaggerfallUnityItem(itemGroup, templateIndex);
            }

            // Create classic item
            int groupIndex = DaggerfallUnity.Instance.ItemHelper.GetGroupIndex(itemGroup, templateIndex);
            if (groupIndex == -1)
            {
                Debug.LogErrorFormat("ItemBuilder.CreateItem() encountered an item with an invalid GroupIndex. Check you're passing 'template index' matching a value in ItemEnums - e.g. (int)Weapons.Dagger NOT a 'group index' (e.g. 0).");
                return null;
            }
            DaggerfallUnityItem newItem = new DaggerfallUnityItem(itemGroup, groupIndex);

            return newItem;
        }

        /// <summary>
        /// Generates men's clothing.
        /// </summary>
        /// <param name="item">Item type to generate.</param>
        /// <param name="race">Race of player.</param>
        /// <param name="variant">Variant to use. If not set, a random variant will be selected.</param>
        /// <param name="dye">Dye to use</param>
        /// <returns>DaggerfallUnityItem.</returns>
        public static DaggerfallUnityItem CreateMensClothing(MensClothing item, Races race, int variant = -1, int minCond = -1, int maxCond = -1, DyeColors dye = DyeColors.Blue)
        {
            // Create item
            int groupIndex = DaggerfallUnity.Instance.ItemHelper.GetGroupIndex(ItemGroups.MensClothing, (int)item);
            DaggerfallUnityItem newItem = new DaggerfallUnityItem(ItemGroups.MensClothing, groupIndex);

            // Random variant
            if (variant < 0)
                variant = UnityEngine.Random.Range(0, newItem.ItemTemplate.variants);

            // Set race, variant, dye
            SetRace(newItem, race);
            SetVariant(newItem, variant);
            newItem.dyeColor = dye;

            if (minCond > -1 && maxCond > -1)
            {
                float condPercentMod = UnityEngine.Random.Range(minCond, maxCond + 1) / 100f;
                newItem.currentCondition = (int)Mathf.Floor(newItem.maxCondition * condPercentMod);
            }

            return newItem;
        }

        /// <summary>
        /// Generates women's clothing.
        /// </summary>
        /// <param name="item">Item type to generate.</param>
        /// <param name="race">Race of player.</param>
        /// <param name="variant">Variant to use. If not set, a random variant will be selected.</param>
        /// <param name="dye">Dye to use</param>
        /// <returns>DaggerfallUnityItem.</returns>
        public static DaggerfallUnityItem CreateWomensClothing(WomensClothing item, Races race, int variant = -1, int minCond = -1, int maxCond = -1, DyeColors dye = DyeColors.Blue)
        {
            // Create item
            int groupIndex = DaggerfallUnity.Instance.ItemHelper.GetGroupIndex(ItemGroups.WomensClothing, (int)item);
            DaggerfallUnityItem newItem = new DaggerfallUnityItem(ItemGroups.WomensClothing, groupIndex);

            // Random variant
            if (variant < 0)
                variant = UnityEngine.Random.Range(0, newItem.ItemTemplate.variants);

            // Set race, variant, dye
            SetRace(newItem, race);
            SetVariant(newItem, variant);
            newItem.dyeColor = dye;

            if (minCond > -1 && maxCond > -1)
            {
                float condPercentMod = UnityEngine.Random.Range(minCond, maxCond + 1) / 100f;
                newItem.currentCondition = (int)Mathf.Floor(newItem.maxCondition * condPercentMod);
            }

            return newItem;
        }

        /// <summary>
        /// Creates a new item of random clothing.
        /// </summary>
        /// <param name="gender">Gender of player</param>
        /// <returns>DaggerfallUnityItem.</returns>
        public static DaggerfallUnityItem CreateRandomClothing(Genders gender, Races race)
        {
            // Create random clothing by gender, including any custom items registered as clothes
            ItemGroups genderClothingGroup = (gender == Genders.Male) ? ItemGroups.MensClothing : ItemGroups.WomensClothing;

            ItemHelper itemHelper = DaggerfallUnity.Instance.ItemHelper;
            Array enumArray = itemHelper.GetEnumArray(genderClothingGroup);
            int[] customItemTemplates = itemHelper.GetCustomItemsForGroup(genderClothingGroup);

            int groupIndex = UnityEngine.Random.Range(0, enumArray.Length + customItemTemplates.Length);
            DaggerfallUnityItem newItem;
            if (groupIndex < enumArray.Length)
                newItem = new DaggerfallUnityItem(genderClothingGroup, groupIndex);
            else
                newItem = CreateItem(genderClothingGroup, customItemTemplates[groupIndex - enumArray.Length]);

            SetRace(newItem, race);

            // Random dye colour
            newItem.dyeColor = RandomClothingDye();

            // Random variant
            SetVariant(newItem, UnityEngine.Random.Range(0, newItem.TotalVariants));

            return newItem;
        }

        /// <summary>
        /// Creates a new shirt item of random variant.
        /// </summary>
        public static DaggerfallUnityItem CreateRandomShirt(Genders gender, Races race, int minCond = -1, int maxCond = -1)
        {
            DaggerfallUnityItem newItem;

            if (gender == Genders.Male)
            {
                newItem = CreateMensClothing((MensClothing)FormulaHelper.PickOneOf((int)MensClothing.Kimono, (int)MensClothing.Short_shirt_with_belt, (int)MensClothing.Short_shirt, (int)MensClothing.Dwynnen_surcoat,
                    (int)MensClothing.Short_tunic, (int)MensClothing.Formal_tunic, (int)MensClothing.Toga, (int)MensClothing.Reversible_tunic, (int)MensClothing.Long_shirt, (int)MensClothing.Long_shirt_with_belt,
                    (int)MensClothing.Short_shirt_closed_top, (int)MensClothing.Short_shirt_closed_top2, (int)MensClothing.Long_shirt_closed_top, (int)MensClothing.Long_shirt_closed_top2, (int)MensClothing.Open_Tunic,
                    (int)MensClothing.Anticlere_Surcoat, (int)MensClothing.Short_shirt_unchangeable, (int)MensClothing.Long_shirt_unchangeable, (int)MensClothing.Vest, (int)MensClothing.Eodoric), race, -1, minCond, maxCond);
            }
            else
            {
                newItem = CreateWomensClothing((WomensClothing)FormulaHelper.PickOneOf((int)WomensClothing.Peasant_blouse, (int)WomensClothing.Eodoric, (int)WomensClothing.Formal_eodoric, (int)WomensClothing.Casual_dress,
                    (int)WomensClothing.Strapless_dress, (int)WomensClothing.Short_shirt, (int)WomensClothing.Short_shirt_belt, (int)WomensClothing.Long_shirt, (int)WomensClothing.Long_shirt_belt,
                    (int)WomensClothing.Short_shirt_closed, (int)WomensClothing.Short_shirt_closed_belt, (int)WomensClothing.Short_shirt_unchangeable, (int)WomensClothing.Long_shirt_closed,
                    (int)WomensClothing.Long_shirt_closed_belt, (int)WomensClothing.Long_shirt_unchangeable, (int)WomensClothing.Open_tunic, (int)WomensClothing.Vest), race, -1, minCond, maxCond);
            }

            //SetRace(newItem, race);

            // Random dye colour
            //newItem.dyeColor = RandomClothingDye();

            // Random variant
            //SetVariant(newItem, UnityEngine.Random.Range(0, newItem.TotalVariants));

            return newItem;
        }

        /// <summary>
        /// Creates a new pants item of random variant.
        /// </summary>
        public static DaggerfallUnityItem CreateRandomPants(Genders gender, Races race, int minCond = -1, int maxCond = -1)
        {
            DaggerfallUnityItem newItem;

            if (gender == Genders.Male)
            {
                newItem = CreateMensClothing((MensClothing)FormulaHelper.PickOneOf((int)MensClothing.Casual_pants, (int)MensClothing.Breeches, (int)MensClothing.Short_skirt, (int)MensClothing.Long_Skirt,
                    (int)MensClothing.Loincloth, (int)MensClothing.Wrap), race, -1, minCond, maxCond);
            }
            else
            {
                newItem = CreateWomensClothing((WomensClothing)FormulaHelper.PickOneOf((int)WomensClothing.Casual_pants, (int)WomensClothing.Loincloth, (int)WomensClothing.Wrap, (int)WomensClothing.Long_skirt,
                    (int)WomensClothing.Tights), race, -1, minCond, maxCond);
            }

            //SetRace(newItem, race);

            // Random dye colour
            //newItem.dyeColor = RandomClothingDye();

            // Random variant
            //SetVariant(newItem, UnityEngine.Random.Range(0, newItem.TotalVariants));

            return newItem;
        }

        /// <summary>
        /// Creates a new shoes item of random variant.
        /// </summary>
        public static DaggerfallUnityItem CreateRandomShoes(Genders gender, Races race, int minCond = -1, int maxCond = -1)
        {
            DaggerfallUnityItem newItem;

            if (gender == Genders.Male)
            {
                newItem = CreateMensClothing((MensClothing)FormulaHelper.PickOneOf((int)MensClothing.Shoes, (int)MensClothing.Sandals, (int)MensClothing.Boots, (int)MensClothing.Tall_Boots), race, -1, minCond, maxCond);
            }
            else
            {
                newItem = CreateWomensClothing((WomensClothing)FormulaHelper.PickOneOf((int)WomensClothing.Shoes, (int)WomensClothing.Sandals, (int)WomensClothing.Boots, (int)WomensClothing.Tall_boots), race, -1, minCond, maxCond);
            }

            //SetRace(newItem, race);

            // Random dye colour
            //newItem.dyeColor = RandomClothingDye();

            // Random variant
            //SetVariant(newItem, UnityEngine.Random.Range(0, newItem.TotalVariants));

            return newItem;
        }

        /// <summary>
        /// Creates a new bra item of random variant.
        /// </summary>
        public static DaggerfallUnityItem CreateRandomBra(Genders gender, Races race, int minCond = -1, int maxCond = -1)
        {
            DaggerfallUnityItem newItem;

            newItem = CreateWomensClothing((WomensClothing)FormulaHelper.PickOneOfCompact((int)WomensClothing.Brassier, 5, (int)WomensClothing.Formal_brassier, 1), race, -1, minCond, maxCond);

            //SetRace(newItem, race);

            // Random dye colour
            //newItem.dyeColor = RandomClothingDye();

            // Random variant
            //SetVariant(newItem, UnityEngine.Random.Range(0, newItem.TotalVariants));

            return newItem;
        }

        /// <summary>
        /// Creates a new book.
        /// </summary>
        /// <param name="fileName">The name of the books resource.</param>
        /// <returns>An instance of the book item or null.</returns>
        public static DaggerfallUnityItem CreateBook(string fileName)
        {
            if (!Path.HasExtension(fileName))
                fileName += ".TXT";

            var entry = BookReplacement.BookMappingEntries.Values.FirstOrDefault(x => x.Name.Equals(fileName, StringComparison.Ordinal));
            if (entry.ID != 0)
                return CreateBook(entry.ID);

            int id;
            if (fileName.Length == 12 && fileName.StartsWith("BOK") && int.TryParse(fileName.Substring(3, 5), out id))
                return CreateBook(id);

            return null;
        }

        /// <summary>
        /// Creates a new book.
        /// </summary>
        /// <param name="id">The numeric id of book resource.</param>
        /// <returns>An instance of the book item or null.</returns>
        public static DaggerfallUnityItem CreateBook(int id)
        {
            var bookFile = new BookFile();

            float[] bookProps = CustomBookPropertyHolder.GetBookProperties(id);
            string name = GameManager.Instance.ItemHelper.GetBookFileName(id);
            if (!BookReplacement.TryImportBook(name, bookFile) &&
                !bookFile.OpenBook(DaggerfallUnity.Instance.Arena2Path, name))
                return null;

            DaggerfallUnityItem bookMade = new DaggerfallUnityItem(ItemGroups.Books, 0)
            {
                message = id,
                value = (int)bookProps[0],
                rarity = (int)bookProps[1],
                weightInKg = bookProps[2],
                CurrentVariant = (int)bookProps[3]
            };

            //bookMade.CurrentVariant = UnityEngine.Random.Range(0, bookMade.TotalVariants);

            return bookMade;
        }

        public static DaggerfallUnityItem CreateDummyBook(int id)
        {
            var bookFile = new BookFile();

            float[] bookProps = CustomBookPropertyHolder.GetBookProperties(id);
            string name = GameManager.Instance.ItemHelper.GetBookFileName(id);
            if (!BookReplacement.TryImportBook(name, bookFile) &&
                !bookFile.OpenBook(DaggerfallUnity.Instance.Arena2Path, name))
                return null;

            DaggerfallUnityItem bookMade = new DaggerfallUnityItem(ItemGroups.Books, 0, true)
            {
                message = id,
                value = (int)bookProps[0],
                rarity = (int)bookProps[1],
                weightInKg = bookProps[2],
                CurrentVariant = (int)bookProps[3]
            };

            //bookMade.CurrentVariant = UnityEngine.Random.Range(0, bookMade.TotalVariants);

            return bookMade;
        }

        /// <summary>
        /// Creates a new random book
        /// </summary>
        /// <returns>DaggerfallUnityItem.</returns>
        public static DaggerfallUnityItem CreateRandomBook()
        {
            /*for (int i = 0; i < 120; i++) // This is simply here for a quick easy testing loop to see the books and their prices in the Unity Debug window. 
            {
                try
                {
                    var bookFound = new BookFile();

                    int id = i;
                    string title = GameManager.Instance.ItemHelper.GetBookFileName(id);
                    if (!BookReplacement.TryImportBook(title, bookFound) &&
                        !bookFound.OpenBook(DaggerfallUnity.Instance.Arena2Path, title))
                        return null;

                    DaggerfallUnityItem printThis = new DaggerfallUnityItem(ItemGroups.Books, 0)
                    {
                        message = id,
                        value = bookFound.Price
                    };

                    Debug.LogFormat("Book ID: {0}, Book Title: {1}, Page Count: {2}, Value: {3}", id, bookFound.Title, bookFound.PageCount, bookFound.Price);
                }
                catch
                {
                    Debug.LogFormat("Book ID: {0} does not exist.", i);
                }
            }*/

            Array enumArray = DaggerfallUnity.Instance.ItemHelper.GetEnumArray(ItemGroups.Books);
            DaggerfallUnityItem book = new DaggerfallUnityItem(ItemGroups.Books, Array.IndexOf(enumArray, Books.Book0));
            book.message = DaggerfallUnity.Instance.ItemHelper.GetRandomBookID();
            //book.CurrentVariant = UnityEngine.Random.Range(0, book.TotalVariants);
            // Update item value for this book.
            BookFile bookFile = new BookFile();
            float[] bookProps = CustomBookPropertyHolder.GetBookProperties(book.message);
            string name = GameManager.Instance.ItemHelper.GetBookFileName(book.message);
            if (!BookReplacement.TryImportBook(name, bookFile))
                bookFile.OpenBook(DaggerfallUnity.Instance.Arena2Path, name);
            book.value = (int)bookProps[0];
            book.rarity = (int)bookProps[1];
            book.weightInKg = bookProps[2];
            book.CurrentVariant = (int)bookProps[3];

            return book;
        }

        /// <summary>
        /// Creates a random book from any of the subject groups.
        /// Passing a non-book subject group will return null.
        /// </summary>
        /// <returns>DaggerfallUnityItem</returns>
        public static DaggerfallUnityItem CreateRandomBookOfSpecificSubject(ItemGroups bookSubject, int enemyLevel = -1, int playerLuck = -1)
        {
            List<DaggerfallUnityItem> booksList = new List<DaggerfallUnityItem>();
            Array enumArray;
            int bookID;
            switch (bookSubject)
            {
                case ItemGroups.Biography_Books:
                case ItemGroups.Faction_Related_Books:
                case ItemGroups.Fiction_Books:
                case ItemGroups.History_Lore_Books:
                case ItemGroups.Humor_Books:
                case ItemGroups.Instruction_Research_Books:
                case ItemGroups.Journals_Logs_Books:
                case ItemGroups.Notes_Letters_Books:
                case ItemGroups.Plays_Poetry_Riddles_Books:
                case ItemGroups.Politics_Law_Books:
                case ItemGroups.Religion_Prophecy_Books:
                case ItemGroups.Travel_Books:
                case ItemGroups.Informational_Books:
                case ItemGroups.No_Topic_Books:
                    enumArray = DaggerfallUnity.Instance.ItemHelper.GetEnumArray(bookSubject);
                    for (int i = 0; i < enumArray.Length; i++)
                    {
                        bookID = (int)enumArray.GetValue(i);
                        DaggerfallUnityItem bookChecked = CreateDummyBook(bookID);
                        booksList.Add(bookChecked);
                    }
                    break;
                default:
                    return null;
            }

            int chosenBookID = ChooseBookFromFilteredList(booksList, enemyLevel, playerLuck);

            // Create item
            DaggerfallUnityItem book = CreateBook(chosenBookID);

            return book;
        }

        /// <summary>
        /// Creates a random book of a random subject group.
        /// </summary>
        /// <returns>DaggerfallUnityItem</returns>
        public static DaggerfallUnityItem CreateRandomBookOfRandomSubject(int enemyLevel = -1, int playerLuck = -1)
        {
            List<DaggerfallUnityItem> booksList = new List<DaggerfallUnityItem>();
            ItemGroups itemGroup;
            Array enumArray;
            int bookID;
            int group = UnityEngine.Random.Range(0, 12); // Keeping this range to 12 for now, until I actually add books to the "Informational and no_topic" subjects, otherwise will just give a null. 
            switch (group)
            {
                case 0:
                    itemGroup = ItemGroups.Biography_Books;
                    break;
                case 1:
                    itemGroup = ItemGroups.Faction_Related_Books;
                    break;
                case 2:
                    itemGroup = ItemGroups.Fiction_Books;
                    break;
                case 3:
                    itemGroup = ItemGroups.History_Lore_Books;
                    break;
                case 4:
                    itemGroup = ItemGroups.Humor_Books;
                    break;
                case 5:
                    itemGroup = ItemGroups.Instruction_Research_Books;
                    break;
                case 6:
                    itemGroup = ItemGroups.Journals_Logs_Books;
                    break;
                case 7:
                    itemGroup = ItemGroups.Notes_Letters_Books;
                    break;
                case 8:
                    itemGroup = ItemGroups.Plays_Poetry_Riddles_Books;
                    break;
                case 9:
                    itemGroup = ItemGroups.Politics_Law_Books;
                    break;
                case 10:
                    itemGroup = ItemGroups.Religion_Prophecy_Books;
                    break;
                case 11:
                    itemGroup = ItemGroups.Travel_Books;
                    break;
                case 12:
                    itemGroup = ItemGroups.Informational_Books;
                    break;
                case 13:
                    itemGroup = ItemGroups.No_Topic_Books;
                    break;
                default:
                    return null;
            }

            // Randomise book within group
            enumArray = DaggerfallUnity.Instance.ItemHelper.GetEnumArray(itemGroup);
            for (int i = 0; i < enumArray.Length; i++)
            {
                bookID = (int)enumArray.GetValue(i);
                DaggerfallUnityItem bookChecked = CreateDummyBook(bookID);
                booksList.Add(bookChecked);
            }

            int chosenBookID = ChooseBookFromFilteredList(booksList, enemyLevel, playerLuck);

            // Create item
            DaggerfallUnityItem book = CreateBook(chosenBookID);

            return book;
        }

        public static int ChooseBookFromFilteredList(List<DaggerfallUnityItem> bookList, int enemyLevel = -1, int playerLuck = -1)
        {
            int[] bookRolls = new int[] { };
            List<int> bookRollsList = new List<int>();

            for (int i = 0; i < bookList.Count; i++)
            {
                int bookRarity = (bookList[i].rarity - 21) * -1; // This is to "flip" the rarity values, so 20 will become 1 and 1 will become 20. 
                int arraystart = bookRollsList.Count;
                int fillElements = 0;
                if (enemyLevel != -1)
                {
                    if (bookRarity >= 15)
                    {
                        if (enemyLevel >= 21)
                            fillElements = (int)Mathf.Clamp(Mathf.Ceil(bookRarity - (enemyLevel / 1.5f)), 1, 400);
                        else if (enemyLevel >= 11)
                            fillElements = (int)Mathf.Clamp(Mathf.Ceil(bookRarity - (enemyLevel / 2.5f)), 1, 400);
                        else
                            fillElements = (int)Mathf.Clamp(Mathf.Ceil(bookRarity + (60f / enemyLevel)), 1, 400);
                    }
                    else if (bookRarity >= 8)
                    {
                        if (enemyLevel >= 21)
                            fillElements = (int)Mathf.Clamp(Mathf.Ceil(bookRarity + enemyLevel), 1, 400);
                        else if (enemyLevel >= 11)
                            fillElements = (int)Mathf.Clamp(Mathf.Ceil(bookRarity + (enemyLevel / 2f)), 1, 400);
                        else
                            fillElements = (int)Mathf.Clamp(Mathf.Ceil(bookRarity - (5f / enemyLevel)), 1, 400);
                    }
                    else
                    {
                        if (enemyLevel >= 21)
                            fillElements = (int)Mathf.Clamp(Mathf.Ceil(bookRarity + (enemyLevel / 2f)), 1, 400);
                        else if (enemyLevel >= 11)
                            fillElements = (int)Mathf.Clamp(Mathf.Ceil(bookRarity - (5f / enemyLevel)), 1, 400);
                        else
                            fillElements = (int)Mathf.Clamp(Mathf.Ceil(bookRarity - (5f / enemyLevel)), 1, 400);
                    }
                }
                else
                {
                    float luckMod = (playerLuck - 50) / 5f;

                    if (bookRarity >= 15)
                    {
                        fillElements = (int)Mathf.Clamp(Mathf.Ceil((bookRarity * 2.5f) - (luckMod * 2)), 1, 400);
                    }
                    else if (bookRarity >= 8)
                    {
                        fillElements = (int)Mathf.Clamp(Mathf.Ceil((bookRarity * 1.5f) + luckMod), 1, 400);
                    }
                    else
                    {
                        fillElements = (int)Mathf.Clamp(Mathf.Ceil(bookRarity + (luckMod / 2)), 1, 400);
                    }
                }

                bookRolls = FormulaHelper.FillArray(bookRollsList, arraystart, fillElements, i);
            }

            int chosenBookIndex = FormulaHelper.PickOneOf(bookRolls);

            return bookList[chosenBookIndex].message;
        }

        /// <summary>
        /// Creates a new random religious item.
        /// </summary>
        /// <returns>DaggerfallUnityItem.</returns>
        public static DaggerfallUnityItem CreateRandomReligiousItem()
        {
            Array enumArray = DaggerfallUnity.Instance.ItemHelper.GetEnumArray(ItemGroups.ReligiousItems);
            int groupIndex = UnityEngine.Random.Range(0, enumArray.Length);
            DaggerfallUnityItem newItem = new DaggerfallUnityItem(ItemGroups.ReligiousItems, groupIndex);

            return newItem;
        }

        public static DaggerfallUnityItem CreateRandomlyFilledSoulTrap()
        {
            // Create a trapped soul type and filter invalid creatures
            MobileTypes soul = MobileTypes.None;
            while (soul == MobileTypes.None)
            {
                MobileTypes randomSoul = (MobileTypes)UnityEngine.Random.Range((int)MobileTypes.Rat, (int)MobileTypes.Lamia + 1);
                if (randomSoul == MobileTypes.Horse_Invalid ||
                    randomSoul == MobileTypes.Dragonling)       // NOTE: Dragonling (34) is soulless, only soul of Dragonling_Alternate (40) from B0B70Y16 has a soul
                    continue;
                else
                    soul = randomSoul;
            }

            // Generate item
            DaggerfallUnityItem newItem = CreateItem(ItemGroups.MiscItems, (int)MiscItems.Soul_trap);
            newItem.TrappedSoulType = soul;
            MobileEnemy mobileEnemy = GameObjectHelper.EnemyDict[(int)soul];
            newItem.value = 5000 + mobileEnemy.SoulPts;

            return newItem;
        }

        /// <summary>
        /// Creates a new random gem.
        /// </summary>
        /// <returns>DaggerfallUnityItem.</returns>
        public static DaggerfallUnityItem CreateRandomGem(int enemyLevel = -1, int playerLuck = -1)
        {
            List<DaggerfallUnityItem> gemList = new List<DaggerfallUnityItem>();

            Array enumArray = DaggerfallUnity.Instance.ItemHelper.GetEnumArray(ItemGroups.Gems);
            for (int i = 0; i < enumArray.Length; i++)
            {
                int gemID = (int)enumArray.GetValue(i);
                DaggerfallUnityItem gemDummyChecked = new DaggerfallUnityItem(ItemGroups.Gems, gemID, true);
                gemList.Add(gemDummyChecked);
            }

            int chosenGemID = ChooseGemFromFilteredList(gemList, enemyLevel, playerLuck);

            // Create item
            DaggerfallUnityItem gem = new DaggerfallUnityItem(ItemGroups.Gems, chosenGemID);

            return gem;
        }

        public static int ChooseGemFromFilteredList(List<DaggerfallUnityItem> gemList, int enemyLevel = -1, int playerLuck = -1)
        {
            int[] gemRolls = new int[] { };
            List<int> gemRollsList = new List<int>();

            for (int i = 0; i < gemList.Count; i++)
            {
                int gemRarity = (gemList[i].rarity - 101) * -1; // This is to "flip" the rarity values, so 100 will become 1 and 1 will become 100. 
                int arraystart = gemRollsList.Count;
                int fillElements = 0;
                if (enemyLevel != -1)
                {
                    if (gemRarity >= 60)
                    {
                        if (enemyLevel >= 21)
                            fillElements = (int)Mathf.Clamp(Mathf.Ceil(gemRarity - (enemyLevel / 1.5f)), 1, 400);
                        else if (enemyLevel >= 11)
                            fillElements = (int)Mathf.Clamp(Mathf.Ceil(gemRarity - (enemyLevel / 2.5f)), 1, 400);
                        else
                            fillElements = (int)Mathf.Clamp(Mathf.Ceil(gemRarity + (60f / enemyLevel)), 1, 400);
                    }
                    else if (gemRarity >= 35)
                    {
                        if (enemyLevel >= 21)
                            fillElements = (int)Mathf.Clamp(Mathf.Ceil(gemRarity + enemyLevel), 1, 400);
                        else if (enemyLevel >= 11)
                            fillElements = (int)Mathf.Clamp(Mathf.Ceil(gemRarity + (enemyLevel / 2f)), 1, 400);
                        else
                            fillElements = (int)Mathf.Clamp(Mathf.Ceil(gemRarity - (5f / enemyLevel)), 1, 400);
                    }
                    else
                    {
                        if (enemyLevel >= 21)
                            fillElements = (int)Mathf.Clamp(Mathf.Ceil(gemRarity + (enemyLevel / 2f)), 1, 400);
                        else if (enemyLevel >= 11)
                            fillElements = (int)Mathf.Clamp(Mathf.Ceil(gemRarity - (5f / enemyLevel)), 1, 400);
                        else
                            fillElements = (int)Mathf.Clamp(Mathf.Ceil(gemRarity - (5f / enemyLevel)), 1, 400);
                    }
                }
                else
                {
                    float luckMod = (playerLuck - 50) / 5f;

                    if (gemRarity >= 60)
                    {
                        fillElements = (int)Mathf.Clamp(Mathf.Ceil((gemRarity * 2.5f) - (luckMod * 2)), 1, 400);
                    }
                    else if (gemRarity >= 35)
                    {
                        fillElements = (int)Mathf.Clamp(Mathf.Ceil((gemRarity * 1.5f) + luckMod), 1, 400);
                    }
                    else
                    {
                        fillElements = (int)Mathf.Clamp(Mathf.Ceil(gemRarity + (luckMod / 2)), 1, 400);
                    }
                }

                gemRolls = FormulaHelper.FillArray(gemRollsList, arraystart, fillElements, i);
            }

            int chosenGemIndex = FormulaHelper.PickOneOf(gemRolls);

            return gemList[chosenGemIndex].TemplateIndex;
        }

        /// <summary>
        /// Creates a new random jewellery.
        /// </summary>
        /// <returns>DaggerfallUnityItem.</returns>
        public static DaggerfallUnityItem CreateRandomJewellery()
        {
            Array enumArray = DaggerfallUnity.Instance.ItemHelper.GetEnumArray(ItemGroups.Jewellery);
            int groupIndex = UnityEngine.Random.Range(0, enumArray.Length);
            DaggerfallUnityItem newItem = new DaggerfallUnityItem(ItemGroups.Jewellery, groupIndex);

            return newItem;
        }

        /// <summary>
        /// Creates a new random drug.
        /// </summary>
        /// <returns>DaggerfallUnityItem.</returns>
        public static DaggerfallUnityItem CreateRandomDrug()
        {
            Array enumArray = DaggerfallUnity.Instance.ItemHelper.GetEnumArray(ItemGroups.Drugs);
            int groupIndex = UnityEngine.Random.Range(0, enumArray.Length);
            DaggerfallUnityItem newItem = new DaggerfallUnityItem(ItemGroups.Drugs, groupIndex);

            return newItem;
        }

        /// <summary>
        /// Generates a weapon.
        /// </summary>
        /// <param name="weapon"></param>
        /// <param name="material">Ignored for arrows</param>
        /// <returns></returns>
        public static DaggerfallUnityItem CreateWeapon(Weapons weapon, WeaponMaterialTypes material)
        {
            // Create item
            int groupIndex = DaggerfallUnity.Instance.ItemHelper.GetGroupIndex(ItemGroups.Weapons, (int)weapon);
            DaggerfallUnityItem newItem = new DaggerfallUnityItem(ItemGroups.Weapons, groupIndex);

            if (weapon == Weapons.Arrow)
            {   // Handle arrows
                newItem.stackCount = UnityEngine.Random.Range(1, 20 + 1);
                newItem.currentCondition = 0; // not sure if this is necessary, but classic does it
            }
            else
            {
                ApplyWeaponMaterial(newItem, material);
            }
            return newItem;
        }

        /// <summary>
        /// Creates random weapon.
        /// </summary>
        /// <param name="playerLevel">Player level for material type.</param>
        /// <returns>DaggerfallUnityItem</returns>
        public static DaggerfallUnityItem CreateRandomWeapon(int enemyLevel = -1, int buildingQuality = -1, int playerLuck = -1)
        {
            // Create a random weapon type, including any custom items registered as weapons
            ItemHelper itemHelper = DaggerfallUnity.Instance.ItemHelper;
            Array enumArray = itemHelper.GetEnumArray(ItemGroups.Weapons);
            int[] customItemTemplates = itemHelper.GetCustomItemsForGroup(ItemGroups.Weapons);

            int groupIndex = UnityEngine.Random.Range(0, enumArray.Length + customItemTemplates.Length);
            DaggerfallUnityItem newItem;
            if (groupIndex < enumArray.Length)
                newItem = new DaggerfallUnityItem(ItemGroups.Weapons, groupIndex);
            else
                newItem = CreateItem(ItemGroups.Weapons, customItemTemplates[groupIndex - enumArray.Length]);
 
            // Random weapon material
            WeaponMaterialTypes material = FormulaHelper.RandomMaterial(enemyLevel, buildingQuality, playerLuck);
            ApplyWeaponMaterial(newItem, material);

            // Handle arrows
            if (groupIndex == 18)
            {
                newItem.stackCount = UnityEngine.Random.Range(1, 20 + 1);
                newItem.currentCondition = 0; // not sure if this is necessary, but classic does it
                newItem.nativeMaterialValue = 0; // Arrows don't have a material
            }

            return newItem;
        }

        /// <summary>Set material and adjust weapon stats accordingly</summary>
        public static void ApplyWeaponMaterial(DaggerfallUnityItem weapon, WeaponMaterialTypes material)
        {
            weapon.nativeMaterialValue = (int)material;
            weapon = SetItemPropertiesByMaterial(weapon, material);
            weapon.dyeColor = DaggerfallUnity.Instance.ItemHelper.GetWeaponDyeColor(material);

            // Female characters use archive - 1 (i.e. 233 rather than 234) for weapons
            if (GameManager.Instance.PlayerEntity.Gender == Genders.Female)
                weapon.PlayerTextureArchive -= 1;
        }

        /// <summary>
        /// Generates armour.
        /// </summary>
        /// <param name="gender">Gender armor is created for.</param>
        /// <param name="race">Race armor is created for.</param>
        /// <param name="armor">Type of armor item to create.</param>
        /// <param name="material">Material of armor.</param>
        /// <param name="variant">Visual variant of armor. If -1, a random variant is chosen.</param>
        /// <returns>DaggerfallUnityItem</returns>
        public static DaggerfallUnityItem CreateArmor(Genders gender, Races race, Armor armor, ArmorMaterialTypes material, int variant = -1)
        {
            // Create item
            int groupIndex = DaggerfallUnity.Instance.ItemHelper.GetGroupIndex(ItemGroups.Armor, (int)armor);
            DaggerfallUnityItem newItem = new DaggerfallUnityItem(ItemGroups.Armor, groupIndex);

            ApplyArmorSettings(newItem, gender, race, material, variant);

            return newItem;
        }

        /// <summary>
        /// Creates random armor.
        /// </summary>
        /// <param name="playerLevel">Player level for material type.</param>
        /// <param name="gender">Gender armor is created for.</param>
        /// <param name="race">Race armor is created for.</param>
        /// <returns>DaggerfallUnityItem</returns>
        public static DaggerfallUnityItem CreateRandomArmor(Genders gender, Races race, int enemyLevel = -1, int buildingQuality = -1, int playerLuck = -1, int armorType = -1)
        {
            // Create a random armor type, including any custom items registered as armor
            ItemHelper itemHelper = DaggerfallUnity.Instance.ItemHelper;
            Array enumArray = itemHelper.GetEnumArray(ItemGroups.Armor);
            int[] customItemTemplates = itemHelper.GetCustomItemsForGroup(ItemGroups.Armor);

            int groupIndex = UnityEngine.Random.Range(0, enumArray.Length + customItemTemplates.Length);
            DaggerfallUnityItem newItem;
            if (groupIndex < enumArray.Length)
                newItem = new DaggerfallUnityItem(ItemGroups.Armor, groupIndex);
            else
                newItem = CreateItem(ItemGroups.Armor, customItemTemplates[groupIndex - enumArray.Length]);

            ApplyArmorSettings(newItem, gender, race, FormulaHelper.RandomArmorMaterial(enemyLevel, buildingQuality, playerLuck, armorType));

            return newItem;
        }

        /// <summary>Set gender, body morphology and material of armor</summary>
        public static void ApplyArmorSettings(DaggerfallUnityItem armor, Genders gender, Races race, ArmorMaterialTypes material, int variant = 0)
        {
            // Adjust for gender
            if (gender == Genders.Female)
                armor.PlayerTextureArchive = firstFemaleArchive;
            else
                armor.PlayerTextureArchive = firstMaleArchive;

            // Adjust for body morphology
            SetRace(armor, race);

            // Adjust material
            ApplyArmorMaterial(armor, material);

            // Adjust for variant
            if (variant >= 0)
                SetVariant(armor, variant);
            else
                RandomizeArmorVariant(armor);
        }

        /// <summary>Set material and adjust armor stats accordingly</summary>
        public static void ApplyArmorMaterial(DaggerfallUnityItem armor, ArmorMaterialTypes material)
        {
            armor.nativeMaterialValue = (int)material;

            if (armor.nativeMaterialValue == (int)ArmorMaterialTypes.Leather)
            {
                armor.weightInKg *= 0.65f;
                armor.density *= 150;
                armor.shear *= 50;
                armor.fracture *= 650;
                armor.meltingPoint *= 75;
                armor.conductivity *= 25;
                armor.brittleness *= 25;
            }
            else if (armor.nativeMaterialValue == (int)ArmorMaterialTypes.Chain)
            {
                armor.weightInKg *= 1.75f;
                armor.value *= 2;
                armor.density *= 200;
                armor.shear *= 550;
                armor.fracture *= 50;
                armor.meltingPoint *= 75;
                armor.conductivity *= 175;
                armor.brittleness *= 150;
            }
            else if (armor.nativeMaterialValue >= (int)ArmorMaterialTypes.Iron)
            {
                int plateMaterial = armor.nativeMaterialValue - 0x0200;
                armor = SetItemPropertiesByMaterial(armor, (WeaponMaterialTypes)plateMaterial);
            }

            armor.dyeColor = DaggerfallUnity.Instance.ItemHelper.GetArmorDyeColor(material);
        }

        /*/// <summary>
        /// Generates a weapon.
        /// </summary>
        /// <param name="weapon"></param>
        /// <param name="material">Ignored for arrows</param>
        /// <returns></returns>
        public static DaggerfallUnityItem CreateWeapon(Weapons weapon, WeaponMaterialTypes material)
        {
            // Create item
            int groupIndex = DaggerfallUnity.Instance.ItemHelper.GetGroupIndex(ItemGroups.Weapons, (int)weapon);
            DaggerfallUnityItem newItem = new DaggerfallUnityItem(ItemGroups.Weapons, groupIndex);

            if (weapon == Weapons.Arrow)
            {   // Handle arrows
                newItem.stackCount = UnityEngine.Random.Range(1, 20 + 1);
                newItem.currentCondition = 0; // not sure if this is necessary, but classic does it
            }
            else
            {
                ApplyWeaponMaterial(newItem, material);
            }
            return newItem;
        }*/

        /// <summary>
        /// Creates random ingot.
        /// </summary>
        /// <param name="playerLevel">Player level for material type.</param>
        /// <returns>DaggerfallUnityItem</returns>
        public static DaggerfallUnityItem CreateRandomIngot(int enemyLevel = -1, int buildingQuality = -1, int playerLuck = -1)
        {
            DaggerfallUnityItem newItem = CreateItem(ItemGroups.UselessItems2, 810);

            // Random weapon material
            WeaponMaterialTypes material = FormulaHelper.RandomMaterial(enemyLevel, buildingQuality, playerLuck);
            ApplyIngotMaterial(newItem, material);

            return newItem;
        }

        /// <summary>Set material and adjust weapon stats accordingly</summary>
        public static void ApplyIngotMaterial(DaggerfallUnityItem ingot, WeaponMaterialTypes material)
        {
            ingot.nativeMaterialValue = (int)material;
            ingot = SetItemPropertiesByMaterial(ingot, material);
            ingot.dyeColor = DaggerfallUnity.Instance.ItemHelper.GetWeaponDyeColor(material);
        }

        /// <summary>
        /// Creates random magic item in same manner as classic.
        /// </summary>
        /// <returns>DaggerfallUnityItem</returns>
        public static DaggerfallUnityItem CreateRandomMagicItem(Genders gender, Races race, int enemyLevel = -1, int buildingQuality = -1, int playerLuck = -1)
        {
            return CreateRegularMagicItem(chooseAtRandom, enemyLevel, playerLuck, gender, race);
        }

        /// <summary>
        /// Create a regular non-artifact magic item.
        /// </summary>
        /// <param name="chosenItem">An integer index of the item to create, or -1 for a random one.</param>
        /// <param name="playerLevel">The player level to create an item for.</param>
        /// <param name="gender">The gender to create an item for.</param>
        /// <param name="race">The race to create an item for.</param>
        /// <returns>DaggerfallUnityItem</returns>
        /// <exception cref="Exception">When a base item cannot be created.</exception>
        public static DaggerfallUnityItem CreateRegularMagicItem(int chosenItem, int enemylevel, int playerLuck, Genders gender, Races race)
        {
            byte[] itemGroups0 = { 2, 3, 6, 10, 12, 14, 25 };
            byte[] itemGroups1 = { 2, 3, 6, 12, 25 };

            DaggerfallUnityItem newItem = null;

            // Get the list of magic item templates read from MAGIC.DEF
            MagicItemsFile magicItemsFile = new MagicItemsFile(Path.Combine(DaggerfallUnity.Instance.Arena2Path, "MAGIC.DEF"));
            List<MagicItemTemplate> magicItems = magicItemsFile.MagicItemsList;

            // Reduce the list to only the regular magic items.
            MagicItemTemplate[] regularMagicItems = magicItems.Where(template => template.type == MagicItemTypes.RegularMagicItem).ToArray();
            if (chosenItem > regularMagicItems.Length)
                throw new Exception(string.Format("Magic item subclass {0} does not exist", chosenItem));

            // Pick a random one if needed.
            if (chosenItem == chooseAtRandom)
            {
                chosenItem = UnityEngine.Random.Range(0, regularMagicItems.Length);
            }

            // Get the chosen template
            MagicItemTemplate magicItem = regularMagicItems[chosenItem];

            // Get the item group. The possible groups are determined by the 33rd byte (magicItem.group) of the MAGIC.DEF template being used.
            ItemGroups group = 0;
            if (magicItem.group == 0)
                group = (ItemGroups)itemGroups0[UnityEngine.Random.Range(0, 7)];
            else if (magicItem.group == 1)
                group = (ItemGroups)itemGroups1[UnityEngine.Random.Range(0, 5)];
            else if (magicItem.group == 2)
                group = ItemGroups.Weapons;

            // Create the base item
            if (group == ItemGroups.Weapons)
            {
                newItem = CreateRandomWeapon(enemylevel, -1, playerLuck);

                // No arrows as enchanted items
                while (newItem.GroupIndex == 18)
                    newItem = CreateRandomWeapon(enemylevel, -1, playerLuck);
            }
            else if (group == ItemGroups.Armor)
                newItem = CreateRandomArmor(gender, race, enemylevel, -1, playerLuck);
            else if (group == ItemGroups.MensClothing || group == ItemGroups.WomensClothing)
                newItem = CreateRandomClothing(gender, race);
            else if (group == ItemGroups.ReligiousItems)
                newItem = CreateRandomReligiousItem();
            else if (group == ItemGroups.Gems)
                newItem = CreateRandomGem();
            else // Only other possibility is jewellery
                newItem = CreateRandomJewellery();

            if (newItem == null)
                throw new Exception("CreateRegularMagicItem() failed to create an item.");

            // Replace the regular item name with the magic item name
            newItem.shortName = magicItem.name;

            // Add the enchantments
            newItem.legacyMagic = new DaggerfallEnchantment[magicItem.enchantments.Length];
            for (int i = 0; i < magicItem.enchantments.Length; ++i)
                newItem.legacyMagic[i] = magicItem.enchantments[i];

            // Set the condition/magic uses
            newItem.maxCondition = magicItem.uses;
            newItem.currentCondition = magicItem.uses;

            // Set the value of the item. This is determined by the enchantment point cost/spell-casting cost
            // of the enchantments on the item.
            int value = 0;
            for (int i = 0; i < magicItem.enchantments.Length; ++i)
            {
                if (magicItem.enchantments[i].type != EnchantmentTypes.None
                    && magicItem.enchantments[i].type < EnchantmentTypes.ItemDeteriorates)
                {
                    switch (magicItem.enchantments[i].type)
                    {
                        case EnchantmentTypes.CastWhenUsed:
                        case EnchantmentTypes.CastWhenHeld:
                        case EnchantmentTypes.CastWhenStrikes:
                            // Enchantments that cast a spell. The parameter is the spell index in SPELLS.STD.
                            value += Formulas.FormulaHelper.GetSpellEnchantPtCost(magicItem.enchantments[i].param);
                            break;
                        case EnchantmentTypes.RepairsObjects:
                        case EnchantmentTypes.AbsorbsSpells:
                        case EnchantmentTypes.EnhancesSkill:
                        case EnchantmentTypes.FeatherWeight:
                        case EnchantmentTypes.StrengthensArmor:
                            // Enchantments that provide an effect that has no parameters
                            value += enchantmentPointCostsForNonParamTypes[(int)magicItem.enchantments[i].type];
                            break;
                        case EnchantmentTypes.SoulBound:
                            // Bound soul
                            MobileEnemy mobileEnemy = GameObjectHelper.EnemyDict[magicItem.enchantments[i].param];
                            value += mobileEnemy.SoulPts; // TODO: Not sure about this. Should be negative? Needs to be tested.
                            break;
                        default:
                            // Enchantments that provide a non-spell effect with a parameter (parameter = when effect applies, what enemies are affected, etc.)
                            value += enchantmentPtsForItemPowerArrays[(int)magicItem.enchantments[i].type][magicItem.enchantments[i].param];
                            break;
                    }
                }
            }

            newItem.value = value;

            return newItem;
        }

        /// <summary>
        /// Sets properties for a weapon or piece of armor based on its material.
        /// </summary>
        /// <param name="item">Item to have its properties modified.</param>
        /// <param name="material">Material to use to apply properties.</param>
        /// <returns>DaggerfallUnityItem</returns>
        public static DaggerfallUnityItem SetItemPropertiesByMaterial(DaggerfallUnityItem item, WeaponMaterialTypes material)
        {
            item.value *= 3 * valueMultipliersByMaterial[(int)material];
            item.weightInKg = CalculateWeightForMaterial(item, material);
            item.maxCondition = item.maxCondition * conditionMultipliersByMaterial[(int)material] / 4;
            item.currentCondition = item.maxCondition;
            item.density *= densityMultipliersByMaterial[(int)material];
            item.shear *= shearMultipliersByMaterial[(int)material];
            item.fracture *= fractureMultipliersByMaterial[(int)material];
            item.meltingPoint *= meltingPointMultipliersByMaterial[(int)material];
            item.conductivity *= conductivityMultipliersByMaterial[(int)material];
            item.brittleness *= brittlenessMultipliersByMaterial[(int)material];

            return item;
        }

        static float CalculateWeightForMaterial(DaggerfallUnityItem item, WeaponMaterialTypes material)
        {
            float quarterKgs = item.weightInKg * 4f;
            float matQuarterKgs = quarterKgs * weightMultipliersByMaterial[(int)material] / 4f;
            return matQuarterKgs / 4;
        }

        /// <summary>
        /// Creates a random ingredient from any of the ingredient groups.
        /// Passing a non-ingredient group will return null.
        /// </summary>
        /// <param name="ingredientGroup">Ingredient group.</param>
        /// <returns>DaggerfallUnityItem</returns>
        public static DaggerfallUnityItem CreateRandomIngredient(ItemGroups ingredientGroup)
        {
            int groupIndex;
            Array enumArray;
            switch (ingredientGroup)
            {
                case ItemGroups.MiscPlantIngredients:
                case ItemGroups.FlowerPlantIngredients:
                case ItemGroups.FruitPlantIngredients:
                case ItemGroups.AnimalPartIngredients:
                case ItemGroups.CreatureIngredients:
                case ItemGroups.SolventIngredients:
                case ItemGroups.MetalIngredients:
                    enumArray = DaggerfallUnity.Instance.ItemHelper.GetEnumArray(ingredientGroup);
                    groupIndex = UnityEngine.Random.Range(0, enumArray.Length);
                    break;
                default:
                    return null;
            }

            // Create item
            DaggerfallUnityItem newItem = new DaggerfallUnityItem(ingredientGroup, groupIndex);

            newItem.CurrentVariant = UnityEngine.Random.Range(0, newItem.TotalVariants);

            return newItem;
        }

        /// <summary>
        /// Creates a random ingredient from a random ingredient group.
        /// </summary>
        /// <returns>DaggerfallUnityItem</returns>
        public static DaggerfallUnityItem CreateRandomIngredient()
        {
            // Randomise ingredient group
            ItemGroups itemGroup;
            int group = UnityEngine.Random.Range(0, 7);
            Array enumArray;
            switch (group)
            {
                case 0:
                    itemGroup = ItemGroups.MiscPlantIngredients;
                    break;
                case 1:
                    itemGroup = ItemGroups.FlowerPlantIngredients;
                    break;
                case 2:
                    itemGroup = ItemGroups.FruitPlantIngredients;
                    break;
                case 3:
                    itemGroup = ItemGroups.AnimalPartIngredients;
                    break;
                case 4:
                    itemGroup = ItemGroups.CreatureIngredients;
                    break;
                case 5:
                    itemGroup = ItemGroups.SolventIngredients;
                    break;
                case 6:
                    itemGroup = ItemGroups.MetalIngredients;
                    break;
                default:
                    return null;
            }

            // Randomise ingredient within group
            enumArray = DaggerfallUnity.Instance.ItemHelper.GetEnumArray(itemGroup);
            int groupIndex = UnityEngine.Random.Range(0, enumArray.Length);

            // Create item
            DaggerfallUnityItem newItem = new DaggerfallUnityItem(itemGroup, groupIndex);

            return newItem;
        }

        /// <summary>
        /// Creates a potion.
        /// </summary>
        /// <param name="recipe">Recipe index for the potion</param>
        /// <returns>Potion DaggerfallUnityItem</returns>
        public static DaggerfallUnityItem CreatePotion(int recipeKey, int stackSize = 1)
        {
            return new DaggerfallUnityItem(ItemGroups.UselessItems1, 1) { PotionRecipeKey = recipeKey, stackCount = stackSize };
        }

        /// <summary>
        /// Creates a dummy potion to be used for data referencing.
        /// </summary>
        /// <param name="recipe">Recipe index for the potion</param>
        /// <returns>Potion DaggerfallUnityItem</returns>
        public static DaggerfallUnityItem CreateDummyPotion(int recipeKey, int stackSize = 1)
        {
            return new DaggerfallUnityItem(ItemGroups.UselessItems1, 1, true) { PotionRecipeKey = recipeKey, stackCount = stackSize };
        }

        /// <summary>
        /// Creates a random potion from all registered recipes.
        /// </summary>
        /// <returns>Potion DaggerfallUnityItem</returns>
        public static DaggerfallUnityItem CreateRandomPotion(int stackSize = 1)
        {
            List<int> recipeKeys = GameManager.Instance.EntityEffectBroker.GetPotionRecipeKeys();
            int recipeIdx = UnityEngine.Random.Range(0, recipeKeys.Count);


            /*for (int i = 0; i < recipeKeys.Count; i++)  // This is simply here for a quick easy testing loop to see the potions and their prices in the Unity Debug window. 
            {
                DaggerfallUnityItem printThis = CreateDummyPotion(recipeKeys[i]);
                MagicAndEffects.EntityEffectBroker effectBroker = GameManager.Instance.EntityEffectBroker;
                MagicAndEffects.PotionRecipe potionRecipe = effectBroker.GetPotionRecipe(printThis.PotionRecipeKey);
                MagicAndEffects.IEntityEffect potionEffect = effectBroker.GetPotionRecipeEffect(potionRecipe);

                Debug.LogFormat("Potion Recipe ID: {0}, ||| Potion Name: {1}, ||| Potion Weight: {2}, ||| Potion Value: {3} ||| Potion Rarity: {4} ||| Potion Effect Group Name: {5}", recipeKeys[i], printThis.LongName, printThis.weightInKg, printThis.value, potionRecipe.Rarity, potionEffect.GroupName);
            }*/

            return CreatePotion(recipeKeys[recipeIdx]);
        }

        /// <summary>
        /// Creates a random (classic) potion
        /// </summary>
        /// <returns>Potion DaggerfallUnityItem</returns>
        public static DaggerfallUnityItem CreateRandomClassicPotion()
        {
            int recipeIdx = UnityEngine.Random.Range(0, MagicAndEffects.PotionRecipe.classicRecipeKeys.Length);
            return CreatePotion(MagicAndEffects.PotionRecipe.classicRecipeKeys[recipeIdx]);
        }

        /// <summary>
        /// Creates a random potion from an array of filtered recipe types and set rarity.
        /// </summary>
        public static DaggerfallUnityItem CreateRandomSpecificPotion(byte[,] allowedPots, int enemyLevel = -1, int playerLuck = -1)
        {
            int[] potTypeRolls = new int[] { };
            List<int> potTypeRollsList = new List<int>();

            for (int i = 0; i < allowedPots.Length/2; i++) // Divide by 2, because the "Length" property counts ALL elements in a multidimensional array, not just one row or the columns, so divide by the number of rows basically. 
            {
                int arraystart = potTypeRollsList.Count;
                int fillElements = allowedPots[i, 1];

                if (allowedPots[i, 0] == 1)
                    potTypeRolls = FormulaHelper.FillArray(potTypeRollsList, arraystart, fillElements, i);
            }

            List<DaggerfallUnityItem> filteredPotList = GetPotTypeRecipeKeyList(FormulaHelper.PickOneOf(potTypeRolls));

            int chosenPotionRecipeKey = ChoosePotionFromFilteredList(filteredPotList, enemyLevel, playerLuck);

            return CreatePotion(chosenPotionRecipeKey);
        }

        public static List<DaggerfallUnityItem> GetPotTypeRecipeKeyList(int potionType) // Possibly condense/simplify the "CreatePotion" method used to only the bare minimum for what I need in this case, to save processing, maybe. 
        {
            List<int> allRecipeKeys = GameManager.Instance.EntityEffectBroker.GetPotionRecipeKeys();
            List<DaggerfallUnityItem> filteredPotions = new List<DaggerfallUnityItem>();

            MagicAndEffects.EntityEffectBroker effectBroker = GameManager.Instance.EntityEffectBroker;

            switch (potionType)
            {
                case 0:
                    for (int i = 0; i < allRecipeKeys.Count; i++)
                    {
                        DaggerfallUnityItem potChecked = CreateDummyPotion(allRecipeKeys[i]);
                        MagicAndEffects.PotionRecipe potionRecipe = effectBroker.GetPotionRecipe(potChecked.PotionRecipeKey);
                        MagicAndEffects.IEntityEffect potionEffect = effectBroker.GetPotionRecipeEffect(potionRecipe);
                        if (allRecipeKeys[i] != 422980775 && (potionEffect.Key == "Heal-Health" || potionEffect.Key == "Regenerate")) // Excluding "Miraculous Closing Wounds" potion, Recipe ID will change if recipe/hashcode changes, just keep note. 
                            filteredPotions.Add(potChecked);
                    }
                    break;
                case 1:
                    for (int i = 0; i < allRecipeKeys.Count; i++)
                    {
                        DaggerfallUnityItem potChecked = CreateDummyPotion(allRecipeKeys[i]);
                        MagicAndEffects.PotionRecipe potionRecipe = effectBroker.GetPotionRecipe(potChecked.PotionRecipeKey);
                        MagicAndEffects.IEntityEffect potionEffect = effectBroker.GetPotionRecipeEffect(potionRecipe);
                        if (potionEffect.Key == "Heal-Fatigue")
                            filteredPotions.Add(potChecked);
                    }
                    break;
                case 2:
                    for (int i = 0; i < allRecipeKeys.Count; i++)
                    {
                        DaggerfallUnityItem potChecked = CreateDummyPotion(allRecipeKeys[i]);
                        MagicAndEffects.PotionRecipe potionRecipe = effectBroker.GetPotionRecipe(potChecked.PotionRecipeKey);
                        MagicAndEffects.IEntityEffect potionEffect = effectBroker.GetPotionRecipeEffect(potionRecipe);
                        if (allRecipeKeys[i] != -1754277189 && (potionEffect.Key == "Heal-SpellPoints")) // Excluding "Greater Restore Power" potion, Recipe ID will change if recipe/hashcode changes, just keep note. 
                            filteredPotions.Add(potChecked);
                    }
                    break;
                case 3:
                    for (int i = 0; i < allRecipeKeys.Count; i++)
                    {
                        DaggerfallUnityItem potChecked = CreateDummyPotion(allRecipeKeys[i]);
                        MagicAndEffects.PotionRecipe potionRecipe = effectBroker.GetPotionRecipe(potChecked.PotionRecipeKey);
                        MagicAndEffects.IEntityEffect potionEffect = effectBroker.GetPotionRecipeEffect(potionRecipe);
                        if (allRecipeKeys[i] != -1357071761 && (potionEffect.GroupName == "Cure" || potionEffect.Key == "Dispel-Magic")) // Excluding "Purification" potion, Recipe ID will change if recipe/hashcode changes, just keep note. 
                            filteredPotions.Add(potChecked);
                    }
                    break;
                case 4:
                    for (int i = 0; i < allRecipeKeys.Count; i++)
                    {
                        DaggerfallUnityItem potChecked = CreateDummyPotion(allRecipeKeys[i]);
                        MagicAndEffects.PotionRecipe potionRecipe = effectBroker.GetPotionRecipe(potChecked.PotionRecipeKey);
                        MagicAndEffects.IEntityEffect potionEffect = effectBroker.GetPotionRecipeEffect(potionRecipe);
                        if (potionEffect.GroupName == "Fortify Attribute")
                            filteredPotions.Add(potChecked);
                    }
                    break;
                case 5:
                    for (int i = 0; i < allRecipeKeys.Count; i++)
                    {
                        DaggerfallUnityItem potChecked = CreateDummyPotion(allRecipeKeys[i]);
                        MagicAndEffects.PotionRecipe potionRecipe = effectBroker.GetPotionRecipe(potChecked.PotionRecipeKey);
                        MagicAndEffects.IEntityEffect potionEffect = effectBroker.GetPotionRecipeEffect(potionRecipe);
                        if ((allRecipeKeys[i] != 9062553 || allRecipeKeys[i] != -1944350794 || allRecipeKeys[i] != -1586429900) && (potionEffect.GroupName == "Elemental Resistance" || potionEffect.GroupName == "Shield" || potionEffect.GroupName == "Spell Resistance" || potionEffect.GroupName == "Spell Reflection" || potionEffect.GroupName == "Spell Absorption")) // Excluding "Peryite's Spell Immunity", "Stendarr's Shield", and "Perfect Mirror" potions. 
                            filteredPotions.Add(potChecked);
                    }
                    break;
                case 6:
                    for (int i = 0; i < allRecipeKeys.Count; i++)
                    {
                        DaggerfallUnityItem potChecked = CreateDummyPotion(allRecipeKeys[i]);
                        MagicAndEffects.PotionRecipe potionRecipe = effectBroker.GetPotionRecipe(potChecked.PotionRecipeKey);
                        MagicAndEffects.IEntityEffect potionEffect = effectBroker.GetPotionRecipeEffect(potionRecipe);
                        if (allRecipeKeys[i] != -1073126825 && (potionEffect.GroupName == "Invisibility" || potionEffect.GroupName == "Shadow" || potionEffect.GroupName == "Chameleon" || potionEffect.GroupName == "Comprehend Languages" || potionEffect.GroupName == "Levitate" || potionEffect.GroupName == "Water Walking" || potionEffect.GroupName == "Water Breathing" || potionEffect.GroupName == "Slowfall" || potionEffect.GroupName == "Free Action" || potionEffect.GroupName == "Light" || potionEffect.GroupName == "Jumping" || potionEffect.GroupName == "Climbing")) // Excluding "Clavicus Vile's Deception" potion, Recipe ID will change if recipe/hashcode changes. 
                            filteredPotions.Add(potChecked);
                    }
                    break;
                default:
                    return filteredPotions;
            }
            return filteredPotions;
        }

        public static int ChoosePotionFromFilteredList(List<DaggerfallUnityItem> potList, int enemyLevel = -1, int playerLuck = -1)
        {
            int[] potRolls = new int[] { };
            List<int> potRollsList = new List<int>();
            MagicAndEffects.EntityEffectBroker effectBroker = GameManager.Instance.EntityEffectBroker;

            for (int i = 0; i < potList.Count; i++)
            {
                MagicAndEffects.PotionRecipe potionRecipe = effectBroker.GetPotionRecipe(potList[i].PotionRecipeKey);
                int arraystart = potRollsList.Count;
                int fillElements = 0;
                if (enemyLevel != -1)
                {
                    if (potionRecipe.Rarity >= 30)
                    {
                        if (enemyLevel >= 21)
                            fillElements = (int)Mathf.Clamp(Mathf.Ceil(potionRecipe.Rarity - (enemyLevel / 1.5f)), 1, 300);
                        else if (enemyLevel >= 11)
                            fillElements = (int)Mathf.Clamp(Mathf.Ceil(potionRecipe.Rarity - (enemyLevel / 2.5f)), 1, 300);
                        else
                            fillElements = (int)Mathf.Clamp(Mathf.Ceil(potionRecipe.Rarity + (120f / enemyLevel)), 1, 300);
                    }
                    else if (potionRecipe.Rarity >= 15)
                    {
                        if (enemyLevel >= 21)
                            fillElements = (int)Mathf.Clamp(Mathf.Ceil(potionRecipe.Rarity + enemyLevel), 1, 300);
                        else if (enemyLevel >= 11)
                            fillElements = (int)Mathf.Clamp(Mathf.Ceil(potionRecipe.Rarity + (enemyLevel / 2f)), 1, 300);
                        else
                            fillElements = (int)Mathf.Clamp(Mathf.Ceil(potionRecipe.Rarity - (10f / enemyLevel)), 1, 300);
                    }
                    else
                    {
                        if (enemyLevel >= 21)
                            fillElements = (int)Mathf.Clamp(Mathf.Ceil(potionRecipe.Rarity + (enemyLevel / 2f)), 1, 300);
                        else if (enemyLevel >= 11)
                            fillElements = (int)Mathf.Clamp(Mathf.Ceil(potionRecipe.Rarity - (5f / enemyLevel)), 1, 300);
                        else
                            fillElements = (int)Mathf.Clamp(Mathf.Ceil(potionRecipe.Rarity - (10f / enemyLevel)), 1, 300);
                    }
                }
                else
                {
                    float luckMod = (playerLuck - 50) / 5f;

                    if (potionRecipe.Rarity >= 30)
                    {
                        fillElements = (int)Mathf.Clamp(Mathf.Ceil(potionRecipe.Rarity - (luckMod * 2)), 1, 300);
                    }
                    else if (potionRecipe.Rarity >= 15)
                    {
                        fillElements = (int)Mathf.Clamp(Mathf.Ceil(potionRecipe.Rarity + luckMod), 1, 300);
                    }
                    else
                    {
                        fillElements = (int)Mathf.Clamp(Mathf.Ceil(potionRecipe.Rarity + (luckMod / 2)), 1, 300);
                    }
                }

                potRolls = FormulaHelper.FillArray(potRollsList, arraystart, fillElements, i);
            }

            int chosenPotIndex = FormulaHelper.PickOneOf(potRolls);

            return potList[chosenPotIndex].PotionRecipeKey;
        }

        /// <summary>
        /// Generates gold pieces.
        /// </summary>
        /// <param name="amount">Total number of gold pieces in stack.</param>
        /// <returns></returns>
        public static DaggerfallUnityItem CreateGoldPieces(int amount)
        {
            if (amount <= 0) // Will see if this causes issues, it might, could not as well not sure. 
                return null;

            DaggerfallUnityItem newItem = CreateItem(ItemGroups.Currency, (int)Currency.Gold_pieces);
            newItem.stackCount = amount;
            newItem.value = 1;

            return newItem;
        }

        /// <summary>
        /// Sets a random variant of clothing item.
        /// </summary>
        /// <param name="item">Item to randomize variant.</param>
        public static void RandomizeClothingVariant(DaggerfallUnityItem item)
        {
            int totalVariants = item.ItemTemplate.variants;
            SetVariant(item, UnityEngine.Random.Range(0, totalVariants));
        }

        /// <summary>
        /// Sets a random variant of armor item.
        /// </summary>
        /// <param name="item">Item to randomize variant.</param>
        public static void RandomizeArmorVariant(DaggerfallUnityItem item)
        {
            int variant = 0;

            // We only need to pick randomly where there is more than one possible variant. Otherwise we can just pass in 0 to SetVariant and
            // the correct variant will still be chosen.
            if (item.IsOfTemplate(ItemGroups.Armor, (int)Armor.Cuirass) && (item.nativeMaterialValue >= (int)ArmorMaterialTypes.Iron))
            {
                variant = UnityEngine.Random.Range(1, 4);
            }
            else if (item.IsOfTemplate(ItemGroups.Armor, (int)Armor.Greaves))
            {
                if (item.nativeMaterialValue == (int)ArmorMaterialTypes.Leather)
                    variant = UnityEngine.Random.Range(0, 2);
                else if (item.nativeMaterialValue >= (int)ArmorMaterialTypes.Iron)
                    variant = UnityEngine.Random.Range(2, 6);
            }
            else if (item.IsOfTemplate(ItemGroups.Armor, (int)Armor.Left_Pauldron) || item.IsOfTemplate(ItemGroups.Armor, (int)Armor.Right_Pauldron))
            {
                if (item.nativeMaterialValue >= (int)ArmorMaterialTypes.Iron)
                    variant = UnityEngine.Random.Range(1, 4);
            }
            else if (item.IsOfTemplate(ItemGroups.Armor, (int)Armor.Boots) && (item.nativeMaterialValue >= (int)ArmorMaterialTypes.Iron))
            {
                variant = UnityEngine.Random.Range(1, 3);
            }
            else if (item.IsOfTemplate(ItemGroups.Armor, (int)Armor.Helm))
            {
                variant = UnityEngine.Random.Range(0, item.ItemTemplate.variants);
            }
            SetVariant(item, variant);
        }

        #endregion

        #region Static Utility Methods

        public static void SetRace(DaggerfallUnityItem item, Races race)
        {
            int offset = (int)GetBodyMorphology(race);
            item.PlayerTextureArchive += offset;
        }

        public static void SetVariant(DaggerfallUnityItem item, int variant)
        {
            // Range check
            int totalVariants = item.ItemTemplate.variants;
            if (variant < 0 || variant >= totalVariants)
                return;

            // Clamp to appropriate variant based on material family
            if (item.IsOfTemplate(ItemGroups.Armor, (int)Armor.Cuirass))
            {
                if (item.nativeMaterialValue == (int)ArmorMaterialTypes.Leather)
                    variant = 0;
                else if (item.nativeMaterialValue == (int)ArmorMaterialTypes.Chain || item.nativeMaterialValue == (int)ArmorMaterialTypes.Chain2)
                    variant = 4;
                else
                    variant = Mathf.Clamp(variant, 1, 3);
            }
            else if (item.IsOfTemplate(ItemGroups.Armor, (int)Armor.Greaves))
            {
                if (item.nativeMaterialValue == (int)ArmorMaterialTypes.Leather)
                    variant = Mathf.Clamp(variant, 0, 1);
                else if (item.nativeMaterialValue == (int)ArmorMaterialTypes.Chain || item.nativeMaterialValue == (int)ArmorMaterialTypes.Chain2)
                    variant = 6;
                else
                    variant = Mathf.Clamp(variant, 2, 5);
            }
            else if (item.IsOfTemplate(ItemGroups.Armor, (int)Armor.Left_Pauldron) || item.IsOfTemplate(ItemGroups.Armor, (int)Armor.Right_Pauldron))
            {
                if (item.nativeMaterialValue == (int)ArmorMaterialTypes.Leather)
                    variant = 0;
                else if (item.nativeMaterialValue == (int)ArmorMaterialTypes.Chain || item.nativeMaterialValue == (int)ArmorMaterialTypes.Chain2)
                    variant = 4;
                else
                    variant = Mathf.Clamp(variant, 1, 3);
            }
            else if (item.IsOfTemplate(ItemGroups.Armor, (int)Armor.Gauntlets))
            {
                if (item.nativeMaterialValue == (int)ArmorMaterialTypes.Leather)
                    variant = 0;
                else
                    variant = 1;
            }
            else if (item.IsOfTemplate(ItemGroups.Armor, (int)Armor.Boots))
            {
                if (item.nativeMaterialValue == (int)ArmorMaterialTypes.Leather)
                    variant = 0;
                else
                    variant = Mathf.Clamp(variant, 1, 2);
            }

            // Store variant
            item.CurrentVariant = variant;
        }

        public static BodyMorphology GetBodyMorphology(Races race)
        {
            switch (race)
            {
                case Races.Argonian:
                    return BodyMorphology.Argonian;

                case Races.DarkElf:
                case Races.HighElf:
                case Races.WoodElf:
                    return BodyMorphology.Elf;

                case Races.Breton:
                case Races.Nord:
                case Races.Redguard:
                    return BodyMorphology.Human;

                case Races.Khajiit:
                    return BodyMorphology.Khajiit;

                default:
                    throw new Exception("GetBodyMorphology() encountered unsupported race value.");
            }
        }

        #endregion
    }
}
