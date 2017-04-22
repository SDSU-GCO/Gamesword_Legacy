using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS
{
    public class GameManager : MonoBehaviour
    {
        static GameManager current;
        public const string playerTag = "Player";

        // Use this for initialization
        void Start()
        {
            if (current != null)
            {
                Destroy(this.gameObject);
                return;
            }
            current = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}