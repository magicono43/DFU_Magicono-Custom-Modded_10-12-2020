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
    /// Fortify Attribute - Intelligence
    /// </summary>
    public class FortifyIntelligence : FortifyEffect
    {
        public static readonly string EffectKey = "Fortify-Intelligence";

        public override void SetProperties()
        {
            properties.Key = EffectKey;
            properties.ClassicKey = MakeClassicKey(9, 1);
            properties.SupportDuration = true;
            properties.SupportMagnitude = true;
            properties.AllowedTargets = EntityEffectBroker.TargetFlags_All;
            properties.AllowedElements = EntityEffectBroker.ElementFlags_MagicOnly;
            properties.AllowedCraftingStations = MagicCraftingStations.SpellMaker | MagicCraftingStations.PotionMaker;
            properties.MagicSkill = DFCareer.MagicSkills.Restoration;
            properties.DurationCosts = MakeEffectCosts(28, 100);
            properties.MagnitudeCosts = MakeEffectCosts(40, 120);
            fortifyStat = DFCareer.Stats.Intelligence;
        }

        public override string GroupName => TextManager.Instance.GetLocalizedText("fortifyAttribute");
        public override string SubGroupName => TextManager.Instance.GetLocalizedText("intelligence");
        public override TextFile.Token[] SpellMakerDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1533);
        public override TextFile.Token[] SpellBookDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1233);

        public override void SetPotionProperties()
        {
            // Duration 25 + 0 per 1 levels, Magnitude 20-20 + 0-0 per 1 levels
            EffectSettings scholarWitSettings = SetEffectDuration(DefaultEffectSettings(), 25, 0, 1);
            scholarWitSettings = SetEffectMagnitude(scholarWitSettings, 20, 20, 0, 0, 1);
            PotionRecipe scholarWit = new PotionRecipe(
                "Scholar's Wit",
                49,
                0,
                scholarWitSettings,
                (int)Items.SolventIngredients.Rain_water,
                (int)Items.FlowerPlantIngredients.Black_rose,
                (int)Items.MiscPlantIngredients.Ginkgo_leaves,
                (int)Items.MiscPlantIngredients.Pine_branch,
                (int)Items.AnimalPartIngredients.Pearl,
                (int)Items.CreatureIngredients.Harpy_Feather,
                (int)Items.MetalIngredients.Tin);

            // Duration 30 + 0 per 1 levels, Magnitude 45-45 + 0-0 per 1 levels
            EffectSettings julianosWisdomSettings = SetEffectDuration(DefaultEffectSettings(), 30, 0, 1);
            julianosWisdomSettings = SetEffectMagnitude(julianosWisdomSettings, 45, 45, 0, 0, 1);
            PotionRecipe julianosWisdom = new PotionRecipe(
                "Julianos' Wisdom",
                175,
                0,
                julianosWisdomSettings,
                (int)Items.SolventIngredients.Pure_water,
                (int)Items.FlowerPlantIngredients.Black_poppy,
                (int)Items.MiscPlantIngredients.Green_leaves,
                (int)Items.CreatureIngredients.Lich_dust,
                (int)Items.CreatureIngredients.Basilisk_eye);

            // Assign recipe
            scholarWit.TextureRecord = 34;
            julianosWisdom.TextureRecord = 7;
            AssignPotionRecipes(scholarWit, julianosWisdom);
        }
    }
}
