using Project.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Core.Installers
{
    public class BottomPanelInstaller : MonoInstaller
    {
        [SerializeField] private BottomPanelStateChanger bottomPanel;

        public override void InstallBindings()
        {
            BindBottomPanel();
        }

        private void BindBottomPanel()
        {
            Container
                .Bind<BottomPanelStateChanger>()
                .FromInstance(bottomPanel)
                .AsSingle();
        }
    }
}