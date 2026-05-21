using UnityEngine;

public class ShopNPC : MonoBehaviour
{
    public GameObject shopUI;

    private bool playerNear;

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            bool isOpen = !shopUI.activeSelf;

            shopUI.SetActive(isOpen);

            Cursor.visible = isOpen;

            Cursor.lockState = isOpen
                ? CursorLockMode.None
                : CursorLockMode.Locked;
        }

        if (shopUI.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            shopUI.SetActive(false);

            Cursor.visible = false;

            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;

            shopUI.SetActive(false);

            Cursor.visible = false;

            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}