using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GS
{
    [CreateAssetMenu()]
    public class EnemyDefensesObject : ScriptableObject
    {
        public float meleeDef;
        public float magicDef;
        public float superSpecialDef;
        public float fireDef;
        public float iceDef;
        public float electricDef;
        public float neutralDef;
    }
}