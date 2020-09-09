using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class PersistentSlotData
{

    public static DataManager.SaveSlot slotPosition;
    public static SceneLoader.Scene currentScene;

    public static SceneLoader.Scene lastCheckpointScene;
    
    public static System.DateTime saveTimestamp;

    public static bool gameFinished;

    public static int smallCollectable = 0;
    public static int bigCollectable = 0;

    public static Dictionary<SceneLoader.Scene, Dictionary<BigCollectable, bool>> bigCollectableStatus = new Dictionary<SceneLoader.Scene, Dictionary<BigCollectable, bool>>();


    public static Dictionary<SceneLoader.Scene, int> smallCollectableStatus = new Dictionary<SceneLoader.Scene, int>();

    public static Dictionary<SceneLoader.Scene, int> bigCollectableInserted = new Dictionary<SceneLoader.Scene, int>();
    public static Dictionary<SceneLoader.Scene, int> bigCollectableNeeded = new Dictionary<SceneLoader.Scene, int>();

    /*list of all big collectables in every scene*/
    public enum BigCollectable
    {
        Lobby_GreatBeginnings,
        Lobby_Generous,
        Lobby_Firestarter,
        Lobby_Foodie,
        Space1_SpaceInvader,
        Space1_OuterWorld,
        Snow1_Climber,
        Snow1_Foodie,
        Ninja1_Marksman,
        Ninja1_Assassin,
    }

    /********************** Methods **********************/

    /*initializes number of small collectables for each scene*/
    private static void initBigCollectableNeeded()
    {
        bigCollectableNeeded[SceneLoader.Scene.Level1Scene] = 0;
        bigCollectableNeeded[SceneLoader.Scene.Level2Scene] = 1;
        bigCollectableNeeded[SceneLoader.Scene.Level4Scene] = 3;
        bigCollectableNeeded[SceneLoader.Scene.Level3Scene] = 6;

    }

    /*initializes number of small collectables for each scene*/
    private static void initColectableDictionary(Dictionary<SceneLoader.Scene, int> dictionary)
    {
        foreach (SceneLoader.Scene scene in Enum.GetValues(typeof(SceneLoader.Scene)))
        {
            dictionary.Add(scene, 0);
        }
    }

    /*Initializes collectables in persistent data*/
    public static void initCollectableStatus()
    {

        initBigColStatus();

        initColectableDictionary(smallCollectableStatus);

        initColectableDictionary(bigCollectableInserted);

        initBigCollectableNeeded();
    }


    /*initializes number of big collectables for each scene*/
    private static void initBigColStatus()
    {
        foreach (SceneLoader.Scene scene in Enum.GetValues(typeof(SceneLoader.Scene)))
        {

            Dictionary<BigCollectable, bool> bigCollectables = new Dictionary<BigCollectable, bool>();

            List<BigCollectable> selectedBigCollectables = BigColInScene(scene);
            foreach (BigCollectable collectable in selectedBigCollectables)
            {
                bigCollectables.Add(collectable, false);
            }

            bigCollectableStatus.Add(scene, bigCollectables);
        }
    }

    /*returns list of big collectables that are assigned into specfic scene*/
    public static List<BigCollectable> BigColInScene(SceneLoader.Scene scene)
    {
        List<BigCollectable> selectedBigCollectables = new List<BigCollectable>();

        switch (scene)
        {
            case SceneLoader.Scene.Level1Scene:
            selectedBigCollectables.Add(BigCollectable.Lobby_GreatBeginnings);
            selectedBigCollectables.Add(BigCollectable.Lobby_Generous);
            selectedBigCollectables.Add(BigCollectable.Lobby_Firestarter);
            selectedBigCollectables.Add(BigCollectable.Lobby_Foodie);

            break;

            case SceneLoader.Scene.Level2Scene:
            selectedBigCollectables.Add(BigCollectable.Space1_SpaceInvader);
            selectedBigCollectables.Add(BigCollectable.Space1_OuterWorld);
            break;

            case SceneLoader.Scene.Level3Scene:
            selectedBigCollectables.Add(BigCollectable.Ninja1_Marksman);
            selectedBigCollectables.Add(BigCollectable.Ninja1_Assassin);
            break;
            case SceneLoader.Scene.Level4Scene:
            selectedBigCollectables.Add(BigCollectable.Snow1_Climber);
            selectedBigCollectables.Add(BigCollectable.Snow1_Foodie);
            break;

        }

        return selectedBigCollectables;
    }



}
