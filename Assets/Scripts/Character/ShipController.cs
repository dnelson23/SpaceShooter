using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Character
{
    class ShipController : Components.ControllerBase
    {
        private static ShipController _instance;
        public static ShipController Instance
        {
            get
            {
                _instance = _instance ?? GameObject.FindObjectOfType<ShipController>();
                if(_instance == null)
                {
                    Debug.LogWarning("No ShipController in scene but an object is attempting to access it");
                }
                return _instance;
            }
        }

        // custom component declarations
        // ShipState does not need to be added as a component, so we just create a new object for it
        ShipState _state = new ShipState();
        public ShipState.States State
        {
            get { return _state.currentState; }
            set { _state.ChangeState(value); }
        }
        Components.Movement _movement;
        Components.Weapon _weapon;
        Components.HitPoints _health;
        public float CurrentHealth
        {
            get { return _health.curHitPoints; }
        }

        // standard variables
        public Components.Bullet BulletPrefab;
        public float lives = 3f;
        float fireRate = 1f;
        float lastFired = 0f;
        float maxSpeed = 0.06f;
        float rotateSpeed = 3f;
        float hitPoints = 100f;

        protected override void Awake()
        {
            // add custom components to gameobject
            _movement = gameObject.AddComponent<Components.Movement>();
            _health = gameObject.AddComponent<Components.HitPoints>();
            _weapon = gameObject.AddComponent<Components.Weapon>();
        }

        void Start()
        {
            // set special component variables
            _health.SetHitPoints(hitPoints);
            _weapon.SetBulletPrefab(BulletPrefab);
        }

        void Update()
        {
            if (IsFiring() && lastFired >= fireRate)
            {
                lastFired = 0f;
                _weapon.Fire();
            }
            lastFired += Time.deltaTime;

            float hor = 0f, vert = 0f;
            GetMovementInput(ref hor, ref vert);
            Vector3 newMoveVect = (transform.forward * vert) * maxSpeed;

            _movement.SetMoveVector(newMoveVect);
            _movement.Rotate(transform.right * hor, rotateSpeed);
        }
        
        void GetMovementInput(ref float hor, ref float vert)
        {
            if(Input.GetKey(KeyCode.A)) { hor -= 1f; }
            if(Input.GetKey(KeyCode.D)) { hor += 1f; }
            if(Input.GetKey(KeyCode.W)) { vert += 1f; }
            if(Input.GetKey(KeyCode.S)) { vert -= 1f; }
        }

        bool IsFiring()
        {
            if (Input.GetKey(KeyCode.Space)) { return true; }
            return false;
        }
    }
}
