using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{

    [SerializeField] Slider playerHealthBar;
    [SerializeField] Slider coolDownBar;
    [SerializeField] Transform deadScreen;
    [SerializeField] AudioClip deadSound;


    [SerializeField] Image dialogueNarratorImage;
    [SerializeField] TextMeshProUGUI dialogueText;

    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.Instance.PlayerDamageEvent != null && GameManager.Instance.PlayerDeadEvent != null)
        {
            GameManager.Instance.PlayerHealthChangeEvent.AddListener(PlayerHealthChangeEventHandler);
            GameManager.Instance.PlayerDeadEvent.AddListener(PlayerDeadEventHandler);
            GameManager.Instance.DialogueDisplayEvent.AddListener(DialogueDisplayEventHandler);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void PlayerDeadEventHandler()
    {
        if(deadScreen != null)
        {
            if(deadSound != null)
            {
                //GameManager.Instance.AudioManager.Play(deadSound);
            }
            deadScreen.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void HeatupValueChangeEventHandler(float value)
    {
        if(coolDownBar != null)
        {
            coolDownBar.value = value;
        }
    }


    void PlayerHealthChangeEventHandler(float value)
    {
        if(playerHealthBar != null)
        {
            playerHealthBar.value -= value;
        }
    }

    bool isProcessingDialogue = false;


    void DialogueDisplayEventHandler(ScriptableObject dialogue)
    {
        if (isProcessingDialogue)
            StopCoroutine("DialogueProcess");
        if (dialogueNarratorImage != null && dialogueText != null)
            StartCoroutine(DialogueProcess((Dialogue)dialogue));
    }

    IEnumerator<WaitForSeconds> DialogueProcess(Dialogue dialogue)
    {
        dialogueNarratorImage.gameObject.SetActive(true);

        dialogueNarratorImage.sprite = dialogue.narratorImage;
        isProcessingDialogue = true;
        for (int i = 0; i <= dialogue.message.Length - 1; i++)
        {
            dialogueText.text = dialogue.message.Substring(0, i);

            yield return new WaitForSeconds(.05f);
        }

        yield return new WaitForSeconds(2f);

        if (dialogue.daughterDialogue != null)
        {
            DialogueDisplayEventHandler(dialogue.daughterDialogue);
        }
        else
        {
            dialogueText.text = null;
            dialogueNarratorImage.sprite = null;
            isProcessingDialogue = false;
            dialogueNarratorImage.gameObject.SetActive(false);
        }

    }
}




