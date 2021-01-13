// Project:         Daggerfall Tools For Unity
// Copyright:       Copyright (C) 2009-2020 Daggerfall Workshop
// Web Site:        http://www.dfworkshop.net
// License:         MIT License (http://www.opensource.org/licenses/mit-license.php)
// Source Code:     https://github.com/Interkarma/daggerfall-unity
// Original Author: Hazelnut

using System;
using DaggerfallWorkshop.Utility;
using DaggerfallConnect.Arena2;
using System.Collections.Generic;
using DaggerfallWorkshop.Game.UserInterfaceWindows;
using UnityEngine;
using DaggerfallConnect.FallExe;
using DaggerfallWorkshop.Game.Utility;
using DaggerfallWorkshop.Game.Entity;
using DaggerfallWorkshop.Utility.AssetInjection;
using DaggerfallWorkshop.Game.MagicAndEffects;
using DaggerfallWorkshop.Game.Formulas;

namespace DaggerfallWorkshop.Game.Items
{
    public partial class DaggerfallUnityItem : IMacroContextProvider
    {
        [NonSerializedAttribute]
        private ItemMacroDataSource dataSource;

        public MacroDataSource GetMacroDataSource()
        {
            if (dataSource == null)
                dataSource = new ItemMacroDataSource(this);
            return dataSource;
        }

        public string GetPaintingFilename() { return dataSource.paintingFilename; }
        public int GetPaintingFileIdx() { return (int)dataSource.paintingFileIdx; }

        public TextFile.Token[] InitPaintingInfo(int paintingTextId = 250)
        {
            GetMacroDataSource();
            if (ItemGroup == ItemGroups.Paintings && dataSource.paintingInfo == null)
            {
                DFRandom.srand(message);
                uint paintingIndex = DFRandom.rand() % 180;
                dataSource.paintingFileIdx = paintingIndex & 7;
                char paintingFileChar = (char)((paintingIndex >> 3) + 97);
                dataSource.paintingFilename = paintingFileChar + "paint.cif";

                byte[] paintingRecord = DaggerfallUnity.Instance.ContentReader.PaintFileReader.Read(paintingIndex);
                Debug.LogFormat("painting file: {0}, index: {1}, cif idx: {2}, record: {3} {4} {5}", dataSource.paintingFilename, paintingIndex, dataSource.paintingFileIdx, paintingRecord[0], paintingRecord[1], paintingRecord[2]);

                dataSource.paintingSub = GetPaintingRecordPart(paintingRecord, 0, 9) + 6100; // for %sub macro
                dataSource.paintingAdj = GetPaintingRecordPart(paintingRecord, 10, 19) + 6200; // for %adj macro
                dataSource.paintingPp1 = GetPaintingRecordPart(paintingRecord, 20, 29) + 6300; // for %pp1 macro
                dataSource.paintingPp2 = GetPaintingRecordPart(paintingRecord, 30, 39) + 6400; // for %pp2 macro

                ITextProvider textProvider = DaggerfallUnity.Instance.TextProvider;
                dataSource.paintingInfo = textProvider.GetRandomTokens(paintingTextId, true);
            }
            return dataSource.paintingInfo;
        }

        private int GetPaintingRecordPart(byte[] paintingRecord, int start, int end)
        {
            int i = start;
            while (i <= end && paintingRecord[i] != 0xFF)
                i++;
            return (i - start == 1) ? paintingRecord[i - 1] : paintingRecord[DFRandom.random_range_inclusive(start, i - 1)];
        }


        /// <summary>
        /// MacroDataSource context sensitive methods for items in Daggerfall Unity.
        /// </summary>
        private class ItemMacroDataSource : MacroDataSource
        {
            private readonly string[] conditions = {
                TextManager.Instance.GetLocalizedText("Broken"), TextManager.Instance.GetLocalizedText("Useless"),
                TextManager.Instance.GetLocalizedText("Battered"), TextManager.Instance.GetLocalizedText("Worn"),
                TextManager.Instance.GetLocalizedText("Used"), TextManager.Instance.GetLocalizedText("SlightlyUsed"),
                TextManager.Instance.GetLocalizedText("AlmostNew"), TextManager.Instance.GetLocalizedText("New") };
            private readonly int[] conditionThresholds = { 1, 5, 15, 40, 60, 75, 91, 101 };

