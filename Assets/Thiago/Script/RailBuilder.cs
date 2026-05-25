using TMPro;
using UnityEngine;

public class RailBuilder : MonoBehaviour
{
    public static RailBuilder Instance;

    [Header("Prefabs")]
    public GameObject railPrefab;
    public GameObject previewPrefab;

    [Header("Config")]
    public LayerMask groundLayer;
    public float gridSize = 1f;

    [Header("Preço")]
    public int railPrice = 50;

    [Header("UI")]
    public TMP_Text railText;

    private GameObject currentPreview;

    private bool buildMode;

    private int railsOwned;

    private float currentRotation;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateRailUI();
    }

    void Update()
    {
        HandleBuildToggle();

        if (!buildMode)
            return;

        HandleRotation();
        HandlePreview();
        HandlePlacement();
    }

    void HandleBuildToggle()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (buildMode)
            {
                ExitBuildMode();
            }
            else
            {
                EnterBuildMode();
            }
        }
    }

    void HandleRotation()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentRotation += 90f;

            currentRotation %= 360f;
        }
    }

    public void BuyRail()
    {
        if (!MoneyManager.Instance.SpendMoney(railPrice))
        {
            Debug.Log("Dinheiro insuficiente");
            return;
        }

        railsOwned++;

        UpdateRailUI();

        Debug.Log("Comprou trilho");
    }

    void EnterBuildMode()
    {
        if (railsOwned <= 0)
        {
            Debug.Log("Sem trilhos");
            return;
        }

        buildMode = true;

        currentPreview = Instantiate(previewPrefab);
    }

    void HandlePreview()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
        {
            Vector3 snappedPosition = SnapPosition(hit.point);

            currentPreview.transform.position = snappedPosition;

            currentPreview.transform.rotation =
                Quaternion.Euler(0f, currentRotation, 0f);
        }
    }

    void HandlePlacement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(
                railPrefab,
                currentPreview.transform.position,
                Quaternion.Euler(0f, currentRotation, 0f)
            );

            railsOwned--;

            UpdateRailUI();

            if (railsOwned <= 0)
            {
                ExitBuildMode();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            ExitBuildMode();
        }
    }

    void ExitBuildMode()
    {
        buildMode = false;

        if (currentPreview != null)
        {
            Destroy(currentPreview);
        }
    }

    void UpdateRailUI()
    {
        railText.text = "Trilhos: " + railsOwned;
    }

    Vector3 SnapPosition(Vector3 position)
    {
        float x = Mathf.Round(position.x / gridSize) * gridSize;
        float y = Mathf.Round(position.y / gridSize) * gridSize;
        float z = Mathf.Round(position.z / gridSize) * gridSize;

        return new Vector3(x, y, z);
    }
}