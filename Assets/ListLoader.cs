using System.Collections.Generic;
using UnityEngine;

public class ListLoader : MonoBehaviour
{


    [SerializeField]
    protected List<GameObject> ObjectList = new List<GameObject>();


    /*Return restricted area box list*/
    protected void ReloadList<T>(ref List<GameObject> GameObjectList, T[] GameObjectArray) where T : MonoBehaviour
    {
        GameObjectList.Clear();

        for (int i = 0; i < GameObjectArray.Length; i++)
        {
            GameObjectList.Add(GameObjectArray[i].gameObject);
        }
    }

    /*Returns provate GameObject List*/
    public List<GameObject> GetObjectList()
    {
        return ObjectList;
    }

}
