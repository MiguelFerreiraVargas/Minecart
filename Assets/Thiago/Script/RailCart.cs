using UnityEngine;

public class RailCart : MonoBehaviour
{
    [Header("Pontos do Trilho")]
    public Transform[] points;

    [Header("Velocidade")]
    public float speed = 5f;

    [Header("Rotação")]
    public float rotationSpeed = 5f;

    [Header("Interação")]
    public float interactDistance = 5f;

    [Header("Camera do Player")]
    public Camera playerCamera;

    private int currentPoint = 0;
    private bool moving = false;
    private bool goingForward = true;

    void Update()
    {
        // INTERAÇÃO
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }

        // MOVIMENTO
        if (moving)
        {
            MoveCart();
        }
    }

    void TryInteract()
    {
        Ray ray = playerCamera.ScreenPointToRay(
            new Vector3(Screen.width / 2, Screen.height / 2)
        );

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            // verifica se olhou pra ESSE carrinho
            if (hit.collider.gameObject == gameObject)
            {
                StartCart();
            }
        }
    }

    void StartCart()
    {
        if (moving) return;

        // indo
        if (currentPoint == 0)
        {
            goingForward = true;

            if (points.Length > 1)
                currentPoint = 1;
        }
        // voltando
        else
        {
            goingForward = false;
            currentPoint = points.Length - 2;
        }

        moving = true;
    }

    void MoveCart()
    {
        Transform target = points[currentPoint];

        // MOVE
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        // ROTAÇÃO
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            target.rotation,
            rotationSpeed * Time.deltaTime
        );

        // CHEGOU
        if (Vector3.Distance(transform.position, target.position) < 0.01f)
        {
            transform.position = target.position;

            if (goingForward)
            {
                currentPoint++;

                if (currentPoint >= points.Length)
                {
                    currentPoint = points.Length - 1;
                    moving = false;
                }
            }
            else
            {
                currentPoint--;

                if (currentPoint < 0)
                {
                    currentPoint = 0;
                    moving = false;
                }
            }
        }
    }
}