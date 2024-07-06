using UnityEngine;

public class DialogueContainer : MonoBehaviour
{
    Dialogue dialogue;

    [SerializeField]
    Sprite[] narratorPics;


    [SerializeField, TextArea]
    string[] messages;

    private void Start()
    {
        dialogue = ScriptableObject.CreateInstance<Dialogue>();
        Dialogue currentDialogue = dialogue;
        for (int i = 0; i <= messages.Length - 1; i++)
        {
            currentDialogue.message = messages[i];
            currentDialogue.narratorImage = narratorPics[i];
            if (i < messages.Length - 1)
            {
                currentDialogue.daughterDialogue = new Dialogue();
                currentDialogue = currentDialogue.daughterDialogue;
            }
        }
    }

    


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            GameManager.Instance.DialogueDisplayEvent.Invoke(dialogue);
            Destroy(gameObject);
        }
    }

}