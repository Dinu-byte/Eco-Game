using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField] [TextArea] private string[] dialogue;
    [SerializeField] private Response[] responses;

    public string[] Dialogue => dialogue; // getter

    public Response[] Responses => responses; // getter

    public bool hasResponses => Responses != null && Responses.Length > 0;
}