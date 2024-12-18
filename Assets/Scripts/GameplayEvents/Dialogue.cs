using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public static Dialogue instance;
    public TextMeshProUGUI textComponent;
    private bool isTyping = false;
    public string[] lines;
    public float textSpeed;

    public int index = 1;

    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void DisplayText()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    public void StartDialogue()
    {
        StartCoroutine(TypeLine());
    }

    public IEnumerator TypeLine()
    {
        if (isTyping)
        {
            yield break;
        }
        isTyping = true;
        textComponent.text = string.Empty;
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        isTyping = false;
    }
}
