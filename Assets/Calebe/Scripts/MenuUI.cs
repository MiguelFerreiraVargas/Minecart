using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public GameObject canvasMenu;
    public GameObject canvasOptions;

    public void AbrirOptions()
    {
        canvasMenu.SetActive(false);
        canvasOptions.SetActive(true);
    }

    public void VoltarMenu()
    {
        canvasMenu.SetActive(true);
        canvasOptions.SetActive(false);
    }
}