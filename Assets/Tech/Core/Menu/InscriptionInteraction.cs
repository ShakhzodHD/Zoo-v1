using UnityEngine;
using UnityEngine.UI;

public class InscriptionInteraction : MonoBehaviour
{
    [SerializeField] private Text textBar;
    [SerializeField] private string textInscription;
    public void Show(InscriptionType type)
    {
        textBar.text = textInscription + type.ToString();
        textBar.gameObject.SetActive(true);
    }
    public void Show(string text)
    {
        textBar.text = text;
        textBar.gameObject.SetActive(true);
    }
    public void Clear()
    {
        textBar.text = null;
        textBar.gameObject.SetActive(false);
    }
}
