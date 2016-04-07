using System;
using UnityEngine;

namespace Assets.Scripts.PowerUps
{
    [RequireComponent(typeof(SphereCollider))]
    [RequireComponent(typeof(MeshRenderer))]
    public class Powerup
    {
        public enum Types { Blaster, Spread, Shield, Default }

        protected Types _type = Types.Default;

        protected Color _color = Color.white;

        public static Types GetRandomPowerupType()
        {
            return (Types)UnityEngine.Random.Range(1, 3);
        }

        public static Powerup InitPowerupByType(Types newPowerup)
        {
            var type = Type.GetType(newPowerup.ToString());
            var pUpType = Type.GetType(type.ToString());
            return (Powerup)Activator.CreateInstance(pUpType);
        }

        public bool IsDefault()
        {
            return _type == Types.Default ? true : false;
        }
    }
}
