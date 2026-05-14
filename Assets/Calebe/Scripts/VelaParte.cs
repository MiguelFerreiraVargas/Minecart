using UnityEngine;

public class VelaParte : MonoBehaviour
{
    private Light luz;
    private Renderer[] renderers;
    private bool apagada = false;

    void Start()
    {
        luz = GetComponentInChildren<Light>();

        // Pega TODOS os renderizadores (inclui SkinnedMesh dos bones)
        renderers = GetComponentsInChildren<Renderer>();
    }

    void OnMouseDown()
    {
        ToggleVela();
    }

    void ToggleVela()
    {
        apagada = !apagada;

        // Luz
        if (luz != null)
            luz.enabled = !apagada;

        //  Desliga o visual do fogo (mesh/bones)
        foreach (Renderer r in renderers)
        {
            r.enabled = !apagada;
        }

        Debug.Log(gameObject.name + " apagada: " + apagada);
    }

    public bool EstaApagada()
    {
        return apagada;
    }
}