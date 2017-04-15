using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS
{
    public class PoolSpawner : Spawner
    {
        List<PoolableObject> pooledObjects = new List<PoolableObject>();

        public override IEnumerator spawn()
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

                int num = (int)Random.Range(0, objects.Length);
                PoolableObject p = pooledObjects.Find(x => !x.gameObject.activeSelf && x.objectNum == num);

                if (p == null)
                {
                    p = Instantiate(objects[num], transform.position, transform.rotation).GetComponent<PoolableObject>();
                    p.objectNum = num;
                    pooledObjects.Add(p);
                }
                p.reset();
                yield return new WaitForSeconds(interval);
            }
        }
    }
}
