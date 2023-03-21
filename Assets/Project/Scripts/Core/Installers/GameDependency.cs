using System;
using Project.Scripts.ScriptableObject.UnitAbilities;

namespace Project.Scripts.Core.Installers
{
    [Serializable]
    public class GameDependency
    {
        public readonly ObjectPool MySoldiersPool;
        public readonly ObjectPool EnemySoldiersPool;
        public readonly ColorParameters[] Colors;
        public readonly ShapeParameters[] Shapes;
        public readonly SizeParameters[] Size;

        public GameDependency(
            ObjectPool mySoldiersPool,
            ObjectPool enemySoldiersPool,
            ColorParameters[] colors,
            ShapeParameters[] shapes,
            SizeParameters[] size
            )
        {
            MySoldiersPool = mySoldiersPool;
            EnemySoldiersPool = enemySoldiersPool;
            Colors = colors;
            Shapes = shapes;
            Size = size;
        }
    }
}