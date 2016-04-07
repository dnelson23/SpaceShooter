using System;
using UnityEngine;

namespace Assets.Scripts.PowerUps
{
    [RequireComponent(typeof(SphereCollider))]
    [RequireComponent(typeof(MeshRenderer))]
    public class Powerup : MonoBehaviour
    {
        public enum Types { Blaster, Spread, Shield, Default }

        protected Types _type = Types.Default;

        protected Color _color = Color.white;

        public static Powerup GetRandomPowerup()
        {
            string typeName = ((Types)UnityEngine.Random.Range(1, 3)).ToString();

            var type = Type.GetType(typeName);
            var pUpType = Type.GetType(type.ToString());
            return (Powerup)Activator.CreateInstance(pUpType);
        }

        public bool IsDefault()
        {
            return _type == Types.Default ? true : false;
        }
    }
}
