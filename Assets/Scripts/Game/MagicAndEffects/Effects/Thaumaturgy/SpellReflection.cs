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
    /// Spell Reflection
    /// </summary>
    public class SpellReflection : IncumbentEffect
    {
        public static readonly string EffectKey = "SpellReflection";

        public override void SetProperties()
        {
            properties.Key = EffectKey;
            properties.ClassicKey = MakeClassicKey(21, 255);
            properties.SupportDuration = true;
            properties.SupportChance = true;
            properties.ChanceFunction = ChanceFunction.Custom;
            properties.AllowedTargets = EntityEffectBroker.TargetFlags_All;
            properties.AllowedElements = EntityEffectBroker.ElementFlags_MagicOnly;
            properties.AllowedCraftingStations = MagicCraftingStations.SpellMaker | MagicCraftingStations.PotionMaker;
            properties.MagicSkill = DFCareer.MagicSkills.Thaumaturgy;
            properties.DurationCosts = MakeEffectCosts(28, 140);
            properties.ChanceCosts = MakeEffectCosts(28, 140);
        }

        public override string GroupName => TextManager.Instance.GetLocalizedText("spellReflection");
        public override TextFile.Token[] SpellMakerDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1569);
        public override TextFile.Token[] SpellBookDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1269);

        public override void SetPotionProperties()
        {
            // Duration 15 + 0 per 1 levels, Chance 50-50 + 0-0 per 1 levels
            EffectSettings imperfectMirrorSettings = SetEffectDuration(DefaultEffectSettings(), 15, 0, 1);
            imperfectMirrorSettings = SetEffectChance(imperfectMirrorSettings, 50, 0, 1);
            PotionRecipe imperfectMirror = new PotionRecipe(
                "Imperfect Mirror",
                140,
                0,
                65,
                imperfectMirrorSettings,
                (int)Items.SolventIngredients.Nectar,
                (int)Items.FruitPlantIngredients.Cactus,
                (int)Items.FlowerPlantIngredients.White_rose,
                (int)Items.FlowerPlantIngredients.Black_rose,
                (int)Items.CreatureIngredients.Wereboar_tusk,
                (int)Items.MetalIngredients.Tin,
                (int)Items.MetalIngredients.Mercury,
                (int)Items.Gems.Turquoise);

            // Duration 10 + 0 per 1 levels, Chance 100-100 + 0-0 per 1 levels
            EffectSettings perfectMirrorSettings = SetEffectDuration(DefaultEffectSettings(), 10, 0, 1);
            perfectMirrorSettings = SetEffectChance(perfectMirrorSettings, 100, 0, 1);
            PotionRecipe perfectMirror = new PotionRecipe(
                "Perfect Mirror",
                890,
                0,
                100,
                perfectMirrorSettings,
                (int)Items.SolventIngredients.Elixir_vitae,
                (int)Items.FruitPlantIngredients.Cactus,
                (int)Items.CreatureIngredients.Unicorn_horn,
                (int)Items.CreatureIngredients.Basilisk_eye,
                (int)Items.MetalIngredients.Platinum,
                (int)Items.MetalIngredients.Mercury,
                (int)Items.Gems.Diamond);

            // Assign recipe
            imperfectMirror.TextureRecord = 32;
            perfectMirror.TextureRecord = 3;
            AssignPotionRecipes(imperfectMirror, perfectMirror);
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