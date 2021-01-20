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
    /// Cure - Poison
    /// </summary>
    public class CurePoison : BaseEntityEffect
    {
        public static readonly string EffectKey = "Cure-Poison";

        public override void SetProperties()
        {
            properties.Key = EffectKey;
            properties.ClassicKey = MakeClassicKey(3, 1);
            properties.SupportChance = true;
            properties.AllowedTargets = EntityEffectBroker.TargetFlags_All;
            properties.AllowedElements = EntityEffectBroker.ElementFlags_MagicOnly;
            properties.AllowedCraftingStations = MagicCraftingStations.SpellMaker | MagicCraftingStations.PotionMaker;
            properties.MagicSkill = DFCareer.MagicSkills.Restoration;
            properties.ChanceCosts = MakeEffectCosts(8, 100);
        }

        public override string GroupName => TextManager.Instance.GetLocalizedText("cure");
        public override string SubGroupName => TextManager.Instance.GetLocalizedText("poison");
        public override TextFile.Token[] SpellMakerDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1510);
        public override TextFile.Token[] SpellBookDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1210);

        public override void SetPotionProperties()
        {
            // Chance 100-100 + 0-0 per 1 levels
            EffectSettings curePoisonSettings = SetEffectChance(DefaultEffectSettings(), 100, 0, 1);
            PotionRecipe curePoison = new PotionRecipe(
                "Cure Poison",
                34,
                0,
                curePoisonSettings,
                (int)Items.SolventIngredients.Pure_water,
                (int)Items.FruitPlantIngredients.Green_berries,
                (int)Items.AnimalPartIngredients.Snake_venom,
                (int)Items.AnimalPartIngredients.Spider_venom,
                (int)Items.AnimalPartIngredients.Small_scorpion_stinger,
                (int)Items.AnimalPartIngredients.Small_tooth);

            curePoison.TextureRecord = 14;
            AssignPotionRecipes(curePoison);
        }

        public override void MagicRound()
        {
            base.MagicRound();

            // Get peered entity gameobject
            DaggerfallEntityBehaviour entityBehaviour = GetPeeredEntityBehaviour(manager);
            if (!entityBehaviour)
                return;

            // Implement effect
            manager.CureAllPoisons();

            //Debug.LogFormat("Cured entity of all poisons");
        }
    }
}
