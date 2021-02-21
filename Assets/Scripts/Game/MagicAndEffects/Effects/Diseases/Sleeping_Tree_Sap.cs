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
    public class Sleeping_Tree_Sap : DiseaseEffect
    {
        Diseases diseaseType = Diseases.Sleeping_Tree_Sap;
        int tickCycleDelay = 12;

        public override void SetProperties()
        {
            properties.Key = GetClassicDiseaseEffectKey(diseaseType);
            properties.ShowSpellIcon = false;
            classicDiseaseType = diseaseType;
            diseaseData = new DiseaseData(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xFF, 0xFF, 50, 50); // Sleeping_Tree_Sap (Drug)
            bypassSavingThrows = true;
        }

        public override TextFile.Token[] ContractedMessageTokens => DaggerfallUnity.Instance.TextProvider.CreateTokens(
                TextFile.Formatting.JustifyCenter,
                "You have recently consumed Sleeping Tree Sap, a sap extracted",
                "from the very rare trees natively found in Skyrim. Those",
                "who drink the sap gain supernatural fortitude for the duration,",
                "but also have their movement greatly slowed afterward.");

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
            if (infectionCyclesPassed <= 2) // Simulated Absorption/Incubation period before effects start to kick in, in this case 3 cycles, or approx. 36 in-game minutes, or 36 magic rounds.
                return;

            DaggerfallEntityBehaviour host = GetPeeredEntityBehaviour(manager);

            if (infectionCyclesPassed == 3)
            {
                ChangeStatMod(DFCareer.Stats.Endurance, UnityEngine.Random.Range(35, 40 + 1));
                ChangeStatMod(DFCareer.Stats.Speed, -UnityEngine.Random.Range(25, 30 + 1));
                host.Entity.IncreaseHealth(UnityEngine.Random.Range(3, 5 + 1));
                DaggerfallUI.AddHUDText("You feel more healthy than you ever have! But also much slower than usual", 4f);
                return;
            }

            if (infectionCyclesPassed >= 4 && infectionCyclesPassed < 35)
            {
                host.Entity.IncreaseHealth(UnityEngine.Random.Range(1, 4 + 1));
                DaggerfallUI.AddHUDText("Your wounds close before your eyes...", 3f);
                return;
            }

            if (infectionCyclesPassed == 35)
            {
                ChangeStatMod(DFCareer.Stats.Endurance, -UnityEngine.Random.Range(10, 20 + 1));
                ChangeStatMod(DFCareer.Stats.Speed, UnityEngine.Random.Range(5, 8 + 1));
                host.Entity.IncreaseHealth(UnityEngine.Random.Range(1, 3 + 1));
                DaggerfallUI.AddHUDText("You still feel very healthy, but less so than before", 4f);
                return;
            }

            if (infectionCyclesPassed >= 36 && infectionCyclesPassed < 50)
            {
                host.Entity.IncreaseHealth(UnityEngine.Random.Range(1, 2 + 1));
                DaggerfallUI.AddHUDText("Your wounds slowly close before your eyes...", 3f);
                return;
            }

            if (infectionCyclesPassed >= 50)
            {
                if (GetAttributeMod(DFCareer.Stats.Endurance) > 0)
                    SetStatMod(DFCareer.Stats.Endurance, 0);
                ChangeStatMod(DFCareer.Stats.Speed, UnityEngine.Random.Range(7, 10 + 1));

                host.Entity.IncreaseHealth(UnityEngine.Random.Range(1, 2 + 1));
                DaggerfallUI.AddHUDText("The feeling of great health has left, but you still feel much slower than normal...", 4f);
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
