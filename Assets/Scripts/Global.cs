using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Global : MonoBehaviour
{
    public static Global global;

    public static int[] record = { 0, 0 };  //record for biggest size
    public static int language = 1; //1 - English, 2 - Russian

    public static int[] size = { 6, 6 }; //height and width of game

    public static Card activeCard;  //first flipped card to compare with
    public static bool can_flip = false;  //to avoid extra flips
    public static bool is_game = true;  //to check if in menu or in game

    void Awake()
    {
        global = this;
        is_game = SceneManager.GetActiveScene().name == "Game" ? true : false;
        if (!is_game)
        {
            Camera.main.GetComponent<Animator>().Play("Menu");
        }
    }

    //Saving function
    //to call Global.global.Save();
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/info.dat");

        PlayerData data = new PlayerData();
        data.record = record;
        data.language = language;

        bf.Serialize(file, data);
        file.Close();
    }

    //Loading function
    //to call Global.global.Load();
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/info.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/info.dat", FileMode.Open);

            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            record = data.record;
            language = data.language;
        }
    }
}

//class of Player's Data
//easy to store on hard drive and to transform into json
[Serializable]
class PlayerData
{
    public int[] record;
    public int language;
}
