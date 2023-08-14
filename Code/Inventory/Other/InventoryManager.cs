using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace Mygame
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] List<CloseC_Item> CloseC_Items = new List<CloseC_Item>();
        [SerializeField] List<Spell_Item> Spell_Items = new List<Spell_Item>();

        [SerializeField] GameObject CloseGunLayout;
        [SerializeField] GameObject SpellLayout;

        //sprites for current items
        [SerializeField] Image CloseGunImage;
        [SerializeField] Image SpellImage;

        private void Update()
        {
            PlayerData.closeCombatGun = CloseC_Items[0];
            PlayerData.Spell = Spell_Items[0];

            InventoryUI();
        }

        private void InventoryUI()
        {
            CloseGunImage.sprite = CloseC_Items[0].MenuSprite;
            SpellImage.sprite = Spell_Items[0].MenuSprite;
            #region Close Range Inventory
            Image[] closeGunImages = CloseGunLayout.GetComponentsInChildren<Image>();
            for (int i = 0; i < CloseC_Items.Capacity - 1; i++)
            {
                closeGunImages[i].sprite = CloseC_Items[i + 1].MenuSprite;
            }
            #endregion

            #region Spell Inventory
            Image[] spellImages = SpellLayout.GetComponentsInChildren<Image>();
            for (int i = 0; i < Spell_Items.Capacity - 1; i++)
            {
                spellImages[i].sprite = Spell_Items[i + 1].MenuSprite;
            }
            #endregion
        }
    }
}

