using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS
{
    public class Spawner : MonoBehaviour
    {
        public GameObject[] objects;
        public float delay; // Time between when the spanwer becomes active and when enemies start spawning
        public float interval; // Time between spawns
        public bool limitSpawns; // If the spawns are limited
        public int spawnLimit; // Maximum number of spawns
        protected bool stop; // Stop spawning
        protected int numSpawned;

        void Start()
        {
            stop = false;
            numSpawned = 0;
        }

        void OnEnable()
        {
            StartCoroutine(spawn());
        }

        public virtual IEnumerator spawn()
        {
            yield return new WaitForSeconds(delay);

            while (!stop)
            {
                numSpawned++;
                if (limitSpawns && numSpawned > spawnLimit)
                {
                    stop = true;
                    continue;
                }
                int enemy = (int)Random.Range(0, objects.Length);
                Instantiate(objects[enemy], transform.position, transform.rotation);
                yield return new WaitForSeconds(interval);
            }
        }

        public void stopSpawn() // Stops spawning based on whether or not spawner is an unlimited spawner
        {
            StopAllCoroutines();
        }

        public void startSpawn() // Restarts spawning based on whether or not spawner is an unlimited spawner 
        {
            StartCoroutine(spawn());
        }

    }

}
