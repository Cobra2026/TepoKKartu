using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TallyToggle : MonoBehaviour
{
    private TallyController tallyController;
    private Toggle tallyToggle;

    private void Start()
    {
        tallyToggle = GetComponent<Toggle>();
        tallyController = TallyController.Instance;

        if (tallyController.isTallyActivated)
        {
            tallyToggle.isOn = true;
        }
        else
        {
            tallyToggle.isOn = false;
        }
    }

    public void SetActivation(bool toggleValue)
    {
        if(toggleValue)
        {
            tallyController.ActivateTally(toggleValue);
        }
        else
        {
            tallyController.ActivateTally(toggleValue);
        }
    }
}
