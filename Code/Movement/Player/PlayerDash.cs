using UnityEngine;

namespace Mygame
{
    public class PlayerDash : MonoBehaviour
    {
        [SerializeField] private float _dashPower;
        [SerializeField] private float _dashTime;
        [SerializeField] private float _timeBetweenDashes;
        private float _canDashTimer;
        private float _actiontimer;

        private void FixedUpdate()
        {
            if (SkillAndUpgrades.dash)
                Dash(P_Controller._rb, ref P_Controller._shoulDash, P_Controller._moveVector);
        }

        private void Dash(Rigidbody2D rb, ref bool shoulDash, Vector2 DashVector)
        {

            _canDashTimer -= Time.deltaTime;
            bool _canDash = _canDashTimer <= 0;
            _actiontimer -= Time.deltaTime;
            if (shoulDash)
            {
                shoulDash = false;
                if (_canDash)
                {
                    _canDashTimer = _timeBetweenDashes;
                    _actiontimer = _dashTime;
                    rb.AddForce(DashVector * _dashPower);
                }
            }

            if (_actiontimer > 0)
            {
                PlayerData._states = PlayerData.states.isDashing;
            }

            else if (_actiontimer == 0)
            {
                rb.AddForce(-rb.velocity, ForceMode2D.Impulse);
            }

            else if (_actiontimer < 0 && _actiontimer > -0.5f)
                PlayerData._states = PlayerData.states.normal;
        }
    }

}
