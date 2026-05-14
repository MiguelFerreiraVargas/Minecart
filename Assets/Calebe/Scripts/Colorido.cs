using TMPro;
using UnityEngine;

public class Colorido : MonoBehaviour
{
    public TMP_Text texto;
    public float velocidade = 2f;

    void Update()
    {
        float r = Mathf.Sin(Time.time * velocidade) * 0.5f + 0.5f;
        float g = Mathf.Sin(Time.time * velocidade + 2f) * 0.5f + 0.5f;
        float b = Mathf.Sin(Time.time * velocidade + 4f) * 0.5f + 0.5f;

        texto.color = new Color(r, g, b);
    }
}