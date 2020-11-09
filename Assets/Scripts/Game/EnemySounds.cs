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

using UnityEngine;
using System.Collections;
using DaggerfallWorkshop.Utility;

namespace DaggerfallWorkshop.Game
{
    /// <summary>
    /// Example enemy sounds component.
    /// Handles attract and attack sounds.
    /// </summary>
    [RequireComponent(typeof(DaggerfallAudioSource))]
    public class EnemySounds : MonoBehaviour
    {
        public float AttractRadius = 16f;           // Range at which enemy will play attract sounds like "move" and "bark"
        public int MinAttractDelay = 3;             // Minimum time between attract sounds
        public int MaxAttractDelay = 9;             // Maximum time between attract sounds
        public bool MuteHumanSounds = true;         // Human sounds are so bad even Daggerfall doesn't play them. Unmute here if you want them.
        public bool LinearRolloff = true;           // Linear rolloff is more Daggerfall-like
        public bool SoundsFromMobile;               // Assign sounds automatically based on a DaggerfallMobileUnit child object
        public SoundClips MoveSound;
        public SoundClips BarkSound;
        public SoundClips AttackSound;
        public Entity.Races RaceForSounds;

        AudioClip moveClip;
        AudioClip barkClip;
        AudioClip attackClip;

        GameObject player;
        DaggerfallMobileUnit mobile;
        DaggerfallAudioSource dfAudioSource;
        Vector3 directionToPlayer;
        float distanceToPlayer;
        bool playerInAttractRadius;
        float waitTime;
        float waitCounter;
        float volumeScale = 1f;

        void Awake()
        {
            // Save references
            dfAudioSource = GetComponent<DaggerfallAudioSource>();
            player = GameObject.FindGameObjectWithTag("Player");
            mobile = GetComponentInChildren<DaggerfallMobileUnit>();

            // Setup audio source
            dfAudioSource.AudioSource.maxDistance = AttractRadius;
            if (LinearRolloff)
                dfAudioSource.AudioSource.rolloffMode = AudioRolloffMode.Linear;
            dfAudioSource.AudioSource.spatialBlend = 1;

            // Assign sounds from mobile
            if (SoundsFromMobile && mobile)
            {
                MoveSound = (SoundClips)mobile.Summary.Enemy.MoveSound;
                BarkSound = (SoundClips)mobile.Summary.Enemy.BarkSound;
                AttackSound = (SoundClips)mobile.Summary.Enemy.AttackSound;
            }

            RaceForSounds = (Entity.Races)Random.Range(1, 5 + 1);

            // Start attract timer
            StartWaiting();
        }

        void FixedUpdate()
        {
            if (IsReady() && player)
            {
                directionToPlayer = player.transform.position - transform.position;
                distanceToPlayer = directionToPlayer.magnitude;
                playerInAttractRadius = (distanceToPlayer < AttractRadius);
            }
            else
            {
                playerInAttractRadius = false;
            }

            // Step timer.
            // Keep stepping even when player not in attract radius.
            // This means the player will get audio feedback the moment an enemy is near.
            waitCounter += Time.deltaTime;
            if (waitCounter > waitTime && playerInAttractRadius)
            {
                PlayAttractSound();
                StartWaiting();
            }
        }

        #region Custom Physical Hit Sounds

        public void PlayShieldBlockSound(Items.DaggerfallUnityItem weapon, bool metalShield)
        {
            if (IsReady())
            {
                if (weapon != null)
                {
                    if (metalShield)
                    {
                        AudioClip clip = CustomAudioClipImporter.ImportAudioClipMain.myMod.GetAsset<AudioClip>(CustomAudioClipImporter.ImportAudioClipMain.metalShieldClips[Random.Range(0, 8)]);
                        CustomAudioClipImporter.ImportAudioClipMain.audioSource.PlayOneShot(clip);
                    }
                    else
                    {
                        AudioClip clip = CustomAudioClipImporter.ImportAudioClipMain.myMod.GetAsset<AudioClip>(CustomAudioClipImporter.ImportAudioClipMain.leatherShieldClips[Random.Range(0, 9)]);
                        CustomAudioClipImporter.ImportAudioClipMain.audioSource.PlayOneShot(clip);
                    }
                }
                else
                {
                    if (metalShield)
                    {
                        AudioClip clip = CustomAudioClipImporter.ImportAudioClipMain.myMod.GetAsset<AudioClip>(CustomAudioClipImporter.ImportAudioClipMain.metalShieldClips[Random.Range(0, 8)]);
                        CustomAudioClipImporter.ImportAudioClipMain.audioSource.PlayOneShot(clip);
                    }
                    else
                    {
                        AudioClip clip = CustomAudioClipImporter.ImportAudioClipMain.myMod.GetAsset<AudioClip>(CustomAudioClipImporter.ImportAudioClipMain.leatherShieldClips[Random.Range(0, 9)]);
                        CustomAudioClipImporter.ImportAudioClipMain.audioSource.PlayOneShot(clip);
                    }
                }
            }
        }

