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
}