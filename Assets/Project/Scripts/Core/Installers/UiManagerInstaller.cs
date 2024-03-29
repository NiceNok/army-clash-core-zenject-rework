using Project.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Core.Installers
{
    public class UiManagerInstaller : MonoInstaller
    {
        [SerializeField] private UIManager uiManager;
        public override void InstallBindings()
        {
            BindUIManager();
        }

        private void BindUIManager()
        {
            Container
                .Bind<UIManager>()
                .FromInstance(uiManager)
                .AsSingle();
        }
    }
}