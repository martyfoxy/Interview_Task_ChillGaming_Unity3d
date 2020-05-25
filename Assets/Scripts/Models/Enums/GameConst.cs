namespace Assets.Scripts.Models.Enums
{
    /// <summary>
    /// Константы используемые в игре
    /// </summary>
    public static class GameConst
    {
        #region Имена файлов
        /// <summary>
        /// Относительный(от Resources) путь к текстовому файлу с настройками для начала игры
        /// </summary>
        public const string StartUpSettingsFile = "SettingsFiles/data";
        #endregion

        #region Имена характеристик
        public const string HPName = "жизнь";
        public const string ArmorName = "броня";
        public const string DamageName = "урон";
        public const string VampirismName = "вампиризм";
        #endregion

        #region Имена в иерархии префабов
        public const string IconName = "Icon";
        #endregion

        #region Имена анимаций
        public const string IdleAnimationName = "basePlayer_idle";
        public const string AttackAnimationName = "basePlayer_attack";
        public const string DeathAnimationName = "basePlayer_death";
        #endregion

        #region Имена параметров анимаций
        public const string AttackParameter = "Attack";
        public const string HealthParameter = "Health";
        #endregion
    }
}