            private Recipe[] recipeArray;

            public string paintingFilename;
            public uint paintingFileIdx;
            public TextFile.Token[] paintingInfo;
            public int paintingSub;     // for %sub macro
            public int paintingAdj;     // for %adj macro
            public int paintingPp1;     // for %pp1 macro
            public int paintingPp2;     // for %pp2 macro

            private readonly DaggerfallUnityItem parent;
            public ItemMacroDataSource(DaggerfallUnityItem item)
            {
                this.parent = item;
            }

            public override string ItemName()
            {
                return parent.ItemName;
            }

            public override string Worth()
            {
                return (parent.value * parent.stackCount).ToString();
            }

            public override string Material()
            {   // %mat
                switch (parent.itemGroup)
                {
                    case ItemGroups.Armor:
                        return DaggerfallUnity.Instance.TextProvider.GetArmorMaterialName((ArmorMaterialTypes)parent.nativeMaterialValue);
                    case ItemGroups.Weapons:
                        return DaggerfallUnity.Instance.TextProvider.GetWeaponMaterialName((WeaponMaterialTypes)parent.nativeMaterialValue);
                    case ItemGroups.UselessItems2:
                        if (parent.TemplateIndex == 810)
                            return DaggerfallUnity.Instance.TextProvider.GetWeaponMaterialName((WeaponMaterialTypes)parent.nativeMaterialValue);
                        else
                            return base.Material();
                    default:
                        return base.Material();
                }
            }

            public override string Condition()
            {   // %qua
                if (parent.maxCondition > 0 && parent.currentCondition <= parent.maxCondition)
                {
                    int conditionPercentage = parent.ConditionPercentage;
                    int i = 0;
                    while (conditionPercentage > conditionThresholds[i])
                        i++;
                    return conditions[i];
                }
                else
                    return parent.currentCondition.ToString();
            }

            public override string Weight()
            {   // %kg
                float weight = parent.weightInKg * parent.stackCount;

                /*if (weight % 1 == 0)
                    return String.Format("{0:F0}", weight);
                else if (weight % 1 >= 0.0001f && weight % 1 <= 0.009f)
                    return String.Format("{0:F4}", weight);
                else if (weight % 1 >= 0.001f && weight % 1 <= 0.09f) // Won't include for now due to possible performance issues, but I don't think this was the problem, might be quest items or something. 
                    return String.Format("{0:F3}", weight);
                else if (weight % 1 >= 0.01f && weight % 1 <= 0.9f)
                    return String.Format("{0:F2}", weight);
                else
                    return String.Format("{0:F1}", weight);*/
                return String.Format(weight % 1 == 0 ? "{0:F0}" : "{0:F2}", weight);
            }

            public override string Density()
            {   // %den
                if (parent.density <= 100)
                    return "Extremely Low";
                else if (parent.density <= 200 && parent.density > 100)
                    return "Very Low";
                else if (parent.density <= 300 && parent.density > 200)
                    return "Low";
                else if (parent.density <= 350 && parent.density > 300)
                    return "Moderate";
                else if (parent.density <= 400 && parent.density > 350)
                    return "High";
                else if (parent.density <= 500 && parent.density > 400)
                    return "Very High";
                else
                    return "Extremely High";
            }

