using UnityEngine;

public class RailCart : MonoBehaviour
{
    public Transform[] points;
    public float speed = 5f;

    private int currentPoint = 0;
    private bool moving = false;
    private bool goingForward = true;

    void Update()
    {
        if (!moving) return;

        Transform target = points[currentPoint];

        // move
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        // chegou no ponto
        if (Vector3.Distance(transform.position, target.position) < 0.01f)
        {
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
        // se já estiver andando ignora
        if (moving) return;

        // se estiver no começo ? vai pra frente
        if (currentPoint == 0)
        {
            goingForward = true;

            if (points.Length > 1)
                currentPoint = 1;
        }
        else
        {
            // se estiver no final ? volta
            goingForward = false;
            currentPoint = points.Length - 2;
        }

        moving = true;
    }
}