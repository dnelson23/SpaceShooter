﻿using System;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    class AttackCopter : EnemyBase
    {
        void Start()
        {
            acceleration = 15f;
            damage = 100f;
        }
    }
}
