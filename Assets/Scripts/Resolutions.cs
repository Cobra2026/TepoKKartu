using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Resolutions : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Dropdown resolutionDropDown;  // Dropdown for selecting resolution
    public GameObject spriteObject;  // The GameObject with the SpriteRenderer
    private Vector3 originalScale;   // Store original scale of sprite

    private List<Resolution> availableResolutions;
    private int currentResolutionIndex = 0;

    private void Start()
    {
        originalScale = spriteObject.transform.localScale;

        // Define your specific resolutions
        availableResolutions = new List<Resolution>
        {
            new Resolution { width = 2560, height = 1440 },
            new Resolution { width = 1920, height = 1080 },
            new Resolution { width = 1600, height = 900 }
        };

        resolutionDropDown.ClearOptions();

        // Populate the dropdown with resolution options
        List<string> resolutionOptions = new List<string>();
        for (int i = 0; i < availableResolutions.Count; i++)
        {
            string resolutionString = availableResolutions[i].width + " x " + availableResolutions[i].height;
            resolutionOptions.Add(resolutionString);

            if (availableResolutions[i].width == Screen.width && availableResolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropDown.AddOptions(resolutionOptions);
        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution selectedResolution = availableResolutions[resolutionIndex];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);

        // Adjust the scale of the sprite based on the new resolution
        AdjustSpriteScale(selectedResolution.width, selectedResolution.height);
    }

    private void AdjustSpriteScale(int width, int height)
    {
        // Calculate the scale factor based on a reference resolution (e.g., 1920x1080)
        float baseWidth = 1920f;
        float scaleFactor = width / baseWidth;

        // Apply the scale factor to the sprite object
        spriteObject.transform.localScale = originalScale * scaleFactor;
    }
}
