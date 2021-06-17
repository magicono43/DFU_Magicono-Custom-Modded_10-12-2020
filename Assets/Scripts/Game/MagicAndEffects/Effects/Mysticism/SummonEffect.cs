// Project:         Daggerfall Tools For Unity
// Copyright:       Copyright (C) 2009-2020 Daggerfall Workshop
// Web Site:        http://www.dfworkshop.net
// License:         MIT License (http://www.opensource.org/licenses/mit-license.php)
// Source Code:     https://github.com/Interkarma/daggerfall-unity
// Original Author: Kirk Oliveri
// Contributors:    Gavin Clayton (interkarma@dfworkshop.net)
// 
// Notes:
//

using DaggerfallConnect;
using DaggerfallWorkshop.Game.Entity;
using FullSerializer;

namespace DaggerfallWorkshop.Game.MagicAndEffects.MagicEffects
{
    /// <summary>
    /// Summon effect base.
    /// Provides functionality common to all Summon effects which vary only by entity being summoned.
    /// </summary>
    public abstract class SummonEffect : IncumbentEffect
    {
        protected Entity.MonsterCareers monsterCareer = Entity.MonsterCareers.None;

        bool awakeAlert = true;

        public override void Start(EntityEffectManager manager, DaggerfallEntityBehaviour caster = null)
        {
            base.Start(manager, caster);
        }

        public override void ConstantEffect()
        {
            base.ConstantEffect();
            StartSummon();
        }

        public override void Resume(EntityEffectManager.EffectSaveData_v1 effectData, EntityEffectManager manager, DaggerfallEntityBehaviour caster = null)
        {
            base.Resume(effectData, manager, caster);
            StartSummon();
        }

        public override void End()
        {
            base.End();
            EndSummon();
        }

        protected override bool IsLikeKind(IncumbentEffect other)
        {
            return (other is SummonEffect && (other as SummonEffect).monsterCareer == monsterCareer) ? true : false;
        }

        protected override void AddState(IncumbentEffect incumbent)
        {
            // Stack my rounds onto incumbent
            incumbent.RoundsRemaining += RoundsRemaining;
        }

        void StartSummon() // Change this for summon behavior
        {
            // Get peered entity gameobject
            DaggerfallEntityBehaviour entityBehaviour = GetPeeredEntityBehaviour(manager);
            if (!entityBehaviour)
                return;

            // Part where monster ally is actually created
            UnityEngine.GameObject player = GameManager.Instance.PlayerObject;
            int id = 0;
            int team = 0;
            UnityEngine.GameObject[] mobile = DaggerfallWorkshop.Utility.GameObjectHelper.CreateFoeGameObjects(player.transform.position + player.transform.forward * 2, (MobileTypes)id, 1);

            DaggerfallEntityBehaviour behaviour = mobile[0].GetComponent<DaggerfallEntityBehaviour>();
            EnemyEntity entity = behaviour.Entity as EnemyEntity;
            if (args.Length > 1)
            {
                entity.Team = (MobileTeams)team;
            }
            else
                team = (int)entity.Team;

            if (args.Length > 2)
            {
                entity.Level = level;
            }
            else
                level = entity.Level;

            mobile[0].transform.LookAt(mobile[0].transform.position + (mobile[0].transform.position - player.transform.position));
            mobile[0].SetActive(true);

            return string.Format("Created {0} on team {1} of level {2}", (MobileTypes)id, (MobileTeams)team, level);

            // Output "You Summoned Something" if the host manager is player
            if (awakeAlert && manager.EntityBehaviour == GameManager.Instance.PlayerEntityBehaviour)
            {
                DaggerfallUI.AddHUDText("You Summon An Ally", 1.5f); // Change to say exactly what you summoned, rat, etc.
                awakeAlert = false;
            }
        }

        void EndSummon() // Change this for summon behavior
        {
            // Get peered entity gameobject
            DaggerfallEntityBehaviour entityBehaviour = GetPeeredEntityBehaviour(manager);
            if (!entityBehaviour)
                return;

            entityBehaviour.Entity.IsSilenced = false;
        }

        #region Serialization

        [fsObject("v1")]
        public struct SaveData_v1
        {
            public Entity.MonsterCareers monsterCareer;
        }

        public override object GetSaveData()
        {
            SaveData_v1 data = new SaveData_v1();
            data.monsterCareer = monsterCareer;

            return data;
        }

        public override void RestoreSaveData(object dataIn)
        {
            if (dataIn == null)
                return;

            SaveData_v1 data = (SaveData_v1)dataIn;
            monsterCareer = data.monsterCareer;
        }

        #endregion
    }
}
