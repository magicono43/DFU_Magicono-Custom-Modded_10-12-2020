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
    /// Fortify Attribute - Endurance
    /// </summary>
    public class FortifyEndurance : FortifyEffect
    {
        public static readonly string EffectKey = "Fortify-Endurance";

        public override void SetProperties()
        {
            properties.Key = EffectKey;
            properties.ClassicKey = MakeClassicKey(9, 4);
            properties.SupportDuration = true;
            properties.SupportMagnitude = true;
            properties.AllowedTargets = EntityEffectBroker.TargetFlags_All;
            properties.AllowedElements = EntityEffectBroker.ElementFlags_MagicOnly;
            properties.AllowedCraftingStations = MagicCraftingStations.SpellMaker | MagicCraftingStations.PotionMaker;
            properties.MagicSkill = DFCareer.MagicSkills.Restoration;
            properties.DurationCosts = MakeEffectCosts(28, 100);
            properties.MagnitudeCosts = MakeEffectCosts(40, 120);
            fortifyStat = DFCareer.Stats.Endurance;
        }

        public override string GroupName => TextManager.Instance.GetLocalizedText("fortifyAttribute");
        public override string SubGroupName => TextManager.Instance.GetLocalizedText("endurance");
        public override TextFile.Token[] SpellMakerDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1536);
        public override TextFile.Token[] SpellBookDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1236);

        public override void SetPotionProperties()
        {
            // Duration 25 + 0 per 1 levels, Magnitude 20-20 + 0-0 per 1 levels
            EffectSettings bearFortitudeSettings = SetEffectDuration(DefaultEffectSettings(), 25, 0, 1);
            bearFortitudeSettings = SetEffectMagnitude(bearFortitudeSettings, 20, 20, 0, 0, 1);
            PotionRecipe bearFortitude = new PotionRecipe(
                "Bear's Fortitude",
                22,
                0,
                40,
                bearFortitudeSettings,
                (int)Items.SolventIngredients.Nectar,
                (int)Items.FlowerPlantIngredients.Red_rose,
                (int)Items.MiscPlantIngredients.Root_bulb,
                (int)Items.AnimalPartIngredients.Big_tooth,
                (int)Items.MetalIngredients.Brass);

            // Duration 30 + 0 per 1 levels, Magnitude 45-45 + 0-0 per 1 levels
            EffectSettings sanguineEnduranceSettings = SetEffectDuration(DefaultEffectSettings(), 30, 0, 1);
            sanguineEnduranceSettings = SetEffectMagnitude(sanguineEnduranceSettings, 45, 45, 0, 0, 1);
            PotionRecipe sanguineEndurance = new PotionRecipe(
                "Sanguine's Endurance",
                80,
                0,
                20,
                sanguineEnduranceSettings,
                (int)Items.SolventIngredients.Nectar,
                (int)Items.FlowerPlantIngredients.Red_rose,
                (int)Items.AnimalPartIngredients.Big_tooth,
                (int)Items.CreatureIngredients.Nymph_hair,
                (int)Items.CreatureIngredients.Wereboar_tusk,
                (int)Items.MetalIngredients.Brass);

            // Assign recipe
            bearFortitude.TextureRecord = 16;
            sanguineEndurance.TextureRecord = 6;
            AssignPotionRecipes(bearFortitude, sanguineEndurance);
        }
    }
}
