using UnityEngine;

namespace Mygame
{
    public class PlayerWallClimb : MonoBehaviour
    {
        [Header("climb properties")]
        [SerializeField] private float _climbForce;
        [SerializeField] private float _climbForceMultiplier;
        [SerializeField] private float _maxClimbSpeed;
        [SerializeField] private float _stopForce;

        [Header("Wall Jump Properties")]
        [SerializeField] private float _wallJumpForce;
        [SerializeField] private float _extraUpwardForce;
        [SerializeField] private float _timeBetweenJumps;
        private float _wallJUmpTimer;

        private void FixedUpdate()
        {
            if (SkillAndUpgrades.wallClimb)
                WallClimb(P_Controller._shouldClimb, P_Controller._climbValue, P_Controller._rb, P_Controller._isWalled);

            if (SkillAndUpgrades.wallJump)
                WallJump(P_Controller._rb, ref P_Controller._shouldWallJump, P_Controller._moveVector);
        }

        private void WallClimb(bool shouldClimb, float climbValue, Rigidbody2D rb, bool isWalled)
        {

            if (shouldClimb && isWalled)
            {
                #region Climb
                PlayerData._states = PlayerData.states.isClimbing;
                Vector2 zeroGravityForce = rb.mass * Physics2D.gravity.magnitude * Vector2.up;
                rb.AddForce(zeroGravityForce);

                float wantedVelocity = climbValue * _climbForce;

                Vector2 climbVector = new Vector2(0, wantedVelocity * _climbForceMultiplier);

                if (climbValue > 0 && rb.velocity.y < _maxClimbSpeed)
                {
                    rb.AddForce(climbVector * Time.fixedDeltaTime);
                }
                else if (climbValue < 0 && rb.velocity.y > -_maxClimbSpeed)
                {
                    rb.AddForce(climbVector * Time.fixedDeltaTime);
                }
                #endregion

                #region Speed Limit
                Vector2 oppositeForce = new Vector2(0, (wantedVelocity - rb.velocity.y) * _stopForce);

                if (rb.velocity.y > 0 && rb.velocity.y > wantedVelocity)
                {
                    rb.AddForce(oppositeForce);
                }
                else if (rb.velocity.y < 0 && rb.velocity.y < wantedVelocity)
                {
                    rb.AddForce(oppositeForce);
                }
                #endregion
            }
            else
            {
                PlayerData._states = PlayerData.states.normal;
            }
        }

        private void WallJump(Rigidbody2D rb, ref bool shouldWallJump, Vector2 jumpVector)
        {
            _wallJUmpTimer -= Time.deltaTime;
            bool canJump = _wallJUmpTimer < 0;

            if (shouldWallJump)
            {
                shouldWallJump = false;
                if (PlayerData._states == PlayerData.states.isClimbing && canJump)
                {
                    _wallJUmpTimer = _timeBetweenJumps;
                    rb.AddForce(Vector2.up * _extraUpwardForce);
                    rb.AddForce(jumpVector * _wallJumpForce, ForceMode2D.Impulse);
                }
            }
        }
    }
}

