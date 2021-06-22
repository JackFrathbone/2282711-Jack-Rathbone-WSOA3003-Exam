using TMPro;
using UnityEngine;

public class Pop_Up : MonoBehaviour
{
    public GameObject textBox;
    public string popUpText;
    private TextMeshProUGUI textBody;

    private void Start()
    {
        textBody = textBox.GetComponentInChildren<TextMeshProUGUI>();
    }


    public void PopUpOn()
    {
        textBox.SetActive(true);
        textBody.text = popUpText;
    }

    public void PopUpOff()
    {
        textBody.text = "";
        textBox.SetActive(false);
    }
}
