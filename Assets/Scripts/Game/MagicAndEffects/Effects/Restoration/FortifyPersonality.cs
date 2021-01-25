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
    /// Fortify Attribute - Personality
    /// </summary>
    public class FortifyPersonality : FortifyEffect
    {
        public static readonly string EffectKey = "Fortify-Personality";

        public override void SetProperties()
        {
            properties.Key = EffectKey;
            properties.ClassicKey = MakeClassicKey(9, 5);
            properties.SupportDuration = true;
            properties.SupportMagnitude = true;
            properties.AllowedTargets = EntityEffectBroker.TargetFlags_All;
            properties.AllowedElements = EntityEffectBroker.ElementFlags_MagicOnly;
            properties.AllowedCraftingStations = MagicCraftingStations.SpellMaker | MagicCraftingStations.PotionMaker;
            properties.MagicSkill = DFCareer.MagicSkills.Restoration;
            properties.DurationCosts = MakeEffectCosts(28, 100);
            properties.MagnitudeCosts = MakeEffectCosts(40, 120);
            fortifyStat = DFCareer.Stats.Personality;
        }

        public override string GroupName => TextManager.Instance.GetLocalizedText("fortifyAttribute");
        public override string SubGroupName => TextManager.Instance.GetLocalizedText("personality");
        public override TextFile.Token[] SpellMakerDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1537);
        public override TextFile.Token[] SpellBookDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1237);

        public override void SetPotionProperties()
        {
            // Duration 25 + 0 per 1 levels, Magnitude 20-20 + 0-0 per 1 levels
            EffectSettings CharismaSettings = SetEffectDuration(DefaultEffectSettings(), 25, 0, 1);
            CharismaSettings = SetEffectMagnitude(CharismaSettings, 20, 20, 0, 0, 1);
            PotionRecipe Charisma = new PotionRecipe(
                "Charisma",
                32,
                0,
                40,
                CharismaSettings,
                (int)Items.SolventIngredients.Rain_water,
                (int)Items.FlowerPlantIngredients.Red_rose,
                (int)Items.FlowerPlantIngredients.Yellow_rose,
                (int)Items.MiscPlantIngredients.Aloe,
                (int)Items.CreatureIngredients.Ghouls_tongue,
                (int)Items.MetalIngredients.Lead);

            // Duration 30 + 0 per 1 levels, Magnitude 45-45 + 0-0 per 1 levels
            EffectSettings dibellaAllureSettings = SetEffectDuration(DefaultEffectSettings(), 30, 0, 1);
            dibellaAllureSettings = SetEffectMagnitude(dibellaAllureSettings, 45, 45, 0, 0, 1);
            PotionRecipe dibellaAllure = new PotionRecipe(
                "Dibella's Allure",
                96,
                0,
                20,
                dibellaAllureSettings,
                (int)Items.SolventIngredients.Nectar,
                (int)Items.FlowerPlantIngredients.Red_rose,
                (int)Items.AnimalPartIngredients.Snake_venom,
                (int)Items.CreatureIngredients.Nymph_hair,
                (int)Items.CreatureIngredients.Ghouls_tongue,
                (int)Items.CreatureIngredients.Gorgon_snake,
                (int)Items.MetalIngredients.Lodestone);

            // Assign recipe
            Charisma.TextureRecord = 33;
            dibellaAllure.TextureRecord = 7;
            AssignPotionRecipes(Charisma, dibellaAllure);
        }
    }
}
