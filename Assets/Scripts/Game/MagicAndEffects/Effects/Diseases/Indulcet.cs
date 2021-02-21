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
    public class Indulcet : DiseaseEffect
    {
        Diseases diseaseType = Diseases.Indulcet;
        int tickCycleDelay = 10;

        public override void SetProperties()
        {
            properties.Key = GetClassicDiseaseEffectKey(diseaseType);
            properties.ShowSpellIcon = false;
            classicDiseaseType = diseaseType;
            diseaseData = new DiseaseData(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 60, 60); // Indulcet (Drug)
            bypassSavingThrows = true;
        }

        public override TextFile.Token[] ContractedMessageTokens => DaggerfallUnity.Instance.TextProvider.CreateTokens(
                TextFile.Formatting.JustifyCenter,
                "You have recently used Indulcet, a drug that",
                "has the strange properties of temporarily",
                "granting the user favor from the gods, but",
                "it invokes the strong desire to sleep.");

        protected override bool IsLikeKind(IncumbentEffect other)
        {
            return false;
        }

        protected override void UpdateDisease()
        {
            // Do nothing if expiring out
            if (forcedRoundsRemaining == 0)
                return;

            infectionRoundCounter++;

            // Do nothing if disease has run its course
            if (infectionRoundCounter < tickCycleDelay || infectionCyclesLeft == 0)
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

            // Count down cycles remaining
            if (!IsDiseasePermanent() && infectionCyclesLeft <= 0)
            {
                infectionCyclesLeft = 0;
                EndDisease();
            }
        }

        protected override void IncrementDailyDiseaseEffects()
        {
            if (infectionCyclesPassed <= 2) // Simulated Absorption/Incubation period before effects start to kick in, in this case 3 cycles, or approx. 30 in-game minutes, or 30 magic rounds.
                return;

            DaggerfallEntityBehaviour host = GetPeeredEntityBehaviour(manager);

            if (infectionCyclesPassed == 3)
            {
                ChangeStatMod(DFCareer.Stats.Luck, UnityEngine.Random.Range(20, 30 + 1));
                DaggerfallUI.AddHUDText("You feel a warm, calming sensation wash over you", 4f);
                return;
            }

            if (infectionCyclesPassed >= 4 && infectionCyclesPassed < 15)
            {
                host.Entity.DecreaseFatigue(UnityEngine.Random.Range(1, 3 + 1), true);
                DaggerfallUI.AddHUDText("You feel slightly drowsy", 3f);
                return;
            }

            if (infectionCyclesPassed == 15)
            {
                ChangeStatMod(DFCareer.Stats.Luck, -(int)Mathf.Ceil(GetAttributeMod(DFCareer.Stats.Luck) * 0.25f));
                host.Entity.DecreaseFatigue(UnityEngine.Random.Range(1, 3 + 1), true);
                DaggerfallUI.AddHUDText("You feel the warm sensation subside somewhat", 4f);
                return;
            }

            if (infectionCyclesPassed >= 16 && infectionCyclesPassed < 29)
            {
                host.Entity.DecreaseFatigue(UnityEngine.Random.Range(2, 5 + 1), true);
                DaggerfallUI.AddHUDText("You feel drowsy", 3f);
                return;
            }

            if (infectionCyclesPassed == 29)
            {
                ChangeStatMod(DFCareer.Stats.Luck, -(int)Mathf.Ceil(GetAttributeMod(DFCareer.Stats.Luck) * 0.25f));
                host.Entity.DecreaseFatigue(UnityEngine.Random.Range(2, 5 + 1), true);
                DaggerfallUI.AddHUDText("You feel the warm sensation subside even more", 4f);
                return;
            }

            if (infectionCyclesPassed >= 30 && infectionCyclesPassed < 43)
            {
                host.Entity.DecreaseFatigue(UnityEngine.Random.Range(3, 6 + 1), true);
                DaggerfallUI.AddHUDText("You feel sleepy", 3f);
                return;
            }

            if (infectionCyclesPassed == 43)
            {
                ChangeStatMod(DFCareer.Stats.Luck, -(int)Mathf.Ceil(GetAttributeMod(DFCareer.Stats.Luck) * 0.5f));
                host.Entity.DecreaseFatigue(UnityEngine.Random.Range(3, 6 + 1), true);
                DaggerfallUI.AddHUDText("You can barely feel the warm sensation anymore", 4f);
                return;
            }

            if (infectionCyclesPassed >= 44 && infectionCyclesPassed < 52)
            {
                host.Entity.DecreaseFatigue(UnityEngine.Random.Range(4, 7 + 1), true);
                DaggerfallUI.AddHUDText("You feel like passing out", 3f);
                return;
            }

            if (infectionCyclesPassed == 52)
            {
                if (GetAttributeMod(DFCareer.Stats.Luck) > 0)
                    SetStatMod(DFCareer.Stats.Luck, 0);

                host.Entity.DecreaseFatigue(UnityEngine.Random.Range(4, 7 + 1), true);
                DaggerfallUI.AddHUDText("The warm sensation has completely left you, but you still feel drowsy...", 4f);
                return;
            }

            if (infectionCyclesPassed >= 53 && infectionCyclesPassed < 60)
            {
                host.Entity.DecreaseFatigue(UnityEngine.Random.Range(1, 3 + 1), true);
                DaggerfallUI.AddHUDText("You feel slightly drowsy", 3f);
                return;
            }

            if (infectionCyclesPassed >= 60)
            {
                DaggerfallUI.AddHUDText("The drowsy feeling went away", 3f);
                EndDisease();
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
