using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public void TrocarCena(string nomeCena)
    {
        SceneManager.LoadScene(nomeCena);
    }
}