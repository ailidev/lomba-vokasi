using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Security.Cryptography;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance { get; set; }
    GameData m_GameData;
    BinaryFormatter m_Formatter = new BinaryFormatter();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public static string Encode(string strword) {
        MD5 md5 = new MD5CryptoServiceProvider();
        md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(strword));
        byte[] hash = md5.Hash;
        StringBuilder strBuilder = new StringBuilder();

        for (int i = 0; i < hash.Length; i++) {
            strBuilder.Append(hash[i].ToString("x2"));
        }

        return strBuilder.ToString();
    }

    public void Save() {
        string fileName = Encode("savedata.aili");
        string path = Application.persistentDataPath + Path.DirectorySeparatorChar + "." + fileName;

        FileStream stream = new FileStream(path, FileMode.Create);
        m_Formatter.Serialize(stream, m_GameData);
        stream.Close();
    }

    public void Load() {
        string fileName = Encode("savedata.aili");
        string path = Application.persistentDataPath + Path.DirectorySeparatorChar + "." + fileName;

        if (File.Exists(path)) {
            FileStream stream = new FileStream(path, FileMode.Open);
            m_GameData = m_Formatter.Deserialize(stream) as GameData;
            stream.Close();
        }
    }

    public void Delete() {
        string fileName = Encode("savedata.aili");
        string path = Application.persistentDataPath + Path.DirectorySeparatorChar + "." + fileName;

        if (File.Exists(path)) {
            File.Delete(path);
        }
    }
}
