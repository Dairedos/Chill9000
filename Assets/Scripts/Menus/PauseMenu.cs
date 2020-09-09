using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool gameIsPaused = false;

    public GameObject pauseMenuUi;

    private float LastStateOfPauseInput;
    private float pauseInput;

    // Update is called once per frame
    void Update() {

        if (!PersistentSlotData.currentScene.Equals(SceneLoader.Scene.MainMenuScene)) { 

        if (jumpInputRiseEdge(pauseInput)) {
            if (gameIsPaused)
            {
                 ResumeGame();
            }
            else PauseGame();
        }

        LastStateOfPauseInput = pauseInput;
            }
    }

    public void GetPauseEvent(InputAction.CallbackContext context)
    {
        pauseInput = context.ReadValue<float>();
    }

    private bool jumpInputRiseEdge(float jumpInput)
    {

        if (pauseInput.Equals(1f) && (!pauseInput.Equals(LastStateOfPauseInput)))
            return true;

        return false;
    }

    public void ResumeGame(){
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
    public void PauseGame(){
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void ToMainMenu() {
        Time.timeScale = 1f;
        gameIsPaused = false;
        pauseMenuUi.SetActive(false);
        Debug.Log("Going to Menu");
        SceneLoader.Loader.SwapScenes(PersistentSlotData.currentScene, SceneLoader.Scene.MainMenuScene, 0.2f);
    }

    public void ExitGame() {
        Debug.Log("Exiting Game!");
        Application.Quit();
    }
}
