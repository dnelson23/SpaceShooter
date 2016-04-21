#pragma warning disable 0108 // hiding inherited member properties

using System;
using UnityEngine;

namespace Assets.Scripts.PowerUps
{
    class Blaster : Powerup
    {
        void Awake()
        {
            _type = Types.Blaster;
            augmentComponent = "Weapon";
            tier = 1;
        }
    }
}
