using UnityEngine;

namespace Mygame
{
    [CreateAssetMenu(fileName = "Spell_Item", menuName = "Items/Spell_Item", order = 1)]
    public class Spell_Item : ScriptableObject
    {
        public string spellName;
        public float forceToSpawn;
        public float lifeTime;
        public Sprite MenuSprite;
        public GameObject spellPrefab;
    }
}
