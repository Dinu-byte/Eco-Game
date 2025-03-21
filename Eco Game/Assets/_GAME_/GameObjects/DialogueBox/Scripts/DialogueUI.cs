using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;

    private void Start()
    {
        GetComponent<TypeWriterEffect>().Run("Nigger\nFor life! Also fuck you nigga\nI'm gon kick yo' ass.", textLabel);
    }
}
