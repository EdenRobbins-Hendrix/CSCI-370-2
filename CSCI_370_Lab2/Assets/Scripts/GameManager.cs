using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public GameObject Canvas;
    public GameObject Button;
    public int money = 20;
    public int health = 100;
    public int coffeeBeans = 20;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI beanText;
    int day = 0;

    private void Awake() {
        if (main == null) {
            main = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moneyText.text = "Money: " + money;
        healthText.text = "Health: " + health;
        beanText.text = "Beans: " + coffeeBeans;
    }

    void Update(){

    }

    public void changeSceneInShop(){
        Initiate.Fade("InShop", Color.black, 1.0f);
    }

    public void resetGame(){
        money = 20;
        health = 100;
        coffeeBeans = 25;
        day = 0;
        Initiate.Fade("TitleScreen", Color.black, 1.0f);
    }


    public int getDay() {
        return day;
    }

    public void incrementDay(){
        day++;
    }

    public void ReactToClick() {
        Debug.Log("I've been clicked");
        Initiate.Fade("TowerDefense", Color.black, 1.0f);
    }

    public void IncScore(int ds){
        money += ds;
        moneyText.text = "Money: " + money;
    }

    public void IncHealth (int dh){
        health -= dh;
        healthText.text = "Health: " + health;
    }

    public void IncBeans (int db) {
        coffeeBeans += db;
        if (!beanText.IsUnityNull()) {
            beanText.text = "Beans: " + coffeeBeans;
        }
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

    public void ShowDialogue(string dialogue, string name)
    {
        nameText.text = name + "...";
        StartCoroutine(TypeTextUncapped(dialogue));
        dialoguePanel.SetActive(true);
    }

    public void EndDialogue()
    {
        nameText.text = null;
        dialogueText.text = null;
        dialoguePanel.SetActive(false);
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
