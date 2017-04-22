using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS
{
    public class PoolSpawner : Spawner
    {
        List<PoolableObject> pooledObjects = new List<PoolableObject>();
        int lastfound = 0;

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
                PoolableObject p = find(num);

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

        PoolableObject find(int num)
        {
            for (int i = lastfound; i < pooledObjects.Count; i++)
            {
                if (pooledObjects[i].gameObject.activeSelf && pooledObjects[i].objectNum == num)
                {
                    return pooledObjects[i];
                }
            }
            for (int i = lastfound-1; i >= 0; i--)
            {
                if (pooledObjects[i].gameObject.activeSelf && pooledObjects[i].objectNum == num)
                {
                    return pooledObjects[i];
                }
            }
            return null;
        }
    }
}
