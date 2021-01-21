// Project:         Daggerfall Unity Re-imagined for Daggerfall Unity (http://www.dfworkshop.net)
// Copyright:       Copyright (C) 2020 Kirk.O
// License:         MIT License (http://www.opensource.org/licenses/mit-license.php)
// Author:          Kirk.O
// Created On: 	    11/12/2020, 5:15 PM
// Last Edit:		11/12/2020, 5:15 PM
// Version:			1.00
// Special Thanks:  
// Modifier:		

using DaggerfallWorkshop;
using DaggerfallConnect.Arena2;
using DaggerfallWorkshop.Game;
using System.Collections.Generic;

public class CustomTextTokenHolder
{
    public static TextFile.Token[] EquipEncumbranceTextTokens()
    {
        int tokenID = 0;
        float encumbranceValue = GameManager.Instance.PlayerEntity.EquipmentEncumbranceSpeedMod;

        if (encumbranceValue == 1f)
            tokenID = 1;
        else if (encumbranceValue < 1f && encumbranceValue >= 0.90f)
            tokenID = 2;
        else if (encumbranceValue < 0.90f && encumbranceValue >= 0.75f)
            tokenID = 3;
        else if (encumbranceValue < 0.75f && encumbranceValue >= 0.60f)
            tokenID = 4;
        else if (encumbranceValue < 0.60f && encumbranceValue >= 0.45f)
            tokenID = 5;
        else if (encumbranceValue < 0.45f && encumbranceValue >= 0.30f)
            tokenID = 6;
        else if (encumbranceValue < 0.30f && encumbranceValue >= 0.15f)
            tokenID = 7;
        else
            tokenID = 8;

        switch (tokenID)
        {
            case 1:
                return DaggerfallUnity.Instance.TextProvider.CreateTokens(
                    TextFile.Formatting.JustifyCenter,
                    "Your equipment does not encumber you at all.");
            case 2:
                return DaggerfallUnity.Instance.TextProvider.CreateTokens(
                    TextFile.Formatting.JustifyCenter,
                    "You are barely encumbered by your equipment.");
            case 3:
                return DaggerfallUnity.Instance.TextProvider.CreateTokens(
                    TextFile.Formatting.JustifyCenter,
                    "You are slightly encumbered by your equipment.");
            case 4:
                return DaggerfallUnity.Instance.TextProvider.CreateTokens(
                    TextFile.Formatting.JustifyCenter,
                    "You are fairly encumbered by your equipment.");
            case 5:
                return DaggerfallUnity.Instance.TextProvider.CreateTokens(
                    TextFile.Formatting.JustifyCenter,
                    "Your equipment is moderately encumbering you.");
            case 6:
                return DaggerfallUnity.Instance.TextProvider.CreateTokens(
                    TextFile.Formatting.JustifyCenter,
                    "You are greatly encumbered by your equipment.");
            case 7:
                return DaggerfallUnity.Instance.TextProvider.CreateTokens(
                    TextFile.Formatting.JustifyCenter,
                    "You can barely move.");
            case 8:
                return DaggerfallUnity.Instance.TextProvider.CreateTokens(
                    TextFile.Formatting.JustifyCenter,
                    "Frozen molasses would beat you in a footrace.");
            default:
                return DaggerfallUnity.Instance.TextProvider.CreateTokens(
                    TextFile.Formatting.JustifyCenter,
                    "Text Token Not Found");
        }
    }

    public static TextFile.Token[] RepairServiceTextTokens(int index, int cost = 0)
    {
        if (index == 1)
        {
            return DaggerfallUnity.Instance.TextProvider.CreateTokens(
                TextFile.Formatting.JustifyCenter,
                "You don't appear to have the needed materials",
                "on your person. After checking my emergency",
                "stock I seem to have the remaining materials",
                "needed to fill your order. Would you like me",
                "to cover the missing materials to complete",
                "your order? I'll have to charge extra for the",
                "use of my reserves, i'm sure you understand.");
        }
        else if (index == 2)
        {
            return DaggerfallUnity.Instance.TextProvider.CreateTokens(
                TextFile.Formatting.JustifyCenter,
                "Alright, with the added cost of my reserve",
                "stock added in. The final cost for this",
                "order is " + cost + " gold, plus any materials",
                "that you brought along. Does that sound",
                "reasonable to you?");
        }
        else if (index == 3)
        {
            return DaggerfallUnity.Instance.TextProvider.CreateTokens(
                TextFile.Formatting.JustifyCenter,
                "This order won't require any special materials",
                "to bring this stuff back, good as new, not with",
                "a skilled professional such as myself, hehe.",
                "My labor will set you back " + cost + " gold,",
                "how does that sound?");
        }
        else
        {
            return DaggerfallUnity.Instance.TextProvider.CreateTokens(
                    TextFile.Formatting.JustifyCenter,
                    "Text Token Not Found");
        }
    }

