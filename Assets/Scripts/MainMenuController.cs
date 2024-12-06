using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene loading
using UnityEngine.UI; 
public class MainMenuController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnStartButtonClick()
    {
        // Load the main scene
        SceneManager.LoadScene("MainScene"); // Replace "MainScene" with the name of your actual game scene
    }
}
