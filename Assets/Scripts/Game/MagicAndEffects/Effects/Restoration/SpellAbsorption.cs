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
    /// Spell Absorption
    /// </summary>
    public class SpellAbsorption : IncumbentEffect
    {
        public static readonly string EffectKey = "SpellAbsorption";

        public override void SetProperties()
        {
            properties.Key = EffectKey;
            properties.ClassicKey = MakeClassicKey(20, 255);
            properties.SupportDuration = true;
            properties.SupportChance = true;
            properties.ChanceFunction = ChanceFunction.Custom;
            properties.AllowedTargets = EntityEffectBroker.TargetFlags_All;
            properties.AllowedElements = EntityEffectBroker.ElementFlags_MagicOnly;
            properties.AllowedCraftingStations = MagicCraftingStations.SpellMaker | MagicCraftingStations.PotionMaker;
            properties.MagicSkill = DFCareer.MagicSkills.Restoration;
            properties.DurationCosts = MakeEffectCosts(28, 140);
            properties.ChanceCosts = MakeEffectCosts(28, 140);
        }

        public override string GroupName => TextManager.Instance.GetLocalizedText("spellAbsorption");
        public override TextFile.Token[] SpellMakerDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1568);
        public override TextFile.Token[] SpellBookDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1268);

        public override void SetPotionProperties()
        {
            // Duration 20 + 0 per 1 levels, Chance 50-50 + 0-0 per 1 levels
            EffectSettings partSpellAbsorptionSettings = SetEffectDuration(DefaultEffectSettings(), 20, 0, 1);
            partSpellAbsorptionSettings = SetEffectChance(partSpellAbsorptionSettings, 50, 0, 1);
            PotionRecipe partSpellAbsorption = new PotionRecipe(
                "Partial Spell Absorption",
                85,
                0,
                25,
                partSpellAbsorptionSettings,
                (int)Items.SolventIngredients.Nectar,
                (int)Items.FlowerPlantIngredients.Red_poppy,
                (int)Items.MiscPlantIngredients.Root_bulb,
                (int)Items.MiscPlantIngredients.Root_tendrils,
                (int)Items.CreatureIngredients.Ghouls_tongue,
                (int)Items.CreatureIngredients.Nymph_hair,
                (int)Items.MetalIngredients.Silver);

            // Duration 15 + 0 per 1 levels, Chance 100-100 + 0-0 per 1 levels
            EffectSettings spellSpongeSettings = SetEffectDuration(DefaultEffectSettings(), 15, 0, 1);
            spellSpongeSettings = SetEffectChance(spellSpongeSettings, 100, 0, 1);
            PotionRecipe spellSponge = new PotionRecipe(
                "Spell Sponge",
                525,
                0,
                5,
                spellSpongeSettings,
                (int)Items.SolventIngredients.Elixir_vitae,
                (int)Items.MiscPlantIngredients.Root_bulb,
                (int)Items.MiscPlantIngredients.Root_tendrils,
                (int)Items.CreatureIngredients.Ghouls_tongue,
                (int)Items.CreatureIngredients.Lich_dust,
                (int)Items.CreatureIngredients.Wraith_essence,
                (int)Items.MetalIngredients.Lodestone,
                (int)Items.Gems.Ruby);

            // Assign recipe
            partSpellAbsorption.TextureRecord = 6;
            spellSponge.TextureRecord = 7;
            AssignPotionRecipes(partSpellAbsorption, spellSponge);
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
