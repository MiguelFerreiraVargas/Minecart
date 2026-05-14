using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeOut : MonoBehaviour
{
    public Image fadeImage;
    public float fadeSpeed = 1.5f;

    public void TrocarCena(string nomeCena)
    {
        StartCoroutine(fadeOut(nomeCena));
    }

    IEnumerator fadeOut(string nomeCena)
    {
        float alpha = 0;

        while (alpha < 1)
        {
            alpha += Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        SceneManager.LoadScene(nomeCena);
    }
}