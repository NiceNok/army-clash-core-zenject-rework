using System.Linq;
using Project.Scripts.ScriptableObject.UnitAbilities;
using UnityEngine;
using UnityEngine.Assertions;

namespace Project.Scripts.Units
{
    public class MeleeSoldier : Unit
    {
        private float _meleeRangeDistance = 2f;
        [SerializeField] private ObjectPool unitHealthFXPool;

        private void Awake()
        {
            Assert.IsNotNull(unitHealthFXPool);
            
            unitHealthFXPool.Initialize();
        }

        private Unit FindClosestEnemy()
        {
            Vector3 currentPos = transform.position;
            
            return enemies.Where(unit => unit.isActiveAndEnabled)
                .OrderBy(unit => Vector3.Distance(unit.transform.position, currentPos))
                .FirstOrDefault();
        }

        /// <summary>
        /// returns Unit with lowest HP
        /// </summary>
        /// <returns></returns>
        private Unit FindPoorEnemy()
        {
            if(IsMeleeDistance()) return currentEnemy;

            return enemies.Where(unit => unit.isActiveAndEnabled)
                .OrderBy(unit => unit.HealthPoints)
                .FirstOrDefault();
        }

        public override void GetDamage(double damage)
        {
            ChangeHealth(-damage);
            ShowHealthChange(damage);
            currentEnemy = FindClosestEnemy();
            if (IsDied) PerformDeath();
        }

        private void ShowHealthChange(double damage)
        {
            var obj = unitHealthFXPool.GetObject();
            var hp = obj.GetComponent<UnitHealthChangeView>();
            hp.SetText($"-{(int)damage}");
            hp.OnFinish += ReturnHealthViewObject;
        }

        /// <summary>
        /// return object to pool
        /// </summary>
        /// <param name="hp"></param>
        private void ReturnHealthViewObject(UnitHealthChangeView hp)
        {
            unitHealthFXPool.ReturnObject(hp.gameObject);
            hp.OnFinish -= ReturnHealthViewObject;
        }

        protected override void MoveToEnemy()
        {
            currentEnemy = Type == UnitShape.Cube ? FindClosestEnemy() : FindPoorEnemy();
            if (currentEnemy == null || isAttack || IsMeleeDistance()) return;
            var step = MovementSpeed / 2 * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, currentEnemy.transform.position, (float)step);
        }

        private bool IsMeleeDistance()
        {
            if (!currentEnemy) return false;
            var dist = Vector3.Distance(currentEnemy.transform.position, transform.position);
            if (dist > _meleeRangeDistance) return false;
            return true;
        }

        public override void PerformAttack()
        {
            if (!IsMeleeDistance()) return;
            currentEnemy.GetDamage(AttackPoints);
        }

        private new void PerformDeath()
        {
            CancelInvoke("PerformAttack");
            base.PerformDeath();
        }
    }
}