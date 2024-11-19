using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button _settings;
    [SerializeField] private Button _play;
    private void Start()
    {
        _settings.onClick.AddListener(OnButtonSettingsClick);
        _play.onClick.AddListener(OnButtonPlayClick);
    }
    private void OnButtonSettingsClick()
    {
        //Bootstrap.Instance.UIManager.ChangeMenuState(MenuStates.Settings);
    }

    private void OnButtonPlayClick()
    {
        Bootstrap.Instance.UIManager.ChangeMenuState(MenuStates.Gameplay);
        Bootstrap.Instance.ChangeGameState(GameStates.InProgress);
        Bootstrap.Instance.ScenesService.InitialLoad();
        Bootstrap.Instance.ScenesService.LoadLevel(Constants.FIRST_SCENE_NAME);
    }
}
