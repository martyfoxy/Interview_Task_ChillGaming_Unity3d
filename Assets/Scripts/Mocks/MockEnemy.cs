using Assets.Scripts.Interface;
using Assets.Scripts.Models;
using Assets.Scripts.Models.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Mocks
{
    public class MockEnemy : IEnemy
    {
        private float hp = 100f;
        private float armor = 25f;
        private float damage = 25f;
        private float vampirism = 0f;

        private List<Buff> _buffs;

        public void Attack()
        {
            
        }

        public void ChangeState(StatesEnum state)
        {

        }

        public void BeginPlay(List<Stat> startStats, List<Buff> startBuffs)
        {
            hp = startStats.Find(x => x.id == GameConst.HealthId).value;
            armor = startStats.Find(x => x.id == GameConst.ArmorId).value;
            damage = startStats.Find(x => x.id == GameConst.DamageId).value;
            vampirism = startStats.Find(x => x.id == GameConst.VampirismId).value;

            _buffs = new List<Buff>(startBuffs);
        }

        public void VampirismRestore(float damage)
        {

        }

        public float TakeDamage(float damage)
        {
            //Урон, гасящийся броней
            var resDamage = (100 - armor) / 100 * damage;

            hp -= resDamage;

            return resDamage;
        }

        public Animator GetAnimator()
        {
            return new Animator();
        }

        public float GetHP()
        {
            return hp;
        }

        public float GetDamage()
        {
            return damage;
        }

        public float GetArmor()
        {
            return armor;
        }

        public float GetVampirism()
        {
            return vampirism;
        }

        public List<Buff> GetBuffs()
        {
            return _buffs;
        }
    }
}
