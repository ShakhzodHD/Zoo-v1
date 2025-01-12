using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button back;

    private PlayerInput playerInput;
    private bool isPaused = false;
    public void Init()
    {
        playerInput = Bootstrap.Instance.PlayerInput;
        playerInput.actions["Pause"].performed += OnPausePerformed;
    }
    private void Start()
    {
        back.onClick.AddListener(OnButtonBackClick);
    }

    private void OnPausePerformed(InputAction.CallbackContext context)
    {
        TogglePause();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;
            Bootstrap.Instance.UIManager.ChangeMenuState(MenuStates.Pause);
        }
        else
        {
            Time.timeScale = 1;
            Bootstrap.Instance.UIManager.ChangeMenuState(MenuStates.Gameplay);
        }
    }

    private void OnButtonBackClick()
    {
        TogglePause();
    }
}
