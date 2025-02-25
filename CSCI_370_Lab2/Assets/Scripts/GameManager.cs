using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public int coffeeBeans = 25;
    public int currentwave = 1;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI beanText;
    int day = 0;
    public bool spawning = true;

    private void Awake() {
    if (main == null) {
        main = this;
        DontDestroyOnLoad(gameObject);
        }
    else {
        Destroy(gameObject); 
        }

    if (healthText != null) {
        healthText.text = "Health: " + health;
        }
    if (moneyText != null) {
        moneyText.text = "Money: " + money;
        }
    if (beanText != null) {
        beanText.text = "Beans: " + coffeeBeans;
        }
    }

    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name == "TowerDefense") {
            GameManager.main.healthText = GameObject.Find("TextHealth").GetComponent<TextMeshProUGUI>();
            GameManager.main.moneyText = GameObject.Find("TextMoney").GetComponent<TextMeshProUGUI>();
            GameManager.main.beanText = GameObject.Find("TextBeans").GetComponent<TextMeshProUGUI>();
            GameManager.main.Canvas = GameObject.Find("Canvas");
            beanText.text = "Beans: " + coffeeBeans;
            moneyText.text = "Money: " + money;
            healthText.text = "Health: " + health;
        }
        if (scene.name == "InShop") {
            Canvas = GameObject.FindWithTag("Canvas");
            dialoguePanel = GameObject.FindWithTag("DialogueBox");
            // GameManager.main.dialoguePanel = GameObject.Find("DialoguePanel");
            // GameManager.main.nameText = GameObject.Find("NameText").GetComponent<TextMeshProUGUI>();
            // GameManager.main.dialogueText = GameObject.Find("DialogueText").GetComponent<TextMeshProUGUI>();
            GameManager.main.moneyText = GameObject.Find("Money").GetComponent<TextMeshProUGUI>();
            GameManager.main.beanText = GameObject.Find("Beans").GetComponent<TextMeshProUGUI>();
            beanText.text = "Beans: " + coffeeBeans;
            moneyText.text = "Money: " + money;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update(){

    }

    public void changeSceneInShop(){
        StopAllCoroutines();
        spawning = false;
        GameObject Directions = startpoint.parent.GameObject();
        DontDestroyOnLoad(Directions);
        healthText.text = "";
        Initiate.Fade("InShop", Color.black, 1.0f);
        Canvas = GameObject.FindWithTag("Respawn");
    }

    public void resetGame(){
        spawning = false;
        StopAllCoroutines();
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
        StopAllCoroutines();
        spawning = true;
        healthText.text = "Health: " + health;
        if (!dialogueText.IsUnityNull() && !nameText.IsUnityNull()){
            DontDestroyOnLoad(dialogueText);
            DontDestroyOnLoad(nameText);
        }
        Debug.Log("I've been clicked");
        Initiate.Fade("TowerDefense", Color.black, 1.0f);
        Canvas = GameObject.FindWithTag("Respawn");
    }

    public void IncScore(int ds){
        money += ds;
        // moneyText = Canvas.transform.GetChild(0).GameObject().GetComponent<TextMeshProUGUI>();
        moneyText.text = "Money: " + money;
    }

    public void IncHealth (int dh){
        health -= dh;
        GameObject light = GameObject.FindWithTag("Finish");
        AudioSource audio = light.GetComponent<AudioSource>();
        audio.Play();
        // healthText = Canvas.transform.GetChild(2).GameObject().GetComponent<TextMeshProUGUI>();
        healthText.text = "Health: " + health;
    }

    public void IncBeans (int db) {
        coffeeBeans += db;

        // beanText = Canvas.transform.GetChild(1).GameObject().GetComponent<TextMeshProUGUI>();
        if (!beanText.IsUnityNull()) {
            beanText.text = "Beans: " + coffeeBeans;
        }
    }


    public void StartDialogue(String[] dialogue, int StartPosition, string name) {
        dialoguePanel = GameObject.FindWithTag("DialogueBox");
        dialoguePanel.transform.position = new Vector3(420, 50, 0);
        TextMeshProUGUI[] texts = Canvas.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI text in texts) {
            if (text.GameObject().CompareTag("Name")) {
                nameText = text;
            }
            else if (text.GameObject().CompareTag("Dialogue")) {
                dialogueText = text;
            }
        }
        nameText.text = name + "...";
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

        dialoguePanel.transform.position = new Vector3(0, 900000f, 0);
        OnDialogueEnded?.Invoke();
    }

    public void SkipLine(){
        skipLineTriggered = true;
    }

    public void ShowDialogue(string dialogue, string name)
    {
        nameText.text = name + "...";
        StartCoroutine(TypeTextUncapped(dialogue));
        
        dialoguePanel.transform.position = new Vector3(420, 50, 0);
    }

    public void EndDialogue()
    {
        
        dialoguePanel.transform.position = new Vector3(0, 900000f, 0);
        nameText.text = null;
        dialogueText.text = null;
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
