using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace JesusMayCry
{
    public class EnemyLocomotionManager : MonoBehaviour
    {
        EnemyManager enemyManager;

        public CharacterStats currentTarget;
        public LayerMask detectionLayer;
        private CapsuleCollider capsule;
        private Animator anim;

        private void Awake()
        {
            enemyManager = GetComponent<EnemyManager>();
            capsule = GetComponent<CapsuleCollider>();
            anim = GetComponent<Animator>();
        }

        void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.CompareTag("Sword"))
            {
                Debug.Log("HIT!");
                anim.SetBool("gotDamaged", true);
                //Object.Destroy(this.gameObject);
            }
        }
        public void HandleDetection()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, detectionLayer);

            for (int i = 0; i < colliders.Length; i++)
            {
                CharacterStats characterStats = colliders[i].transform.GetComponent<CharacterStats>();

                if(characterStats != null)
                {
                    Vector3 targetDirection = characterStats.transform.position - transform.position;
                    float viewableAngle = Vector3.Angle(targetDirection, transform.forward);
                    if(viewableAngle > enemyManager.minimumDetectionAngle && viewableAngle < enemyManager.maximumDetectionAngle)
                    {
                        currentTarget = characterStats;
                    }
                }
            }
        }
    }
}
