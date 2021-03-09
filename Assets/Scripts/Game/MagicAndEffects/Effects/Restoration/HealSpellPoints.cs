// Project:         Daggerfall Tools For Unity
// Copyright:       Copyright (C) 2009-2020 Daggerfall Workshop
// Web Site:        http://www.dfworkshop.net
// License:         MIT License (http://www.opensource.org/licenses/mit-license.php)
// Source Code:     https://github.com/Interkarma/daggerfall-unity
// Original Author: Hazelnut
// Contributors:    
// 
// Notes:
//

using DaggerfallWorkshop.Game.Entity;

namespace DaggerfallWorkshop.Game.MagicAndEffects.MagicEffects
{
    /// <summary>
    /// Heal - Magicka
    /// </summary>
    public class HealSpellPoints : BaseEntityEffect
    {
        public static readonly string EffectKey = "Heal-SpellPoints";

        public override void SetProperties()
        {
            properties.Key = EffectKey;
            properties.SupportMagnitude = true;
            properties.AllowedTargets = EntityEffectBroker.TargetFlags_Self;
            properties.AllowedElements = EntityEffectBroker.ElementFlags_MagicOnly;
            properties.AllowedCraftingStations = MagicCraftingStations.PotionMaker;
        }

        public override string GroupName => TextManager.Instance.GetLocalizedText("heal");
        public override string SubGroupName => TextManager.Instance.GetLocalizedText("spellPoints");

        public override void SetPotionProperties()
        {
            // Magnitude 30-30 + 0-0 per 1 levels
            EffectSettings minorRestorePowerSettings = SetEffectMagnitude(DefaultEffectSettings(), 30, 30, 0, 0, 1);
            PotionRecipe minorRestorePower = new PotionRecipe(
                "Minor Restore Power",
                20,
                0,
                30,
                minorRestorePowerSettings,
                (int)Items.SolventIngredients.Rain_water,
                (int)Items.FruitPlantIngredients.Green_berries,
                (int)Items.FlowerPlantIngredients.White_poppy,
                (int)Items.MiscPlantIngredients.Twigs,
                (int)Items.MiscPlantIngredients.Bamboo,
                (int)Items.MetalIngredients.Tin);

            // Magnitude 75-75 + 0-0 per 1 levels
            EffectSettings lesserRestorePowerSettings = SetEffectMagnitude(DefaultEffectSettings(), 75, 75, 0, 0, 1);
            PotionRecipe lesserRestorePower = new PotionRecipe(
                "Lesser Restore Power",
                70,
                0,
                65,
                lesserRestorePowerSettings,
                (int)Items.SolventIngredients.Nectar,
                (int)Items.FruitPlantIngredients.Green_berries,
                (int)Items.MiscPlantIngredients.Root_bulb,
                (int)Items.CreatureIngredients.Werewolfs_blood,
                (int)Items.CreatureIngredients.Harpy_Feather,
                (int)Items.MetalIngredients.Silver);

            // Magnitude 170-170 + 0-0 per 1 levels
            EffectSettings restorePowerSettings = SetEffectMagnitude(DefaultEffectSettings(), 170, 170, 0, 0, 1);
            PotionRecipe restorePower = new PotionRecipe(
                "Restore Power",
                280,
                0,
                85,
                restorePowerSettings,
                (int)Items.SolventIngredients.Nectar,
                (int)Items.FruitPlantIngredients.Green_berries,
                (int)Items.CreatureIngredients.Wereboar_tusk,
                (int)Items.CreatureIngredients.Fairy_dragon_scales,
                (int)Items.MetalIngredients.Gold,
                (int)Items.Gems.Turquoise);

            // Magnitude 400-400 + 0-0 per 1 levels
            EffectSettings greaterRestorePowerSettings = SetEffectMagnitude(DefaultEffectSettings(), 400, 400, 0, 0, 1);
            PotionRecipe greaterRestorePower = new PotionRecipe(
                "Greater Restore Power",
                775,
                0,
                95,
                greaterRestorePowerSettings,
                (int)Items.SolventIngredients.Elixir_vitae,
                (int)Items.CreatureIngredients.Wraith_essence,
                (int)Items.CreatureIngredients.Lich_dust,
                (int)Items.CreatureIngredients.Dragons_scales,
                (int)Items.MetalIngredients.Platinum,
                (int)Items.Gems.Sapphire);

            // Assign recipe
            minorRestorePower.TextureRecord = 12;
            lesserRestorePower.TextureRecord = 13;
            restorePower.TextureRecord = 16;
            greaterRestorePower.TextureRecord = 7;
            AssignPotionRecipes(minorRestorePower, lesserRestorePower, restorePower, greaterRestorePower);
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
            entityBehaviour.Entity.IncreaseMagicka(magnitude);

            UnityEngine.Debug.LogFormat("{0} incremented {1}'s magicka by {2} points", Key, entityBehaviour.EntityType.ToString(), magnitude);
        }
    }
}
