using System.Collections.Generic;
using System.Linq;
using Project.Scripts.Core.Installers;
using Project.Scripts.ScriptableObject.UnitAbilities;
using Project.Scripts.UI;
using Project.Scripts.Units;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Project.Scripts
{
    public sealed class GameplayController : MonoBehaviour, IInitializable
    {
        private ObjectPool _mySoldiersPool;
        private ObjectPool _enemySoldiersPool;
        private ColorParameters[] _colors;
        private ShapeParameters[] _shapes;
        private SizeParameters[] _size;

        [Inject] private EndGamePanel _endGamePanel;
        [Inject] private TopPanelView _topPanelView;
        [Inject] private BottomPanelView _bottomPanelView;
        
        [Inject]
        public void Construct(GameDependency constr)
        {
            _mySoldiersPool = constr.MySoldiersPool;
            _enemySoldiersPool = constr.EnemySoldiersPool;
            _colors = constr.Colors;
            _shapes = constr.Shapes;
            _size = constr.Size;
        }

        
        private void Start()
        {
            Initialize();
        }

        [Header("Army Values (base)")] [Space(15)]
        public int myArmySize = 20;
        public int enemyArmySize = 20;
        public float distanceBetweenUnits = 1.6f;
        public int gridWidth = 5;

        private List<Unit> _mySoldiers;
        private List<Unit> _enemySoldiers;

        public static bool Battle;
        
        public void Initialize()
        {
            Debug.Log("Initialize gameplay");
            
            _mySoldiersPool.Initialize();
            _enemySoldiersPool.Initialize();
            _mySoldiers = new List<Unit>();
            _enemySoldiers = new List<Unit>();
            InitializeSoldiers();
        }
        
        private void InitializeSoldiers()
        {
            for (int i = 0; i < myArmySize; i++)
            {
                var obj = _mySoldiersPool.GetObject();
                var soldier = obj.GetComponent<MeleeSoldier>();
                _mySoldiers.Add(soldier);
            }

            for (int i = 0; i < enemyArmySize; i++)
            {
                var obj = _enemySoldiersPool.GetObject();
                var soldier = obj.GetComponent<MeleeSoldier>();
                _enemySoldiers.Add(soldier);
            }

            RandomizeSoldiers();
        }

        private void RandomizeSoldiers()
        {
            for (int i = 0; i < _mySoldiers.Count; i++)
            {
                var randomShape = _shapes[Random.Range(0, _shapes.Length)];
                var randomColor = _colors[Random.Range(0, _colors.Length)];
                var randomSize = _size[Random.Range(0, _size.Length)];
                SetGridPosition(_mySoldiers[i].transform, i);
                
                _mySoldiers[i].Init(randomShape, randomColor, randomSize);
                _mySoldiers[i].SetEnemies(_enemySoldiers);
                _mySoldiers[i].OnDeath += ReturnSoldierToPool;
            }

            for (int i = 0; i < _enemySoldiers.Count; i++)
            {
                var randomShape = _shapes[Random.Range(0, _shapes.Length)];
                var randomColor = _colors[Random.Range(0, _colors.Length)];
                var randomSize = _size[Random.Range(0, _size.Length)];
                SetGridPosition(_enemySoldiers[i].transform, i);
                _enemySoldiers[i].Init(randomShape, randomColor, randomSize);
                _enemySoldiers[i].SetEnemies(_mySoldiers);
                _enemySoldiers[i].SetEnemyView(randomColor.coloredMaterialEnemy);
                _enemySoldiers[i].OnDeath += ReturnEnemyToPool;

            }
        }

        private void ReturnSoldierToPool(GameObject obj)
        {
            _mySoldiersPool.ReturnObject(obj);
            CheckGameState();
        }

        private void ReturnEnemyToPool(GameObject obj)
        {
            _enemySoldiersPool.ReturnObject(obj);
            CheckGameState();
        }

        private void CheckGameState()
        {
            float aliveUnitsCount = _mySoldiers.Count(soldier => soldier.gameObject.activeInHierarchy);
            float aliveEnemyCount = _enemySoldiers.Count(soldier => soldier.gameObject.activeInHierarchy);
            
            _topPanelView.ChangeState(
                aliveUnitsCount/ _mySoldiers.Count, 
                aliveEnemyCount / _enemySoldiers.Count);

            if (aliveEnemyCount <= 0)
                EndGame(true);
            else if (aliveUnitsCount <= 0)
                EndGame(false);

        }

        private void EndGame(bool state)
        {
            Battle = false;
            _endGamePanel.OpenEndPanel(state);
        }

        public void RandomizeArmyButton()
        {
            foreach (var soldier in _mySoldiers)
                soldier.OnDeath -= ReturnEnemyToPool;

            foreach (var soldier in _enemySoldiers)
                soldier.OnDeath -= ReturnEnemyToPool;

            RandomizeSoldiers();
        }

        public void StartButton()
        {
            Battle = true;
            _bottomPanelView.EnableBottomPanel(false);
        }

        private void SetGridPosition(Transform tr, int id)
        {
            var z = id % gridWidth * distanceBetweenUnits;
            var x = id / gridWidth * distanceBetweenUnits;
            tr.localPosition = new Vector3(x, 0, z);
        }
    }
}
