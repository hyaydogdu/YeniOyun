using UnityEngine;


namespace Mygame
{
    public class PlayerData : MonoBehaviour
    {
        [Header("Health Properties")]
        public static int _maxHealth = 100;
        public static int _health;

        [Header("Inventory Properties")]
        public static CloseC_Item closeCombatGun;
        public static Spell_Item Spell;

        #region States
        public enum states
        {
            normal,
            isDashing,
            isClimbing
        }
        public static states _states;
        #endregion

        #region BuiltIn Methods
        private void Start()
        {
            _states = states.normal;
            _health = _maxHealth;
        }
        #endregion

        #region Health Methods
        public static void TakeDamage(int damagePoint)
        {
            if (_health - damagePoint > 0)
            {
                _health -= damagePoint;
            }
            if (_health - damagePoint <= 0)
            {
                Die();
            }
        }

        public static void Heal(int healPoint)
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

        public static void ResetHealth()
        {
            _health = _maxHealth;
        }

        private static void Die()
        {
            print("die");
        }
        #endregion
    }

}
