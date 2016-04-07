using System;
using System.Collections.Generic;
using UnityEngine;
using PowerupTypes = Assets.Scripts.PowerUps.Powerup.Types;

namespace Assets.Scripts.Character
{
    class ShipController : Components.ControllerBase
    {
        #region Singleton
        /*
         * This section creates allows for the singleton pattern to be used by this object.
         * Any other script can retrieve a reference to the player ShipController by calling ShipController.Instance
         */
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
        #endregion

        // custom component declarations
        // ShipState does not need to be added as a component, so we just create a new object for it
        ShipState _state = new ShipState();
        public ShipState.States State
        {
            get { return _state.currentState; }
            set { _state.ChangeState(value); }
        }

        // movement
        Components.RigidbodyMovement2D _movement;
        public float acceleration = 15f;
        public float maxSpeed = 25f;
        public float rotateSpeed = 2.5f;

        // weapon
        Components.Weapon _weapon;
        float fireRate = 1f;
        float lastFired = 0f;

        // health
        Components.HitPoints _health;
        public float CurrentHealth
        {
            get { return _health.curHitPoints; }
        }
        float hitPoints = 100f;

        // Powerups
        List<PowerupTypes> currentPowerups = new List<PowerupTypes>();

        // standard properties
        public float lives = 3f;

        protected override void Awake()
        {
            // add custom components to gameobject
            _movement = gameObject.AddComponent<Components.RigidbodyMovement2D>();
            _health = gameObject.AddComponent<Components.HitPoints>();
            _weapon = gameObject.AddComponent<Components.Weapon>();
        }

        void Start()
        {
            // set special component variables
            _health.SetMaxHitPoints(hitPoints);
            _movement.acceleration = acceleration;
            _movement.maxSpeed = maxSpeed;
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
            Vector3 newMoveVect = transform.forward * vert;

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
            return Input.GetKey(KeyCode.Space) ? true : false;
        }
    }
}
