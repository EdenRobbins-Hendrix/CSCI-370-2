using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager main;
    public Transform startpoint;
    public Transform[] path;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] GameObject dialoguePanel;
    public static event Action OnDialogueStarted;
    public static event Action OnDialogueEnded;
    bool skipLineTriggered;
    public float charactersPerSec = 90;

    int money = 0;
    public TextMeshProUGUI moneyText;

    private void Awake() {
        main = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialogue(String[] dialogue, int StartPosition, string name) {
        nameText.text = name + "...";
        dialoguePanel.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(RunDialogue(dialogue, StartPosition));
    }

    IEnumerator RunDialogue(string[] dialogue, int StartPosition) {
        skipLineTriggered = false;
        OnDialogueStarted?.Invoke();

        for (int i = StartPosition; i < dialogue.Length; i++) {
            dialogueText.text = null;
            StartCoroutine(TypeTextUncapped(dialogue[i]));

            while (!skipLineTriggered) {
                yield return null;
            }
            skipLineTriggered = false;
        }

        OnDialogueEnded?.Invoke();
        dialoguePanel.SetActive(false);
    }

    public void SkipLine(){
        skipLineTriggered = true;
    }

    IEnumerator TypeTextUncapped(string text) {
        float timer = 0;
        float interval = 1 / charactersPerSec;
        string textBuffer = null;
        char[] chars = text.ToCharArray();
        int i = 0;

        while (i < chars.Length) {
            if (timer < Time.deltaTime) {
                textBuffer += chars[i];
                dialogueText.text = textBuffer;
                timer += interval;
                i++;
            }
            else{
                timer -= Time.deltaTime;
                yield return null;
            }
        }
    }

}
