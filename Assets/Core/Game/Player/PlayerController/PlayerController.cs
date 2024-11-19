using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInputSystem))]
public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private float moveSpeed = 4.0f;
    [SerializeField] private float sprintSpeed = 6.0f;
    [SerializeField] private float rotationSpeed = 1.0f;
    [SerializeField] private float speedChangeRate = 10.0f;

    [Header("Sprint")]
    [SerializeField] public float stamina = 100f;               // Текущая выносливость
    [SerializeField] public const float maxStamina = 100f;      // Максимальная выносливость
    [SerializeField] private const float staminaDrainRate = 20f; // Скорость расхода выносливости
    [SerializeField] private const float staminaRegenRate = 10f; // Скорость восстановления выносливости

    [Space]
    [SerializeField] private float jumpHeight = 1.2f;
    [SerializeField] private float gravity = -15.0f;
    [SerializeField] private float jumpTimeout = 0.1f;
    [SerializeField] private float fallTimeout = 0.15f;

    [Space]
    [SerializeField] private bool grounded = true;
    [SerializeField] private float groundedOffset = -0.14f;
    [SerializeField] private float groundedRadius = 0.5f;
    [SerializeField] private LayerMask groundLayers;

    [Header("Camera")]
    [SerializeField] private GameObject cameraTarget;
    [SerializeField] private float topClamp = 90.0f;
    [SerializeField] private float bottomClamp = -90.0f;

    private float _targetPitch;

    public float _speed; //временно public
    private float _rotationVelocity;
    private float _verticalVelocity;
    private float _terminalVelocity = 53.0f;

    private bool canSprint => stamina > 10f;    // Условие для возможности спринта

    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;

    private PlayerInput _playerInput;
    private CharacterController _controller;
    private PlayerInputSystem _input;

    private const float _threshold = 0.01f;
    private bool IsCurrentDeviceMouse
    {
        get { return _playerInput.currentControlScheme == "Keyboard&Mouse"; }
    }
    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _input = GetComponent<PlayerInputSystem>();
        _playerInput = GetComponent<PlayerInput>();

        _jumpTimeoutDelta = jumpTimeout;
        _fallTimeoutDelta = fallTimeout;
    }
    private void Update()
    {
        JumpAndGravity();
        GroundedCheck();
        Move();
    }
    private void LateUpdate()
    {
        CameraRotation();
    }
    private void GroundedCheck()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
        grounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);
        Debug.DrawLine(transform.position, spherePosition, Color.red, 0.1f);
    }
    private void Move()
    {
        float targetSpeed;

        if (_input.sprint && canSprint)
        {
            targetSpeed = sprintSpeed;
            stamina -= staminaDrainRate * Time.deltaTime;
        }
        else
        {
            _input.sprint = false;
            targetSpeed = moveSpeed;
            stamina += staminaRegenRate * Time.deltaTime;
        }

        stamina = Mathf.Clamp(stamina, 0f, maxStamina);


        if (_input.move == Vector2.zero) targetSpeed = 0.0f;

        float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

        float speedOffset = 0.1f;
        float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

        if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * speedChangeRate);

            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }

        Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

        if (_input.move != Vector2.zero)
        {
            inputDirection = transform.right * _input.move.x + transform.forward * _input.move.y;
        }

        _controller.Move(inputDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
    }
    private void JumpAndGravity()
    {
        if (grounded)
        {
            _fallTimeoutDelta = fallTimeout;

            if (_verticalVelocity < 0.0f) _verticalVelocity = -2f;

            if (_input.jump && _jumpTimeoutDelta <= 0.0f)
            {
                _verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            if (_jumpTimeoutDelta >= 0.0f) _jumpTimeoutDelta -= Time.deltaTime;
        }
        else
        {
            _jumpTimeoutDelta = jumpTimeout;

            if (_fallTimeoutDelta >= 0.0f) _fallTimeoutDelta -= Time.deltaTime;

            _input.jump = false;
        }

        if (_verticalVelocity < _terminalVelocity) _verticalVelocity += gravity * Time.deltaTime;
    }
    private void CameraRotation()
    {
        if (_input.look.sqrMagnitude >= _threshold)
        {
            float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

            _targetPitch += _input.look.y * rotationSpeed * deltaTimeMultiplier;
            _rotationVelocity = _input.look.x * rotationSpeed * deltaTimeMultiplier;

            _targetPitch = ClampAngle(_targetPitch, bottomClamp, topClamp);

            cameraTarget.transform.localRotation = Quaternion.Euler(_targetPitch, 0.0f, 0.0f);

            transform.Rotate(Vector3.up * _rotationVelocity);
        }
    }
    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (grounded) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z), groundedRadius);
    }
}
