using Assets.Scripts.Interface;

namespace Assets.Scripts.Signals
{
    /// <summary>
    /// Сигнал об окончании игры
    /// </summary>
    public class EndOfGameSignal
    {
        public ICharacter looser;
    }
}
