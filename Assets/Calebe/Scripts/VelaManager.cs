using UnityEngine;

public class VelaManager : MonoBehaviour
{
    public VelaParte[] velas;
    public GameObject objetoParaAtivar; // botão, porta, etc

    private bool jaAtivado = false;

    public void VerificarVelas()
    {
        if (jaAtivado) return;

        foreach (VelaParte vela in velas)
        {
            if (!vela.EstaApagada())
                return;
        }

        AtivarEvento();
    }

    void AtivarEvento()
    {
        jaAtivado = true;

        Debug.Log(" TODAS AS VELAS APAGADAS!");

        if (objetoParaAtivar != null)
            objetoParaAtivar.SetActive(true);
    }
}