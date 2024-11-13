using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;
    private int lastIndex = -1;

    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // void Update()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         if (textComponent.text == lines[index])
    //         {
    //             NextLine();
    //         }
    //         else
    //         {
    //             StopAllCoroutines();
    //             textComponent.text = lines[index];
    //         }
    //     }
    // }

    void StartDialogue()
    {
        index = GetRandomLineIndex();
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NextLine()
    {
        if (textComponent.text == lines[index])
        {
           index = GetRandomLineIndex();

            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            StopAllCoroutines();
            textComponent.text = lines[index];
        }
    }

    int GetRandomLineIndex()
    {
        int randomIndex;

        do
        {
            randomIndex = Random.Range(0, lines.Length);
        } while (randomIndex == lastIndex);

        lastIndex = randomIndex;
        return randomIndex;
    }
}
