using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Simple test for doing damage
namespace GS
{
    public class GameSword : MonoBehaviour
    {
        PlayerStats stats;

        void Start()
        {
            stats = GetComponentInParent<PlayerStats>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.GetComponent<Enemy>().takeDamage(DamageType.MELEE, 10f, 10f, 10f);
            }
        }
    }
}
