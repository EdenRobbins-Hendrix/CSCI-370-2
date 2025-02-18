using Unity.Multiplayer.Center.Common.Analytics;
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
        if (Input.GetKeyDown(KeyCode.KeypadEnter)){
            Interact();
        }
    }

    void Interact() {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, interactDistance, Vector2.up, 0, LayerMask.GetMask("NPC"));
            if (hit){
                if (hit.collider.gameObject.CompareTag("CoffeeMachine"))
                {
                    

                }
            } 
    }

}
