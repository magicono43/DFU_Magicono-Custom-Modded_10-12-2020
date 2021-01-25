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
    /// Spell Resistance
    /// </summary>
    public class SpellResistance : IncumbentEffect
    {
        public static readonly string EffectKey = "SpellResistance";

        public override void SetProperties()
        {
            properties.Key = EffectKey;
            properties.ClassicKey = MakeClassicKey(22, 255);
            properties.SupportDuration = true;
            properties.SupportChance = true;
            properties.ChanceFunction = ChanceFunction.Custom;
            properties.AllowedTargets = EntityEffectBroker.TargetFlags_All;
            properties.AllowedElements = EntityEffectBroker.ElementFlags_MagicOnly;
            properties.AllowedCraftingStations = MagicCraftingStations.SpellMaker | MagicCraftingStations.PotionMaker;
            properties.MagicSkill = DFCareer.MagicSkills.Thaumaturgy;
            properties.DurationCosts = MakeEffectCosts(20, 100);
            properties.ChanceCosts = MakeEffectCosts(20, 100);
        }

        public override string GroupName => TextManager.Instance.GetLocalizedText("spellResistance");
        public override TextFile.Token[] SpellMakerDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1570);
        public override TextFile.Token[] SpellBookDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1270);

        public override void SetPotionProperties()
        {
            // Duration 20 + 0 per 1 levels, Chance 25-25 + 0-0 per 1 levels
            EffectSettings lesserSpellResistanceSettings = SetEffectDuration(DefaultEffectSettings(), 30, 0, 1);
            lesserSpellResistanceSettings = SetEffectChance(lesserSpellResistanceSettings, 25, 0, 1);
            PotionRecipe lesserSpellResistance = new PotionRecipe(
                "Lesser Spell Resistance",
                105,
                0,
                25,
                lesserSpellResistanceSettings,
                (int)Items.SolventIngredients.Pure_water,
                (int)Items.FruitPlantIngredients.Fig,
                (int)Items.FlowerPlantIngredients.Golden_poppy,
                (int)Items.CreatureIngredients.Saints_hair,
                (int)Items.CreatureIngredients.Harpy_Feather,
                (int)Items.MetalIngredients.Silver);

            // Duration 20 + 0 per 1 levels, Chance 55-55 + 0-0 per 1 levels
            EffectSettings spellResistanceSettings = SetEffectDuration(DefaultEffectSettings(), 25, 0, 1);
            spellResistanceSettings = SetEffectChance(spellResistanceSettings, 55, 0, 1);
            PotionRecipe spellResistance = new PotionRecipe(
                "Spell Resistance",
                310,
                0,
                15,
                spellResistanceSettings,
                (int)Items.SolventIngredients.Ichor,
                (int)Items.FruitPlantIngredients.Yellow_berries,
                (int)Items.FlowerPlantIngredients.Black_poppy,
                (int)Items.CreatureIngredients.Fairy_dragon_scales,
                (int)Items.CreatureIngredients.Lich_dust,
                (int)Items.MetalIngredients.Gold,
                (int)Items.Gems.Jade);

            // Duration 20 + 0 per 1 levels, Chance 100-100 + 0-0 per 1 levels
            EffectSettings peryiteSpellImmunitySettings = SetEffectDuration(DefaultEffectSettings(), 20, 0, 1);
            peryiteSpellImmunitySettings = SetEffectChance(peryiteSpellImmunitySettings, 100, 0, 1);
            PotionRecipe peryiteSpellImmunity = new PotionRecipe(
                "Peryite's Spell Immunity",
                1150,
                0,
                1,
                peryiteSpellImmunitySettings,
                (int)Items.SolventIngredients.Ichor,
                (int)Items.CreatureIngredients.Werewolfs_blood,
                (int)Items.CreatureIngredients.Dragons_scales,
                (int)Items.CreatureIngredients.Lich_dust,
                (int)Items.Gems.Ruby,
                (int)Items.Gems.Sapphire,
                (int)Items.Gems.Emerald,
                (int)Items.Gems.Diamond);

            // Assign recipe
            lesserSpellResistance.TextureRecord = 11;
            spellResistance.TextureRecord = 14;
            peryiteSpellImmunity.TextureRecord = 2;
            AssignPotionRecipes(lesserSpellResistance, spellResistance, peryiteSpellImmunity);
        }

        protected override bool IsLikeKind(IncumbentEffect other)
        {
            return (other.Key == Key) ? true : false;
        }

        protected override void AddState(IncumbentEffect incumbent)
        {
            incumbent.RoundsRemaining += RoundsRemaining;
        }
    }
}