using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Wayway.Engine
{
    public static class GameObjectExtension
    {
        public static void DestroyAllChildren(this GameObject gameObject, bool includeInactive = false)
        {
            foreach (Transform child in gameObject.GetComponentInChildren<Transform>(includeInactive))
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        public static T GetComponentOnlyChildren<T>(this GameObject gameobject) where T : MonoBehaviour
        {
            return gameobject.GetComponentsInChildren<T>().Where(x => x.gameObject != gameobject).First();
        }

        public static List<T> GetComponentsInOnlyChildren<T>(this GameObject gameobject) where T : MonoBehaviour
        {
            var selfBehaviour = gameobject.GetComponent<T>();
            var result = new List<T>();

            gameobject.GetComponentsInChildren(result);

            if (selfBehaviour) result.Remove(selfBehaviour);

            return result;
        }
    }
}


