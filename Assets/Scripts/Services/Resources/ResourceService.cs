using PEMath;
using PEPhysx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResourceService : MonoBehaviour
{
    public static ResourceService instance;
    public void InitializeService()
    {
        instance = this;
        this.Log("Initialize Resource Service Completed");
    }

    private Action progressCallback = null;
    public void AysncLoadScene(string sceneName, Action<float> loadRate, Action loaded)
    {
        AsyncOperation sceneAsync = SceneManager.LoadSceneAsync(sceneName);

        progressCallback = () =>
         {
             float progress = sceneAsync.progress;
             loadRate?.Invoke(progress);

             if (progress == 1)
             {
                 loaded?.Invoke();
                 progressCallback = null;
                 sceneAsync = null;
             }
         };
    }
    private void Update()
    {
        progressCallback?.Invoke();
    }
    private Dictionary<string, AudioClip> audioDictionary = new Dictionary<string, AudioClip>();
    public AudioClip LoadAudio(string path, bool cache = false)
    {
        AudioClip audioClip = null;
        if (!audioDictionary.TryGetValue(path, out audioClip))
        {

            audioClip = Resources.Load<AudioClip>(path);
            if (cache)
            {
                audioDictionary.Add(path, audioClip);
            }
        }
        return audioClip;
    }

    private Dictionary<string, Sprite> spriteDictionary = new Dictionary<string, Sprite>();
    public Sprite LoadSprite(string path, bool cache = false)
    {
        Sprite sprite = null;
        if (!spriteDictionary.TryGetValue(path, out sprite))
        {

            sprite = Resources.Load<Sprite>(path);
            if (cache)
            {
                spriteDictionary.Add(path, sprite);
            }
        }
        return sprite;
    }

    //public UnitConfig GetUnitConfigByID(int unitID)
    //{
    //    switch (unitID)
    //    {
    //        case 101:
    //            return new UnitConfig
    //            {
    //                unitID = 101,
    //                unitName = "ys",
    //                resourceName = "arthur",

    //                hp = 6500,
    //                def = 0,
    //                moveSpeed = 5,
    //                colConfig = new ColliderConfig
    //                {
    //                    mType = ColliderType.Cylinder,
    //                    mRadius = (PEInt)0.5f
    //                }
    //            };
    //        case 102:
    //            return new UnitConfig
    //            {
    //                unitID = 101,
    //                unitName = "hy",
    //                resourceName = "houyi",

    //                hp = 3500,
    //                def = 10,
    //                moveSpeed = 5,
    //                colConfig = new ColliderConfig
    //                {
    //                    mType = ColliderType.Cylinder,
    //                    mRadius = (PEInt)0.5f
    //                }
    //            };
    //        default:
    //            return null;
    //    }
    //}

    //public MapConfig GetMapConfigByID(int mapID)
    //{
    //    switch (mapID)
    //    {
    //        case 101:
    //            return new MapConfig
    //            {
    //                mapID = 101,
    //                spawnDelay = 2000,
    //                spawnInterval = 15000,
    //                waveInterval = 5000,
    //            };
    //        case 102:
    //            return new MapConfig
    //            {
    //                mapID = 102,
    //                spawnDelay = 2000,
    //                spawnInterval = 15000,
    //                waveInterval = 5000,
    //            };
    //        default:
    //            return null;

    //    }

    //}
}

