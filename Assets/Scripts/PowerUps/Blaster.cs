#pragma warning disable 0108 // hiding inherited member properties

using System;
using UnityEngine;

namespace Assets.Scripts.PowerUps
{
    class Blaster : Powerup
    {
        private Types _type = Types.Blaster;
        public Types Type
        {
            get { return _type; }
        }

        private Color _color = Color.green;
    }
}
