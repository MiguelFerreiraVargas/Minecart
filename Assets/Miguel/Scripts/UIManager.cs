using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject shopUI;
    public GameObject interactUI; // texto "Pressione TAB"

    private bool isOpen = false;

    void Update()
    {
        if (isOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseUI();
        }
    }

    public void OpenUI()
    {
        shopUI.SetActive(true);
        interactUI.SetActive(false); // 👈 some o texto

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;

        isOpen = true;
    }

    public void CloseUI()
    {
        shopUI.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1f;

        isOpen = false;
    }

    public bool IsOpen()
    {
        return isOpen;
    }
}