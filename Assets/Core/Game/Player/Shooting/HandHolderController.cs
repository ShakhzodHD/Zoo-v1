using System.Collections.Generic;
using UnityEngine;

public class HandHolderController : MonoBehaviour
{
    public List<HandHolderSlot> handSlots = new();

    [SerializeField] private Vector2 sensitivity = new(1, 1);
    [SerializeField] private Vector2 yClamp = new(-60, 60);
    [SerializeField] private bool smooth;
    [SerializeField] private float interpolationSpeed = 25.0f;

    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody playerCharacterRigidbody;
    private Quaternion rotationCharacter;
    private Quaternion rotationCamera;

    private void Start()
    {
        rotationCharacter = player.transform.localRotation;
        rotationCamera = transform.localRotation;
        Initialize();
    }

    private void Initialize()
    {
        foreach (var slot in handSlots)
        {
            slot.InactiveSlot();
        }
    }

    public void SwitchHand(int index)
    {
        for (int i = 0; i < handSlots.Count; i++)
        {
            if (i == index) 
            {
                handSlots[i].SetActiveSlot();
            }
            else
            {
                handSlots[i].InactiveSlot();
            }
        }
    }

    private void LateUpdate()
    {
        Vector2 frameInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        frameInput *= sensitivity;

        Quaternion rotationYaw = Quaternion.Euler(0.0f, frameInput.x, 0.0f);
        Quaternion rotationPitch = Quaternion.Euler(-frameInput.y, 0.0f, 0.0f);

        rotationCamera *= rotationPitch;
        rotationCharacter *= rotationYaw;

        Quaternion localRotation = transform.localRotation;

        if (smooth)
        {
            localRotation = Quaternion.Slerp(localRotation, rotationCamera, Time.deltaTime * interpolationSpeed);
            playerCharacterRigidbody.MoveRotation(Quaternion.Slerp(playerCharacterRigidbody.rotation, rotationCharacter, Time.deltaTime * interpolationSpeed));
        }
        else
        {
            localRotation *= rotationPitch;
            localRotation = Clamp(localRotation);
            playerCharacterRigidbody.MoveRotation(playerCharacterRigidbody.rotation * rotationYaw);
        }

        transform.localRotation = localRotation;
    }

    private Quaternion Clamp(Quaternion rotation)
    {
        rotation.x /= rotation.w;
        rotation.y /= rotation.w;
        rotation.z /= rotation.w;
        rotation.w = 1.0f;

        float pitch = 2.0f * Mathf.Rad2Deg * Mathf.Atan(rotation.x);
        pitch = Mathf.Clamp(pitch, yClamp.x, yClamp.y);
        rotation.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * pitch);

        return rotation;

    }
}