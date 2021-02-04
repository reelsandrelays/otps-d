using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBeWayviour : MonoBehaviour
{
    protected SceneManager_Base SceneInstance { get { return SceneManager_Base.Instance; } }
    protected Player_Base PlayerInstance { get { return Player_Base.Instance; } }
    protected SceneEventsManager_Base EventsManager { get { return SceneEventsManager_Base.Instance; } }
}
