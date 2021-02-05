using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager_Sample : SceneManager_Base
{
    public Text guideMessage;
    public GuideObject capsule;

    private List<GuideObject> guideObject;

    private int progression;

    // Start is called before the first frame update
    void Start()
    {
        guideObject = new List<GuideObject>();
        progression = 0;

        UpdateGuideMessage();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            progression++;

            if (progression > 11) return;

            (EventsManager as SceneEventsManager_Sample)?.ProgressSampleScene.Invoke(0);
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            progression--;

            if (progression < 0) progression = 0;

            (EventsManager as SceneEventsManager_Sample)?.ProgressSampleScene.Invoke(0);
        }
    }

    public static void ProgressSampleScene(float parameter)
    {
        for (int i = (Instance as SceneManager_Sample).guideObject.Count - 1; i >= 0; i--)
        {
            if ((Instance as SceneManager_Sample).guideObject[i].life <= 0)
            {
                Destroy((Instance as SceneManager_Sample).guideObject[i].gameObject);
                (Instance as SceneManager_Sample).guideObject.RemoveAt(i);
            }
        }

        if ((Instance as SceneManager_Sample).guideObject.Count > 0) (Instance as SceneManager_Sample).progression = 8;

        switch ((Instance as SceneManager_Sample).progression)
        {
            case 7:
                {
                    (Instance as SceneManager_Sample).guideObject.Add(Instantiate((Instance as SceneManager_Sample).capsule, new Vector3(-5, 0), Quaternion.identity));
                    (Instance as SceneManager_Sample).guideObject.Add(Instantiate((Instance as SceneManager_Sample).capsule, new Vector3(5, 0), Quaternion.identity));
                    (Instance as SceneManager_Sample).UpdateGuideMessage();
                    break;
                }
            default:
                {
                    (Instance as SceneManager_Sample).UpdateGuideMessage();
                    break;
                }
        }
    }

    public void UpdateGuideMessage() { guideMessage.text = GuideMessage[progression]; }

    private readonly string[] GuideMessage = new string[]
    {
        "샘플 신임다. Space 바를 눌러서 진행하심 됨다.",
        "CustomEvent에 대해 설명 드리겠슴다. Space ->",
        "먼저 Assets/Samples 폴더 안의 Event_Sample 오브젝트를 눌러서 Tooltip을 확인하시죠.",
        "CustomEvent들은 UnityEvent에 비해 에디터 상에서 할 수 있는 건 별로 없슴다. Space ->",
        "대신 코드 상에서 편하게 운용할 수 있도록 나름대로 이렇게 저렇게 해 본 형태가 현재의 구조임다. Space ->",

        "각 CustomEvent에 대한 반응의 등록은 SceneEventsManager에서 할 수도 있고 반응을 할 오브젝트에서 직접 할 수도 있슴다. Space ->",
        "Hierarchy의 SceneEventsManager_Sample에 붙어 있는 SceneEventsManager_Sample 스크립트를 확인하시겠슴다.",
        "새로 나타난 캡슐 중에 아무 거나 골라서 붙어 있는 GuideObject 스크립트를 확인하시겠슴다. Hierarchy의 Capsule(Clone)임다.",
        "이 캡슐들 다 없어질 때까지 Space 바 눌러서 진행하시죠.",
        "이게 다임다. 기존의 것은 이벤트 오브젝트(SO)를 만들고\nEvent Listener 스크립트 컴포넌트를 만든 다음\n만든 스크립트를 이벤트에 반응할 오브젝트에 붙이거나\n리스너 오브젝트를 따로 만들어놓고 거기에 붙여서\n반응을 시키는 구조였는데 Space ->",

        "지금 이 구조는 이벤트 오브젝트를 만든 다음의 과정들을 유니티 에디터에서 수행하다가 비주얼 스튜디오에서 수행하도록 바뀐 거라 보심 됨다. Space ->",
        "그래도 모르시겠거나 궁금하신 점 있으시면 물어봐주심 되겟슴다."
    };
}
