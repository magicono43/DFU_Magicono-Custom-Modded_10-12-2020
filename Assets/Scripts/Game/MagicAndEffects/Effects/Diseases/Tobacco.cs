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
    public class Tobacco : DiseaseEffect
    {
        Diseases diseaseType = Diseases.Tobacco;
        int tickCycleDelay = 5;

        public override void SetProperties()
        {
            properties.Key = GetClassicDiseaseEffectKey(diseaseType);
            properties.ShowSpellIcon = false;
            classicDiseaseType = diseaseType;
            diseaseData = new DiseaseData(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xFF, 0xFF, 22, 22); // Tobacco (Drug)
            bypassSavingThrows = true;
        }

        public override TextFile.Token[] ContractedMessageTokens => DaggerfallUnity.Instance.TextProvider.CreateTokens(
                TextFile.Formatting.JustifyCenter,
                "You have recently smoked Tobacco, a plant with",
                "moderate anxiolytic properties when consumed. Users",
                "experience a calming effect for the duration, but",
                "has negative effects on health when used frequently.");

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
            if (infectionCyclesPassed <= 0) // Simulated Absorption/Incubation period before effects start to kick in, in this case 1 cycles, or approx. 5 in-game minutes, or 5 magic rounds.
                return;

            DaggerfallEntityBehaviour host = GetPeeredEntityBehaviour(manager);

            if (infectionCyclesPassed == 1)
            {
                ChangeStatMod(DFCareer.Stats.Willpower, UnityEngine.Random.Range(3, 7 + 1));
                ChangeStatMod(DFCareer.Stats.Agility, UnityEngine.Random.Range(3, 5 + 1));
                ChangeStatMod(DFCareer.Stats.Endurance, UnityEngine.Random.Range(3, 4 + 1));
                host.Entity.DecreaseHealth(UnityEngine.Random.Range(2, 5 + 1));
                DaggerfallUI.AddHUDText("You feel more relaxed and limber", 4f);
                return;
            }

            if (infectionCyclesPassed >= 22)
            {
                if (GetAttributeMod(DFCareer.Stats.Willpower) > 0)
                    SetStatMod(DFCareer.Stats.Willpower, 0);
                if (GetAttributeMod(DFCareer.Stats.Agility) > 0)
                    SetStatMod(DFCareer.Stats.Agility, 0);

                DaggerfallUI.AddHUDText("The relaxed feeling has left, and you feel just a bit less healthy than before", 4f);
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
