using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] bool firstInteraction = true;
    [SerializeField] int repeatStartPosition;
    public string npcName;
    public DialogueAsset dialogueAsset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int  StartPosition {
        get
        {
            if (firstInteraction) {
                firstInteraction = false;
                return 0;
            }
            else{
                return repeatStartPosition;
            }
        }
    }
}
