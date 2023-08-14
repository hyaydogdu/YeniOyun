using UnityEngine;

namespace Mygame
{
    public class E_LittleController : E_ControllerBase
    {
        [Header("Raycast Propeties")]
        public Transform _eyeOrigin;
        public Vector2 _castSize;
        public LayerMask _layerMask;

        [Header("Movement Properties")]
        [SerializeField] private float _speed = 10;
        [SerializeField] private Transform _player;
        [SerializeField] private float smoothTime;

        private bool _playerInRange;
        private bool _playerInFightRange;

        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            ChaseAndHitPlayer();
        }

        private void ChaseAndHitPlayer()
        {
            RaycastHit2D raycastHit2D = Physics2D.BoxCast(_eyeOrigin.position, _castSize, 0, Vector2.left, 0, _layerMask);
            if (raycastHit2D)
                if (raycastHit2D.transform.CompareTag("Player"))
                {
                    if (Vector2.Distance(transform.position, _player.transform.position) > 1f)
                    {
                        float _moveSpeed = Mathf.Lerp(0, _speed, smoothTime);
                        rb.gravityScale = 1;
                        transform.position = Vector2.MoveTowards(transform.position, _player.position, _moveSpeed * Time.deltaTime);
                    }

                    else
                    {
                        PlayerData.TakeDamage(20);
                        Die();
                    }
                }
        }
    }
}

