using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("Jump Variables")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpableTime;
    [SerializeField] private float _extraJumpForce;
    [SerializeField] private float _coyoteTime;
    private int extraJumpRights;
    private float Jumptimer;
    private float coyoteTimer;

    private void FixedUpdate()
    {
        if (PlayerData._states == PlayerData.states.normal)
            Jump(ref P_Controller._shouldJump, ref P_Controller._shouldExtraJump, P_Controller._rb,
            P_Controller._isGrounded, SkillAndUpgrades.doubleJump, SkillAndUpgrades.thripleJump);
    }

    private void Jump(ref bool shouldJump, ref bool shouldExtraJump, Rigidbody2D rb, bool isGrounded, bool doubleJump, bool thripleJump)
    {
        bool _isGroundedwCoyote;
        #region Implement Coyote Time
        coyoteTimer -= Time.deltaTime;
        if (isGrounded)
            coyoteTimer = _coyoteTime;

        _isGroundedwCoyote = coyoteTimer > 0;

        #endregion

        #region Normal Jump Action
        Jumptimer -= Time.fixedDeltaTime;
        bool extraPowerForCoyoteTime = false;
        if (shouldJump && _isGroundedwCoyote)
        {
            Jumptimer = _jumpableTime;
            coyoteTimer = 0;
        }
        if (shouldJump && _isGroundedwCoyote && !isGrounded)
        {
            extraPowerForCoyoteTime = true;
        }
        if (Jumptimer < 0)
        {
            shouldJump = false;
        }
        if (Jumptimer > 0 && shouldJump)
        {
            if (extraPowerForCoyoteTime)
            {
                extraPowerForCoyoteTime = false;
                float _gravityScale = Mathf.Pow(rb.gravityScale, 1.5f);
                rb.AddForce(rb.mass * Physics2D.gravity.magnitude * _gravityScale * (rb.velocity.magnitude / 3.14f) * Vector2.up);
            }
            rb.AddForce(Vector2.up * _jumpForce * Jumptimer);
        }
        #endregion

        #region Set Multiple Jumps
        if (thripleJump && doubleJump && isGrounded)
            extraJumpRights = 2;
        else if (doubleJump && isGrounded)
            extraJumpRights = 1;
        else if (isGrounded)
            extraJumpRights = 0;
        #endregion

        #region Extra Jump Action
        if (shouldExtraJump)
        {
            shouldExtraJump = false;
            if (!isGrounded && extraJumpRights > 0)
            {
                rb.AddForce(Vector2.up * _extraJumpForce);
                extraJumpRights--;
            }
        }
        #endregion
    }
}
