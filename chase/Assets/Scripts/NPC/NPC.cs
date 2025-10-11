using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour, IInteractable
{
    public NPCDialogue dialogueData;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    //public Text dialogueText;
    //public string[] dialogue;
    //private int index;
    public UnityEngine.UI.Image portraitImage;

    private int dialogueIndex;
    private bool isTyping, isDialogueActive;
    private Transform player;
    private Vector3 originalScale;
    private Transform currPlatform;

    void Start()
    {
        originalScale = transform.localScale;
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    //if dia box not active, can interact
    public bool CanInteract()
    {
        return !isDialogueActive;
    }

    public void Interact()
    {
        // If not dia data or game is paused and no dia is active
        if (dialogueData == null || (PauseController.IsGamePaused && !isDialogueActive))
            return;

        if (isDialogueActive)
        {
            NextLine();
        }
        else
        {
            FacePlayer();
            StartDialogue();
        }
    }
    void FacePlayer()
    {
        if (player == null) return;
        if (player.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        } else
        {
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
    }

    void StartDialogue()
    {
        isDialogueActive = true;
        dialogueIndex = 0;

        nameText.SetText(dialogueData.npcName);
        portraitImage.sprite = dialogueData.npcPortrait;

        dialoguePanel.SetActive(true);
        PauseController.SetPause(true);

        StartCoroutine(TypeLine());
    }

    void NextLine()
    {
        if (isTyping)
        {
            // Skip typing anomation and show full line
            StopAllCoroutines();
            dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }
        else if (++dialogueIndex < dialogueData.dialogueLines.Length) {
            // If another line, type next line
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.SetText("");

        foreach(char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }

        isTyping = false;

        if (dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            NextLine();
        }
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueText.SetText("");
        dialoguePanel.SetActive(false);
        PauseController.SetPause(false);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Platform"))
    //    {
    //        currPlatform = collision.transform;
    //        transform.SetParent(currPlatform);
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Platform"))
    //    {
    //        transform.SetParent(null);
    //        currPlatform = null;
    //    }
    //}
}
