using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Mygame
{
    public class SpellLoot : MonoBehaviour
    {
        SpriteRenderer spriteRenderer;
        BoxCollider2D boxCollider;

        [SerializeField] Spell_Item spell_Item;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            boxCollider = GetComponent<BoxCollider2D>();
        }

        void Start()
        {
            spriteRenderer.sprite = spell_Item.MenuSprite;
        }

        void OnTriggerStay2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                if (P_Controller.Interaction)
                {
                    print("Getit");
                    P_Controller.Interaction = false;
                    PlayerData.Spell = spell_Item;
                    Destroy(this.gameObject);
                }
            }
        }
    }
}