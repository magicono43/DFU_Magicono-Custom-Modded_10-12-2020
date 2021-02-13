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

using UnityEngine;
using DaggerfallConnect;
using DaggerfallConnect.Utility;
using DaggerfallWorkshop.Utility;
using DaggerfallWorkshop.Game.Utility;
using DaggerfallWorkshop.Game.Entity;
using DaggerfallWorkshop.Game.MagicAndEffects.MagicEffects;
using DaggerfallWorkshop.Game.Formulas;
using DaggerfallWorkshop.Game.Items;

namespace DaggerfallWorkshop.Game.UserInterface
{
    /// <summary>
    /// Implements character paper doll.
    /// </summary>
    public class PaperDoll : Panel
    {
        #region UI Rects

        const int paperDollWidth = 110;
        const int paperDollHeight = 184;
        const int waistHeight = 40;

        readonly DFSize backgroundFullSize = new DFSize(125, 198);
        readonly Rect backgroundSubRect = new Rect(8, 7, paperDollWidth, paperDollHeight);

        #endregion

        #region Fields

        static readonly Color32 maskColor = new Color(255, 0, 200, 0);   // Special mask colour used on helmets, cloaks, etc.

        const bool showBackgroundLayer = true;
        const bool showCharacterLayer = true;

        readonly Panel backgroundPanel = new Panel();
        readonly Panel characterPanel = new Panel();

        readonly TextLabel[] armourLabelsB = new TextLabel[DaggerfallEntity.NumberBodyParts];
        readonly Vector2[] armourLabelPosB = new Vector2[] { new Vector2(82, 2), new Vector2(2, 28), new Vector2(92, 30), new Vector2(3, 58), new Vector2(1, 90), new Vector2(6, 120), new Vector2(6, 156) };
        readonly TextLabel[] armourLabelsS = new TextLabel[DaggerfallEntity.NumberBodyParts];
        readonly Vector2[] armourLabelPosS = new Vector2[] { new Vector2(82, 8), new Vector2(2, 34), new Vector2(92, 36), new Vector2(3, 64), new Vector2(1, 96), new Vector2(6, 126), new Vector2(6, 162) };
        readonly TextLabel[] armourLabelsP = new TextLabel[DaggerfallEntity.NumberBodyParts];
        readonly Vector2[] armourLabelPosP = new Vector2[] { new Vector2(82, 14), new Vector2(2, 40), new Vector2(92, 42), new Vector2(3, 70), new Vector2(1, 102), new Vector2(6, 132), new Vector2(6, 168) };
        readonly TextLabel[] shieldLabels = new TextLabel[DaggerfallEntity.NumberBodyParts];
        readonly Vector2[] shieldLabelsPos = new Vector2[] { new Vector2(82, 20), new Vector2(2, 46), new Vector2(92, 48), new Vector2(3, 76), new Vector2(1, 108), new Vector2(6, 138), new Vector2(6, 174) };

        string lastBackgroundName = string.Empty;

        #endregion

        #region Properties

        public static Color32 MaskColor
        {
            get { return maskColor; }
        }

        #endregion

        #region Constructors

        public PaperDoll()
        {
            // Setup panels
            Size = new Vector2(paperDollWidth, paperDollHeight);
            characterPanel.Size = new Vector2(paperDollWidth, paperDollHeight);

            // Add panels
            Components.Add(backgroundPanel);
            Components.Add(characterPanel);

            // Set initial display flags
            backgroundPanel.Enabled = showBackgroundLayer;
            characterPanel.Enabled = showCharacterLayer;

            for (int bpIdx = 0; bpIdx < DaggerfallEntity.NumberBodyParts; bpIdx++)
            {
                armourLabelsB[bpIdx] = DaggerfallUI.AddDefaultShadowedTextLabel(armourLabelPosB[bpIdx], characterPanel);
                armourLabelsB[bpIdx].Text = "";
                armourLabelsS[bpIdx] = DaggerfallUI.AddDefaultShadowedTextLabel(armourLabelPosS[bpIdx], characterPanel);
                armourLabelsS[bpIdx].Text = "";
                armourLabelsP[bpIdx] = DaggerfallUI.AddDefaultShadowedTextLabel(armourLabelPosP[bpIdx], characterPanel);
                armourLabelsP[bpIdx].Text = "";
                shieldLabels[bpIdx] = DaggerfallUI.AddDefaultShadowedTextLabel(shieldLabelsPos[bpIdx], characterPanel);
                shieldLabels[bpIdx].Text = "";
            }
        }

        #endregion

        #region Overrides

