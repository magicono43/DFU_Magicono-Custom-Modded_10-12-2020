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
    /// Dispel - Magic
    /// </summary>
    public class DispelMagic : BaseEntityEffect
    {
        public static readonly string EffectKey = "Dispel-Magic";

        public override void SetProperties()
        {
            properties.Key = EffectKey;
            properties.ClassicKey = MakeClassicKey(6, 0);
            properties.SupportChance = true;
            properties.AllowedTargets = EntityEffectBroker.TargetFlags_Self;
            properties.AllowedElements = EntityEffectBroker.ElementFlags_MagicOnly;
            properties.AllowedCraftingStations = MagicCraftingStations.SpellMaker | MagicCraftingStations.PotionMaker;
            properties.MagicSkill = DFCareer.MagicSkills.Mysticism;
            properties.ChanceCosts = MakeEffectCosts(120, 180);
        }

        public override string GroupName => TextManager.Instance.GetLocalizedText("dispel");
        public override string SubGroupName => TextManager.Instance.GetLocalizedText("magic");
        public override TextFile.Token[] SpellMakerDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1516);
        public override TextFile.Token[] SpellBookDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1216);

        public override void SetPotionProperties()
        {
            // Chance 100-100 + 0-0 per 1 levels
            EffectSettings cleanseMagicSettings = SetEffectChance(DefaultEffectSettings(), 100, 0, 1);
            PotionRecipe cleanseMagic = new PotionRecipe(
                "Cleanse Magic",
                200,
                0,
                cleanseMagicSettings,
                (int)Items.SolventIngredients.Pure_water,
                (int)Items.FlowerPlantIngredients.White_poppy,
                (int)Items.FlowerPlantIngredients.Clover,
                (int)Items.MiscPlantIngredients.Aloe,
                (int)Items.AnimalPartIngredients.Pearl,
                (int)Items.AnimalPartIngredients.Ivory,
                (int)Items.CreatureIngredients.Fairy_dragon_scales,
                (int)Items.MetalIngredients.Silver);

            cleanseMagic.TextureRecord = 32;
            AssignPotionRecipes(cleanseMagic);
        }

        public override void MagicRound()
        {
            base.MagicRound();

            // Clear all spell bundles on target - including this one
            manager.ClearSpellBundles();
        }
    }
}
