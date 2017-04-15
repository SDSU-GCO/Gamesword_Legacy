using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for all respawnable objects
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