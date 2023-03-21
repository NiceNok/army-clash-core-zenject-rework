using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.UI
{
    public class TopPanelView : MonoBehaviour
    {
        [SerializeField] private Slider myUnitsProgressBar;
        [SerializeField] private Slider enemyUnitsProgressBar;
    
        public TopPanelView(TopPanelView topPanelView)
        {
            myUnitsProgressBar = topPanelView.myUnitsProgressBar;
            enemyUnitsProgressBar = topPanelView.enemyUnitsProgressBar;
        }
    
        public void ChangeState(float myUnits, float enemyUnits)
        {
            myUnitsProgressBar.DOValue(myUnits, 0.2f).SetEase(Ease.InOutSine).SetAutoKill();
            enemyUnitsProgressBar.DOValue(enemyUnits, 0.2f).SetEase(Ease.InOutSine).SetAutoKill();
        }
    }
}
