using UnityEngine;
using Cinemachine;

namespace Mygame
{
    public class PlayerAnim : MonoBehaviour
    {
        [Header("Particle Effects")]
        [SerializeField] ParticleSystem _particalSystem;

        private void FixedUpdate()
        {
            ParticleEffects(P_Controller._rb, P_Controller._isGrounded);
        }

        private void ParticleEffects(Rigidbody2D rb, bool isGrounded)
        {
            _particalSystem.Play();
            float xVel = rb.velocity.x;
            xVel = Mathf.Abs(xVel);
            var emission = _particalSystem.emission;

            if (isGrounded && PlayerData._states == PlayerData.states.normal && xVel > 5f)
            {
                emission.enabled = true;
            }
            else
            {
                emission.enabled = false;
            }
        }
    }
}

