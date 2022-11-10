using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance { get; private set; }

    //Cosas a guardar

    private void Awake()
    {
        if(instance != null && instance != this)
            Destroy(gameObject);
        else
        instance = this;
    }

    public void Load()
    {

    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData_Storage dataa = new PlayerData_Storage();
    }
}

[Serializable]
class PlayerData_Storage
{

}
