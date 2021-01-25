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
    /// Fortify Attribute - Luck
    /// </summary>
    public class FortifyLuck : FortifyEffect
    {
        public static readonly string EffectKey = "Fortify-Luck";

        public override void SetProperties()
        {
            properties.Key = EffectKey;
            properties.ClassicKey = MakeClassicKey(9, 7);
            properties.SupportDuration = true;
            properties.SupportMagnitude = true;
            properties.AllowedTargets = EntityEffectBroker.TargetFlags_All;
            properties.AllowedElements = EntityEffectBroker.ElementFlags_MagicOnly;
            properties.AllowedCraftingStations = MagicCraftingStations.SpellMaker | MagicCraftingStations.PotionMaker;
            properties.MagicSkill = DFCareer.MagicSkills.Restoration;
            properties.DurationCosts = MakeEffectCosts(28, 100);
            properties.MagnitudeCosts = MakeEffectCosts(40, 120);
            fortifyStat = DFCareer.Stats.Luck;
        }

        public override string GroupName => TextManager.Instance.GetLocalizedText("fortifyAttribute");
        public override string SubGroupName => TextManager.Instance.GetLocalizedText("luck");
        public override TextFile.Token[] SpellMakerDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1539);
        public override TextFile.Token[] SpellBookDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1239);

        public override void SetPotionProperties()
        {
            // Duration 25 + 0 per 1 levels, Magnitude 20-20 + 0-0 per 1 levels
            EffectSettings dumbLuckSettings = SetEffectDuration(DefaultEffectSettings(), 25, 0, 1);
            dumbLuckSettings = SetEffectMagnitude(dumbLuckSettings, 20, 20, 0, 0, 1);
            PotionRecipe dumbLuck = new PotionRecipe(
                "Dumb Luck",
                57,
                0,
                20,
                dumbLuckSettings,
                (int)Items.SolventIngredients.Rain_water,
                (int)Items.FlowerPlantIngredients.Clover,
                (int)Items.FlowerPlantIngredients.Golden_poppy,
                (int)Items.AnimalPartIngredients.Ivory,
                (int)Items.CreatureIngredients.Nymph_hair,
                (int)Items.MetalIngredients.Gold);

            // Duration 30 + 0 per 1 levels, Magnitude 45-45 + 0-0 per 1 levels
            EffectSettings zenitharFortuneSettings = SetEffectDuration(DefaultEffectSettings(), 30, 0, 1);
            zenitharFortuneSettings = SetEffectMagnitude(zenitharFortuneSettings, 45, 45, 0, 0, 1);
            PotionRecipe zenitharFortune = new PotionRecipe(
                "Zenithar's Fortune",
                145,
                0,
                3,
                zenitharFortuneSettings,
                (int)Items.SolventIngredients.Nectar,
                (int)Items.FlowerPlantIngredients.Clover,
                (int)Items.CreatureIngredients.Werewolfs_blood,
                (int)Items.CreatureIngredients.Giant_blood,
                (int)Items.MetalIngredients.Silver,
                (int)Items.MetalIngredients.Gold,
                (int)Items.MetalIngredients.Platinum,
                (int)Items.Gems.Jade);

            // Assign recipe
            dumbLuck.TextureRecord = 34;
            zenitharFortune.TextureRecord = 1;
            AssignPotionRecipes(dumbLuck, zenitharFortune);
        }
    }
}
