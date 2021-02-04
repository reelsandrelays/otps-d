using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SWayptableObject : ScriptableObject
{
    protected SceneManager_Base SceneInstance { get { return SceneManager_Base.Instance; } }
    protected Player_Base PlayerInstance { get { return Player_Base.Instance; } }
}