    public static TextFile.Token[] PotionSpellEffectsTextTokens(DaggerfallWorkshop.Game.Items.DaggerfallUnityItem item)
    {
        TextFile.Token[] tokens = null;
        string effectDuration = "";
        string effectMagnitude = "";
        string effectChance = "";

        DaggerfallWorkshop.Game.MagicAndEffects.EntityEffectBroker effectBroker = GameManager.Instance.EntityEffectBroker;
        DaggerfallWorkshop.Game.MagicAndEffects.PotionRecipe potionRecipe = effectBroker.GetPotionRecipe(item.PotionRecipeKey);
        DaggerfallWorkshop.Game.MagicAndEffects.IEntityEffect potionEffect = effectBroker.GetPotionRecipeEffect(potionRecipe);

        DaggerfallWorkshop.Game.MagicAndEffects.EffectEntry[] potionEffects;
        List<string> secondaryEffects = potionRecipe.SecondaryEffects;
        if (secondaryEffects != null)
        {
            potionEffects = new DaggerfallWorkshop.Game.MagicAndEffects.EffectEntry[secondaryEffects.Count + 1];
            potionEffects[0] = new DaggerfallWorkshop.Game.MagicAndEffects.EffectEntry(potionEffect.Key, potionRecipe.Settings);
            for (int i = 0; i < secondaryEffects.Count; i++)
            {
                DaggerfallWorkshop.Game.MagicAndEffects.IEntityEffect effect = effectBroker.GetEffectTemplate(secondaryEffects[i]);
                potionEffects[i + 1] = new DaggerfallWorkshop.Game.MagicAndEffects.EffectEntry(effect.Key, potionRecipe.Settings);
            }
        }
        else
        {
            potionEffects = new DaggerfallWorkshop.Game.MagicAndEffects.EffectEntry[] { new DaggerfallWorkshop.Game.MagicAndEffects.EffectEntry(potionEffect.Key, potionRecipe.Settings) };
        }


        for (int i = 0; i < potionEffects.Length; ++i)
        {
            if (potionEffects[i].Settings.DurationBase <= 1)
                effectDuration = "Instant";
            else
                effectDuration = potionEffects[i].Settings.DurationBase.ToString() + " Rounds";

            if (potionEffects[i].Settings.MagnitudeBaseMax <= 1 && !potionEffects[i].Key.Contains("Regenerate"))
                effectMagnitude = "N/A";
            else
                effectMagnitude = potionEffects[i].Settings.MagnitudeBaseMax.ToString();

            if (potionEffects[i].Settings.ChanceBase <= 1)
                effectChance = "N/A";
            else
                effectChance = potionEffects[i].Settings.ChanceBase.ToString() + "%";

            string effectName = potionEffects[i].Key.Replace("-", " ");
            effectName = effectName.Replace("Elemental", "");
            effectName = effectName.Replace("SpellResistance", "Spell Resistance");
            effectName = effectName.Replace("Resistance ", "Resist ");
            effectName = effectName.Replace("WaterBreathing", "Water Breathing");
            effectName = effectName.Replace("SpellPoints", "Spell-Points");
            effectName = effectName.Replace("FreeAction", "Free Action");
            effectName = effectName.Replace("SpellReflection", "Spell Reflection");
            effectName = effectName.Replace("SpellAbsorption", "Spell Absorption");
            effectName = effectName.Replace("WaterWalking", "Water Walking");
            effectName = effectName.Replace("ComprehendLanguages", "Comprehend Languages");
            effectName = effectName + ": " + " Dur = " + effectDuration + "," + "  Mag = " + effectMagnitude + "," + "  Chan = " + effectChance;
            TextFile.Token[] effectTitle = DaggerfallUnity.Instance.TextProvider.CreateTokens(TextFile.Formatting.JustifyCenter, effectName);
            tokens = TextFile.AppendTokens(tokens, effectTitle, false);
        }

        return tokens;
    }
}