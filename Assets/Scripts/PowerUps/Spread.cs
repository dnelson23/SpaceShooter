using System;
using UnityEngine;

namespace Assets.Scripts.PowerUps
{
    class Spread : Powerup
    {
        void Awake()
        {
            _type = Types.Spread;
            augmentComponent = "Weapon";
            tier = 2;
        }
    }
}
