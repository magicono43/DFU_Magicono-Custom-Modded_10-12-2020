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
    public class Incense_of_Mara : DiseaseEffect
    {
        Diseases diseaseType = Diseases.Incense_of_Mara;
        int tickCycleDelay = 4;

        public override void SetProperties()
        {
            properties.Key = GetClassicDiseaseEffectKey(diseaseType);
            properties.ShowSpellIcon = false;
            classicDiseaseType = diseaseType;
            diseaseData = new DiseaseData(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xFF, 0xFF, 90, 90); // Incense_of_Mara (Drug)
            bypassSavingThrows = true;
        }

        public override TextFile.Token[] ContractedMessageTokens => DaggerfallUnity.Instance.TextProvider.CreateTokens(
                TextFile.Formatting.JustifyCenter,
                "You have recently inhaled Incense of Mara, an incredibly strong",
                "aphrodisiac often used by Wealthy Bretons and Nords who need",
                "some help in the bedchambers. Those who inhale the fragrance",
                "feel extreme arousal, but with pronounced downs-sides afterward.");

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
            if (infectionCyclesPassed <= 0) // Simulated Absorption/Incubation period before effects start to kick in, in this case 1 cycles, or approx. 4 in-game minutes, or 4 magic rounds.
                return;

            DaggerfallEntityBehaviour host = GetPeeredEntityBehaviour(manager);

            if (infectionCyclesPassed == 1)
            {
                ChangeStatMod(DFCareer.Stats.Personality, UnityEngine.Random.Range(25, 35 + 1));
                ChangeStatMod(DFCareer.Stats.Endurance, UnityEngine.Random.Range(25, 35 + 1));
                ChangeStatMod(DFCareer.Stats.Luck, UnityEngine.Random.Range(3, 6 + 1));
                host.Entity.DecreaseMagicka(UnityEngine.Random.Range(5, 10 + 1));
                DaggerfallUI.AddHUDText("You feel extremely aroused! But your mind wanders...", 4f);
                return;
            }

            if (infectionCyclesPassed >= 2 && infectionCyclesPassed < 50)
            {
                host.Entity.DecreaseMagicka(UnityEngine.Random.Range(2, 3 + 1));
                DaggerfallUI.AddHUDText("Your mind wanders...", 3f);
                return;
            }

            if (infectionCyclesPassed == 50)
            {
                ChangeStatMod(DFCareer.Stats.Personality, -UnityEngine.Random.Range(7, 13 + 1));
                ChangeStatMod(DFCareer.Stats.Endurance, -UnityEngine.Random.Range(7, 13 + 1));
                ChangeStatMod(DFCareer.Stats.Willpower, -UnityEngine.Random.Range(4, 8 + 1));
                ChangeStatMod(DFCareer.Stats.Strength, -UnityEngine.Random.Range(3, 7 + 1));
                ChangeStatMod(DFCareer.Stats.Speed, -UnityEngine.Random.Range(4, 7 + 1));
                host.Entity.DecreaseMagicka(UnityEngine.Random.Range(2, 3 + 1));
                DaggerfallUI.AddHUDText("Your feeling of arousal seems to be diminishing", 4f);
                return;
            }

            if (infectionCyclesPassed >= 51 && infectionCyclesPassed < 90)
            {
                host.Entity.DecreaseMagicka(UnityEngine.Random.Range(1, 2 + 1));
                DaggerfallUI.AddHUDText("Your mind wanders...", 3f);
                return;
            }

            if (infectionCyclesPassed >= 90)
            {
                if (GetAttributeMod(DFCareer.Stats.Personality) > 0)
                    SetStatMod(DFCareer.Stats.Personality, 0);
                if (GetAttributeMod(DFCareer.Stats.Endurance) > 0)
                    SetStatMod(DFCareer.Stats.Endurance, 0);
                if (GetAttributeMod(DFCareer.Stats.Luck) > 0)
                    SetStatMod(DFCareer.Stats.Luck, 0);
                ChangeStatMod(DFCareer.Stats.Willpower, -UnityEngine.Random.Range(3, 6 + 1));
                ChangeStatMod(DFCareer.Stats.Strength, -UnityEngine.Random.Range(2, 5 + 1));
                ChangeStatMod(DFCareer.Stats.Speed, -UnityEngine.Random.Range(3, 5 + 1));

                DaggerfallUI.AddHUDText("You no longer feel aroused, but you feel weaker and slower, mentally and physically", 4f);
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
