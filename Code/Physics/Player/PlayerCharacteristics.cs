using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mygame
{
    public class PlayerCharacteristics : MonoBehaviour
    {
        [SerializeField] private float fallMultiplayer;
        [SerializeField] private float DragInAir;

        private void FixedUpdate()
        {
            updatePhysics(P_Controller._rb, P_Controller._isGrounded);
        }

        private void updatePhysics(Rigidbody2D rb, bool IsGrounded)
        {
            if (rb.velocity.y < 0 && PlayerData._states == PlayerData.states.normal)
            {
                rb.gravityScale = fallMultiplayer;
            }
            else
            {
                rb.gravityScale = 1f;
            }

            if (!IsGrounded)
            {
                rb.drag = DragInAir;
            }
            else
            {
                rb.drag = 0;
            }
        }
    }

}
