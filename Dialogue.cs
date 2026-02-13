using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// Ryan Smith Objective and Dialogue Smaller Script

public class Dialogue : MonoBehaviour
{
    [Header("Non-player Character")]
    public string[] dialogueList;
    public TMP_Text dialogueText;

    [Header("Objective")]
    public string[] objectiveStates;
    private int objectiveStatesIndex;
    private bool objectiveComplete = false;
    public TMP_Text objectiveText;
    public int objectiveCompleteText;

    //-----------------------------------Start is called once upon creation-------------------------
    private void Start()
    {
        dialogueText.text = " "; // Displays Line
        objectiveText.text = objectiveStates[objectiveStatesIndex]; // Displays what would be the first description at this point
    }

    //-----------------------------------Update is called once per frame----------------------------
    private void Update()
    {
        if (objectiveComplete == true) // Always checks if the player has completed the objective 
        {
            objectiveText.text = objectiveStates[objectiveCompleteText]; // Instant change to show player has progressed
        }
    }

    //-----------------------------------Dialogue System----------------------------
    IEnumerator dialogueCoroutine()
    {
        if (objectiveStatesIndex < 1)
        {
            objectiveStatesIndex++; // Progresses Objective
            objectiveText.text = objectiveStates[objectiveStatesIndex];
        }

        for (int i = 0; i < dialogueList.Length; i++) // For loop saves from repetition + easier to use with inspector as it gets list amount
        {
            dialogueText.text = dialogueList[i]; 
            yield return new WaitForSeconds(2); // Waits before changing, so player can understand dialogue
        }

        if (objectiveStatesIndex == 1) // Temporary 
        {
            objectiveComplete = true;
        }

        dialogueText.text = " "; // Hides last line 
    }

    //-----------------------------------Temporary Trigger----------------------------

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger"))
        {
            StartCoroutine(dialogueCoroutine());
        }

        if (other.gameObject.CompareTag("Exit") && objectiveComplete == true)
        {
            SceneManager.LoadScene(0);
        }
    }
}