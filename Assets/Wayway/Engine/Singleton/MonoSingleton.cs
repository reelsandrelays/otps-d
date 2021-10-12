using UnityEngine;
using System;

namespace Wayway.Engine
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public bool ShowDebugMessage = false;

        private static T instance;
        private static object _lock = new object();
        private static bool isFirst = true;

        public static T Instance
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        Type type = typeof(T);

                        instance = (T)FindObjectOfType(type);

                        if (FindObjectsOfType(type).Length > 1)
                        {
                            Debug.LogError($"[Singleton] Same【{type.Name}】Duplication.");
                            return instance;
                        }
                        else if (instance == null)
                        {
                            Debug.LogWarning($"[Singleton] 【{type.Name}】Is Null. \n" +
                                $"Project MonoSingleton doesn't make Auto {type.Name} gameObject. \n" +
                                $"return Null;");

                            return null;
                        }
                    }
                }

                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (isFirst)
            {
                instance = this as T;
                isFirst = false;

                if (ShowDebugMessage)
                    Debug.Log($"[Singleton] Create instance at firstTime : 【{instance.GetType().Name}】");
            }
            else
            {
                if (instance != null)
                {
                    Debug.Log(
                        $"[Singleton] 【{instance.GetType().Name}】 Is already Exist. \n" +
                        $"Called From【{instance.gameObject.name}】 gameObject. \n");
                }
                else
                {
                    instance = this as T;

                    if (ShowDebugMessage)
                        Debug.Log($"[Singleton] Override instance : 【{instance.GetType().Name}】");
                }
            }
        }

        protected virtual void OnDestroy()
        {
            if (instance != null && instance.gameObject == gameObject)
            {
                if (ShowDebugMessage)
                    Debug.Log($"[Singleton] Destory instance : 【{instance.GetType().Name}】");

                instance = (T)((object)null);
            }
        }
    }
}