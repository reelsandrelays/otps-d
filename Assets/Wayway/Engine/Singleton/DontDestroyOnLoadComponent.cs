using UnityEngine;

namespace Wayway.Engine
{
    public class DontDestroyOnLoadComponent : MonoBehaviour
    {
        public bool ShowDebugMessage = false;
        public MonoBehaviour targetBehaviour;

        protected virtual void Awake()
        {
            if (!transform.root.gameObject.Equals(gameObject))
            {
                if (ShowDebugMessage)
                {
                    Debug.LogError($"Don't Destroyed Component must be in root gameObject! \n" +
                    $"Root Object is : {transform.root.gameObject} \n" +
                    $"Currrent Object is : {gameObject}");
                }

                return;
            }

            // Duplication Check        
            var components = FindObjectsOfType(targetBehaviour.GetType());

            if (components.Length > 1)
            {
                if (ShowDebugMessage)
                {
                    Debug.Log($"{targetBehaviour.GetType()} is came from another scene. \n" +
                       $"In this scene {targetBehaviour.GetType()} gameobject Destory");
                }

                /* Annotation */
                gameObject.SetActive(false);

                Destroy(gameObject);
            }
            else if (components.Length == 1)
            {
                if (ShowDebugMessage)
                {
                    Debug.Log($"Don't Destroyed On Load :: {targetBehaviour.GetType()} registed");
                }

                DontDestroyOnLoad(gameObject);
            }
            else
            {
                if (ShowDebugMessage)
                {
                    Debug.LogError($"Can't Find {targetBehaviour.GetType()}.");
                }
            }
        }
    }
}



/// Annotation
/// if (gameObject.SetActive(false)) skipped, Awake, Start called twice;
