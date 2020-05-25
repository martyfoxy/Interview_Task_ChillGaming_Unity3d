using Assets.Scripts.ScriptableObjects;
using UnityEngine;
using Zenject;

/// <summary>
/// Установщик для связывания данных на json со скриптовым объектом
/// </summary>
[CreateAssetMenu(fileName = "GameSettings", menuName = "Installers/GameSettingsInstaller")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    public GameSettings GameSettings;

    public override void InstallBindings()
    {
        Container.BindInstance(GameSettings);
    }
}