using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bootstrap : MonoBehaviour
{
    public static Bootstrap Instance { get; private set; }
    public UIManager UIManager { get; private set; }
    public ScenesService ScenesService { get; private set; }
    public GameStates GameState { get; private set; } = GameStates.InMenu;
    public event Action<GameStates> OnGameStateChanged;
    public GameSettings GameSettings { get; private set; }
    public Camera Camera { get; set; }
    public PlayerInput PlayerInput { get; set; }
    public InventoryManager InventoryManager { get; private set; }
    public InscriptionInteraction InscriptionInteraction { get; set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        Init();

        GameSettings = Resources.Load<GameSettings>(Constants.GAME_SETTINGS_RESOURCES_PATH);
        PlayerInput = GameSettings.PlayerPrefab.GetComponent<PlayerInput>();

        UIManager.Init();

        DontDestroyOnLoad(gameObject);
        ScenesService.OnLevelLoaded += OnLevelLoaded;
    }
    private void Init()
    {
        ScenesService = FindObjectOfType<ScenesService>();
        UIManager = FindObjectOfType<UIManager>();
        InventoryManager = FindObjectOfType<InventoryManager>();
        InscriptionInteraction = FindObjectOfType<InscriptionInteraction>();
    }
    private void OnLevelLoaded()
    {
        ChangeGameState(GameState == GameStates.InProgress ? GameStates.InProgress : GameStates.InMenu);
    }
    public void ChangeGameState(GameStates gameState)
    {
        if (GameState == gameState && GameState != GameStates.InProgress) return;
        GameState = gameState;
        OnGameStateChanged?.Invoke(gameState);
    }
}
