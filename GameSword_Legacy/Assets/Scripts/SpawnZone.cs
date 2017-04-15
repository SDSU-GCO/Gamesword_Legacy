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
            foreach (Spawner s in spawners)
            {
                s.gameObject.SetActive(false);
            }
        }

        void OnTriggerEnter(Collider c)
        {
            if (c.gameObject.tag == "Player")
            {
                foreach (Spawner s in spawners)
                {
                    if (!started)
                    {
                        s.gameObject.SetActive(true);
                        started = true;
                    }
                    else
                    {
                        s.startSpawn();
                    }
                }
            }
        }
        void OnTriggerExit(Collider c)
        {
            if (c.gameObject.tag == "Player")
            {
                foreach (Spawner s in spawners)
                {
                    s.stopSpawn();
                }
            }
        }
    }

}
