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
    public class Skooma : DiseaseEffect
    {
        Diseases diseaseType = Diseases.Skooma;
        int tickCycleDelay = 6;

        public override void SetProperties()
        {
            properties.Key = GetClassicDiseaseEffectKey(diseaseType);
            properties.ShowSpellIcon = false;
            classicDiseaseType = diseaseType;
            diseaseData = new DiseaseData(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xFF, 0xFF, 30, 30); // Skooma (Drug)
            bypassSavingThrows = true;
        }

        public override TextFile.Token[] ContractedMessageTokens => DaggerfallUnity.Instance.TextProvider.CreateTokens(
                TextFile.Formatting.JustifyCenter,
                "You have recently consumed Skooma, a refined form of",
                "moon sugar, more potent and much more addictive. Khajiit",
                "are more tolerant, but still dangerous even for them. The",
                "users experience amazing ecstasy and extreme lethargy.");

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
            if (infectionCyclesPassed <= 0) // Simulated Absorption/Incubation period before effects start to kick in, in this case 1 cycles, or approx. 6 in-game minutes, or 6 magic rounds.
                return;

            DaggerfallEntityBehaviour host = GetPeeredEntityBehaviour(manager);

            if (GameManager.Instance.PlayerEntity.Race == Races.Khajiit) // Khajiit will be much less effected by Moon Sugar, both positvely and negatively due to natural tolerance. 
            {

                if (infectionCyclesPassed == 1)
                {
                    ChangeStatMod(DFCareer.Stats.Speed, UnityEngine.Random.Range(13, 25 + 1));
                    ChangeStatMod(DFCareer.Stats.Strength, UnityEngine.Random.Range(5, 9 + 1));
                    ChangeStatMod(DFCareer.Stats.Intelligence, -UnityEngine.Random.Range(8, 15 + 1));
                    ChangeStatMod(DFCareer.Stats.Personality, -UnityEngine.Random.Range(5, 8 + 1));
                    ChangeStatMod(DFCareer.Stats.Agility, -UnityEngine.Random.Range(5, 11 + 1));
                    host.Entity.DecreaseFatigue(UnityEngine.Random.Range(3, 9 + 1), true);
                    DaggerfallUI.AddHUDText("Ah... the sacred sugar, don't partake too much, you know the consequences", 4f);
                    return;
                }

                if (infectionCyclesPassed >= 2 && infectionCyclesPassed < 17)
                {
                    host.Entity.DecreaseFatigue(UnityEngine.Random.Range(2, 4 + 1), true);
                    DaggerfallUI.AddHUDText("A catnap would be nice...", 3f);
                    return;
                }

                if (infectionCyclesPassed == 18)
                {
                    ChangeStatMod(DFCareer.Stats.Speed, -UnityEngine.Random.Range(5, 8 + 1));
                    ChangeStatMod(DFCareer.Stats.Strength, -UnityEngine.Random.Range(2, 3 + 1));
                    DaggerfallUI.AddHUDText("The sacred sugar will leave soon...", 4f);
                    return;
                }

                if (infectionCyclesPassed >= 19 && infectionCyclesPassed < 30)
                {
                    host.Entity.DecreaseFatigue(UnityEngine.Random.Range(1, 3 + 1), true);
                    DaggerfallUI.AddHUDText("A catnap would be nice...", 3f);
                    return;
                }

                if (infectionCyclesPassed >= 30)
                {
                    if (GetAttributeMod(DFCareer.Stats.Speed) > 0)
                        SetStatMod(DFCareer.Stats.Speed, 0);
                    if (GetAttributeMod(DFCareer.Stats.Strength) > 0)
                        SetStatMod(DFCareer.Stats.Strength, 0);
                    ChangeStatMod(DFCareer.Stats.Intelligence, UnityEngine.Random.Range(4, 6 + 1));
                    ChangeStatMod(DFCareer.Stats.Personality, UnityEngine.Random.Range(2, 4 + 1));
                    ChangeStatMod(DFCareer.Stats.Agility, UnityEngine.Random.Range(2, 4 + 1));

                    host.Entity.DecreaseFatigue(UnityEngine.Random.Range(2, 5 + 1), true);
                    DaggerfallUI.AddHUDText("The sacred sugar left... You can handle it, but be careful with that stuff", 4f);
                    return;
                }
            }
            else
            {
                if (infectionCyclesPassed == 1)
                {
                    ChangeStatMod(DFCareer.Stats.Speed, UnityEngine.Random.Range(25, 40 + 1));
                    ChangeStatMod(DFCareer.Stats.Strength, UnityEngine.Random.Range(10, 15 + 1));
                    ChangeStatMod(DFCareer.Stats.Intelligence, -UnityEngine.Random.Range(20, 30 + 1));
                    ChangeStatMod(DFCareer.Stats.Personality, -UnityEngine.Random.Range(10, 15 + 1));
                    ChangeStatMod(DFCareer.Stats.Agility, -UnityEngine.Random.Range(10, 20 + 1));
                    host.Entity.DecreaseFatigue(UnityEngine.Random.Range(5, 15 + 1), true);
                    DaggerfallUI.AddHUDText("You feel incredible! But mentally foggy and tired as well...", 4f);
                    return;
                }

                if (infectionCyclesPassed >= 2 && infectionCyclesPassed < 17)
                {
                    host.Entity.DecreaseFatigue(UnityEngine.Random.Range(2, 4 + 1), true);
                    DaggerfallUI.AddHUDText("You feel very sleepy...", 3f);
                    return;
                }

                if (infectionCyclesPassed == 18)
                {
                    ChangeStatMod(DFCareer.Stats.Speed, -UnityEngine.Random.Range(10, 15 + 1));
                    ChangeStatMod(DFCareer.Stats.Strength, -UnityEngine.Random.Range(3, 6 + 1));
                    DaggerfallUI.AddHUDText("The incredible feeling is starting to fade, but you still feel dull and tired...", 4f);
                    return;
                }

                if (infectionCyclesPassed >= 19 && infectionCyclesPassed < 30)
                {
                    host.Entity.DecreaseFatigue(UnityEngine.Random.Range(1, 3 + 1), true);
                    DaggerfallUI.AddHUDText("You feel drowsy...", 3f);
                    return;
                }

                if (infectionCyclesPassed >= 30)
                {
                    if (GetAttributeMod(DFCareer.Stats.Speed) > 0)
                        SetStatMod(DFCareer.Stats.Speed, 0);
                    if (GetAttributeMod(DFCareer.Stats.Strength) > 0)
                        SetStatMod(DFCareer.Stats.Strength, 0);
                    ChangeStatMod(DFCareer.Stats.Intelligence, UnityEngine.Random.Range(6, 10 + 1));
                    ChangeStatMod(DFCareer.Stats.Personality, UnityEngine.Random.Range(4, 7 + 1));
                    ChangeStatMod(DFCareer.Stats.Agility, UnityEngine.Random.Range(4, 7 + 1));

                    host.Entity.DecreaseFatigue(UnityEngine.Random.Range(3, 6 + 1), true);
                    DaggerfallUI.AddHUDText("The amazing feeling is gone, now you just feel foggy and lethargic", 4f);
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
