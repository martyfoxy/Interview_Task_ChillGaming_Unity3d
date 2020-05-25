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
        public float HP = 100f;

        public void Attack()
        {
            
        }

        public void ChangeState(StatesEnum state)
        {
            
        }

        public Animator GetAnimator()
        {
            return new Animator();
        }

        public float GetHP()
        {
            return HP;
        }

        public void TakeDamage(float damage)
        {
            
        }

        public void SetDefault(List<Stat> startStats, List<Buff> startBuffs)
        {

        }

        public float GetDamage()
        {
            throw new NotImplementedException();
        }

        public float GetArmor()
        {
            throw new NotImplementedException();
        }

        public float GetVampirism()
        {
            throw new NotImplementedException();
        }

        float ICharacter.TakeDamage(float damage)
        {
            throw new NotImplementedException();
        }

        public void VampirismRestore(float damage)
        {
            throw new NotImplementedException();
        }

        public List<Stat> GetStats()
        {
            throw new NotImplementedException();
        }

        public List<Buff> GetBuffs()
        {
            throw new NotImplementedException();
        }
    }
}
