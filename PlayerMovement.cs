using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveForce;
    [SerializeField] private float _moveForceMultiplier;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _stopForce;

    private void FixedUpdate()
    {
        if (PlayerData._states == PlayerData.states.normal)
            Move(P_Controller._moveValue, P_Controller._rb, P_Controller._coll);
    }

    private void Move(float moveValue, Rigidbody2D rb, Collider2D coll)
    {
        float wantedVelocity = moveValue * _moveForce;
        Vector2 moveVector = new Vector2(wantedVelocity * _moveForceMultiplier, 0);
        // rb.velocity = moveVector;
        if (moveValue > 0 && rb.velocity.x < _maxSpeed)
        {
            rb.AddForce(moveVector * Time.fixedDeltaTime);
        }
        else if (moveValue < 0 && rb.velocity.x > -_maxSpeed)
        {
            rb.AddForce(moveVector * Time.fixedDeltaTime);
        }
        Vector2 oppositeForce = new Vector2((wantedVelocity - rb.velocity.x) * _stopForce, 0);
        if (rb.velocity.x > 0 && rb.velocity.x > wantedVelocity)
        {
            rb.AddForce(oppositeForce);
        }
        else if (rb.velocity.x < 0 && rb.velocity.x < wantedVelocity)
        {
            rb.AddForce(oppositeForce);
        }
    }
}
