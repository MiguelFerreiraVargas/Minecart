
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditorInternal.ReorderableList;

[RequireComponent(typeof(Camera))]
public class PlayerPickup : MonoBehaviour
{
    [Header("Pickup")]
    [SerializeField] private float pickupDistance = 4f;
    [SerializeField] private LayerMask pickupLayer;

    [Header("Hold")]
    [SerializeField] private float holdDistance = 2f;
    [SerializeField] private float moveForce = 25f;
    [SerializeField] private float maxVelocity = 15f;
    [SerializeField] private float rotationForce = 12f;



    [Header("Line Effect")]
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private int linePoints = 20;
    [SerializeField] private float waveAmplitude = 0.08f;
    [SerializeField] private float waveSpeed = 6f;
    [SerializeField] private float waveFrequency = 2f;

    private Rigidbody heldObject;
    private Camera cam;

    private Transform holdPoint;

    private Vector3 targetRotation;

    private void Awake()
    {
        cam = Camera.main;

        holdPoint = new GameObject("Hold Point").transform;
        holdPoint.SetParent(transform);
        holdPoint.localPosition = new Vector3(0f, 0f, holdDistance);

        if (lineRenderer != null)
        {
            lineRenderer.positionCount = linePoints;
            lineRenderer.enabled = false;
        }
    }

    private void Update()
    {
        HandleInput();
        HandleScroll();
        UpdateLine();
        ObjectSway();
    }

    private void FixedUpdate()
    {
        if (heldObject != null)
        {
            MoveObject();
            RotateObject();
        }
    }

    private void HandleInput()
    {
        // Pickup com botão direito
        if (Input.GetMouseButtonDown(1))
        {
            if (heldObject == null)
            {
                TryPickup();
            }
            else
            {
                Drop();
            }
        }

        // Sem arremesso
        // Botão esquerdo livre pra outras mecânicas

        // Rotacionar segurando R
        if (heldObject != null && Input.GetKey(KeyCode.R))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            targetRotation += new Vector3(mouseY, -mouseX, 0f) * rotationForce;
        }
    }

    private void TryPickup()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, pickupDistance, pickupLayer))
        {
            Rigidbody rb = hit.collider.attachedRigidbody;

            if (rb == null)
                return;

            heldObject = rb;

            heldObject.useGravity = false;
            heldObject.linearDamping = 8f;
            heldObject.angularDamping = 8f;

            heldObject.interpolation = RigidbodyInterpolation.Interpolate;

            targetRotation = heldObject.rotation.eulerAngles;

            if (lineRenderer != null)
                lineRenderer.enabled = true;
        }
    }

    private void MoveObject()
    {
        Vector3 targetPosition = holdPoint.position;
        Vector3 direction = targetPosition - heldObject.position;

        heldObject.AddForce(direction * moveForce, ForceMode.Acceleration);

        if (heldObject.linearVelocity.magnitude > maxVelocity)
        {
            heldObject.linearVelocity = heldObject.linearVelocity.normalized * maxVelocity;
        }
    }

    private void RotateObject()
    {
        Quaternion targetRot = Quaternion.Euler(targetRotation);

        Quaternion delta = targetRot * Quaternion.Inverse(heldObject.rotation);

        delta.ToAngleAxis(out float angle, out Vector3 axis);

        if (angle > 180f)
            angle -= 360f;

        heldObject.angularVelocity = axis * angle * Mathf.Deg2Rad * rotationForce;
    }

    private void HandleScroll()
    {
        if (heldObject == null)
            return;

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (Mathf.Abs(scroll) > 0.01f)
        {
            holdDistance += scroll * 2f;
            holdDistance = Mathf.Clamp(holdDistance, 1f, 5f);

            holdPoint.localPosition = new Vector3(0f, 0f, holdDistance);
        }
    }

    private void ObjectSway()
    {
        if (heldObject == null)
            return;

        Vector3 sway = new Vector3(
            Mathf.Sin(Time.time * 4f) * 0.03f,
            Mathf.Cos(Time.time * 3f) * 0.02f,
            0f
        );

        holdPoint.position += cam.transform.TransformDirection(sway);
    }

    private void Drop()
    {
        if (heldObject == null)
            return;

        heldObject.useGravity = true;

        heldObject.linearDamping = 0f;
        heldObject.angularDamping = 0.05f;

        heldObject = null;

        if (lineRenderer != null)
            lineRenderer.enabled = false;
    }

    private void UpdateLine()
    {
        if (heldObject == null || lineRenderer == null)
            return;

        Vector3 start = cam.transform.position + cam.transform.forward * 0.15f + cam.transform.right * 0.08f;
        Vector3 end = heldObject.worldCenterOfMass;

        Vector3 direction = (end - start).normalized;
        Vector3 side = Vector3.Cross(direction, cam.transform.up).normalized;

        float distance = Vector3.Distance(start, end);

        for (int i = 0; i < linePoints; i++)
        {
            float t = i / (float)(linePoints - 1);

            Vector3 point = Vector3.Lerp(start, end, t);

            // Curvatura principal
            float curve = Mathf.Sin(t * Mathf.PI) * 0.15f;
            point -= cam.transform.up * curve;

            // Ondulação viva estilo REPO
            float wave = Mathf.Sin((t * 8f) - (Time.time * waveSpeed)) * waveAmplitude;
            point += side * wave;

            // Tremidinha pequena
            point += Random.insideUnitSphere * 0.003f;

            lineRenderer.SetPosition(i, point);
        }
    }
}