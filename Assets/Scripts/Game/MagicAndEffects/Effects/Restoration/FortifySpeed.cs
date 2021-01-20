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
    /// Fortify Attribute - Speed
    /// </summary>
    public class FortifySpeed : FortifyEffect
    {
        public static readonly string EffectKey = "Fortify-Speed";

        public override void SetProperties()
        {
            properties.Key = EffectKey;
            properties.ClassicKey = MakeClassicKey(9, 6);
            properties.SupportDuration = true;
            properties.SupportMagnitude = true;
            properties.AllowedTargets = EntityEffectBroker.TargetFlags_All;
            properties.AllowedElements = EntityEffectBroker.ElementFlags_MagicOnly;
            properties.AllowedCraftingStations = MagicCraftingStations.SpellMaker | MagicCraftingStations.PotionMaker;
            properties.MagicSkill = DFCareer.MagicSkills.Restoration;
            properties.DurationCosts = MakeEffectCosts(28, 100);
            properties.MagnitudeCosts = MakeEffectCosts(40, 120);
            fortifyStat = DFCareer.Stats.Speed;
        }

        public override string GroupName => TextManager.Instance.GetLocalizedText("fortifyAttribute");
        public override string SubGroupName => TextManager.Instance.GetLocalizedText("speed");
        public override TextFile.Token[] SpellMakerDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1538);
        public override TextFile.Token[] SpellBookDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1238);

        public override void SetPotionProperties()
        {
            // Duration 25 + 0 per 1 levels, Magnitude 20-20 + 0-0 per 1 levels
            EffectSettings hareSpeedSettings = SetEffectDuration(DefaultEffectSettings(), 25, 0, 1);
            hareSpeedSettings = SetEffectMagnitude(hareSpeedSettings, 20, 20, 0, 0, 1);
            PotionRecipe hareSpeed = new PotionRecipe(
                "Hare Speed",
                33,
                0,
                hareSpeedSettings,
                (int)Items.SolventIngredients.Pure_water,
                (int)Items.FlowerPlantIngredients.Red_poppy,
                (int)Items.FlowerPlantIngredients.Yellow_Flowers,
                (int)Items.MiscPlantIngredients.Bamboo,
                (int)Items.AnimalPartIngredients.Small_scorpion_stinger,
                (int)Items.AnimalPartIngredients.Small_tooth,
                (int)Items.MetalIngredients.Copper);

            // Duration 30 + 0 per 1 levels, Magnitude 45-45 + 0-0 per 1 levels
            EffectSettings kynarethSwiftnessSettings = SetEffectDuration(DefaultEffectSettings(), 30, 0, 1);
            kynarethSwiftnessSettings = SetEffectMagnitude(kynarethSwiftnessSettings, 45, 45, 0, 0, 1);
            PotionRecipe kynarethSwiftness = new PotionRecipe(
                "Kynareth's Swiftness",
                185,
                0,
                kynarethSwiftnessSettings,
                (int)Items.SolventIngredients.Pure_water,
                (int)Items.FlowerPlantIngredients.Yellow_Flowers,
                (int)Items.MiscPlantIngredients.Bamboo,
                (int)Items.CreatureIngredients.Harpy_Feather,
                (int)Items.CreatureIngredients.Werewolfs_blood,
                (int)Items.CreatureIngredients.Fairy_dragon_scales,
                (int)Items.MetalIngredients.Mercury);

            // Assign recipe
            hareSpeed.TextureRecord = 35;
            kynarethSwiftness.TextureRecord = 32;
            AssignPotionRecipes(hareSpeed, kynarethSwiftness);
        }
    }
}
