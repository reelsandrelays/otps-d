using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Wayway.Engine.Audio
{
    public class AudioWindowEditor : OdinMenuEditorWindow
    {
        List<AudioClipData> audioClipDatas;

        [MenuItem("Tools/AudioManager")]
        public static void OpenWidow()
        {
            var window = GetWindow<AudioWindowEditor>();

            window.minSize = new Vector2(800f, 800f);
            window.MenuWidth = 280f;
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();

            audioClipDatas = ScriptableObjectUtility.GetScriptableObjects<AudioClipData>();

            DrawBgmAudioClipData(tree, "BGM clipdata", AudioType.Bgm);
            DrawBgmAudioClipData(tree, "SFX.General clipdata", AudioType.SfxGeneral);
            DrawBgmAudioClipData(tree, "SFX.Projectile clipdata", AudioType.SfxProjectile);
            DrawBgmAudioClipData(tree, "SFX.UI clipdata", AudioType.SfxUI);
            DrawBgmAudioClipData(tree, "SFX.Bumper clipdata", AudioType.Bumper);
            DrawBgmAudioClipData(tree, "Un Sorted", new List<AudioType>() { AudioType.None });

            tree.EnumerateTree().AddThumbnailIcons(true);

            return tree;
        }

        void DrawBgmAudioClipData(OdinMenuTree tree, string groupName, AudioType type)
        {
            var drawer = new AudioClipDataDrawer(audioClipDatas.FindAll(x => x.Type == type));

            tree.Add(groupName, drawer);
        }
        void DrawBgmAudioClipData(OdinMenuTree tree, string groupName, List<AudioType> type)
        {
            var drawer = new AudioClipDataDrawer(audioClipDatas.FindAll(x => type.Contains(x.Type)));

            tree.Add(groupName, drawer);
        }

        public class AudioClipDataDrawer
        {
            public AudioClipDataDrawer(List<AudioClipData> clips) { Clips = clips; }
            [TableList] public List<AudioClipData> Clips;
        }
    }
}

