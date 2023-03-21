using Project.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Core.Installers
{
    public class BottomPanelInstaller : MonoInstaller
    {
        [SerializeField] private BottomPanelView bottomPanel;

        public override void InstallBindings()
        {
            BindBottomPanel();
        }

        private void BindBottomPanel()
        {
            Container
                .Bind<BottomPanelView>()
                .FromInstance(bottomPanel)
                .AsSingle();
        }
    }
}