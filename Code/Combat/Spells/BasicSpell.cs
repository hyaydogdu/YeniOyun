using System.Collections;
using UnityEngine;

namespace Mygame
{

    public class BasicSpell : MonoBehaviour
    {
        Rigidbody2D rb;
        private Vector2 _attackDir;
        public float _startForce;
        public string[] _explosionLayers;

        private void OnEnable()
        {
            rb = GetComponent<Rigidbody2D>();
            _attackDir = P_Combat.attackDir;
            if(PlayerData.Spell != null){
                rb.AddForce(_attackDir * PlayerData.Spell.forceToSpawn);
                StartCoroutine(DestroyWithTime(PlayerData.Spell.lifeTime));
            }
                
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                other.gameObject.GetComponent<E_ControllerBase>().TakeDamage(50);

            for (int i = 0; i < _explosionLayers.Length; i++)
            {
                if (other.gameObject.layer == LayerMask.NameToLayer(_explosionLayers[i]))
                    Destroy(this.gameObject);
            }
        }

        IEnumerator DestroyWithTime(float time)
        {
            yield return new WaitForSeconds(time);
            Destroy(this.gameObject);
        }
    }
}
