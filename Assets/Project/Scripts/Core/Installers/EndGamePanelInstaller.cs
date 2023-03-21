using System;
using Project.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Core.Installers
{
    public class EndGamePanelInstaller : MonoInstaller
    {
        [SerializeField] private Settings settings;
        public override void InstallBindings()
        {
            BindEndGamePanel();
        }

        private void BindEndGamePanel()
        {
            Container
                .Bind<EndGamePanel>()
                .FromInstance(settings.instance)
                .AsSingle();
        }

        [Serializable]
        private class Settings
        {
            public EndGamePanel instance;
        }
    }

    
}