            public override string Shear()
            {   // %she
                if (parent.shear <= 100)
                    return "Extremely Low";
                else if (parent.shear <= 200 && parent.shear > 100)
                    return "Very Low";
                else if (parent.shear <= 300 && parent.shear > 200)
                    return "Low";
                else if (parent.shear <= 350 && parent.shear > 300)
                    return "Moderate";
                else if (parent.shear <= 400 && parent.shear > 350)
                    return "High";
                else if (parent.shear <= 500 && parent.shear > 400)
                    return "Very High";
                else
                    return "Extremely High";
            }

            public override string Fracture()
            {   // %fra
                if (parent.fracture <= 100)
                    return "Extremely Low";
                else if (parent.fracture <= 200 && parent.fracture > 100)
                    return "Very Low";
                else if (parent.fracture <= 300 && parent.fracture > 200)
                    return "Low";
                else if (parent.fracture <= 350 && parent.fracture > 300)
                    return "Moderate";
                else if (parent.fracture <= 400 && parent.fracture > 350)
                    return "High";
                else if (parent.fracture <= 500 && parent.fracture > 400)
                    return "Very High";
                else
                    return "Extremely High";
            }

            public override string MeltingPoint()
            {   // %mpo
                if (parent.meltingPoint <= 25)
                    return "Extremely Low";
                else if (parent.meltingPoint <= 50 && parent.meltingPoint > 25)
                    return "Very Low";
                else if (parent.meltingPoint <= 75 && parent.meltingPoint > 50)
                    return "Low";
                else if (parent.meltingPoint <= 150 && parent.meltingPoint > 75)
                    return "Moderate";
                else if (parent.meltingPoint <= 175 && parent.meltingPoint > 150)
                    return "High";
                else if (parent.meltingPoint <= 200 && parent.meltingPoint > 175)
                    return "Very High";
                else
                    return "Extremely High";
            }

            public override string Conductivity()
            {   // %con
                if (parent.conductivity <= 25)
                    return "Extremely Low";
                else if (parent.conductivity <= 50 && parent.conductivity > 25)
                    return "Very Low";
                else if (parent.conductivity <= 75 && parent.conductivity > 50)
                    return "Low";
                else if (parent.conductivity <= 150 && parent.conductivity > 75)
                    return "Moderate";
                else if (parent.conductivity <= 175 && parent.conductivity > 150)
                    return "High";
                else if (parent.conductivity <= 200 && parent.conductivity > 175)
                    return "Very High";
                else
                    return "Extremely High";
            }

            public override string Brittleness()
            {   // %bri
                if (parent.brittleness <= 25)
                    return "Extremely Low";
                else if (parent.brittleness <= 50 && parent.brittleness > 25)
                    return "Very Low";
                else if (parent.brittleness <= 75 && parent.brittleness > 50)
                    return "Low";
                else if (parent.brittleness <= 150 && parent.brittleness > 75)
                    return "Moderate";
                else if (parent.brittleness <= 175 && parent.brittleness > 150)
                    return "High";
                else if (parent.brittleness <= 200 && parent.brittleness > 175)
                    return "Very High";
                else
                    return "Extremely High";
            }
            
            public override string WeaponDamageBludgeoning()
            {   // %wdmb
                int minDamLowerLimit = FormulaHelper.CalculateWeaponMinDamTypeLowerLimit(parent, 1);
                int minDamUpperLimit = FormulaHelper.CalculateWeaponMinDamTypeUpperLimit(parent, 1);
                int maxDamLowerLimit = FormulaHelper.CalculateWeaponMaxDamTypeLowerLimit(parent, 1);
                int maxDamUpperLimit = FormulaHelper.CalculateWeaponMaxDamTypeUpperLimit(parent, 1);
                int matMod = parent.GetWeaponMaterialModDensity();
                float conditionMulti =  FormulaHelper.AlterDamageBasedOnWepCondition(parent, 1);
                return String.Format("{0} - {1}", Mathf.Clamp((int)Mathf.Round((parent.GetBaseBludgeoningDamageMin() + matMod) * conditionMulti), minDamLowerLimit, minDamUpperLimit), Mathf.Clamp((int)Mathf.Round((parent.GetBaseBludgeoningDamageMax() + matMod) * conditionMulti), maxDamLowerLimit, maxDamUpperLimit));
            }

