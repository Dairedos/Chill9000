using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    [SerializeField]
    private GameObject player;

    public static SceneLoader Loader;

    private bool GameStart = false;

    public enum Scene
    {
        NeverUnload = 0,
        MainMenuScene = 1,
        Level1Scene = 2,
        Level2Scene = 3,
        Level3Scene = 4,
        Level4Scene = 5,

    }

    void Awake()
    {
        if (!GameStart)
        {

            Loader = this;

            PersistentSlotData.currentScene = Scene.MainMenuScene;
            SceneManager.LoadSceneAsync(Scene.MainMenuScene.ToString(), LoadSceneMode.Additive);

            GameStart = true;

            Debug.Log("First time loading: "+ Scene.MainMenuScene.ToString());
        }
    }


    public void LoadScene(Scene scene)
    {

        StartCoroutine(SetActiveScene(scene, SceneManager.LoadSceneAsync(scene.ToString(), LoadSceneMode.Additive)));
        
        PersistentSlotData.currentScene = scene;

    }

    public void UnloadScene(Scene scene)
    {
        StartCoroutine(Unload(scene.ToString()));
    }

    IEnumerator Unload(string SceneName)
    {

        yield return null;

        SceneManager.UnloadScene(SceneName);
    }

    private IEnumerator SetActiveScene(Scene scene, AsyncOperation operation)
    {

        yield return new WaitUntil(() => operation.isDone);
        
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene.ToString()));
        Debug.Log("[INFO] SetActiveScene : " + scene.ToString());
    }

    public void SwapScenes(Scene previousScene, Scene nextScene, float delay)
    {
        StartCoroutine(Swap(previousScene, nextScene, delay));
    }

    private IEnumerator Swap(Scene previousScene, Scene nextScene, float delay)
    {
        yield return new WaitForSeconds(delay);
        UnloadScene(previousScene);
        LoadScene(nextScene);

    }
}
