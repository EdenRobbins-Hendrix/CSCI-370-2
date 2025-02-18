using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] bool firstInteraction = true;
    [SerializeField] int repeatStartPosition;
    public string npcName;
    public DialogueAsset dialogueAsset;
    public DialogueAsset dialogueAsset2;
    public DialogueAsset dialogueAsset3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int  StartPosition {
        get
        {
            if (firstInteraction) {
                ChangeDialogue();
                firstInteraction = false;
                return 0;
            }
            else{
                return repeatStartPosition;
            }
        }
    }

    public void ChangeDialogue() {
        if (GameManager.main.getDay() == 1){
        }
        if (GameManager.main.getDay() == 2){
        dialogueAsset = dialogueAsset2;
        }
        if (GameManager.main.getDay() >= 3){
        dialogueAsset = dialogueAsset3;
        }
    }
}