            public override string WeaponDamageSlashing()
            {   // %wdms
                int minDamLowerLimit = FormulaHelper.CalculateWeaponMinDamTypeLowerLimit(parent, 2);
                int minDamUpperLimit = FormulaHelper.CalculateWeaponMinDamTypeUpperLimit(parent, 2);
                int maxDamLowerLimit = FormulaHelper.CalculateWeaponMaxDamTypeLowerLimit(parent, 2);
                int maxDamUpperLimit = FormulaHelper.CalculateWeaponMaxDamTypeUpperLimit(parent, 2);
                int matMod = parent.GetWeaponMaterialModShear();
                float conditionMulti = FormulaHelper.AlterDamageBasedOnWepCondition(parent, 2);
                return String.Format("{0} - {1}", Mathf.Clamp((int)Mathf.Round((parent.GetBaseSlashingDamageMin() + matMod) * conditionMulti), minDamLowerLimit, minDamUpperLimit), Mathf.Clamp((int)Mathf.Round((parent.GetBaseSlashingDamageMax() + matMod) * conditionMulti), maxDamLowerLimit, maxDamUpperLimit));
            }

            public override string WeaponDamagePiercing()
            {   // %wdmp
                int minDamLowerLimit = FormulaHelper.CalculateWeaponMinDamTypeLowerLimit(parent, 3);
                int minDamUpperLimit = FormulaHelper.CalculateWeaponMinDamTypeUpperLimit(parent, 3);
                int maxDamLowerLimit = FormulaHelper.CalculateWeaponMaxDamTypeLowerLimit(parent, 3);
                int maxDamUpperLimit = FormulaHelper.CalculateWeaponMaxDamTypeUpperLimit(parent, 3);
                int matMod = parent.GetWeaponMaterialModFracture();
                float conditionMulti = FormulaHelper.AlterDamageBasedOnWepCondition(parent, 3);
                return String.Format("{0} - {1}", Mathf.Clamp((int)Mathf.Round((parent.GetBasePiercingDamageMin() + matMod) * conditionMulti), minDamLowerLimit, minDamUpperLimit), Mathf.Clamp((int)Mathf.Round((parent.GetBasePiercingDamageMax() + matMod) * conditionMulti), maxDamLowerLimit, maxDamUpperLimit));
            }

            public override string WeaponDamage()
            {   // %wdm
                int matMod = parent.GetWeaponMaterialModifier();
                return String.Format("{0} - {1}", parent.GetBaseDamageMin() + matMod, parent.GetBaseDamageMax() + matMod);
            }

            public override string DamageReductionBludgeoning()
            {   // %dredb
                float damReduction = 0f;

                //if (parent.nativeMaterialValue < 512)
                    //return String.Format("{0}%", 0 * 100);

                if (parent.IsShield)
                    damReduction = FormulaHelper.PercentageDamageReductionCalculation(parent, true, 0f, 1);
                else
                    damReduction = FormulaHelper.PercentageDamageReductionCalculation(parent, false, 0f, 1);

                return String.Format("{0}%", (int)Mathf.Round((damReduction - 1) * -100f));
            }

            public override string DamageReductionSlashing()
            {   // %dreds
                float damReduction = 0f;

                //if (parent.nativeMaterialValue < 512)
                    //return String.Format("{0}%", 0 * 100);

                if (parent.IsShield)
                    damReduction = FormulaHelper.PercentageDamageReductionCalculation(parent, true, 0f, 2);
                else
                    damReduction = FormulaHelper.PercentageDamageReductionCalculation(parent, false, 0f, 2);

                return String.Format("{0}%", (int)Mathf.Round((damReduction - 1) * -100f));
            }

