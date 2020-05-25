using Assets.Scripts.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Signals
{
    /// <summary>
    /// Сигнал об изменении характеристики персонажа
    /// </summary>
    public class CharacterStatChangedSignal
    {
        public ICharacter character;
        public int StatId;
        public float Value;
    }
}
