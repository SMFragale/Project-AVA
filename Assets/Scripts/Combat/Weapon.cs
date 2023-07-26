using System.Collections;
using AVA.Core;
using AVA.Stats;
using UnityEngine;

namespace AVA.Combat
{
    public abstract class Weapon : MonoWaiter
    {
        [SerializeField]
        protected Transform characterTransform;

        CharacterStats statsController;


        [SerializeField]
        [Range(0, 10)]
        protected float baseAttackRate = 0.5f;
        protected bool isAttacking = true;

        public abstract void Attack(Vector3 direction);

        void Awake()
        {
            statsController = GetComponentInParent<CharacterStats>();
            dependencies = new System.Collections.Generic.List<IReadyCheck> { statsController };
        }
        protected override void OnDependenciesReady()
        {
            
        }

        public IEnumerator StartAttacking()
        {
            if(!isReady()) 
            {
                Debug.Log("Weapon in object" + gameObject.name + " is not ready to attack, waiting for dependencies");
                yield return null;
            }
            isAttacking = true;
            while (isAttacking)
            {
                Attack(characterTransform.forward.normalized);
                yield return new WaitForSeconds(baseAttackRate/statsController.GetStat(Stats.StatType.AttackSpeed));
            }
        }

        public void StopAttacking()
        {
            isAttacking = false;
        }
    }
}