            public override string DamageReductionPiercing()
            {   // %dredp
                float damReduction = 0f;

                //if (parent.nativeMaterialValue < 512)
                    //return String.Format("{0}%", 0 * 100);

                if (parent.IsShield)
                    damReduction = FormulaHelper.PercentageDamageReductionCalculation(parent, true, 0f, 3);
                else
                    damReduction = FormulaHelper.PercentageDamageReductionCalculation(parent, false, 0f, 3);

                return String.Format("{0}%", (int)Mathf.Round((damReduction - 1) * -100f));
            }

            // Armour mod is double what classic displays, but this is correct according to Allofich.
            public override string ArmourMod()
            {   // %mod
                int matMod = parent.GetMaterialArmorValue();
                return String.Format("+{0}%", matMod);
            }

            public override string BookAuthor()
            {   // %ba
                BookFile bookFile = new BookFile();

                string name = GameManager.Instance.ItemHelper.GetBookFileName(parent.message);
                if (name != null)
                {
                    if (!BookReplacement.TryImportBook(name, bookFile))
                        bookFile.OpenBook(DaggerfallUnity.Instance.Arena2Path, name);

                    if (bookFile.Author != null)
                        return bookFile.Author;
                }

                return TextManager.Instance.GetLocalizedText("unknownAuthor");
            }

            public override string PaintingSubject()
            {   // %sub
                DFRandom.rand(); // Classic uses every other value.
                TextFile.Token[] tokens = DaggerfallUnity.Instance.TextProvider.GetRandomTokens(paintingSub, true);
                return (tokens.Length > 0) ? tokens[0].text : "%sub[idxError]";
            }
            public override string PaintingAdjective()
            {   // %adj
                DFRandom.rand(); // Classic uses every other value.
                TextFile.Token[] tokens = DaggerfallUnity.Instance.TextProvider.GetRandomTokens(paintingAdj, true);
                MacroHelper.ExpandMacros(ref tokens);
                return (tokens.Length > 0) ? tokens[0].text : "%adj[idxError]";
            }
            public override string PaintingPrefix1()
            {   // %pp1
                DFRandom.rand(); // Classic uses every other value.
                TextFile.Token[] tokens = DaggerfallUnity.Instance.TextProvider.GetRandomTokens(paintingPp1, true);
                return (tokens.Length > 0) ? tokens[0].text : "%pp1[idxError]";
            }
            public override string PaintingPrefix2()
            {   // %pp2
                DFRandom.rand(); // Classic uses every other value.
                TextFile.Token[] tokens = DaggerfallUnity.Instance.TextProvider.GetRandomTokens(paintingPp2, true);
                MacroHelper.ExpandMacros(ref tokens);
                return (tokens.Length > 0) ? tokens[0].text : "%pp2[idxError]";
            }
            public override string ArtistName()
            {   // %an
                DFRandom.rand(); // Classic uses every other value.
                uint rand = DFRandom.rand() & 1;
                Genders gender = (Genders)rand;
                rand = DFRandom.rand();
                NameHelper.BankTypes race = (NameHelper.BankTypes)(rand & 7);
                return DaggerfallUnity.Instance.NameHelper.FullName(race, gender);
            }

            public override string HeldSoul()
            {   // %hs
                if (parent.trappedSoulType == MobileTypes.None)
                    return TextManager.Instance.GetLocalizedText("Nothing");
                MobileEnemy soul;
                EnemyBasics.GetEnemy(parent.trappedSoulType, out soul);
                return TextManager.Instance.GetLocalizedEnemyName(soul.ID);
            }

            public override string Potion()
            {   // %po
                string potionName = PotionRecipe.UnknownPowers;
                PotionRecipe potionRecipe = GameManager.Instance.EntityEffectBroker.GetPotionRecipe(parent.potionRecipeKey);
                if (potionRecipe != null)
                    potionName = potionRecipe.DisplayName;

                if (parent.IsPotionRecipe)
                    return potionName;                                          // "Potion recipe for %po"
                else if (parent.IsPotion)
                    return TextManager.Instance.GetLocalizedText("potionOf").Replace("%po", potionName);     // "Potion of %po" (255=Unknown Powers)

                throw new NotImplementedException();
            }

