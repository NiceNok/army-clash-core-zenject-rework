using Project.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Core.Installers
{
    public class TopPanelInstaller : MonoInstaller
    {
        [SerializeField] private TopPanelView topPanelView;

        public override void InstallBindings()
        {
            BindTopPanel();
        }

        private void BindTopPanel()
        {
            Container
                .Bind<TopPanelView>()
                .FromInstance(topPanelView)
                .AsSingle();
        }
    }
}