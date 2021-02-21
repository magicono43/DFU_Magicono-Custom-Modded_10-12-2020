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
using UnityEngine;

namespace DaggerfallWorkshop.Game.MagicAndEffects.MagicEffects
{
    public class Moon_Sugar : DiseaseEffect
    {
        Diseases diseaseType = Diseases.Moon_Sugar;
        int tickCycleDelay = 8;

        public override void SetProperties()
        {
            properties.Key = GetClassicDiseaseEffectKey(diseaseType);
            properties.ShowSpellIcon = false;
            classicDiseaseType = diseaseType;
            diseaseData = new DiseaseData(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xFF, 0xFF, 25, 25); // Moon_Sugar (Drug)
            bypassSavingThrows = true;
        }

        public override TextFile.Token[] ContractedMessageTokens => DaggerfallUnity.Instance.TextProvider.CreateTokens(
                TextFile.Formatting.JustifyCenter,
                "You have recently consumed Moon Sugar, a",
                "potent narcotic, unless one has a high tolerance",
                "to it, such as most Khajiit. Those with a low",
                "tolerance experience ecstasy and a crash afterward.");

        protected override bool IsLikeKind(IncumbentEffect other)
        {
            return false;
        }

        protected override void UpdateDisease()
        {
            if (infectionCyclesLeft <= 0) // This will keep the effect active even after the cycles have passed, that way the stat damage is persistent, but the check will remove the disease when all stat damage from this effect are healed. 
            {
                infectionCyclesLeft = 0;

                if (AllAttributesHealed())
                    CureDisease();

                return;
            }

            // Do nothing if expiring out
            if (forcedRoundsRemaining == 0)
                return;

            infectionRoundCounter++;

            // Do nothing if disease has run its course
            if (infectionRoundCounter < tickCycleDelay)
                return;

            // Raise incubation over flag on first tick
            if (!incubationOver)
                incubationOver = true;

            // Reset infectionRoundCounter to 0 to start counting from start again
            infectionRoundCounter = 0;

            // Increment infectionCyclesPassed, in order to keep track of how long infection has been going on since start
            infectionCyclesPassed++;

            // Increment effects for this cycle
            IncrementDailyDiseaseEffects();

            // Update cycles left tracking
            infectionCyclesLeft--;
        }

        protected override void IncrementDailyDiseaseEffects()
        {
            if (infectionCyclesPassed <= 0) // Simulated Absorption/Incubation period before effects start to kick in, in this case 1 cycles, or approx. 8 in-game minutes, or 8 magic rounds.
                return;

            DaggerfallEntityBehaviour host = GetPeeredEntityBehaviour(manager);

            if (GameManager.Instance.PlayerEntity.Race == Races.Khajiit) // Khajiit will be much less effected by Moon Sugar, both positvely and negatively due to natural tolerance. 
            {
                if (infectionCyclesPassed == 1)
                {
                    ChangeStatMod(DFCareer.Stats.Speed, UnityEngine.Random.Range(6, 9 + 1));
                    ChangeStatMod(DFCareer.Stats.Luck, UnityEngine.Random.Range(3, 5 + 1));
                    ChangeStatMod(DFCareer.Stats.Intelligence, -UnityEngine.Random.Range(3, 6 + 1));
                    host.Entity.IncreaseFatigue(UnityEngine.Random.Range(3, 10 + 1), true);
                    DaggerfallUI.AddHUDText("Ah... the sweet sugar", 4f);
                    return;
                }

                if (infectionCyclesPassed == 17)
                {
                    ChangeStatMod(DFCareer.Stats.Speed, -UnityEngine.Random.Range(1, 3 + 1));
                    DaggerfallUI.AddHUDText("You can feel the moons departing you soon...", 3f);
                    return;
                }

                if (infectionCyclesPassed >= 25)
                {
                    if (GetAttributeMod(DFCareer.Stats.Speed) > 0)
                        SetStatMod(DFCareer.Stats.Speed, 0);
                    if (GetAttributeMod(DFCareer.Stats.Luck) > 0)
                        SetStatMod(DFCareer.Stats.Luck, 0);
                    ChangeStatMod(DFCareer.Stats.Intelligence, UnityEngine.Random.Range(1, 2 + 1));

                    DaggerfallUI.AddHUDText("The moons depart you...", 4f);
                    return;
                }
            }
            else
            {
                if (infectionCyclesPassed == 1)
                {
                    ChangeStatMod(DFCareer.Stats.Speed, UnityEngine.Random.Range(15, 20 + 1));
                    ChangeStatMod(DFCareer.Stats.Luck, UnityEngine.Random.Range(3, 5 + 1));
                    ChangeStatMod(DFCareer.Stats.Intelligence, -UnityEngine.Random.Range(10, 15 + 1));
                    host.Entity.IncreaseFatigue(UnityEngine.Random.Range(10, 20 + 1), true);
                    DaggerfallUI.AddHUDText("Your body is flooded with an absolutely amazing feeling!", 4f);
                    return;
                }

                if (infectionCyclesPassed == 17)
                {
                    ChangeStatMod(DFCareer.Stats.Speed, -UnityEngine.Random.Range(7, 10 + 1));
                    DaggerfallUI.AddHUDText("The feeling of ecstasy is beginning to fade...", 3f);
                    return;
                }

                if (infectionCyclesPassed >= 25)
                {
                    if (GetAttributeMod(DFCareer.Stats.Speed) > 0)
                        SetStatMod(DFCareer.Stats.Speed, 0);
                    if (GetAttributeMod(DFCareer.Stats.Luck) > 0)
                        SetStatMod(DFCareer.Stats.Luck, 0);
                    ChangeStatMod(DFCareer.Stats.Intelligence, UnityEngine.Random.Range(5, 7 + 1));

                    host.Entity.DecreaseFatigue(UnityEngine.Random.Range(35, 55 + 1), true);
                    DaggerfallUI.AddHUDText("The state of euphoria has left and you feel absolutely exausted...", 4f);
                    return;
                }
            }
        }

        public override void CureDisease()
        {
            EndDisease();
        }

        protected override void EndDisease()
        {
            // Set disease as completed and allow effect system to expire effect
            daysOfSymptomsLeft = completedDiseaseValue;
            forcedRoundsRemaining = 0;
            infectionCyclesLeft = 0;
        }
    }
}
