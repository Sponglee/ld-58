using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Scriptable Objects/GameSettingsInstaller")]
public class GameSettingsInstaller : ScriptableObjectInstaller
{
    [SerializeField] private CameraPreset _cameraSettingsPreset;
    [SerializeField] private AudioPreset _audioSettings;
    [SerializeField] private GamePreset _gameSettings;
    [SerializeField] private UpgradePreset _upgradeSettings;

    public override void InstallBindings()
    {
        Container.Bind<CameraPreset>().FromInstance(_cameraSettingsPreset).AsSingle();
        Container.Bind<AudioPreset>().FromInstance(_audioSettings).AsSingle();
        Container.Bind<GamePreset>().FromInstance(_gameSettings).AsSingle();
        Container.Bind<UpgradePreset>().FromInstance(_upgradeSettings).AsSingle();
    }
}