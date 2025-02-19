using UnityEngine;

public class GoToTD : MonoBehaviour
{
    [SerializeField] float interactDistance = 1.5f;
    public GameObject exit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
                if (hit.collider.gameObject == exit)
                
                {
                    Debug.Log("Leaving!");
                    GameManager.main.incrementDay();
                    Initiate.Fade("TowerDefense", Color.black, 1.0f);
                }
            } 
    }
}
