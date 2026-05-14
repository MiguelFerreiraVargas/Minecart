using UnityEngine;

public class RailCart : MonoBehaviour
{
    public Transform[] points;
    public float speed = 5f;

    private int currentPoint = 0;
    private bool moving = false;

    void Update()
    {
        if (!moving) return;

        if (currentPoint >= points.Length) return;

        Transform target = points[currentPoint];

        // move
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        // distância até o ponto
        float distance = Vector3.Distance(
            transform.position,
            target.position
        );

        // chegou
        if (distance <= 0.01f)
        {
            // trava EXATAMENTE no ponto
            transform.position = target.position;

            currentPoint++;

            // terminou
            if (currentPoint >= points.Length)
            {
                moving = false;
            }
        }
    }

    void OnMouseDown()
    {
        moving = true;
    }
}