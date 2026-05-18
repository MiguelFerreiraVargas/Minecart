using UnityEngine;
using UnityEngine.SceneManagement;

public class Pressione : MonoBehaviour
{
    [SerializeField] private string nomeDaCena;

    private bool podeContinuar = true;

    private void Update()
    {
        VerificarEntrada();
    }

    private void VerificarEntrada()
    {
        if (podeContinuar && Input.anyKeyDown)
        {
            CarregarCena();
        }
    }

    private void CarregarCena()
    {
        SceneManager.LoadScene(nomeDaCena);
    }
}