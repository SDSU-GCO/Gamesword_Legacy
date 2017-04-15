using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS
{
    public class PoolableObject : MonoBehaviour
    {
        public int objectNum;

        void Start()
        {

        }

        public virtual void reset()
        {
            gameObject.SetActive(true);
        }
    }
}