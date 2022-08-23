using System;
using Project.Scripts.ScriptableObject.UnitAbilities;

namespace Project.Scripts.Core.Installers
{
    [Serializable]
    public class GameDependency
    {
        public readonly ObjectPool mySoldiersPool;
        public readonly ObjectPool enemySoldiersPool;
        public readonly ColorParameters[] colors;
        public readonly ShapeParameters[] shapes;
        public readonly SizeParameters[] size;

        public GameDependency(
            ObjectPool mySoldiersPool,
            ObjectPool enemySoldiersPool,
            ColorParameters[] colors,
            ShapeParameters[] shapes,
            SizeParameters[] size
            )
        {
            this.mySoldiersPool = mySoldiersPool;
            this.enemySoldiersPool = enemySoldiersPool;
            this.colors = colors;
            this.shapes = shapes;
            this.size = size;
        }
    }
}