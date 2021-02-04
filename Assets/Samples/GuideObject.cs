using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 0. 얘네처럼 반응을 할 오브젝트에
public class GuideObject : MonoBeWayviour
{
    // 1. 이벤트를 먹여서
    public Event_Sample sampleEvent;

    public Material[] material;

    public string objectName;

    public int life;
    
    void Start()
    {
        life = Random.Range(2, 6);
        objectName = Name[Random.Range(0, Name.Length)];

        // 2. 자기들이 직접 등록하게 할 수도 있슴다.
        sampleEvent.Response += Response;

        // 3. 아니면 이렇게 SceneEvent_Manager를 경유해서 등록해도 됨다.
        //(EventsManager as SceneEventsManager_Sample).ProgressSampleScene.Response += Response;
    }

    // 4. 다만 이런 케이스는 이렇게 비활성화 될 때 꼭 해제해주어야 하겠슴다.
    private void OnDisable() { sampleEvent.Response -= Response; }

    // 5. 다시 게임 창으로 돌아가서 진행하시겠슴다.

    private void Response(float parameter)
    {
        Debug.Log(objectName);
        GetComponent<MeshRenderer>().material = material[Random.Range(0, material.Length)];

        life--;
    }

    private readonly string[] Name = new string[] { "김희성 1", "김희성 2", "김희성 3", "김희성 4", "김희성 5", "김희성 6", "김희성 7", "김희성 8" };
}
