using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NpcDialogueController : MonoBehaviour
{
    #region PRIVATE VARIABLES
    [Header("DEBUG PURPOSE ONLY")]
    private bool isInRange;
    private int dialogueIndex;
    private int counter = 0;
    private bool dialogueEnded;
    #endregion

    #region COMPONENTS
    [SerializeField] private KeyCode interactKey;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Text nameOfTheNpc;
    [SerializeField] private Text dialogueText;
    [SerializeField] private string[] dialogue;
    [SerializeField] private GameObject ContinueButton;
    [SerializeField] private float wordSpeed;
    #endregion

    private void Start()
    {
        interactKey = KeyCode.F;
        /*dialoguePanel = GameObject.Find("DialoguePanel");
        dialogueText = GameObject.Find("TextOfTheNpc").GetComponent<Text>();
        nameOfTheNpc = GameObject.Find("NameOfTheNpc").GetComponent<Text>();
        ContinueButton = GameObject.Find("ContinueButton");*/
        wordSpeed = 0.08f;
        dialogueEnded = true;
        nameOfTheNpc.text = gameObject.name;
    }

    private void Update()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(interactKey)&& dialogueEnded)
            {
                dialogueEnded = false;
                if (dialoguePanel.activeInHierarchy)
                {
                    zeroText();

                }
                else
                {
                    dialoguePanel.SetActive(true);
                    StartCoroutine(Type());
                }
            }
            if (dialogueText.text == dialogue[dialogueIndex] && !dialogueEnded)
            {
                ContinueButton.SetActive(true);
            }
            if (counter == dialogue.Length)
            {

                dialogueText.text = "";
                dialogueIndex = 0;
                counter = 0;
                dialogueEnded = true;
            }
        }
        
    }

    public void zeroText()
    {
        dialogueText.text = "";
        dialogueIndex = 0;
        dialoguePanel.SetActive(false);
    }
    IEnumerator Type()
    {
        foreach (char letter in dialogue[dialogueIndex].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }
    public void NextLine()
    {
        ContinueButton.SetActive(false);
        counter++;
        if (dialogueIndex < dialogue.Length - 1)
        {
            dialogueIndex++;
            dialogueText.text = "";
            StartCoroutine(Type());
        }
        else
        {
            zeroText();
        }
    }
    public bool getDialogueEnded()
    {
        return dialogueEnded;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
            Debug.Log("Player in range");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
            zeroText();
            counter = 0;
            dialogueEnded = true;
            Debug.Log("Player out of range");
        }
    }
}
