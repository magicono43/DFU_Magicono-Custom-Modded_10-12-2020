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
    /// Invisibility - True
    /// </summary>
    public class InvisibilityTrue : ConcealmentEffect
    {
        public static readonly string EffectKey = "Invisibility-True";

        public override void SetProperties()
        {
            properties.Key = EffectKey;
            properties.ClassicKey = MakeClassicKey(13, 1);
            properties.SupportDuration = true;
            properties.AllowedTargets = EntityEffectBroker.TargetFlags_All;
            properties.AllowedElements = EntityEffectBroker.ElementFlags_MagicOnly;
            properties.AllowedCraftingStations = MagicCraftingStations.SpellMaker | MagicCraftingStations.PotionMaker;
            properties.MagicSkill = DFCareer.MagicSkills.Illusion;
            properties.DurationCosts = MakeEffectCosts(60, 140);
            concealmentFlag = MagicalConcealmentFlags.InvisibleTrue;
            startConcealmentMessageKey = "youAreInvisible";
        }

        public override string GroupName => TextManager.Instance.GetLocalizedText("invisibility");
        public override string SubGroupName => TextManager.Instance.GetLocalizedText("true");
        public override string DisplayName => string.Format("{0} ({1})", GroupName, SubGroupName);
        public override TextFile.Token[] SpellMakerDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1561);
        public override TextFile.Token[] SpellBookDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1261);

        public override void SetPotionProperties()
        {
            // Duration 15 + 0 per 1 levels
            EffectSettings trueInvisibilitySettings = SetEffectDuration(DefaultEffectSettings(), 15, 0, 1);
            PotionRecipe trueInvisibility = new PotionRecipe(
                "True Invisibility",
                425,
                0,
                100,
                trueInvisibilitySettings,
                (int)Items.SolventIngredients.Pure_water,
                (int)Items.FlowerPlantIngredients.White_rose,
                (int)Items.FlowerPlantIngredients.White_poppy,
                (int)Items.AnimalPartIngredients.Pearl,
                (int)Items.CreatureIngredients.Ectoplasm,
                (int)Items.CreatureIngredients.Saints_hair,
                (int)Items.Gems.Diamond);

            trueInvisibility.TextureRecord = 3;
            AssignPotionRecipes(trueInvisibility);
        }

        protected override bool IsLikeKind(IncumbentEffect other)
        {
            return (other is InvisibilityTrue);
        }
    }
}
