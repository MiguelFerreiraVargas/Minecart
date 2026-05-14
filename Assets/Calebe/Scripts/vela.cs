using UnityEngine;

public class vela : MonoBehaviour
{
    private Light luz;
    private GameObject chama;
    private bool apagada = false;

    void Start()
    {
        // Pega automaticamente a luz e a chama dentro dessa parte
        luz = GetComponentInChildren<Light>();
        chama = transform.Find("Chama")?.gameObject;
    }

    void OnMouseDown()
    {
        ToggleVela();
    }

    void ToggleVela()
    {
        apagada = !apagada;

        if (luz != null)
            luz.enabled = !apagada;

        if (chama != null)
            chama.SetActive(!apagada);

        Debug.Log("Clicou em: " + gameObject.name);
    }
}