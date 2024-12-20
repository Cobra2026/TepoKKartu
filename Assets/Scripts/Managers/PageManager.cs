using System.Collections.Generic;
using UnityEngine;

public class PageManager : MonoBehaviour
{
    [Header("Page List")]
    [SerializeField] private List<GameObject> pages;
    
    public void ActivatePage(int pageIndex)
    {
        if (pageIndex < 0 || pageIndex >= pages.Count)
        {
            Debug.LogWarning("Page index out of range.");
            return;
        }

        for (int i = 0; i < pages.Count; i++)
        {
            pages[i].SetActive(i == pageIndex);
        }

        Debug.Log($"Activated Page {pageIndex + 1}");
    }
}
