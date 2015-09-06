﻿// Project:         Daggerfall Tools For Unity
// Copyright:       Copyright (C) 2009-2015 Daggerfall Workshop
// Web Site:        http://www.dfworkshop.net
// License:         MIT License (http://www.opensource.org/licenses/mit-license.php)
// Source Code:     https://github.com/Interkarma/daggerfall-unity
// Original Author: Gavin Clayton (interkarma@dfworkshop.net)
// Contributors:    
// 
// Notes:
//

using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using DaggerfallConnect;
using DaggerfallConnect.Arena2;
using DaggerfallWorkshop;
using DaggerfallWorkshop.Utility;
using DaggerfallWorkshop.Game.UserInterface;
using DaggerfallWorkshop.Game.Player;
using DaggerfallWorkshop.Game.Entity;

namespace DaggerfallWorkshop.Game.UserInterfaceWindows
{
    /// <summary>
    /// Implements character reflexes picker.
    /// </summary>
    public class CreateCharReflexSelect : DaggerfallPopupWindow
    {
        const string nativeImgName = "CHAR05I0.IMG";
        const int strPlayerReflexesDetermine = 307;

        Texture2D nativeTexture;

        ReflexPicker reflexPicker;

        public CreateCharReflexSelect(IUserInterfaceManager uiManager)
            : base(uiManager)
        {
        }

        public PlayerReflexes PlayerReflexes
        {
            get { return reflexPicker.PlayerReflexes; }
        }

        protected override void Setup()
        {
            // Load native texture
            nativeTexture = DaggerfallUI.GetTextureFromImg(nativeImgName);
            if (!nativeTexture)
                throw new Exception("CreateCharReflexSelect: Could not load native texture.");

            // Setup native panel background
            NativePanel.BackgroundTexture = nativeTexture;

            // Setup info panel
            Panel infoPanel = new Panel();
            DaggerfallUI.Instance.SetDaggerfallPopupStyle(infoPanel);
            NativePanel.Components.Add(infoPanel);
            infoPanel.HorizontalAlignment = HorizontalAlignment.Center;
            infoPanel.Position = new Vector2(0, 15);

            // Setup info text
            MultilineTextLabel infoText = new MultilineTextLabel();
            infoPanel.Components.Add(infoText);
            infoText.SetTextTokens(DaggerfallUnity.Instance.TextProvider.GetRSCTokens(strPlayerReflexesDetermine));
            infoText.HorizontalAlignment = HorizontalAlignment.Center;
            infoText.VerticalAlignment = VerticalAlignment.Middle;
            infoPanel.Size = infoText.Size;

            // Setup button picker
            reflexPicker = new ReflexPicker();
            NativePanel.Components.Add(reflexPicker);
            reflexPicker.Position = new Vector2(127, 148f);

            // Add "OK" button
            Button okButton = DaggerfallUI.AddButton(new Rect(263, 172, 39, 22), NativePanel);
            okButton.OnMouseClick += OkButton_OnMouseClick;
        }

        #region Event Handlers

        void OkButton_OnMouseClick(BaseScreenComponent sender, Vector2 position)
        {
            CloseWindow();
        }

        #endregion
    }
}