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
    /// Heal - Fatigue
    /// </summary>
    public class HealFatigue : BaseEntityEffect
    {
        public static readonly string EffectKey = "Heal-Fatigue";

        public override void SetProperties()
        {
            properties.Key = EffectKey;
            properties.ClassicKey = MakeClassicKey(10, 9);
            properties.SupportMagnitude = true;
            properties.AllowedTargets = EntityEffectBroker.TargetFlags_All;
            properties.AllowedElements = EntityEffectBroker.ElementFlags_MagicOnly;
            properties.AllowedCraftingStations = MagicCraftingStations.SpellMaker | MagicCraftingStations.PotionMaker;
            properties.MagicSkill = DFCareer.MagicSkills.Restoration;
            properties.MagnitudeCosts = MakeEffectCosts(8, 28);
        }

        public override string GroupName => TextManager.Instance.GetLocalizedText("heal");
        public override string SubGroupName => TextManager.Instance.GetLocalizedText("fatigue");
        public override TextFile.Token[] SpellMakerDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1549);
        public override TextFile.Token[] SpellBookDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1249);

        public override void SetPotionProperties()
        {
            // Magnitude 15-15 + 0-0 per 1 levels
            EffectSettings minorEnergySettings = SetEffectMagnitude(DefaultEffectSettings(), 15, 15, 0, 0, 1);
            PotionRecipe minorEnergy = new PotionRecipe(
                "Minor Energy",
                7,
                0,
                10,
                minorEnergySettings,
                (int)Items.SolventIngredients.Rain_water,
                (int)Items.FruitPlantIngredients.Yellow_berries,
                (int)Items.MiscPlantIngredients.Green_leaves);

            // Magnitude 40-40 + 0-0 per 1 levels
            EffectSettings lesserEnergySettings = SetEffectMagnitude(DefaultEffectSettings(), 40, 40, 0, 0, 1);
            PotionRecipe lesserEnergy = new PotionRecipe(
                "Lesser Energy",
                20,
                0,
                30,
                lesserEnergySettings,
                (int)Items.SolventIngredients.Pure_water,
                (int)Items.FruitPlantIngredients.Fig,
                (int)Items.MiscPlantIngredients.Aloe,
                (int)Items.MiscPlantIngredients.Ginkgo_leaves);

            // Magnitude 90-90 + 0-0 per 1 levels
            EffectSettings energySettings = SetEffectMagnitude(DefaultEffectSettings(), 90, 90, 0, 0, 1);
            PotionRecipe energy = new PotionRecipe(
                "Energy",
                80,
                0,
                55,
                energySettings,
                (int)Items.SolventIngredients.Nectar,
                (int)Items.FruitPlantIngredients.Cactus,
                (int)Items.MiscPlantIngredients.Pine_branch,
                (int)Items.CreatureIngredients.Nymph_hair,
                (int)Items.CreatureIngredients.Werewolfs_blood);

            // Magnitude 200-200 + 0-0 per 1 levels
            EffectSettings pureEnergySettings = SetEffectMagnitude(DefaultEffectSettings(), 200, 200, 0, 0, 1);
            PotionRecipe pureEnergy = new PotionRecipe(
                "Pure Energy",
                220,
                0,
                75,
                energySettings,
                (int)Items.SolventIngredients.Elixir_vitae,
                (int)Items.FruitPlantIngredients.Yellow_berries,
                (int)Items.AnimalPartIngredients.Small_scorpion_stinger,
                (int)Items.CreatureIngredients.Nymph_hair,
                (int)Items.CreatureIngredients.Ghouls_tongue,
                (int)Items.MetalIngredients.Copper,
                (int)Items.Gems.Malachite);

            // Assign recipe
            lesserEnergy.TextureRecord = 1;
            energy.TextureRecord = 6;
            pureEnergy.TextureRecord = 5;
            AssignPotionRecipes(minorEnergy, lesserEnergy, energy, pureEnergy);
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
            entityBehaviour.Entity.IncreaseFatigue(magnitude, true);

            //Debug.LogFormat("{0} incremented {1}'s fatigue by {2} points", Key, entityBehaviour.EntityType.ToString(), magnitude);
        }
    }
}
