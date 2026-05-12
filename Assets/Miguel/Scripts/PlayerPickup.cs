using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    [Header("Pickup Settings")]
    public float pickupDistance = 3f;
    public LayerMask pickupLayer;

    [Header("Hold Settings")]
    public float holdDistance = 2f;
    public float moveForce = 150f;
    public float damping = 10f;

    private Rigidbody heldObject;
    private Transform holdPoint;

    void Start()
    {
        holdPoint = new GameObject("HoldPoint").transform;
        holdPoint.SetParent(transform);
        holdPoint.localPosition = new Vector3(0, 0, holdDistance);
    }

    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        if (heldObject != null)
        {
            MoveObject();
        }
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryPickup();
        }

        if (Input.GetMouseButtonUp(0))
        {
            Drop();
        }

        if (heldObject != null)
        {
            HandleScroll();
            HandleThrow();
        }
    }

    void TryPickup()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupDistance, pickupLayer))
        {
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

            if (rb != null)
            {
                heldObject = rb;

                heldObject.useGravity = false;
                heldObject.linearDamping = damping;
                heldObject.angularDamping = damping;

                heldObject.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }
    }

    void MoveObject()
    {
        Vector3 targetPosition = holdPoint.position;
        Vector3 direction = targetPosition - heldObject.position;

        heldObject.linearVelocity = direction * moveForce * Time.fixedDeltaTime;
    }

    void Drop()
    {
        if (heldObject == null) return;

        heldObject.useGravity = true;
        heldObject.linearDamping = 0f;
        heldObject.angularDamping = 0.05f;

        heldObject.constraints = RigidbodyConstraints.None;

        heldObject = null;
    }

    void HandleScroll()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0f)
        {
            holdDistance += scroll * 2f;
            holdDistance = Mathf.Clamp(holdDistance, 1f, 4f);

            holdPoint.localPosition = new Vector3(0, 0, holdDistance);
        }
    }

    void HandleThrow()
    {
        if (Input.GetMouseButtonDown(1))
        {
            heldObject.AddForce(transform.forward * 500f);
            Drop();
        }
    }
}
