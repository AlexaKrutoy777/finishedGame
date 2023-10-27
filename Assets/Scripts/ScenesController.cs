using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using File = System.IO.File;

public class ScenesController : MonoBehaviour
{
    public List<SceneInfo> Scenes { get; set; }

    public int CurrentSceneIndex
    {
        get
        {
            return SceneManager.GetActiveScene().buildIndex;
        }
    }

    public SceneInfo? NextSceneInfo
    {
        get
        {
            return Scenes.SingleOrDefault(x => x.SceneIndex == CurrentSceneIndex + 1);
        }
    }

    public int IngotCount { get; set; }

    private string _path;

    private async void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _path = Application.persistentDataPath + "/save.data"; //   C:/Users/Notebook/AppData/LocalLow/DefaultCompany/AlexaProject/save.data  C:\Users\Алексей\AppData\LocalLow\DefaultCompany\AlexaProject

        if (File.Exists(_path))
        {
            await LoadData();
            return;
        }

        IngotCount = 0;

        Scenes = new List<SceneInfo>
        {
            new SceneInfo
            {
                Name = "1",
                SceneIndex = ScenesIndexes.Level1,
                IsOpen = true,
            },

            new SceneInfo
            {
                Name = "2",
                SceneIndex = ScenesIndexes.Level2,
                IsOpen = false,
                Price = 5,
            },

            new SceneInfo
            {
                Name = "3",
                SceneIndex = ScenesIndexes.Level3,
                IsOpen = false,
                Price = 5,

            },

            new SceneInfo
            {
                Name = "4",
                SceneIndex = ScenesIndexes.Level4,
                IsOpen = false,
                Price = 10,

            },

            new SceneInfo
            {
                Name = "5",
                SceneIndex = ScenesIndexes.Level5,
                IsOpen = false,
                Price = 12,

            },
        };
    }

    public void AddIngot(int count)
    {
        IngotCount += count;
    }

    public async Task SaveData()
    {
        var savedData = new SaveData
        {
            IngotCount = IngotCount,
            ScenesInfo = Scenes,
        };
        
        var serializedValue = JsonConvert.SerializeObject(savedData);

        await File.WriteAllTextAsync(_path, string.Empty);

        FileStream fstream = new FileStream(_path, FileMode.OpenOrCreate);

        byte[] buffer = Encoding.Default.GetBytes(serializedValue);
        await fstream.WriteAsync(buffer, 0, buffer.Length);
    }
    
    public async Task LoadData()
    {
        FileStream fstream = File.OpenRead(_path);
        byte[] buffer = new byte[fstream.Length];
        await fstream.ReadAsync(buffer, 0, buffer.Length);
        string textFromFile = Encoding.Default.GetString(buffer);

        var deserializedData = JsonConvert.DeserializeObject<SaveData>(textFromFile);

        IngotCount = deserializedData.IngotCount;
        Scenes = deserializedData.ScenesInfo;
    }

    public void ReastartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void NextLevel()
    {
        try
        {
            SceneManager.LoadScene(NextSceneInfo.SceneIndex);
        }
        catch
        {
            Debug.Log("Попытка перейти на не существующий уровень");
        }
    }

    public void GoToLevel(int level)
    {
        SceneManager.LoadScene(level);
        Debug.Log("sasa");

    }

    public void OpenScene(int sceneIndex)
    {
        var sceneInfo = Scenes.Select(x => x)
                              .FirstOrDefault(x => x.SceneIndex == sceneIndex);
        sceneInfo.IsOpen = true;
    }
}

public static class ScenesIndexes
{
    public static readonly int Menu = 0;
    public static readonly int LevelsMenu = 1;
    public static readonly int Level1 = 2;
    public static readonly int Level2 = 3;
    public static readonly int Level3 = 4;
    public static readonly int Level4 = 5;
    public static readonly int Level5 = 6;
}

