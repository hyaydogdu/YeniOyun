using UnityEngine;
using UnityEngine.InputSystem;


namespace Mygame
{
    public class InputReceiver : MonoBehaviour
    {
        #region variables
        //unnecessary
        private PlayerActionAsset _playerActionAsset;
        private float currentVelocity;
        private float currentVelocity2;
        //inheritance values
        public static float _moveValue;
        public static bool _shouldJump;
        public static bool _shouldExtraJump;
        public static bool _shouldWallJump;
        public static float _climbValue;
        public static bool _shouldClimb;
        public static bool _shoulDash;
        public static bool _shouldAttack;
        public static bool _shouldSpell;
        public static bool Interaction;
        public static Vector2 _mouseposition;
        #endregion

        #region BuiltInMethods
        protected virtual void Awake()
        {
            _playerActionAsset = new PlayerActionAsset();
        }

        protected virtual void OnEnable()
        {
            _playerActionAsset.Enable();
        }

        protected virtual void OnDisable()
        {
            _playerActionAsset.Disable();
        }

        #endregion

        #region Input Methods
        public void OnMove(InputValue value)
        {
            _moveValue = value.Get<float>();
        }

        public void OnJumpPressed()
        {
            _shouldJump = true;
            _shouldExtraJump = true;
            _shouldWallJump = true;
        }

        public void OnJumpReleased()
        {
            _shouldJump = false;
        }

        public void OnWallClimb(InputValue value)
        {
            _climbValue = value.Get<float>();
        }

        public void OnWallClimbPressed()
        {
            _shouldClimb = true;
        }

        public void OnWallClimbReleased()
        {
            _shouldClimb = false;
        }

        public void OnDash()
        {
            _shoulDash = true;
        }

        public void OnAttack()
        {
            _shouldAttack = true;
        }

        public void OnSpell()
        {
            _shouldSpell = true;
        }

        public void OnHitPoint(InputValue value)
        {
            _mouseposition = value.Get<Vector2>();
        }

        public void OnInteract()
        {
            Interaction = true;
        }
        #endregion

    }
}

