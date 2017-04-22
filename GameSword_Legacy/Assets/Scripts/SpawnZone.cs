using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS
{
    // Attach to object with a collider set as a trigger
    public class SpawnZone : MonoBehaviour
    {
        public Spawner[] spawners;
        bool started = false; // If spawning has started at least once

        void Start()
        {
            for (int i = 0; i < spawners.Length; i++)
            {
                spawners[i].gameObject.SetActive(false);
            }
        }

        void OnTriggerEnter(Collider c)
        {
            if (c.CompareTag(GameManager.playerTag))
            {
                for (int i = 0; i  < spawners.Length; i++)
                {
                    if (!started)
                    {
                        spawners[i].gameObject.SetActive(true);
                        started = true;
                    }
                    else
                    {
                        spawners[i].startSpawn();
                    }
                }
            }
        }
        void OnTriggerExit(Collider c)
        {
            if (c.CompareTag(GameManager.playerTag))
            {
                for (int i = 0; i < spawners.Length; i++)
                {
                    spawners[i].stopSpawn();
                }
            }
        }
    }

}
