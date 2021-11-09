using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get { return instance; } }
    private static SaveManager instance;

    //Fields
    public SaveState saveState;
    private const string saveFileName = "data.ss";
    private BinaryFormatter formatter;

    public Action<SaveState> OnLoad;
    public Action<SaveState> OnSave;

    private void Awake()
    {
        instance = this;

        formatter = new BinaryFormatter();

        //Try and load the previous savestate
        Load();
    }

    public void Load()
    {
        try
        {
            FileStream file = new FileStream(Application.persistentDataPath + saveFileName, FileMode.Open, FileAccess.Read);
            Debug.Log(Application.persistentDataPath + saveFileName);
            saveState = formatter.Deserialize(file) as SaveState;
            file.Close();
            OnLoad?.Invoke(saveState);
        }
        catch
        {
            Debug.Log("Save File not found. Let's create a new one.");
            Save();
        }
    }

    public void Save()
    {
        //If there's no previous state found, create a new one
        if(saveState == null)
        {
            saveState = new SaveState();
        }

        saveState.LastSaveTime = DateTime.Now;

        //Open a file in our system and write to it
        FileStream file = new FileStream(Application.persistentDataPath + saveFileName, FileMode.OpenOrCreate, FileAccess.Write);
        formatter.Serialize(file, saveState);
        file.Close();
        OnSave?.Invoke(saveState);
    }
}
