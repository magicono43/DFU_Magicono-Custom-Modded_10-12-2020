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

using System;
using DaggerfallConnect;
using DaggerfallConnect.Arena2;
using DaggerfallWorkshop.Game.Entity;
using FullSerializer;

namespace DaggerfallWorkshop.Game.MagicAndEffects.MagicEffects
{
    /// <summary>
    /// Shield
    /// </summary>
    public class Shield : IncumbentEffect
    {
        public static readonly string EffectKey = "Shield";

        int startingShield;
        int shieldRemaining;

        public override void SetProperties()
        {
            properties.Key = EffectKey;
            properties.ClassicKey = MakeClassicKey(35, 255);
            properties.SupportDuration = true;
            properties.SupportMagnitude = true;
            properties.AllowedTargets = EntityEffectBroker.TargetFlags_All;
            properties.AllowedElements = EntityEffectBroker.ElementFlags_MagicOnly;
            properties.AllowedCraftingStations = MagicCraftingStations.SpellMaker | MagicCraftingStations.PotionMaker;
            properties.MagicSkill = DFCareer.MagicSkills.Alteration;
            properties.DurationCosts = MakeEffectCosts(28, 8);
            properties.MagnitudeCosts = MakeEffectCosts(80, 60);
        }

        public override string GroupName => TextManager.Instance.GetLocalizedText("shield");
        public override TextFile.Token[] SpellMakerDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1590);
        public override TextFile.Token[] SpellBookDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1290);

        public override void SetPotionProperties()
        {
            // Duration 240 + 0 per 1 levels, Magnitude 45-45 + 0-0 per 1 levels
            EffectSettings minorShieldingSettings = SetEffectDuration(DefaultEffectSettings(), 240, 0, 1);
            minorShieldingSettings = SetEffectMagnitude(minorShieldingSettings, 45, 45, 0, 0, 1);
            PotionRecipe minorShielding = new PotionRecipe(
                "Minor Shielding",
                35,
                0,
                minorShieldingSettings,
                (int)Items.SolventIngredients.Rain_water,
                (int)Items.FruitPlantIngredients.Red_berries,
                (int)Items.FruitPlantIngredients.Green_berries,
                (int)Items.MiscPlantIngredients.Ginkgo_leaves,
                (int)Items.MetalIngredients.Iron,
                (int)Items.Gems.Amber);

            // Duration 240 + 0 per 1 levels, Magnitude 110-110 + 0-0 per 1 levels
            EffectSettings lesserShieldingSettings = SetEffectDuration(DefaultEffectSettings(), 240, 0, 1);
            lesserShieldingSettings = SetEffectMagnitude(lesserShieldingSettings, 110, 110, 0, 0, 1);
            PotionRecipe lesserShielding = new PotionRecipe(
                "Lesser Shielding",
                105,
                0,
                lesserShieldingSettings,
                (int)Items.SolventIngredients.Pure_water,
                (int)Items.MiscPlantIngredients.Root_tendrils,
                (int)Items.AnimalPartIngredients.Ivory,
                (int)Items.CreatureIngredients.Wereboar_tusk,
                (int)Items.MetalIngredients.Iron,
                (int)Items.Gems.Malachite);

            // Duration 240 + 0 per 1 levels, Magnitude 265-265 + 0-0 per 1 levels
            EffectSettings shieldingSettings = SetEffectDuration(DefaultEffectSettings(), 240, 0, 1);
            shieldingSettings = SetEffectMagnitude(shieldingSettings, 265, 265, 0, 0, 1);
            PotionRecipe shielding = new PotionRecipe(
                "Shielding",
                340,
                0,
                shieldingSettings,
                (int)Items.SolventIngredients.Ichor,
                (int)Items.FlowerPlantIngredients.Clover,
                (int)Items.CreatureIngredients.Basilisk_eye,
                (int)Items.CreatureIngredients.Fairy_dragon_scales,
                (int)Items.MetalIngredients.Iron,
                (int)Items.Gems.Jade);

            // Duration 240 + 0 per 1 levels, Magnitude 590-590 + 0-0 per 1 levels
            EffectSettings stendarrShieldSettings = SetEffectDuration(DefaultEffectSettings(), 240, 0, 1);
            stendarrShieldSettings = SetEffectMagnitude(stendarrShieldSettings, 590, 590, 0, 0, 1);
            PotionRecipe stendarrShield = new PotionRecipe(
                "Stendarr's Shield",
                890,
                0,
                stendarrShieldSettings,
                (int)Items.SolventIngredients.Ichor,
                (int)Items.FlowerPlantIngredients.White_rose,
                (int)Items.CreatureIngredients.Basilisk_eye,
                (int)Items.CreatureIngredients.Saints_hair,
                (int)Items.CreatureIngredients.Dragons_scales,
                (int)Items.MetalIngredients.Platinum,
                (int)Items.Gems.Emerald);

            // Assign recipe
            lesserShielding.TextureRecord = 12;
            shielding.TextureRecord = 13;
            stendarrShield.TextureRecord = 1;
            AssignPotionRecipes(minorShielding, lesserShielding, shielding, stendarrShield);
        }

        protected override bool IsLikeKind(IncumbentEffect other)
        {
            return other is Shield;
        }

        protected override void AddState(IncumbentEffect incumbent)
        {
            // Stack rounds onto incumbent
            incumbent.RoundsRemaining += RoundsRemaining;

            // Top up shield amount no more than starting value
            Shield incumbentShield = incumbent as Shield;
            incumbentShield.shieldRemaining += GetMagnitude(GetPeeredEntityBehaviour(manager));
            if (incumbentShield.shieldRemaining > incumbentShield.startingShield)
                incumbentShield.shieldRemaining = incumbentShield.startingShield;
        }

        public override void Start(EntityEffectManager manager, DaggerfallEntityBehaviour caster = null)
        {
            base.Start(manager, caster);

            // Set initial shield amount
            startingShield = shieldRemaining = GetMagnitude(GetPeeredEntityBehaviour(manager));
        }

        /// <summary>
        /// Apply damage to shield.
        /// </summary>
        /// <param name="amount">Amount of damage to apply.</param>
        /// <returns>Damaged passed through after removing shield amount. Will be 0 if damage amount less than remaining shield amount.</returns>
        public int DamageShield(int amount)
        {
            if (shieldRemaining > 0)
            {
                shieldRemaining -= amount;
                if (shieldRemaining <= 0)
                {
                    // Shield busted - immediately end effect and return shield overflow amount
                    ResignAsIncumbent();
                    RoundsRemaining = 0;
                    manager.UpdateHUDSpellIcons();
                    return Math.Abs(shieldRemaining);
                }

                return 0;
            }
            else
            {
                return amount;
            }
        }

        #region Serialization

        [fsObject("v1")]
        public struct SaveData_v1
        {
            public int startingShield;
            public int shieldRemaining;
        }

        public override object GetSaveData()
        {
            SaveData_v1 data = new SaveData_v1();
            data.startingShield = startingShield;
            data.shieldRemaining = shieldRemaining;

            return data;
        }

        public override void RestoreSaveData(object dataIn)
        {
            if (dataIn == null)
                return;

            SaveData_v1 data = (SaveData_v1)dataIn;
            startingShield = data.startingShield;
            shieldRemaining = data.shieldRemaining;
        }

        #endregion
    }
}
