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

using System;
using System.Collections.Generic;
using DaggerfallConnect;
using DaggerfallWorkshop.Game;
using DaggerfallWorkshop.Game.Entity;
using DaggerfallWorkshop.Game.Formulas;
using DaggerfallWorkshop.Game.Items;
using UnityEngine;

namespace DaggerfallWorkshop.Utility
{
    /// <summary>
    /// Static definitions for enemies and their animations.
    /// Remaining data is read from MONSTER.BSA.
    /// </summary>
    public static class EnemyBasics
    {
        #region Enemy Animations

        // Speeds in frames-per-second
        public static int MoveAnimSpeed = 6;
        public static int FlyAnimSpeed = 10;
        public static int PrimaryAttackAnimSpeed = 10;
        public static int HurtAnimSpeed = 4;
        public static int IdleAnimSpeed = 4;
        public static int RangedAttack1AnimSpeed = 10;
        public static int RangedAttack2AnimSpeed = 10;

        /// <summary>Struct for return values of method that alters enemy stat values for formula purposes on the fly.</summary>
        public struct CustomEnemyStatValues
        {
            public int weaponSkillCustom;
            public int critSkillCustom;
            public int dodgeSkillCustom;
            public int strengthCustom; // Not currently used
            public int agilityCustom;
            public int speedCustom;
            public int willpowerCustom; // Not currently used
            public int luckCustom;
        }

