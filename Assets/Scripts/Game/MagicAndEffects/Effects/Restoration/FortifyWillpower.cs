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
    /// Fortify Attribute - Willpower
    /// </summary>
    public class FortifyWillpower : FortifyEffect
    {
        public static readonly string EffectKey = "Fortify-Willpower";

        public override void SetProperties()
        {
            properties.Key = EffectKey;
            properties.ClassicKey = MakeClassicKey(9, 2);
            properties.SupportDuration = true;
            properties.SupportMagnitude = true;
            properties.AllowedTargets = EntityEffectBroker.TargetFlags_All;
            properties.AllowedElements = EntityEffectBroker.ElementFlags_MagicOnly;
            properties.AllowedCraftingStations = MagicCraftingStations.SpellMaker | MagicCraftingStations.PotionMaker;
            properties.MagicSkill = DFCareer.MagicSkills.Restoration;
            properties.DurationCosts = MakeEffectCosts(28, 100);
            properties.MagnitudeCosts = MakeEffectCosts(40, 120);
            fortifyStat = DFCareer.Stats.Willpower;
        }

        public override string GroupName => TextManager.Instance.GetLocalizedText("fortifyAttribute");
        public override string SubGroupName => TextManager.Instance.GetLocalizedText("willpower");
        public override TextFile.Token[] SpellMakerDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1534);
        public override TextFile.Token[] SpellBookDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1234);

        public override void SetPotionProperties()
        {
            // Duration 25 + 0 per 1 levels, Magnitude 20-20 + 0-0 per 1 levels
            EffectSettings ironWillSettings = SetEffectDuration(DefaultEffectSettings(), 25, 0, 1);
            ironWillSettings = SetEffectMagnitude(ironWillSettings, 20, 20, 0, 0, 1);
            PotionRecipe ironWill = new PotionRecipe(
                "Iron Will",
                35,
                0,
                30,
                ironWillSettings,
                (int)Items.SolventIngredients.Nectar,
                (int)Items.FlowerPlantIngredients.Red_Flowers,
                (int)Items.MiscPlantIngredients.Root_bulb,
                (int)Items.MiscPlantIngredients.Twigs,
                (int)Items.AnimalPartIngredients.Big_tooth,
                (int)Items.CreatureIngredients.Orcs_blood,
                (int)Items.MetalIngredients.Iron);

            // Duration 30 + 0 per 1 levels, Magnitude 45-45 + 0-0 per 1 levels
            EffectSettings unwaveringDeterminationSettings = SetEffectDuration(DefaultEffectSettings(), 30, 0, 1);
            unwaveringDeterminationSettings = SetEffectMagnitude(unwaveringDeterminationSettings, 45, 45, 0, 0, 1);
            PotionRecipe unwaveringDetermination = new PotionRecipe(
                "Unwavering Determination",
                115,
                0,
                10,
                unwaveringDeterminationSettings,
                (int)Items.SolventIngredients.Nectar,
                (int)Items.MiscPlantIngredients.Root_bulb,
                (int)Items.CreatureIngredients.Orcs_blood,
                (int)Items.CreatureIngredients.Giant_blood,
                (int)Items.CreatureIngredients.Wereboar_tusk,
                (int)Items.MetalIngredients.Iron,
                (int)Items.MetalIngredients.Brass,
                (int)Items.Gems.Jade);

            // Assign recipe
            ironWill.TextureRecord = 1;
            unwaveringDetermination.TextureRecord = 3;
            AssignPotionRecipes(ironWill, unwaveringDetermination);
        }
    }
}
