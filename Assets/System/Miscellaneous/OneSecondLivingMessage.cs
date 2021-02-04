using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneSecondLivingMessage : MonoBehaviour
{
    private IEnumerator LifeTime()
    {
        Text text = GetComponent<Text>();
        text.color = Color.white;

        float lifeTime = 1.0f;

        while (lifeTime > 0f)
        {
            lifeTime -= Time.deltaTime;
            text.color = new Color(1, 1, 1, lifeTime);

            yield return null;
        }

        gameObject.SetActive(false);
    }

    private void OnEnable() { StartCoroutine(LifeTime()); }
}
