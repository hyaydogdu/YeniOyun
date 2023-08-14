using System;
using System.Collections;
using UnityEngine;

namespace Mygame
{
    public abstract class E_ControllerBase : MonoBehaviour
    {
        [Header("Health Properties")]
        public int _maxHealth;
        public static int _health;

        [Header("Detection Properties")]
        public float radius;
        [Range(0, 360)]
        public float angle;
        [HideInInspector] public GameObject playerRef;

        public LayerMask targetMask;
        public LayerMask obstructionMask;

        public bool canSeePlayer;

        public enum States
        {
            normal,
            suspecious,
            chasing,
            fighting
        }
        public static States _states;

        #region Built In Methods
        protected virtual void Start()
        {
            _states = States.normal;
            _health = _maxHealth;
            playerRef = GameObject.FindGameObjectWithTag("Player");
            StartCoroutine(FOVRoutine());
        }
        #endregion

        #region Health Methods
        public void TakeDamage(int damagePoint)
        {
            if (_health - damagePoint > 0)
            {
                _health -= damagePoint;
            }
            else
            {
                Die();
            }
        }

        public void Heal(int healPoint)
        {
            if (_health + healPoint <= _maxHealth)
            {
                _health += healPoint;
            }
            else if (_health + healPoint > _maxHealth)
            {
                _health = _maxHealth;
            }
        }

        public void ResetHealth()
        {
            _health = _maxHealth;
        }

        protected virtual void Die()
        {
            Debug.Log("Enemy died");
            Destroy(this.gameObject);
        }
        #endregion

        #region Detection Methods
        private IEnumerator FOVRoutine()
        {
            WaitForSeconds wait = new WaitForSeconds(0.2f);

            while (true)
            {
                yield return wait;
                FieldOfViewCheck();
            }
        }

        private void FieldOfViewCheck()
        {
            Collider2D[] rangeChecks = Physics2D.OverlapCircleAll(transform.position, radius, targetMask);

            if (rangeChecks.Length != 0)
            {
                Transform target = rangeChecks[0].transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;

                if (Vector2.Angle(transform.right, directionToTarget) < angle / 2)
                {
                    float distanceToTarget = Vector2.Distance(transform.position, target.position);

                    if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                        canSeePlayer = true;
                    else
                        canSeePlayer = false;
                }
                else
                    canSeePlayer = false;
            }
            else if (canSeePlayer)
                canSeePlayer = false;
        }
        #endregion
    }

}
