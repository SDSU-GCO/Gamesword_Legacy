using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS
{
    enum DamageType { MELEE, MAGIC, SUPER_SPECIAL };
    public enum Element { NULL_DAMAGE, FIRE_DAMAGE, ICE_DAMAGE, LIGHTNING_DAMAGE };

    //wtf is the keyword! KEYWORD!!!!
    public class Enemy : PoolableObject
    {
        public float hp;
        public float knockBack;
        public float stunned;
        public string enemyName;

        public EnemyDefensesObject EnemyDamage;
        public EnemyDefensesObject EnemyKnockback;
        public EnemyDefensesObject EnemyHitStun;

        Vector3 originalPosition;
        Quaternion originalRotation;

        void awake()
        {
            originalPosition = transform.position;
            originalRotation = transform.rotation;
        }

        public override void reset()
        {
            gameObject.SetActive(true);
            transform.position = originalPosition;
            transform.rotation = originalRotation;
        }

        void takeDamage(DamageType damageType, float attackPower, float attackKnockback, float attackHitstun, Element element = Element.NULL_DAMAGE)
        {
            float res = 0f;
            float elemRes = 0f;
            EnemyDefensesObject TempEnemyStruct = EnemyDamage;
    
            for (int i = 0; i < 3; i++)
            {
                switch (i)
                {
                    case 1:
                        TempEnemyStruct = EnemyDamage;
                        break;

                    case 2:
                        TempEnemyStruct = EnemyKnockback;
                        break;

                    case 3:
                        TempEnemyStruct = EnemyHitStun;
                        break;
                }


                //attack type resistances;
                switch (damageType)
                {
                    case DamageType.MELEE:
                        res = TempEnemyStruct.meleeDef;
                        break;

                    case DamageType.MAGIC:
                        res = TempEnemyStruct.magicDef;
                        break;

                    case DamageType.SUPER_SPECIAL:
                        res = TempEnemyStruct.superSpecialDef;
                        break;

                    default:
                        Debug.Log("ERR, no damage type specified in \"takeDamage\" method.\n");
                        break;
                }

                //elemental resistances
                switch (element)
                {
                    case Element.NULL_DAMAGE:
                        elemRes = TempEnemyStruct.meleeDef;
                        break;

                    case Element.FIRE_DAMAGE:
                        elemRes = TempEnemyStruct.magicDef;
                        break;

                    case Element.ICE_DAMAGE:
                        elemRes = TempEnemyStruct.superSpecialDef;
                        break;

                    case Element.LIGHTNING_DAMAGE:
                        elemRes = TempEnemyStruct.superSpecialDef;
                        break;

                    default:
                        Debug.Log("ERR, no elemental attribute specified in \"takeDamage\" method.\n");
                        break;
                }

                switch (i)
                {
                    //dmg - ((res+elemRes)*dmg
                    case 1:
                        float dmg = attackPower - (res + elemRes) * attackPower;
                        if (Mathf.Abs(hp - dmg) > 0 && (hp - dmg) < System.Single.MaxValue)
                        {
                            hp = hp - dmg;
                        }
                        break;
                    case 2:
                        float tmpKB = attackKnockback - (res + elemRes) * attackKnockback;
                        if (Mathf.Abs(knockBack - tmpKB) > 0 && (knockBack - tmpKB) < System.Single.MaxValue)
                        {
                            knockBack = knockBack - tmpKB;
                        }
                        break;
                    case 3:
                        float tmpStun = attackHitstun - (res + elemRes) * attackHitstun;
                        if (Mathf.Abs(stunned - tmpStun) > 0 && (stunned - tmpStun) < System.Single.MaxValue)
                        {
                            stunned = stunned - tmpStun;
                        }
                        break;
                }//end switch
            }//end for
            Debug.Log(hp);
            Debug.Log(knockBack);
            Debug.Log(stunned);
            return;
        }

    }
}
