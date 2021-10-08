using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject creditsTab;
    public GameObject mainMenuTab;
    public GameObject optionsTab;
    
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Credits()
    {
        creditsTab.SetActive(!creditsTab.activeSelf);
        mainMenuTab.SetActive(!mainMenuTab.activeSelf);
    }
    
    public void Options()
    {
        optionsTab.SetActive(!optionsTab.activeSelf);
        mainMenuTab.SetActive(!mainMenuTab.activeSelf);
    }
    
}

