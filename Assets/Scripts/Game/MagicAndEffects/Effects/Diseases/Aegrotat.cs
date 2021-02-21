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
    public class Aegrotat : DiseaseEffect
    {
        Diseases diseaseType = Diseases.Aegrotat;
        int tickCycleDelay = 2;

        public override void SetProperties()
        {
            properties.Key = GetClassicDiseaseEffectKey(diseaseType);
            properties.ShowSpellIcon = false;
            classicDiseaseType = diseaseType;
            diseaseData = new DiseaseData(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xFF, 0xFF, 25, 25); // Aegrotat (Drug)
            bypassSavingThrows = true;
        }

        public override TextFile.Token[] ContractedMessageTokens => DaggerfallUnity.Instance.TextProvider.CreateTokens(
                TextFile.Formatting.JustifyCenter,
                "You have recently inhaled Aegrotat, an",
                "extremely potent and dangerous arcane drug.",
                "providing great bursts of magical energy,",
                "but burning and scarring the user's innards.");

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
            if (infectionCyclesPassed <= 1) // Simulated Absorption/Incubation period before effects start to kick in, in this case 2 cycles, or approx. 4 in-game minutes, or 4 magic rounds.
                return;

            DaggerfallEntityBehaviour host = GetPeeredEntityBehaviour(manager);

            if (infectionCyclesPassed == 2)
            {
                ChangeStatMod(DFCareer.Stats.Intelligence, UnityEngine.Random.Range(20, 30 + 1));
                ChangeStatMod(DFCareer.Stats.Endurance, -UnityEngine.Random.Range(2, 5 + 1));
                host.Entity.IncreaseMagicka(UnityEngine.Random.Range(20, 35 + 1));
                host.Entity.DecreaseHealth(UnityEngine.Random.Range(2, 5 + 1));
                DaggerfallUI.AddHUDText("This horrible burning sensation must be the feeling of pure magical energy!", 4f);
                return;
            }

            if (infectionCyclesPassed >= 3 && infectionCyclesPassed < 17)
            {
                ChangeStatMod(DFCareer.Stats.Endurance, -1);
                host.Entity.IncreaseMagicka(UnityEngine.Random.Range(15, 30 + 1));
                host.Entity.DecreaseHealth(UnityEngine.Random.Range(1, 3 + 1));
                DaggerfallUI.AddHUDText("You feel another surge of burning", 3f);
                return;
            }

            if (infectionCyclesPassed == 17)
            {
                ChangeStatMod(DFCareer.Stats.Intelligence, -10);
                ChangeStatMod(DFCareer.Stats.Endurance, -1);
                host.Entity.IncreaseMagicka(UnityEngine.Random.Range(15, 25 + 1));
                host.Entity.DecreaseHealth(UnityEngine.Random.Range(1, 3 + 1));
                DaggerfallUI.AddHUDText("You feel the burning becoming less intense, the stuff must be wearing off...", 4f);
                return;
            }

            if (infectionCyclesPassed >= 18 && infectionCyclesPassed < 25)
            {
                ChangeStatMod(DFCareer.Stats.Endurance, -1);
                host.Entity.IncreaseMagicka(UnityEngine.Random.Range(10, 20 + 1));
                host.Entity.DecreaseHealth(UnityEngine.Random.Range(1, 2 + 1));
                DaggerfallUI.AddHUDText("Another spike of burning...", 3f);
                return;
            }

            if (infectionCyclesPassed >= 25)
            {
                if (GetAttributeMod(DFCareer.Stats.Intelligence) > 0)
                    SetStatMod(DFCareer.Stats.Intelligence, 0);

                ChangeStatMod(DFCareer.Stats.Endurance, -1);
                host.Entity.IncreaseMagicka(UnityEngine.Random.Range(7, 15 + 1));
                host.Entity.DecreaseHealth(UnityEngine.Random.Range(1, 2 + 1));
                DaggerfallUI.AddHUDText("You no longer feel the burning, nor the magical energy, but you don't feel very healthy after that experience", 4f);
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
