using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    private float desiredAlpha;
    private float currentAlpha;
    public Image fadeImage;

    private void Start()
    {
        desiredAlpha = 1.0f;
        currentAlpha = fadeImage.color.a;
    }

    void Update()
    {
        currentAlpha = Mathf.MoveTowards(currentAlpha, desiredAlpha, 2.0f * Time.deltaTime);
    }
}