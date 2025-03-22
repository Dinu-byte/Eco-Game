using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue;
    [SerializeField] private GameObject dialogueBox;

    private TypeWriterEffect typeWriterEffect;

    public bool IsOpen {  get; private set; }


    private void Start()
    {
        typeWriterEffect = GetComponent<TypeWriterEffect>();
        CloseDialogueBox();
    }

    public void ShowDialogue (DialogueObject dialogueObject)
    {
        IsOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue (DialogueObject dialogueObject)
    {

        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];

            yield return RunTypingEffect(dialogue);

            textLabel.text = dialogue;

            if (i == dialogueObject.Dialogue.Length - 1) break;

            yield return null;

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        }

        yield return new WaitForSeconds(0.3f);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E)); // wait for the player to press E to close the dialogue box.
        CloseDialogueBox();

    }

    private IEnumerator RunTypingEffect(string dialogue)
    {
        typeWriterEffect.Run(dialogue, textLabel);

        while (typeWriterEffect.isRunning)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.E))
            {
                typeWriterEffect.Stop();
            }
        }
    }

    private void CloseDialogueBox ()
    {
        IsOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }

}
