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
    /// Cure - Disease
    /// </summary>
    public class CureDisease : BaseEntityEffect
    {
        public static readonly string EffectKey = "Cure-Disease";

        public override void SetProperties()
        {
            properties.Key = EffectKey;
            properties.ClassicKey = MakeClassicKey(3, 0);
            properties.SupportChance = true;
            properties.AllowedTargets = EntityEffectBroker.TargetFlags_All;
            properties.AllowedElements = EntityEffectBroker.ElementFlags_MagicOnly;
            properties.AllowedCraftingStations = MagicCraftingStations.SpellMaker | MagicCraftingStations.PotionMaker;
            properties.MagicSkill = DFCareer.MagicSkills.Restoration;
            properties.ChanceCosts = MakeEffectCosts(8, 100);
        }

        public override string GroupName => TextManager.Instance.GetLocalizedText("cure");
        public override string SubGroupName => TextManager.Instance.GetLocalizedText("disease");
        public override TextFile.Token[] SpellMakerDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1509);
        public override TextFile.Token[] SpellBookDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1209);

        public override void SetPotionProperties()
        {
            // Chance 100-100 + 0-0 per 1 levels
            EffectSettings cureDiseaseSettings = SetEffectChance(DefaultEffectSettings(), 100, 0, 1);
            PotionRecipe cureDisease = new PotionRecipe(
                "Cure Disease",
                32,
                0,
                25,
                cureDiseaseSettings,
                (int)Items.SolventIngredients.Nectar,
                (int)Items.FruitPlantIngredients.Red_berries,
                (int)Items.FlowerPlantIngredients.Yellow_Flowers,
                (int)Items.FlowerPlantIngredients.White_rose,
                (int)Items.AnimalPartIngredients.Big_tooth,
                (int)Items.AnimalPartIngredients.Small_tooth,
                (int)Items.MetalIngredients.Copper);

            // Chance 100-100 + 0-0 per 1 levels, Magnitude 100-100 + 0-0 per 1 levels
            EffectSettings purificationSettings = SetEffectChance(DefaultEffectSettings(), 100, 0, 1);
            purificationSettings = SetEffectMagnitude(purificationSettings, 100, 100, 0, 0, 1);
            PotionRecipe purification = new PotionRecipe(
                "Purification",
                1850,
                0,
                100,
                purificationSettings,
                (int)Items.SolventIngredients.Elixir_vitae,
                (int)Items.SolventIngredients.Ichor,
                (int)Items.AnimalPartIngredients.Giant_scorpion_stinger,
                (int)Items.CreatureIngredients.Unicorn_horn,
                (int)Items.CreatureIngredients.Saints_hair,
                (int)Items.CreatureIngredients.Troll_blood,
                (int)Items.CreatureIngredients.Mummy_wrappings,
                (int)Items.Gems.Diamond);
            purification.AddSecondaryEffect(CurePoison.EffectKey);
            purification.AddSecondaryEffect(CureParalyzation.EffectKey);
            purification.AddSecondaryEffect(DispelMagic.EffectKey);
            purification.AddSecondaryEffect(HealHealth.EffectKey);
            purification.AddSecondaryEffect(HealStrength.EffectKey);
            purification.AddSecondaryEffect(HealIntelligence.EffectKey);
            purification.AddSecondaryEffect(HealWillpower.EffectKey);
            purification.AddSecondaryEffect(HealAgility.EffectKey);
            purification.AddSecondaryEffect(HealEndurance.EffectKey);
            purification.AddSecondaryEffect(HealPersonality.EffectKey);
            purification.AddSecondaryEffect(HealSpeed.EffectKey);
            purification.AddSecondaryEffect(HealLuck.EffectKey);

            cureDisease.TextureRecord = 35;
            purification.TextureRecord = 32;
            AssignPotionRecipes(cureDisease, purification);
        }

        public override void MagicRound()
        {
            base.MagicRound();

            // Get peered entity gameobject
            DaggerfallEntityBehaviour entityBehaviour = GetPeeredEntityBehaviour(manager);
            if (!entityBehaviour)
                return;

            // Implement effect
            manager.CureAllDiseases();

            UnityEngine.Debug.LogFormat("Cured entity of all diseases");
        }
    }
}
