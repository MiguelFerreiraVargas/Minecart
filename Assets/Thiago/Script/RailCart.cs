using UnityEngine;

public class RailCart : MonoBehaviour
{
    [Header("Pontos do Trilho")]
    public Transform[] points;

    [Header("Velocidade")]
    public float speed = 5f;

    [Header("Velocidade da Rotação")]
    public float rotationSpeed = 5f;

    private int currentPoint = 0;
    private bool moving = false;
    private bool goingForward = true;

    void Update()
    {
        if (!moving) return;

        Transform target = points[currentPoint];

        // MOVE
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        // GIRA baseado na rotação do Point
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            target.rotation,
            rotationSpeed * Time.deltaTime
        );

        // CHEGOU NO POINT
        if (Vector3.Distance(transform.position, target.position) < 0.01f)
        {
            // trava exatamente no point
            transform.position = target.position;

            // indo pra frente
            if (goingForward)
            {
                currentPoint++;

                // chegou no final
                if (currentPoint >= points.Length)
                {
                    currentPoint = points.Length - 1;
                    moving = false;
                }
            }
            // voltando
            else
            {
                currentPoint--;

                // chegou no começo
                if (currentPoint < 0)
                {
                    currentPoint = 0;
                    moving = false;
                }
            }
        }
    }

    void OnMouseDown()
    {
        // se já estiver andando
        if (moving) return;

        // se estiver no começo ? vai
        if (currentPoint == 0)
        {
            goingForward = true;

            if (points.Length > 1)
            {
                currentPoint = 1;
            }
        }
        // se estiver no final ? volta
        else
        {
            goingForward = false;
            currentPoint = points.Length - 2;
        }

        moving = true;
    }
}