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
using System.Collections;
using System.Collections.Generic;

namespace DaggerfallWorkshop.Game.Entity
{
    /// <summary>
    /// Daggerfall stats collection for every entity.
    /// </summary>
    [Serializable]
    public struct DaggerfallStats
    {
        const int defaultValue = 50;

        public int Strength;
        public int Intelligence;
        public int Willpower;
        public int Agility;
        public int Endurance;
        public int Personality;
        public int Speed;
        public int Luck;

        public void SetDefaults()
        {
            Strength = defaultValue;
            Intelligence = defaultValue;
            Willpower = defaultValue;
            Agility = defaultValue;
            Endurance = defaultValue;
            Personality = defaultValue;
            Speed = defaultValue;
            Luck = defaultValue;
        }

        public void Copy(DaggerfallStats other)
        {
            this.Strength = other.Strength;
            this.Intelligence = other.Intelligence;
            this.Willpower = other.Willpower;
            this.Agility = other.Agility;
            this.Endurance = other.Endurance;
            this.Personality = other.Personality;
            this.Speed = other.Speed;
            this.Luck = other.Luck;
        }

        public int GetStatValue(Stats stat)
        {
            switch (stat)
            {
                case Stats.Strength:
                    return this.Strength;
                case Stats.Intelligence:
                    return this.Intelligence;
                case Stats.Willpower:
                    return this.Willpower;
                case Stats.Agility:
                    return this.Agility;
                case Stats.Endurance:
                    return this.Endurance;
                case Stats.Personality:
                    return this.Personality;
                case Stats.Speed:
                    return this.Speed;
                case Stats.Luck:
                    return this.Luck;
                default:
                    return 0;
            }
        }

        public void SetStatValue(Stats stat, int value)
        {
            switch (stat)
            {
                case Stats.Strength:
                    this.Strength = value;
                    break;
                case Stats.Intelligence:
                    this.Intelligence = value;
                    break;
                case Stats.Willpower:
                    this.Willpower = value;
                    break;
                case Stats.Agility:
                    this.Agility = value;
                    break;
                case Stats.Endurance:
                    this.Endurance = value;
                    break;
                case Stats.Personality:
                    this.Personality = value;
                    break;
                case Stats.Speed:
                    this.Speed = value;
                    break;
                case Stats.Luck:
                    this.Luck = value;
                    break;
            }
        }

        public int GetStatValue(int index)
        {
            return GetStatValue((Stats)index);
        }

        public void SetStatValue(int index, int value)
        {
            SetStatValue((Stats)index, value);
        }
    }
}