using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager main;
    public Transform startpoint;
    public Transform[] path;
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
}
