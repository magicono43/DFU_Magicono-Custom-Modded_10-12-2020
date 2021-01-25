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
    /// Water Walking.
    /// </summary>
    public class WaterWalking : IncumbentEffect
    {
        public static readonly string EffectKey = "WaterWalking";

        public override void SetProperties()
        {
            properties.Key = EffectKey;
            properties.ClassicKey = MakeClassicKey(31, 255);
            properties.SupportDuration = true;
            properties.AllowedTargets = EntityEffectBroker.TargetFlags_All;
            properties.AllowedElements = ElementTypes.Magic;
            properties.AllowedCraftingStations = MagicCraftingStations.SpellMaker | MagicCraftingStations.PotionMaker;
            properties.MagicSkill = DFCareer.MagicSkills.Thaumaturgy;
            properties.DurationCosts = MakeEffectCosts(20, 8);
        }

        public override string GroupName => TextManager.Instance.GetLocalizedText("waterWalking");
        public override TextFile.Token[] SpellMakerDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1583);
        public override TextFile.Token[] SpellBookDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1283);

        public override void SetPotionProperties()
        {
            // Duration 15 + 0 per 1 levels
            EffectSettings waterStrideSettings = SetEffectDuration(DefaultEffectSettings(), 15, 0, 1);
            PotionRecipe waterStride = new PotionRecipe(
                "Water Stride",
                40,
                0,
                25,
                waterStrideSettings,
                (int)Items.SolventIngredients.Rain_water,
                (int)Items.SolventIngredients.Pure_water,
                (int)Items.MiscPlantIngredients.Palm,
                (int)Items.AnimalPartIngredients.Pearl,
                (int)Items.MetalIngredients.Silver);

            waterStride.TextureRecord = 35;
            AssignPotionRecipes(waterStride);
        }

        public override void ConstantEffect()
        {
            base.ConstantEffect();
            StartWaterWalking();
        }

        public override void Start(EntityEffectManager manager, DaggerfallEntityBehaviour caster = null)
        {
            base.Start(manager, caster);
            StartWaterWalking();
        }

        public override void Resume(EntityEffectManager.EffectSaveData_v1 effectData, EntityEffectManager manager, DaggerfallEntityBehaviour caster = null)
        {
            base.Resume(effectData, manager, caster);
            StartWaterWalking();
        }

        public override void End()
        {
            base.End();
            StopWaterWalking();
        }

        protected override bool IsLikeKind(IncumbentEffect other)
        {
            return (other is WaterWalking);
        }

        protected override void AddState(IncumbentEffect incumbent)
        {
            // Stack my rounds onto incumbent
            incumbent.RoundsRemaining += RoundsRemaining;
        }

        void StartWaterWalking()
        {
            // Get peered entity gameobject
            DaggerfallEntityBehaviour entityBehaviour = GetPeeredEntityBehaviour(manager);
            if (!entityBehaviour)
                return;

            entityBehaviour.Entity.IsWaterWalking = true;
        }

        void StopWaterWalking()
        {
            // Get peered entity gameobject
            DaggerfallEntityBehaviour entityBehaviour = GetPeeredEntityBehaviour(manager);
            if (!entityBehaviour)
                return;

            entityBehaviour.Entity.IsWaterWalking = false;
        }
    }
}
