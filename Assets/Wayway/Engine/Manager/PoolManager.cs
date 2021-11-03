/* PoolManager.cs
 * Wayway Studio
 * Pooling Control
 * Assign GameObject
 * Odin Inspector Require
 * 2021.10.12 */

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Wayway.Engine.Pool;

namespace Wayway.Engine
{    
    public class PoolManager : MonoBehaviour
    {
        [SerializeField] private List<PoolChannel> poolingComponents;
        [SerializeField] private PoolChannel dummyPool;

        private Dictionary<PoolType, PoolChannel> channelTable;
        public Dictionary<PoolType, PoolChannel> ChannelTable
        {
            get
            {
                if (channelTable == null)
                {
                    channelTable = new Dictionary<PoolType, PoolChannel>();
                    poolingComponents.ForEach(x => channelTable.Add(x.Type, x));
                }

                return channelTable;
            }
        }

        public void RegisterPoolField(PoolType type, Transform transform) => GetPool(type).TargetField = transform;

        public void RegisterPool(List<Poolable> poolables) => poolables.ForEach(x => RegisterPool(x));
        public void RegisterPool(List<GameObject> prefabs) => prefabs.ForEach(x => RegisterPool(x));
        public void RegisterPool(Poolable poolable) => GetPool(poolable.Type).Regist(poolable);
        public void RegisterPool(GameObject prefab)
        {
            if (prefab.TryGetComponent<Poolable>(out var poolable))
            {
                RegisterPool(poolable);
            }
            else
                Debug.LogWarning($"Can't Find <Poolable> in {prefab.name}; Returning");
        }

        public GameObject Draw(Poolable poolable, bool activity = true)
            => Draw(poolable, poolable.gameObject.transform.position, poolable.gameObject.transform.rotation, activity);
        public GameObject Draw(Poolable poolable, Transform transform, bool activity = true) => Draw(poolable, transform.position, transform.rotation, activity);
        public GameObject Draw(Poolable poolable, Quaternion rotation, bool activity = true) => Draw(poolable, poolable.gameObject.transform.position, rotation, activity);
        public GameObject Draw(Poolable poolable, Vector3 position, bool activity = true) => Draw(poolable, position, poolable.gameObject.transform.rotation, activity);

        /// <summary>
        /// Draw instance by poolable in Prefab.Component
        /// </summary>
        /// <param name="poolable">Main Component of Pooling</param>
        /// <param name="position">initial position</param>
        /// <param name="rotation">initial rotation</param>
        /// <param name="activity">SetActive(activity) at OnDrawed</param>
        /// <returns>Pre Pooled or Instatiated Object</returns>
        public GameObject Draw(Poolable poolable, Vector3 position, Quaternion rotation, bool activity = true)
        {
            return GetPool(poolable.Type).Draw(poolable, position, rotation, activity);
        }


        public GameObject Draw(GameObject prefab, bool activity = true) => Draw(prefab, prefab.transform.position, prefab.transform.rotation, activity);
        public GameObject Draw(GameObject prefab, Transform transform, bool activity = true) => Draw(prefab, transform.position, transform.rotation, activity);
        public GameObject Draw(GameObject prefab, Quaternion rotation, bool activity = true) => Draw(prefab, prefab.transform.position, rotation, activity);
        public GameObject Draw(GameObject prefab, Vector3 position, bool activity = true) => Draw(prefab, position, prefab.transform.rotation, activity);

        /// <summary>
        /// Draw instance by prefab as a Key
        /// </summary>
        /// <param name="prefab">Key Prefab. actual key is <+Prefab.Poolable.ID : string></param>
        /// <param name="position">initial position</param>
        /// <param name="rotation">initial rotation</param>
        /// <param name="activity">SetActive(activity) at OnDrawed</param>
        /// <returns>Pre Pooled or Instatiated Object</returns>
        public GameObject Draw(GameObject prefab, Vector3 position, Quaternion rotation, bool activity = true)
        {
            if (prefab.TryGetComponent<Poolable>(out var poolableObject))
            {
                if (!poolableObject.PreventPool)
                {
                    return Draw(poolableObject, position, rotation, activity);
                }
                else
                {
                    return InstantiateWarning(prefab, position, rotation);
                }
            }
            else
                return InstantiateWarning(prefab, position, rotation);
        }

        public void Return(GameObject instance) => Return(instance, null);
        public void Return(GameObject instance, Action callback)
        {
            if (instance.TryGetComponent<Poolable>(out var result))
            {
                if (!result.PreventPool)
                {
                    Return(result, callback);
                }
                else
                {
                    result.OnReturned?.Invoke();
                    Destroy(instance);
                }
            }
            else
            {
                Destroy(instance);
            }
        }

        public void Return(GameObject instance, float delayTime) => StartCoroutine(DelayRoutine(instance, delayTime));
        public void Return(GameObject instance, float delayTime, Action callback) => StartCoroutine(DelayRoutine(instance, delayTime, callback));
        public void Return(Poolable poolable) => Return(poolable, null);
        public void Return(Poolable poolable, Action callback)
        {
            GetPool(poolable.Type).Return(poolable);

            callback?.Invoke();
        }

        private IEnumerator DelayRoutine(GameObject instance, float delayTime) => DelayRoutine(instance, delayTime, null);
        private IEnumerator DelayRoutine(GameObject instance, float delayTime, Action callback)
        {
            yield return new WaitForSeconds(delayTime);

            Return(instance, callback);
        }

        public void ClearPool() => poolingComponents.ForEach(x => x.ClearPool());

        private PoolChannel GetPool(PoolType type)
        {
            return ChannelTable.TryGetValue(type, out var result) ? result
                                                                    : dummyPool;
        }

        private GameObject InstantiateWarning(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            var result = Instantiate(prefab, position, rotation);
            result.transform.SetParent(dummyPool.transform);

            if (result.TryGetComponent<Poolable>(out var poolable))
            {
                poolable.OnDrawed?.Invoke();
                return result;
            }
            else
            {
                return result;
            }
        }

    }
    
}
