using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TallyController : MonoBehaviour
{
    public static TallyController Instance;
    public bool isTallyActivated = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void ActivateTally(bool activated)
    {
        if (activated)
        {
            isTallyActivated = true;
        }
        else
        {
            isTallyActivated = false;
        }
    }

}