        // Move animations (double as idle animations for swimming and flying enemies, and enemies without idle animations)
        public static MobileAnimation[] MoveAnims = new MobileAnimation[]
        {
            new MobileAnimation() {Record = 0, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing south (front facing player)
            new MobileAnimation() {Record = 1, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing south-west
            new MobileAnimation() {Record = 2, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing west
            new MobileAnimation() {Record = 3, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing north-west
            new MobileAnimation() {Record = 4, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing north (back facing player)
            new MobileAnimation() {Record = 3, FramePerSecond = MoveAnimSpeed, FlipLeftRight = true},              // Facing north-east
            new MobileAnimation() {Record = 2, FramePerSecond = MoveAnimSpeed, FlipLeftRight = true},              // Facing east
            new MobileAnimation() {Record = 1, FramePerSecond = MoveAnimSpeed, FlipLeftRight = true},              // Facing south-east
        };

        // PrimaryAttack animations
        public static MobileAnimation[] PrimaryAttackAnims = new MobileAnimation[]
        {
            new MobileAnimation() {Record = 5, FramePerSecond = PrimaryAttackAnimSpeed, FlipLeftRight = false},    // Facing south (front facing player)
            new MobileAnimation() {Record = 6, FramePerSecond = PrimaryAttackAnimSpeed, FlipLeftRight = false},    // Facing south-west
            new MobileAnimation() {Record = 7, FramePerSecond = PrimaryAttackAnimSpeed, FlipLeftRight = false},    // Facing west
            new MobileAnimation() {Record = 8, FramePerSecond = PrimaryAttackAnimSpeed, FlipLeftRight = false},    // Facing north-west
            new MobileAnimation() {Record = 9, FramePerSecond = PrimaryAttackAnimSpeed, FlipLeftRight = false},    // Facing north (back facing player)
            new MobileAnimation() {Record = 8, FramePerSecond = PrimaryAttackAnimSpeed, FlipLeftRight = true},     // Facing north-east
            new MobileAnimation() {Record = 7, FramePerSecond = PrimaryAttackAnimSpeed, FlipLeftRight = true},     // Facing east
            new MobileAnimation() {Record = 6, FramePerSecond = PrimaryAttackAnimSpeed, FlipLeftRight = true},     // Facing south-east
        };

        // Hurt animations
        public static MobileAnimation[] HurtAnims = new MobileAnimation[]
        {
            new MobileAnimation() {Record = 10, FramePerSecond = HurtAnimSpeed, FlipLeftRight = false},            // Facing south (front facing player)
            new MobileAnimation() {Record = 11, FramePerSecond = HurtAnimSpeed, FlipLeftRight = false},            // Facing south-west
            new MobileAnimation() {Record = 12, FramePerSecond = HurtAnimSpeed, FlipLeftRight = false},            // Facing west
            new MobileAnimation() {Record = 13, FramePerSecond = HurtAnimSpeed, FlipLeftRight = false},            // Facing north-west
            new MobileAnimation() {Record = 14, FramePerSecond = HurtAnimSpeed, FlipLeftRight = false},            // Facing north (back facing player)
            new MobileAnimation() {Record = 13, FramePerSecond = HurtAnimSpeed, FlipLeftRight = true},             // Facing north-east
            new MobileAnimation() {Record = 12, FramePerSecond = HurtAnimSpeed, FlipLeftRight = true},             // Facing east
            new MobileAnimation() {Record = 11, FramePerSecond = HurtAnimSpeed, FlipLeftRight = true},             // Facing south-east
        };

        // Idle animations (most monsters have a static idle sprite)
        public static MobileAnimation[] IdleAnims = new MobileAnimation[]
        {
            new MobileAnimation() {Record = 15, FramePerSecond = IdleAnimSpeed, FlipLeftRight = false},            // Facing south (front facing player)
            new MobileAnimation() {Record = 16, FramePerSecond = IdleAnimSpeed, FlipLeftRight = false},            // Facing south-west
            new MobileAnimation() {Record = 17, FramePerSecond = IdleAnimSpeed, FlipLeftRight = false},            // Facing west
            new MobileAnimation() {Record = 18, FramePerSecond = IdleAnimSpeed, FlipLeftRight = false},            // Facing north-west
            new MobileAnimation() {Record = 19, FramePerSecond = IdleAnimSpeed, FlipLeftRight = false},            // Facing north (back facing player)
            new MobileAnimation() {Record = 18, FramePerSecond = IdleAnimSpeed, FlipLeftRight = true},             // Facing north-east
            new MobileAnimation() {Record = 17, FramePerSecond = IdleAnimSpeed, FlipLeftRight = true},             // Facing east
            new MobileAnimation() {Record = 16, FramePerSecond = IdleAnimSpeed, FlipLeftRight = true},             // Facing south-east
        };

        // RangedAttack1 animations (humanoid mobiles only)
        public static MobileAnimation[] RangedAttack1Anims = new MobileAnimation[]
        {
            new MobileAnimation() {Record = 20, FramePerSecond = RangedAttack1AnimSpeed, FlipLeftRight = false},   // Facing south (front facing player)
            new MobileAnimation() {Record = 21, FramePerSecond = RangedAttack1AnimSpeed, FlipLeftRight = false},   // Facing south-west
            new MobileAnimation() {Record = 22, FramePerSecond = RangedAttack1AnimSpeed, FlipLeftRight = false},   // Facing west
            new MobileAnimation() {Record = 23, FramePerSecond = RangedAttack1AnimSpeed, FlipLeftRight = false},   // Facing north-west
            new MobileAnimation() {Record = 24, FramePerSecond = RangedAttack1AnimSpeed, FlipLeftRight = false},   // Facing north (back facing player)
            new MobileAnimation() {Record = 23, FramePerSecond = RangedAttack1AnimSpeed, FlipLeftRight = true},    // Facing north-east
            new MobileAnimation() {Record = 22, FramePerSecond = RangedAttack1AnimSpeed, FlipLeftRight = true},    // Facing east
            new MobileAnimation() {Record = 21, FramePerSecond = RangedAttack1AnimSpeed, FlipLeftRight = true},    // Facing south-east
        };

        // RangedAttack2 animations (475, 489, 490 humanoid mobiles only)
        public static MobileAnimation[] RangedAttack2Anims = new MobileAnimation[]
        {
            new MobileAnimation() {Record = 25, FramePerSecond = RangedAttack2AnimSpeed, FlipLeftRight = false},   // Facing south (front facing player)
            new MobileAnimation() {Record = 26, FramePerSecond = RangedAttack2AnimSpeed, FlipLeftRight = false},   // Facing south-west
            new MobileAnimation() {Record = 27, FramePerSecond = RangedAttack2AnimSpeed, FlipLeftRight = false},   // Facing west
            new MobileAnimation() {Record = 28, FramePerSecond = RangedAttack2AnimSpeed, FlipLeftRight = false},   // Facing north-west
            new MobileAnimation() {Record = 29, FramePerSecond = RangedAttack2AnimSpeed, FlipLeftRight = false},   // Facing north (back facing player)
            new MobileAnimation() {Record = 28, FramePerSecond = RangedAttack2AnimSpeed, FlipLeftRight = true},    // Facing north-east
            new MobileAnimation() {Record = 27, FramePerSecond = RangedAttack2AnimSpeed, FlipLeftRight = true},    // Facing east
            new MobileAnimation() {Record = 26, FramePerSecond = RangedAttack2AnimSpeed, FlipLeftRight = true},    // Facing south-east
        };

        // Female thief idle animations
        public static MobileAnimation[] FemaleThiefIdleAnims = new MobileAnimation[]
        {
            new MobileAnimation() {Record = 15, FramePerSecond = IdleAnimSpeed, FlipLeftRight = false},            // Facing south (front facing player)
            new MobileAnimation() {Record = 11, FramePerSecond = IdleAnimSpeed, FlipLeftRight = false},            // Facing south-west
            new MobileAnimation() {Record = 17, FramePerSecond = IdleAnimSpeed, FlipLeftRight = false},            // Facing west
            new MobileAnimation() {Record = 18, FramePerSecond = IdleAnimSpeed, FlipLeftRight = false},            // Facing north-west
            new MobileAnimation() {Record = 19, FramePerSecond = IdleAnimSpeed, FlipLeftRight = false},            // Facing north (back facing player)
            new MobileAnimation() {Record = 18, FramePerSecond = IdleAnimSpeed, FlipLeftRight = true},             // Facing north-east
            new MobileAnimation() {Record = 17, FramePerSecond = IdleAnimSpeed, FlipLeftRight = true},             // Facing east
            new MobileAnimation() {Record = 11, FramePerSecond = IdleAnimSpeed, FlipLeftRight = true},             // Facing south-east
        };

        // Rat idle animations
        public static MobileAnimation[] RatIdleAnims = new MobileAnimation[]
        {
            new MobileAnimation() {Record = 15, FramePerSecond = IdleAnimSpeed, FlipLeftRight = false},            // Facing south (front facing player)
            new MobileAnimation() {Record = 16, FramePerSecond = IdleAnimSpeed, FlipLeftRight = true},             // Facing south-west
            new MobileAnimation() {Record = 17, FramePerSecond = IdleAnimSpeed, FlipLeftRight = true},             // Facing west
            new MobileAnimation() {Record = 18, FramePerSecond = IdleAnimSpeed, FlipLeftRight = true},             // Facing north-west
            new MobileAnimation() {Record = 19, FramePerSecond = IdleAnimSpeed, FlipLeftRight = false},            // Facing north (back facing player)
            new MobileAnimation() {Record = 18, FramePerSecond = IdleAnimSpeed, FlipLeftRight = false},            // Facing north-east
            new MobileAnimation() {Record = 17, FramePerSecond = IdleAnimSpeed, FlipLeftRight = false},            // Facing east
            new MobileAnimation() {Record = 16, FramePerSecond = IdleAnimSpeed, FlipLeftRight = false},            // Facing south-east
        };

        // Wraith and ghost idle/move animations
        public static MobileAnimation[] GhostWraithMoveAnims = new MobileAnimation[]
        {
            new MobileAnimation() {Record = 0, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing south (front facing player)
            new MobileAnimation() {Record = 1, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing south-west
            new MobileAnimation() {Record = 2, FramePerSecond = MoveAnimSpeed, FlipLeftRight = true},              // Facing west
            new MobileAnimation() {Record = 3, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing north-west
            new MobileAnimation() {Record = 4, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing north (back facing player)
            new MobileAnimation() {Record = 3, FramePerSecond = MoveAnimSpeed, FlipLeftRight = true},              // Facing north-east
            new MobileAnimation() {Record = 2, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing east
            new MobileAnimation() {Record = 1, FramePerSecond = MoveAnimSpeed, FlipLeftRight = true},              // Facing south-east
        };

        // Ghost and Wraith attack animations
        public static MobileAnimation[] GhostWraithAttackAnims = new MobileAnimation[]
        {
            new MobileAnimation() {Record = 5, FramePerSecond = PrimaryAttackAnimSpeed, FlipLeftRight = false},    // Facing south (front facing player)
            new MobileAnimation() {Record = 6, FramePerSecond = PrimaryAttackAnimSpeed, FlipLeftRight = false},    // Facing south-west
            new MobileAnimation() {Record = 7, FramePerSecond = PrimaryAttackAnimSpeed, FlipLeftRight = true},     // Facing west
            new MobileAnimation() {Record = 8, FramePerSecond = PrimaryAttackAnimSpeed, FlipLeftRight = false},    // Facing north-west
            new MobileAnimation() {Record = 9, FramePerSecond = PrimaryAttackAnimSpeed, FlipLeftRight = false},    // Facing north (back facing player)
            new MobileAnimation() {Record = 8, FramePerSecond = PrimaryAttackAnimSpeed, FlipLeftRight = true},     // Facing north-east
            new MobileAnimation() {Record = 7, FramePerSecond = PrimaryAttackAnimSpeed, FlipLeftRight = false},    // Facing east
            new MobileAnimation() {Record = 6, FramePerSecond = PrimaryAttackAnimSpeed, FlipLeftRight = true},     // Facing south-east
        };

        // Seducer special animations - has player-facing orientation only
        public static MobileAnimation[] SeducerTransform1Anims = new MobileAnimation[]
        {
            new MobileAnimation() {Record = 23, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing south (front facing player)
            new MobileAnimation() {Record = 23, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing south (front facing player)
            new MobileAnimation() {Record = 23, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing south (front facing player)
            new MobileAnimation() {Record = 23, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing south (front facing player)
            new MobileAnimation() {Record = 23, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing south (front facing player)
            new MobileAnimation() {Record = 23, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing south (front facing player)
            new MobileAnimation() {Record = 23, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing south (front facing player)
            new MobileAnimation() {Record = 23, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing south (front facing player)
        };
        public static MobileAnimation[] SeducerTransform2Anims = new MobileAnimation[]
        {
            new MobileAnimation() {Record = 22, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing south (front facing player)
            new MobileAnimation() {Record = 22, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing south (front facing player)
            new MobileAnimation() {Record = 22, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing south (front facing player)
            new MobileAnimation() {Record = 22, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing south (front facing player)
            new MobileAnimation() {Record = 22, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing south (front facing player)
            new MobileAnimation() {Record = 22, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing south (front facing player)
            new MobileAnimation() {Record = 22, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing south (front facing player)
            new MobileAnimation() {Record = 22, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing south (front facing player)
        };
        public static MobileAnimation[] SeducerIdleMoveAnims = new MobileAnimation[]
        {
            new MobileAnimation() {Record = 21, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing south (front facing player)
            new MobileAnimation() {Record = 21, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing south (front facing player)
            new MobileAnimation() {Record = 21, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing south (front facing player)
            new MobileAnimation() {Record = 21, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing south (front facing player)
            new MobileAnimation() {Record = 21, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing south (front facing player)
            new MobileAnimation() {Record = 21, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing south (front facing player)
            new MobileAnimation() {Record = 21, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing south (front facing player)
            new MobileAnimation() {Record = 21, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing south (front facing player)
        };
        public static MobileAnimation[] SeducerAttackAnims = new MobileAnimation[]
        {
            new MobileAnimation() {Record = 20, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing south (front facing player)
            new MobileAnimation() {Record = 20, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing south (front facing player)
            new MobileAnimation() {Record = 20, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing south (front facing player)
            new MobileAnimation() {Record = 20, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing south (front facing player)
            new MobileAnimation() {Record = 20, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing south (front facing player)
            new MobileAnimation() {Record = 20, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing south (front facing player)
            new MobileAnimation() {Record = 20, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing south (front facing player)
            new MobileAnimation() {Record = 20, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false},             // Facing south (front facing player)
        };

        // Slaughterfish special idle/move animation - needs to bounce back and forth between frame 0-N rather than loop
        // Move animations (double as idle animations for swimming and flying enemies, and enemies without idle animations)
        public static MobileAnimation[] SlaughterfishMoveAnims = new MobileAnimation[]
        {
            new MobileAnimation() {Record = 0, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false, BounceAnim = true},   // Facing south (front facing player)
            new MobileAnimation() {Record = 1, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false, BounceAnim = true},   // Facing south-west
            new MobileAnimation() {Record = 2, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false, BounceAnim = true},   // Facing west
            new MobileAnimation() {Record = 3, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false, BounceAnim = true},   // Facing north-west
            new MobileAnimation() {Record = 4, FramePerSecond = MoveAnimSpeed, FlipLeftRight = false, BounceAnim = true},   // Facing north (back facing player)
            new MobileAnimation() {Record = 3, FramePerSecond = MoveAnimSpeed, FlipLeftRight = true, BounceAnim = true},    // Facing north-east
            new MobileAnimation() {Record = 2, FramePerSecond = MoveAnimSpeed, FlipLeftRight = true, BounceAnim = true},    // Facing east
            new MobileAnimation() {Record = 1, FramePerSecond = MoveAnimSpeed, FlipLeftRight = true, BounceAnim = true},    // Facing south-east
        };

        #endregion

        #region Enemy Definitions

        // Defines additional data for known enemy types
        // Fills in the blanks where source of data in game files is unknown
        // Suspect at least some of this data is also hard-coded in Daggerfall
        public static MobileEnemy[] Enemies = new MobileEnemy[]
        {
            // Rat
            new MobileEnemy()
            {
                ID = 0,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Animal,
                MaleTexture = 255,
                FemaleTexture = 255,
                CorpseTexture = CorpseTexture(401, 1),
                HasIdle = true,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = false,
                CanBashDownDoors = false,
                CanDrinkPotions = false,
                MoveSound = (int)SoundClips.EnemyRatMove,
                BarkSound = (int)SoundClips.EnemyRatBark,
                AttackSound = (int)SoundClips.EnemyRatAttack,
                MinMetalToHit = WeaponMaterialTypes.None,
                MinDamage = 1,
                MaxDamage = 3,
                MinHealth = 15,
                MaxHealth = 35,
                Level = 1,
                ArmorValue = 0,
                ParrySounds = false,
                MapChance = 0,
                Weight = 2,
                PrimaryAttackAnimFrames = new int[] { 0, 1, 2, -1, 3, 4, 5 },
                Team = MobileTeams.Vermin,
            },

            // Imp
            new MobileEnemy()
            {
                ID = 1,
                Behaviour = MobileBehaviour.Flying,
                Affinity = MobileAffinity.Darkness,
                MaleTexture = 256,
                FemaleTexture = 256,
                CorpseTexture = CorpseTexture(406, 5),
                HasIdle = false,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = false,
                CanDrinkPotions = false,
                MoveSound = (int)SoundClips.EnemyImpMove,
                BarkSound = (int)SoundClips.EnemyImpBark,
                AttackSound = (int)SoundClips.EnemyImpAttack,
                MinMetalToHit = WeaponMaterialTypes.Steel,
                MinDamage = 2,
                MaxDamage = 13,
                MinHealth = 10,
                MaxHealth = 20,
                Level = 2,
                ArmorValue = 0,
                ParrySounds = false,
                MapChance = 1,
                Weight = 40,
                SeesThroughInvisibility = true,
                LootTableKey = "D",
                SoulPts = 1000,
                PrimaryAttackAnimFrames = new int[] { 0, 1, 2, -1, 3, 1 },
                SpellAnimFrames = new int[] { 0, 1, 2, 3, 1 },
                Team = MobileTeams.Magic,
            },

            // Spriggan
            new MobileEnemy()
            {
                ID = 2,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Daylight,
                MaleTexture = 257,
                FemaleTexture = 257,
                CorpseTexture = CorpseTexture(406, 3),
                HasIdle = true,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = false,
                BloodIndex = 2,
                MoveSound = (int)SoundClips.EnemySprigganMove,
                BarkSound = (int)SoundClips.EnemySprigganBark,
                AttackSound = (int)SoundClips.EnemySprigganAttack,
                MinMetalToHit = WeaponMaterialTypes.None,
                MinDamage = 5,
                MaxDamage = 11,
                MinHealth = 30,
                MaxHealth = 55,
                Level = 3,
                ArmorValue = 0,
                ParrySounds = false,
                MapChance = 0,
                Weight = 240,
                LootTableKey = "B",
                SoulPts = 1000,
                PrimaryAttackAnimFrames = new int[] { 0, 1, -1, 2, 3, 3, 3 },
                Team = MobileTeams.Spriggans,
            },

            // Giant Bat
            new MobileEnemy()
            {
                ID = 3,
                Behaviour = MobileBehaviour.Flying,
                Affinity = MobileAffinity.Animal,
                MaleTexture = 258,
                FemaleTexture = 258,
                CorpseTexture = CorpseTexture(401, 0),
                HasIdle = false,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = false,
                CanBashDownDoors = false,
                CanDrinkPotions = false,
                MoveSound = (int)SoundClips.EnemyGiantBatMove,
                BarkSound = (int)SoundClips.EnemyGiantBatBark,
                AttackSound = (int)SoundClips.EnemyGiantBatAttack,
                MinMetalToHit = WeaponMaterialTypes.None,
                MinDamage = 1,
                MaxDamage = 4,
                MinHealth = 5,
                MaxHealth = 13,
                Level = 2,
                ArmorValue = 0,
                ParrySounds = false,
                MapChance = 0,
                Weight = 80,
                PrimaryAttackAnimFrames = new int[] { 0, 1, -1, 2, 3 },
                Team = MobileTeams.Vermin,
            },

            // Grizzly Bear
            new MobileEnemy()
            {
                ID = 4,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Animal,
                MaleTexture = 259,
                FemaleTexture = 259,
                CorpseTexture = CorpseTexture(401, 2),
                HasIdle = true,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = false,
                CanBashDownDoors = true,
                CanDrinkPotions = false,
                MoveSound = (int)SoundClips.EnemyBearMove,
                BarkSound = (int)SoundClips.EnemyBearBark,
                AttackSound = (int)SoundClips.EnemyBearAttack,
                MinMetalToHit = WeaponMaterialTypes.None,
                MinDamage = 6,
                MaxDamage = 14,
                MinHealth = 55,
                MaxHealth = 110,
                Level = 4,
                ArmorValue = 0,
                ParrySounds = false,
                MapChance = 0,
                Weight = 1000,
                PrimaryAttackAnimFrames = new int[] { 0, 1, 2, -1, 3, 0 },
                Team = MobileTeams.Bears,
            },

            // Sabertooth Tiger
            new MobileEnemy()
            {
                ID = 5,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Animal,
                MaleTexture = 260,
                FemaleTexture = 260,
                CorpseTexture = CorpseTexture(401, 3),
                HasIdle = true,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = false,
                CanBashDownDoors = true,
                CanDrinkPotions = false,
                MoveSound = (int)SoundClips.EnemyTigerMove,
                BarkSound = (int)SoundClips.EnemyTigerBark,
                AttackSound = (int)SoundClips.EnemyTigerAttack,
                MinMetalToHit = WeaponMaterialTypes.None,
                MinDamage = 8,
                MaxDamage = 18,
                MinHealth = 35,
                MaxHealth = 60,
                Level = 4,
                ArmorValue = 0,
                ParrySounds = false,
                MapChance = 0,
                Weight = 1000,
                PrimaryAttackAnimFrames = new int[] { 0, 1, 2, -1, 3, 4, 5 },
                Team = MobileTeams.Tigers,
            },

            // Spider
            new MobileEnemy()
            {
                ID = 6,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Animal,
                MaleTexture = 261,
                FemaleTexture = 261,
                CorpseTexture = CorpseTexture(401, 4),
                HasIdle = true,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = false,
                CanBashDownDoors = false,
                CanDrinkPotions = false,
                MoveSound = (int)SoundClips.EnemySpiderMove,
                BarkSound = (int)SoundClips.EnemySpiderBark,
                AttackSound = (int)SoundClips.EnemySpiderAttack,
                MinMetalToHit = WeaponMaterialTypes.None,
                MinDamage = 3,
                MaxDamage = 9,
                MinHealth = 12,
                MaxHealth = 28,
                Level = 2,
                ArmorValue = 0,
                ParrySounds = false,
                MapChance = 0,
                Weight = 400,
                PrimaryAttackAnimFrames = new int[] { 0, 1, -1, 2, 3, 4, 5 },
                Team = MobileTeams.Spiders,
            },

            // Orc
            new MobileEnemy()
            {
                ID = 7,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Darkness,
                MaleTexture = 262,
                FemaleTexture = 262,
                CorpseTexture = CorpseTexture(96, 2),
                HasIdle = true,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = true,
                MoveSound = (int)SoundClips.EnemyOrcMove,
                BarkSound = (int)SoundClips.EnemyOrcBark,
                AttackSound = (int)SoundClips.EnemyOrcAttack,
                MinMetalToHit = WeaponMaterialTypes.None,
                MinDamage = 6,
                MaxDamage = 13,
                MinHealth = 40,
                MaxHealth = 70,
                Level = 6,
                ArmorValue = 0,
                ParrySounds = true,
                MapChance = 0,
                Weight = 600,
                LootTableKey = "A",
                SoulPts = 1000,
                PrimaryAttackAnimFrames = new int[] { 0, 1, 2, -1, 3, 4, -1, 5, 0 },
                ChanceForAttack2 = 50,
                PrimaryAttackAnimFrames2 = new int[] { 4, -1, 5, 0 },
                Team = MobileTeams.Orcs,
            },

            // Centaur
            new MobileEnemy()
            {
                ID = 8,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Daylight,
                MaleTexture = 263,
                FemaleTexture = 263,
                CorpseTexture = CorpseTexture(406, 0),
                HasIdle = true,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = true,
                MoveSound = (int)SoundClips.EnemyCentaurMove,
                BarkSound = (int)SoundClips.EnemyCentaurBark,
                AttackSound = (int)SoundClips.EnemyCentaurAttack,
                MinMetalToHit = WeaponMaterialTypes.None,
                MinDamage = 7,
                MaxDamage = 16,
                MinHealth = 35,
                MaxHealth = 65,
                Level = 5,
                ArmorValue = 0,
                ParrySounds = true,
                MapChance = 1,
                Weight = 1200,
                LootTableKey = "C",
                SoulPts = 3000,
                PrimaryAttackAnimFrames = new int[] { 0, 1, 1, 1, 2, -1, 3, 3, 4 },
                ChanceForAttack2 = 50,
                PrimaryAttackAnimFrames2 = new int[] { 0, 1, 1, 1, 2, -1, 3, 3, 2, 1, 1, -1, 2, 3, 3, 4 },
                Team = MobileTeams.Centaurs,
            },

            // Werewolf
            new MobileEnemy()
            {
                ID = 9,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Darkness,
                MaleTexture = 264,
                FemaleTexture = 264,
                CorpseTexture = CorpseTexture(96, 5),
                HasIdle = false,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = false,
                MoveSound = (int)SoundClips.EnemyWerewolfMove,
                BarkSound = (int)SoundClips.EnemyWerewolfBark,
                AttackSound = (int)SoundClips.EnemyWerewolfAttack,
                MinMetalToHit = WeaponMaterialTypes.Silver,
                MinDamage = 8,
                MaxDamage = 19,
                MinHealth = 30,
                MaxHealth = 55,
                Level = 8,
                ArmorValue = 0,
                MapChance = 0,
                ParrySounds = false,
                Weight = 480,
                SoulPts = 1000,
                PrimaryAttackAnimFrames = new int[] { 0, 1, -1, 2, -1, 2 },
                Team = MobileTeams.Werecreatures,
            },

            // Nymph
            new MobileEnemy()
            {
                ID = 10,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Daylight,
                MaleTexture = 265,
                FemaleTexture = 265,
                CorpseTexture = CorpseTexture(406, 2),
                HasIdle = true,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = false,
                MoveSound = (int)SoundClips.EnemyNymphMove,
                BarkSound = (int)SoundClips.EnemyNymphBark,
                AttackSound = (int)SoundClips.EnemyNymphAttack,
                MinMetalToHit = WeaponMaterialTypes.Silver,
                MinDamage = 2,
                MaxDamage = 9,
                MinHealth = 25,
                MaxHealth = 45,
                Level = 6,
                ArmorValue = 0,
                ParrySounds = false,
                MapChance = 1,
                Weight = 200,
                LootTableKey = "C",
                SoulPts = 10000,
                PrimaryAttackAnimFrames = new int[] { 0, 1, 2, 3, 4, -1, 5 },
                Team = MobileTeams.Nymphs,
            },

            // Slaughterfish
            new MobileEnemy()
            {
                ID = 11,
                Behaviour = MobileBehaviour.Aquatic,
                Affinity = MobileAffinity.Water,
                MaleTexture = 266,
                FemaleTexture = 266,
                CorpseTexture = CorpseTexture(305, 1),
                HasIdle = false,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = false,
                CanBashDownDoors = false,
                CanDrinkPotions = false,
                MoveSound = (int)SoundClips.EnemyEelMove,
                BarkSound = (int)SoundClips.EnemyEelBark,
                AttackSound = (int)SoundClips.EnemyEelAttack,
                MinMetalToHit = WeaponMaterialTypes.None,
                MinDamage = 4,
                MaxDamage = 12,
                MinHealth = 25,
                MaxHealth = 50,
                Level = 7,
                ArmorValue = 0,
                ParrySounds = false,
                MapChance = 0,
                Weight = 400,
                PrimaryAttackAnimFrames = new int[] { 0, -1, 1, 2, 3, 4, -1, 5, 0 },
                ChanceForAttack2 = 33,
                PrimaryAttackAnimFrames2 = new int[] { 0, 3, -1, 5, 4, 3, 3, -1, 5, 4, 3, -1, 5, 0 },
                ChanceForAttack3 = 33,
                PrimaryAttackAnimFrames3 = new int[] { 0, 3, -1, 5, 0 },
                Team = MobileTeams.Aquatic,
            },

            // Orc Sergeant
            new MobileEnemy()
            {
                ID = 12,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Darkness,
                MaleTexture = 267,
                FemaleTexture = 267,
                CorpseTexture = CorpseTexture(96, 2),
                HasIdle = true,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = true,
                MoveSound = (int)SoundClips.EnemyOrcSergeantMove,
                BarkSound = (int)SoundClips.EnemyOrcSergeantBark,
                AttackSound = (int)SoundClips.EnemyOrcSergeantAttack,
                MinMetalToHit = WeaponMaterialTypes.None,
                MinDamage = 8,
                MaxDamage = 18,
                MinHealth = 50,
                MaxHealth = 85,
                Level = 9,
                ArmorValue = 0,
                ParrySounds = true,
                MapChance = 1,
                Weight = 600,
                LootTableKey = "A",
                SoulPts = 1000,
                PrimaryAttackAnimFrames = new int[] { 0, -1, 1, 2, 3, 4, -1, 5, 0 },
                ChanceForAttack2 = 50,
                PrimaryAttackAnimFrames2 = new int[] { 5, 4, 3, -1, 2, 1, 0 },
                Team = MobileTeams.Orcs,
            },

            // Harpy
            new MobileEnemy()
            {
                ID = 13,
                Behaviour = MobileBehaviour.Flying,
                Affinity = MobileAffinity.Daylight,
                MaleTexture = 268,
                FemaleTexture = 268,
                CorpseTexture = CorpseTexture(406, 4),
                HasIdle = false,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = false,
                CanBashDownDoors = true,
                CanDrinkPotions = false,
                MoveSound = (int)SoundClips.EnemyHarpyMove,
                BarkSound = (int)SoundClips.EnemyHarpyBark,
                AttackSound = (int)SoundClips.EnemyHarpyAttack,
                MinMetalToHit = WeaponMaterialTypes.Dwarven,
                MinDamage = 7,
                MaxDamage = 15,
                MinHealth = 25,
                MaxHealth = 60,
                Level = 8,
                ArmorValue = 0,
                ParrySounds = false,
                MapChance = 0,
                Weight = 200,
                LootTableKey = "D",
                SoulPts = 3000,
                PrimaryAttackAnimFrames = new int[] { 0, 1, 2, -1, 3 },
                Team = MobileTeams.Harpies,
            },

            // Wereboar
            new MobileEnemy()
            {
                ID = 14,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Darkness,
                MaleTexture = 269,
                FemaleTexture = 269,
                CorpseTexture = CorpseTexture(96, 5),
                HasIdle = false,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = false,
                MoveSound = (int)SoundClips.EnemyWereboarMove,
                BarkSound = (int)SoundClips.EnemyWereboarBark,
                AttackSound = (int)SoundClips.EnemyWereboarAttack,
                MinMetalToHit = WeaponMaterialTypes.Silver,
                MinDamage = 7,
                MaxDamage = 17,
                MinHealth = 65,
                MaxHealth = 95,
                Level = 8,
                ArmorValue = 0,
                MapChance = 0,
                ParrySounds = false,
                Weight = 560,
                SoulPts = 1000,
                PrimaryAttackAnimFrames = new int[] { 0, -1, 1, 2, 2 },
                Team = MobileTeams.Werecreatures,
            },

            // Skeletal Warrior
            new MobileEnemy()
            {
                ID = 15,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Undead,
                MaleTexture = 270,
                FemaleTexture = 270,
                CorpseTexture = CorpseTexture(306, 1),
                HasIdle = true,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = false,
                BloodIndex = 2,
                MoveSound = (int)SoundClips.EnemySkeletonMove,
                BarkSound = (int)SoundClips.EnemySkeletonBark,
                AttackSound = (int)SoundClips.EnemySkeletonAttack,
                MinMetalToHit = WeaponMaterialTypes.None,
                MinDamage = 7,
                MaxDamage = 14,
                MinHealth = 20,
                MaxHealth = 40,
                Level = 11,
                ArmorValue = 0,
                ParrySounds = true,
                MapChance = 1,
                Weight = 80,
                SeesThroughInvisibility = true,
                LootTableKey = "H",
                PrimaryAttackAnimFrames = new int[] { 0, 1, 2, 3, -1, 4, 5 },
                Team = MobileTeams.Undead,
            },

            // Giant
            new MobileEnemy()
            {
                ID = 16,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Daylight,
                MaleTexture = 271,
                FemaleTexture = 271,
                CorpseTexture = CorpseTexture(406, 1),
                HasIdle = true,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = true,
                MoveSound = (int)SoundClips.EnemyGiantMove,
                BarkSound = (int)SoundClips.EnemyGiantBark,
                AttackSound = (int)SoundClips.EnemyGiantAttack,
                MinMetalToHit = WeaponMaterialTypes.None,
                MinDamage = 8,
                MaxDamage = 18,
                MinHealth = 65,
                MaxHealth = 100,
                Level = 10,
                ArmorValue = 0,
                ParrySounds = false,
                MapChance = 1,
                LootTableKey = "F",
                Weight = 3000,
                SoulPts = 3000,
                PrimaryAttackAnimFrames = new int[] { 0, 1, -1, 2, 3, 4, -1, 5 },
                Team = MobileTeams.Giants,
            },

            // Zombie
            new MobileEnemy()
            {
                ID = 17,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Undead,
                MaleTexture = 272,
                FemaleTexture = 272,
                CorpseTexture = CorpseTexture(306, 4),
                HasIdle = true,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = false,
                CanBashDownDoors = true,
                CanDrinkPotions = false,
                MoveSound = (int)SoundClips.EnemyZombieMove,
                BarkSound = (int)SoundClips.EnemyZombieBark,
                AttackSound = (int)SoundClips.EnemyZombieAttack,
                MinMetalToHit = WeaponMaterialTypes.None,
                MinDamage = 4,
                MaxDamage = 8,
                MinHealth = 65,
                MaxHealth = 125,
                Level = 5,
                ArmorValue = 0,
                ParrySounds = false,
                MapChance = 1,
                Weight = 4000,
                LootTableKey = "G",
                PrimaryAttackAnimFrames = new int[] { 0, 1, 2, -1, 3, 4 },
                ChanceForAttack2 = 50,
                PrimaryAttackAnimFrames2 = new int[] { 0, 2, -1, 3, 4 },
                Team = MobileTeams.Undead,
            },

            // Ghost
            new MobileEnemy()
            {
                ID = 18,
                Behaviour = MobileBehaviour.Spectral,
                Affinity = MobileAffinity.Undead,
                MaleTexture = 273,
                FemaleTexture = 273,
                CorpseTexture = CorpseTexture(306, 0),
                HasIdle = false,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = false,
                BloodIndex = 2,
                MoveSound = (int)SoundClips.EnemyGhostMove,
                BarkSound = (int)SoundClips.EnemyGhostBark,
                AttackSound = (int)SoundClips.EnemyGhostAttack,
                MinMetalToHit = WeaponMaterialTypes.Silver,
                MinDamage = 7,
                MaxDamage = 14,
                MinHealth = 20,
                MaxHealth = 40,
                Level = 11,
                ArmorValue = 0,
                ParrySounds = false,
                MapChance = 1,
                Weight = 0,
                SeesThroughInvisibility = true,
                LootTableKey = "I",
                NoShadow = true,
                SoulPts = 30000,
                PrimaryAttackAnimFrames = new int[] { 0, 1, -1, 2, 3 },
                SpellAnimFrames = new int[] { 0, 0, 0, 0, 0, 0 },
                Team = MobileTeams.Undead,
            },

            // Mummy
            new MobileEnemy()
            {
                ID = 19,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Undead,
                MaleTexture = 274,
                FemaleTexture = 274,
                CorpseTexture = CorpseTexture(306, 5),
                HasIdle = true,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = false,
                CanBashDownDoors = true,
                CanDrinkPotions = false,
                BloodIndex = 2,
                MoveSound = (int)SoundClips.EnemyMummyMove,
                BarkSound = (int)SoundClips.EnemyMummyBark,
                AttackSound = (int)SoundClips.EnemyMummyAttack,
                MinMetalToHit = WeaponMaterialTypes.Silver,
                MinDamage = 6,
                MaxDamage = 14,
                MinHealth = 75,
                MaxHealth = 110,
                Level = 15,
                ArmorValue = 0,
                ParrySounds = false,
                MapChance = 1,
                Weight = 300,
                SeesThroughInvisibility = true,
                LootTableKey = "E",
                SoulPts = 10000,
                PrimaryAttackAnimFrames = new int[] { 0, 1, 2, -1, 3, 4 },
                Team = MobileTeams.Undead,
            },

            // Giant Scorpion
            new MobileEnemy()
            {
                ID = 20,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Animal,
                MaleTexture = 275,
                FemaleTexture = 275,
                CorpseTexture = CorpseTexture(401, 5),
                HasIdle = false,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = false,
                CanBashDownDoors = false,
                CanDrinkPotions = false,
                MoveSound = (int)SoundClips.EnemyScorpionMove,
                BarkSound = (int)SoundClips.EnemyScorpionBark,
                AttackSound = (int)SoundClips.EnemyScorpionAttack,
                MinMetalToHit = WeaponMaterialTypes.None,
                MinDamage = 7,
                MaxDamage = 16,
                MinHealth = 22,
                MaxHealth = 40,
                Level = 4,
                ParrySounds = false,
                ArmorValue = 0,
                MapChance = 0,
                Weight = 600,
                PrimaryAttackAnimFrames = new int[] { 0, 1, -1, 3, 2, 1, 0 },
                Team = MobileTeams.Scorpions,
            },

            // Orc Shaman
            new MobileEnemy()
            {
                ID = 21,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Darkness,
                MaleTexture = 276,
                FemaleTexture = 276,
                CorpseTexture = CorpseTexture(96, 2),
                HasIdle = true,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = true,
                MoveSound = (int)SoundClips.EnemyOrcShamanMove,
                BarkSound = (int)SoundClips.EnemyOrcShamanBark,
                AttackSound = (int)SoundClips.EnemyOrcShamanAttack,
                MinMetalToHit = WeaponMaterialTypes.None,
                MinDamage = 7,
                MaxDamage = 15,
                MinHealth = 45,
                MaxHealth = 70,
                Level = 15,
                ArmorValue = 0,
                ParrySounds = true,
                MapChance = 3,
                Weight = 400,
                LootTableKey = "U",
                SoulPts = 3000,
                PrimaryAttackAnimFrames = new int[] { 0, 1, -1, 3, 2, 1, 0 },
                ChanceForAttack2 = 20,
                PrimaryAttackAnimFrames2 = new int[] { 0, -1, 4, 5, 0 },
                ChanceForAttack3 = 20,
                PrimaryAttackAnimFrames3 = new int[] { 0, 1, -1, 3, 2, 1, 0, -1, 4, 5, 0 },
                ChanceForAttack4 = 20,
                PrimaryAttackAnimFrames4 = new int[] { 0, 1, -1, 3, 2, -1, 3, 2, 1, 0 }, // Not used in classic. Fight stance used instead.
                ChanceForAttack5 = 20,
                PrimaryAttackAnimFrames5 = new int[] { 0, -1, 4, 5, -1, 4, 5, 0 }, // Not used in classic. Spell animation played instead.
                HasSpellAnimation = true,
                SpellAnimFrames = new int[] { 0, 0, 1, 2, 3, 3, 3 },
                Team = MobileTeams.Orcs,
            },

            // Gargoyle
            new MobileEnemy()
            {
                ID = 22,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Darkness,
                MaleTexture = 277,
                FemaleTexture = 277,
                CorpseTexture = CorpseTexture(96, 1),
                HasIdle = true,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = false,
                BloodIndex = 2,
                MoveSound = (int)SoundClips.EnemyGargoyleMove,
                BarkSound = (int)SoundClips.EnemyGargoyleBark,
                AttackSound = (int)SoundClips.EnemyGargoyleAttack,
                MinMetalToHit = WeaponMaterialTypes.Mithril,
                MinDamage = 16,
                MaxDamage = 32,
                MinHealth = 50,
                MaxHealth = 100,
                Level = 14,
                ArmorValue = 0,
                MapChance = 0,
                ParrySounds = false,
                Weight = 300,
                SoulPts = 3000,
                PrimaryAttackAnimFrames = new int[] { 0, 2, 1, 2, 3, -1, 4, 0 },
                Team = MobileTeams.Magic,
            },

            // Wraith
            new MobileEnemy()
            {
                ID = 23,
                Behaviour = MobileBehaviour.Spectral,
                Affinity = MobileAffinity.Undead,
                MaleTexture = 278,
                FemaleTexture = 278,
                CorpseTexture = CorpseTexture(306, 0),
                HasIdle = false,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = false,
                BloodIndex = 2,
                MoveSound = (int)SoundClips.EnemyWraithMove,
                BarkSound = (int)SoundClips.EnemyWraithBark,
                AttackSound = (int)SoundClips.EnemyWraithAttack,
                MinMetalToHit = WeaponMaterialTypes.Silver,
                MinDamage = 16,
                MaxDamage = 32,
                MinHealth = 30,
                MaxHealth = 50,
                Level = 15,
                ArmorValue = 0,
                ParrySounds = false,
                MapChance = 1,
                Weight = 0,
                SeesThroughInvisibility = true,
                LootTableKey = "I",
                NoShadow = true,
                SoulPts = 30000,
                PrimaryAttackAnimFrames = new int[] { 0, 1, 2, -1, 3 },
                SpellAnimFrames = new int[] { 0, 0, 0, 0, 0 },
                Team = MobileTeams.Undead,
            },

            // Orc Warlord
            new MobileEnemy()
            {
                ID = 24,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Darkness,
                MaleTexture = 279,
                FemaleTexture = 279,
                CorpseTexture = CorpseTexture(96, 2),
                HasIdle = true,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = true,
                MoveSound = (int)SoundClips.EnemyOrcWarlordMove,
                BarkSound = (int)SoundClips.EnemyOrcWarlordBark,
                AttackSound = (int)SoundClips.EnemyOrcWarlordAttack,
                MinMetalToHit = WeaponMaterialTypes.None,
                MinDamage = 20,
                MaxDamage = 36,
                MinHealth = 80,
                MaxHealth = 125,
                Level = 19,
                ArmorValue = 0,
                ParrySounds = true,
                MapChance = 2,
                Weight = 700,
                LootTableKey = "T",
                SoulPts = 1000,
                PrimaryAttackAnimFrames = new int[] { 0, 1, -1, 2, 3, 4, -1, 5 },
                ChanceForAttack2 = 33,
                PrimaryAttackAnimFrames2 = new int[] { 4, -1, 5, 0 },
                ChanceForAttack3 = 33,
                PrimaryAttackAnimFrames3 = new int[] { 0, 1, -1, 2, 3, 4 -1, 5, 0, 4, -1, 5, 0 },
                Team = MobileTeams.Orcs,
            },

            // Frost Daedra
            new MobileEnemy()
            {
                ID = 25,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Daedra,
                MaleTexture = 280,
                FemaleTexture = 280,
                CorpseTexture = CorpseTexture(400, 3),
                HasIdle = true,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = false,
                BloodIndex = 2,
                MoveSound = (int)SoundClips.EnemyFrostDaedraMove,
                BarkSound = (int)SoundClips.EnemyFrostDaedraBark,
                AttackSound = (int)SoundClips.EnemyFrostDaedraAttack,
                MinMetalToHit = WeaponMaterialTypes.Mithril,
                MinDamage = 25,
                MaxDamage = 40,
                MinHealth = 90,
                MaxHealth = 160,
                Level = 17,
                ArmorValue = 0,
                ParrySounds = true,
                MapChance = 0,
                Weight = 800,
                SeesThroughInvisibility = true,
                LootTableKey = "J",
                NoShadow = true,
                GlowColor = new Color(18, 68, 88) * 0.1f,
                SoulPts = 50000,
                PrimaryAttackAnimFrames = new int[] { 0, 1, -1, 2, 3, -1, 4, 5, 0 },
                ChanceForAttack2 = 50,
                PrimaryAttackAnimFrames2 = new int[] { -1, 4, 5, 0 },
                SpellAnimFrames = new int[] { 1, 1, 3, 3 },
                Team = MobileTeams.Daedra,
            },

            // Fire Daedra
            new MobileEnemy()
            {
                ID = 26,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Daedra,
                MaleTexture = 281,
                FemaleTexture = 281,
                CorpseTexture = CorpseTexture(400, 2),
                HasIdle = true,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = false,
                BloodIndex = 2,
                MoveSound = (int)SoundClips.EnemyFireDaedraMove,
                BarkSound = (int)SoundClips.EnemyFireDaedraBark,
                AttackSound = (int)SoundClips.EnemyFireDaedraAttack,
                MinMetalToHit = WeaponMaterialTypes.Mithril,
                MinDamage = 35,
                MaxDamage = 55,
                MinHealth = 60,
                MaxHealth = 100,
                Level = 17,
                ArmorValue = 0,
                ParrySounds = true,
                MapChance = 0,
                Weight = 800,
                SeesThroughInvisibility = true,
                LootTableKey = "J",
                NoShadow = true,
                GlowColor = new Color(243, 239, 44) * 0.05f,
                SoulPts = 50000,
                PrimaryAttackAnimFrames = new int[] { 0, 1, -1, 2, 3, -1, 4 },
                ChanceForAttack2 = 50,
                PrimaryAttackAnimFrames2 = new int[] { 3, -1, 4 },
                SpellAnimFrames = new int[] { 1, 1, 3, 3 },
                Team = MobileTeams.Daedra,
            },

            // Daedroth
            new MobileEnemy()
            {
                ID = 27,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Daedra,
                MaleTexture = 282,
                FemaleTexture = 282,
                CorpseTexture = CorpseTexture(400, 1),
                HasIdle = true,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = true,
                MoveSound = (int)SoundClips.EnemyLesserDaedraMove,
                BarkSound = (int)SoundClips.EnemyLesserDaedraBark,
                AttackSound = (int)SoundClips.EnemyLesserDaedraAttack,
                MinMetalToHit = WeaponMaterialTypes.Mithril,
                MinDamage = 20,
                MaxDamage = 32,
                MinHealth = 70,
                MaxHealth = 120,
                Level = 18,
                ArmorValue = 0,
                ParrySounds = true,
                MapChance = 0,
                Weight = 400,
                SeesThroughInvisibility = true,
                LootTableKey = "E",
                SoulPts = 10000,
                PrimaryAttackAnimFrames = new int[] { 0, 1, -1, 2, 3, 4, -1, 5 },
                ChanceForAttack2 = 33,
                PrimaryAttackAnimFrames2 = new int[] { 4, -1, 5, 0 },
                ChanceForAttack3 = 33,
                PrimaryAttackAnimFrames3 = new int[] { 0, 1, -1, 2, 3, 4, -1, 5, 0, 4, -1, 5, 0 },
                SpellAnimFrames = new int[] { 1, 1, 3, 3 },
                Team = MobileTeams.Daedra,
            },

            // Vampire
            new MobileEnemy()
            {
                ID = 28,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Darkness,
                MaleTexture = 283,
                FemaleTexture = 283,
                CorpseTexture = CorpseTexture(96, 3),
                HasIdle = true,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = true,
                MoveSound = (int)SoundClips.EnemyFemaleVampireMove,
                BarkSound = (int)SoundClips.EnemyFemaleVampireBark,
                AttackSound = (int)SoundClips.EnemyFemaleVampireAttack,
                MinMetalToHit = WeaponMaterialTypes.Silver,
                MinDamage = 15,
                MaxDamage = 32,
                MinHealth = 70,
                MaxHealth = 105,
                Level = 17,
                ArmorValue = 0,
                ParrySounds = false,
                MapChance = 3,
                Weight = 400,
                SeesThroughInvisibility = true,
                LootTableKey = "Q",
                NoShadow = true,
                SoulPts = 70000,
                PrimaryAttackAnimFrames = new int[] { 0, 1, 2, 3, -1, 4, 5 },
                SpellAnimFrames = new int[] { 1, 1, 5, 5 },
                Team = MobileTeams.Undead,
            },

            // Daedra Seducer
            new MobileEnemy()
            {
                ID = 29,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Daedra,
                MaleTexture = 284,
                FemaleTexture = 284,
                CorpseTexture = CorpseTexture(400, 6),          // Has a winged and unwinged corpse, only using unwinged here
                HasIdle = true,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                HasSeducerTransform1 = true,
                HasSeducerTransform2 = true,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = true,
                MoveSound = (int)SoundClips.EnemySeducerMove,
                BarkSound = (int)SoundClips.EnemySeducerBark,
                AttackSound = (int)SoundClips.EnemySeducerAttack,
                MinMetalToHit = WeaponMaterialTypes.Mithril,
                MinDamage = 30,
                MaxDamage = 60,
                MinHealth = 70,
                MaxHealth = 95,
                Level = 19,
                ArmorValue = 0,
                ParrySounds = false,
                MapChance = 1,
                Weight = 200,
                SeesThroughInvisibility = true,
                LootTableKey = "Q",
                SoulPts = 150000,
                PrimaryAttackAnimFrames = new int[] { 0, 1, -1, 2 },
                SpellAnimFrames = new int[] { 0, 1, 2 },
                SeducerTransform1Frames = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 },
                SeducerTransform2Frames = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 },
                Team = MobileTeams.Daedra,
            },

            // Vampire Ancient
            new MobileEnemy()
            {
                ID = 30,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Darkness,
                MaleTexture = 285,
                FemaleTexture = 285,
                CorpseTexture = CorpseTexture(96, 3),
                HasIdle = true,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = true,
                MoveSound = (int)SoundClips.EnemyVampireMove,
                BarkSound = (int)SoundClips.EnemyVampireBark,
                AttackSound = (int)SoundClips.EnemyVampireAttack,
                MinMetalToHit = WeaponMaterialTypes.Mithril,
                MinDamage = 18,
                MaxDamage = 35,
                MinHealth = 85,
                MaxHealth = 140,
                Level = 20,
                ArmorValue = 0,
                ParrySounds = false,
                MapChance = 3,
                Weight = 400,
                SeesThroughInvisibility = true,
                LootTableKey = "Q",
                NoShadow = true,
                SoulPts = 100000,
                PrimaryAttackAnimFrames = new int[] { 0, 1, 2, 3, -1, 4, 5 },
                SpellAnimFrames = new int[] { 1, 1, 5, 5 },
                Team = MobileTeams.Undead,
            },

            // Daedra Lord
            new MobileEnemy()
            {
                ID = 31,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Daedra,
                MaleTexture = 286,
                FemaleTexture = 286,
                CorpseTexture = CorpseTexture(400, 4),
                HasIdle = true,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = true,
                MoveSound = (int)SoundClips.EnemyDaedraLordMove,
                BarkSound = (int)SoundClips.EnemyDaedraLordBark,
                AttackSound = (int)SoundClips.EnemyDaedraLordAttack,
                MinMetalToHit = WeaponMaterialTypes.Mithril,
                MinDamage = 26,
                MaxDamage = 42,
                MinHealth = 170,
                MaxHealth = 285,
                Level = 21,
                ArmorValue = 0,
                ParrySounds = true,
                MapChance = 0,
                Weight = 1000,
                SeesThroughInvisibility = true,
                LootTableKey = "S",
                SoulPts = 800000,
                PrimaryAttackAnimFrames = new int[] { 0, 1, -1, 2, 3, -1, 4 },
                ChanceForAttack2 = 33,
                PrimaryAttackAnimFrames2 = new int[] { 3, -1, 4, 0, -1, 4, 3, -1, 4, 0, -1, 4, 3 },
                ChanceForAttack3 = 33,
                PrimaryAttackAnimFrames3 = new int[] { 0, 1, -1, 2, 1, 0, 1, -1, 2, 1, 0 },
                SpellAnimFrames = new int[] { 1, 1, 3, 3 },
                Team = MobileTeams.Daedra,
            },

            // Lich
            new MobileEnemy()
            {
                ID = 32,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Undead,
                MaleTexture = 287,
                FemaleTexture = 287,
                CorpseTexture = CorpseTexture(306, 2),
                HasIdle = true,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = false,
                BloodIndex = 2,
                MoveSound = (int)SoundClips.EnemyLichMove,
                BarkSound = (int)SoundClips.EnemyLichBark,
                AttackSound = (int)SoundClips.EnemyLichAttack,
                MinMetalToHit = WeaponMaterialTypes.Mithril,
                MinDamage = 25,
                MaxDamage = 45,
                MinHealth = 85,
                MaxHealth = 135,
                Level = 20,
                ArmorValue = 0,
                ParrySounds = false,
                MapChance = 4,
                Weight = 300,
                SeesThroughInvisibility = true,
                LootTableKey = "S",
                SoulPts = 100000,
                PrimaryAttackAnimFrames = new int[] { 0, 1, 1, 2, -1, 3, 4, 4 },
                SpellAnimFrames = new int[] { 0, 1, 2, 3, 4 },
                Team = MobileTeams.Undead,
            },

            // Ancient Lich
            new MobileEnemy()
            {
                ID = 33,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Undead,
                MaleTexture = 288,
                FemaleTexture = 288,
                CorpseTexture = CorpseTexture(306, 3),
                HasIdle = true,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = false,
                BloodIndex = 2,
                MoveSound = (int)SoundClips.EnemyLichKingMove,
                BarkSound = (int)SoundClips.EnemyLichKingBark,
                AttackSound = (int)SoundClips.EnemyLichKingAttack,
                MinMetalToHit = WeaponMaterialTypes.Mithril,
                MinDamage = 35,
                MaxDamage = 55,
                MinHealth = 115,
                MaxHealth = 195,
                Level = 21,
                ArmorValue = 0,
                ParrySounds = false,
                MapChance = 4,
                Weight = 300,
                SeesThroughInvisibility = true,
                LootTableKey = "S",
                SoulPts = 250000,
                PrimaryAttackAnimFrames = new int[] { 0, 1, 1, 2, -1, 3, 4, 4 },
                SpellAnimFrames = new int[] { 0, 1, 2, 3, 4 },
                Team = MobileTeams.Undead,
            },

            // Dragonling
            new MobileEnemy()
            {
                ID = 34,
                Behaviour = MobileBehaviour.Flying,
                Affinity = MobileAffinity.Daylight,
                MaleTexture = 289,
                FemaleTexture = 289,
                CorpseTexture = CorpseTexture(96, 0),
                HasIdle = false,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = false,
                CanBashDownDoors = false,
                CanDrinkPotions = false,
                MoveSound = (int)SoundClips.EnemyFaeryDragonMove,
                BarkSound = (int)SoundClips.EnemyFaeryDragonBark,
                AttackSound = (int)SoundClips.EnemyFaeryDragonAttack,
                MinMetalToHit = WeaponMaterialTypes.None,
                MinDamage = 12,
                MaxDamage = 24,
                MinHealth = 35,
                MaxHealth = 60,
                Level = 16,
                ArmorValue = 0,
                ParrySounds = false,
                MapChance = 0,
                Weight = 10000,
                PrimaryAttackAnimFrames = new int[] { 0, 1, 2, -1, 3 },
                Team = MobileTeams.Dragonlings,
            },

            // Fire Atronach
            new MobileEnemy()
            {
                ID = 35,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Golem,
                MaleTexture = 290,
                FemaleTexture = 290,
                CorpseTexture = CorpseTexture(405, 2),
                HasIdle = true,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = false,
                BloodIndex = 2,
                MoveSound = (int)SoundClips.EnemyFireAtronachMove,
                BarkSound = (int)SoundClips.EnemyFireAtronachBark,
                AttackSound = (int)SoundClips.EnemyFireAtronachAttack,
                MinMetalToHit = WeaponMaterialTypes.None,
                MinDamage = 9,
                MaxDamage = 17,
                MinHealth = 40,
                MaxHealth = 60,
                Level = 16,
                ArmorValue = 0,
                ParrySounds = false,
                MapChance = 0,
                NoShadow = true,
                GlowColor = new Color(243, 150, 44) * 0.05f,
                Weight = 1000,
                SoulPts = 30000,
                PrimaryAttackAnimFrames = new int[] { 0, -1, 1, 2, 3, 4 },
                Team = MobileTeams.Magic,
            },

            // Iron Atronach
            new MobileEnemy()
            {
                ID = 36,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Golem,
                MaleTexture = 291,
                FemaleTexture = 291,
                CorpseTexture = CorpseTexture(405, 1),
                HasIdle = true,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = false,
                BloodIndex = 2,
                MoveSound = (int)SoundClips.EnemyIronAtronachMove,
                BarkSound = (int)SoundClips.EnemyIronAtronachBark,
                AttackSound = (int)SoundClips.EnemyIronAtronachAttack,
                MinMetalToHit = WeaponMaterialTypes.None,
                MinDamage = 12,
                MaxDamage = 23,
                MinHealth = 95,
                MaxHealth = 155,
                Level = 21,
                ArmorValue = 0,
                ParrySounds = true,
                MapChance = 0,
                Weight = 1000,
                SoulPts = 30000,
                PrimaryAttackAnimFrames = new int[] { 0, 1, 2, -1, 3, 4 },
                Team = MobileTeams.Magic,
            },

            // Flesh Atronach
            new MobileEnemy()
            {
                ID = 37,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Golem,
                MaleTexture = 292,
                FemaleTexture = 292,
                CorpseTexture = CorpseTexture(405, 0),
                HasIdle = true,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = false,
                MoveSound = (int)SoundClips.EnemyFleshAtronachMove,
                BarkSound = (int)SoundClips.EnemyFleshAtronachBark,
                AttackSound = (int)SoundClips.EnemyFleshAtronachAttack,
                MinMetalToHit = WeaponMaterialTypes.None,
                MinDamage = 3,
                MaxDamage = 8,
                MinHealth = 120,
                MaxHealth = 225,
                Level = 16,
                ArmorValue = 0,
                ParrySounds = false,
                MapChance = 0,
                Weight = 1000,
                SoulPts = 30000,
                PrimaryAttackAnimFrames = new int[] { 0, 1, 2, -1, 3, 4 },
                Team = MobileTeams.Magic,
            },

            // Ice Atronach
            new MobileEnemy()
            {
                ID = 38,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Golem,
                MaleTexture = 293,
                FemaleTexture = 293,
                CorpseTexture = CorpseTexture(405, 3),
                HasIdle = true,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = false,
                BloodIndex = 2,
                MoveSound = (int)SoundClips.EnemyIceAtronachMove,
                BarkSound = (int)SoundClips.EnemyIceAtronachBark,
                AttackSound = (int)SoundClips.EnemyIceAtronachAttack,
                MinMetalToHit = WeaponMaterialTypes.None,
                MinDamage = 5,
                MaxDamage = 13,
                MinHealth = 70,
                MaxHealth = 110,
                Level = 16,
                ArmorValue = 0,
                ParrySounds = true,
                MapChance = 0,
                Weight = 1000,
                SoulPts = 30000,
                PrimaryAttackAnimFrames = new int[] { 0, 1, 2, -1, 3, 4 },
                ChanceForAttack2 = 50,
                PrimaryAttackAnimFrames2 = new int[] { 0, -1, 3, 4 },
                Team = MobileTeams.Magic,
            },

            // Weights in classic (From offset 0x1BD8D9 in FALL.EXE) only have entries
            // up through Horse. Dragonling, Dreugh and Lamia use nonsense values from
            // the adjacent data. For Daggerfall Unity, using values inferred from
            // other enemy types.

            // Horse (unused, but can appear in merchant-sold soul traps)
            new MobileEnemy()
            {
                ID = 39,
            },

            // Dragonling
            new MobileEnemy()
            {
                ID = 40,
                Behaviour = MobileBehaviour.Flying,
                Affinity = MobileAffinity.Daylight,
                MaleTexture = 295,
                FemaleTexture = 295,
                CorpseTexture = CorpseTexture(96, 0),
                HasIdle = false,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = false,
                CanBashDownDoors = true,
                CanDrinkPotions = false,
                MoveSound = (int)SoundClips.EnemyFaeryDragonMove,
                BarkSound = (int)SoundClips.EnemyFaeryDragonBark,
                AttackSound = (int)SoundClips.EnemyFaeryDragonAttack,
                MinMetalToHit = WeaponMaterialTypes.None,
                MinDamage = 35,
                MaxDamage = 95,
                MinHealth = 125,
                MaxHealth = 230,
                Level = 21,
                ArmorValue = 0,
                ParrySounds = false,
                MapChance = 0,
                Weight = 10000, // Using same value as other dragonling
                SoulPts = 500000,
                PrimaryAttackAnimFrames = new int[] { 0, 1, -1, 2, 3 },
                Team = MobileTeams.Dragonlings,
            },

            // Dreugh
            new MobileEnemy()
            {
                ID = 41,
                Behaviour = MobileBehaviour.Aquatic,
                Affinity = MobileAffinity.Water,
                MaleTexture = 296,
                FemaleTexture = 296,
                CorpseTexture = CorpseTexture(305, 0),
                HasIdle = false,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = false,
                CanBashDownDoors = true,
                CanDrinkPotions = false,
                MoveSound = (int)SoundClips.EnemyDreughMove,
                BarkSound = (int)SoundClips.EnemyDreughBark,
                AttackSound = (int)SoundClips.EnemyDreughAttack,
                MinMetalToHit = WeaponMaterialTypes.None,
                MinDamage = 6,
                MaxDamage = 16,
                MinHealth = 45,
                MaxHealth = 90,
                Level = 16,
                ArmorValue = 0,
                ParrySounds = false,
                MapChance = 0,
                Weight = 600, // Using same value as orc
                LootTableKey = "R",
                SoulPts = 10000,
                PrimaryAttackAnimFrames = new int[] { 0, 1, 2, 3, -1, 4, 5, -1, 6, 7 },
                ChanceForAttack2 = 33,
                PrimaryAttackAnimFrames2 = new int[] { 0, 1, 2, 3, -1, 4 },
                ChanceForAttack3 = 33,
                PrimaryAttackAnimFrames3 = new int[] { 0, 5, -1, 6, 7 },
                Team = MobileTeams.Aquatic,
            },

            // Lamia
            new MobileEnemy()
            {
                ID = 42,
                Behaviour = MobileBehaviour.Aquatic,
                Affinity = MobileAffinity.Water,
                MaleTexture = 297,
                FemaleTexture = 297,
                CorpseTexture = CorpseTexture(305, 2),
                HasIdle = false,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = false,
                MoveSound = (int)SoundClips.EnemyLamiaMove,
                BarkSound = (int)SoundClips.EnemyLamiaBark,
                AttackSound = (int)SoundClips.EnemyLamiaAttack,
                MinMetalToHit = WeaponMaterialTypes.None,
                MinDamage = 5,
                MaxDamage = 13,
                MinHealth = 35,
                MaxHealth = 65,
                Level = 16,
                ArmorValue = 0,
                ParrySounds = false,
                MapChance = 0,
                LootTableKey = "R",
                Weight = 200, // Using same value as nymph
                SoulPts = 10000,
                PrimaryAttackAnimFrames = new int[] { 0, -1, 1, 2, 3, 4, -1, 5, 0 },
                ChanceForAttack2 = 33,
                PrimaryAttackAnimFrames2 = new int[] { 0, 3, -1, 5, 4, 3, 3, -1, 5, 4, 3, -1, 5, 0 },
                ChanceForAttack3 = 33,
                PrimaryAttackAnimFrames3 = new int[] { 0, 3, -1, 5, 0 },
                Team = MobileTeams.Aquatic,
            },

            // Mage
            new MobileEnemy()
            {
                ID = 128,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Human,
                MaleTexture = 486,
                FemaleTexture = 485,
                CorpseTexture = CorpseTexture(380, 1),
                HasIdle = true,
                HasRangedAttack1 = true,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = true,
                MoveSound = (int)SoundClips.EnemyHumanMove,
                BarkSound = (int)SoundClips.EnemyHumanBark,
                AttackSound = (int)SoundClips.EnemyHumanAttack,
                ParrySounds = false,
                MapChance = 3,
                LootTableKey = "U",
                CastsMagic = true,
                PrimaryAttackAnimFrames = new int[] { 0, 1, -1, 3, 2, 1, 0, -1, 5, 4, 0 },
                ChanceForAttack2 = 33,
                PrimaryAttackAnimFrames2 = new int[] { 0, 1, -1, 3, 2, 1, 0 },
                ChanceForAttack3 = 33,
                PrimaryAttackAnimFrames3 = new int[] { 0, -1, 5, 4, 0 },
                HasSpellAnimation = true,
                SpellAnimFrames = new int[] { 0, 1, 2, 3, 3 },
                Team = MobileTeams.KnightsAndMages,
            },

            // Spellsword
            new MobileEnemy()
            {
                ID = 129,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Human,
                MaleTexture = 476,
                FemaleTexture = 475,
                CorpseTexture = CorpseTexture(380, 1),
                HasIdle = true,
                HasRangedAttack1 = true,
                HasRangedAttack2 = false,       // Female has RangedAttack2, male variant does not. Setting false for consistency.
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = true,
                MoveSound = (int)SoundClips.EnemyHumanMove,
                BarkSound = (int)SoundClips.EnemyHumanBark,
                AttackSound = (int)SoundClips.EnemyHumanAttack,
                ParrySounds = true,
                MapChance = 1,
                LootTableKey = "P",
                CastsMagic = true,
                PrimaryAttackAnimFrames = new int[] { 0, 1, -1, 2, 3, 4, 5 },
                ChanceForAttack2 = 33,
                PrimaryAttackAnimFrames2 = new int[] { 5, 4, 3, -1, 2, 1, 0 },
                ChanceForAttack3 = 33,
                PrimaryAttackAnimFrames3 = new int[] { 0, 1, -1, 2, 2, 1, 0 },
                RangedAttackAnimFrames = new int[] { 3, 2, 0, 0, 0, -1, 1, 1, 2, 3 },
                HasSpellAnimation = true,
                SpellAnimFrames = new int[] { 0, 1, 2, 3, 3 },
                Team = MobileTeams.KnightsAndMages,
            },

            // Battlemage
            new MobileEnemy()
            {
                ID = 130,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Human,
                MaleTexture = 490,
                FemaleTexture = 489,
                CorpseTexture = CorpseTexture(380, 1),
                HasIdle = true,
                HasRangedAttack1 = true,
                HasRangedAttack2 = true,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = true,
                MoveSound = (int)SoundClips.EnemyHumanMove,
                BarkSound = (int)SoundClips.EnemyHumanBark,
                AttackSound = (int)SoundClips.EnemyHumanAttack,
                ParrySounds = true,
                MapChance = 2,
                LootTableKey = "U",
                CastsMagic = true,
                PrimaryAttackAnimFrames = new int[] { 0, 1, -1, 2, 3, 4, -1, 5, 0 },
                ChanceForAttack2 = 33,
                PrimaryAttackAnimFrames2 = new int[] { 4, 4, -1, 5, 0, 0 },
                ChanceForAttack3 = 33,
                PrimaryAttackAnimFrames3 = new int[] { 4, -1, 5, 0, 0, 1, -1, 2, 3, 4, -1, 5, 0 },
                RangedAttackAnimFrames = new int[] { 3, 2, 0, 0, 0, -1, 1, 1, 2, 3 },
                HasSpellAnimation = true,
                SpellAnimFrames = new int[] { 0, 1, 2, 3, 3 },
                Team = MobileTeams.KnightsAndMages,
            },

            // Sorcerer
            new MobileEnemy()
            {
                ID = 131,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Human,
                MaleTexture = 478,
                FemaleTexture = 477,
                CorpseTexture = CorpseTexture(380, 1),
                HasIdle = true,
                HasRangedAttack1 = true,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = true,
                MoveSound = (int)SoundClips.EnemyHumanMove,
                BarkSound = (int)SoundClips.EnemyHumanBark,
                AttackSound = (int)SoundClips.EnemyHumanAttack,
                ParrySounds = false,
                MapChance = 3,
                LootTableKey = "U",
                CastsMagic = false,
                PrimaryAttackAnimFrames = new int[] { 0, 1, 2, -1, 3, 4, 5 },
                ChanceForAttack2 = 50,
                PrimaryAttackAnimFrames2 = new int[] { 4, 5, -1, 3, 2, 1, 0 },
                RangedAttackAnimFrames = new int[] { 3, 2, 0, 0, 0, -1, 1, 1, 2, 3 },
                Team = MobileTeams.KnightsAndMages,
            },

            // Healer
            new MobileEnemy()
            {
                ID = 132,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Human,
                MaleTexture = 486,
                FemaleTexture = 485,
                CorpseTexture = CorpseTexture(380, 1),
                HasIdle = true,
                HasRangedAttack1 = true,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = true,
                MoveSound = (int)SoundClips.EnemyHumanMove,
                BarkSound = (int)SoundClips.EnemyHumanBark,
                AttackSound = (int)SoundClips.EnemyHumanAttack,
                ParrySounds = false,
                MapChance = 1,
                LootTableKey = "U",
                CastsMagic = true,
                PrimaryAttackAnimFrames = new int[] { 0, 1, -1, 3, 2, 1, 0, -1, 5, 4, 0 },
                ChanceForAttack2 = 33,
                PrimaryAttackAnimFrames2 = new int[] { 0, 1, -1, 3, 2, 1, 0 },
                ChanceForAttack3 = 33,
                PrimaryAttackAnimFrames3 = new int[] { 0, -1, 5, 4, 0 },
                HasSpellAnimation = true,
                SpellAnimFrames = new int[] { 0, 1, 2, 3, 3 },
                Team = MobileTeams.KnightsAndMages,
            },

            // Nightblade
            new MobileEnemy()
            {
                ID = 133,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Human,
                MaleTexture = 490,
                FemaleTexture = 489,
                CorpseTexture = CorpseTexture(380, 1),
                HasIdle = true,
                HasRangedAttack1 = true,
                HasRangedAttack2 = true,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = true,
                MoveSound = (int)SoundClips.EnemyHumanMove,
                BarkSound = (int)SoundClips.EnemyHumanBark,
                AttackSound = (int)SoundClips.EnemyHumanAttack,
                ParrySounds = true,
                MapChance = 1,
                LootTableKey = "U",
                CastsMagic = true,
                PrimaryAttackAnimFrames = new int[] { 0, 1, -1, 2, 3, 4, -1, 5, 0 },
                ChanceForAttack2 = 33,
                PrimaryAttackAnimFrames2 = new int[] { 4, 4, -1, 5, 0, 0 },
                ChanceForAttack3 = 33,
                PrimaryAttackAnimFrames3 = new int[] { 4, -1, 5, 0, 0, 1, -1, 2, 3, 4, -1, 5, 0 },
                RangedAttackAnimFrames = new int[] { 3, 2, 0, 0, 0, -1, 1, 1, 2, 3 },
                HasSpellAnimation = true,
                SpellAnimFrames = new int[] { 0, 1, 2, 3, 3 },
                Team = MobileTeams.Criminals,
            },

            // Bard
            new MobileEnemy()
            {
                ID = 134,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Human,
                MaleTexture = 484,
                FemaleTexture = 483,
                CorpseTexture = CorpseTexture(380, 1),
                HasIdle = true,
                HasRangedAttack1 = true,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = true,
                MoveSound = (int)SoundClips.EnemyHumanMove,
                BarkSound = (int)SoundClips.EnemyHumanBark,
                AttackSound = (int)SoundClips.EnemyHumanAttack,
                ParrySounds = true,
                MapChance = 2,
                LootTableKey = "O",
                CastsMagic = false,
                PrimaryAttackAnimFrames = new int[] { 0, 1, -1, 2, 3, 4, -1, 5, 0 },
                ChanceForAttack2 = 33,
                PrimaryAttackAnimFrames2 = new int[] { 4, 4, -1, 5, 0, 0 },
                ChanceForAttack3 = 33,
                PrimaryAttackAnimFrames3 = new int[] { 4, -1, 5, 0, 0, 1, -1, 2, 3, 4, -1, 5, 0 },
                RangedAttackAnimFrames = new int[] { 3, 2, 0, 0, 0, -1, 1, 1, 2, 3 },
                Team = MobileTeams.KnightsAndMages,
            },

            // Burglar
            new MobileEnemy()
            {
                ID = 135,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Human,
                MaleTexture = 484,
                FemaleTexture = 483,
                CorpseTexture = CorpseTexture(380, 1),
                HasIdle = true,
                HasRangedAttack1 = true,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = true,
                MoveSound = (int)SoundClips.EnemyHumanMove,
                BarkSound = (int)SoundClips.EnemyHumanBark,
                AttackSound = (int)SoundClips.EnemyHumanAttack,
                ParrySounds = true,
                MapChance = 1,
                LootTableKey = "O",
                CastsMagic = false,
                PrimaryAttackAnimFrames = new int[] { 0, 1, -1, 2, 3, 4, -1, 5, 0 },
                ChanceForAttack2 = 33,
                PrimaryAttackAnimFrames2 = new int[] { 4, 4, -1, 5, 0, 0 },
                ChanceForAttack3 = 33,
                PrimaryAttackAnimFrames3 = new int[] { 4, -1, 5, 0, 0, 1, -1, 2, 3, 4, -1, 5, 0 },
                RangedAttackAnimFrames = new int[] { 3, 2, 0, 0, 0, -1, 1, 1, 2, 3 },
                Team = MobileTeams.Criminals,
            },

            // Rogue
            new MobileEnemy()
            {
                ID = 136,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Human,
                MaleTexture = 480,
                FemaleTexture = 479,
                CorpseTexture = CorpseTexture(380, 1),
                HasIdle = true,
                HasRangedAttack1 = true,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = true,
                MoveSound = (int)SoundClips.EnemyHumanMove,
                BarkSound = (int)SoundClips.EnemyHumanBark,
                AttackSound = (int)SoundClips.EnemyHumanAttack,
                ParrySounds = true,
                MapChance = 1,
                LootTableKey = "O",
                CastsMagic = false,
                PrimaryAttackAnimFrames = new int[] { 0, 1, -1, 2, 3, 4, -1, 5, 0 },
                ChanceForAttack2 = 33,
                PrimaryAttackAnimFrames2 = new int[] { 4, 4, -1, 5, 0, 0 },
                ChanceForAttack3 = 33,
                PrimaryAttackAnimFrames3 = new int[] { 4, -1, 5, 0, 0, 1, -1, 2, 3, 4, -1, 5, 0 },
                RangedAttackAnimFrames = new int[] { 3, 2, 0, 0, 0, -1, 1, 1, 2, 3 },
                Team = MobileTeams.Criminals,
            },

            // Acrobat
            new MobileEnemy()
            {
                ID = 137,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Human,
                MaleTexture = 484,
                FemaleTexture = 483,
                CorpseTexture = CorpseTexture(380, 1),
                HasIdle = true,
                HasRangedAttack1 = true,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = true,
                MoveSound = (int)SoundClips.EnemyHumanMove,
                BarkSound = (int)SoundClips.EnemyHumanBark,
                AttackSound = (int)SoundClips.EnemyHumanAttack,
                ParrySounds = true,
                MapChance = 0,
                LootTableKey = "O",
                CastsMagic = false,
                PrimaryAttackAnimFrames = new int[] { 0, 1, -1, 2, 3, 4, -1, 5, 0 },
                ChanceForAttack2 = 33,
                PrimaryAttackAnimFrames2 = new int[] { 4, 4, -1, 5, 0, 0 },
                ChanceForAttack3 = 33,
                PrimaryAttackAnimFrames3 = new int[] { 4, -1, 5, 0, 0, 1, -1, 2, 3, 4, -1, 5, 0 },
                RangedAttackAnimFrames = new int[] { 3, 2, 0, 0, 0, -1, 1, 1, 2, 3 },
                Team = MobileTeams.KnightsAndMages,
            },

            // Thief
            new MobileEnemy()
            {
                ID = 138,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Human,
                MaleTexture = 484,
                FemaleTexture = 483,
                CorpseTexture = CorpseTexture(380, 1),
                HasIdle = true,
                HasRangedAttack1 = true,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = true,
                MoveSound = (int)SoundClips.EnemyHumanMove,
                BarkSound = (int)SoundClips.EnemyHumanBark,
                AttackSound = (int)SoundClips.EnemyHumanAttack,
                ParrySounds = true,
                MapChance = 2,
                LootTableKey = "O",
                CastsMagic = false,
                PrimaryAttackAnimFrames = new int[] { 0, 1, -1, 2, 3, 4, -1, 5, 0 },
                ChanceForAttack2 = 33,
                PrimaryAttackAnimFrames2 = new int[] { 4, 4, -1, 5, 0, 0 },
                ChanceForAttack3 = 33,
                PrimaryAttackAnimFrames3 = new int[] { 4, -1, 5, 0, 0, 1, -1, 2, 3, 4, -1, 5, 0 },
                RangedAttackAnimFrames = new int[] { 3, 2, 0, 0, 0, -1, 1, 1, 2, 3 },
                Team = MobileTeams.Criminals,
            },

            // Assassin
            new MobileEnemy()
            {
                ID = 139,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Human,
                MaleTexture = 480,
                FemaleTexture = 479,
                CorpseTexture = CorpseTexture(380, 1),
                HasIdle = true,
                HasRangedAttack1 = true,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = true,
                MoveSound = (int)SoundClips.EnemyHumanMove,
                BarkSound = (int)SoundClips.EnemyHumanBark,
                AttackSound = (int)SoundClips.EnemyHumanAttack,
                ParrySounds = true,
                MapChance = 0,
                LootTableKey = "O",
                CastsMagic = false,
                PrimaryAttackAnimFrames = new int[] { 0, 1, -1, 2, 3, 4, -1, 5, 0 },
                ChanceForAttack2 = 33,
                PrimaryAttackAnimFrames2 = new int[] { 4, 4, -1, 5, 0, 0 },
                ChanceForAttack3 = 33,
                PrimaryAttackAnimFrames3 = new int[] { 4, -1, 5, 0, 0, 1, -1, 2, 3, 4, -1, 5, 0 },
                RangedAttackAnimFrames = new int[] { 3, 2, 0, 0, 0, -1, 1, 1, 2, 3 },
                Team = MobileTeams.Criminals,
            },

            // Monk
            new MobileEnemy()
            {
                ID = 140,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Human,
                MaleTexture = 488,
                FemaleTexture = 487,
                CorpseTexture = CorpseTexture(380, 1),
                HasIdle = true,
                HasRangedAttack1 = true,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = true,
                MoveSound = (int)SoundClips.EnemyHumanMove,
                BarkSound = (int)SoundClips.EnemyHumanBark,
                AttackSound = (int)SoundClips.EnemyHumanAttack,
                ParrySounds = true,
                MapChance = 1,
                LootTableKey = "T",
                CastsMagic = false,
                PrimaryAttackAnimFrames = new int[] { 0, 0, 1, -1, 2, 2, 1, 0 },
                ChanceForAttack2 = 33,
                PrimaryAttackAnimFrames2 = new int[] { 0, 1, -1, 2, 3, 4, 5 },
                ChanceForAttack3 = 33,
                PrimaryAttackAnimFrames3 = new int[] { 5, 5, 3, -1, 2, 1, 0 },
                RangedAttackAnimFrames = new int[] { 3, 2, 0, 0, 0, -1, 1, 1, 2, 3 },
                Team = MobileTeams.KnightsAndMages,
            },

            // Archer
            new MobileEnemy()
            {
                ID = 141,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Human,
                MaleTexture = 482,
                FemaleTexture = 481,
                CorpseTexture = CorpseTexture(380, 1),
                HasIdle = true,
                HasRangedAttack1 = true,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = true,
                PrefersRanged = true,
                MoveSound = (int)SoundClips.EnemyHumanMove,
                BarkSound = (int)SoundClips.EnemyHumanBark,
                AttackSound = (int)SoundClips.EnemyHumanAttack,
                ParrySounds = true,
                MapChance = 0,
                LootTableKey = "C",
                CastsMagic = false,
                PrimaryAttackAnimFrames = new int[] { 0, 1, -1, 2, 3, 4, -1, 5 },
                ChanceForAttack2 = 50,
                PrimaryAttackAnimFrames2 = new int[] { 3, 4, -1, 5, 0 },
                RangedAttackAnimFrames = new int[] { 3, 2, 0, 0, 0, -1, 1, 1, 2, 3 },
                Team = MobileTeams.KnightsAndMages,
            },

            // Ranger
            new MobileEnemy()
            {
                ID = 142,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Human,
                MaleTexture = 482,
                FemaleTexture = 481,
                CorpseTexture = CorpseTexture(380, 1),
                HasIdle = true,
                HasRangedAttack1 = true,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = true,
                MoveSound = (int)SoundClips.EnemyHumanMove,
                BarkSound = (int)SoundClips.EnemyHumanBark,
                AttackSound = (int)SoundClips.EnemyHumanAttack,
                ParrySounds = true,
                MapChance = 1,
                LootTableKey = "C",
                CastsMagic = false,
                PrimaryAttackAnimFrames = new int[] { 0, 1, -1, 2, 3, 4, -1, 5 },
                ChanceForAttack2 = 50,
                PrimaryAttackAnimFrames2 = new int[] { 3, 4, -1, 5, 0 },
                RangedAttackAnimFrames = new int[] { 3, 2, 0, 0, 0, -1, 1, 1, 2, 3 },
                Team = MobileTeams.KnightsAndMages,
            },

            // Barbarian
            new MobileEnemy()
            {
                ID = 143,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Human,
                MaleTexture = 488,
                FemaleTexture = 487,
                CorpseTexture = CorpseTexture(380, 1),
                HasIdle = true,
                HasRangedAttack1 = true,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = true,
                MoveSound = (int)SoundClips.EnemyHumanMove,
                BarkSound = (int)SoundClips.EnemyHumanBark,
                AttackSound = (int)SoundClips.EnemyHumanAttack,
                ParrySounds = true,
                MapChance = 0,
                LootTableKey = "T",
                CastsMagic = false,
                PrimaryAttackAnimFrames = new int[] { 0, 0, 1, -1, 2, 2, 1, 0 },
                ChanceForAttack2 = 33,
                PrimaryAttackAnimFrames2 = new int[] { 0, 1, -1, 2, 3, 4, 5 },
                ChanceForAttack3 = 33,
                PrimaryAttackAnimFrames3 = new int[] { 5, 5, 3, -1, 2, 1, 0 },
                RangedAttackAnimFrames = new int[] { 3, 2, 0, 0, 0, -1, 1, 1, 2, 3 },
                Team = MobileTeams.Criminals,
            },

            // Warrior
            new MobileEnemy()
            {
                ID = 144,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Human,
                MaleTexture = 488,
                FemaleTexture = 487,
                CorpseTexture = CorpseTexture(380, 1),
                HasIdle = true,
                HasRangedAttack1 = true,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = true,
                MoveSound = (int)SoundClips.EnemyHumanMove,
                BarkSound = (int)SoundClips.EnemyHumanBark,
                AttackSound = (int)SoundClips.EnemyHumanAttack,
                ParrySounds = true,
                MapChance = 0,
                LootTableKey = "T",
                CastsMagic = false,
                PrimaryAttackAnimFrames = new int[] { 0, 0, 1, -1, 2, 2, 1, 0 },
                ChanceForAttack2 = 33,
                PrimaryAttackAnimFrames2 = new int[] { 0, 1, -1, 2, 3, 4, 5 },
                ChanceForAttack3 = 33,
                PrimaryAttackAnimFrames3 = new int[] { 5, 5, 3, -1, 2, 1, 0 },
                RangedAttackAnimFrames = new int[] { 3, 2, 0, 0, 0, -1, 1, 1, 2, 3 },
                Team = MobileTeams.KnightsAndMages,
            },

            // Knight
            new MobileEnemy()
            {
                ID = 145,
                Behaviour = MobileBehaviour.General,
                Affinity = MobileAffinity.Human,
                MaleTexture = 488,
                FemaleTexture = 487,
                CorpseTexture = CorpseTexture(380, 1),
                HasIdle = true,
                HasRangedAttack1 = true,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = true,
                MoveSound = (int)SoundClips.EnemyHumanMove,
                BarkSound = (int)SoundClips.EnemyHumanBark,
                AttackSound = (int)SoundClips.EnemyHumanAttack,
                ParrySounds = true,
                MapChance = 1,
                LootTableKey = "T",
                CastsMagic = false,
                PrimaryAttackAnimFrames = new int[] { 0, 0, 1, -1, 2, 2, 1, 0 },
                ChanceForAttack2 = 33,
                PrimaryAttackAnimFrames2 = new int[] { 0, 1, -1, 2, 3, 4, 5 },
                ChanceForAttack3 = 33,
                PrimaryAttackAnimFrames3 = new int[] { 5, 5, 3, -1, 2, 1, 0 },
                RangedAttackAnimFrames = new int[] { 3, 2, 0, 0, 0, -1, 1, 1, 2, 3 },
                Team = MobileTeams.KnightsAndMages,
            },

            // City Watch - The Haltmeister
            new MobileEnemy()
            {
                ID = 146,
                Behaviour = MobileBehaviour.Guard,
                Affinity = MobileAffinity.Human,
                MaleTexture = 399,
                FemaleTexture = 399,
                CorpseTexture = CorpseTexture(380, 1),
                HasIdle = true,
                HasRangedAttack1 = false,
                HasRangedAttack2 = false,
                CanOpenDoors = true,
                CanBashDownDoors = true,
                CanDrinkPotions = true,
                MoveSound = (int)SoundClips.None,
                BarkSound = (int)SoundClips.Halt,
                AttackSound = (int)SoundClips.None,
                ParrySounds = true,
                MapChance = 0,
                CastsMagic = false,
                PrimaryAttackAnimFrames = new int[] { 0, 1, -1, 2, 3, 4 },
                Team = MobileTeams.CityWatch,
            },
        };

        #endregion

        #region Enemy Damage Type Resistance

        public static float HumanClassPhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                return 1f;
            }
            else if (damType == 2)
            {
                if (armor != null && armor.shear >= 300 || shieldBlockSuccess)
                    return 1f;
                else
                    return 1.50f;
            }
            else if (damType == 3)
            {
                if (armor != null && armor.density >= 300 || shieldBlockSuccess)
                    return 1f;
                else
                    return 1.50f;
            }
            else
            {
                return 1f;
            }
        }

        public static float RatPhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                return 1.50f;
            }
            else if (damType == 2)
            {
                return 1.50f;
            }
            else if (damType == 3)
            {
                return 1.50f;
            }
            else
            {
                return 1f;
            }
        }

        public static float GiantBatPhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                return 1.50f;
            }
            else if (damType == 2)
            {
                return 1.50f;
            }
            else if (damType == 3)
            {
                return 1.50f;
            }
            else
            {
                return 1f;
            }
        }

        public static float GrizzlyBearPhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                return 1f;
            }
            else if (damType == 2)
            {
                return 1.50f;
            }
            else if (damType == 3)
            {
                return 1.50f;
            }
            else
            {
                return 1f;
            }
        }

        public static float SabertoothTigerPhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                return 1f;
            }
            else if (damType == 2)
            {
                return 1.50f;
            }
            else if (damType == 3)
            {
                return 1.50f;
            }
            else
            {
                return 1f;
            }
        }

        public static float SpiderPhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                return 1.50f;
            }
            else if (damType == 2)
            {
                return 1f;
            }
            else if (damType == 3)
            {
                return 0.65f;
            }
            else
            {
                return 1f;
            }
        }

        public static float SlaughterfishPhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                return 1f;
            }
            else if (damType == 2)
            {
                return 1f;
            }
            else if (damType == 3)
            {
                return 1f;
            }
            else
            {
                return 1f;
            }
        }

        public static float GiantScorpionPhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                return 1.50f;
            }
            else if (damType == 2)
            {
                return 1f;
            }
            else if (damType == 3)
            {
                return 0.65f;
            }
            else
            {
                return 1f;
            }
        }

        public static float ImpPhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                if (armor != null && armor.fracture >= 300 || shieldBlockSuccess)
                    return 1f;
                else
                    return 1.50f;
            }
            else if (damType == 2)
            {
                if (armor != null && armor.shear >= 300 || shieldBlockSuccess)
                    return 1f;
                else
                    return 1.50f;
            }
            else if (damType == 3)
            {
                if (armor != null && armor.density >= 300 || shieldBlockSuccess)
                    return 1f;
                else
                    return 1.50f;
            }
            else
            {
                return 1f;
            }
        }

        public static float SprigganPhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                return 0.65f;
            }
            else if (damType == 2)
            {
                return 1.50f;
            }
            else if (damType == 3)
            {
                return 0.32f;
            }
            else
            {
                return 1f;
            }
        }

        public static float CentaurPhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                return 1f;
            }
            else if (damType == 2)
            {
                if (armor != null && armor.shear >= 300 || shieldBlockSuccess)
                    return 1f;
                else
                    return 1.50f;
            }
            else if (damType == 3)
            {
                if (armor != null && armor.density >= 300 || shieldBlockSuccess)
                    return 1f;
                else
                    return 1.50f;
            }
            else
            {
                return 1f;
            }
        }

        public static float NymphPhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                if (armor != null && armor.fracture >= 300 || shieldBlockSuccess)
                    return 1f;
                else
                    return 1.50f;
            }
            else if (damType == 2)
            {
                if (armor != null && armor.shear >= 300 || shieldBlockSuccess)
                    return 1f;
                else
                    return 1.50f;
            }
            else if (damType == 3)
            {
                if (armor != null && armor.density >= 300 || shieldBlockSuccess)
                    return 1f;
                else
                    return 1.50f;
            }
            else
            {
                return 1f;
            }
        }

        public static float HarpyPhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                return 1f;
            }
            else if (damType == 2)
            {
                return 1.50f;
            }
            else if (damType == 3)
            {
                return 1.50f;
            }
            else
            {
                return 1f;
            }
        }

        public static float GiantPhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                return 1f;
            }
            else if (damType == 2)
            {
                if (armor != null && armor.shear >= 300 || shieldBlockSuccess)
                    return 1f;
                else
                    return 1.50f;
            }
            else if (damType == 3)
            {
                if (armor != null && armor.density >= 300 || shieldBlockSuccess)
                    return 1f;
                else
                    return 1.50f;
            }
            else
            {
                return 1f;
            }
        }

        public static float GargoylePhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                return 2f;
            }
            else if (damType == 2)
            {
                return 0.65f;
            }
            else if (damType == 3)
            {
                return 0.32f;
            }
            else
            {
                return 1f;
            }
        }

        public static float DragonlingPhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                return 1.50f;
            }
            else if (damType == 2)
            {
                return 1f;
            }
            else if (damType == 3)
            {
                return 1.50f;
            }
            else
            {
                return 1f;
            }
        }

        public static float LargeDragonlingPhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                return 1f;
            }
            else if (damType == 2)
            {
                return 0.65f;
            }
            else if (damType == 3)
            {
                return 1.50f;
            }
            else
            {
                return 1f;
            }
        }

        public static float DreughPhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                return 1.50f;
            }
            else if (damType == 2)
            {
                return 0.65f;
            }
            else if (damType == 3)
            {
                return 1f;
            }
            else
            {
                return 1f;
            }
        }

        public static float LamiaPhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                return 1.50f;
            }
            else if (damType == 2)
            {
                return 1f;
            }
            else if (damType == 3)
            {
                return 1.50f;
            }
            else
            {
                return 1f;
            }
        }

        public static float OrcPhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                return 1f;
            }
            else if (damType == 2)
            {
                return 0.65f;
            }
            else if (damType == 3)
            {
                return 1f;
            }
            else
            {
                return 1f;
            }
        }

