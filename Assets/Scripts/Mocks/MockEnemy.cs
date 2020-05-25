using Assets.Scripts.Interface;
using Assets.Scripts.Models;
using Assets.Scripts.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Mocks
{
    public class MockEnemy : IEnemy
    {
        public float HP = 100f;
        public float Armor = 0f;
        public float Damage = 0f;
        public float Vampirism = 0f;

        public List<Stat> DefaultStats;
        public List<Buff> EnemyBuffs;

        public void Attack()
        {

        }

        public void ChangeState(StatesEnum state)
        {

        }

        public void BeginPlay(List<Stat> startStats, List<Buff> startBuffs)
        {
            DefaultStats = new List<Stat>(startStats);
            EnemyBuffs = new List<Buff>(startBuffs);
        }

        public void VampirismRestore(float damage)
        {

        }

        public float TakeDamage(float damage)
        {
            //Урон, гасящийся броней
            var resDamage = (100 - Armor) / 100 * damage;
            return resDamage;
        }

        public Animator GetAnimator()
        {
            return new Animator();
        }

        public float GetHP()
        {
            return HP;
        }

        public float GetDamage()
        {
            return Damage;
        }

        public float GetArmor()
        {
            return Armor;
        }

        public float GetVampirism()
        {
            return Vampirism;
        }

        public List<Buff> GetBuffs()
        {
            return EnemyBuffs;
        }
    }
}
