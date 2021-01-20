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
    /// Regenerate
    /// </summary>
    public class Regenerate : IncumbentEffect
    {
        public static readonly string EffectKey = "Regenerate";

        public override void SetProperties()
        {
            properties.Key = EffectKey;
            properties.ClassicKey = MakeClassicKey(18, 255);
            properties.SupportDuration = true;
            properties.SupportMagnitude = true;
            properties.AllowedTargets = EntityEffectBroker.TargetFlags_All;
            properties.AllowedElements = EntityEffectBroker.ElementFlags_MagicOnly;
            properties.AllowedCraftingStations = MagicCraftingStations.SpellMaker | MagicCraftingStations.PotionMaker;
            properties.MagicSkill = DFCareer.MagicSkills.Restoration;
            properties.DurationCosts = MakeEffectCosts(100, 20);
            properties.MagnitudeCosts = MakeEffectCosts(8, 8);
        }

        public override string GroupName => TextManager.Instance.GetLocalizedText("regenerate");
        public override TextFile.Token[] SpellMakerDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1566);
        public override TextFile.Token[] SpellBookDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1266);

        public override void SetPotionProperties()
        {
            // Duration 70 + 0 per 1 levels, Magnitude 1-1 + 0-0 per 1 levels
            EffectSettings minorTrollBloodSettings = SetEffectDuration(DefaultEffectSettings(), 70, 0, 1);
            minorTrollBloodSettings = SetEffectMagnitude(minorTrollBloodSettings, 1, 1, 0, 0, 1);
            PotionRecipe minorTrollBlood = new PotionRecipe(
                "Minor Troll's Blood",
                39,
                0,
                minorTrollBloodSettings,
                (int)Items.SolventIngredients.Rain_water,
                (int)Items.FlowerPlantIngredients.Red_Flowers,
                (int)Items.FlowerPlantIngredients.Red_poppy,
                (int)Items.MiscPlantIngredients.Aloe,
                (int)Items.CreatureIngredients.Orcs_blood,
                (int)Items.Gems.Amber);

            // Duration 80 + 0 per 1 levels, Magnitude 2-2 + 0-0 per 1 levels
            EffectSettings lesserTrollBloodSettings = SetEffectDuration(DefaultEffectSettings(), 80, 0, 1);
            lesserTrollBloodSettings = SetEffectMagnitude(lesserTrollBloodSettings, 2, 2, 0, 0, 1);
            PotionRecipe lesserTrollBlood = new PotionRecipe(
                "Lesser Troll's Blood",
                116,
                0,
                lesserTrollBloodSettings,
                (int)Items.SolventIngredients.Nectar,
                (int)Items.FlowerPlantIngredients.Red_poppy,
                (int)Items.CreatureIngredients.Orcs_blood,
                (int)Items.CreatureIngredients.Giant_blood,
                (int)Items.CreatureIngredients.Wereboar_tusk,
                (int)Items.MetalIngredients.Lead,
                (int)Items.Gems.Amber);

            // Duration 90 + 0 per 1 levels, Magnitude 4-4 + 0-0 per 1 levels
            EffectSettings TrollBloodSettings = SetEffectDuration(DefaultEffectSettings(), 90, 0, 1);
            TrollBloodSettings = SetEffectMagnitude(TrollBloodSettings, 4, 4, 0, 0, 1);
            PotionRecipe TrollBlood = new PotionRecipe(
                "Troll's Blood",
                535,
                0,
                TrollBloodSettings,
                (int)Items.SolventIngredients.Ichor,
                (int)Items.MiscPlantIngredients.Root_tendrils,
                (int)Items.CreatureIngredients.Troll_blood,
                (int)Items.CreatureIngredients.Daedra_heart,
                (int)Items.CreatureIngredients.Mummy_wrappings,
                (int)Items.MetalIngredients.Mercury,
                (int)Items.Gems.Ruby);

            // Duration 110 + 0 per 1 levels, Magnitude 7-7 + 0-0 per 1 levels
            EffectSettings miraculousClosingWoundsSettings = SetEffectDuration(DefaultEffectSettings(), 110, 0, 1);
            miraculousClosingWoundsSettings = SetEffectMagnitude(miraculousClosingWoundsSettings, 7, 7, 0, 0, 1);
            PotionRecipe miraculousClosingWounds = new PotionRecipe(
                "Miraculous Closing Wounds",
                1470,
                0,
                miraculousClosingWoundsSettings,
                (int)Items.SolventIngredients.Elixir_vitae,
                (int)Items.CreatureIngredients.Troll_blood,
                (int)Items.CreatureIngredients.Unicorn_horn,
                (int)Items.CreatureIngredients.Daedra_heart,
                (int)Items.CreatureIngredients.Saints_hair,
                (int)Items.CreatureIngredients.Dragons_scales,
                (int)Items.MetalIngredients.Platinum,
                (int)Items.Gems.Emerald);

            // Assign recipe
            minorTrollBlood.TextureRecord = 15;
            lesserTrollBlood.TextureRecord = 16;
            TrollBlood.TextureRecord = 6;
            miraculousClosingWounds.TextureRecord = 5;
            AssignPotionRecipes(minorTrollBlood, lesserTrollBlood, TrollBlood, miraculousClosingWounds);
        }


        public override void Start(EntityEffectManager manager, DaggerfallEntityBehaviour caster = null)
        {
            base.Start(manager, caster);

            // Output "You are regenerating." if the host manager is player
            if (manager.EntityBehaviour == GameManager.Instance.PlayerEntityBehaviour)
            {
                DaggerfallUI.AddHUDText(TextManager.Instance.GetLocalizedText("youAreRegenerating"), 1.5f);
            }
        }

        protected override bool IsLikeKind(IncumbentEffect other)
        {
            return (other is Regenerate);
        }

        protected override void AddState(IncumbentEffect incumbent)
        {
            // Stack my rounds onto incumbent
            incumbent.RoundsRemaining += RoundsRemaining;
        }

        public override void MagicRound()
        {
            base.MagicRound();

            // Get peered entity gameobject
            DaggerfallEntityBehaviour entityBehaviour = GetPeeredEntityBehaviour(manager);
            if (!entityBehaviour)
                return;

            // Increase target health
            entityBehaviour.Entity.IncreaseHealth(GetMagnitude(caster));
        }
    }
}
