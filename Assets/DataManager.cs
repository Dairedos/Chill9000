using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class DataManager : MonoBehaviour
{
    public enum SaveSlot
    {
        First,
        Second,
        Third,
        None,

    }



    public static DataManager manager;
    void Awake()
    {
        if (manager == null)
        {
            this.LoadGeneralData();
            PersistentSlotData.initCollectableStatus();
            manager = this;
        }
    }
    
    public void SaveSlotData(SaveSlot slotPosition)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/PersistentData" + slotPosition.ToString() + ".dat");
        PersistentSlotDataSerializer dataSerializer = new PersistentSlotDataSerializer();


        /*[ENTER] Persistent data*/

        dataSerializer.slotPosition = PersistentSlotData.slotPosition;
        dataSerializer.currentScene = PersistentSlotData.currentScene;
        dataSerializer.lastCheckpointScene = PersistentSlotData.lastCheckpointScene;

        dataSerializer.saveTimestamp = PersistentSlotData.saveTimestamp;

        dataSerializer.gameFinished = PersistentSlotData.gameFinished;

        dataSerializer.smallCollectableInventory = PersistentSlotData.smallCollectable;
        dataSerializer.bigCollectableInventory = PersistentSlotData.bigCollectable;

        dataSerializer.bigCollectableInserted = PersistentSlotData.bigCollectableInserted;

        dataSerializer.bigCollectableStatus = PersistentSlotData.bigCollectableStatus;
        dataSerializer.smallCollectableStatus = PersistentSlotData.smallCollectableStatus;

        /*[EXIT] Persistent data*/

        bf.Serialize(file, dataSerializer);
        file.Close();
    }

    public string LoadSlotInfoData(SaveSlot slot)
    {
        String slotInfoData;

            if (File.Exists(Application.persistentDataPath + "/PersistentData" + slot.ToString() + ".dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/PersistentData" + slot.ToString() + ".dat", FileMode.Open);
                PersistentSlotDataSerializer dataSerializer = (PersistentSlotDataSerializer)bf.Deserialize(file);

                if(dataSerializer.gameFinished)
                    slotInfoData = ("Level: " + dataSerializer.lastCheckpointScene + "\nDate: " + dataSerializer.saveTimestamp.ToString()+"\n(Complete)");
                else
                    slotInfoData = ("Level: " + dataSerializer.lastCheckpointScene + "\nDate: " + dataSerializer.saveTimestamp.ToString());
        }
            else
            {
                slotInfoData = ("Empty");
            }

        return slotInfoData;
    }

    public void LoadSlotData(SaveSlot slotPosition)
    {
        if (File.Exists(Application.persistentDataPath + "/PersistentData" + slotPosition.ToString() + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/PersistentData" + slotPosition.ToString() + ".dat", FileMode.Open);
            PersistentSlotDataSerializer dataSerializer = (PersistentSlotDataSerializer)bf.Deserialize(file);

            /*[ENTER] Persistent data*/

            PersistentSlotData.slotPosition = dataSerializer.slotPosition;
            PersistentSlotData.currentScene = dataSerializer.currentScene;
            PersistentSlotData.lastCheckpointScene = dataSerializer.lastCheckpointScene;

            PersistentSlotData.saveTimestamp = dataSerializer.saveTimestamp;

            PersistentSlotData.gameFinished = dataSerializer.gameFinished;

            PersistentSlotData.smallCollectable = dataSerializer.smallCollectableInventory;
            PersistentSlotData.bigCollectable = dataSerializer.bigCollectableInventory;

            PersistentSlotData.bigCollectableInserted = dataSerializer.bigCollectableInserted;

            PersistentSlotData.bigCollectableStatus = dataSerializer.bigCollectableStatus;
            PersistentSlotData.smallCollectableStatus = dataSerializer.smallCollectableStatus;


            /*[EXIT] Persistent data*/
        }
        else
        {
            PersistentSlotData.lastCheckpointScene = SceneLoader.Scene.Level1Scene;
            PersistentSlotData.saveTimestamp = System.DateTime.UtcNow;
            PersistentSlotData.slotPosition = slotPosition;
            SaveSlotData(slotPosition);
        }

    }

    [Serializable]
    class PersistentSlotDataSerializer
    {
        public DataManager.SaveSlot slotPosition;
        public SceneLoader.Scene currentScene;
        public SceneLoader.Scene lastCheckpointScene;

        public bool gameFinished;

        public int smallCollectableInventory;
        public int bigCollectableInventory;
        

        public System.DateTime saveTimestamp;

        public int NinjaLevel1_BigColInserted;
        public int SpaceLevel1_BigColInserted;
        public int SnowLevel1_BigColInserted;


        public Dictionary<SceneLoader.Scene, Dictionary<PersistentSlotData.BigCollectable, bool>> bigCollectableStatus = new Dictionary<SceneLoader.Scene, Dictionary<PersistentSlotData.BigCollectable, bool>>();
        public Dictionary<SceneLoader.Scene, int> smallCollectableStatus = new Dictionary<SceneLoader.Scene, int>();
        public Dictionary<SceneLoader.Scene, int> bigCollectableInserted = new Dictionary<SceneLoader.Scene, int>();
    }

    public void DeleteSavedData(SaveSlot slot)
    {
        if (File.Exists(Application.persistentDataPath + "/PersistentData" + slot.ToString() + ".dat"))
        {
            File.Delete(Application.persistentDataPath + "/PersistentData" + slot.ToString() + ".dat");
        }
    }


    public void SaveGeneralData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/PersistentDataGeneral.dat");
        PersistentGeneralDataSerializer dataSerializer = new PersistentGeneralDataSerializer();

        /*[ENTER] Persistent data*/

        dataSerializer.smallCollectables = PersistentGeneralData.smallCollectables;

        dataSerializer.collectedAllBigCollectables = PersistentGeneralData.collectedAllBigCollectables;
        dataSerializer.collectedAllSmallCollectables = PersistentGeneralData.collectedAllSmallCollectables;

        dataSerializer.gameFinished = PersistentGeneralData.gameFinished;

        /*[EXIT] Persistent data*/

        bf.Serialize(file, dataSerializer);
        file.Close();
        Debug.Log("general data saved!"); 
    }

    public void LoadGeneralData()
    {
        if (File.Exists(Application.persistentDataPath + "/PersistentDataGeneral.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/PersistentDataGeneral.dat", FileMode.Open);
            PersistentGeneralDataSerializer dataSerializer = (PersistentGeneralDataSerializer)bf.Deserialize(file);

            /*[ENTER] Persistent data*/

           PersistentGeneralData.smallCollectables = dataSerializer.smallCollectables;

           PersistentGeneralData.collectedAllBigCollectables = dataSerializer.collectedAllBigCollectables;
           PersistentGeneralData.collectedAllSmallCollectables = dataSerializer.collectedAllSmallCollectables;

           PersistentGeneralData.gameFinished = dataSerializer.gameFinished;


            /*[EXIT] Persistent data*/
            Debug.Log("general data loaded!");
        }
        else
        {
            PersistentGeneralData.smallCollectables = 0;

            PersistentGeneralData.collectedAllBigCollectables = false;
            PersistentGeneralData.collectedAllSmallCollectables = false;

            PersistentGeneralData.gameFinished = false;

            SaveGeneralData();
        }

    }

    [Serializable]
    class PersistentGeneralDataSerializer
    {
        public int smallCollectables;

        public bool collectedAllSmallCollectables;
        public bool collectedAllBigCollectables;

        public bool gameFinished;
        
    }
}
