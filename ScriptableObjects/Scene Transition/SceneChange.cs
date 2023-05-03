using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DistantLands.Cozy.Data;

[CreateAssetMenu(fileName = "New Scene Transition", menuName = "Scene Transitions/Scene Change")]
public class SceneChange : ScriptableObject
{
    public List<string> scenesToLoad;
    public List<string> scenesToUnload;
    public bool unloadThisScene = false;

    [Header("Active Scene")]
    public string activeSceneName = string.Empty;

    [Header("Enter Audio")]
    public AudioClip audio = null;

    [Header("Spawns")]
    public bool hideAlfonso = false;
    public string playerSpawnLocation = string.Empty;
    public string planeSpawnLocation = string.Empty;
    public bool spawnAlfonsoToPlane = false;

    [Header("Cozy Settings")]
    public AtmosphereProfile atmosphere;
    public WeatherProfile weather;
    /*
    [Header("Modstation")]
    public string playerReturnSpawn = string.Empty;
    public string playerReturnScene = string.Empty;
    public string planeReturnRunwaySpawn = string.Empty;
    public string planeReturnParkSpawn = string.Empty;
    */

    [Header("Enter race")]
    public string raceScene;

}
