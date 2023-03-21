using System;
using Project.Scripts.ScriptableObject.UnitAbilities;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Core.Installers
{
    public class GameInstaller: MonoInstaller
    {
        [SerializeField] private Settings settings;
        [SerializeField] private GameplayController gameplayController;
        
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
                    settings.mySoldiersPool,
                    settings.enemySoldiersPool,
                    settings.colors,
                    settings.shapes,
                    settings.size
                    );
        }
        
        private void BindGameManager()
        {
            Container.Bind(typeof(GameplayController))
                .FromInstance(gameplayController)
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