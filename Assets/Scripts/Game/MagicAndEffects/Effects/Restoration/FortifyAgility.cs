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

using DaggerfallConnect;
using DaggerfallConnect.Arena2;

namespace DaggerfallWorkshop.Game.MagicAndEffects.MagicEffects
{
    /// <summary>
    /// Fortify Attribute - Agility
    /// </summary>
    public class FortifyAgility : FortifyEffect
    {
        public static readonly string EffectKey = "Fortify-Agility";

        public override void SetProperties()
        {
            properties.Key = EffectKey;
            properties.ClassicKey = MakeClassicKey(9, 3);
            properties.SupportDuration = true;
            properties.SupportMagnitude = true;
            properties.AllowedTargets = EntityEffectBroker.TargetFlags_All;
            properties.AllowedElements = EntityEffectBroker.ElementFlags_MagicOnly;
            properties.AllowedCraftingStations = MagicCraftingStations.SpellMaker | MagicCraftingStations.PotionMaker;
            properties.MagicSkill = DFCareer.MagicSkills.Restoration;
            properties.DurationCosts = MakeEffectCosts(28, 100);
            properties.MagnitudeCosts = MakeEffectCosts(40, 120);
            fortifyStat = DFCareer.Stats.Agility;
        }

        public override string GroupName => TextManager.Instance.GetLocalizedText("fortifyAttribute");
        public override string SubGroupName => TextManager.Instance.GetLocalizedText("agility");
        public override TextFile.Token[] SpellMakerDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1535);
        public override TextFile.Token[] SpellBookDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1235);

        public override void SetPotionProperties()
        {
            // Duration 25 + 0 per 1 levels, Magnitude 20-20 + 0-0 per 1 levels
            EffectSettings acrobatAgilitySettings = SetEffectDuration(DefaultEffectSettings(), 25, 0, 1);
            acrobatAgilitySettings = SetEffectMagnitude(acrobatAgilitySettings, 20, 20, 0, 0, 1);
            PotionRecipe acrobatAgility = new PotionRecipe(
                "Acrobat's Agility",
                25,
                0,
                45,
                acrobatAgilitySettings,
                (int)Items.SolventIngredients.Pure_water,
                (int)Items.FlowerPlantIngredients.Yellow_rose,
                (int)Items.FlowerPlantIngredients.Golden_poppy,
                (int)Items.MiscPlantIngredients.Bamboo,
                (int)Items.CreatureIngredients.Harpy_Feather);

            // Duration 30 + 0 per 1 levels, Magnitude 45-45 + 0-0 per 1 levels
            EffectSettings kynarethGraceSettings = SetEffectDuration(DefaultEffectSettings(), 30, 0, 1);
            kynarethGraceSettings = SetEffectMagnitude(kynarethGraceSettings, 45, 45, 0, 0, 1);
            PotionRecipe kynarethGrace = new PotionRecipe(
                "Kynareth's Grace",
                90,
                0,
                90,
                kynarethGraceSettings,
                (int)Items.SolventIngredients.Pure_water,
                (int)Items.FlowerPlantIngredients.Yellow_rose,
                (int)Items.MiscPlantIngredients.Bamboo,
                (int)Items.CreatureIngredients.Harpy_Feather,
                (int)Items.CreatureIngredients.Harpy_Feather,
                (int)Items.CreatureIngredients.Ectoplasm);

            // Assign recipe
            acrobatAgility.TextureRecord = 1;
            kynarethGrace.TextureRecord = 33;
            AssignPotionRecipes(acrobatAgility, kynarethGrace);
        }
    }
}
