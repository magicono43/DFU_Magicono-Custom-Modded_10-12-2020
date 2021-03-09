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
    /// Invisibility - Normal
    /// </summary>
    public class InvisibilityNormal : ConcealmentEffect
    {
        public static readonly string EffectKey = "Invisibility-Normal";

        public override void SetProperties()
        {
            properties.Key = EffectKey;
            properties.ClassicKey = MakeClassicKey(13, 0);
            properties.SupportDuration = true;
            properties.AllowedTargets = EntityEffectBroker.TargetFlags_All;
            properties.AllowedElements = EntityEffectBroker.ElementFlags_MagicOnly;
            properties.AllowedCraftingStations = MagicCraftingStations.SpellMaker | MagicCraftingStations.PotionMaker;
            properties.MagicSkill = DFCareer.MagicSkills.Illusion;
            properties.DurationCosts = MakeEffectCosts(40, 120);
            concealmentFlag = MagicalConcealmentFlags.InvisibleNormal;
            startConcealmentMessageKey = "youAreInvisible";
        }

        public override string GroupName => TextManager.Instance.GetLocalizedText("invisibility");
        public override string SubGroupName => TextManager.Instance.GetLocalizedText("normal");
        public override string DisplayName => string.Format("{0} ({1})", GroupName, SubGroupName);
        public override TextFile.Token[] SpellMakerDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1560);
        public override TextFile.Token[] SpellBookDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1260);

        public override void SetPotionProperties()
        {
            // Duration 15 + 0 per 1 levels
            EffectSettings lesserInvisibilitySettings = SetEffectDuration(DefaultEffectSettings(), 15, 0, 1);
            PotionRecipe lesserInvisibility = new PotionRecipe(
                "Lesser Invisibility",
                100,
                0,
                70,
                lesserInvisibilitySettings,
                (int)Items.SolventIngredients.Pure_water,
                (int)Items.FlowerPlantIngredients.White_rose,
                (int)Items.FlowerPlantIngredients.White_poppy,
                (int)Items.AnimalPartIngredients.Ivory,
                (int)Items.CreatureIngredients.Ectoplasm,
                (int)Items.MetalIngredients.Platinum);

            lesserInvisibility.TextureRecord = 32;
            AssignPotionRecipes(lesserInvisibility);
        }

        protected override bool IsLikeKind(IncumbentEffect other)
        {
            return (other is InvisibilityNormal);
        }
    }
}
