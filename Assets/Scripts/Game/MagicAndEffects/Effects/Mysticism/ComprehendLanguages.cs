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
    /// Comprehend Languages
    /// </summary>
    public class ComprehendLanguages : IncumbentEffect
    {
        public static readonly string EffectKey = "ComprehendLanguages";

        public override void SetProperties()
        {
            properties.Key = EffectKey;
            properties.ClassicKey = MakeClassicKey(44, 255);
            properties.SupportDuration = true;
            properties.SupportChance = true;
            properties.ChanceFunction = ChanceFunction.Custom;
            properties.AllowedTargets = EntityEffectBroker.TargetFlags_Self;
            properties.AllowedElements = EntityEffectBroker.ElementFlags_MagicOnly;
            properties.AllowedCraftingStations = MagicCraftingStations.SpellMaker | MagicCraftingStations.PotionMaker;
            properties.MagicSkill = DFCareer.MagicSkills.Mysticism;
            properties.DurationCosts = MakeEffectCosts(60, 68);
            properties.ChanceCosts = MakeEffectCosts(40, 68);
        }

        public override string GroupName => TextManager.Instance.GetLocalizedText("comprehendLanguages");
        public override TextFile.Token[] SpellMakerDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1605);
        public override TextFile.Token[] SpellBookDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1305);

        public override void SetPotionProperties()
        {
            // Duration 20 + 0 per 1 levels, Chance 25-25 + 0-0 per 1 levels
            EffectSettings silverTongueSettings = SetEffectDuration(DefaultEffectSettings(), 20, 0, 1);
            silverTongueSettings = SetEffectChance(silverTongueSettings, 25, 0, 1);
            PotionRecipe silverTongue = new PotionRecipe(
                "Silver Tongue",
                48,
                0,
                20,
                silverTongueSettings,
                (int)Items.SolventIngredients.Rain_water,
                (int)Items.FruitPlantIngredients.Cactus,
                (int)Items.FlowerPlantIngredients.Black_rose,
                (int)Items.AnimalPartIngredients.Small_tooth,
                (int)Items.CreatureIngredients.Ghouls_tongue,
                (int)Items.MetalIngredients.Silver);

            // Duration 20 + 0 per 1 levels, Chance 55-55 + 0-0 per 1 levels
            EffectSettings theDiplomatSettings = SetEffectDuration(DefaultEffectSettings(), 20, 0, 1);
            theDiplomatSettings = SetEffectChance(theDiplomatSettings, 55, 0, 1);
            PotionRecipe theDiplomat = new PotionRecipe(
                "The Diplomat",
                180,
                0,
                10,
                theDiplomatSettings,
                (int)Items.SolventIngredients.Nectar,
                (int)Items.FruitPlantIngredients.Cactus,
                (int)Items.AnimalPartIngredients.Snake_venom,
                (int)Items.CreatureIngredients.Ghouls_tongue,
                (int)Items.CreatureIngredients.Gorgon_snake,
                (int)Items.CreatureIngredients.Wraith_essence,
                (int)Items.MetalIngredients.Gold);

            // Duration 20 + 0 per 1 levels, Chance 100-100 + 0-0 per 1 levels
            EffectSettings vileDeceptionSettings = SetEffectDuration(DefaultEffectSettings(), 20, 0, 1);
            vileDeceptionSettings = SetEffectChance(vileDeceptionSettings, 100, 0, 1);
            PotionRecipe vileDeception = new PotionRecipe(
                "Clavicus Vile's Deception",
                715,
                0,
                1,
                vileDeceptionSettings,
                (int)Items.SolventIngredients.Ichor,
                (int)Items.AnimalPartIngredients.Snake_venom,
                (int)Items.CreatureIngredients.Ghouls_tongue,
                (int)Items.CreatureIngredients.Daedra_heart,
                (int)Items.CreatureIngredients.Gorgon_snake,
                (int)Items.CreatureIngredients.Werewolfs_blood,
                (int)Items.MetalIngredients.Platinum,
                (int)Items.Gems.Sapphire);

            // Assign recipe
            silverTongue.TextureRecord = 11;
            theDiplomat.TextureRecord = 12;
            vileDeception.TextureRecord = 13;
            AssignPotionRecipes(silverTongue, theDiplomat, vileDeception);
        }

        protected override bool IsLikeKind(IncumbentEffect other)
        {
            return other is ComprehendLanguages;
        }

        protected override void AddState(IncumbentEffect incumbent)
        {
            // Stack rounds onto incumbent
            incumbent.RoundsRemaining += RoundsRemaining;
        }

        public override void Start(EntityEffectManager manager, DaggerfallEntityBehaviour caster = null)
        {
            base.Start(manager, caster);
        }
    }
}
