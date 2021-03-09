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
using DaggerfallWorkshop.Game.Entity;

namespace DaggerfallWorkshop.Game.MagicAndEffects.MagicEffects
{
    /// <summary>
    /// Heal - Health
    /// </summary>
    public class HealHealth : BaseEntityEffect
    {
        public static readonly string EffectKey = "Heal-Health";

        public override void SetProperties()
        {
            properties.Key = EffectKey;
            properties.ClassicKey = MakeClassicKey(10, 8);
            properties.SupportMagnitude = true;
            properties.AllowedTargets = EntityEffectBroker.TargetFlags_All;
            properties.AllowedElements = EntityEffectBroker.ElementFlags_MagicOnly;
            properties.AllowedCraftingStations = MagicCraftingStations.SpellMaker | MagicCraftingStations.PotionMaker;
            properties.MagicSkill = DFCareer.MagicSkills.Restoration;
            properties.MagnitudeCosts = MakeEffectCosts(20, 28);
        }

        public override string GroupName => TextManager.Instance.GetLocalizedText("heal");
        public override string SubGroupName => TextManager.Instance.GetLocalizedText("health");
        public override TextFile.Token[] SpellMakerDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1548);
        public override TextFile.Token[] SpellBookDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1248);

        public override void SetPotionProperties()
        {
            // Magnitude 20-20 + 0-0 per 1 levels
            EffectSettings minorMendingSettings = SetEffectMagnitude(DefaultEffectSettings(), 20, 20, 0, 0, 1);
            PotionRecipe minorMending = new PotionRecipe(
                "Minor Mending",
                14,
                0,
                20,
                minorMendingSettings,
                (int)Items.SolventIngredients.Rain_water,
                (int)Items.FruitPlantIngredients.Red_berries,
                (int)Items.FlowerPlantIngredients.Red_Flowers,
                (int)Items.FlowerPlantIngredients.Red_rose,
                (int)Items.MiscPlantIngredients.Root_tendrils);

            // Magnitude 45-45 + 0-0 per 1 levels
            EffectSettings lesserMendingSettings = SetEffectMagnitude(DefaultEffectSettings(), 45, 45, 0, 0, 1);
            PotionRecipe lesserMending = new PotionRecipe(
                "Lesser Mending",
                40,
                0,
                45,
                lesserMendingSettings,
                (int)Items.SolventIngredients.Pure_water,
                (int)Items.FruitPlantIngredients.Red_berries,
                (int)Items.MiscPlantIngredients.Root_bulb,
                (int)Items.MiscPlantIngredients.Palm,
                (int)Items.MiscPlantIngredients.Pine_branch,
                (int)Items.MetalIngredients.Mercury);

            // Magnitude 100-100 + 0-0 per 1 levels
            EffectSettings mendingSettings = SetEffectMagnitude(DefaultEffectSettings(), 100, 100, 0, 0, 1);
            PotionRecipe mending = new PotionRecipe(
                "Mending",
                160,
                0,
                60,
                lesserMendingSettings,
                (int)Items.SolventIngredients.Pure_water,
                (int)Items.FruitPlantIngredients.Red_berries,
                (int)Items.FlowerPlantIngredients.White_rose,
                (int)Items.AnimalPartIngredients.Ivory,
                (int)Items.CreatureIngredients.Troll_blood,
                (int)Items.CreatureIngredients.Saints_hair);

            // Magnitude 230-230 + 0-0 per 1 levels
            EffectSettings trueMendingSettings = SetEffectMagnitude(DefaultEffectSettings(), 230, 230, 0, 0, 1);
            PotionRecipe trueMending = new PotionRecipe(
                "True Mending",
                440,
                0,
                90,
                lesserMendingSettings,
                (int)Items.SolventIngredients.Ichor,
                (int)Items.CreatureIngredients.Daedra_heart,
                (int)Items.CreatureIngredients.Mummy_wrappings,
                (int)Items.CreatureIngredients.Troll_blood,
                (int)Items.MetalIngredients.Mercury,
                (int)Items.Gems.Amber);

            // Assign recipes
            minorMending.TextureRecord = 15;
            lesserMending.TextureRecord = 16;
            mending.TextureRecord = 6;
            trueMending.TextureRecord = 7;
            AssignPotionRecipes(minorMending, lesserMending, mending, trueMending);
        }

        public override void MagicRound()
        {
            base.MagicRound();

            // Get peered entity gameobject
            DaggerfallEntityBehaviour entityBehaviour = GetPeeredEntityBehaviour(manager);
            if (!entityBehaviour)
                return;

            // Implement effect
            int magnitude = GetMagnitude(caster);
            entityBehaviour.Entity.IncreaseHealth(magnitude);

            UnityEngine.Debug.LogFormat("{0} incremented {1}'s health by {2} points", Key, entityBehaviour.EntityType.ToString(), magnitude);
        }
    }
}
