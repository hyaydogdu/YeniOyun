using UnityEngine;
using UnityEngine.UI;

namespace Mygame
{
    [CreateAssetMenu(fileName = "CloseCombatGun", menuName = "Items/CloseC_Item", order = 0)]
    public class CloseC_Item : ScriptableObject
    {
        public string gunName;
        public float Range;
        public Sprite MenuSprite;
    }
}
