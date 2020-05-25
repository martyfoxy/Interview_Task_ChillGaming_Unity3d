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
    public class MockPlayer : IPlayer
    {
        private float hp = 100f;
        private float armor = 0f;
        private float damage = 0f;
        private float vampirism = 0f;

        private List<Buff> playerBuffs;

        public void Attack()
        {
            
        }

        public void ChangeState(StatesEnum state)
        {
            
        }

        public void BeginPlay(List<Stat> startStats, List<Buff> startBuffs)
        {
            playerBuffs = new List<Buff>(startBuffs);

            hp = startStats.Find(x => x.title == GameConst.HPName).value;
            armor = startStats.Find(x => x.title == GameConst.ArmorName).value;
            damage = startStats.Find(x => x.title == GameConst.DamageName).value;
            vampirism = startStats.Find(x => x.title == GameConst.VampirismName).value;

            playerBuffs = new List<Buff>(startBuffs);
        }

        public void VampirismRestore(float damage)
        {

        }

        public float TakeDamage(float damage)
        {
            //Урон, гасящийся броней
            var resDamage = (100 - armor) / 100 * damage;
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
            return playerBuffs;
        }
    }
}
