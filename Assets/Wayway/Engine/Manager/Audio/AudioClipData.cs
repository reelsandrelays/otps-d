using UnityEngine;

namespace Wayway.Engine.Audio
{
    public class AudioClipData : ScriptableObject, IAudioPlayable
    {
        [SerializeField] private AudioType type;
        [SerializeField] private AudioClip audioClip;
        [SerializeField] private int priority;

        public AudioType Type => type;
        public AudioClip AudioClip => audioClip;
        public int Priority => priority;

        /// <summary>
        /// UnComment if AudioManager is Assigned;
        /// </summary>
        public void Play() { return; }// => Maingame.AudioManager.Play(this);
        public void Pause() { return; }// => Maingame.AudioManager.Pause(this);
        public void Stop() { return; }// => Maingame.AudioManager.Stop(this);

#if UNITY_EDITOR
        #region editor Function :: play & stop
        void PlayPreviewClip()
        {
            AudioClipUtility.StopAllClips();
            AudioClipUtility.PlayClip(AudioClip);
        }
        void StopPreviewClip() => AudioClipUtility.StopAllClips();
        #endregion
#endif
    }
}


