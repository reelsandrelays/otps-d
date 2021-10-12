using System.Collections.Generic;
using UnityEngine;

namespace Wayway.Engine.Audio
{
    public class AudioClipRandomGroup : ScriptableObject, IAudioPlayable
    {
        [SerializeField] private List<AudioClipData> clipList;
        private AudioClipData pickedClip;

        public AudioType Type => pickedClip != null ? pickedClip.Type : AudioType.Bumper;
        public AudioClip AudioClip => pickedClip != null ? pickedClip.AudioClip : null;
        public int Priority => pickedClip != null ? pickedClip.Priority : 256;

        public void Play()
        {
            if (clipList == null) return;

            pickedClip = clipList[Random.Range(0, clipList.Count - 1)];
            pickedClip.Play();
        }

        public void Pause() { if (pickedClip != null) pickedClip.Pause(); }
        public void Stop() { if (pickedClip != null) pickedClip.Stop(); }

#if UNITY_EDITOR
        #region editor Function :: play random & stop
        void PlayPreviewClip()
        {
            AudioClipUtility.StopAllClips();
            AudioClipUtility.PlayClip(clipList.Random().AudioClip);
        }
        void StopPreviewClip() => AudioClipUtility.StopAllClips();
        #endregion
#endif
    }
}


