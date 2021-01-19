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
    /// ElementalResistance Fire/Frost/Poison/Shock/Magic multi-effect
    /// </summary>
    public class ElementalResistance : IncumbentEffect
    {
        #region Fields

        const int totalVariants = 5;
        //const int savingThrowModifier = 75;
        public static readonly string[] subGroupTextKeys = { "Fire", "Frost", "Poison", "Shock", "Magicka" };
        readonly VariantProperties[] variantProperties = new VariantProperties[totalVariants];

        #endregion

        #region Structs

        struct VariantProperties
        {
            public DFCareer.Elements elementResisted;
            public EffectProperties effectProperties;
            public PotionProperties potionProperties;
        }

        #endregion

        #region Properties

        public override EffectProperties Properties
        {
            get { return variantProperties[currentVariant].effectProperties; }
        }

        public override bool ChanceSuccess
        {
            // Always allow effect to succeed startup - we want to use chance component in a custom way
            get { return true; }
        }

        public DFCareer.Elements ElementResisted
        {
            get { return variantProperties[currentVariant].elementResisted; }
        }

        public override PotionProperties PotionProperties
        {
            get { return variantProperties[currentVariant].potionProperties; }
        }

        #endregion

        #region Overrides

        public override void SetProperties()
        {
            // Set properties shared by all variants
            properties.SupportDuration = true;
            properties.SupportChance = true;
            properties.AllowedTargets = EntityEffectBroker.TargetFlags_All;
            properties.AllowedElements = EntityEffectBroker.ElementFlags_MagicOnly;
            properties.AllowedCraftingStations = MagicCraftingStations.SpellMaker | MagicCraftingStations.PotionMaker;
            properties.MagicSkill = DFCareer.MagicSkills.Alteration;
            properties.DurationCosts = MakeEffectCosts(100, 100);
            properties.ChanceCosts = MakeEffectCosts(8, 100);

            // Set unique variant properties
            variantCount = totalVariants;
            SetVariantProperties(DFCareer.Elements.Fire);
            SetVariantProperties(DFCareer.Elements.Frost);
            SetVariantProperties(DFCareer.Elements.DiseaseOrPoison);
            SetVariantProperties(DFCareer.Elements.Shock);
            SetVariantProperties(DFCareer.Elements.Magic);
        }

        public override string GroupName => TextManager.Instance.GetLocalizedText("elementalResistance");
        public override string SubGroupName => TextManager.Instance.GetLocalizedText(subGroupTextKeys[currentVariant]);
        public override TextFile.Token[] SpellMakerDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1527 + currentVariant);
        public override TextFile.Token[] SpellBookDescription => DaggerfallUnity.Instance.TextProvider.GetRSCTokens(1227 + currentVariant);

        public override void SetPotionProperties()
        {
            // Duration 48 + 0 per 1 levels, Chance 1-1 + 0-0 per 1 levels
            EffectSettings protectionSettings = SetEffectDuration(DefaultEffectSettings(), 48, 0, 1);
            protectionSettings = SetEffectChance(protectionSettings, 1, 0, 1);

            // Duration 20 + 0 per 1 levels, Chance 100-100 + 0-0 per 1 levels
            EffectSettings immunitySettings = SetEffectDuration(DefaultEffectSettings(), 20, 0, 1);
            immunitySettings = SetEffectChance(immunitySettings, 100, 0, 1);

            // Protection
            PotionRecipe fireProtection = new PotionRecipe(
                "Fire Protection",
                34,
                0,
                protectionSettings,
                (int)Items.SolventIngredients.Pure_water,
                (int)Items.FruitPlantIngredients.Red_berries,
                (int)Items.FlowerPlantIngredients.Red_Flowers,
                (int)Items.MetalIngredients.Sulphur,
                (int)Items.Gems.Amber);

            PotionRecipe frostProtection = new PotionRecipe(
                "Frost Protection",
                43,
                0,
                protectionSettings,
                (int)Items.SolventIngredients.Pure_water,
                (int)Items.FruitPlantIngredients.Fig,
                (int)Items.MiscPlantIngredients.Pine_branch,
                (int)Items.Gems.Turquoise);

            PotionRecipe shockProtection = new PotionRecipe(
                "Shock Protection",
                39,
                0,
                protectionSettings,
                (int)Items.SolventIngredients.Pure_water,
                (int)Items.FruitPlantIngredients.Yellow_berries,
                (int)Items.FlowerPlantIngredients.Yellow_Flowers,
                (int)Items.MetalIngredients.Lodestone,
                (int)Items.Gems.Malachite);

            PotionRecipe poisonProtection = new PotionRecipe(
                "Poison Protection",
                36,
                4,
                protectionSettings,
                (int)Items.SolventIngredients.Pure_water,
                (int)Items.FruitPlantIngredients.Green_berries,
                (int)Items.AnimalPartIngredients.Snake_venom,
                (int)Items.AnimalPartIngredients.Spider_venom,
                (int)Items.AnimalPartIngredients.Small_scorpion_stinger);

            PotionRecipe magickaProtection = new PotionRecipe(
                "Magicka Protection",
                40,
                0,
                protectionSettings,
                (int)Items.SolventIngredients.Pure_water,
                (int)Items.FruitPlantIngredients.Cactus,
                (int)Items.FlowerPlantIngredients.Clover,
                (int)Items.MiscPlantIngredients.Ginkgo_leaves,
                (int)Items.AnimalPartIngredients.Ivory,
                (int)Items.CreatureIngredients.Harpy_Feather);

            PotionRecipe elementalProtection = new PotionRecipe(
                "Elemental Protection",
                245,
                0,
                protectionSettings,
                (int)Items.SolventIngredients.Pure_water,
                (int)Items.CreatureIngredients.Giant_blood,
                (int)Items.CreatureIngredients.Saints_hair,
                (int)Items.MetalIngredients.Gold,
                (int)Items.Gems.Jade,
                (int)Items.Gems.Turquoise,
                (int)Items.Gems.Malachite,
                (int)Items.Gems.Amber);
            elementalProtection.AddSecondaryEffect(variantProperties[(int)DFCareer.Elements.Fire].effectProperties.Key);
            elementalProtection.AddSecondaryEffect(variantProperties[(int)DFCareer.Elements.Frost].effectProperties.Key);
            elementalProtection.AddSecondaryEffect(variantProperties[(int)DFCareer.Elements.DiseaseOrPoison].effectProperties.Key);
            elementalProtection.AddSecondaryEffect(variantProperties[(int)DFCareer.Elements.Shock].effectProperties.Key);


            // Immunity
            PotionRecipe fireImmunity = new PotionRecipe(
                "Fire Immunity",
                350,
                0,
                immunitySettings,
                (int)Items.SolventIngredients.Ichor,
                (int)Items.FruitPlantIngredients.Red_berries,
                (int)Items.CreatureIngredients.Dragons_scales,
                (int)Items.MetalIngredients.Sulphur,
                (int)Items.Gems.Ruby);

            PotionRecipe frostImmunity = new PotionRecipe(
                "Frost Immunity",
                265,
                0,
                immunitySettings,
                (int)Items.SolventIngredients.Ichor,
                (int)Items.MiscPlantIngredients.Pine_branch,
                (int)Items.CreatureIngredients.Mummy_wrappings,
                (int)Items.CreatureIngredients.Werewolfs_blood,
                (int)Items.MetalIngredients.Silver,
                (int)Items.Gems.Sapphire);

            PotionRecipe shockImmunity = new PotionRecipe(
                "Shock Immunity",
                340,
                0,
                immunitySettings,
                (int)Items.SolventIngredients.Ichor,
                (int)Items.FruitPlantIngredients.Yellow_berries,
                (int)Items.MiscPlantIngredients.Root_bulb,
                (int)Items.CreatureIngredients.Wraith_essence,
                (int)Items.MetalIngredients.Lodestone,
                (int)Items.Gems.Diamond);

            PotionRecipe poisonImmunity = new PotionRecipe(
                "Poison Immunity",
                335,
                0,
                immunitySettings,
                (int)Items.SolventIngredients.Ichor,
                (int)Items.AnimalPartIngredients.Snake_venom,
                (int)Items.AnimalPartIngredients.Spider_venom,
                (int)Items.AnimalPartIngredients.Giant_scorpion_stinger,
                (int)Items.CreatureIngredients.Gorgon_snake,
                (int)Items.CreatureIngredients.Troll_blood,
                (int)Items.Gems.Emerald);

            PotionRecipe magickaImmunity = new PotionRecipe(
                "Magicka Immunity",
                460,
                0,
                immunitySettings,
                (int)Items.SolventIngredients.Ichor,
                (int)Items.FruitPlantIngredients.Cactus,
                (int)Items.CreatureIngredients.Fairy_dragon_scales,
                (int)Items.CreatureIngredients.Basilisk_eye,
                (int)Items.CreatureIngredients.Harpy_Feather,
                (int)Items.MetalIngredients.Platinum,
                (int)Items.Gems.Diamond);

            // Assign potion recipes
            fireProtection.TextureRecord = 15;
            frostProtection.TextureRecord = 11;
            shockProtection.TextureRecord = 12;
            poisonProtection.TextureRecord = 14;
            magickaProtection.TextureRecord = 13;
            elementalProtection.TextureRecord = 2;
            fireImmunity.TextureRecord = 34;
            frostImmunity.TextureRecord = 32;
            shockImmunity.TextureRecord = 33;
            poisonImmunity.TextureRecord = 35;
            magickaImmunity.TextureRecord = 6;

            variantProperties[(int)DFCareer.Elements.Fire].potionProperties.Recipes = new PotionRecipe[] { fireProtection, fireImmunity };
            variantProperties[(int)DFCareer.Elements.Frost].potionProperties.Recipes = new PotionRecipe[] { frostProtection, frostImmunity };
            variantProperties[(int)DFCareer.Elements.Shock].potionProperties.Recipes = new PotionRecipe[] { shockProtection, shockImmunity };
            variantProperties[(int)DFCareer.Elements.DiseaseOrPoison].potionProperties.Recipes = new PotionRecipe[] { poisonProtection, poisonImmunity };
            variantProperties[(int)DFCareer.Elements.Magic].potionProperties.Recipes = new PotionRecipe[] { magickaProtection, magickaImmunity, elementalProtection };
        }

        protected override bool IsLikeKind(IncumbentEffect other)
        {
            return (other is ElementalResistance && (other as ElementalResistance).ElementResisted == ElementResisted) ? true : false;
        }

        protected override void AddState(IncumbentEffect incumbent)
        {
            // Stack my rounds onto incumbent
            incumbent.RoundsRemaining += RoundsRemaining;
        }

        public override void ConstantEffect()
        {
            base.ConstantEffect();
            StartResisting();
        }

        public override void Start(EntityEffectManager manager, DaggerfallEntityBehaviour caster = null)
        {
            base.Start(manager, caster);
            StartResisting();
        }

        public override void Resume(EntityEffectManager.EffectSaveData_v1 effectData, EntityEffectManager manager, DaggerfallEntityBehaviour caster = null)
        {
            base.Resume(effectData, manager, caster);
            StartResisting();
        }

        public override void End()
        {
            base.End();
            StopResisting();
        }

        #endregion

        #region Private Methods

        void SetVariantProperties(DFCareer.Elements element)
        {
            int variantIndex = (int)element;

            VariantProperties vp = new VariantProperties();
            vp.effectProperties = properties;
            vp.effectProperties.Key = string.Format("ElementalResistance-{0}", subGroupTextKeys[variantIndex]);
            vp.effectProperties.ClassicKey = MakeClassicKey(8, (byte)variantIndex);
            vp.elementResisted = element;
            variantProperties[variantIndex] = vp;
        }

        void StartResisting()
        {
            // Get peered entity gameobject
            DaggerfallEntityBehaviour entityBehaviour = GetPeeredEntityBehaviour(manager);
            if (!entityBehaviour)
                return;

            entityBehaviour.Entity.SetResistanceFlag(variantProperties[currentVariant].elementResisted, true);
            entityBehaviour.Entity.RaiseResistanceChance(variantProperties[currentVariant].elementResisted, ChanceValue());
        }

        void StopResisting()
        {
            // Get peered entity gameobject
            DaggerfallEntityBehaviour entityBehaviour = GetPeeredEntityBehaviour(manager);
            if (!entityBehaviour)
                return;

            entityBehaviour.Entity.SetResistanceFlag(variantProperties[currentVariant].elementResisted, false);
        }

        #endregion  
    }
}
