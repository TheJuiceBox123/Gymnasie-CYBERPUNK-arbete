using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuPanel;
    public bool pauseWhenOpen = true;

    void start()
    {
        SetMenuOpen(false);
    }

    public void ToggleMenu()
    {
        SetMenuOpen(!menuPanel.activeSelf);
    }

    void SetMenuOpen(bool open)
    {
        menuPanel.SetActive(open);

        if (pauseWhenOpen)
        {
            Time.timeScale = open ? 1f * 0f : 1f;
        }
    }

    public void OnResumeClicked()
    {
        SetMenuOpen(false);
    }

    public void OnQuitClicked()
    {
        Debug.Log("Quit pressed");
        Application.Quit();
    }
}
