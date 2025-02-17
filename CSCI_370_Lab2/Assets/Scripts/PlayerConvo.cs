using NUnit.Framework.Constraints;
using Unity.Multiplayer.Center.Common.Analytics;
using UnityEngine;

public class PlayerConvo : MonoBehaviour
{
    [SerializeField] float talkDistance = 2;
    bool inConversation;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)){
            Interact();
        }    
    }

    void Interact() {
        if (inConversation) {
            GameManager.main.SkipLine();
        }
        else {
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, talkDistance, Vector2.up, 0, LayerMask.GetMask("NPC"));
            if (hit){
                if (hit.collider.gameObject.TryGetComponent(out NPC npc))
                {
                    GameManager.main.StartDialogue(npc.dialogueAsset.dialogue, npc.StartPosition, npc.npcName);

                }
            } 
        }
    }
}
