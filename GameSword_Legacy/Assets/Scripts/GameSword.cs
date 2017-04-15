using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS
{
    public class GameSword : MonoBehaviour
    {
        PlayerStats stats;
        Collider col;
        // Element currentElement;

        // Use this for initialization
        void Start()
        {
            stats = GetComponentInParent<PlayerStats>();
            col = GetComponent<Collider>();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
