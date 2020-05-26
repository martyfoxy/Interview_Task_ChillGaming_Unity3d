using Assets.Scripts.Interface;

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