        public void PlayArmorAbsorbSound(Items.DaggerfallUnityItem weapon, bool metalArmor)
        {
            if (IsReady())
            {
                if (weapon != null)
                {
                    if (metalArmor)
                    {
                        AudioClip clip = CustomAudioClipImporter.ImportAudioClipMain.myMod.GetAsset<AudioClip>(CustomAudioClipImporter.ImportAudioClipMain.metalArmorClips[Random.Range(0, 12)]);
                        CustomAudioClipImporter.ImportAudioClipMain.audioSource.PlayOneShot(clip);
                    }
                    else
                    {
                        AudioClip clip = CustomAudioClipImporter.ImportAudioClipMain.myMod.GetAsset<AudioClip>(CustomAudioClipImporter.ImportAudioClipMain.leatherArmorClips[Random.Range(0, 12)]);
                        CustomAudioClipImporter.ImportAudioClipMain.audioSource.PlayOneShot(clip);
                    }
                }
                else
                {
                    if (metalArmor)
                    {
                        AudioClip clip = CustomAudioClipImporter.ImportAudioClipMain.myMod.GetAsset<AudioClip>(CustomAudioClipImporter.ImportAudioClipMain.metalArmorClips[Random.Range(0, 12)]);
                        CustomAudioClipImporter.ImportAudioClipMain.audioSource.PlayOneShot(clip);
                    }
                    else
                    {
                        AudioClip clip = CustomAudioClipImporter.ImportAudioClipMain.myMod.GetAsset<AudioClip>(CustomAudioClipImporter.ImportAudioClipMain.leatherArmorClips[Random.Range(0, 12)]);
                        CustomAudioClipImporter.ImportAudioClipMain.audioSource.PlayOneShot(clip);
                    }
                }
            }
        }

        public void PlayWeaponHitSound(Items.DaggerfallUnityItem weapon, int mainDamType, bool critStrikeSuccess)
        {
            if (IsReady())
            {
                if (critStrikeSuccess)
                {
                    if (weapon != null)
                    {
                        if (mainDamType == 1)
                        {
                            AudioClip clip = CustomAudioClipImporter.ImportAudioClipMain.myMod.GetAsset<AudioClip>(CustomAudioClipImporter.ImportAudioClipMain.critBludgeoningHitClips[Random.Range(0, 14)]);
                            CustomAudioClipImporter.ImportAudioClipMain.audioSource.PlayOneShot(clip);
                        }
                        else if (mainDamType == 2)
                        {
                            AudioClip clip = CustomAudioClipImporter.ImportAudioClipMain.myMod.GetAsset<AudioClip>(CustomAudioClipImporter.ImportAudioClipMain.critSlashingHitClips[Random.Range(0, 13)]);
                            CustomAudioClipImporter.ImportAudioClipMain.audioSource.PlayOneShot(clip);
                        }
                        else if (mainDamType == 3)
                        {
                            AudioClip clip = CustomAudioClipImporter.ImportAudioClipMain.myMod.GetAsset<AudioClip>(CustomAudioClipImporter.ImportAudioClipMain.critPiercingHitClips[Random.Range(0, 14)]);
                            CustomAudioClipImporter.ImportAudioClipMain.audioSource.PlayOneShot(clip);
                        }
                        else
                        {
                            PlayHitSound(weapon);
                        }
                    }
                    else
                    {
                        PlayHitSound(weapon);
                    }
                }
                else
                {
                    if (weapon != null)
                    {
                        if (mainDamType == 1)
                        {
                            PlayHitSound(weapon);
                        }
                        else if (mainDamType == 2)
                        {
                            PlayHitSound(weapon);
                        }
                        else if (mainDamType == 3)
                        {
                            PlayHitSound(weapon);
                        }
                        else
                        {
                            PlayHitSound(weapon);
                        }
                    }
                    else
                    {
                        PlayHitSound(weapon);
                    }
                }
            }
        }

        #endregion

