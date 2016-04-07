#pragma warning disable 0108 // hiding inherited member properties

using System;
using UnityEngine;

namespace Assets.Scripts.PowerUps
{
    class Spread : Powerup
    {
        private Types _type = Types.Spread;
        public Types Type
        {
            get { return _type; }
        }

        private Color _color = Color.red;
    }
}
