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
    /// Free Action.
    /// </summary>
    public class FreeAction : IncumbentEffect
    {
        public static readonly string EffectKey = "FreeAction";

        public override void SetProperties()
        {
            properties.Key = EffectKey;
            properties.ClassicKey = MakeClassicKey(26, 255);
            properties.SupportDuration = true;
            properties.AllowedTargets = EntityEffectBroker.TargetFlags_All;
            properties.AllowedElements = ElementTypes.Magic;
            properties.AllowedCraftingStations = MagicCraftingStations.SpellMaker | MagicCraftingStations.PotionMaker;
            properties.MagicSkill = DFCareer.MagicSkills.Restoration;
            properties.DurationCosts = MakeEffectCosts(20, 8);
        }

        public override string GroupName => TextManager.Instance.GetLocalizedText("freeAction");
        public override TextFile.Token[] SpellMakerDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1576);
        public override TextFile.Token[] SpellBookDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1276);

        public override void SetPotionProperties()
        {
            // Duration 20 + 0 per 1 levels
            EffectSettings freeActionSettings = SetEffectDuration(DefaultEffectSettings(), 20, 0, 1);
            PotionRecipe freeAction = new PotionRecipe(
                "Free Action",
                165,
                0,
                freeActionSettings,
                (int)Items.SolventIngredients.Ichor,
                (int)Items.MiscPlantIngredients.Bamboo,
                (int)Items.AnimalPartIngredients.Spider_venom,
                (int)Items.CreatureIngredients.Gorgon_snake,
                (int)Items.CreatureIngredients.Nymph_hair,
                (int)Items.MetalIngredients.Copper,
                (int)Items.MetalIngredients.Mercury,
                (int)Items.Gems.Malachite);

            freeAction.TextureRecord = 1;
            AssignPotionRecipes(freeAction);
        }

        public override void ConstantEffect()
        {
            base.ConstantEffect();
            StartFreeAction();
        }

        public override void Start(EntityEffectManager manager, DaggerfallEntityBehaviour caster = null)
        {
            base.Start(manager, caster);
            StartFreeAction();
        }

        public override void Resume(EntityEffectManager.EffectSaveData_v1 effectData, EntityEffectManager manager, DaggerfallEntityBehaviour caster = null)
        {
            base.Resume(effectData, manager, caster);
            StartFreeAction();
        }

        public override void End()
        {
            base.End();
            StopFreeAction();
        }

        protected override bool IsLikeKind(IncumbentEffect other)
        {
            return (other is FreeAction);
        }

        protected override void AddState(IncumbentEffect incumbent)
        {
            // Stack my rounds onto incumbent
            incumbent.RoundsRemaining += RoundsRemaining;
        }

        void StartFreeAction()
        {
            // Get peered entity gameobject
            DaggerfallEntityBehaviour entityBehaviour = GetPeeredEntityBehaviour(manager);
            if (!entityBehaviour)
                return;

            entityBehaviour.Entity.IsImmuneToParalysis = true;
        }

        void StopFreeAction()
        {
            // Get peered entity gameobject
            DaggerfallEntityBehaviour entityBehaviour = GetPeeredEntityBehaviour(manager);
            if (!entityBehaviour)
                return;

            entityBehaviour.Entity.IsImmuneToParalysis = false;
        }
    }
}
