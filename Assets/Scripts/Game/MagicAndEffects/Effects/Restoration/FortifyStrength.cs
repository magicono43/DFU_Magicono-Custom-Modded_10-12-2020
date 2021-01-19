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
    /// Fortify Attribute - Strength
    /// </summary>
    public class FortifyStrength : FortifyEffect
    {
        public static readonly string EffectKey = "Fortify-Strength";

        public override void SetProperties()
        {
            properties.Key = EffectKey;
            properties.ClassicKey = MakeClassicKey(9, 0);
            properties.SupportDuration = true;
            properties.SupportMagnitude = true;
            properties.AllowedTargets = EntityEffectBroker.TargetFlags_All;
            properties.AllowedElements = EntityEffectBroker.ElementFlags_MagicOnly;
            properties.AllowedCraftingStations = MagicCraftingStations.SpellMaker | MagicCraftingStations.PotionMaker;
            properties.MagicSkill = DFCareer.MagicSkills.Restoration;
            properties.DurationCosts = MakeEffectCosts(28, 100);
            properties.MagnitudeCosts = MakeEffectCosts(40, 120);
            fortifyStat = DFCareer.Stats.Strength;
        }

        public override string GroupName => TextManager.Instance.GetLocalizedText("fortifyAttribute");
        public override string SubGroupName => TextManager.Instance.GetLocalizedText("strength");
        public override TextFile.Token[] SpellMakerDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1532);
        public override TextFile.Token[] SpellBookDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1232);

        public override void SetPotionProperties()
        {
            // Duration 25 + 0 per 1 levels, Magnitude 20-20 + 0-0 per 1 levels
            EffectSettings orcStrengthSettings = SetEffectDuration(DefaultEffectSettings(), 25, 0, 1);
            orcStrengthSettings = SetEffectMagnitude(orcStrengthSettings, 20, 20, 0, 0, 1);
            PotionRecipe orcStrength = new PotionRecipe(
                "Orc Strength",
                30,
                0,
                orcStrengthSettings,
                (int)Items.SolventIngredients.Pure_water,
                (int)Items.MiscPlantIngredients.Root_tendrils,
                (int)Items.AnimalPartIngredients.Big_tooth,
                (int)Items.CreatureIngredients.Orcs_blood,
                (int)Items.MetalIngredients.Iron);

            // Duration 30 + 0 per 1 levels, Magnitude 45-45 + 0-0 per 1 levels
            EffectSettings giantStrengthSettings = SetEffectDuration(DefaultEffectSettings(), 30, 0, 1);
            giantStrengthSettings = SetEffectMagnitude(giantStrengthSettings, 45, 45, 0, 0, 1);
            PotionRecipe giantStrength = new PotionRecipe(
                "Giant Strength",
                75,
                0,
                giantStrengthSettings,
                (int)Items.SolventIngredients.Pure_water,
                (int)Items.MiscPlantIngredients.Root_tendrils,
                (int)Items.AnimalPartIngredients.Big_tooth,
                (int)Items.CreatureIngredients.Giant_blood,
                (int)Items.CreatureIngredients.Giant_blood,
                (int)Items.MetalIngredients.Iron,
                (int)Items.MetalIngredients.Lodestone);

            // Assign recipe
            orcStrength.TextureRecord = 13;
            giantStrength.TextureRecord = 6;
            AssignPotionRecipes(orcStrength, giantStrength);
        }
    }
}
