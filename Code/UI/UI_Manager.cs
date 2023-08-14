using TMPro;
using UnityEngine;

namespace Mygame
{
    public class UI_Manager : MonoBehaviour
    {
        [Header("health UI")]
        public TextMeshProUGUI _playerHealthText;
        string _healthText;
        // [Header("Inventory UI")]
        // public GameObject inventory;

        void Update()
        {
            // InventoryOpenClose();
            UI_Texts();
        }

        private void UI_Texts()
        {
            _healthText = "Player Health : " + PlayerData._health;
            _playerHealthText.text = _healthText;
        }

        // private void InventoryOpenClose()
        // {
        //     if (P_Controller._inventoryOpened)
        //     {
        //         inventory.SetActive(true);
        //         Time.timeScale = 0;
        //     }
        //     else
        //     {
        //         inventory.SetActive(false);
        //         Time.timeScale = 1;
        //     }
        // }
    }
}

