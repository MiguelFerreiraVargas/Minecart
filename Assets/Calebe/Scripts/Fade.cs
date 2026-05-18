using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TransicaoCena : MonoBehaviour
{
    [SerializeField] private Animator fadeAnimator;
    [SerializeField] private string proximaCena;
    [SerializeField] private float tempoFade = 1f;

    private bool carregando = false;

    private void Start()
    {
        FadeEntrada();
    }

    private void Update()
    {
        VerificarEntrada();
    }

    private void VerificarEntrada()
    {
        if (!carregando && Input.anyKeyDown)
        {
            StartCoroutine(TrocarCena());
        }
    }

    private void FadeEntrada()
    {
        fadeAnimator.SetTrigger("FadeIn");
    }

    private IEnumerator TrocarCena()
    {
        carregando = true;

        fadeAnimator.SetTrigger("FadeOut");

        yield return new WaitForSeconds(tempoFade);

        SceneManager.LoadScene(proximaCena);
    }
}