using UnityEngine;

namespace Project.Scripts.ScriptableObject.UnitAbilities
{
    [CreateAssetMenu(fileName = "Unit Color SO", menuName = "Unit Color SO", order = 52)]
    public class ColorParameters : UnityEngine.ScriptableObject
    {
        public UnitColor color;
        public Material coloredMaterial;
        public Material coloredMaterialEnemy;
        public double attackPoints;
        public double healthPoints;
        public double movementSpeed;
        public double attackSpeed;
    }

    public enum UnitColor
    {
        Blue = 0,
        Green = 1,
        Red = 2
    }
}