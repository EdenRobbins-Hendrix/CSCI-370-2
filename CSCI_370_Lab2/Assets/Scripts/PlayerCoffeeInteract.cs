using UnityEngine;

public class PlayerCoffeeInteract : MonoBehaviour
{
    [SerializeField] float interactDistance = 2;
    public GameObject coffeeMachine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)){
            Interact();
        }
    }

    void Interact() {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, interactDistance, Vector2.up, 0, LayerMask.GetMask("NPC"));
        Debug.Log("Sending signal");
            if (hit){

            Debug.Log("Signal received");
                if (hit.collider.gameObject == coffeeMachine)
                
                {
                    Debug.Log("Coffeetime!");
                    if (GameManager.main.money > 0){
                    GameManager.main.IncBeans(1);
                    GameManager.main.IncScore(-1);}

                }
            } 
    }

}
