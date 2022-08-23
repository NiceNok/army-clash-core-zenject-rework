using System.Collections.Generic;
using Project.Scripts.Core.Installers;
using Project.Scripts.ScriptableObject.UnitAbilities;
using Project.Scripts.UI;
using Project.Scripts.Units;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Project.Scripts
{
    public class GameManager : MonoBehaviour, IInitializable
    {
        private ObjectPool _mySoldiersPool;
        private ObjectPool _enemySoldiersPool;
        private ColorParameters[] _colors;
        private ShapeParameters[] _shapes;
        private SizeParameters[] _size;

        [Inject] private EndGamePanel _endGamePanel;
        [Inject] private TopPanelStateChanger _topPanelStateChanger;
        [Inject] private BottomPanelStateChanger _bottomPanelStateChanger;
        
        [Inject]
        public void Construct(GameDependency constr)
        {
            _mySoldiersPool = constr.mySoldiersPool;
            _enemySoldiersPool = constr.enemySoldiersPool;
            _colors = constr.colors;
            _shapes = constr.shapes;
            _size = constr.size;
        }

        
        private void Start()
        {
            Initialize();
        }

        [Header("Army Values (base)")] [Space(15)]
        public int MyArmySize = 20;
        public int EnemyArmySize = 20;
        public float DistanceBetweenUnits = 1.6f;
        public int GridWidth = 5;

        private List<Unit> _mySoldiers;
        private List<Unit> _enemySoldiers;

        public static bool Battle;
        
        public virtual void Initialize()
        {
            Debug.LogError("init");
            _mySoldiersPool.Initialize();
            _enemySoldiersPool.Initialize();
            _mySoldiers = new List<Unit>();
            _enemySoldiers = new List<Unit>();
            InitializeSoldiers();
        }
        
        private void InitializeSoldiers()
        {
            for (int i = 0; i < MyArmySize; i++)
            {
                var obj = _mySoldiersPool.GetObject();
                var soldier = obj.GetComponent<MeleeSoldier>();
                _mySoldiers.Add(soldier);
            }

            for (int i = 0; i < EnemyArmySize; i++)
            {
                var obj = _enemySoldiersPool.GetObject();
                var soldier = obj.GetComponent<MeleeSoldier>();
                _enemySoldiers.Add(soldier);
            }

            RandomizeSoldiers();
        }

        void RandomizeSoldiers()
        {
            for (int i = 0; i < _mySoldiers.Count; i++)
            {
                var randomShape = _shapes[Random.Range(0, _shapes.Length)];
                var randomColor = _colors[Random.Range(0, _colors.Length)];
                var randomSize = _size[Random.Range(0, _size.Length)];
                SetGridPosition(_mySoldiers[i].transform, i);
                _mySoldiers[i].Init(randomShape, randomColor, randomSize);
                _mySoldiers[i].SetEnemies(_enemySoldiers, Constants.EnemyTag);
                _mySoldiers[i].OnDeath += ReturnSoldierToPool;
            }

            for (int i = 0; i < _enemySoldiers.Count; i++)
            {
                var randomShape = _shapes[Random.Range(0, _shapes.Length)];
                var randomColor = _colors[Random.Range(0, _colors.Length)];
                var randomSize = _size[Random.Range(0, _size.Length)];
                SetGridPosition(_enemySoldiers[i].transform, i);
                _enemySoldiers[i].Init(randomShape, randomColor, randomSize);
                _enemySoldiers[i].SetEnemies(_mySoldiers, Constants.FriendlyTag);
                _enemySoldiers[i].SetEnemyView(randomColor.coloredMaterialEnemy);
                _enemySoldiers[i].OnDeath += ReturnEnemyToPool;

            }
        }

        void ReturnSoldierToPool(GameObject obj)
        {
            _mySoldiersPool.ReturnObject(obj);
            CheckGameState();
        }

        void ReturnEnemyToPool(GameObject obj)
        {
            _enemySoldiersPool.ReturnObject(obj);
            CheckGameState();
        }

        void CheckGameState()
        {
            float aliveUnitsCount = 0;
            float aliveEnemyCount = 0;
            foreach (var soldier in _mySoldiers)
                if (soldier.gameObject.activeInHierarchy)
                    aliveUnitsCount++;

            foreach (var soldier in _enemySoldiers)
                if (soldier.gameObject.activeInHierarchy)
                    aliveEnemyCount++;


            
            _topPanelStateChanger.СhangeState(
                aliveUnitsCount/ _mySoldiers.Count, 
                aliveEnemyCount / _enemySoldiers.Count);
                

            if (aliveEnemyCount <= 0)
                EndGame(true);
            else if (aliveUnitsCount <= 0)
                EndGame(false);

        }

        void EndGame(bool state)
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
            _bottomPanelStateChanger.EnableBottomPanel(false);
        }

        void SetGridPosition(Transform tr, int id)
        {
            var z = id % GridWidth * DistanceBetweenUnits;
            var x = id / GridWidth * DistanceBetweenUnits;
            tr.localPosition = new Vector3(x, 0, z);
        }
    }
}
