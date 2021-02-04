using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEventsManager_Sample : SceneEventsManager_Base
{
    // 0. 이 이벤트에 대한 반응의 등록을 아래의 SetEvents()
    public Event_Sample ProgressSampleScene;

    protected override void SetEvents()
    {
        // 1. 여기서 할 수도 있고
        ProgressSampleScene.Response += Response;
        ProgressSampleScene.Response += SceneManager_Sample.ProgressSampleScene;
    }

    // 2. 게임 창으로 돌아가서 Space 누르고 계속 진행하시겠슴다.

    private void Response(float parameter) { Debug.Log(ProgressSampleScene.ResponseList.Length); }
}
