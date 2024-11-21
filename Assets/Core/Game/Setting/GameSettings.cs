using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/Game Settings")]
public class GameSettings : ScriptableObject
{
    [Header("PREFABS")]
    public GameObject PlayerPrefab;
    public GameObject[] WeaponsPrefabs;

    [Header("SOUNDS")]
    public AudioClip PistolShootSound;

    [Header("CROSS ICONS")]
    public Sprite defaultCross;
    public Sprite interactableCrossPC;
    public Sprite interactableCrossMobile;
    [HideInInspector] public Sprite universalInteractableCross;

    public void ChangeInteractableCross(bool isMobile)
    {
        if (isMobile) universalInteractableCross = interactableCrossMobile;
        else universalInteractableCross = interactableCrossPC;
    }
}
