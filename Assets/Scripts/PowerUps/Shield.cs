#pragma warning disable 0108 // hiding inherited member properties

using System;
using UnityEngine;

namespace Assets.Scripts.PowerUps
{
    class Shield : Powerup
    {
        private Types _type = Types.Shield;
        public Types Type
        {
            get { return _type; }
        }

        private Color _color = Color.blue;

        float maxShieldHealth = 100f;
        float curShieldHealth;
    }
}
