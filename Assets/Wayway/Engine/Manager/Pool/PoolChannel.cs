using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Wayway.Engine.Pool
{
    public class PoolChannel : MonoBehaviour
    {
        [SerializeField] private PoolType type;
        [SerializeField] private Transform targetField;

        private Dictionary<string, List<GameObject>> poolTable;

        public PoolType Type => type;
        public Transform TargetField { get => targetField ??= transform; set => targetField = value; }
        public Dictionary<string, List<GameObject>> PoolTable => poolTable ??= new Dictionary<string, List<GameObject>>();

        public void Regist(Poolable poolable)
        {
            if (!PoolTable.ContainsKey(poolable.ID))
                NewPool(poolable);
        }

        public GameObject Draw(Poolable poolable, Vector3 position, Quaternion rotation, bool active)
        {
            var poolObject = GetPooledObject(poolable);

            poolObject.transform.SetParent(TargetField);                        // 1.Hierarchy
            poolObject.transform.SetPositionAndRotation(position, rotation);    // 2.object Transform
            poolObject.SetActive(active);                                       // 3.Activeness : default == true
            poolObject.GetComponent<Poolable>().OnDrawed?.Invoke();             // 4.OnDrawed Event Call

            return poolObject;
        }

        public void Return(Poolable poolable)
        {
            if (!PoolTable.ContainsKey(poolable.ID))
                NewPool(poolable);

            PoolTable[poolable.ID].Add(poolable.gameObject);

            poolable.gameObject.transform.SetParent(transform);
            poolable.gameObject.SetActive(false);
            poolable.OnReturned?.Invoke();
        }

        public void ClearPool()
        {
            PoolTable.Clear();
            gameObject.DestroyAllChildren(true);
        }


        private GameObject GetPooledObject(Poolable poolable)
        {
            if (!PoolTable.ContainsKey(poolable.ID))
                NewPool(poolable);

            if (PoolTable[poolable.ID].IsNullOrEmpty())
                NewInstance(poolable);

            var target = PoolTable[poolable.ID].First();

            PoolTable[poolable.ID].Remove(target);

            return target;
        }

        private void NewPool(Poolable poolable)
        {
            PoolTable.Add(poolable.ID, new List<GameObject>());

            AddInstance(poolable);
        }

        private void AddInstance(Poolable poolable)
        {
            int addCount = poolable.PoolCount - PoolTable[poolable.ID].Count;

            for (var i = 0; i < addCount; ++i)
            {
                NewInstance(poolable);
            }
        }

        private GameObject NewInstance(Poolable poolable)
        {
            var newInstance = Instantiate(poolable.gameObject, transform);
            newInstance.SetActive(false);

            PoolTable[poolable.ID].Add(newInstance);

            return newInstance;
        }
    }
}