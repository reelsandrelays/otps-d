using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckFPS : MonoBehaviour
{
    private Text fps;

    private float accum = 0f;
    private int frames = 0;

    private IEnumerator FPS()
    {
        while (true)
        {
            float fps = accum / frames;

            this.fps.text = fps.ToString("f" + Mathf.Clamp(1, 0, 10));

            this.fps.color = (fps >= 30) ? Color.green : ((fps > 10) ? Color.yellow : Color.red);

            accum = 0.0f;
            frames = 0;

            yield return new WaitForSeconds(.5f);
        }
    }

    private void Awake()
    {
        //Application.targetFrameRate = 60;
        fps = GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FPS());
    }

    // Update is called once per frame
    void Update()
    {
        accum += Time.timeScale / Time.deltaTime;
        frames++;
    }
}
