using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.UI
{
    public class TopPanelStateChanger : MonoBehaviour
    {
        [SerializeField] private Slider _myUnitsProgressBar;
        [SerializeField] private Slider _enemyUnitsProgressBar;
    
        public  TopPanelStateChanger(TopPanelStateChanger topPanelStateChanger)
        {
            _myUnitsProgressBar = topPanelStateChanger._myUnitsProgressBar;
            _enemyUnitsProgressBar = topPanelStateChanger._enemyUnitsProgressBar;
        }
    
        public void СhangeState(float myUnits, float enemyUnits)
        {
            _myUnitsProgressBar.value = myUnits;
            _enemyUnitsProgressBar.value = enemyUnits;
        }
    }
}
