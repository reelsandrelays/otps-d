using System.Collections.Generic;
using System.Linq;
using System;

using UnityEngine;
using UnityEngine.Audio;

using Wayway.Engine.Audio;
using AudioType = Wayway.Engine.Audio.AudioType;

namespace Wayway.Engine
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioMixer masterMixer;
        [SerializeField] private List<AudioChannel> audioChannels;

        private Dictionary<AudioType, AudioChannel> audioTable;
        private AudioSource oneShotSource;

        /* Exposed Volume Parameta from AudioMixer >> Volume.EVP  */
        private const string MASTER_EVP = "MasterVolume";
        private const string BGM_EVP = "Bgm.Volume";
        private const string SFX_GENERAL_EVP = "General.SfxVolume";
        private const string SFX_PROJECTILE_EVP = "Projectile.SfxVolume";
        private const string SFX_UI_EVP = "UI.SfxVolume";
        private const string SFX_BUMPER_EVP = "Bumper";

        public AudioSource OneShotSource => oneShotSource;

        public void SetUp()
        {
            audioTable = new Dictionary<AudioType, AudioChannel>();
            oneShotSource = GetComponent<AudioSource>();

            GetComponentsInChildren(audioChannels);

            audioChannels.ForEach(x => x.SetUp());
            audioChannels.ForEach(x => audioTable.Add(x.Type, x));
            audioTable.ForEach(x => x.Value.ExposedVolumeParameter = GetEvp(x.Value.Type));
        }

        public void Play(IAudioPlayable clipData) { if (TryGetChannel(clipData, out var source)) source.Play(clipData); }
        public void Pause(IAudioPlayable clipData) { if (TryGetChannel(clipData, out var source)) source.Pause(clipData); }
        public void Stop(IAudioPlayable clipData) { if (TryGetChannel(clipData, out var source)) source.Stop(clipData); }

        public void StopAll(Action callback) { StopAll(); callback?.Invoke(); }
        public void StopAll() => audioChannels.ForEach(x => x.StopAll());
        public void StopAll(AudioChannel channel) => channel.StopAll();
        public void StopAll(AudioType type) { if (TryGetChannel(type, out var result)) result.StopAll(); }

        public void PlayOneShot(AudioClipData audioClipData) { if (OneShotSource != null) OneShotSource.PlayOneShot(audioClipData.AudioClip); }
        public void StopOneShot() { if (OneShotSource != null) OneShotSource.Stop(); }

        // Volume Control
        public void MasterFadeOut(float duration) => MasterFadeOut(duration, () => { StopAll(); MasterFadeIn(0); });
        public void MasterFadeOut(float duration, Action callback) => MasterVolumeFade(-80f, duration, callback);
        public void MasterFadeIn(float duration) => MasterVolumeFade(0f, duration);
        public void AudioFadeOut(AudioType type, float duration) { if (TryGetChannel(type, out var result)) result.AudioFadeOut(duration); }
        public void AudioFadeIn(AudioType type, float duration) { if (TryGetChannel(type, out var result)) result.AudioFadeIn(duration); }
        public void Mute(AudioType type, bool value) { if (TryGetChannel(type, out var result)) result.Mute(value); }
        public void MuteAll(bool value) => audioTable.ForEach((pair) => Mute(pair.Key, value));


        private bool TryGetChannel(IAudioPlayable clipData, out AudioChannel result) => audioTable.TryGetValue(clipData.Type, out result);
        private bool TryGetChannel(AudioType type, out AudioChannel result) => audioTable.TryGetValue(type, out result);
        private void SetMasterVolume(float destVolume) => masterMixer.SetFloat("MasterVolume", destVolume);
        private void MasterVolumeFade(float target, float duration) => MasterVolumeFade(target, duration, null);
        private void MasterVolumeFade(float target, float duration, Action callback)
        {
            masterMixer.GetFloat(MASTER_EVP, out var currentVolume);

            LeanTween.cancel(transform.gameObject);
            LeanTween.value(transform.gameObject, SetMasterVolume, currentVolume, target, duration).setOnComplete(callback);
        }

        private string GetEvp(AudioType type) => type switch
        {
            AudioType.Bgm => BGM_EVP,
            AudioType.SfxGeneral => SFX_GENERAL_EVP,
            AudioType.SfxProjectile => SFX_PROJECTILE_EVP,
            AudioType.SfxUI => SFX_UI_EVP,
            _ => SFX_BUMPER_EVP,
        };

#if UNITY_EDITOR
        #region -editor Function :: GetAudioChannels
        void GetAudioChannels()
        {
            audioChannels = GetComponentsInChildren<AudioChannel>().ToList();
            audioChannels.ForEach(x => audioTable.Add(x.Type, x));
        }
        #endregion
#endif
    }
}

