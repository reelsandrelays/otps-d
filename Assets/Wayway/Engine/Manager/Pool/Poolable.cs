using UnityEngine;
using UnityEngine.Events;

namespace Wayway.Engine.Pool
{
    public class Poolable : MonoBehaviour
    {
        public UnityEvent OnDrawed;
        public UnityEvent OnReturned;

        [SerializeField] private bool preventPool = false;
        [SerializeField] private string iD;
        [SerializeField] private PoolType type;
        [SerializeField] private int poolCount;

        public bool PreventPool =>
#if UNITY_EDITOR
        preventPool;
#else
        false;
#endif

        public string ID
        {
            get
            {
                if (string.IsNullOrEmpty(iD))
                {
                    iD = gameObject.name.Replace("(Clone)", "").Replace(" ", "");
                }
                return iD;
            }
        }
        public PoolType Type => type;
        public int PoolCount => poolCount;

        /// <summary>
        /// UnComment if PoolManager Assigned
        /// </summary>
        public void Return() { return; } // => Maingame.PoolManager.Return(this);


#if UNITY_EDITOR
        private void Reset() => GenerateID();

        void GenerateID()
        {
            iD = gameObject.name.Replace("(Clone)", "").Replace(" ", "");
            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif
    }
}


