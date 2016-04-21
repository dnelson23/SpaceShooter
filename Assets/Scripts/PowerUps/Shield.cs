#pragma warning disable 0108 // hiding inherited member properties

using System;
using UnityEngine;

namespace Assets.Scripts.PowerUps
{
    class Shield : Powerup
    {
        void Awake()
        {
            augmentComponent = "Health";
            tier = 1;
            shieldHealth = 100f;
        }
    }
}
