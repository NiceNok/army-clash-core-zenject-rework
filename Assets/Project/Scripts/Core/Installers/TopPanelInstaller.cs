using Project.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Core.Installers
{
    public class TopPanelInstaller : MonoInstaller
    {
        [SerializeField] private TopPanelStateChanger _topPanelStateChanger;

        public override void InstallBindings()
        {
            BindTopPanel();
        }

        private void BindTopPanel()
        {
            Container
                .Bind<TopPanelStateChanger>()
                .FromInstance(_topPanelStateChanger)
                .AsSingle();
        }
    }
}