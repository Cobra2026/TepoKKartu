using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public List<SceneAsset> storedScenes = new List<SceneAsset>();
    public List<Slider> sliders;

    public void StoreScene(string scenePath)
    {
        SceneAsset sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);

        if (sceneAsset != null && !storedScenes.Contains(sceneAsset))
        {
            storedScenes.Add(sceneAsset);
            Debug.Log("Stored Scene: " + sceneAsset.name);
        }
        else
        {
            Debug.Log("Scene not found or already stored.");
        }
    }

    public List<SceneAsset> GetStoredScenes()
    {
        return storedScenes;
    }

    public void LoadSceneByName(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
            Debug.Log("Loading Scene: " + sceneName);
        }
        
        else
        {
            Debug.LogError("Scene name is invalid.");
        }
    }

        public void QuitGame()
    {
        #if UNITY_EDITOR
        Debug.Log("Game quit! This won't work in the editor.");
        #else

        Application.Quit();
        #endif
    }

    public void SetAllSlidersValue(float value)
    {
        foreach (Slider slider in sliders)
        {
            if (slider != null)
            {
                slider.value = value;
            }
        }
    }
}