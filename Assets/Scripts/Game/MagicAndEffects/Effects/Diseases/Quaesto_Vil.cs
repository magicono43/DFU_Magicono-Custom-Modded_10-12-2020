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
    public class Quaesto_Vil : DiseaseEffect
    {
        Diseases diseaseType = Diseases.Quaesto_Vil;
        int tickCycleDelay = 15;

        public override void SetProperties()
        {
            properties.Key = GetClassicDiseaseEffectKey(diseaseType);
            properties.ShowSpellIcon = false;
            classicDiseaseType = diseaseType;
            diseaseData = new DiseaseData(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xFF, 0xFF, 60, 60); // Quaesto_Vil (Drug)
            bypassSavingThrows = true;
        }

        public override TextFile.Token[] ContractedMessageTokens => DaggerfallUnity.Instance.TextProvider.CreateTokens(
                TextFile.Formatting.JustifyCenter,
                "You have recently consumed Quaesto Vil, a",
                "fairly potent and long lasting stimulant.",
                "providing enduring energy and alterness,",
                "but leaving the user less willful and dexterous.");

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
            if (infectionCyclesPassed <= 3) // Simulated Absorption/Incubation period before effects start to kick in, in this case 4 cycles, or approx. 60 in-game minutes, or 60 magic rounds.
                return;

            DaggerfallEntityBehaviour host = GetPeeredEntityBehaviour(manager);

            if (infectionCyclesPassed == 4)
            {
                host.Entity.IncreaseFatigue(UnityEngine.Random.Range(8, 16 + 1), true);
                DaggerfallUI.AddHUDText("Suddenly you feel more awake and alert", 4f);
                return;
            }

            if (infectionCyclesPassed >= 5 && infectionCyclesPassed < 25)
            {
                host.Entity.IncreaseFatigue(UnityEngine.Random.Range(5, 11 + 1), true);
                DaggerfallUI.AddHUDText("You feel a surge of energy", 3f);
                return;
            }

            if (infectionCyclesPassed == 25)
            {
                ChangeStatMod(DFCareer.Stats.Willpower, -UnityEngine.Random.Range(1, 4 + 1));
                ChangeStatMod(DFCareer.Stats.Agility, -UnityEngine.Random.Range(3, 7 + 1));
                host.Entity.IncreaseFatigue(UnityEngine.Random.Range(3, 7 + 1), true);
                DaggerfallUI.AddHUDText("The surges of energy are becoming less intense and your joints feel slightly stiff", 4f);
                return;
            }

            if (infectionCyclesPassed >= 26 && infectionCyclesPassed < 46)
            {
                host.Entity.IncreaseFatigue(UnityEngine.Random.Range(3, 7 + 1), true);
                DaggerfallUI.AddHUDText("You feel more alert", 3f);
                return;
            }

            if (infectionCyclesPassed == 46)
            {
                ChangeStatMod(DFCareer.Stats.Willpower, -UnityEngine.Random.Range(4, 8 + 1));
                ChangeStatMod(DFCareer.Stats.Agility, -UnityEngine.Random.Range(1, 4 + 1));
                host.Entity.IncreaseFatigue(UnityEngine.Random.Range(2, 5 + 1), true);
                DaggerfallUI.AddHUDText("The surges of energy are greatly diminished and you feel less willful", 4f);
                return;
            }

            if (infectionCyclesPassed >= 47 && infectionCyclesPassed < 60)
            {
                host.Entity.IncreaseFatigue(UnityEngine.Random.Range(1, 4 + 1), true);
                DaggerfallUI.AddHUDText("You feel a slight jolt of energy", 3f);
                return;
            }

            if (infectionCyclesPassed >= 60)
            {
                ChangeStatMod(DFCareer.Stats.Willpower, -UnityEngine.Random.Range(2, 6 + 1));
                ChangeStatMod(DFCareer.Stats.Agility, -UnityEngine.Random.Range(1, 5 + 1));
                host.Entity.IncreaseFatigue(UnityEngine.Random.Range(1, 4 + 1), true);
                DaggerfallUI.AddHUDText("The last trickle of energy has left. You are now left with stiffer joints and general lack of will", 4f);
                return;
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