            public override TextFile.Token[] PotionRecipeIngredients(TextFile.Formatting format)
            {
                List<TextFile.Token> ingredientsTokens = new List<TextFile.Token>();
                PotionRecipe potionRecipe = GameManager.Instance.EntityEffectBroker.GetPotionRecipe(parent.potionRecipeKey);
                if (potionRecipe != null)
                {
                    foreach (PotionRecipe.Ingredient ingredient in potionRecipe.Ingredients)
                    {
                        ItemTemplate ingredientTemplate = DaggerfallUnity.Instance.ItemHelper.GetItemTemplate(ingredient.id);
                        ingredientsTokens.Add(TextFile.CreateTextToken(ingredientTemplate.name));
                        ingredientsTokens.Add(TextFile.CreateFormatToken(format));
                    }
                }
                return ingredientsTokens.ToArray();
            }

            public override TextFile.Token[] MagicPowers(TextFile.Formatting format)
            {   // %mpw
                if (parent.IsArtifact)
                {
                    // Use appropriate artifact description message. (8700-8721)
                    try {
                        ArtifactsSubTypes artifactType = ItemHelper.GetArtifactSubType(parent.shortName);
                        return DaggerfallUnity.Instance.TextProvider.GetRSCTokens(8700 + (int)artifactType);
                    } catch (KeyNotFoundException e) {
                        Debug.Log(e.Message);
                        return null;
                    }
                }
                else if (!parent.IsIdentified)
                {
                    // Powers unknown.
                    TextFile.Token nopowersToken = TextFile.CreateTextToken(TextManager.Instance.GetLocalizedText("powersUnknown"));
                    return new TextFile.Token[] { nopowersToken };
                }
                else
                {
                    // List item powers. 
                    List<TextFile.Token> magicPowersTokens = new List<TextFile.Token>();
                    for (int i = 0; i < parent.legacyMagic.Length; i++)
                    {
                        // Also 65535 to handle saves from when the type was read as an unsigned value
                        if (parent.legacyMagic[i].type == EnchantmentTypes.None || (int)parent.legacyMagic[i].type == 65535)
                            break;

                        string firstPart = TextManager.Instance.GetLocalizedTextList("itemPowers")[(int)parent.legacyMagic[i].type] + " ";

                        if (parent.legacyMagic[i].type == EnchantmentTypes.SoulBound && parent.legacyMagic[i].param != -1)
                        {
                            magicPowersTokens.Add(TextFile.CreateTextToken(firstPart + TextManager.Instance.GetLocalizedTextList("enemyNames")[parent.legacyMagic[i].param]));
                        }
                        else if (parent.legacyMagic[i].type == EnchantmentTypes.ExtraSpellPts)
                        {
                            magicPowersTokens.Add(TextFile.CreateTextToken(firstPart + TextManager.Instance.GetLocalizedTextList("extraSpellPtsTimes")[parent.legacyMagic[i].param]));
                        }
                        else if (parent.legacyMagic[i].type == EnchantmentTypes.PotentVs || parent.legacyMagic[i].type == EnchantmentTypes.LowDamageVs)
                        {
                            magicPowersTokens.Add(TextFile.CreateTextToken(firstPart + TextManager.Instance.GetLocalizedTextList("enemyGroupNames")[parent.legacyMagic[i].param]));
                        }
                        else if (parent.legacyMagic[i].type == EnchantmentTypes.RegensHealth)
                        {
                            magicPowersTokens.Add(TextFile.CreateTextToken(firstPart + TextManager.Instance.GetLocalizedTextList("regensHealthTimes")[parent.legacyMagic[i].param]));
                        }
                        else if (parent.legacyMagic[i].type == EnchantmentTypes.VampiricEffect)
                        {
                            magicPowersTokens.Add(TextFile.CreateTextToken(firstPart + TextManager.Instance.GetLocalizedTextList("vampiricEffectRanges")[parent.legacyMagic[i].param]));
                        }
                        else if (parent.legacyMagic[i].type == EnchantmentTypes.IncreasedWeightAllowance)
                        {
                            magicPowersTokens.Add(TextFile.CreateTextToken(firstPart + TextManager.Instance.GetLocalizedTextList("increasedWeightAllowances")[parent.legacyMagic[i].param]));
                        }
                        else if (parent.legacyMagic[i].type == EnchantmentTypes.EnhancesSkill)
                        {
                            magicPowersTokens.Add(TextFile.CreateTextToken(firstPart + DaggerfallUnity.Instance.TextProvider.GetSkillName((DaggerfallConnect.DFCareer.Skills)parent.legacyMagic[i].param)));
                        }
                        else if (parent.legacyMagic[i].type == EnchantmentTypes.ImprovesTalents)
                        {
                            magicPowersTokens.Add(TextFile.CreateTextToken(firstPart + TextManager.Instance.GetLocalizedTextList("improvedTalents")[parent.legacyMagic[i].param]));
                        }
                        else if (parent.legacyMagic[i].type == EnchantmentTypes.GoodRepWith || parent.legacyMagic[i].type == EnchantmentTypes.BadRepWith)
                        {
                            magicPowersTokens.Add(TextFile.CreateTextToken(firstPart + TextManager.Instance.GetLocalizedTextList("repWithGroups")[parent.legacyMagic[i].param]));
                        }
                        else if (parent.legacyMagic[i].type == EnchantmentTypes.ItemDeteriorates)
                        {
                            magicPowersTokens.Add(TextFile.CreateTextToken(firstPart + TextManager.Instance.GetLocalizedTextList("itemDeteriorateLocations")[parent.legacyMagic[i].param]));
                        }
                        else if (parent.legacyMagic[i].type == EnchantmentTypes.UserTakesDamage)
                        {
                            magicPowersTokens.Add(TextFile.CreateTextToken(firstPart + TextManager.Instance.GetLocalizedTextList("userTakesDamageLocations")[parent.legacyMagic[i].param]));
                        }
                        else if (parent.legacyMagic[i].type == EnchantmentTypes.HealthLeech)
                        {
                            magicPowersTokens.Add(TextFile.CreateTextToken(firstPart + TextManager.Instance.GetLocalizedTextList("healthLeechStopConditions")[parent.legacyMagic[i].param]));
                        }
                        else if (parent.legacyMagic[i].type == EnchantmentTypes.BadReactionsFrom)
                        {
                            magicPowersTokens.Add(TextFile.CreateTextToken(firstPart + TextManager.Instance.GetLocalizedTextList("badReactionFromEnemyGroups")[parent.legacyMagic[i].param]));
                        }
                        else if (parent.legacyMagic[i].type <= EnchantmentTypes.CastWhenStrikes)
                        {
                            List<DaggerfallConnect.Save.SpellRecord.SpellRecordData> spells = DaggerfallSpellReader.ReadSpellsFile();
                            bool found = false;

                            foreach (DaggerfallConnect.Save.SpellRecord.SpellRecordData spell in spells)
                            {
                                if (spell.index == parent.legacyMagic[i].param)
                                {
                                    magicPowersTokens.Add(TextFile.CreateTextToken(firstPart + spell.spellName));
                                    found = true;
                                    break;
                                }
                            }

                            if (!found)
                                magicPowersTokens.Add(TextFile.CreateTextToken(firstPart + "ERROR"));
                        }
                        else
                        {
                            magicPowersTokens.Add(TextFile.CreateTextToken(firstPart));
                        }

                        magicPowersTokens.Add(TextFile.CreateFormatToken(format));
                    }
                    return magicPowersTokens.ToArray();
                }
            }

        }
    }
}