        public override void Update()
        {
            // Update display flags
            backgroundPanel.Enabled = showBackgroundLayer;
            characterPanel.Enabled = showCharacterLayer;

            base.Update();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Redraws paper doll image and selection mask.
        /// Call this after changing equipment, loading a new game, etc.
        /// Only call when required as constructing paper doll image is expensive.
        /// </summary>
        /// <param name="playerEntity"></param>
        public void Refresh(bool toggleArmorValues = false, PlayerEntity playerEntity = null)
        {
            // Get current player entity if one not provided
            if (playerEntity == null)
                playerEntity = GameManager.Instance.PlayerEntity;

            // Racial override can suppress body and items
            bool suppressBody = false;
            RacialOverrideEffect racialOverride = GameManager.Instance.PlayerEffectManager.GetRacialOverrideEffect();
            if (racialOverride != null)
                suppressBody = racialOverride.SuppressPaperDollBodyAndItems;

            // Update background
            RefreshBackground(playerEntity);

            // Display paper doll render
            DaggerfallUI.Instance.PaperDollRenderer.Refresh(PaperDollRenderer.LayerFlags.All, playerEntity);
            characterPanel.BackgroundTexture = DaggerfallUI.Instance.PaperDollRenderer.PaperDollTexture;

            // Update armour values
            if (toggleArmorValues)
                RefreshArmourValues(playerEntity, suppressBody);
        }

        /// <summary>
        /// Gets equip index at position.
        /// </summary>
        /// <param name="x">X position to sample.</param>
        /// <param name="y">Y position to sample.</param>
        /// <returns>Equip index or 0xff if point empty.</returns>
        public byte GetEquipIndex(int x, int y)
        {
            return DaggerfallUI.Instance.PaperDollRenderer.GetEquipIndex(x, y);
        }

        #endregion

        #region Private Methods

        // Refresh armour value labels
        void RefreshArmourValues(PlayerEntity playerEntity, bool suppress = false)
        {
            DaggerfallUnityItem shield = playerEntity.ItemEquipTable.GetItem(EquipSlots.LeftHand); // Checks if character is using a shield or not.
            if (shield != null && !shield.IsShield)
                shield = null;
            bool hasShield = (shield != null) ? true : false; // if shield has a value, then true, if not, false.
            int[] shieldCovered = { 0, 0, 0, 0, 0, 0, 0 }; // shield's effect on the 7 armor values
            if (hasShield)
            {
                int armorBonus = shield.GetShieldArmorValue();
                BodyParts[] protectedBodyParts = shield.GetShieldProtectedBodyParts();

                foreach (var BodyPart in protectedBodyParts)
                {
                    shieldCovered[(int)BodyPart] = armorBonus;
                }
            }


            for (int bpIdx = 0; bpIdx < DaggerfallEntity.NumberBodyParts; bpIdx++)
            {
                int armorMod = playerEntity.DecreasedArmorValueModifier - playerEntity.IncreasedArmorValueModifier;
                float armorDamReduc = 0f;
                float bpAvB = 0;
                float bpAvS = 0;
                float bpAvP = 0;
                string bludResist = "";
                string slasResist = "";
                string pierResist = "";

                EquipSlots hitSlot = DaggerfallUnityItem.GetEquipSlotForBodyPart((BodyParts)bpIdx);
                DaggerfallUnityItem armor = playerEntity.ItemEquipTable.GetItem(hitSlot);
                if (armor != null)
                {
                    armorDamReduc = FormulaHelper.PercentageDamageReductionCalculation(armor, false, 0f, 1);
                    bpAvB = (int)Mathf.Round((armorDamReduc - 1) * -100f) - armorMod;

                    armorDamReduc = FormulaHelper.PercentageDamageReductionCalculation(armor, false, 0f, 2);
                    bpAvS = (int)Mathf.Round((armorDamReduc - 1) * -100f) - armorMod;

                    armorDamReduc = FormulaHelper.PercentageDamageReductionCalculation(armor, false, 0f, 3);
                    bpAvP = (int)Mathf.Round((armorDamReduc - 1) * -100f) - armorMod;

                    bludResist = "B: " + bpAvB + "%";
                    slasResist = "S: " + bpAvS + "%";
                    pierResist = "P: " + bpAvP + "%";

                    if (armor.ItemGroup == ItemGroups.Jewellery)
                    {
                        bludResist = "";
                        slasResist = "";
                        pierResist = "";
                    }
                }
                armourLabelsB[bpIdx].Text = (!suppress) ? bludResist : string.Empty;
                armourLabelsS[bpIdx].Text = (!suppress) ? slasResist : string.Empty;
                armourLabelsP[bpIdx].Text = (!suppress) ? pierResist : string.Empty;

                if (armorMod < 0)
                {
                    armourLabelsB[bpIdx].TextColor = DaggerfallUI.DaggerfallUnityStatDrainedTextColor2;
                    armourLabelsS[bpIdx].TextColor = DaggerfallUI.DaggerfallUnityStatDrainedTextColor2;
                    armourLabelsP[bpIdx].TextColor = DaggerfallUI.DaggerfallUnityStatDrainedTextColor2;
                }
                else if (armorMod > 0)
                {
                    armourLabelsB[bpIdx].TextColor = DaggerfallUI.DaggerfallUnityStatIncreasedTextColor2;
                    armourLabelsS[bpIdx].TextColor = DaggerfallUI.DaggerfallUnityStatIncreasedTextColor2;
                    armourLabelsP[bpIdx].TextColor = DaggerfallUI.DaggerfallUnityStatIncreasedTextColor2;
                }
                else
                {
                    armourLabelsB[bpIdx].TextColor = DaggerfallUI.DaggerfallDefaultTextColor;
                    armourLabelsS[bpIdx].TextColor = DaggerfallUI.DaggerfallDefaultTextColor;
                    armourLabelsP[bpIdx].TextColor = DaggerfallUI.DaggerfallDefaultTextColor;
                }

                if (hasShield)
                {
                    bool covered = (shieldCovered[bpIdx] > 0) ? true : false;
                    float shieldBlockChan = (int)Mathf.Round(FormulaHelper.ShieldBlockChance(shield, playerEntity, covered));
                    string shieldText = "Bk:" + shieldBlockChan + "%";

                    if (covered)
                    {
                        shieldLabels[bpIdx].Text = (!suppress) ? shieldText : string.Empty;
                        shieldLabels[bpIdx].TextColor = DaggerfallUI.DaggerfallDefaultTextColor;
                    }
                    else
                    {
                        shieldLabels[bpIdx].Text = (!suppress) ? shieldText : string.Empty;
                        shieldLabels[bpIdx].TextColor = DaggerfallUI.DaggerfallDefaultTextColor;
                    }
                }
                else
                    shieldLabels[bpIdx].Text = "";
            }
        }

        // Update player background panel
        void RefreshBackground(PlayerEntity entity)
        {
            // Allow racial override background (vampire / transformed were-creature)
            // If racial override is not present or returns null then standard racial background will be used
            // The racial override has full control over which texture is displayed, such as when were-creature transformed or not
            Texture2D customBackground;
            RacialOverrideEffect racialOverride = GameManager.Instance.PlayerEffectManager.GetRacialOverrideEffect();
            if (racialOverride != null && racialOverride.GetCustomPaperDollBackgroundTexture(entity, out customBackground))
            {
                backgroundPanel.BackgroundTexture = customBackground;
                backgroundPanel.Size = new Vector2(paperDollWidth, paperDollHeight);
                lastBackgroundName = string.Empty;
                return;
            }

            // Use standard racial background
            string backgroundName = GetPaperDollBackground(entity);
            if (lastBackgroundName != backgroundName)
            {
                Texture2D texture = ImageReader.GetTexture(backgroundName, 0, 0, false);
                backgroundPanel.BackgroundTexture = ImageReader.GetSubTexture(texture, backgroundSubRect, backgroundFullSize);
                backgroundPanel.Size = new Vector2(paperDollWidth, paperDollHeight);
                lastBackgroundName = backgroundName;
            }
        }

        readonly char[] regionBackgroundIdxChars =
            {'3','1','2','2', '2','0','5','1', '5','2','1','1', '2','2','2','0', '2','0','2','2', '3','0','5','6', '2','2','2','2', '0','0','0','0',
             '0','6','6','6', '0','6','6','0', '6','0','0','3', '3','3','3','3', '3','5','5','5', '5','1','3','3', '3','2','0','0', '2','3' };

        string GetPaperDollBackground(PlayerEntity entity)
        {
            if (DaggerfallUnity.Settings.EnableGeographicBackgrounds)
            {
                PlayerGPS playerGPS = GameManager.Instance.PlayerGPS;
                PlayerEnterExit playerEnterExit = GameManager.Instance.PlayerEnterExit;
                DFPosition position = playerGPS.CurrentMapPixel;
                int region = DaggerfallUnity.Instance.ContentReader.MapFileReader.GetPoliticIndex(position.X, position.Y) - 128;
                if (region < 0 || region >= DaggerfallUnity.Instance.ContentReader.MapFileReader.RegionCount || region >= regionBackgroundIdxChars.Length)
                    return entity.RaceTemplate.PaperDollBackground;

                // Set background based on location.
                if (playerGPS.IsPlayerInTown(true))
                    return "SCBG04I0.IMG";                                          // Town
                else if (playerEnterExit.IsPlayerInsideDungeon)
                    return "SCBG07I0.IMG";                                          // Dungeon
                else if (playerGPS.CurrentLocation.MapTableData.LocationType == DFRegion.LocationTypes.Graveyard && playerGPS.IsPlayerInLocationRect)
                    return "SCBG08I0.IMG";                                          // Graveyard
                else                            
                    return "SCBG0" + regionBackgroundIdxChars[region] + "I0.IMG";   // Region
            }
            else
            {
                return entity.RaceTemplate.PaperDollBackground;
            }
        }

        #endregion
    }
}