        public void PlayAttackSound()
        {
            if (IsReady())
            {
                // Humans must be flagged to play sounds
                if (IgnoreHumanSounds())
                    return;

                // Play attack sound only about half the time
                if (Random.value > 0.5f)
                    dfAudioSource.AudioSource.PlayOneShot(attackClip, volumeScale * DaggerfallUnity.Settings.SoundVolume);
            }
        }

        public void PlayHitSound(Items.DaggerfallUnityItem weapon)
        {
            if (IsReady())
            {
                int sound;
                if (weapon != null)
                {
                    sound = (int)SoundClips.Hit1 + Random.Range(0, 5);
                }
                else
                {
                    sound = (int)SoundClips.Hit1 + Random.Range(2, 4);
                }

                dfAudioSource.PlayOneShot(sound, 1, 1.1f);
            }
        }

        public void PlayParrySound()
        {
            if (IsReady())
            {
                int sound = (int)SoundClips.Parry1 + UnityEngine.Random.Range(0, 9);
                dfAudioSource.PlayOneShot(sound, 1, 1.1f);
            }
        }

        public void PlayMissSound(Items.DaggerfallUnityItem weapon)
        {
            if (IsReady())
            {
                if (weapon != null)
                {
                    dfAudioSource.PlayOneShot(weapon.GetSwingSound());
                }
                else
                {
                    dfAudioSource.PlayOneShot(SoundClips.SwingHighPitch);
                }
            }
        }

        public void PlayCombatVoice(Entity.Genders gender, bool isAttack, bool heavyDamage = false)
        {
            // Male high elf sounds sound odd when coming from NPCs. Switch out for wood elf.
            if (gender == Entity.Genders.Male && RaceForSounds == Entity.Races.HighElf)
                RaceForSounds = Entity.Races.WoodElf;

            if (IsReady())
            {
                SoundClips sound;
                if (isAttack)
                    sound = Entity.DaggerfallEntity.GetRaceGenderAttackSound(RaceForSounds, gender);
                else
                    sound = Entity.DaggerfallEntity.GetRaceGenderPainSound(RaceForSounds, gender, heavyDamage);

                float pitch = dfAudioSource.AudioSource.pitch;
                dfAudioSource.AudioSource.pitch = pitch + Random.Range(0, 0.3f);
                dfAudioSource.PlayOneShot(sound);
                dfAudioSource.AudioSource.pitch = pitch;
            }
        }

        #region Private Methods

        private bool IsReady()
        {
            if (dfAudioSource == null)
                return false;

            if (!moveClip)
                moveClip = dfAudioSource.GetAudioClip((int)MoveSound);
            if (!barkClip)
                barkClip = dfAudioSource.GetAudioClip((int)BarkSound);
            if (!attackClip)
                attackClip = dfAudioSource.GetAudioClip((int)AttackSound);

            return true;
        }

        private void StartWaiting()
        {
            // Reset countdown to next sound
            waitTime = Random.Range(MinAttractDelay, MaxAttractDelay + 1);
            waitCounter = 0;
        }

        private void PlayAttractSound()
        {
            // Humans must be flagged to play sounds
            if (IgnoreHumanSounds())
                return;

            // Dampen volume if something is between enemy and player
            SetVolumeScale();

            // Random chance favors bark sound
            if (Random.value > 0.8f)
                dfAudioSource.AudioSource.PlayOneShot(moveClip, volumeScale * DaggerfallUnity.Settings.SoundVolume);
            else
                dfAudioSource.AudioSource.PlayOneShot(barkClip, volumeScale * DaggerfallUnity.Settings.SoundVolume);
        }

        private bool IgnoreHumanSounds()
        {
            if (mobile.Summary.Enemy.ID > 127 && mobile.Summary.Enemy.ID != 146 && MuteHumanSounds)
                return true;
            else
                return false;
        }

        private void SetVolumeScale()
        {
            // Start at full volume scale
            volumeScale = 1f;

            // If something is between enemy and player then dampen sound.
            // This is a basic cheat for sound propagation.
            // Only checks when enemy plays attract sound, so not very expensive.
            RaycastHit hit;
            Ray ray = new Ray(transform.position, directionToPlayer);
            if (Physics.Raycast(ray, out hit))
            {
                // Ignore player hit
                if (hit.transform.gameObject == player)
                    return;

                // Doors aren't static but we want them to dampen also
                DaggerfallActionDoor door = hit.transform.GetComponent<DaggerfallActionDoor>();

                // Dampen based on hit
                if (GameObjectHelper.IsStaticGeometry(hit.transform.gameObject) || door)
                    volumeScale = 0.25f;
            }
        }

        #endregion
    }
}