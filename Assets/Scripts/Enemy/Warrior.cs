using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Enemy
{
    class Warrior : EnemyBase
    {
        void Start()
        {
            acceleration = 15f;
            damage = 50f;
        }
    }
}
