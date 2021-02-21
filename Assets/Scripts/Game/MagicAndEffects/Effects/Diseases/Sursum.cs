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
    public class Sursum : DiseaseEffect
    {
        Diseases diseaseType = Diseases.Sursum;
        int tickCycleDelay = 5;

        public override void SetProperties()
        {
            properties.Key = GetClassicDiseaseEffectKey(diseaseType);
            properties.ShowSpellIcon = false;
            classicDiseaseType = diseaseType;
            diseaseData = new DiseaseData(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xFF, 0xFF, 32, 32); // Sursum (Drug)
            bypassSavingThrows = true;
        }

        public override TextFile.Token[] ContractedMessageTokens => DaggerfallUnity.Instance.TextProvider.CreateTokens(
                TextFile.Formatting.JustifyCenter,
                "You have recently smoked Sursum, a potent",
                "performance enhancing drug. Known to cause",
                "great bursts of strength, but leaves a",
                "lingering reduction in cognitive ability.");

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
            if (infectionCyclesPassed <= 1) // Simulated Absorption/Incubation period before effects start to kick in, in this case 2 cycles, or approx. 10 in-game minutes, or 10 magic rounds.
                return;

            DaggerfallEntityBehaviour host = GetPeeredEntityBehaviour(manager);

            if (infectionCyclesPassed == 2)
            {
                ChangeStatMod(DFCareer.Stats.Strength, UnityEngine.Random.Range(10, 15 + 1));
                ChangeStatMod(DFCareer.Stats.Intelligence, -UnityEngine.Random.Range(7, 12 + 1));
                ChangeStatMod(DFCareer.Stats.Personality, -UnityEngine.Random.Range(3, 8 + 1));
                host.Entity.DecreaseMagicka(UnityEngine.Random.Range(2, 4 + 1));
                DaggerfallUI.AddHUDText("Your muscles bulge and contract with abnormal vigor, but your mind is clouded by sudden rage", 4f);
                return;
            }

            if (infectionCyclesPassed >= 3 && infectionCyclesPassed < 12)
            {
                host.Entity.DecreaseMagicka(UnityEngine.Random.Range(1, 3 + 1));
                DaggerfallUI.AddHUDText("Irrational anger disrupts your concentration", 3f);
                return;
            }

            if (infectionCyclesPassed == 12)
            {
                ChangeStatMod(DFCareer.Stats.Strength, UnityEngine.Random.Range(8, 10 + 1));
                ChangeStatMod(DFCareer.Stats.Intelligence, -UnityEngine.Random.Range(5, 10 + 1));
                ChangeStatMod(DFCareer.Stats.Personality, -UnityEngine.Random.Range(2, 6 + 1));
                host.Entity.DecreaseMagicka(UnityEngine.Random.Range(2, 4 + 1));
                DaggerfallUI.AddHUDText("Your strength grows further, but so does this undirected boiling anger within", 4f);
                return;
            }

            if (infectionCyclesPassed >= 13 && infectionCyclesPassed < 22)
            {
                host.Entity.DecreaseMagicka(UnityEngine.Random.Range(2, 5 + 1));
                DaggerfallUI.AddHUDText("Your concentration wanes...", 3f);
                return;
            }

            if (infectionCyclesPassed == 22)
            {
                ChangeStatMod(DFCareer.Stats.Strength, UnityEngine.Random.Range(7, 10 + 1));
                ChangeStatMod(DFCareer.Stats.Intelligence, -UnityEngine.Random.Range(3, 7 + 1));
                ChangeStatMod(DFCareer.Stats.Personality, -UnityEngine.Random.Range(1, 5 + 1));
                host.Entity.DecreaseMagicka(UnityEngine.Random.Range(3, 6 + 1));
                DaggerfallUI.AddHUDText("Your arms feel as thick as tree stumps!... Why are you here again?", 4f);
                return;
            }

            if (infectionCyclesPassed >= 23 && infectionCyclesPassed < 28)
            {
                host.Entity.DecreaseMagicka(UnityEngine.Random.Range(4, 7 + 1));
                DaggerfallUI.AddHUDText("Can't... Focus...", 3f);
                return;
            }

            if (infectionCyclesPassed == 28)
            {
                ChangeStatMod(DFCareer.Stats.Strength, -(int)Mathf.Ceil(GetAttributeMod(DFCareer.Stats.Strength) * 0.5f));
                ChangeStatMod(DFCareer.Stats.Intelligence, (int)Mathf.Ceil(GetAttributeMod(DFCareer.Stats.Intelligence) * 0.5f));
                ChangeStatMod(DFCareer.Stats.Personality, (int)Mathf.Ceil(GetAttributeMod(DFCareer.Stats.Personality) * 0.5f));
                DaggerfallUI.AddHUDText("Your muscles begin to reduce in size, as does the intense anger", 4f);
                return;
            }

            if (infectionCyclesPassed >= 29 && infectionCyclesPassed < 32)
            {
                return;
            }

            if (infectionCyclesPassed >= 32)
            {
                if (GetAttributeMod(DFCareer.Stats.Strength) > 0)
                    SetStatMod(DFCareer.Stats.Strength, 0);
                ChangeStatMod(DFCareer.Stats.Intelligence, UnityEngine.Random.Range(3, 4 + 1));
                if (GetAttributeMod(DFCareer.Stats.Personality) < 0)
                    SetStatMod(DFCareer.Stats.Personality, 0);

                host.Entity.DecreaseFatigue(UnityEngine.Random.Range(4, 7 + 1), true);
                DaggerfallUI.AddHUDText("Your muscles have returned to their normal size, and the haze of anger is gone, but you still feel mentally foggy", 4f);
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
