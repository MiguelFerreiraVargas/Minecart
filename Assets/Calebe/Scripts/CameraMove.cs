using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform alvo;
    public float velocidade = 2f;

    void Start()
    {
        alvo = null; //  garante que não começa movendo
    }

    void Update()
    {
        if (alvo != null)
        {
            transform.position = Vector3.Lerp(transform.position, alvo.position, Time.deltaTime * velocidade);
            transform.rotation = Quaternion.Lerp(transform.rotation, alvo.rotation, Time.deltaTime * velocidade);
        }
    }

    public void MoverPara(Transform novoAlvo)
    {
        alvo = novoAlvo;
    }
}