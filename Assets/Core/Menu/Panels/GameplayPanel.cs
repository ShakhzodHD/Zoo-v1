using UnityEngine;
using UnityEngine.UI;

public class GameplayPanel : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button pause;
    public void Init()
    {
        Debug.Log("��������������� ����������� ������");
    }
    private void Start()
    {
        pause.onClick.AddListener(OnButtonPauseClick);
    }
    private void OnButtonPauseClick()
    {
        //Bootstrap.Instance.TimeScaleController.PauseGame();
        Bootstrap.Instance.UIManager.ChangeMenuState(MenuStates.Pause);
    }
}
