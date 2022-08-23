using System;
using Project.Scripts.ScriptableObject.UnitAbilities;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Core.Installers
{
    public class GameInstaller: MonoInstaller
    {
        [SerializeField] private Settings _settings;
        [SerializeField] private GameManager _gameManager;
        
        public override void InstallBindings()
        {      
            BindSettings();
            BindGameManager();
        }

        private void BindSettings()
        {
            Container
                .Bind<GameDependency>()
                .AsSingle()
                .WithArguments(
                    _settings.mySoldiersPool,
                    _settings.enemySoldiersPool,
                    _settings.colors,
                    _settings.shapes,
                    _settings.size
                    );
        }
        
        private void BindGameManager()
        {
            Container.Bind(typeof(GameManager))
                .FromInstance(_gameManager)
                .AsSingle();
        }
        [Serializable]
        private class Settings
        {
            public ObjectPool mySoldiersPool;
            public ObjectPool enemySoldiersPool;
            public ColorParameters[] colors;
            public ShapeParameters[] shapes;
            public SizeParameters[] size;
        }
    }
}