using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS
{
    // Class for player health, ECC, potion cooldown
    public class PlayerStats : MonoBehaviour
    {
        public int maxHealth;
        int health;
        public int maxECC;
        int ECC;
        public float cooldownTime = 20f;
        float timer = 0;
        public Element[] currentElements;
        // Use this for initialization
        void Start()
        {
            health = maxHealth;
        }

        // Update is called once per frame
        void Update()
        {
            timer -= Time.deltaTime;
            if (Input.GetButtonDown("Fire3"))
            {
                if (timer < 0f)
                {
                    usePotion();
                }
                else
                {
                    Debug.Log(timer);
                }
            }
        }

        void usePotion()
        {
            Debug.Log("Potion used");
            timer = cooldownTime;
        }
    }
}
