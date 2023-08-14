using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mygame
{
    public class Obstacles : MonoBehaviour
    {

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                PlayerData.TakeDamage(20);
                print(PlayerData._health);
            }

        }
    }
}

