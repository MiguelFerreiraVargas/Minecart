using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float distance = 3f;
    public LayerMask interactLayer;
    public GameObject interactUI;
    public UIManager uiManager;

    private GameObject currentNPC;

    void Update()
    {
        if (uiManager.IsOpen())
        {
            interactUI.SetActive(false);
            return;
        }

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distance, interactLayer))
        {
            if (hit.collider.CompareTag("NPC"))
            {
                interactUI.SetActive(true);
                currentNPC = hit.collider.gameObject;

                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    uiManager.OpenUI();
                }

                return;
            }
        }

        interactUI.SetActive(false);
        currentNPC = null;
    }
}