        public static float OrcSergeantPhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                return 1f;
            }
            else if (damType == 2)
            {
                return 0.65f;
            }
            else if (damType == 3)
            {
                return 1f;
            }
            else
            {
                return 1f;
            }
        }

        public static float OrcShamanPhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                if (armor != null && armor.fracture >= 300 || shieldBlockSuccess)
                    return 1f;
                else
                    return 1.50f;
            }
            else if (damType == 2)
            {
                if (armor != null && armor.shear >= 300 || shieldBlockSuccess)
                    return 1f;
                else
                    return 1.50f;
            }
            else if (damType == 3)
            {
                if (armor != null && armor.density >= 300 || shieldBlockSuccess)
                    return 1f;
                else
                    return 1.50f;
            }
            else
            {
                return 1f;
            }
        }

        public static float OrcWarlordPhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                return 1f;
            }
            else if (damType == 2)
            {
                return 0.65f;
            }
            else if (damType == 3)
            {
                return 1f;
            }
            else
            {
                return 1f;
            }
        }

        public static float WerewolfPhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                return 1f;
            }
            else if (damType == 2)
            {
                return 1.50f;
            }
            else if (damType == 3)
            {
                return 1.50f;
            }
            else
            {
                return 1f;
            }
        }

        public static float WereboarPhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                return 1f;
            }
            else if (damType == 2)
            {
                return 1.50f;
            }
            else if (damType == 3)
            {
                return 1.50f;
            }
            else
            {
                return 1f;
            }
        }

        public static float FireAtronachPhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                return 1f;
            }
            else if (damType == 2)
            {
                return 1f;
            }
            else if (damType == 3)
            {
                return 1f;
            }
            else
            {
                return 1f;
            }
        }

        public static float IronAtronachPhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                return 0.32f;
            }
            else if (damType == 2)
            {
                return 1f;
            }
            else if (damType == 3)
            {
                return 0.65f;
            }
            else
            {
                return 1f;
            }
        }

        public static float FleshAtronachPhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                return 1f;
            }
            else if (damType == 2)
            {
                return 1.50f;
            }
            else if (damType == 3)
            {
                return 1.50f;
            }
            else
            {
                return 1f;
            }
        }

        public static float IceAtronachPhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                return 2f;
            }
            else if (damType == 2)
            {
                return 1f;
            }
            else if (damType == 3)
            {
                return 0.65f;
            }
            else
            {
                return 1f;
            }
        }

        public static float SkeletalWarriorPhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                if (armor != null && armor.fracture >= 300 || shieldBlockSuccess)
                    return 1f;
                else
                    return 2f;
            }
            else if (damType == 2)
            {
                return 1f;
            }
            else if (damType == 3)
            {
                return 0.32f;
            }
            else
            {
                return 1f;
            }
        }

        public static float ZombiePhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                return 1f;
            }
            else if (damType == 2)
            {
                if (armor != null && armor.shear >= 300 || shieldBlockSuccess)
                    return 1f;
                else
                    return 1.50f;
            }
            else if (damType == 3)
            {
                return 0.65f;
            }
            else
            {
                return 1f;
            }
        }

        public static float GhostPhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                return 0.65f;
            }
            else if (damType == 2)
            {
                return 0.65f;
            }
            else if (damType == 3)
            {
                return 0.65f;
            }
            else
            {
                return 1f;
            }
        }

        public static float MummyPhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                return 1f;
            }
            else if (damType == 2)
            {
                return 1.50f;
            }
            else if (damType == 3)
            {
                return 0.65f;
            }
            else
            {
                return 1f;
            }
        }

        public static float WraithPhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                return 0.65f;
            }
            else if (damType == 2)
            {
                return 0.65f;
            }
            else if (damType == 3)
            {
                return 0.65f;
            }
            else
            {
                return 1f;
            }
        }

        public static float VampirePhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                return 1f;
            }
            else if (damType == 2)
            {
                if (armor != null && armor.shear >= 300 || shieldBlockSuccess)
                    return 1f;
                else
                    return 1.50f;
            }
            else if (damType == 3)
            {
                if (armor != null && armor.density >= 300 || shieldBlockSuccess)
                    return 1f;
                else
                    return 1.50f;
            }
            else
            {
                return 1f;
            }
        }

        public static float VampireAncientPhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                return 1f;
            }
            else if (damType == 2)
            {
                if (armor != null && armor.shear >= 300 || shieldBlockSuccess)
                    return 1f;
                else
                    return 1.50f;
            }
            else if (damType == 3)
            {
                if (armor != null && armor.density >= 300 || shieldBlockSuccess)
                    return 1f;
                else
                    return 1.50f;
            }
            else
            {
                return 1f;
            }
        }

        public static float LichPhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                if (armor != null && armor.fracture >= 300 || shieldBlockSuccess)
                    return 1f;
                else
                    return 2f;
            }
            else if (damType == 2)
            {
                return 1f;
            }
            else if (damType == 3)
            {
                return 0.32f;
            }
            else
            {
                return 1f;
            }
        }

        public static float AncientLichPhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                if (armor != null && armor.fracture >= 300 || shieldBlockSuccess)
                    return 1f;
                else
                    return 2f;
            }
            else if (damType == 2)
            {
                return 1f;
            }
            else if (damType == 3)
            {
                return 0.32f;
            }
            else
            {
                return 1f;
            }
        }

        public static float FrostDaedraPhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                return 1f;
            }
            else if (damType == 2)
            {
                return 0.65f;
            }
            else if (damType == 3)
            {
                return 1f;
            }
            else
            {
                return 1f;
            }
        }

        public static float FireDaedraPhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                return 0.65f;
            }
            else if (damType == 2)
            {
                return 0.65f;
            }
            else if (damType == 3)
            {
                return 1f;
            }
            else
            {
                return 1f;
            }
        }

        public static float DaedrothPhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                return 1f;
            }
            else if (damType == 2)
            {
                return 0.65f;
            }
            else if (damType == 3)
            {
                return 1f;
            }
            else
            {
                return 1f;
            }
        }

        public static float DaedraSeducerPhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                if (armor != null && armor.fracture >= 300 || shieldBlockSuccess)
                    return 1f;
                else
                    return 1.50f;
            }
            else if (damType == 2)
            {
                if (armor != null && armor.shear >= 300 || shieldBlockSuccess)
                    return 1f;
                else
                    return 1.50f;
            }
            else if (damType == 3)
            {
                if (armor != null && armor.density >= 300 || shieldBlockSuccess)
                    return 1f;
                else
                    return 1.50f;
            }
            else
            {
                return 1f;
            }
        }

        public static float DaedraLordPhysicalDamTypeWeaknesses(DaggerfallEntity tarEnemy, int struckBodyPart, int damType, bool shieldBlockSuccess, DaggerfallUnityItem armor = null)
        {
            if (damType == 1)
            {
                return 0.65f;
            }
            else if (damType == 2)
            {
                return 0.65f;
            }
            else if (damType == 3)
            {
                return 1f;
            }
            else
            {
                return 1f;
            }
        }

        #endregion

        #region Enemy Elemental Type Resistance

        public static float HumanClassElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit) // These are just place-holder for now, i'm likely going to make seperate magic resistance values for the different types of human classes.
        {
            if (elementType == DFCareer.Elements.Fire)
                return 1.00f;
            else if (elementType == DFCareer.Elements.Frost)
                return 1.00f;
            else if (elementType == DFCareer.Elements.Shock)
                return 1.00f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 1.00f;
            else // Magic
                return 1.00f;
        }

        public static float RatElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 1.50f;
            else if (elementType == DFCareer.Elements.Frost)
                return 1.50f;
            else if (elementType == DFCareer.Elements.Shock)
                return 1.50f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 1.50f;
            else // Magic
                return 1.50f;
        }

        public static float GiantBatElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 1.50f;
            else if (elementType == DFCareer.Elements.Frost)
                return 1.50f;
            else if (elementType == DFCareer.Elements.Shock)
                return 1.50f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 1.50f;
            else // Magic
                return 1.50f;
        }

        public static float GrizzlyBearElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 1.50f;
            else if (elementType == DFCareer.Elements.Frost)
                return 1.00f;
            else if (elementType == DFCareer.Elements.Shock)
                return 1.50f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 1.50f;
            else // Magic
                return 1.00f;
        }

        public static float SabertoothTigerElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 1.50f;
            else if (elementType == DFCareer.Elements.Frost)
                return 1.00f;
            else if (elementType == DFCareer.Elements.Shock)
                return 1.50f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 1.50f;
            else // Magic
                return 1.00f;
        }

        public static float SpiderElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 1.00f;
            else if (elementType == DFCareer.Elements.Frost)
                return 1.50f;
            else if (elementType == DFCareer.Elements.Shock)
                return 1.00f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 0.65f;
            else // Magic
                return 1.00f;
        }

        public static float SlaughterfishElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 1.00f;
            else if (elementType == DFCareer.Elements.Frost)
                return 1.50f;
            else if (elementType == DFCareer.Elements.Shock)
                return 2.00f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 1.00f;
            else // Magic
                return 1.00f;
        }

        public static float GiantScorpionElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 0.65f;
            else if (elementType == DFCareer.Elements.Frost)
                return 1.50f;
            else if (elementType == DFCareer.Elements.Shock)
                return 1.00f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 0.65f;
            else // Magic
                return 1.00f;
        }

        public static float ImpElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 1.00f;
            else if (elementType == DFCareer.Elements.Frost)
                return 1.50f;
            else if (elementType == DFCareer.Elements.Shock)
                return 1.50f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 0.65f;
            else // Magic
                return 0.65f;
        }

        public static float SprigganElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 2.00f;
            else if (elementType == DFCareer.Elements.Frost)
                return 1.00f;
            else if (elementType == DFCareer.Elements.Shock)
                return 0.32f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 1.00f;
            else // Magic
                return 0.65f;
        }

        public static float CentaurElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 1.00f;
            else if (elementType == DFCareer.Elements.Frost)
                return 1.00f;
            else if (elementType == DFCareer.Elements.Shock)
                return 1.50f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 1.50f;
            else // Magic
                return 1.50f;
        }

        public static float NymphElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 1.50f;
            else if (elementType == DFCareer.Elements.Frost)
                return 1.50f;
            else if (elementType == DFCareer.Elements.Shock)
                return 1.50f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 0.65f;
            else // Magic
                return 0.65f;
        }

        public static float HarpyElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 1.00f;
            else if (elementType == DFCareer.Elements.Frost)
                return 1.50f;
            else if (elementType == DFCareer.Elements.Shock)
                return 1.00f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 1.00f;
            else // Magic
                return 0.65f;
        }

        public static float GiantElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 1.50f;
            else if (elementType == DFCareer.Elements.Frost)
                return 1.00f;
            else if (elementType == DFCareer.Elements.Shock)
                return 1.50f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 1.50f;
            else // Magic
                return 1.50f;
        }

        public static float GargoyleElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 0.65f;
            else if (elementType == DFCareer.Elements.Frost)
                return 1.00f;
            else if (elementType == DFCareer.Elements.Shock)
                return 0.32f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 0.32f;
            else // Magic
                return 1.00f;
        }

        public static float DragonlingElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 0.65f;
            else if (elementType == DFCareer.Elements.Frost)
                return 1.50f;
            else if (elementType == DFCareer.Elements.Shock)
                return 1.00f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 1.00f;
            else // Magic
                return 0.65f;
        }

        public static float LargeDragonlingElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 0.32f;
            else if (elementType == DFCareer.Elements.Frost)
                return 1.50f;
            else if (elementType == DFCareer.Elements.Shock)
                return 1.00f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 0.65f;
            else // Magic
                return 0.32f;
        }

        public static float DreughElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 0.65f;
            else if (elementType == DFCareer.Elements.Frost)
                return 1.50f;
            else if (elementType == DFCareer.Elements.Shock)
                return 1.50f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 1.00f;
            else // Magic
                return 0.65f;
        }

        public static float LamiaElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 0.65f;
            else if (elementType == DFCareer.Elements.Frost)
                return 1.50f;
            else if (elementType == DFCareer.Elements.Shock)
                return 1.50f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 0.32f;
            else // Magic
                return 0.32f;
        }

        public static float OrcElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 1.50f;
            else if (elementType == DFCareer.Elements.Frost)
                return 1.00f;
            else if (elementType == DFCareer.Elements.Shock)
                return 1.50f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 1.50f;
            else // Magic
                return 1.50f;
        }

        public static float OrcSergeantElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 1.00f;
            else if (elementType == DFCareer.Elements.Frost)
                return 1.00f;
            else if (elementType == DFCareer.Elements.Shock)
                return 1.50f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 1.50f;
            else // Magic
                return 1.50f;
        }

        public static float OrcShamanElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 0.65f;
            else if (elementType == DFCareer.Elements.Frost)
                return 1.00f;
            else if (elementType == DFCareer.Elements.Shock)
                return 1.00f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 1.00f;
            else // Magic
                return 0.65f;
        }

        public static float OrcWarlordElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 0.65f;
            else if (elementType == DFCareer.Elements.Frost)
                return 0.65f;
            else if (elementType == DFCareer.Elements.Shock)
                return 1.00f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 1.50f;
            else // Magic
                return 1.00f;
        }

        public static float WerewolfElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 1.50f;
            else if (elementType == DFCareer.Elements.Frost)
                return 1.00f;
            else if (elementType == DFCareer.Elements.Shock)
                return 1.50f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 1.00f;
            else // Magic
                return 1.00f;
        }

        public static float WereboarElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 1.50f;
            else if (elementType == DFCareer.Elements.Frost)
                return 1.00f;
            else if (elementType == DFCareer.Elements.Shock)
                return 1.50f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 1.00f;
            else // Magic
                return 1.00f;
        }

        public static float FireAtronachElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 0f; // Have to see how I might add restoration as well? But have to see how that works in classic code first as well, probably some flag somewhere.
            else if (elementType == DFCareer.Elements.Frost)
                return 2.00f;
            else if (elementType == DFCareer.Elements.Shock)
                return 1.00f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 0.65f;
            else // Magic
                return 1.00f;
        }

        public static float IronAtronachElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 0.65f;
            else if (elementType == DFCareer.Elements.Frost)
                return 1.00f;
            else if (elementType == DFCareer.Elements.Shock)
                return 0f; // Have to see how I might add restoration as well? But have to see how that works in classic code first as well, probably some flag somewhere.
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 0.32f;
            else // Magic
                return 2.00f;
        }

        public static float FleshAtronachElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 1.00f;
            else if (elementType == DFCareer.Elements.Frost)
                return 1.00f;
            else if (elementType == DFCareer.Elements.Shock)
                return 1.00f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 2.00f;
            else // Magic // Have to see how I might add restoration as well? But have to see how that works in classic code first as well, probably some flag somewhere.
                return 0f;
        }

        public static float IceAtronachElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 2.00f;
            else if (elementType == DFCareer.Elements.Frost)
                return 0f; // Have to see how I might add restoration as well? But have to see how that works in classic code first as well, probably some flag somewhere.
            else if (elementType == DFCareer.Elements.Shock)
                return 0.65f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 0.65f;
            else // Magic
                return 1.00f;
        }

        public static float SkeletalWarriorElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 1.00f;
            else if (elementType == DFCareer.Elements.Frost)
                return 1.00f;
            else if (elementType == DFCareer.Elements.Shock)
                return 0.65f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 0.65f;
            else // Magic
                return 2.00f;
        }

        public static float ZombieElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 1.50f;
            else if (elementType == DFCareer.Elements.Frost)
                return 0.65f;
            else if (elementType == DFCareer.Elements.Shock)
                return 0.65f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 0.32f;
            else // Magic
                return 1.00f;
        }

        public static float GhostElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 1.00f;
            else if (elementType == DFCareer.Elements.Frost)
                return 0.65f;
            else if (elementType == DFCareer.Elements.Shock)
                return 1.00f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 0.65f;
            else // Magic
                return 1.50f;
        }

        public static float MummyElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 1.50f;
            else if (elementType == DFCareer.Elements.Frost)
                return 0.65f;
            else if (elementType == DFCareer.Elements.Shock)
                return 0.65f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 0.65f;
            else // Magic
                return 1.50f;
        }

        public static float WraithElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 1.00f;
            else if (elementType == DFCareer.Elements.Frost)
                return 0.65f;
            else if (elementType == DFCareer.Elements.Shock)
                return 1.00f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 0.65f;
            else // Magic
                return 1.50f;
        }

        public static float VampireElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 2.00f;
            else if (elementType == DFCareer.Elements.Frost)
                return 0.65f;
            else if (elementType == DFCareer.Elements.Shock)
                return 0.65f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 0.65f;
            else // Magic
                return 1.00f;
        }

        public static float VampireAncientElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 2.00f;
            else if (elementType == DFCareer.Elements.Frost)
                return 0.65f;
            else if (elementType == DFCareer.Elements.Shock)
                return 0.65f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 0.65f;
            else // Magic
                return 1.00f;
        }

        public static float LichElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 0.65f;
            else if (elementType == DFCareer.Elements.Frost)
                return 1.00f;
            else if (elementType == DFCareer.Elements.Shock)
                return 0.65f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 0.65f;
            else // Magic
                return 0.65f;
        }

        public static float AncientLichElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 0.65f;
            else if (elementType == DFCareer.Elements.Frost)
                return 1.00f;
            else if (elementType == DFCareer.Elements.Shock)
                return 0.65f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 0.65f;
            else // Magic
                return 0.32f;
        }

        public static float FrostDaedraElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 2.00f;
            else if (elementType == DFCareer.Elements.Frost)
                return 0f; // Have to see how I might add restoration as well? But have to see how that works in classic code first as well, probably some flag somewhere.
            else if (elementType == DFCareer.Elements.Shock)
                return 0.65f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 1.00f;
            else // Magic
                return 1.00f;
        }

        public static float FireDaedraElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 0f; // Have to see how I might add restoration as well? But have to see how that works in classic code first as well, probably some flag somewhere.
            else if (elementType == DFCareer.Elements.Frost)
                return 2.00f;
            else if (elementType == DFCareer.Elements.Shock)
                return 0.65f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 1.00f;
            else // Magic
                return 1.00f;
        }

        public static float DaedrothElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 1.00f; // Have to see how I might add restoration as well? But have to see how that works in classic code first as well, probably some flag somewhere.
            else if (elementType == DFCareer.Elements.Frost)
                return 1.50f;
            else if (elementType == DFCareer.Elements.Shock)
                return 0.32f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 1.00f;
            else // Magic
                return 0.65f;
        }

        public static float DaedraSeducerElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 1.00f; // Have to see how I might add restoration as well? But have to see how that works in classic code first as well, probably some flag somewhere.
            else if (elementType == DFCareer.Elements.Frost)
                return 1.50f;
            else if (elementType == DFCareer.Elements.Shock)
                return 1.00f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 1.00f;
            else // Magic
                return 0.65f;
        }

        public static float DaedraLordElementalDamTypeWeaknesses(DFCareer.Elements elementType, DaggerfallEntity target, bool singlePartHit)
        {
            if (elementType == DFCareer.Elements.Fire)
                return 0.65f; // Have to see how I might add restoration as well? But have to see how that works in classic code first as well, probably some flag somewhere.
            else if (elementType == DFCareer.Elements.Frost)
                return 1.00f;
            else if (elementType == DFCareer.Elements.Shock)
                return 0.65f;
            else if (elementType == DFCareer.Elements.DiseaseOrPoison)
                return 0.65f;
            else // Magic
                return 0.65f;
        }

        #endregion

        #region Enemy Stat Hack Edits

        /// <summary>
        /// Custom values assigned to specific enemy entities on the fly for use in combat formula.
        /// </summary>
        public static CustomEnemyStatValues EnemyCustomAttributeCalculator(DaggerfallEntity enemy) // One issue with this method of "altering" the enemy stats, is that if the enemy target gets their stats drained, it won't currently work in this method, not that it matters too much, but still should consider fixing that later on possibly.
        {
            CustomEnemyStatValues values = new CustomEnemyStatValues();
            values.weaponSkillCustom = 30;
            values.critSkillCustom = 30;
            values.dodgeSkillCustom = 0;
            values.strengthCustom = 50;
            values.agilityCustom = 50;
            values.speedCustom = 50;
            values.willpowerCustom = 50;
            values.luckCustom = 50;

            EnemyEntity AITarget = enemy as EnemyEntity;

            if (AITarget.EntityType == EntityTypes.EnemyClass) // Since the enemy classes have dynamic levels, I could add some amount per level they are and see how that works out.
            {
                int classLvl = Mathf.Clamp(enemy.Level, 1, 25); // Use this to determine increases class human enemies attributes and skills get per level.

                switch (AITarget.CareerIndex)
                {
                    case (int)ClassCareers.Mage:
                        values.weaponSkillCustom = (int)Mathf.Round(15 + (classLvl * 1.5f));
                        values.critSkillCustom = (int)Mathf.Round(5 + (classLvl * 0.5f));
                        values.dodgeSkillCustom = (int)Mathf.Round(-25 + (classLvl * 1f));
                        values.strengthCustom = 42;
                        values.agilityCustom = 45;
                        values.speedCustom = (int)Mathf.Round(43 + (classLvl * 1f));
                        values.willpowerCustom = (int)Mathf.Round(65 + (classLvl * 1f));
                        values.luckCustom = (int)Mathf.Round(50 + (classLvl * 0.5f));
                        return values;
                    case (int)ClassCareers.Spellsword:
                        values.weaponSkillCustom = (int)Mathf.Round(40 + (classLvl * 2f));
                        values.critSkillCustom = (int)Mathf.Round(8 + (classLvl * 1f));
                        values.dodgeSkillCustom = (int)Mathf.Round(-35 + (classLvl * 0.5f));
                        values.strengthCustom = 60;
                        values.agilityCustom = (int)Mathf.Round(50 + (classLvl * 1f));
                        values.speedCustom = 45;
                        values.willpowerCustom = (int)Mathf.Round(55 + (classLvl * 1f));
                        values.luckCustom = (int)Mathf.Round(50 + (classLvl * 0.5f));
                        return values;
                    case (int)ClassCareers.Battlemage:
                        values.weaponSkillCustom = (int)Mathf.Round(35 + (classLvl * 2f));
                        values.critSkillCustom = (int)Mathf.Round(8 + (classLvl * 1f));
                        values.dodgeSkillCustom = (int)Mathf.Round(-35 + (classLvl * 0.5f));
                        values.strengthCustom = 50;
                        values.agilityCustom = 50;
                        values.speedCustom = (int)Mathf.Round(50 + (classLvl * 0.5f));
                        values.willpowerCustom = (int)Mathf.Round(55 + (classLvl * 1f));
                        values.luckCustom = (int)Mathf.Round(50 + (classLvl * 1f));
                        return values;
                    case (int)ClassCareers.Sorcerer:
                        values.weaponSkillCustom = (int)Mathf.Round(15 + (classLvl * 1.5f));
                        values.critSkillCustom = (int)Mathf.Round(5 + (classLvl * 0.5f));
                        values.dodgeSkillCustom = (int)Mathf.Round(-25 + (classLvl * 1f));
                        values.strengthCustom = 45;
                        values.agilityCustom = (int)Mathf.Round(50 + (classLvl * 0.5f));
                        values.speedCustom = 45;
                        values.willpowerCustom = (int)Mathf.Round(65 + (classLvl * 1f));
                        values.luckCustom = (int)Mathf.Round(50 + (classLvl * 1f));
                        return values;
                    case (int)ClassCareers.Healer:
                        values.weaponSkillCustom = (int)Mathf.Round(15 + (classLvl * 1.5f));
                        values.critSkillCustom = (int)Mathf.Round(5 + (classLvl * 0.5f));
                        values.dodgeSkillCustom = (int)Mathf.Round(-5 + (classLvl * 1f));
                        values.strengthCustom = 42;
                        values.agilityCustom = 45;
                        values.speedCustom = (int)Mathf.Round(46 + (classLvl * 1f));
                        values.willpowerCustom = (int)Mathf.Round(62 + (classLvl * 1f));
                        values.luckCustom = (int)Mathf.Round(50 + (classLvl * 0.5f));
                        return values;
                    case (int)ClassCareers.Nightblade:
                        values.weaponSkillCustom = (int)Mathf.Round(25 + (classLvl * 2f));
                        values.critSkillCustom = (int)Mathf.Round(15 + (classLvl * 2f));
                        values.dodgeSkillCustom = (int)Mathf.Round(-5 + (classLvl * 0.5f));
                        values.strengthCustom = 45;
                        values.agilityCustom = (int)Mathf.Round(65 + (classLvl * 1f));
                        values.speedCustom = (int)Mathf.Round(50 + (classLvl * 0.5f));
                        values.willpowerCustom = (int)Mathf.Round(60 + (classLvl * 0.5f));
                        values.luckCustom = 45;
                        return values;
                    case (int)ClassCareers.Bard:
                        values.weaponSkillCustom = (int)Mathf.Round(27 + (classLvl * 2f));
                        values.critSkillCustom = (int)Mathf.Round(8 + (classLvl * 1.5f));
                        values.dodgeSkillCustom = (int)Mathf.Round(-35 + (classLvl * 0.5f));
                        values.strengthCustom = (int)Mathf.Round(45 + (classLvl * 0.5f));
                        values.agilityCustom = (int)Mathf.Round(55 + (classLvl * 1f));
                        values.speedCustom = (int)Mathf.Round(50 + (classLvl * 1f));
                        values.willpowerCustom = (int)Mathf.Round(50 + (classLvl * 0.5f));
                        values.luckCustom = 45;
                        return values;
                    case (int)ClassCareers.Burglar:
                        values.weaponSkillCustom = (int)Mathf.Round(25 + (classLvl * 2f));
                        values.critSkillCustom = (int)Mathf.Round(20 + (classLvl * 2f));
                        values.dodgeSkillCustom = (int)Mathf.Round(-15 + (classLvl * 1f));
                        values.strengthCustom = 44;
                        values.agilityCustom = (int)Mathf.Round(62 + (classLvl * 1f));
                        values.speedCustom = (int)Mathf.Round(58 + (classLvl * 1f));
                        values.willpowerCustom = 46;
                        values.luckCustom = 50;
                        return values;
                    case (int)ClassCareers.Rogue:
                        values.weaponSkillCustom = (int)Mathf.Round(32 + (classLvl * 2.5f));
                        values.critSkillCustom = (int)Mathf.Round(25 + (classLvl * 2f));
                        values.dodgeSkillCustom = (int)Mathf.Round(-5 + (classLvl * 0.5f));
                        values.strengthCustom = (int)Mathf.Round(58 + (classLvl * 0.5f));
                        values.agilityCustom = (int)Mathf.Round(62 + (classLvl * 1f));
                        values.speedCustom = 50;
                        values.willpowerCustom = 48;
                        values.luckCustom = 44;
                        return values;
                    case (int)ClassCareers.Acrobat:
                        values.weaponSkillCustom = (int)Mathf.Round(20 + (classLvl * 1.5f));
                        values.critSkillCustom = (int)Mathf.Round(12 + (classLvl * 1.5f));
                        values.dodgeSkillCustom = (int)Mathf.Round(0 + (classLvl * 0.5f));
                        values.strengthCustom = 40;
                        values.agilityCustom = (int)Mathf.Round(65 + (classLvl * 1f));
                        values.speedCustom = (int)Mathf.Round(63 + (classLvl * 1f));
                        values.willpowerCustom = 45;
                        values.luckCustom = (int)Mathf.Round(51 + (classLvl * 1f));
                        return values;
                    case (int)ClassCareers.Thief:
                        values.weaponSkillCustom = (int)Mathf.Round(32 + (classLvl * 2f));
                        values.critSkillCustom = (int)Mathf.Round(20 + (classLvl * 2.5f));
                        values.dodgeSkillCustom = (int)Mathf.Round(-30 + (classLvl * 1f));
                        values.strengthCustom = 45;
                        values.agilityCustom = (int)Mathf.Round(58 + (classLvl * 1f));
                        values.speedCustom = (int)Mathf.Round(58 + (classLvl * 1f));
                        values.willpowerCustom = 45;
                        values.luckCustom = (int)Mathf.Round(50 + (classLvl * 0.5f));
                        return values;
                    case (int)ClassCareers.Assassin:
                        values.weaponSkillCustom = (int)Mathf.Round(32 + (classLvl * 2f));
                        values.critSkillCustom = (int)Mathf.Round(40 + (classLvl * 3f));
                        values.dodgeSkillCustom = (int)Mathf.Round(-28 + (classLvl * 1f));
                        values.strengthCustom = 55;
                        values.agilityCustom = (int)Mathf.Round(60 + (classLvl * 1f));
                        values.speedCustom = (int)Mathf.Round(50 + (classLvl * 0.5f));
                        values.willpowerCustom = 45;
                        values.luckCustom = 48;
                        return values;
                    case (int)ClassCareers.Monk:
                        values.weaponSkillCustom = (int)Mathf.Round(35 + (classLvl * 2f));
                        values.critSkillCustom = (int)Mathf.Round(35 + (classLvl * 2f));
                        values.dodgeSkillCustom = (int)Mathf.Round(-5 + (classLvl * 0.5f));
                        values.strengthCustom = (int)Mathf.Round(50 + (classLvl * 0.5f));
                        values.agilityCustom = (int)Mathf.Round(62 + (classLvl * 1f));
                        values.speedCustom = (int)Mathf.Round(58 + (classLvl * 1f));
                        values.willpowerCustom = 45;
                        values.luckCustom = 50;
                        return values;
                    case (int)ClassCareers.Archer:
                        values.weaponSkillCustom = (int)Mathf.Round(35 + (classLvl * 2f));
                        values.critSkillCustom = (int)Mathf.Round(20 + (classLvl * 2f));
                        values.dodgeSkillCustom = (int)Mathf.Round(-5 + (classLvl * 0.5f));
                        values.strengthCustom = (int)Mathf.Round(55 + (classLvl * 1f));
                        values.agilityCustom = (int)Mathf.Round(60 + (classLvl * 1f));
                        values.speedCustom = (int)Mathf.Round(53 + (classLvl * 1f));
                        values.willpowerCustom = 45;
                        values.luckCustom = (int)Mathf.Round(50 + (classLvl * 0.5f));
                        return values;
                    case (int)ClassCareers.Ranger:
                        values.weaponSkillCustom = (int)Mathf.Round(35 + (classLvl * 2f));
                        values.critSkillCustom = (int)Mathf.Round(20 + (classLvl * 2f));
                        values.dodgeSkillCustom = (int)Mathf.Round(-30 + (classLvl * 1f));
                        values.strengthCustom = (int)Mathf.Round(60 + (classLvl * 1f));
                        values.agilityCustom = (int)Mathf.Round(55 + (classLvl * 1f));
                        values.speedCustom = (int)Mathf.Round(47 + (classLvl * 0.5f));
                        values.willpowerCustom = (int)Mathf.Round(48 + (classLvl * 0.5f));
                        values.luckCustom = (int)Mathf.Round(50 + (classLvl * 0.5f));
                        return values;
                    case (int)ClassCareers.Barbarian:
                        values.weaponSkillCustom = (int)Mathf.Round(40 + (classLvl * 2.5f));
                        values.critSkillCustom = (int)Mathf.Round(15 + (classLvl * 1.5f));
                        values.dodgeSkillCustom = (int)Mathf.Round(-30 + (classLvl * 0.5f));
                        values.strengthCustom = (int)Mathf.Round(65 + (classLvl * 1f));
                        values.agilityCustom = (int)Mathf.Round(50 + (classLvl * 0.5f));
                        values.speedCustom = (int)Mathf.Round(50 + (classLvl * 0.5f));
                        values.willpowerCustom = 45;
                        values.luckCustom = 50;
                        return values;
                    case (int)ClassCareers.Warrior:
                        values.weaponSkillCustom = (int)Mathf.Round(40 + (classLvl * 3f));
                        values.critSkillCustom = (int)Mathf.Round(8 + (classLvl * 2f));
                        values.dodgeSkillCustom = (int)Mathf.Round(-30 + (classLvl * 0.5f));
                        values.strengthCustom = (int)Mathf.Round(60 + (classLvl * 1f));
                        values.agilityCustom = (int)Mathf.Round(57 + (classLvl * 1f));
                        values.speedCustom = 47;
                        values.willpowerCustom = 47;
                        values.luckCustom = (int)Mathf.Round(50 + (classLvl * 0.5f));
                        return values;
                    case (int)ClassCareers.Knight:
                        values.weaponSkillCustom = (int)Mathf.Round(35 + (classLvl * 2.5f));
                        values.critSkillCustom = (int)Mathf.Round(8 + (classLvl * 1.5f));
                        values.dodgeSkillCustom = (int)Mathf.Round(-30 + (classLvl * 1f));
                        values.strengthCustom = (int)Mathf.Round(60 + (classLvl * 1f));
                        values.agilityCustom = (int)Mathf.Round(50 + (classLvl * 0.5f));
                        values.speedCustom = (int)Mathf.Round(45 + (classLvl * 0.5f));
                        values.willpowerCustom = (int)Mathf.Round(48 + (classLvl * 0.5f));
                        values.luckCustom = (int)Mathf.Round(45 + (classLvl * 1f));
                        return values;
                    default:
                        return values;
                }
            }
            else
            {
                switch (AITarget.CareerIndex)
                {
                    case 0:
                        values.weaponSkillCustom = 25;
                        values.critSkillCustom = 5;
                        values.dodgeSkillCustom = -20;
                        values.strengthCustom = 30;
                        values.agilityCustom = 80;
                        values.speedCustom = 55;
                        values.willpowerCustom = 60;
                        values.luckCustom = 40;
                        return values;
                    case 3:
                        values.weaponSkillCustom = 25;
                        values.critSkillCustom = 8;
                        values.dodgeSkillCustom = -10;
                        values.strengthCustom = 25;
                        values.agilityCustom = 70;
                        values.speedCustom = 70;
                        values.willpowerCustom = 50;
                        values.luckCustom = 45;
                        return values;
                    case 4:
                        values.weaponSkillCustom = 45;
                        values.critSkillCustom = 15;
                        values.dodgeSkillCustom = -35;
                        values.strengthCustom = 90;
                        values.agilityCustom = 60;
                        values.speedCustom = 45;
                        values.willpowerCustom = 30;
                        values.luckCustom = 55;
                        return values;
                    case 5:
                        values.weaponSkillCustom = 50;
                        values.critSkillCustom = 20;
                        values.dodgeSkillCustom = -25;
                        values.strengthCustom = 75;
                        values.agilityCustom = 70;
                        values.speedCustom = 65;
                        values.willpowerCustom = 25;
                        values.luckCustom = 60;
                        return values;
                    case 6:
                        values.weaponSkillCustom = 30;
                        values.critSkillCustom = 15;
                        values.dodgeSkillCustom = -20;
                        values.strengthCustom = 40;
                        values.agilityCustom = 80;
                        values.speedCustom = 70;
                        values.willpowerCustom = 35;
                        values.luckCustom = 50;
                        return values;
                    case 11:
                        values.weaponSkillCustom = 40;
                        values.critSkillCustom = 25;
                        values.dodgeSkillCustom = -20;
                        values.strengthCustom = 45;
                        values.agilityCustom = 75;
                        values.speedCustom = 65;
                        values.willpowerCustom = 60;
                        values.luckCustom = 50;
                        return values;
                    case 20:
                        values.weaponSkillCustom = 35;
                        values.critSkillCustom = 20;
                        values.dodgeSkillCustom = -20;
                        values.strengthCustom = 70;
                        values.agilityCustom = 70;
                        values.speedCustom = 60;
                        values.willpowerCustom = 25;
                        values.luckCustom = 40;
                        return values;
                    case 1:
                        values.weaponSkillCustom = 25;
                        values.critSkillCustom = 10;
                        values.dodgeSkillCustom = -15;
                        values.strengthCustom = 15;
                        values.agilityCustom = 75;
                        values.speedCustom = 70;
                        values.willpowerCustom = 80;
                        values.luckCustom = 65;
                        return values;
                    case 2:
                        values.weaponSkillCustom = 35;
                        values.critSkillCustom = 15;
                        values.dodgeSkillCustom = 10;
                        values.strengthCustom = 90;
                        values.agilityCustom = 40;
                        values.speedCustom = 30;
                        values.willpowerCustom = 40;
                        values.luckCustom = 70;
                        return values;
                    case 8:
                        values.weaponSkillCustom = 55;
                        values.critSkillCustom = 30;
                        values.dodgeSkillCustom = -30;
                        values.strengthCustom = 80;
                        values.agilityCustom = 55;
                        values.speedCustom = 75;
                        values.willpowerCustom = 40;
                        values.luckCustom = 55;
                        return values;
                    case 10:
                        values.weaponSkillCustom = 30;
                        values.critSkillCustom = 8;
                        values.dodgeSkillCustom = 5;
                        values.strengthCustom = 30;
                        values.agilityCustom = 85;
                        values.speedCustom = 60;
                        values.willpowerCustom = 60;
                        values.luckCustom = 80;
                        return values;
                    case 13:
                        values.weaponSkillCustom = 40;
                        values.critSkillCustom = 20;
                        values.dodgeSkillCustom = -15;
                        values.strengthCustom = 65;
                        values.agilityCustom = 80;
                        values.speedCustom = 65;
                        values.willpowerCustom = 40;
                        values.luckCustom = 30;
                        return values;
                    case 16:
                        values.weaponSkillCustom = 60;
                        values.critSkillCustom = 25;
                        values.dodgeSkillCustom = -35;
                        values.strengthCustom = 120;
                        values.agilityCustom = 20;
                        values.speedCustom = 35;
                        values.willpowerCustom = 50;
                        values.luckCustom = 50;
                        return values;
                    case 22:
                        values.weaponSkillCustom = 70;
                        values.critSkillCustom = 40;
                        values.dodgeSkillCustom = -15;
                        values.strengthCustom = 90;
                        values.agilityCustom = 40;
                        values.speedCustom = 40;
                        values.willpowerCustom = 60;
                        values.luckCustom = 40;
                        return values;
                    case 34:
                        values.weaponSkillCustom = 60;
                        values.critSkillCustom = 20;
                        values.dodgeSkillCustom = 0;
                        values.strengthCustom = 40;
                        values.agilityCustom = 80;
                        values.speedCustom = 80;
                        values.willpowerCustom = 70;
                        values.luckCustom = 70;
                        return values;
                    case 40:
                        values.weaponSkillCustom = 85;
                        values.critSkillCustom = 45;
                        values.dodgeSkillCustom = 10;
                        values.strengthCustom = 130;
                        values.agilityCustom = 60;
                        values.speedCustom = 40;
                        values.willpowerCustom = 80;
                        values.luckCustom = 90;
                        return values;
                    case 41:
                        values.weaponSkillCustom = 60;
                        values.critSkillCustom = 35;
                        values.dodgeSkillCustom = -20;
                        values.strengthCustom = 75;
                        values.agilityCustom = 85;
                        values.speedCustom = 60;
                        values.willpowerCustom = 55;
                        values.luckCustom = 45;
                        return values;
                    case 42:
                        values.weaponSkillCustom = 60;
                        values.critSkillCustom = 45;
                        values.dodgeSkillCustom = -10;
                        values.strengthCustom = 55;
                        values.agilityCustom = 90;
                        values.speedCustom = 65;
                        values.willpowerCustom = 65;
                        values.luckCustom = 70;
                        return values;
                    case 7:
                        values.weaponSkillCustom = 50;
                        values.critSkillCustom = 25;
                        values.dodgeSkillCustom = -35;
                        values.strengthCustom = 85;
                        values.agilityCustom = 45;
                        values.speedCustom = 45;
                        values.willpowerCustom = 65;
                        values.luckCustom = 50;
                        return values;
                    case 12:
                        values.weaponSkillCustom = 65;
                        values.critSkillCustom = 40;
                        values.dodgeSkillCustom = -25;
                        values.strengthCustom = 90;
                        values.agilityCustom = 50;
                        values.speedCustom = 50;
                        values.willpowerCustom = 75;
                        values.luckCustom = 55;
                        return values;
                    case 21:
                        values.weaponSkillCustom = 55;
                        values.critSkillCustom = 30;
                        values.dodgeSkillCustom = -15;
                        values.strengthCustom = 70;
                        values.agilityCustom = 60;
                        values.speedCustom = 60;
                        values.willpowerCustom = 85;
                        values.luckCustom = 70;
                        return values;
                    case 24:
                        values.weaponSkillCustom = 85;
                        values.critSkillCustom = 60;
                        values.dodgeSkillCustom = -10;
                        values.strengthCustom = 105;
                        values.agilityCustom = 65;
                        values.speedCustom = 55;
                        values.willpowerCustom = 80;
                        values.luckCustom = 65;
                        return values;
                    case 9:
                        values.weaponSkillCustom = 55;
                        values.critSkillCustom = 45;
                        values.dodgeSkillCustom = -15;
                        values.strengthCustom = 75;
                        values.agilityCustom = 85;
                        values.speedCustom = 85;
                        values.willpowerCustom = 40;
                        values.luckCustom = 30;
                        return values;
                    case 14:
                        values.weaponSkillCustom = 60;
                        values.critSkillCustom = 40;
                        values.dodgeSkillCustom = -20;
                        values.strengthCustom = 90;
                        values.agilityCustom = 75;
                        values.speedCustom = 75;
                        values.willpowerCustom = 60;
                        values.luckCustom = 35;
                        return values;
                    case 35:
                        values.weaponSkillCustom = 75;
                        values.critSkillCustom = 55;
                        values.dodgeSkillCustom = -15;
                        values.strengthCustom = 80;
                        values.agilityCustom = 70;
                        values.speedCustom = 70;
                        values.willpowerCustom = 70;
                        values.luckCustom = 50;
                        return values;
                    case 36:
                        values.weaponSkillCustom = 65;
                        values.critSkillCustom = 35;
                        values.dodgeSkillCustom = -25;
                        values.strengthCustom = 115;
                        values.agilityCustom = 55;
                        values.speedCustom = 40;
                        values.willpowerCustom = 60;
                        values.luckCustom = 60;
                        return values;
                    case 37:
                        values.weaponSkillCustom = 60;
                        values.critSkillCustom = 30;
                        values.dodgeSkillCustom = -35;
                        values.strengthCustom = 95;
                        values.agilityCustom = 65;
                        values.speedCustom = 65;
                        values.willpowerCustom = 80;
                        values.luckCustom = 50;
                        return values;
                    case 38:
                        values.weaponSkillCustom = 70;
                        values.critSkillCustom = 45;
                        values.dodgeSkillCustom = -20;
                        values.strengthCustom = 85;
                        values.agilityCustom = 80;
                        values.speedCustom = 60;
                        values.willpowerCustom = 6;
                        values.luckCustom = 60;
                        return values;
                    case 15:
                        values.weaponSkillCustom = 50;
                        values.critSkillCustom = 20;
                        values.dodgeSkillCustom = -20;
                        values.strengthCustom = 40;
                        values.agilityCustom = 60;
                        values.speedCustom = 60;
                        values.willpowerCustom = 35;
                        values.luckCustom = 40;
                        return values;
                    case 17:
                        values.weaponSkillCustom = 35;
                        values.critSkillCustom = 15;
                        values.dodgeSkillCustom = -40;
                        values.strengthCustom = 80;
                        values.agilityCustom = 30;
                        values.speedCustom = 35;
                        values.willpowerCustom = 75;
                        values.luckCustom = 40;
                        return values;
                    case 18:
                        values.weaponSkillCustom = 55;
                        values.critSkillCustom = 30;
                        values.dodgeSkillCustom = -5;
                        values.strengthCustom = 30;
                        values.agilityCustom = 60;
                        values.speedCustom = 70;
                        values.willpowerCustom = 40;
                        values.luckCustom = 35;
                        return values;
                    case 19:
                        values.weaponSkillCustom = 60;
                        values.critSkillCustom = 35;
                        values.dodgeSkillCustom = -15;
                        values.strengthCustom = 85;
                        values.agilityCustom = 60;
                        values.speedCustom = 60;
                        values.willpowerCustom = 65;
                        values.luckCustom = 65;
                        return values;
                    case 23:
                        values.weaponSkillCustom = 70;
                        values.critSkillCustom = 50;
                        values.dodgeSkillCustom = 0;
                        values.strengthCustom = 40;
                        values.agilityCustom = 65;
                        values.speedCustom = 65;
                        values.willpowerCustom = 40;
                        values.luckCustom = 25;
                        return values;
                    case 28:
                        values.weaponSkillCustom = 80;
                        values.critSkillCustom = 45;
                        values.dodgeSkillCustom = 15;
                        values.strengthCustom = 80;
                        values.agilityCustom = 80;
                        values.speedCustom = 70;
                        values.willpowerCustom = 60;
                        values.luckCustom = 30;
                        return values;
                    case 30:
                        values.weaponSkillCustom = 90;
                        values.critSkillCustom = 60;
                        values.dodgeSkillCustom = 35;
                        values.strengthCustom = 90;
                        values.agilityCustom = 85;
                        values.speedCustom = 80;
                        values.willpowerCustom = 70;
                        values.luckCustom = 40;
                        return values;
                    case 32:
                        values.weaponSkillCustom = 70;
                        values.critSkillCustom = 35;
                        values.dodgeSkillCustom = 10;
                        values.strengthCustom = 45;
                        values.agilityCustom = 65;
                        values.speedCustom = 60;
                        values.willpowerCustom = 85;
                        values.luckCustom = 35;
                        return values;
                    case 33:
                        values.weaponSkillCustom = 80;
                        values.critSkillCustom = 40;
                        values.dodgeSkillCustom = 20;
                        values.strengthCustom = 50;
                        values.agilityCustom = 65;
                        values.speedCustom = 65;
                        values.willpowerCustom = 95;
                        values.luckCustom = 45;
                        return values;
                    case 25:
                        values.weaponSkillCustom = 85;
                        values.critSkillCustom = 65;
                        values.dodgeSkillCustom = 30;
                        values.strengthCustom = 120;
                        values.agilityCustom = 80;
                        values.speedCustom = 70;
                        values.willpowerCustom = 80;
                        values.luckCustom = 60;
                        return values;
                    case 26:
                        values.weaponSkillCustom = 95;
                        values.critSkillCustom = 75;
                        values.dodgeSkillCustom = 15;
                        values.strengthCustom = 110;
                        values.agilityCustom = 90;
                        values.speedCustom = 80;
                        values.willpowerCustom = 60;
                        values.luckCustom = 60;
                        return values;
                    case 27:
                        values.weaponSkillCustom = 90;
                        values.critSkillCustom = 70;
                        values.dodgeSkillCustom = 5;
                        values.strengthCustom = 130;
                        values.agilityCustom = 90;
                        values.speedCustom = 70;
                        values.willpowerCustom = 90;
                        values.luckCustom = 65;
                        return values;
                    case 29:
                        values.weaponSkillCustom = 80;
                        values.critSkillCustom = 75;
                        values.dodgeSkillCustom = 40;
                        values.strengthCustom = 75;
                        values.agilityCustom = 110;
                        values.speedCustom = 85;
                        values.willpowerCustom = 85;
                        values.luckCustom = 85;
                        return values;
                    case 31:
                        values.weaponSkillCustom = 110;
                        values.critSkillCustom = 90;
                        values.dodgeSkillCustom = 35;
                        values.strengthCustom = 130;
                        values.agilityCustom = 90;
                        values.speedCustom = 80;
                        values.willpowerCustom = 80;
                        values.luckCustom = 75;
                        return values;
                    default:
                        return values;
                }
            }
        }

        /// <summary>
        /// This is literally there just to initialize this struct in another method, because I can't figure out how to do what I want with it otherwise, hopefully it works out.
        /// </summary>
        public static CustomEnemyStatValues EnemyCustomAttributeInitializer(DaggerfallEntity enemy)
        {
            CustomEnemyStatValues values = new CustomEnemyStatValues();
            values.weaponSkillCustom = 30;
            values.critSkillCustom = 30;
            values.dodgeSkillCustom = 0;
            values.strengthCustom = 50;
            values.agilityCustom = 50;
            values.speedCustom = 50;
            values.willpowerCustom = 50;
            values.luckCustom = 50;
            return values;
        }

        #endregion

        #region Enemy Loot Table Related Methods

        public static int[] ArmorSlotEquipCalculator(DaggerfallEntity enemy, int[] traits, int[] equipTableProps)
        {
            if (equipTableProps[3] == -1)
                return equipTableProps;
            else if (equipTableProps[3] == 0) // Index 6 = Helm, 7 = Right Pauld, 8 = Left Pauld, 9 = Chest, 10 = Gloves, 11 = Legs, 12 = Boots.
            {
                for (int i = 6; i < equipTableProps.Length; i++)
                {
                    equipTableProps[i] = RollArmorTypeUsedBasedOnClass(enemy);
                }
            }
            else if (equipTableProps[3] == 1)
            {
                equipTableProps[6] = RollArmorTypeUsedBasedOnClass(enemy);
                equipTableProps[10] = RollArmorTypeUsedBasedOnClass(enemy);
                equipTableProps[12] = RollArmorTypeUsedBasedOnClass(enemy);
            }
            else if (equipTableProps[3] == 2)
            {
                equipTableProps[7] = RollArmorTypeUsedBasedOnClass(enemy);
                equipTableProps[8] = RollArmorTypeUsedBasedOnClass(enemy);
                equipTableProps[9] = RollArmorTypeUsedBasedOnClass(enemy);
                equipTableProps[11] = RollArmorTypeUsedBasedOnClass(enemy);
            }
            else if (equipTableProps[3] == 3)
            {
                equipTableProps[9] = RollArmorTypeUsedBasedOnClass(enemy);
                equipTableProps[10] = RollArmorTypeUsedBasedOnClass(enemy);
                equipTableProps[11] = RollArmorTypeUsedBasedOnClass(enemy);
            }
            else if (equipTableProps[3] == 4)
            {
                equipTableProps[7] = RollArmorTypeUsedBasedOnClass(enemy);
                equipTableProps[8] = RollArmorTypeUsedBasedOnClass(enemy);
                equipTableProps[9] = RollArmorTypeUsedBasedOnClass(enemy);
            }
            else if (equipTableProps[3] == 5)
            {
                equipTableProps[6] = RollArmorTypeUsedBasedOnClass(enemy);
                equipTableProps[10] = RollArmorTypeUsedBasedOnClass(enemy);
                equipTableProps[11] = RollArmorTypeUsedBasedOnClass(enemy);
                equipTableProps[12] = RollArmorTypeUsedBasedOnClass(enemy);
            }
            else if (equipTableProps[3] == 6)
            {
                equipTableProps[6] = RollArmorTypeUsedBasedOnClass(enemy);
                equipTableProps[9] = RollArmorTypeUsedBasedOnClass(enemy);
                equipTableProps[10] = RollArmorTypeUsedBasedOnClass(enemy);
            }
            else if (equipTableProps[3] == 7)
            {
                equipTableProps[7] = RollArmorTypeUsedBasedOnClass(enemy);
                equipTableProps[8] = RollArmorTypeUsedBasedOnClass(enemy);
                equipTableProps[10] = RollArmorTypeUsedBasedOnClass(enemy);
            }
            else if (equipTableProps[3] == 8)
            {
                equipTableProps[6] = RollArmorTypeUsedBasedOnClass(enemy);
                equipTableProps[7] = RollArmorTypeUsedBasedOnClass(enemy);
                equipTableProps[8] = RollArmorTypeUsedBasedOnClass(enemy);
                equipTableProps[11] = RollArmorTypeUsedBasedOnClass(enemy);
            }

            return equipTableProps;
        }

        public static int RollArmorTypeUsedBasedOnClass(DaggerfallEntity enemy)
        {
            EnemyEntity AITarget = enemy as EnemyEntity;

            if (AITarget.EntityType == EntityTypes.EnemyClass) // Since the enemy classes have dynamic levels, I could add some amount per level they are and see how that works out.
            {
                switch (AITarget.CareerIndex)
                {
                    case (int)ClassCareers.Mage:
                    case (int)ClassCareers.Nightblade:
                    case (int)ClassCareers.Burglar:
                    case (int)ClassCareers.Acrobat:
                    case (int)ClassCareers.Thief:
                    case (int)ClassCareers.Monk:
                    case (int)ClassCareers.Barbarian:
                        return 0;
                    case (int)ClassCareers.Spellsword:
                    case (int)ClassCareers.Assassin:
                        return FormulaHelper.PickOneOf(0, 1);
                    case (int)ClassCareers.Battlemage:
                        return FormulaHelper.PickOneOf(1, 2, 2);
                    case (int)ClassCareers.Sorcerer:
                    case (int)ClassCareers.Healer:
                        return FormulaHelper.PickOneOf(0, 0, 1);
                    case (int)ClassCareers.Bard:
                        return FormulaHelper.PickOneOf(0, 1, 1, 2);
                    case (int)ClassCareers.Rogue:
                        return FormulaHelper.PickOneOf(0, 0, 0, 1, 1, 2);
                    case (int)ClassCareers.Archer:
                    case (int)ClassCareers.Ranger:
                        return FormulaHelper.PickOneOf(0, 0, 1, 2);
                    case (int)ClassCareers.Warrior:
                    case (int)ClassCareers.Knight:
                        return FormulaHelper.PickOneOf(0, 1, 1, 2, 2, 2);
                    default:
                        return -1;
                }
            }
            else
            {
                switch (AITarget.CareerIndex)
                {
                    case 7:
                        return FormulaHelper.PickOneOf(0, 1, 2, 2);
                    case 8:
                        return FormulaHelper.PickOneOf(0, 0, 1, 2);
                    case 12:
                        return FormulaHelper.PickOneOf(0, 1, 2, 2, 2);
                    case 15:
                    case 17:
                        return FormulaHelper.PickOneOf(0, 0, 0, 1, 2);
                    case 21:
                        return 0;
                    case 24:
                        return FormulaHelper.PickOneOf(0, 1, 2, 2, 2, 2);
                    case 25:
                    case 26:
                    case 27:
                    case 31:
                        return FormulaHelper.PickOneOf(1, 2, 2, 2);
                    case 28:
                    case 30:
                        return FormulaHelper.PickOneOf(0, 0, 1);
                    default:
                        return -1;
                }
            }
        }

        public static int[] TraitEquipModCalculator(DaggerfallEntity enemy, int[] traits, int[] equipTableProps)
        {
            if (traits[0] == -1 && traits[1] == -1 && traits[2] == -1)
                return equipTableProps;

            if (traits[0] == (int)MobilePersonalityQuirks.Prepared || traits[1] == (int)MobilePersonalityQuirks.Prepared)
            {
                equipTableProps[13] = (int)Mathf.Clamp(equipTableProps[13] + 15, 1, 95);
                equipTableProps[14] = (int)Mathf.Clamp(equipTableProps[14] + 15, 1, 95);
            }

            if (traits[0] == (int)MobilePersonalityQuirks.Reckless || traits[1] == (int)MobilePersonalityQuirks.Reckless)
            {
                equipTableProps[13] = (int)Mathf.Clamp(equipTableProps[13] - 15, 1, 95);
                equipTableProps[14] = (int)Mathf.Clamp(equipTableProps[14] - 15, 1, 95);
            }

            if (traits[0] == (int)MobilePersonalityQuirks.Cautious || traits[1] == (int)MobilePersonalityQuirks.Cautious)
            {
                for (int i = 7; i < equipTableProps.Length; i++)
                {
                    if (equipTableProps[i] > -1 && equipTableProps[i] < 2)
                        equipTableProps[i] += 1;
                }

                equipTableProps[13] = (int)Mathf.Clamp(equipTableProps[13] + 5, 1, 95);
                equipTableProps[14] = (int)Mathf.Clamp(equipTableProps[14] + 5, 1, 95);
            }

            if (traits[0] == (int)MobilePersonalityQuirks.Cowardly || traits[1] == (int)MobilePersonalityQuirks.Cowardly)
            {
                for (int i = 6; i < equipTableProps.Length; i++)
                {
                    equipTableProps[i] = 2;
                }
            }

            if (traits[0] == (int)MobilePersonalityQuirks.Hoarder || traits[1] == (int)MobilePersonalityQuirks.Hoarder)
            {
                equipTableProps[13] = (int)Mathf.Clamp(equipTableProps[13] - 10, 1, 95);
                equipTableProps[14] = (int)Mathf.Clamp(equipTableProps[14] - 10, 1, 95);
            }

            if (traits[0] == (int)MobilePersonalityQuirks.Sadistic || traits[1] == (int)MobilePersonalityQuirks.Sadistic)
            {
                if (equipTableProps[4] == 0)
                {
                    equipTableProps[4] = FormulaHelper.PickOneOf(0, 0, 1);
                    equipTableProps[5] = FormulaHelper.PickOneOf((int)Poisons.Nux_Vomica, (int)Poisons.Nux_Vomica, (int)Poisons.Moonseed, (int)Poisons.Moonseed, (int)Poisons.Pyrrhic_Acid);
                }
            }

            if (traits[2] == (int)MobilePersonalityInterests.Collector)
            {
                equipTableProps[13] = (int)Mathf.Clamp(equipTableProps[13] + 5, 1, 95);
                equipTableProps[14] = (int)Mathf.Clamp(equipTableProps[14] + 5, 1, 95);
            }

            if (traits[2] == (int)MobilePersonalityInterests.Survivalist)
            {
                equipTableProps[13] = (int)Mathf.Clamp(equipTableProps[13] + 10, 1, 95);
                equipTableProps[14] = (int)Mathf.Clamp(equipTableProps[14] + 10, 1, 95);
            }

            if (traits[2] == (int)MobilePersonalityInterests.Diver)
            {
                equipTableProps[13] = (int)Mathf.Clamp(equipTableProps[13] - 5, 1, 95);
                equipTableProps[14] = (int)Mathf.Clamp(equipTableProps[14] - 5, 1, 95);
            }

            if (traits[2] == (int)MobilePersonalityInterests.Handy)
            {
                equipTableProps[13] = (int)Mathf.Clamp(equipTableProps[13] + 15, 1, 95);
                equipTableProps[14] = (int)Mathf.Clamp(equipTableProps[14] + 15, 1, 95);
            }

            return equipTableProps;
        }

        public static void TraitExtraLootModCalculator(int[] traits, int[] equipTableProps, int[] extraLootProps, out int[] finalEquipTableProps, out int[] finalExtraLootProps)
        {
            finalEquipTableProps = equipTableProps;
            finalExtraLootProps = extraLootProps;

            if (traits[0] == -1 && traits[1] == -1 && traits[2] == -1)
                return;

            if (traits[0] > -1 || traits[1] > -1)
            {
                if (traits[0] == (int)MobilePersonalityQuirks.Prepared || traits[1] == (int)MobilePersonalityQuirks.Prepared)
                {
                    finalEquipTableProps[0] = (int)Mathf.Ceil(finalEquipTableProps[0] * 1.5f);
                    finalExtraLootProps[0] = (int)Mathf.Ceil(finalExtraLootProps[0] * 1.5f);
                    finalExtraLootProps[1] = (finalExtraLootProps[1] >= 1) ? (int)Mathf.Ceil(finalExtraLootProps[1] * 2) : finalExtraLootProps[1] = 1;
                    finalExtraLootProps[4] = (int)Mathf.Ceil(equipTableProps[4] * 2f);
                    finalExtraLootProps[5] = (int)Mathf.Ceil(equipTableProps[5] * 1.5f);
                    finalExtraLootProps[7] = (finalExtraLootProps[7] >= 1) ? (int)Mathf.Ceil(finalExtraLootProps[7] * 1.5f) : finalExtraLootProps[1] = 1;
                    finalExtraLootProps[8] = (finalExtraLootProps[8] >= 1) ? (int)Mathf.Ceil(finalExtraLootProps[8] * 2) : finalExtraLootProps[1] = 1;
                }

                if (traits[0] == (int)MobilePersonalityQuirks.Reckless || traits[1] == (int)MobilePersonalityQuirks.Reckless)
                {
                    finalEquipTableProps[0] = (int)Mathf.Floor(finalEquipTableProps[0] * 0.75f);
                    finalExtraLootProps[0] = (int)Mathf.Floor(finalExtraLootProps[0] * 0.5f);
                    finalExtraLootProps[1] = (int)Mathf.Floor(finalExtraLootProps[1] * 0.5f);
                    finalExtraLootProps[4] = (int)Mathf.Floor(equipTableProps[4] * 0.75f);
                    finalExtraLootProps[5] = (int)Mathf.Floor(equipTableProps[5] * 0.5f);
                    finalExtraLootProps[7] = (int)Mathf.Floor(equipTableProps[7] * 0.75f);
                    finalExtraLootProps[8] = (int)Mathf.Floor(equipTableProps[8] * 0.75f);
                }

                if (traits[0] == (int)MobilePersonalityQuirks.Cautious || traits[1] == (int)MobilePersonalityQuirks.Cautious)
                {
                    finalEquipTableProps[0] = (int)Mathf.Ceil(finalEquipTableProps[0] * 1.25f);
                    finalExtraLootProps[0] = (int)Mathf.Ceil(finalExtraLootProps[0] * 1.25f);
                    finalExtraLootProps[1] = (finalExtraLootProps[1] >= 1) ? (int)Mathf.Ceil(finalExtraLootProps[1] * 1.5f) : finalExtraLootProps[1] = 1;
                    finalExtraLootProps[4] = (int)Mathf.Ceil(equipTableProps[4] * 1.5f);
                    finalExtraLootProps[5] = (int)Mathf.Ceil(equipTableProps[5] * 2f);
                    finalExtraLootProps[7] = (finalExtraLootProps[7] >= 1) ? (int)Mathf.Ceil(finalExtraLootProps[7] * 2f) : finalExtraLootProps[1] = 1;
                }

                if (traits[0] == (int)MobilePersonalityQuirks.Curious || traits[1] == (int)MobilePersonalityQuirks.Curious)
                {
                    finalEquipTableProps[8] = (finalEquipTableProps[8] >= 1) ? (int)Mathf.Ceil(finalEquipTableProps[8] * 1.5f) : finalEquipTableProps[8] = 1;
                }

                if (traits[0] == (int)MobilePersonalityQuirks.Addict || traits[1] == (int)MobilePersonalityQuirks.Addict)
                {
                    finalEquipTableProps[0] = (int)Mathf.Floor(finalEquipTableProps[0] * 0.35f);
                    finalExtraLootProps[1] = (int)Mathf.Floor(finalExtraLootProps[1] * 0.5f);
                }

                if (traits[0] == (int)MobilePersonalityQuirks.Nyctophobic || traits[1] == (int)MobilePersonalityQuirks.Nyctophobic)
                {
                    finalExtraLootProps[5] = (finalExtraLootProps[5] >= 1) ? (int)Mathf.Ceil(finalExtraLootProps[5] * 3f) : finalExtraLootProps[5] = 1;
                }

                if (traits[0] == (int)MobilePersonalityQuirks.Nyctophilic || traits[1] == (int)MobilePersonalityQuirks.Nyctophilic)
                {
                    finalExtraLootProps[5] = 0;
                }

                if (traits[0] == (int)MobilePersonalityQuirks.Hoarder || traits[1] == (int)MobilePersonalityQuirks.Hoarder)
                {
                    finalEquipTableProps[0] = (int)Mathf.Ceil(finalEquipTableProps[0] * 1.15f);
                }

                if (traits[0] == (int)MobilePersonalityQuirks.Vain || traits[1] == (int)MobilePersonalityQuirks.Vain)
                {
                    finalEquipTableProps[0] = (int)Mathf.Ceil(finalEquipTableProps[0] * 1.65f);
                }

                if (traits[0] == (int)MobilePersonalityQuirks.Greedy || traits[1] == (int)MobilePersonalityQuirks.Greedy)
                {
                    finalEquipTableProps[0] = (int)Mathf.Ceil(finalEquipTableProps[0] * 2.25f);
                }

                if (traits[0] == (int)MobilePersonalityQuirks.Glutton || traits[1] == (int)MobilePersonalityQuirks.Glutton)
                {
                    finalExtraLootProps[4] = (finalExtraLootProps[4] >= 1) ? (int)Mathf.Ceil(finalExtraLootProps[4] * 3f) : finalExtraLootProps[4] = 2;
                }

                if (traits[0] == (int)MobilePersonalityQuirks.Faster || traits[1] == (int)MobilePersonalityQuirks.Faster)
                {
                    finalExtraLootProps[4] = 0;
                }

                if (traits[0] == (int)MobilePersonalityQuirks.Romantic || traits[1] == (int)MobilePersonalityQuirks.Romantic)
                {
                    finalEquipTableProps[2] = (finalEquipTableProps[2] >= 1) ? (int)Mathf.Ceil(finalEquipTableProps[2] * 1.5f) : finalEquipTableProps[2] = 1;
                    finalExtraLootProps[2] = (finalExtraLootProps[2] >= 1) ? (int)Mathf.Ceil(finalExtraLootProps[2] * 1.5f) : finalExtraLootProps[2] = 1;
                }

                if (traits[0] == (int)MobilePersonalityQuirks.Alcoholic || traits[1] == (int)MobilePersonalityQuirks.Alcoholic)
                {
                    finalEquipTableProps[0] = (int)Mathf.Floor(finalEquipTableProps[0] * 0.50f);
                }
            }

            if (traits[2] > -1)
            {
                if (traits[2] == (int)MobilePersonalityInterests.God_Fearing || traits[2] == (int)MobilePersonalityInterests.Occultist)
                {
                    finalExtraLootProps[6] = (finalExtraLootProps[6] >= 1) ? (int)Mathf.Ceil(finalExtraLootProps[6] * 2) : finalExtraLootProps[6] = 1;
                }

                if (traits[2] == (int)MobilePersonalityInterests.Collector)
                {
                    finalEquipTableProps[7] = (finalEquipTableProps[7] >= 1) ? (int)Mathf.Ceil(finalEquipTableProps[7] * 1.5f) : finalEquipTableProps[7] = 1;
                    finalExtraLootProps[2] = (finalExtraLootProps[2] >= 1) ? (int)Mathf.Ceil(finalExtraLootProps[2] * 1.5f) : finalExtraLootProps[2] = 1;
                }

                if (traits[2] == (int)MobilePersonalityInterests.Survivalist)
                {
                    finalEquipTableProps[3] = (finalEquipTableProps[3] >= 1) ? (int)Mathf.Ceil(finalEquipTableProps[3] * 2f) : finalEquipTableProps[3] = 2;
                    finalExtraLootProps[0] = (finalExtraLootProps[0] >= 1) ? (int)Mathf.Ceil(finalExtraLootProps[0] * 2f) : finalExtraLootProps[0] = 6;
                    finalExtraLootProps[10] = (finalExtraLootProps[10] >= 1) ? (int)Mathf.Ceil(finalExtraLootProps[10] * 2f) : finalExtraLootProps[10] = 1;
                }

                if (traits[2] == (int)MobilePersonalityInterests.Hunter)
                {
                    finalExtraLootProps[0] = (finalExtraLootProps[0] >= 1) ? (int)Mathf.Ceil(finalExtraLootProps[0] * 2.5f) : finalExtraLootProps[0] = 12;
                    finalExtraLootProps[4] = (finalExtraLootProps[4] >= 1) ? (int)Mathf.Ceil(finalExtraLootProps[4] * 1.5f) : finalExtraLootProps[4] = 2;
                    finalExtraLootProps[10] = (finalExtraLootProps[10] >= 1) ? (int)Mathf.Ceil(finalExtraLootProps[10] * 2f) : finalExtraLootProps[10] = 1;
                }

                if (traits[2] == (int)MobilePersonalityInterests.Brewer)
                {
                    finalEquipTableProps[1] = (finalEquipTableProps[1] >= 1) ? (int)Mathf.Ceil(finalEquipTableProps[1] * 1.5f) : finalEquipTableProps[1] = 1;
                    finalEquipTableProps[2] = (finalEquipTableProps[2] >= 1) ? (int)Mathf.Ceil(finalEquipTableProps[2] * 1.5f) : finalEquipTableProps[2] = 1;
                    finalEquipTableProps[3] = (finalEquipTableProps[3] >= 1) ? (int)Mathf.Ceil(finalEquipTableProps[3] * 1.5f) : finalEquipTableProps[3] = 1;
                    finalEquipTableProps[6] = (finalEquipTableProps[6] >= 1) ? (int)Mathf.Ceil(finalEquipTableProps[6] * 1.5f) : finalEquipTableProps[6] = 1;
                    finalExtraLootProps[1] = (finalExtraLootProps[1] >= 1) ? (int)Mathf.Ceil(finalExtraLootProps[1] * 1.5f) : finalExtraLootProps[1] = 2;
                }

                if (traits[2] == (int)MobilePersonalityInterests.Anthophile)
                {
                    finalEquipTableProps[1] = (finalEquipTableProps[1] >= 1) ? (int)Mathf.Ceil(finalEquipTableProps[1] * 3f) : finalEquipTableProps[1] = 2;
                    finalEquipTableProps[2] = (finalEquipTableProps[2] >= 1) ? (int)Mathf.Ceil(finalEquipTableProps[2] * 3f) : finalEquipTableProps[2] = 2;
                }

                if (traits[2] == (int)MobilePersonalityInterests.Bookworm)
                {
                    finalEquipTableProps[8] = (finalEquipTableProps[8] >= 1) ? (int)Mathf.Ceil(finalEquipTableProps[8] * 2f) : finalEquipTableProps[8] = 2;
                }

                if (traits[2] == (int)MobilePersonalityInterests.Cartographer)
                {
                    finalExtraLootProps[11] = FormulaHelper.PickOneOfCompact(1, 98, 2, 12, 3, 1);
                }

                if (traits[2] == (int)MobilePersonalityInterests.Fisher)
                {
                    finalEquipTableProps[4] = (finalEquipTableProps[4] >= 1) ? (int)Mathf.Ceil(finalEquipTableProps[4] * 2f) : finalEquipTableProps[4] = 2;
                    finalExtraLootProps[4] = (finalExtraLootProps[4] >= 1) ? (int)Mathf.Ceil(finalExtraLootProps[4] * 1.5f) : finalExtraLootProps[4] = 1;
                }

                if (traits[2] == (int)MobilePersonalityInterests.Diver)
                {
                    finalEquipTableProps[4] = (finalEquipTableProps[4] >= 1) ? (int)Mathf.Ceil(finalEquipTableProps[4] * 2.5f) : finalEquipTableProps[4] = 3;
                }

                if (traits[2] == (int)MobilePersonalityInterests.Magic_Collector)
                {
                    finalExtraLootProps[3] = FormulaHelper.PickOneOfCompact(1, 98, 2, 10, 3, 1);
                }

                if (traits[2] == (int)MobilePersonalityInterests.Writer)
                {
                    finalEquipTableProps[8] = (finalEquipTableProps[8] >= 1) ? (int)Mathf.Ceil(finalEquipTableProps[8] * 1.5f) : finalEquipTableProps[8] = 1;
                }

                if (traits[2] == (int)MobilePersonalityInterests.Handy)
                {
                    finalExtraLootProps[8] = (finalExtraLootProps[8] >= 1) ? (int)Mathf.Ceil(finalExtraLootProps[8] * 2f) : finalExtraLootProps[8] = 2;
                }
            }

            return;
        }

        public static int[] EnemyEquipTableCalculator(DaggerfallEntity enemy, int[] traits)
        {
            // Index meanings: 0 = primaryWeapon, 1 = secondaryWeapon, 2 = shield, 3 = armorCoverage, 4 = usesPoison 0 or 1 bool, 5 = poisonApplied, 6-12 = armorSlots -1-2 Nothing to Plate armor, 13 = minCond%, 14 = maxCond%.
            // Main variable used in determining enemy loot/equipment values for purposes of targeted loot generation based on many context related variables.
            int[] equipTableProps = { -1, -1, -1, -1, 0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };

            EnemyEntity AITarget = enemy as EnemyEntity;

            if (EnemyEntity.EquipmentUser(AITarget))
            {
                if (AITarget.EntityType == EntityTypes.EnemyClass) // Since the enemy classes have dynamic levels, I could add some amount per level they are and see how that works out.
                {
                    switch (AITarget.CareerIndex)
                    {
                        case (int)ClassCareers.Mage:
                            equipTableProps[0] = (int)Weapons.Staff;
                            equipTableProps[1] = FormulaHelper.PickOneOfCompact(-1, 3, (int)Weapons.Dagger, 1, (int)Weapons.Tanto, 1, (int)Weapons.Shortsword, 1);
                            equipTableProps[2] = FormulaHelper.PickOneOfCompact(-1, 4, (int)Armor.Buckler, 1);
                            equipTableProps[3] = FormulaHelper.PickOneOfCompact(-1, 7, 0, 1, 1, 2, 2, 2, 3, 2, 4, 2, 5, 2, 6, 2, 7, 2, 8, 2);
                            equipTableProps = ArmorSlotEquipCalculator(enemy, traits, equipTableProps);
                            equipTableProps[13] = 20;
                            equipTableProps[14] = 60;
                            break;
                        case (int)ClassCareers.Spellsword:
                            equipTableProps[0] = FormulaHelper.PickOneOf((int)Weapons.Battle_Axe, (int)Weapons.Broadsword, (int)Weapons.Katana, (int)Weapons.Longsword, (int)Weapons.Saber, (int)Weapons.Mace);
                            equipTableProps[1] = FormulaHelper.PickOneOfCompact(-1, 3, (int)Weapons.Dagger, 1, (int)Weapons.Wakazashi, 1, (int)Weapons.Shortsword, 1, (int)Weapons.Short_Bow, 1);
                            equipTableProps[2] = FormulaHelper.PickOneOfCompact(-1, 5, (int)Armor.Buckler, 1, (int)Armor.Round_Shield, 1, (int)Armor.Kite_Shield, 1);
                            equipTableProps[3] = FormulaHelper.PickOneOfCompact(0, 1, 1, 2, 2, 2, 3, 2, 4, 2, 5, 2, 6, 2, 7, 2, 8, 2);
                            equipTableProps = ArmorSlotEquipCalculator(enemy, traits, equipTableProps);
                            equipTableProps[13] = 30;
                            equipTableProps[14] = 70;
                            break;
                        case (int)ClassCareers.Battlemage:
                            equipTableProps[0] = FormulaHelper.PickOneOf((int)Weapons.Battle_Axe, (int)Weapons.War_Axe, (int)Weapons.Broadsword, (int)Weapons.Saber, (int)Weapons.Longsword, (int)Weapons.Katana, (int)Weapons.Claymore, (int)Weapons.Dai_Katana);
                            equipTableProps[1] = FormulaHelper.PickOneOf((int)Weapons.Dagger, (int)Weapons.Wakazashi, (int)Weapons.Shortsword, (int)Weapons.Flail, (int)Weapons.Mace, (int)Weapons.Staff, (int)Weapons.Warhammer, (int)Weapons.Short_Bow, (int)Weapons.Long_Bow);
                            equipTableProps[2] = FormulaHelper.PickOneOfCompact(-1, 3, (int)Armor.Buckler, 1, (int)Armor.Round_Shield, 1);
                            equipTableProps[3] = FormulaHelper.PickOneOfCompact(0, 1, 1, 2, 2, 2, 3, 2, 4, 2, 5, 2, 6, 2, 7, 2, 8, 2);
                            equipTableProps = ArmorSlotEquipCalculator(enemy, traits, equipTableProps);
                            equipTableProps[13] = 30;
                            equipTableProps[14] = 70;
                            break;
                        case (int)ClassCareers.Sorcerer:
                            equipTableProps[0] = FormulaHelper.PickOneOf((int)Weapons.Dagger, (int)Weapons.Tanto, (int)Weapons.Wakazashi, (int)Weapons.Shortsword, (int)Weapons.Mace, (int)Weapons.Flail, (int)Weapons.Warhammer);
                            equipTableProps[1] = FormulaHelper.PickOneOfCompact(-1, 5, (int)Weapons.Dagger, 1, (int)Weapons.Wakazashi, 1, (int)Weapons.Shortsword, 1, (int)Weapons.Tanto, 1, (int)Weapons.Mace, 1);
                            equipTableProps[3] = FormulaHelper.PickOneOfCompact(-1, 5, 0, 1, 1, 2, 2, 2, 3, 2, 4, 2, 5, 2, 6, 2, 7, 2, 8, 2);
                            equipTableProps = ArmorSlotEquipCalculator(enemy, traits, equipTableProps);
                            equipTableProps[13] = 20;
                            equipTableProps[14] = 60;
                            break;
                        case (int)ClassCareers.Healer:
                            equipTableProps[0] = (int)Weapons.Staff;
                            equipTableProps[1] = FormulaHelper.PickOneOfCompact(-1, 5, (int)Weapons.Dagger, 1, (int)Weapons.Tanto, 1, (int)Weapons.Shortsword, 1);
                            equipTableProps[2] = FormulaHelper.PickOneOfCompact(-1, 3, (int)Armor.Buckler, 1, (int)Armor.Round_Shield, 1);
                            equipTableProps[3] = FormulaHelper.PickOneOfCompact(-1, 4, 0, 1, 1, 2, 2, 2, 3, 2, 4, 2, 5, 2, 6, 2, 7, 2, 8, 2);
                            equipTableProps = ArmorSlotEquipCalculator(enemy, traits, equipTableProps);
                            equipTableProps[13] = 25;
                            equipTableProps[14] = 65;
                            break;
                        case (int)ClassCareers.Nightblade:
                            equipTableProps[0] = FormulaHelper.PickOneOf((int)Weapons.Dagger, (int)Weapons.Tanto, (int)Weapons.Shortsword, (int)Weapons.Wakazashi);
                            equipTableProps[1] = FormulaHelper.PickOneOf((int)Weapons.Dagger, (int)Weapons.Tanto, (int)Weapons.Shortsword, (int)Weapons.Wakazashi);
                            equipTableProps[2] = FormulaHelper.PickOneOfCompact(-1, 3, (int)Armor.Buckler, 1);
                            equipTableProps[3] = 0;
                            equipTableProps[4] = FormulaHelper.PickOneOfCompact(0, 1, 1, 2);
                            equipTableProps[5] = UnityEngine.Random.Range(128, 135 + 1);
                            equipTableProps = ArmorSlotEquipCalculator(enemy, traits, equipTableProps);
                            equipTableProps[13] = 40;
                            equipTableProps[14] = 80;
                            break;
                        case (int)ClassCareers.Bard:
                            equipTableProps[0] = FormulaHelper.PickOneOf((int)Weapons.Dagger, (int)Weapons.Tanto, (int)Weapons.Shortsword, (int)Weapons.Wakazashi);
                            equipTableProps[1] = FormulaHelper.PickOneOfCompact(-1, 3, (int)Weapons.Dagger, 1, (int)Weapons.Tanto, 1, (int)Weapons.Shortsword, 1, (int)Weapons.Wakazashi, 1, (int)Weapons.Short_Bow, 1);
                            equipTableProps[2] = FormulaHelper.PickOneOfCompact(-1, 4, (int)Armor.Buckler, 1, (int)Armor.Round_Shield, 1, (int)Armor.Kite_Shield, 1);
                            equipTableProps[3] = FormulaHelper.PickOneOfCompact(-1, 3, 0, 1, 1, 2, 2, 2, 3, 2, 4, 2, 5, 2, 6, 2, 7, 2, 8, 2);
                            equipTableProps = ArmorSlotEquipCalculator(enemy, traits, equipTableProps);
                            equipTableProps[13] = 30;
                            equipTableProps[14] = 70;
                            break;
                        case (int)ClassCareers.Burglar:
                            equipTableProps[0] = FormulaHelper.PickOneOf((int)Weapons.Dagger, (int)Weapons.Tanto, (int)Weapons.Shortsword, (int)Weapons.Wakazashi);
                            equipTableProps[1] = FormulaHelper.PickOneOfCompact(-1, 3, (int)Weapons.Dagger, 1, (int)Weapons.Tanto, 1, (int)Weapons.Shortsword, 1, (int)Weapons.Wakazashi, 1);
                            equipTableProps[3] = FormulaHelper.PickOneOfCompact(-1, 3, 0, 1, 1, 2, 2, 2, 3, 2, 4, 2, 5, 2, 6, 2, 7, 2, 8, 2);
                            equipTableProps = ArmorSlotEquipCalculator(enemy, traits, equipTableProps);
                            equipTableProps[13] = 20;
                            equipTableProps[14] = 60;
                            break;
                        case (int)ClassCareers.Rogue:
                            equipTableProps[0] = FormulaHelper.PickOneOf((int)Weapons.Broadsword, (int)Weapons.Claymore, (int)Weapons.Dai_Katana, (int)Weapons.Katana, (int)Weapons.Longsword, (int)Weapons.Saber);
                            equipTableProps[1] = FormulaHelper.PickOneOfCompact(-1, 4, (int)Weapons.Broadsword, 1, (int)Weapons.Claymore, 1, (int)Weapons.Dai_Katana, 1, (int)Weapons.Katana, 1, (int)Weapons.Longsword, 1, (int)Weapons.Saber, 1, (int)Weapons.Flail, 1, (int)Weapons.Mace, 1, (int)Weapons.Warhammer, 1);
                            equipTableProps[2] = FormulaHelper.PickOneOfCompact(-1, 2, (int)Armor.Buckler, 1, (int)Armor.Round_Shield, 1, (int)Armor.Kite_Shield, 1);
                            equipTableProps[3] = FormulaHelper.PickOneOfCompact(0, 1, 1, 2, 2, 2, 3, 2, 4, 2, 5, 2, 6, 2, 7, 2, 8, 2);
                            equipTableProps[4] = FormulaHelper.PickOneOfCompact(0, 4, 1, 1);
                            equipTableProps[5] = UnityEngine.Random.Range(128, 135 + 1);
                            equipTableProps = ArmorSlotEquipCalculator(enemy, traits, equipTableProps);
                            equipTableProps[13] = 20;
                            equipTableProps[14] = 60;
                            break;
                        case (int)ClassCareers.Acrobat:
                            equipTableProps[0] = FormulaHelper.PickOneOf((int)Weapons.Dagger, (int)Weapons.Tanto, (int)Weapons.Shortsword, (int)Weapons.Wakazashi);
                            equipTableProps[1] = FormulaHelper.PickOneOfCompact(-1, 4, (int)Weapons.Dagger, 1, (int)Weapons.Tanto, 1, (int)Weapons.Shortsword, 1, (int)Weapons.Wakazashi, 1, (int)Weapons.Short_Bow, 1, (int)Weapons.Long_Bow, 1);
                            equipTableProps[3] = FormulaHelper.PickOneOfCompact(-1, 7, 0, 1, 1, 2, 2, 2, 3, 2, 4, 2, 5, 2, 6, 2, 7, 2, 8, 2);
                            equipTableProps = ArmorSlotEquipCalculator(enemy, traits, equipTableProps);
                            equipTableProps[13] = 25;
                            equipTableProps[14] = 65;
                            break;
                        case (int)ClassCareers.Thief:
                            equipTableProps[0] = FormulaHelper.PickOneOf((int)Weapons.Dagger, (int)Weapons.Tanto, (int)Weapons.Shortsword, (int)Weapons.Wakazashi);
                            equipTableProps[1] = FormulaHelper.PickOneOfCompact(-1, 3, (int)Weapons.Dagger, 1, (int)Weapons.Tanto, 1, (int)Weapons.Shortsword, 1, (int)Weapons.Wakazashi, 1);
                            equipTableProps[2] = FormulaHelper.PickOneOfCompact(-1, 4, (int)Armor.Buckler, 1);
                            equipTableProps[3] = FormulaHelper.PickOneOfCompact(-1, 4, 0, 1, 1, 2, 2, 2, 3, 2, 4, 2, 5, 2, 6, 2, 7, 2, 8, 2);
                            equipTableProps = ArmorSlotEquipCalculator(enemy, traits, equipTableProps);
                            equipTableProps[13] = 20;
                            equipTableProps[14] = 60;
                            break;
                        case (int)ClassCareers.Assassin:
                            equipTableProps[0] = FormulaHelper.PickOneOf((int)Weapons.Dagger, (int)Weapons.Tanto, (int)Weapons.Shortsword, (int)Weapons.Wakazashi, (int)Weapons.Broadsword, (int)Weapons.Claymore, (int)Weapons.Dai_Katana, (int)Weapons.Katana, (int)Weapons.Longsword, (int)Weapons.Saber, (int)Weapons.Flail, (int)Weapons.Mace, (int)Weapons.Warhammer);
                            equipTableProps[1] = FormulaHelper.PickOneOf((int)Weapons.Dagger, (int)Weapons.Tanto, (int)Weapons.Shortsword, (int)Weapons.Wakazashi, (int)Weapons.Short_Bow, (int)Weapons.Long_Bow, (int)Weapons.Battle_Axe, (int)Weapons.War_Axe);
                            equipTableProps[3] = FormulaHelper.PickOneOfCompact(0, 1, 1, 2, 2, 2, 3, 2, 4, 2, 5, 2, 6, 2, 7, 2, 8, 2);
                            equipTableProps[4] = 1;
                            equipTableProps[5] = UnityEngine.Random.Range(128, 135 + 1);
                            equipTableProps = ArmorSlotEquipCalculator(enemy, traits, equipTableProps);
                            equipTableProps[13] = 40;
                            equipTableProps[14] = 80;
                            break;
                        case (int)ClassCareers.Monk:
                            equipTableProps[0] = (int)Weapons.Staff;
                            equipTableProps[1] = FormulaHelper.PickOneOfCompact(-1, 4, (int)Weapons.Dagger, 1, (int)Weapons.Tanto, 1, (int)Weapons.Shortsword, 1, (int)Weapons.Wakazashi, 1, (int)Weapons.Battle_Axe, 1, (int)Weapons.War_Axe, 1, (int)Weapons.Flail, 1, (int)Weapons.Mace, 1, (int)Weapons.Warhammer, 1, (int)Weapons.Short_Bow, 1, (int)Weapons.Long_Bow, 1);
                            equipTableProps = ArmorSlotEquipCalculator(enemy, traits, equipTableProps);
                            equipTableProps[13] = 25;
                            equipTableProps[14] = 65;
                            break;
                        case (int)ClassCareers.Archer:
                            equipTableProps[0] = FormulaHelper.PickOneOf((int)Weapons.Short_Bow, (int)Weapons.Long_Bow);
                            equipTableProps[1] = FormulaHelper.PickOneOf((int)Weapons.Dagger, (int)Weapons.Tanto, (int)Weapons.Shortsword, (int)Weapons.Wakazashi, (int)Weapons.Broadsword, (int)Weapons.Claymore, (int)Weapons.Dai_Katana, (int)Weapons.Katana, (int)Weapons.Longsword, (int)Weapons.Saber, (int)Weapons.Battle_Axe, (int)Weapons.War_Axe, (int)Weapons.Flail, (int)Weapons.Mace, (int)Weapons.Warhammer, (int)Weapons.Short_Bow, (int)Weapons.Long_Bow);
                            equipTableProps[3] = FormulaHelper.PickOneOfCompact(-1, 3, 0, 1, 1, 2, 2, 2, 3, 2, 4, 2, 5, 2, 6, 2, 7, 2, 8, 2);
                            equipTableProps[4] = FormulaHelper.PickOneOfCompact(0, 9, 1, 1);
                            equipTableProps[5] = UnityEngine.Random.Range(128, 135 + 1);
                            equipTableProps = ArmorSlotEquipCalculator(enemy, traits, equipTableProps);
                            equipTableProps[13] = 35;
                            equipTableProps[14] = 75;
                            break;
                        case (int)ClassCareers.Ranger:
                            equipTableProps[0] = FormulaHelper.PickOneOf((int)Weapons.Short_Bow, (int)Weapons.Long_Bow, (int)Weapons.Battle_Axe, (int)Weapons.War_Axe, (int)Weapons.Broadsword, (int)Weapons.Claymore, (int)Weapons.Dai_Katana, (int)Weapons.Katana, (int)Weapons.Longsword, (int)Weapons.Saber);
                            equipTableProps[1] = FormulaHelper.PickOneOf((int)Weapons.Dagger, (int)Weapons.Tanto, (int)Weapons.Shortsword, (int)Weapons.Wakazashi, (int)Weapons.Broadsword, (int)Weapons.Battle_Axe, (int)Weapons.Mace, (int)Weapons.Short_Bow, (int)Weapons.Long_Bow);
                            equipTableProps[2] = FormulaHelper.PickOneOfCompact(-1, 4, (int)Armor.Buckler, 1, (int)Armor.Round_Shield, 1, (int)Armor.Kite_Shield, 1);
                            equipTableProps[3] = FormulaHelper.PickOneOfCompact(-1, 4, 0, 1, 1, 2, 2, 2, 3, 2, 4, 2, 5, 2, 6, 2, 7, 2, 8, 2);
                            equipTableProps[4] = FormulaHelper.PickOneOfCompact(0, 4, 1, 1);
                            equipTableProps[5] = UnityEngine.Random.Range(128, 135 + 1);
                            equipTableProps = ArmorSlotEquipCalculator(enemy, traits, equipTableProps);
                            equipTableProps[13] = 30;
                            equipTableProps[14] = 70;
                            break;
                        case (int)ClassCareers.Barbarian:
                            equipTableProps[0] = FormulaHelper.PickOneOf((int)Weapons.Broadsword, (int)Weapons.Claymore, (int)Weapons.Dai_Katana, (int)Weapons.Katana, (int)Weapons.Longsword, (int)Weapons.Saber, (int)Weapons.Flail, (int)Weapons.Mace, (int)Weapons.Warhammer, (int)Weapons.Battle_Axe, (int)Weapons.War_Axe, (int)Weapons.Short_Bow, (int)Weapons.Long_Bow);
                            equipTableProps[1] = FormulaHelper.PickOneOf((int)Weapons.Broadsword, (int)Weapons.Claymore, (int)Weapons.Dai_Katana, (int)Weapons.Katana, (int)Weapons.Longsword, (int)Weapons.Saber, (int)Weapons.Flail, (int)Weapons.Mace, (int)Weapons.Warhammer, (int)Weapons.Battle_Axe, (int)Weapons.War_Axe, (int)Weapons.Short_Bow, (int)Weapons.Long_Bow);
                            equipTableProps[2] = FormulaHelper.PickOneOfCompact(-1, 5, (int)Armor.Buckler, 1, (int)Armor.Round_Shield, 1, (int)Armor.Kite_Shield, 1, (int)Armor.Tower_Shield, 1);
                            equipTableProps[3] = FormulaHelper.PickOneOfCompact(-1, 6, 0, 1, 1, 2, 2, 2, 3, 2, 4, 2, 5, 2, 6, 2, 7, 2, 8, 2);
                            equipTableProps = ArmorSlotEquipCalculator(enemy, traits, equipTableProps);
                            equipTableProps[13] = 20;
                            equipTableProps[14] = 60;
                            break;
                        case (int)ClassCareers.Warrior:
                            equipTableProps[0] = FormulaHelper.PickOneOf((int)Weapons.Broadsword, (int)Weapons.Claymore, (int)Weapons.Dai_Katana, (int)Weapons.Katana, (int)Weapons.Longsword, (int)Weapons.Saber, (int)Weapons.Flail, (int)Weapons.Mace, (int)Weapons.Warhammer, (int)Weapons.Battle_Axe, (int)Weapons.War_Axe, (int)Weapons.Short_Bow, (int)Weapons.Long_Bow, (int)Weapons.Shortsword, (int)Weapons.Wakazashi);
                            equipTableProps[1] = FormulaHelper.PickOneOf((int)Weapons.Broadsword, (int)Weapons.Claymore, (int)Weapons.Dai_Katana, (int)Weapons.Katana, (int)Weapons.Longsword, (int)Weapons.Saber, (int)Weapons.Flail, (int)Weapons.Mace, (int)Weapons.Warhammer, (int)Weapons.Battle_Axe, (int)Weapons.War_Axe, (int)Weapons.Short_Bow, (int)Weapons.Long_Bow, (int)Weapons.Shortsword, (int)Weapons.Wakazashi, (int)Weapons.Dagger, (int)Weapons.Tanto);
                            equipTableProps[2] = FormulaHelper.PickOneOfCompact(-1, 2, (int)Armor.Buckler, 1, (int)Armor.Round_Shield, 1, (int)Armor.Kite_Shield, 1, (int)Armor.Tower_Shield, 1);
                            equipTableProps[3] = FormulaHelper.PickOneOfCompact(0, 1, 1, 2, 2, 2, 3, 2, 4, 2, 5, 2, 6, 2, 7, 2, 8, 2);
                            equipTableProps = ArmorSlotEquipCalculator(enemy, traits, equipTableProps);
                            equipTableProps[13] = 40;
                            equipTableProps[14] = 80;
                            break;
                        case (int)ClassCareers.Knight:
                            equipTableProps[0] = FormulaHelper.PickOneOf((int)Weapons.Broadsword, (int)Weapons.Claymore, (int)Weapons.Dai_Katana, (int)Weapons.Katana, (int)Weapons.Longsword, (int)Weapons.Saber, (int)Weapons.Flail, (int)Weapons.Mace, (int)Weapons.Warhammer, (int)Weapons.Battle_Axe, (int)Weapons.War_Axe, (int)Weapons.Short_Bow, (int)Weapons.Long_Bow, (int)Weapons.Shortsword, (int)Weapons.Wakazashi);
                            equipTableProps[1] = FormulaHelper.PickOneOf((int)Weapons.Broadsword, (int)Weapons.Claymore, (int)Weapons.Dai_Katana, (int)Weapons.Katana, (int)Weapons.Longsword, (int)Weapons.Saber, (int)Weapons.Flail, (int)Weapons.Mace, (int)Weapons.Warhammer, (int)Weapons.Battle_Axe, (int)Weapons.War_Axe, (int)Weapons.Short_Bow, (int)Weapons.Long_Bow, (int)Weapons.Shortsword, (int)Weapons.Wakazashi, (int)Weapons.Dagger, (int)Weapons.Tanto);
                            equipTableProps[2] = FormulaHelper.PickOneOfCompact(-1, 2, (int)Armor.Buckler, 1, (int)Armor.Round_Shield, 1, (int)Armor.Kite_Shield, 1, (int)Armor.Tower_Shield, 1);
                            equipTableProps[3] = FormulaHelper.PickOneOfCompact(0, 1, 1, 2, 2, 2, 3, 2, 4, 2, 5, 2, 6, 2, 7, 2, 8, 2);
                            equipTableProps = ArmorSlotEquipCalculator(enemy, traits, equipTableProps);
                            equipTableProps[13] = 45;
                            equipTableProps[14] = 85;
                            break;
                        default:
                            return equipTableProps;
                    }
                }
                else
                {
                    switch (AITarget.CareerIndex)
                    {
                        case 7:
                            equipTableProps[0] = FormulaHelper.PickOneOf((int)Weapons.Broadsword, (int)Weapons.Claymore, (int)Weapons.Dai_Katana, (int)Weapons.Katana, (int)Weapons.Longsword, (int)Weapons.Saber, (int)Weapons.Flail, (int)Weapons.Mace, (int)Weapons.Warhammer, (int)Weapons.Battle_Axe, (int)Weapons.War_Axe, (int)Weapons.Short_Bow, (int)Weapons.Long_Bow, (int)Weapons.Shortsword, (int)Weapons.Wakazashi);
                            equipTableProps[1] = FormulaHelper.PickOneOfCompact(-1, 15, (int)Weapons.Broadsword, 1, (int)Weapons.Claymore, 1, (int)Weapons.Dai_Katana, 1, (int)Weapons.Katana, 1, (int)Weapons.Longsword, 1, (int)Weapons.Saber, 1, (int)Weapons.Flail, 1, (int)Weapons.Mace, 1, (int)Weapons.Warhammer, 1, (int)Weapons.Battle_Axe, 1, (int)Weapons.War_Axe, 1, (int)Weapons.Short_Bow, 1, (int)Weapons.Long_Bow, 1, (int)Weapons.Shortsword, 1, (int)Weapons.Wakazashi, 1);
                            equipTableProps[2] = FormulaHelper.PickOneOfCompact(-1, 6, (int)Armor.Buckler, 1, (int)Armor.Round_Shield, 1, (int)Armor.Kite_Shield, 1, (int)Armor.Tower_Shield, 1);
                            equipTableProps[3] = FormulaHelper.PickOneOfCompact(0, 1, 1, 2, 2, 2, 3, 2, 4, 2, 5, 2, 6, 2, 7, 2, 8, 2);
                            equipTableProps = ArmorSlotEquipCalculator(enemy, traits, equipTableProps);
                            equipTableProps[13] = 30;
                            equipTableProps[14] = 70;
                            break;
                        case 8:
                            equipTableProps[0] = FormulaHelper.PickOneOf((int)Weapons.Broadsword, (int)Weapons.Claymore, (int)Weapons.Dai_Katana, (int)Weapons.Katana, (int)Weapons.Longsword, (int)Weapons.Saber, (int)Weapons.Flail, (int)Weapons.Mace, (int)Weapons.Warhammer, (int)Weapons.Battle_Axe, (int)Weapons.War_Axe, (int)Weapons.Short_Bow, (int)Weapons.Long_Bow);
                            equipTableProps[1] = FormulaHelper.PickOneOfCompact(-1, 15, (int)Weapons.Broadsword, 1, (int)Weapons.Claymore, 1, (int)Weapons.Dai_Katana, 1, (int)Weapons.Katana, 1, (int)Weapons.Longsword, 1, (int)Weapons.Saber, 1, (int)Weapons.Flail, 1, (int)Weapons.Mace, 1, (int)Weapons.Warhammer, 1, (int)Weapons.Battle_Axe, 1, (int)Weapons.War_Axe, 1, (int)Weapons.Short_Bow, 1, (int)Weapons.Long_Bow, 1);
                            equipTableProps[2] = FormulaHelper.PickOneOfCompact(-1, 6, (int)Armor.Buckler, 1, (int)Armor.Round_Shield, 1, (int)Armor.Kite_Shield, 1);
                            equipTableProps[3] = FormulaHelper.PickOneOfCompact(-1, 5, 4, 1, 6, 1, 7, 1);
                            equipTableProps[4] = FormulaHelper.PickOneOfCompact(0, 9, 1, 1);
                            equipTableProps[5] = UnityEngine.Random.Range(128, 135 + 1);
                            equipTableProps = ArmorSlotEquipCalculator(enemy, traits, equipTableProps);
                            equipTableProps[13] = 15;
                            equipTableProps[14] = 55;
                            break;
                        case 12:
                            equipTableProps[0] = FormulaHelper.PickOneOf((int)Weapons.Broadsword, (int)Weapons.Claymore, (int)Weapons.Dai_Katana, (int)Weapons.Katana, (int)Weapons.Longsword, (int)Weapons.Saber, (int)Weapons.Flail, (int)Weapons.Mace, (int)Weapons.Warhammer, (int)Weapons.Battle_Axe, (int)Weapons.War_Axe, (int)Weapons.Short_Bow, (int)Weapons.Long_Bow, (int)Weapons.Shortsword, (int)Weapons.Wakazashi);
                            equipTableProps[1] = FormulaHelper.PickOneOfCompact(-1, 12, (int)Weapons.Broadsword, 1, (int)Weapons.Claymore, 1, (int)Weapons.Dai_Katana, 1, (int)Weapons.Katana, 1, (int)Weapons.Longsword, 1, (int)Weapons.Saber, 1, (int)Weapons.Flail, 1, (int)Weapons.Mace, 1, (int)Weapons.Warhammer, 1, (int)Weapons.Battle_Axe, 1, (int)Weapons.War_Axe, 1, (int)Weapons.Short_Bow, 1, (int)Weapons.Long_Bow, 1, (int)Weapons.Shortsword, 1, (int)Weapons.Wakazashi, 1);
                            equipTableProps[2] = FormulaHelper.PickOneOfCompact(-1, 5, (int)Armor.Buckler, 1, (int)Armor.Round_Shield, 1, (int)Armor.Kite_Shield, 1, (int)Armor.Tower_Shield, 1);
                            equipTableProps[3] = FormulaHelper.PickOneOfCompact(0, 1, 1, 2, 2, 2, 3, 2, 4, 2, 5, 2, 6, 2, 7, 2, 8, 2);
                            equipTableProps = ArmorSlotEquipCalculator(enemy, traits, equipTableProps);
                            equipTableProps[13] = 35;
                            equipTableProps[14] = 75;
                            break;
                        case 15:
                            equipTableProps[0] = (int)Weapons.Battle_Axe;
                            equipTableProps[2] = FormulaHelper.PickOneOf((int)Armor.Buckler, (int)Armor.Round_Shield, (int)Armor.Kite_Shield);
                            equipTableProps[3] = FormulaHelper.PickOneOfCompact(-1, 6, 1, 1, 2, 1, 3, 1, 4, 1, 5, 1, 6, 1, 7, 1, 8, 1);
                            equipTableProps = ArmorSlotEquipCalculator(enemy, traits, equipTableProps);
                            equipTableProps[13] = 5;
                            equipTableProps[14] = 45;
                            break;
                        case 17:
                            equipTableProps[3] = FormulaHelper.PickOneOfCompact(-1, 6, 1, 1, 2, 1, 3, 1, 4, 1, 5, 1, 6, 1, 7, 1, 8, 1);
                            equipTableProps = ArmorSlotEquipCalculator(enemy, traits, equipTableProps);
                            equipTableProps[13] = 5;
                            equipTableProps[14] = 45;
                            break;
                        case 21:
                            equipTableProps[0] = (int)Weapons.Staff;
                            equipTableProps[1] = FormulaHelper.PickOneOfCompact(-1, 5, (int)Weapons.Dagger, 1, (int)Weapons.Tanto, 1, (int)Weapons.Shortsword, 1);
                            equipTableProps[2] = FormulaHelper.PickOneOfCompact(-1, 6, (int)Armor.Buckler, 1, (int)Armor.Round_Shield, 1);
                            equipTableProps[3] = FormulaHelper.PickOneOfCompact(-1, 8, 0, 1, 1, 2, 2, 2, 3, 2, 4, 2, 5, 2, 6, 2, 7, 2, 8, 2);
                            equipTableProps = ArmorSlotEquipCalculator(enemy, traits, equipTableProps);
                            equipTableProps[13] = 20;
                            equipTableProps[14] = 60;
                            break;
                        case 23:
                            equipTableProps[0] = FormulaHelper.PickOneOf((int)Weapons.Longsword, (int)Weapons.Saber);
                            equipTableProps[13] = 10;
                            equipTableProps[14] = 50;
                            break;
                        case 24:
                            equipTableProps[0] = FormulaHelper.PickOneOf((int)Weapons.Broadsword, (int)Weapons.Claymore, (int)Weapons.Dai_Katana, (int)Weapons.Katana, (int)Weapons.Longsword, (int)Weapons.Saber, (int)Weapons.Flail, (int)Weapons.Mace, (int)Weapons.Warhammer, (int)Weapons.Battle_Axe, (int)Weapons.War_Axe, (int)Weapons.Short_Bow, (int)Weapons.Long_Bow, (int)Weapons.Shortsword, (int)Weapons.Wakazashi);
                            equipTableProps[1] = FormulaHelper.PickOneOfCompact(-1, 9, (int)Weapons.Broadsword, 1, (int)Weapons.Claymore, 1, (int)Weapons.Dai_Katana, 1, (int)Weapons.Katana, 1, (int)Weapons.Longsword, 1, (int)Weapons.Saber, 1, (int)Weapons.Flail, 1, (int)Weapons.Mace, 1, (int)Weapons.Warhammer, 1, (int)Weapons.Battle_Axe, 1, (int)Weapons.War_Axe, 1, (int)Weapons.Short_Bow, 1, (int)Weapons.Long_Bow, 1, (int)Weapons.Shortsword, 1, (int)Weapons.Wakazashi, 1);
                            equipTableProps[2] = FormulaHelper.PickOneOfCompact(-1, 5, (int)Armor.Buckler, 1, (int)Armor.Round_Shield, 1, (int)Armor.Kite_Shield, 1, (int)Armor.Tower_Shield, 1);
                            equipTableProps[3] = FormulaHelper.PickOneOfCompact(0, 1, 1, 2, 2, 2, 3, 2, 4, 2, 5, 2, 6, 2, 7, 2, 8, 2);
                            equipTableProps = ArmorSlotEquipCalculator(enemy, traits, equipTableProps);
                            equipTableProps[13] = 40;
                            equipTableProps[14] = 80;
                            break;
                        case 25:
                            equipTableProps[0] = (int)Weapons.Warhammer;
                            equipTableProps[3] = FormulaHelper.PickOneOfCompact(0, 1, 1, 2, 2, 2, 3, 2, 4, 2, 5, 2, 6, 2, 7, 2, 8, 2);
                            equipTableProps = ArmorSlotEquipCalculator(enemy, traits, equipTableProps);
                            equipTableProps[13] = 15;
                            equipTableProps[14] = 55;
                            break;
                        case 26:
                            equipTableProps[0] = FormulaHelper.PickOneOf((int)Weapons.Claymore, (int)Weapons.Dai_Katana);
                            equipTableProps[3] = FormulaHelper.PickOneOfCompact(0, 1, 1, 2, 2, 2, 3, 2, 4, 2, 5, 2, 6, 2, 7, 2, 8, 2);
                            equipTableProps = ArmorSlotEquipCalculator(enemy, traits, equipTableProps);
                            equipTableProps[13] = 15;
                            equipTableProps[14] = 55;
                            break;
                        case 27:
                            equipTableProps[0] = (int)Weapons.War_Axe;
                            equipTableProps[3] = FormulaHelper.PickOneOfCompact(0, 1, 1, 2, 2, 2, 3, 2, 4, 2, 5, 2, 6, 2, 7, 2, 8, 2);
                            equipTableProps = ArmorSlotEquipCalculator(enemy, traits, equipTableProps);
                            equipTableProps[13] = 15;
                            equipTableProps[14] = 55;
                            break;
                        case 31:
                            equipTableProps[0] = FormulaHelper.PickOneOf((int)Weapons.Broadsword, (int)Weapons.Claymore, (int)Weapons.Dai_Katana, (int)Weapons.Katana, (int)Weapons.Longsword, (int)Weapons.Saber);
                            equipTableProps[2] = FormulaHelper.PickOneOfCompact(-1, 7, (int)Armor.Buckler, 1, (int)Armor.Round_Shield, 1, (int)Armor.Kite_Shield, 1, (int)Armor.Tower_Shield, 1);
                            equipTableProps[3] = FormulaHelper.PickOneOfCompact(0, 1, 1, 2, 2, 2, 3, 2, 4, 2, 5, 2, 6, 2, 7, 2, 8, 2);
                            equipTableProps = ArmorSlotEquipCalculator(enemy, traits, equipTableProps);
                            equipTableProps[13] = 35;
                            equipTableProps[14] = 75;
                            break;
                        case 28:
                        case 30:
                            equipTableProps[3] = FormulaHelper.PickOneOfCompact(-1, 8, 0, 1, 1, 2, 2, 2, 3, 2, 4, 2, 5, 2, 6, 2, 7, 2, 8, 2);
                            equipTableProps = ArmorSlotEquipCalculator(enemy, traits, equipTableProps);
                            equipTableProps[13] = 25;
                            equipTableProps[14] = 65;
                            break;
                        case 32:
                        case 33:
                            equipTableProps[0] = (int)Weapons.Staff;
                            equipTableProps[13] = 20;
                            equipTableProps[14] = 60;
                            break;
                        default:
                            return equipTableProps;
                    }
                }
            }
            else
            {
                return equipTableProps;
            }

            equipTableProps = TraitEquipModCalculator(enemy, traits, equipTableProps);

            return equipTableProps;
        }

        public static int[] EnemyPredefLootTableCalculator(DaggerfallEntity enemy, int[] traits)
        {
            // Index meanings: 0 = goldCarried, 1 = miscPlants, 2 = flowerPlants, 3 = fruitPlants, 4 = animalParts, 5 = creatureParts, 6 = solvent, 7 = metals, 8 = books, 9 = clothing bool, 10 = extras bool.
            // Main variable used in determining enemy loot/equipment values for purposes of targeted loot generation based on many context related variables.
            int[] equipTableProps = { -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            EnemyEntity AITarget = enemy as EnemyEntity;
            int level = enemy.Level;

            if (AITarget.EntityType == EntityTypes.EnemyClass)
            {
                switch (AITarget.CareerIndex)
                {
                    case (int)ClassCareers.Mage:
                        equipTableProps[0] = UnityEngine.Random.Range(4 * level, 12 * (int)Mathf.Ceil(level / 2) + 1); // I'll definitely have to alter these gold values later, but for now will work.
                        equipTableProps[1] = FormulaHelper.PickOneOfCompact(0, 2, 1, 2, 2, 1);
                        equipTableProps[2] = FormulaHelper.PickOneOfCompact(0, 2, 1, 2, 2, 1);
                        equipTableProps[3] = FormulaHelper.PickOneOfCompact(0, 2, 1, 1);
                        equipTableProps[5] = FormulaHelper.PickOneOfCompact(0, 7, 1, 3, 2, 1);
                        equipTableProps[6] = FormulaHelper.PickOneOfCompact(0, 3, 1, 1);
                        equipTableProps[7] = FormulaHelper.PickOneOfCompact(0, 5, 1, 1);
                        equipTableProps[8] = FormulaHelper.PickOneOfCompact(1, 4, 2, 6, 3, 2, 4, 1);
                        equipTableProps[9] = 1;
                        equipTableProps[10] = 1;
                        break;
                    case (int)ClassCareers.Spellsword:
                        equipTableProps[0] = UnityEngine.Random.Range(6 * level, 14 * (int)Mathf.Ceil(level / 2) + 1);
                        equipTableProps[1] = FormulaHelper.PickOneOfCompact(0, 2, 1, 2, 2, 1);
                        equipTableProps[2] = FormulaHelper.PickOneOfCompact(0, 2, 1, 1);
                        equipTableProps[3] = FormulaHelper.PickOneOfCompact(0, 3, 1, 2, 2, 1);
                        equipTableProps[5] = FormulaHelper.PickOneOfCompact(0, 5, 1, 3, 2, 1);
                        equipTableProps[6] = FormulaHelper.PickOneOfCompact(0, 3, 1, 1);
                        equipTableProps[7] = FormulaHelper.PickOneOfCompact(0, 5, 1, 1);
                        equipTableProps[8] = FormulaHelper.PickOneOfCompact(1, 6, 2, 3, 1, 1);
                        equipTableProps[9] = 1;
                        equipTableProps[10] = 1;
                        break;
                    case (int)ClassCareers.Battlemage:
                        equipTableProps[0] = UnityEngine.Random.Range(8 * level, 16 * (int)Mathf.Ceil(level / 2) + 1);
                        equipTableProps[1] = FormulaHelper.PickOneOfCompact(0, 3, 1, 1);
                        equipTableProps[2] = FormulaHelper.PickOneOfCompact(0, 3, 1, 1);
                        equipTableProps[3] = FormulaHelper.PickOneOfCompact(0, 3, 1, 1);
                        equipTableProps[5] = FormulaHelper.PickOneOfCompact(0, 6, 1, 3, 2, 2);
                        equipTableProps[6] = FormulaHelper.PickOneOfCompact(0, 5, 1, 1);
                        equipTableProps[7] = FormulaHelper.PickOneOfCompact(0, 6, 1, 1);
                        equipTableProps[8] = FormulaHelper.PickOneOfCompact(1, 11, 2, 5, 3, 1);
                        equipTableProps[9] = 1;
                        equipTableProps[10] = 1;
                        break;
                    case (int)ClassCareers.Sorcerer:
                        equipTableProps[0] = UnityEngine.Random.Range(2 * level, 10 * (int)Mathf.Ceil(level / 2) + 1);
                        equipTableProps[1] = FormulaHelper.PickOneOfCompact(0, 2, 1, 1);
                        equipTableProps[2] = FormulaHelper.PickOneOfCompact(0, 2, 1, 2, 2, 1);
                        equipTableProps[3] = FormulaHelper.PickOneOfCompact(0, 2, 1, 2, 2, 1);
                        equipTableProps[5] = FormulaHelper.PickOneOfCompact(0, 6, 1, 1);
                        equipTableProps[6] = FormulaHelper.PickOneOfCompact(0, 4, 1, 3, 2, 1);
                        equipTableProps[7] = FormulaHelper.PickOneOfCompact(0, 6, 1, 4, 2, 1);
                        equipTableProps[8] = FormulaHelper.PickOneOfCompact(1, 10, 2, 3, 3, 1);
                        equipTableProps[9] = 1;
                        equipTableProps[10] = 1;
                        break;
                    case (int)ClassCareers.Healer:
                        equipTableProps[0] = UnityEngine.Random.Range(2 * level, 10 * (int)Mathf.Ceil(level / 2) + 1);
                        equipTableProps[1] = FormulaHelper.PickOneOfCompact(0, 2, 1, 2, 2, 1);
                        equipTableProps[2] = FormulaHelper.PickOneOfCompact(1, 3, 2, 1);
                        equipTableProps[3] = FormulaHelper.PickOneOfCompact(1, 4, 2, 2, 3, 1);
                        equipTableProps[4] = FormulaHelper.PickOneOfCompact(0, 2, 1, 1);
                        equipTableProps[5] = FormulaHelper.PickOneOfCompact(0, 6, 1, 3, 2, 1);
                        equipTableProps[6] = FormulaHelper.PickOneOfCompact(0, 3, 1, 2, 2, 1);
                        equipTableProps[7] = FormulaHelper.PickOneOfCompact(0, 5, 1, 1);
                        equipTableProps[8] = FormulaHelper.PickOneOfCompact(1, 5, 2, 1);
                        equipTableProps[9] = 1;
                        equipTableProps[10] = 1;
                        break;
                    case (int)ClassCareers.Nightblade:
                        equipTableProps[0] = UnityEngine.Random.Range(10 * level, 18 * (int)Mathf.Ceil(level / 2) + 1);
                        equipTableProps[1] = FormulaHelper.PickOneOfCompact(0, 2, 1, 1);
                        equipTableProps[2] = FormulaHelper.PickOneOfCompact(2, 5, 3, 2, 4, 1);
                        equipTableProps[3] = FormulaHelper.PickOneOfCompact(1, 5, 2, 1);
                        equipTableProps[4] = FormulaHelper.PickOneOfCompact(1, 5, 2, 3, 3, 1);
                        equipTableProps[5] = FormulaHelper.PickOneOfCompact(1, 4, 2, 1);
                        equipTableProps[6] = FormulaHelper.PickOneOfCompact(1, 5, 2, 2, 3, 1);
                        equipTableProps[7] = FormulaHelper.PickOneOfCompact(0, 4, 1, 4, 2, 1);
                        equipTableProps[8] = FormulaHelper.PickOneOfCompact(0, 5, 1, 1);
                        equipTableProps[9] = 1;
                        equipTableProps[10] = 1;
                        break;
                    case (int)ClassCareers.Bard:
                        equipTableProps[0] = UnityEngine.Random.Range(7 * level, 15 * (int)Mathf.Ceil(level / 2) + 1);
                        equipTableProps[7] = FormulaHelper.PickOneOfCompact(0, 6, 1, 3, 2, 1);
                        equipTableProps[8] = FormulaHelper.PickOneOfCompact(0, 3, 1, 2);
                        equipTableProps[9] = 1;
                        equipTableProps[10] = 1;
                        break;
                    case (int)ClassCareers.Burglar:
                        equipTableProps[0] = UnityEngine.Random.Range(12 * level, 21 * (int)Mathf.Ceil(level / 2) + 1);
                        equipTableProps[7] = FormulaHelper.PickOneOfCompact(1, 6, 2, 4, 3, 1, 4, 1);
                        equipTableProps[9] = 1;
                        equipTableProps[10] = 1;
                        break;
                    case (int)ClassCareers.Rogue:
                        equipTableProps[0] = UnityEngine.Random.Range(8 * level, 18 * (int)Mathf.Ceil(level / 2) + 1);
                        equipTableProps[7] = FormulaHelper.PickOneOfCompact(0, 2, 1, 5, 2, 2);
                        equipTableProps[9] = 1;
                        break;
                    case (int)ClassCareers.Acrobat:
                        equipTableProps[0] = UnityEngine.Random.Range(4 * level, 12 * (int)Mathf.Ceil(level / 2) + 1);
                        equipTableProps[2] = FormulaHelper.PickOneOfCompact(0, 3, 1, 4, 2, 2);
                        equipTableProps[9] = 1;
                        break;
                    case (int)ClassCareers.Thief:
                        equipTableProps[0] = UnityEngine.Random.Range(14 * level, 23 * (int)Mathf.Ceil(level / 2) + 1);
                        equipTableProps[7] = FormulaHelper.PickOneOfCompact(1, 6, 2, 3, 3, 1);
                        equipTableProps[9] = 1;
                        equipTableProps[10] = 1;
                        break;
                    case (int)ClassCareers.Assassin:
                        equipTableProps[0] = UnityEngine.Random.Range(9 * level, 19 * (int)Mathf.Ceil(level / 2) + 1);
                        equipTableProps[1] = FormulaHelper.PickOneOfCompact(0, 2, 1, 1);
                        equipTableProps[2] = FormulaHelper.PickOneOfCompact(1, 4, 2, 1);
                        equipTableProps[3] = FormulaHelper.PickOneOfCompact(1, 4, 2, 1);
                        equipTableProps[4] = FormulaHelper.PickOneOfCompact(1, 7, 2, 1);
                        equipTableProps[5] = FormulaHelper.PickOneOfCompact(0, 6, 1, 1);
                        equipTableProps[6] = FormulaHelper.PickOneOfCompact(1, 7, 2, 2);
                        equipTableProps[7] = FormulaHelper.PickOneOfCompact(0, 7, 1, 4, 2, 1);
                        equipTableProps[8] = FormulaHelper.PickOneOfCompact(0, 5, 1, 1);
                        equipTableProps[9] = 1;
                        equipTableProps[10] = 1;
                        break;
                    case (int)ClassCareers.Monk:
                        equipTableProps[0] = UnityEngine.Random.Range(2 * level, 8 * (int)Mathf.Ceil(level / 2) + 1);
                        equipTableProps[8] = FormulaHelper.PickOneOfCompact(0, 3, 1, 2);
                        equipTableProps[9] = 1;
                        equipTableProps[10] = 1;
                        break;
                    case (int)ClassCareers.Archer:
                        equipTableProps[0] = UnityEngine.Random.Range(6 * level, 14 * (int)Mathf.Ceil(level / 2) + 1);
                        equipTableProps[2] = FormulaHelper.PickOneOfCompact(0, 2, 1, 1);
                        equipTableProps[3] = FormulaHelper.PickOneOfCompact(0, 2, 1, 1);
                        equipTableProps[4] = FormulaHelper.PickOneOfCompact(0, 4, 1, 3, 2, 1);
                        equipTableProps[5] = FormulaHelper.PickOneOfCompact(0, 7, 1, 1);
                        equipTableProps[6] = FormulaHelper.PickOneOfCompact(0, 6, 1, 3, 2, 1);
                        equipTableProps[7] = FormulaHelper.PickOneOfCompact(0, 10, 1, 3, 2, 1);
                        equipTableProps[9] = 1;
                        equipTableProps[10] = 1;
                        break;
                    case (int)ClassCareers.Ranger:
                        equipTableProps[0] = UnityEngine.Random.Range(3 * level, 10 * (int)Mathf.Ceil(level / 2) + 1);
                        equipTableProps[1] = FormulaHelper.PickOneOfCompact(1, 5, 2, 3, 3, 1);
                        equipTableProps[2] = FormulaHelper.PickOneOfCompact(1, 5, 2, 3, 3, 1);
                        equipTableProps[3] = FormulaHelper.PickOneOfCompact(2, 6, 3, 2, 4, 1);
                        equipTableProps[4] = FormulaHelper.PickOneOfCompact(2, 6, 3, 2, 4, 1);
                        equipTableProps[5] = FormulaHelper.PickOneOfCompact(0, 7, 1, 4, 2, 1);
                        equipTableProps[6] = FormulaHelper.PickOneOfCompact(0, 5, 1, 1);
                        equipTableProps[8] = FormulaHelper.PickOneOfCompact(0, 5, 1, 1);
                        equipTableProps[9] = 1;
                        equipTableProps[10] = 1;
                        break;
                    case (int)ClassCareers.Barbarian:
                        equipTableProps[0] = UnityEngine.Random.Range(6 * level, 13 * (int)Mathf.Ceil(level / 2) + 1);
                        equipTableProps[9] = 1;
                        equipTableProps[10] = 1;
                        break;
                    case (int)ClassCareers.Warrior:
                        equipTableProps[0] = UnityEngine.Random.Range(8 * level, 17 * (int)Mathf.Ceil(level / 2) + 1);
                        equipTableProps[9] = 1;
                        equipTableProps[10] = 1;
                        break;
                    case (int)ClassCareers.Knight:
                        equipTableProps[0] = UnityEngine.Random.Range(7 * level, 15 * (int)Mathf.Ceil(level / 2) + 1);
                        equipTableProps[5] = FormulaHelper.PickOneOfCompact(0, 6, 1, 1);
                        equipTableProps[8] = FormulaHelper.PickOneOfCompact(0, 6, 1, 1);
                        equipTableProps[9] = 1;
                        equipTableProps[10] = 1;
                        break;
                    default:
                        return equipTableProps;
                }
            }
            else
            {
                switch (AITarget.CareerIndex)
                {
                    case 0:
                    case 3:
                        equipTableProps[4] = UnityEngine.Random.Range(0, 7 + 1);
                        break;
                    case 4:
                    case 5:
                    case 11:
                        equipTableProps[4] = UnityEngine.Random.Range(0, 12 + 1);
                        break;
                    case 6:
                        equipTableProps[4] = UnityEngine.Random.Range(0, 4 + 1);
                        break;
                    case 20:
                        equipTableProps[4] = 1;
                        break;
                    case 1:
                        equipTableProps[0] = UnityEngine.Random.Range(0, 6 + 1);
                        equipTableProps[1] = FormulaHelper.PickOneOfCompact(0, 3, 1, 1);
                        equipTableProps[2] = FormulaHelper.PickOneOfCompact(0, 3, 1, 1);
                        equipTableProps[3] = FormulaHelper.PickOneOfCompact(0, 3, 1, 1);
                        equipTableProps[7] = FormulaHelper.PickOneOfCompact(0, 9, 1, 3, 2, 1);
                        break;
                    case 2:
                        equipTableProps[1] = UnityEngine.Random.Range(2, 8 + 1);
                        equipTableProps[2] = UnityEngine.Random.Range(1, 5 + 1);
                        equipTableProps[3] = UnityEngine.Random.Range(1, 3 + 1);
                        break;
                    case 8:
                        equipTableProps[0] = UnityEngine.Random.Range(0, 18 + 1);
                        break;
                    case 10:
                        equipTableProps[2] = UnityEngine.Random.Range(2, 6 + 1);
                        equipTableProps[5] = FormulaHelper.PickOneOfCompact(1, 5, 2, 3, 3, 1);
                        break;
                    case 13:
                        equipTableProps[5] = UnityEngine.Random.Range(3, 10 + 1);
                        break;
                    case 16:
                        equipTableProps[0] = UnityEngine.Random.Range(0, 31 + 1);
                        equipTableProps[5] = UnityEngine.Random.Range(1, 5 + 1);
                        break;
                    case 22:
                        equipTableProps[1] = UnityEngine.Random.Range(0, 3 + 1);
                        equipTableProps[7] = UnityEngine.Random.Range(2, 7 + 1);
                        break;
                    case 34:
                        equipTableProps[4] = UnityEngine.Random.Range(2, 6 + 1);
                        equipTableProps[5] = UnityEngine.Random.Range(1, 3 + 1);
                        break;
                    case 40:
                        equipTableProps[4] = UnityEngine.Random.Range(3, 8 + 1);
                        equipTableProps[5] = UnityEngine.Random.Range(3, 10 + 1);
                        break;
                    case 41:
                        equipTableProps[4] = UnityEngine.Random.Range(0, 4 + 1);
                        break;
                    case 42:
                        equipTableProps[5] = UnityEngine.Random.Range(3, 7 + 1);
                        break;
                    case 7:
                        equipTableProps[0] = UnityEngine.Random.Range(0, 14 + 1);
                        equipTableProps[5] = UnityEngine.Random.Range(1, 2 + 1);
                        equipTableProps[9] = 1;
                        break;
                    case 12:
                        equipTableProps[0] = UnityEngine.Random.Range(0, 27 + 1);
                        equipTableProps[5] = UnityEngine.Random.Range(1, 2 + 1);
                        equipTableProps[9] = 1;
                        break;
                    case 21:
                        equipTableProps[0] = UnityEngine.Random.Range(0, 20 + 1);
                        equipTableProps[1] = FormulaHelper.PickOneOfCompact(0, 4, 1, 2, 2, 1);
                        equipTableProps[2] = FormulaHelper.PickOneOfCompact(1, 4, 2, 1);
                        equipTableProps[3] = FormulaHelper.PickOneOfCompact(1, 5, 2, 2, 3, 1);
                        equipTableProps[4] = FormulaHelper.PickOneOfCompact(0, 4, 1, 1);
                        equipTableProps[5] = UnityEngine.Random.Range(1, 2 + 1);
                        equipTableProps[6] = FormulaHelper.PickOneOfCompact(0, 5, 1, 2, 2, 1);
                        equipTableProps[7] = FormulaHelper.PickOneOfCompact(0, 7, 1, 1);
                        equipTableProps[9] = 1;
                        equipTableProps[10] = 1;
                        break;
                    case 24:
                        equipTableProps[0] = UnityEngine.Random.Range(0, 57 + 1);
                        equipTableProps[5] = UnityEngine.Random.Range(1, 2 + 1);
                        equipTableProps[9] = 1;
                        equipTableProps[10] = 1;
                        break;
                    case 9:
                        equipTableProps[5] = UnityEngine.Random.Range(2, 5 + 1);
                        break;
                    case 14:
                        equipTableProps[5] = FormulaHelper.PickOneOfCompact(0, 1, 1, 2, 2, 1);
                        break;
                    case 35:
                        equipTableProps[7] = UnityEngine.Random.Range(2, 9 + 1);
                        break;
                    case 36:
                        equipTableProps[7] = UnityEngine.Random.Range(2, 9 + 1);
                        break;
                    case 37:
                        equipTableProps[6] = UnityEngine.Random.Range(1, 5 + 1);
                        break;
                    case 38:
                        equipTableProps[6] = UnityEngine.Random.Range(2, 7 + 1);
                        break;
                    case 15:
                        equipTableProps[9] = 1;
                        break;
                    case 17:
                        equipTableProps[9] = 1;
                        break;
                    case 18:
                        equipTableProps[5] = UnityEngine.Random.Range(1, 2 + 1);
                        break;
                    case 19:
                        equipTableProps[5] = UnityEngine.Random.Range(1, 3 + 1);
                        break;
                    case 23:
                        equipTableProps[5] = UnityEngine.Random.Range(2, 5 + 1);
                        break;
                    case 28:
                        equipTableProps[0] = UnityEngine.Random.Range(0, 28 + 1);
                        equipTableProps[4] = UnityEngine.Random.Range(0, 3 + 1);
                        equipTableProps[5] = FormulaHelper.PickOneOfCompact(0, 5, 1, 1);
                        equipTableProps[8] = FormulaHelper.PickOneOfCompact(0, 6, 1, 1);
                        equipTableProps[9] = 1;
                        break;
                    case 30:
                        equipTableProps[0] = UnityEngine.Random.Range(0, 68 + 1);
                        equipTableProps[4] = UnityEngine.Random.Range(0, 3 + 1);
                        equipTableProps[5] = FormulaHelper.PickOneOfCompact(0, 4, 1, 3, 2, 1);
                        equipTableProps[8] = FormulaHelper.PickOneOfCompact(1, 6, 2, 1);
                        equipTableProps[9] = 1;
                        break;
                    case 32:
                        equipTableProps[0] = UnityEngine.Random.Range(0, 34 + 1);
                        equipTableProps[5] = UnityEngine.Random.Range(2, 5 + 1);
                        equipTableProps[8] = FormulaHelper.PickOneOfCompact(2, 8, 3, 1);
                        equipTableProps[9] = 1;
                        break;
                    case 33:
                        equipTableProps[0] = UnityEngine.Random.Range(0, 59 + 1);
                        equipTableProps[5] = UnityEngine.Random.Range(3, 6 + 1);
                        equipTableProps[8] = FormulaHelper.PickOneOfCompact(2, 11, 3, 4, 4, 1);
                        equipTableProps[9] = 1;
                        break;
                    case 25:
                        equipTableProps[5] = 1;
                        equipTableProps[6] = UnityEngine.Random.Range(1, 5 + 1);
                        break;
                    case 26:
                        equipTableProps[5] = 1;
                        equipTableProps[7] = UnityEngine.Random.Range(1, 5 + 1);
                        break;
                    case 27:
                        equipTableProps[4] = UnityEngine.Random.Range(0, 7 + 1);
                        equipTableProps[5] = 1;
                        equipTableProps[9] = 1;
                        break;
                    case 29:
                        equipTableProps[5] = 1;
                        equipTableProps[6] = UnityEngine.Random.Range(0, 3 + 1);
                        equipTableProps[9] = 1;
                        equipTableProps[10] = 1;
                        break;
                    case 31:
                        equipTableProps[5] = 1;
                        equipTableProps[6] = UnityEngine.Random.Range(0, 4 + 1);
                        equipTableProps[9] = 1;
                        equipTableProps[10] = 1;
                        break;
                    default:
                        return equipTableProps;
                }
            }

            return equipTableProps;
        }

        public static int[] EnemyExtraLootCalculator(DaggerfallEntity enemy, int[] traits, int[] predefLootProps)
        {
            // Index meanings: 0 = arrows, 1 = potions, 2 = gems, 3 = magicItems, 4 = foodItems, 5 = lightSources, 6 = religiousItems, 7 = bandages, 8 = repairTools, 9 = drugs, 10 = extraWeapons, 11 = maps. 
            // Main variable used in determining enemy loot/equipment values for purposes of targeted loot generation based on many context related variables.
            int[] extraLootProps = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            EnemyEntity AITarget = enemy as EnemyEntity;
            int level = enemy.Level;

            if (AITarget.EntityType == EntityTypes.EnemyClass)
            {
                switch (AITarget.CareerIndex)
                {
                    case (int)ClassCareers.Mage:
                        extraLootProps[1] = FormulaHelper.PickOneOfCompact(0, 4, 1, 3, 2, 1);
                        extraLootProps[3] = FormulaHelper.PickOneOfCompact(0, 12, 1, 1);
                        extraLootProps[4] = FormulaHelper.PickOneOfCompact(1, 3, 2, 1);
                        extraLootProps[5] = FormulaHelper.PickOneOfCompact(1, 3, 2, 1);
                        extraLootProps[11] = FormulaHelper.PickOneOfCompact(0, 98, 1, 1);
                        break;
                    case (int)ClassCareers.Spellsword:
                        extraLootProps[1] = FormulaHelper.PickOneOfCompact(0, 4, 1, 3, 2, 1);
                        extraLootProps[3] = FormulaHelper.PickOneOfCompact(0, 18, 1, 1);
                        extraLootProps[4] = FormulaHelper.PickOneOfCompact(1, 3, 2, 1);
                        extraLootProps[5] = FormulaHelper.PickOneOfCompact(1, 3, 2, 1);
                        extraLootProps[11] = FormulaHelper.PickOneOfCompact(0, 98, 1, 1);
                        break;
                    case (int)ClassCareers.Battlemage:
                        extraLootProps[1] = FormulaHelper.PickOneOfCompact(0, 4, 1, 3, 2, 1);
                        extraLootProps[3] = FormulaHelper.PickOneOfCompact(0, 18, 1, 1);
                        extraLootProps[4] = FormulaHelper.PickOneOfCompact(1, 3, 2, 1);
                        extraLootProps[5] = FormulaHelper.PickOneOfCompact(1, 3, 2, 1);
                        extraLootProps[11] = FormulaHelper.PickOneOfCompact(0, 98, 1, 1);
                        break;
                    case (int)ClassCareers.Sorcerer:
                        extraLootProps[1] = FormulaHelper.PickOneOfCompact(1, 8, 2, 3, 3, 1);
                        extraLootProps[3] = FormulaHelper.PickOneOfCompact(0, 18, 1, 1);
                        extraLootProps[4] = FormulaHelper.PickOneOfCompact(1, 3, 2, 1);
                        extraLootProps[5] = FormulaHelper.PickOneOfCompact(1, 3, 2, 1);
                        extraLootProps[11] = FormulaHelper.PickOneOfCompact(0, 98, 1, 1);
                        break;
                    case (int)ClassCareers.Healer:
                        extraLootProps[1] = FormulaHelper.PickOneOfCompact(1, 7, 2, 3);
                        extraLootProps[4] = FormulaHelper.PickOneOfCompact(1, 3, 2, 1);
                        extraLootProps[5] = FormulaHelper.PickOneOfCompact(1, 3, 2, 1);
                        extraLootProps[6] = UnityEngine.Random.Range(1, 3 + 1);
                        extraLootProps[7] = UnityEngine.Random.Range(4, 7 + 1);
                        extraLootProps[11] = FormulaHelper.PickOneOfCompact(0, 98, 1, 1);
                        break;
                    case (int)ClassCareers.Nightblade:
                        extraLootProps[1] = FormulaHelper.PickOneOfCompact(0, 2, 1, 3, 2, 1);
                        extraLootProps[4] = FormulaHelper.PickOneOfCompact(1, 3, 2, 1);
                        extraLootProps[5] = FormulaHelper.PickOneOfCompact(1, 3, 2, 1);
                        extraLootProps[10] = 1;
                        extraLootProps[11] = FormulaHelper.PickOneOfCompact(0, 98, 1, 1);
                        break;
                    case (int)ClassCareers.Bard:
                        extraLootProps[1] = FormulaHelper.PickOneOfCompact(0, 5, 1, 4, 2, 1);
                        extraLootProps[4] = FormulaHelper.PickOneOfCompact(2, 7, 3, 1);
                        extraLootProps[5] = FormulaHelper.PickOneOfCompact(1, 3, 2, 1);
                        extraLootProps[9] = FormulaHelper.PickOneOfCompact(0, 4, 1, 7, 2, 2);
                        extraLootProps[11] = FormulaHelper.PickOneOfCompact(0, 98, 1, 1);
                        break;
                    case (int)ClassCareers.Burglar:
                        extraLootProps[1] = FormulaHelper.PickOneOfCompact(0, 5, 1, 4, 2, 1);
                        extraLootProps[2] = FormulaHelper.PickOneOfCompact(0, 9, 1, 6, 2, 1);
                        extraLootProps[4] = FormulaHelper.PickOneOfCompact(1, 3, 2, 1);
                        extraLootProps[5] = FormulaHelper.PickOneOfCompact(1, 3, 2, 1);
                        extraLootProps[11] = FormulaHelper.PickOneOfCompact(0, 98, 1, 1);
                        break;
                    case (int)ClassCareers.Rogue:
                        extraLootProps[1] = FormulaHelper.PickOneOfCompact(0, 5, 1, 4, 2, 1);
                        extraLootProps[4] = FormulaHelper.PickOneOfCompact(1, 3, 2, 1);
                        extraLootProps[5] = FormulaHelper.PickOneOfCompact(1, 3, 2, 1);
                        extraLootProps[11] = FormulaHelper.PickOneOfCompact(0, 98, 1, 1);
                        break;
                    case (int)ClassCareers.Acrobat:
                        extraLootProps[1] = FormulaHelper.PickOneOfCompact(0, 5, 1, 4, 2, 1);
                        extraLootProps[4] = FormulaHelper.PickOneOfCompact(1, 3, 2, 1);
                        extraLootProps[5] = FormulaHelper.PickOneOfCompact(1, 3, 2, 1);
                        extraLootProps[11] = FormulaHelper.PickOneOfCompact(0, 98, 1, 1);
                        break;
                    case (int)ClassCareers.Thief:
                        extraLootProps[1] = FormulaHelper.PickOneOfCompact(0, 5, 1, 4, 2, 1);
                        extraLootProps[2] = FormulaHelper.PickOneOfCompact(0, 9, 1, 2);
                        extraLootProps[4] = FormulaHelper.PickOneOfCompact(1, 3, 2, 1);
                        extraLootProps[5] = FormulaHelper.PickOneOfCompact(1, 3, 2, 1);
                        extraLootProps[11] = FormulaHelper.PickOneOfCompact(0, 98, 1, 1);
                        break;
                    case (int)ClassCareers.Assassin:
                        extraLootProps[0] = UnityEngine.Random.Range(5, 16 + 1);
                        extraLootProps[1] = FormulaHelper.PickOneOfCompact(1, 11, 2, 5, 3, 1);
                        extraLootProps[4] = FormulaHelper.PickOneOfCompact(1, 3, 2, 1);
                        extraLootProps[5] = FormulaHelper.PickOneOfCompact(1, 3, 2, 1);
                        extraLootProps[9] = FormulaHelper.PickOneOfCompact(0, 7, 1, 5, 2, 1);
                        extraLootProps[11] = FormulaHelper.PickOneOfCompact(0, 98, 1, 1);
                        break;
                    case (int)ClassCareers.Monk:
                        extraLootProps[1] = FormulaHelper.PickOneOfCompact(0, 5, 1, 4, 2, 1);
                        extraLootProps[4] = FormulaHelper.PickOneOfCompact(0, 2, 1, 5);
                        extraLootProps[5] = FormulaHelper.PickOneOfCompact(1, 3, 2, 1);
                        extraLootProps[6] = UnityEngine.Random.Range(0, 2 + 1);
                        extraLootProps[11] = FormulaHelper.PickOneOfCompact(0, 98, 1, 1);
                        break;
                    case (int)ClassCareers.Archer:
                        extraLootProps[0] = UnityEngine.Random.Range(16, 35 + 1);
                        extraLootProps[1] = FormulaHelper.PickOneOfCompact(0, 5, 1, 4, 2, 1);
                        extraLootProps[4] = FormulaHelper.PickOneOfCompact(1, 3, 2, 1);
                        extraLootProps[5] = FormulaHelper.PickOneOfCompact(1, 3, 2, 1);
                        extraLootProps[8] = FormulaHelper.PickOneOfCompact(0, 6, 1, 1);
                        extraLootProps[11] = FormulaHelper.PickOneOfCompact(0, 98, 1, 1);
                        break;
                    case (int)ClassCareers.Ranger:
                        extraLootProps[0] = UnityEngine.Random.Range(9, 26 + 1);
                        extraLootProps[1] = FormulaHelper.PickOneOfCompact(0, 2, 1, 6, 2, 1);
                        extraLootProps[4] = FormulaHelper.PickOneOfCompact(2, 7, 3, 1);
                        extraLootProps[5] = FormulaHelper.PickOneOfCompact(1, 3, 2, 1);
                        extraLootProps[8] = FormulaHelper.PickOneOfCompact(0, 6, 1, 1);
                        extraLootProps[11] = FormulaHelper.PickOneOfCompact(0, 98, 1, 1);
                        break;
                    case (int)ClassCareers.Barbarian:
                        extraLootProps[1] = FormulaHelper.PickOneOfCompact(0, 14, 1, 6, 2, 1);
                        extraLootProps[4] = FormulaHelper.PickOneOfCompact(1, 3, 2, 1);
                        extraLootProps[5] = FormulaHelper.PickOneOfCompact(1, 3, 2, 1);
                        extraLootProps[7] = UnityEngine.Random.Range(0, 5 + 1);
                        extraLootProps[8] = FormulaHelper.PickOneOfCompact(0, 10, 1, 1);
                        extraLootProps[11] = FormulaHelper.PickOneOfCompact(0, 98, 1, 1);
                        break;
                    case (int)ClassCareers.Warrior:
                        extraLootProps[0] = UnityEngine.Random.Range(5, 20 + 1);
                        extraLootProps[1] = FormulaHelper.PickOneOfCompact(0, 5, 1, 4, 2, 1);
                        extraLootProps[4] = FormulaHelper.PickOneOfCompact(1, 3, 2, 1);
                        extraLootProps[5] = FormulaHelper.PickOneOfCompact(1, 3, 2, 1);
                        extraLootProps[8] = FormulaHelper.PickOneOfCompact(0, 4, 1, 1);
                        extraLootProps[11] = FormulaHelper.PickOneOfCompact(0, 98, 1, 1);
                        break;
                    case (int)ClassCareers.Knight:
                        extraLootProps[1] = FormulaHelper.PickOneOfCompact(0, 5, 1, 4, 2, 1);
                        extraLootProps[4] = FormulaHelper.PickOneOfCompact(1, 3, 2, 1);
                        extraLootProps[5] = FormulaHelper.PickOneOfCompact(1, 3, 2, 1);
                        extraLootProps[6] = UnityEngine.Random.Range(0, 1 + 1);
                        extraLootProps[8] = FormulaHelper.PickOneOfCompact(0, 3, 1, 1);
                        extraLootProps[11] = FormulaHelper.PickOneOfCompact(0, 98, 1, 1);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (AITarget.CareerIndex)
                {
                    case 8:
                        extraLootProps[1] = FormulaHelper.PickOneOfCompact(0, 12, 1, 5);
                        extraLootProps[4] = FormulaHelper.PickOneOf(0, 1);
                        extraLootProps[5] = FormulaHelper.PickOneOf(0, 1);
                        extraLootProps[11] = FormulaHelper.PickOneOfCompact(0, 98, 1, 1);
                        break;
                    case 7:
                        extraLootProps[1] = FormulaHelper.PickOneOfCompact(0, 12, 1, 5);
                        extraLootProps[4] = FormulaHelper.PickOneOf(0, 1);
                        extraLootProps[5] = FormulaHelper.PickOneOf(0, 1);
                        extraLootProps[8] = FormulaHelper.PickOneOfCompact(0, 4, 1, 1);
                        extraLootProps[11] = FormulaHelper.PickOneOfCompact(0, 98, 1, 1);
                        break;
                    case 12:
                        extraLootProps[1] = FormulaHelper.PickOneOfCompact(0, 10, 1, 6, 2, 1);
                        extraLootProps[4] = FormulaHelper.PickOneOf(0, 1);
                        extraLootProps[5] = FormulaHelper.PickOneOf(0, 1);
                        extraLootProps[8] = FormulaHelper.PickOneOfCompact(0, 3, 1, 1);
                        extraLootProps[11] = FormulaHelper.PickOneOfCompact(0, 98, 1, 1);
                        break;
                    case 21:
                        extraLootProps[1] = FormulaHelper.PickOneOfCompact(1, 8, 2, 3, 3, 1);
                        extraLootProps[4] = FormulaHelper.PickOneOf(0, 1);
                        extraLootProps[5] = FormulaHelper.PickOneOf(0, 1);
                        extraLootProps[6] = UnityEngine.Random.Range(1, 3 + 1);
                        extraLootProps[11] = FormulaHelper.PickOneOfCompact(0, 98, 1, 1);
                        break;
                    case 24:
                        extraLootProps[1] = FormulaHelper.PickOneOfCompact(0, 4, 1, 8, 2, 1);
                        extraLootProps[2] = FormulaHelper.PickOneOfCompact(0, 2, 1, 6, 2, 1);
                        extraLootProps[4] = FormulaHelper.PickOneOf(0, 1, 2);
                        extraLootProps[5] = FormulaHelper.PickOneOf(0, 1);
                        extraLootProps[8] = FormulaHelper.PickOneOf(0, 0, 1);
                        extraLootProps[11] = FormulaHelper.PickOneOfCompact(0, 98, 1, 1);
                        break;
                    case 28:
                        extraLootProps[1] = FormulaHelper.PickOneOfCompact(0, 11, 1, 6);
                        extraLootProps[11] = FormulaHelper.PickOneOfCompact(0, 98, 1, 1);
                        break;
                    case 30:
                        extraLootProps[1] = FormulaHelper.PickOneOfCompact(0, 4, 1, 9, 2, 1);
                        extraLootProps[11] = FormulaHelper.PickOneOfCompact(0, 98, 1, 1);
                        break;
                    case 29:
                    case 31:
                        extraLootProps[2] = FormulaHelper.PickOneOfCompact(0, 2, 1, 4, 2, 3, 3, 1);
                        extraLootProps[3] = FormulaHelper.PickOneOfCompact(0, 15, 1, 1);
                        break;
                    default:
                        break;
                }
            }

            int locationContext = DetermineEnemyLocationContext(enemy); // A very basic implementation of this "location context" specific loot modifier thing, but it should work for now. 
            if (locationContext == 0)
            {
                extraLootProps[4] = 0;
                extraLootProps[5] = 0;
            }

            return extraLootProps;
        }

        public static int DetermineEnemyLocationContext(DaggerfallEntity enemy)
        {
            // -1 = Nothing, 0 = Resident, 1 = Raider.
            MobileTeams team = enemy.Team;
            int playerLocation = GameManager.Instance.PlayerGPS.CurrentLocationIndex;
            DFRegion.DungeonTypes dungType = DFRegion.DungeonTypes.NoDungeon;
            if (playerLocation > -1)
                dungType = GameManager.Instance.PlayerGPS.CurrentRegion.MapTable[playerLocation].DungeonType;

            if (dungType != DFRegion.DungeonTypes.NoDungeon)
            {
                switch(dungType)
                {
                    case DFRegion.DungeonTypes.Mine:
                    case DFRegion.DungeonTypes.NaturalCave:
                    case DFRegion.DungeonTypes.Coven:
                    case DFRegion.DungeonTypes.HarpyNest:
                    case DFRegion.DungeonTypes.RuinedCastle:
                    case DFRegion.DungeonTypes.SpiderNest:
                    case DFRegion.DungeonTypes.GiantStronghold:
                    case DFRegion.DungeonTypes.VolcanicCaves:
                    case DFRegion.DungeonTypes.ScorpionNest:
                        return 1;
                    case DFRegion.DungeonTypes.HumanStronghold:
                    case DFRegion.DungeonTypes.DesecratedTemple:
                    case DFRegion.DungeonTypes.Laboratory:
                        if (team == MobileTeams.KnightsAndMages)
                            return 0;
                        else
                            return 1;
                    case DFRegion.DungeonTypes.Prison:
                    case DFRegion.DungeonTypes.BarbarianStronghold:
                        if (team == MobileTeams.Criminals)
                            return 0;
                        else
                            return 1;
                    case DFRegion.DungeonTypes.Crypt:
                    case DFRegion.DungeonTypes.VampireHaunt:
                    case DFRegion.DungeonTypes.Cemetery:
                        if (team == MobileTeams.Undead)
                            return 0;
                        else
                            return 1;
                    case DFRegion.DungeonTypes.OrcStronghold:
                        if (team == MobileTeams.Orcs)
                            return 0;
                        else
                            return 1;
                    case DFRegion.DungeonTypes.DragonsDen:
                        if (team == MobileTeams.Dragonlings)
                            return 0;
                        else
                            return 1;
                    default:
                        return -1;
                }
            }

            return -1;
        }

        public static int[] EnemyPersonalityTraitGenerator(DaggerfallEntity enemy) // Array Index 0 and 1 are both Quirks, Index 2 is an Interest.
        {
            int[] traits = { -1, -1, -1 };
            EnemyEntity Enemy = enemy as EnemyEntity;

            if (Enemy.EntityType == EntityTypes.EnemyClass)
            {
                switch (Enemy.CareerIndex)
                {
                    case (int)ClassCareers.Mage:
                        traits[0] = FormulaHelper.PickOneOfCompact(-1, 20, 0, 1, 1, 1, 2, 1, 3, 2, 4, 3, 5, 1, 6, 2, 7, 1, 8, 2, 9, 1, 10, 1, 11, 1, 12, 1, 13, 1, 14, 1, 16, 1, 17, 1);
                        traits[1] = FormulaHelper.PickOneOfCompact(-1, 20, 0, 1, 1, 1, 2, 1, 3, 2, 4, 3, 5, 1, 6, 2, 7, 1, 8, 2, 9, 1, 10, 1, 11, 1, 12, 1, 13, 1, 14, 1, 16, 1, 17, 1);
                        traits[2] = FormulaHelper.PickOneOfCompact(-1, 17, 1, 1, 2, 1, 3, 1, 4, 1, 5, 2, 8, 1, 9, 1, 10, 1, 11, 2, 12, 2, 15, 2, 16, 2);
                        break;
                    case (int)ClassCareers.Spellsword:
                    case (int)ClassCareers.Battlemage:
                    case (int)ClassCareers.Sorcerer:
                        traits[0] = FormulaHelper.PickOneOfCompact(-1, 20, 0, 1, 1, 1, 2, 2, 3, 1, 4, 2, 5, 1, 6, 2, 7, 1, 8, 1, 9, 1, 10, 1, 11, 1, 12, 1, 13, 1, 14, 1, 15, 1, 16, 1, 17, 1);
                        traits[1] = FormulaHelper.PickOneOfCompact(-1, 20, 0, 1, 1, 1, 2, 2, 3, 1, 4, 2, 5, 1, 6, 2, 7, 1, 8, 1, 9, 1, 10, 1, 11, 1, 12, 1, 13, 1, 14, 1, 15, 1, 16, 1, 17, 1);
                        traits[2] = FormulaHelper.PickOneOfCompact(-1, 17, 1, 2, 2, 1, 3, 1, 4, 1, 5, 2, 6, 1, 8, 1, 9, 2, 10, 1, 11, 2, 12, 1, 14, 1, 15, 2, 16, 1, 17, 1);
                        break;
                    case (int)ClassCareers.Healer:
                    case (int)ClassCareers.Monk:
                        traits[0] = FormulaHelper.PickOneOfCompact(-1, 20, 0, 2, 1, 1, 2, 2, 3, 2, 4, 1, 6, 2, 7, 1, 12, 3, 15, 1, 17, 1);
                        traits[1] = FormulaHelper.PickOneOfCompact(-1, 20, 0, 2, 1, 1, 2, 2, 3, 2, 4, 1, 6, 2, 7, 1, 12, 3, 15, 1, 17, 1);
                        traits[2] = FormulaHelper.PickOneOfCompact(-1, 17, 0, 3, 3, 1, 4, 2, 6, 1, 9, 2, 10, 2, 11, 2, 12, 1, 13, 1, 14, 1, 15, 1, 16, 2);
                        break;
                    case (int)ClassCareers.Nightblade:
                        traits[0] = FormulaHelper.PickOneOfCompact(-1, 20, 0, 3, 1, 1, 2, 2, 4, 1, 5, 1, 7, 3, 8, 1, 9, 1, 10, 1, 11, 1, 12, 1, 13, 2, 14, 1, 15, 1, 16, 1, 17, 1);
                        traits[1] = FormulaHelper.PickOneOfCompact(-1, 20, 0, 3, 1, 1, 2, 2, 4, 1, 5, 1, 7, 3, 8, 1, 9, 1, 10, 1, 11, 1, 12, 1, 13, 2, 14, 1, 15, 1, 16, 1, 17, 1);
                        traits[2] = FormulaHelper.PickOneOfCompact(-1, 17, 0, 1, 1, 1, 3, 1, 4, 1, 5, 1, 6, 1, 7, 1, 8, 2, 9, 2, 10, 2, 11, 1, 12, 1, 13, 1, 14, 1, 15, 2, 16, 1, 17, 2);
                        break;
                    case (int)ClassCareers.Bard:
                        traits[0] = FormulaHelper.PickOneOfCompact(-1, 20, 0, 1, 1, 2, 2, 1, 3, 1, 4, 1, 5, 2, 6, 1, 7, 1, 8, 1, 9, 2, 10, 1, 11, 2, 12, 1, 13, 1, 14, 1, 15, 2, 16, 1, 17, 3);
                        traits[1] = FormulaHelper.PickOneOfCompact(-1, 20, 0, 1, 1, 2, 2, 1, 3, 1, 4, 1, 5, 2, 6, 1, 7, 1, 8, 1, 9, 2, 10, 1, 11, 2, 12, 1, 13, 1, 14, 1, 15, 2, 16, 1, 17, 3);
                        traits[2] = FormulaHelper.PickOneOfCompact(-1, 17, 0, 1, 1, 1, 2, 1, 3, 3, 4, 2, 5, 1, 8, 2, 9, 1, 10, 1, 11, 1, 13, 1, 14, 1, 15, 1, 16, 1);
                        break;
                    case (int)ClassCareers.Burglar:
                    case (int)ClassCareers.Thief:
                        traits[0] = FormulaHelper.PickOneOfCompact(-1, 20, 0, 1, 1, 2, 2, 1, 3, 1, 4, 1, 5, 1, 7, 2, 8, 1, 9, 2, 10, 3, 11, 1, 12, 1, 13, 2, 14, 1, 15, 2, 16, 1, 17, 1);
                        traits[1] = FormulaHelper.PickOneOfCompact(-1, 20, 0, 1, 1, 2, 2, 1, 3, 1, 4, 1, 5, 1, 7, 2, 8, 1, 9, 2, 10, 3, 11, 1, 12, 1, 13, 2, 14, 1, 15, 2, 16, 1, 17, 1);
                        traits[2] = FormulaHelper.PickOneOfCompact(-1, 17, 2, 2, 3, 1, 4, 1, 5, 2, 6, 1, 7, 1, 8, 2, 9, 1, 10, 1, 13, 1, 14, 1, 15, 1, 17, 1);
                        break;
                    case (int)ClassCareers.Rogue:
                        traits[0] = FormulaHelper.PickOneOfCompact(-1, 20, 0, 1, 1, 2, 2, 1, 4, 1, 5, 1, 6, 1, 7, 1, 8, 1, 9, 2, 10, 2, 11, 2, 12, 1, 13, 2, 14, 2, 15, 2, 16, 1, 17, 1);
                        traits[1] = FormulaHelper.PickOneOfCompact(-1, 20, 0, 1, 1, 2, 2, 1, 4, 1, 5, 1, 6, 1, 7, 1, 8, 1, 9, 2, 10, 2, 11, 2, 12, 1, 13, 2, 14, 2, 15, 2, 16, 1, 17, 1);
                        traits[2] = FormulaHelper.PickOneOfCompact(-1, 17, 2, 1, 3, 1, 4, 1, 5, 2, 6, 1, 7, 1, 8, 2, 9, 2, 10, 1, 13, 1, 14, 1, 15, 1, 17, 1);
                        break;
                    case (int)ClassCareers.Acrobat:
                        traits[0] = FormulaHelper.PickOneOfCompact(-1, 20, 0, 1, 1, 2, 4, 1, 5, 2, 6, 1, 7, 1, 8, 1, 9, 2, 10, 1, 11, 1, 12, 1, 13, 1, 14, 1, 15, 2, 16, 1, 17, 1);
                        traits[1] = FormulaHelper.PickOneOfCompact(-1, 20, 0, 1, 1, 2, 4, 1, 5, 2, 6, 1, 7, 1, 8, 1, 9, 2, 10, 1, 11, 1, 12, 1, 13, 1, 14, 1, 15, 2, 16, 1, 17, 1);
                        traits[2] = FormulaHelper.PickOneOfCompact(-1, 17, 0, 1, 1, 1, 2, 3, 3, 1, 4, 2, 5, 1, 6, 1, 7, 1, 8, 2, 9, 1, 10, 1, 13, 1, 14, 2, 15, 1, 17, 1);
                        break;
                    case (int)ClassCareers.Assassin:
                        traits[0] = FormulaHelper.PickOneOfCompact(-1, 20, 0, 3, 1, 1, 2, 1, 4, 1, 5, 1, 7, 2, 8, 1, 9, 1, 10, 2, 11, 1, 12, 1, 13, 1, 14, 3, 15, 1, 16, 2, 17, 1);
                        traits[1] = FormulaHelper.PickOneOfCompact(-1, 20, 0, 3, 1, 1, 2, 1, 4, 1, 5, 1, 7, 2, 8, 1, 9, 1, 10, 2, 11, 1, 12, 1, 13, 1, 14, 3, 15, 1, 16, 2, 17, 1);
                        traits[2] = FormulaHelper.PickOneOfCompact(-1, 17, 1, 2, 3, 1, 4, 1, 5, 2, 6, 2, 7, 2, 8, 2, 9, 3, 14, 1, 15, 1, 16, 1, 17, 1);
                        break;
                    case (int)ClassCareers.Ranger:
                        traits[0] = FormulaHelper.PickOneOfCompact(-1, 20, 0, 2, 1, 1, 2, 1, 3, 1, 4, 1, 5, 1, 6, 1, 7, 1, 8, 1, 10, 1, 11, 1, 12, 1, 13, 1, 14, 1, 15, 2, 16, 1, 17, 1);
                        traits[1] = FormulaHelper.PickOneOfCompact(-1, 20, 0, 2, 1, 1, 2, 1, 3, 1, 4, 1, 5, 1, 6, 1, 7, 1, 8, 1, 10, 1, 11, 1, 12, 1, 13, 1, 14, 1, 15, 2, 16, 1, 17, 1);
                        traits[2] = FormulaHelper.PickOneOfCompact(-1, 17, 0, 1, 1, 1, 2, 1, 3, 1, 4, 1, 5, 1, 6, 3, 7, 3, 8, 1, 9, 1, 10, 2, 12, 1, 13, 2, 14, 2, 17, 1);
                        break;
                    case (int)ClassCareers.Barbarian:
                        traits[0] = FormulaHelper.PickOneOfCompact(-1, 20, 0, 1, 1, 3, 5, 2, 6, 1, 7, 1, 10, 2, 11, 2, 13, 2, 14, 1, 15, 1, 16, 1, 17, 3);
                        traits[1] = FormulaHelper.PickOneOfCompact(-1, 20, 0, 1, 1, 3, 5, 2, 6, 1, 7, 1, 10, 2, 11, 2, 13, 2, 14, 1, 15, 1, 16, 1, 17, 3);
                        traits[2] = FormulaHelper.PickOneOfCompact(-1, 17, 1, 2, 2, 2, 3, 1, 4, 1, 5, 1, 6, 2, 7, 2, 8, 1, 13, 2, 14, 2, 17, 1);
                        break;
                    case (int)ClassCareers.Archer:
                    case (int)ClassCareers.Warrior:
                        traits[0] = FormulaHelper.PickOneOfCompact(-1, 20, 0, 2, 1, 1, 2, 2, 3, 1, 5, 1, 6, 1, 7, 1, 8, 1, 9, 1, 10, 2, 11, 2, 12, 1, 13, 1, 14, 1, 15, 1, 16, 1, 17, 2);
                        traits[1] = FormulaHelper.PickOneOfCompact(-1, 20, 0, 2, 1, 1, 2, 2, 3, 1, 5, 1, 6, 1, 7, 1, 8, 1, 9, 1, 10, 2, 11, 2, 12, 1, 13, 1, 14, 1, 15, 1, 16, 1, 17, 2);
                        traits[2] = FormulaHelper.PickOneOfCompact(-1, 17, 0, 1, 1, 1, 2, 1, 3, 1, 4, 1, 5, 1, 6, 1, 7, 2, 8, 1, 9, 1, 10, 1, 13, 2, 14, 1, 17, 3);
                        break;
                    case (int)ClassCareers.Knight:
                        traits[0] = FormulaHelper.PickOneOfCompact(-1, 20, 0, 2, 1, 1, 2, 2, 3, 1, 6, 1, 8, 1, 9, 2, 10, 1, 11, 1, 12, 1, 13, 1, 15, 2, 17, 1);
                        traits[1] = FormulaHelper.PickOneOfCompact(-1, 20, 0, 2, 1, 1, 2, 2, 3, 1, 6, 1, 8, 1, 9, 2, 10, 1, 11, 1, 12, 1, 13, 1, 15, 2, 17, 1);
                        traits[2] = FormulaHelper.PickOneOfCompact(-1, 17, 0, 2, 2, 1, 3, 2, 4, 1, 5, 1, 6, 1, 7, 1, 8, 1, 10, 1, 13, 1, 14, 1, 16, 2, 17, 2);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (Enemy.CareerIndex)
                {
                    case 8:
                        traits[0] = FormulaHelper.PickOneOfCompact(-1, 30, 0, 1, 1, 2, 2, 1, 5, 1, 6, 1, 8, 1, 9, 1, 10, 1, 11, 2, 13, 1, 14, 1, 17, 1);
                        traits[1] = FormulaHelper.PickOneOfCompact(-1, 30, 0, 1, 1, 2, 2, 1, 5, 1, 6, 1, 8, 1, 9, 1, 10, 1, 11, 2, 13, 1, 14, 1, 17, 1);
                        traits[2] = FormulaHelper.PickOneOfCompact(-1, 26, 0, 1, 1, 1, 2, 1, 3, 1, 4, 1, 5, 1, 6, 1, 7, 2, 10, 2, 13, 2, 17, 1);
                        break;
                    case 16:
                        traits[0] = FormulaHelper.PickOneOfCompact(-1, 30, 1, 1, 2, 1, 5, 1, 6, 1, 7, 1, 8, 2, 9, 1, 10, 2, 11, 2, 14, 1, 16, 1, 17, 2);
                        traits[1] = FormulaHelper.PickOneOfCompact(-1, 30, 1, 1, 2, 1, 5, 1, 6, 1, 7, 1, 8, 2, 9, 1, 10, 2, 11, 2, 14, 1, 16, 1, 17, 2);
                        traits[2] = FormulaHelper.PickOneOfCompact(-1, 26, 2, 3, 5, 2, 6, 1, 7, 1, 10, 1, 13, 1, 14, 1);
                        break;
                    case 7:
                    case 12:
                    case 24:
                        traits[0] = FormulaHelper.PickOneOfCompact(-1, 30, 0, 1, 1, 2, 5, 1, 6, 1, 7, 1, 8, 1, 9, 2, 10, 2, 11, 2, 13, 1, 14, 1, 17, 2);
                        traits[1] = FormulaHelper.PickOneOfCompact(-1, 30, 0, 1, 1, 2, 5, 1, 6, 1, 7, 1, 8, 1, 9, 2, 10, 2, 11, 2, 13, 1, 14, 1, 17, 2);
                        traits[2] = FormulaHelper.PickOneOfCompact(-1, 26, 0, 1, 1, 1, 2, 1, 3, 1, 4, 1, 5, 1, 6, 1, 7, 1, 8, 1, 9, 1, 13, 1, 14, 1, 17, 3);
                        break;
                    case 21:
                        traits[0] = FormulaHelper.PickOneOfCompact(-1, 30, 0, 1, 1, 1, 2, 1, 3, 1, 4, 1, 5, 1, 6, 2, 7, 1, 8, 1, 9, 1, 10, 1, 11, 1, 12, 1, 13, 1, 14, 1, 17, 2);
                        traits[1] = FormulaHelper.PickOneOfCompact(-1, 30, 0, 1, 1, 1, 2, 1, 3, 1, 4, 1, 5, 1, 6, 2, 7, 1, 8, 1, 9, 1, 10, 1, 11, 1, 12, 1, 13, 1, 14, 1, 17, 2);
                        traits[2] = FormulaHelper.PickOneOfCompact(-1, 26, 0, 2, 1, 2, 2, 1, 3, 1, 4, 1, 5, 1, 6, 1, 8, 1, 9, 2, 10, 2, 11, 1, 13, 1, 15, 1, 17, 2);
                        break;
                    case 28:
                    case 30:
                        traits[0] = FormulaHelper.PickOneOfCompact(-1, 30, 0, 1, 1, 1, 2, 2, 3, 1, 4, 1, 7, 2, 8, 1, 9, 2, 10, 2, 11, 1, 12, 1, 13, 2, 14, 2, 15, 2, 16, 2);
                        traits[1] = FormulaHelper.PickOneOfCompact(-1, 30, 0, 1, 1, 1, 2, 2, 3, 1, 4, 1, 7, 2, 8, 1, 9, 2, 10, 2, 11, 1, 12, 1, 13, 2, 14, 2, 15, 2, 16, 2);
                        traits[2] = FormulaHelper.PickOneOfCompact(-1, 26, 1, 1, 3, 2, 4, 2, 5, 1, 6, 1, 7, 2, 8, 1, 9, 1, 11, 1, 12, 1, 14, 1, 15, 1, 16, 1, 17, 1);
                        break;
                    case 32:
                    case 33:
                        traits[0] = FormulaHelper.PickOneOfCompact(-1, 30, 0, 1, 1, 1, 2, 3, 3, 1, 4, 3, 7, 1, 8, 2, 9, 2, 10, 1, 13, 2, 14, 2);
                        traits[1] = FormulaHelper.PickOneOfCompact(-1, 30, 0, 1, 1, 1, 2, 3, 3, 1, 4, 3, 7, 1, 8, 2, 9, 2, 10, 1, 13, 2, 14, 2);
                        traits[2] = FormulaHelper.PickOneOfCompact(-1, 26, 4, 2, 5, 2, 9, 2, 10, 1, 11, 2, 12, 2, 14, 1, 15, 3, 16, 2);
                        break;
                    case 29:
                        traits[0] = FormulaHelper.PickOneOfCompact(-1, 30, 0, 2, 1, 1, 2, 2, 3, 1, 4, 1, 5, 1, 7, 1, 8, 1, 9, 3, 10, 1, 13, 1, 14, 2, 15, 3, 16, 1, 17, 1);
                        traits[1] = FormulaHelper.PickOneOfCompact(-1, 30, 0, 2, 1, 1, 2, 2, 3, 1, 4, 1, 5, 1, 7, 1, 8, 1, 9, 3, 10, 1, 13, 1, 14, 2, 15, 3, 16, 1, 17, 1);
                        traits[2] = FormulaHelper.PickOneOfCompact(-1, 26, 1, 1, 3, 2, 4, 2, 5, 1, 7, 2, 8, 2, 9, 1, 10, 1, 15, 1);
                        break;
                    case 31:
                        traits[0] = FormulaHelper.PickOneOfCompact(-1, 30, 0, 2, 1, 1, 4, 1, 5, 1, 7, 1, 8, 1, 9, 2, 10, 2, 13, 2, 14, 2, 15, 1, 16, 1, 17, 1);
                        traits[1] = FormulaHelper.PickOneOfCompact(-1, 30, 0, 2, 1, 1, 4, 1, 5, 1, 7, 1, 8, 1, 9, 2, 10, 2, 13, 2, 14, 2, 15, 1, 16, 1, 17, 1);
                        traits[2] = FormulaHelper.PickOneOfCompact(-1, 26, 1, 3, 3, 1, 4, 1, 5, 2, 7, 1, 8, 1, 9, 1, 11, 1, 15, 1, 17, 1);
                        break;
                    default:
                        break;
                }
            }

            if (IncompatiblePairs(traits[0], traits[1]))
                traits[1] = -1;

            return traits;
        }

        public static bool IncompatiblePairs(int q1, int q2) // Keeps quirk 1 and quirk 2 from being the same thing, or from being "incompatible" pairs like being a Glutton and Faster at the same time.
        {
            if (q1 == q2)
                return true;

            if ((q1 == (int)MobilePersonalityQuirks.Prepared && q2 == (int)MobilePersonalityQuirks.Reckless) || (q1 == (int)MobilePersonalityQuirks.Reckless && q2 == (int)MobilePersonalityQuirks.Prepared))
                return true;

            if ((q1 == (int)MobilePersonalityQuirks.Nyctophobic && q2 == (int)MobilePersonalityQuirks.Nyctophilic) || (q1 == (int)MobilePersonalityQuirks.Nyctophilic && q2 == (int)MobilePersonalityQuirks.Nyctophobic))
                return true;

            if ((q1 == (int)MobilePersonalityQuirks.Glutton && q2 == (int)MobilePersonalityQuirks.Faster) || (q1 == (int)MobilePersonalityQuirks.Faster && q2 == (int)MobilePersonalityQuirks.Glutton))
                return true;

            return false;
        }

        #endregion

        #region Helpers

        public static int CorpseTexture(int archive, int record)
        {
            return ((archive << 16) + record);
        }

        public static void ReverseCorpseTexture(int corpseTexture, out int archive, out int record)
        {
            archive = corpseTexture >> 16;
            record = corpseTexture & 0xffff;
        }

        /// <summary>
        /// Build a dictionary of enemies keyed by ID.
        /// Use this once and store for faster enemy lookups.
        /// </summary>
        /// <returns>Resulting dictionary of mobile enemies.</returns>
        public static Dictionary<int, MobileEnemy> BuildEnemyDict()
        {
            Dictionary<int, MobileEnemy> enemyDict = new Dictionary<int, MobileEnemy>();
            foreach (var enemy in Enemies)
            {
                enemyDict.Add(enemy.ID, enemy);
            }

            return enemyDict;
        }

        /// <summary>
        /// Gets enemy definition based on type.
        /// Runs a brute force search for ID, so use sparingly.
        /// Store a dictionary from GetEnemyDict() for faster lookups.
        /// </summary>
        /// <param name="enemyType">Enemy type to extract definition.</param>
        /// <param name="mobileEnemyOut">Receives details of enemy type.</param>
        /// <returns>True if successful.</returns>
        public static bool GetEnemy(MobileTypes enemyType, out MobileEnemy mobileEnemyOut)
        {
            // Cast type enum to ID.
            // You can add additional IDs to enum to create new enemies.
            int id = (int)enemyType;

            // Search for matching definition in enemy list.
            // Don't forget to add new enemy IDs to Enemies definition array.
            for (int i = 0; i < Enemies.Length; i++)
            {
                if (Enemies[i].ID == id)
                {
                    mobileEnemyOut = Enemies[i];
                    return true;
                }
            }

            // No match found, just return an empty definition
            mobileEnemyOut = new MobileEnemy();
            return false;
        }

        /// <summary>
        /// Gets enemy definition based on name.
        /// Runs a brute force search for ID, so use sparingly.
        /// </summary>
        /// <param name="name">Enemy name to extract definition.</param>
        /// <param name="mobileEnemyOut">Receives details of enemy type if found.</param>
        /// <returns>True if successful.</returns>
        public static bool GetEnemy(string name, out MobileEnemy mobileEnemyOut)
        {
            for (int i = 0; i < Enemies.Length; i++)
            {
                if (0 == string.Compare(TextManager.Instance.GetLocalizedEnemyName(Enemies[i].ID), name, StringComparison.InvariantCultureIgnoreCase))
                {
                    mobileEnemyOut = Enemies[i];
                    return true;
                }
            }

            // No match found, just return an empty definition
            mobileEnemyOut = new MobileEnemy();
            return false;
        }

        #endregion

    }
}
