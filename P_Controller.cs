using UnityEngine;
using Cinemachine;

public class P_Controller : InputReceiver
{
    #region Variables
    public static Rigidbody2D _rb;
    public static Collider2D _coll;

    public static Vector2 _moveVector;

    public static bool _isGrounded;
    public static bool _isWalled;
    public static bool _facingLeft;


    [Header("Ground Check Variables")]
    public Vector2 _boxsize;
    public float _castDistanceForGround;
    public LayerMask _groundLayer;

    [Header("Wall Check Properties")]
    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask groundLayer;
    public float _castDistanceForWall;

    [Header("CameraPropeties")]
    [SerializeField] private CinemachineVirtualCamera _virtualMainCam;
    [SerializeField] private float _smoothTime;
    [SerializeField] private float _facingRightXvalue;
    [SerializeField] private float _facingLeftXvalue;

    #endregion

    #region BuiltInMethods
    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody2D>();
        _coll = GetComponent<Collider2D>();
        _virtualMainCam = GameObject.FindGameObjectWithTag("V_MainCam").GetComponent<CinemachineVirtualCamera>();
        PlayerData._states = PlayerData.states.normal;
    }

    private void Update()
    {
        SetMoveVector();
        SetCheckValues();
    }

    private void FixedUpdate()
    {
        ChangeDirection(_rb, ref _virtualMainCam, _moveValue);
    }
    #endregion

    #region Set Value Methods
    private void SetMoveVector()
    {
        _moveVector = new Vector2(_moveValue, _climbValue);
        _moveVector.Normalize();
    }

    private void SetCheckValues()
    {
        _isGrounded = Physics2D.BoxCast(transform.position, _boxsize, 0, -transform.up, _castDistanceForGround, _groundLayer);
        _isWalled = Physics2D.CircleCast(wallCheck.position, wallCheckRadius, transform.right, _castDistanceForWall, groundLayer);
    }
    #endregion

    private void ChangeDirection(Rigidbody2D rb, ref CinemachineVirtualCamera cam, float _moveValue)
    {
        Quaternion _rightRotation = Quaternion.Euler(0, 0, 0);
        Quaternion _leftRotation = Quaternion.Euler(0, 180, 0);
        if (rb.velocity.x > 0.3f && transform.rotation != _rightRotation)
        {
            transform.rotation = _rightRotation;
            _facingLeft = false;
        }

        if (rb.velocity.x < -0.3f && transform.rotation != _leftRotation)
        {
            transform.rotation = _leftRotation;
            _facingLeft = true;
        }

        if (PlayerData._states == PlayerData.states.normal)
        {
            if (rb.velocity.x == 0 && _moveValue > 0.2f)
            {
                transform.rotation = _rightRotation;
                _facingLeft = false;
            }

            if (rb.velocity.x == 0 && _moveValue < -0.2f)
            {
                transform.rotation = _leftRotation;
                _facingLeft = true;
            }
        }
    }
}
