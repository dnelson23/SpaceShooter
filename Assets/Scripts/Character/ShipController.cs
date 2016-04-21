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
         * This section allows for the singleton pattern to be used by this object.
         * Any other script can retrieve a reference to the player ShipController by calling ShipController.Instance
         */
        private static ShipController _instance = null;
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
        PowerupTypes curPowerupType
        {
            get { return curPowerups["Weapon"] == null ? curPowerups["Weapon"].Type : PowerupTypes.Default; }
        }
        public float fireRate = 1f;
        float lastFired = 0f;
        Dictionary<string, GameObject> bullets = new Dictionary<string, GameObject>();

        // health
        Components.HitPoints _health;
        public float CurrentHealth
        {
            get { return _health.curHitPoints; }
        }
        float hitPoints = 100f;

        // Powerups
        Dictionary<string, PowerUps.Powerup> curPowerups = new Dictionary<string, PowerUps.Powerup>();

        // standard properties
        public float lives = 3f;

        protected override void Awake()
        {
            // add custom components to gameobject
            _movement = gameObject.AddComponent<Components.RigidbodyMovement2D>();
            _health = gameObject.AddComponent<Components.HitPoints>();
            _weapon = gameObject.AddComponent<Components.Weapon>();

            // Load bullet prefabs for powerups
            GameObject blaster = Resources.Load<GameObject>("Prefabs/Bullets/Single");
            if(blaster == null) { Debug.Log("Blaster prefab did not load"); }
            else { bullets.Add("Blaster", blaster); }

            GameObject spread = Resources.Load<GameObject>("Prefabs/Bullets/Double");
            if(spread == null) { Debug.Log("Spread prefab did not load"); }
            else { bullets.Add("Spread", spread); }
        }

        void Start()
        {
            // set special component variables
            _health.SetMaxHitPoints(hitPoints);
            _movement.acceleration = acceleration;
            _movement.maxSpeed = maxSpeed;

            // Set rigidbody constraints
            RigidbodyConstraints constraints = RigidbodyConstraints.FreezeRotation |
                                               RigidbodyConstraints.FreezePositionY;
            _movement.rBody.constraints = constraints;

            _weapon.SetBullet(bullets["Blaster"]);
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

        void OnTriggerEnter(Collider col)
        {
            if(col.gameObject.tag == "powerup")
            {
                PowerUps.Powerup pUp = col.gameObject.GetComponent<PowerUps.Powerup>();
                AddPowerup(pUp);
                GameObject.Destroy(col.gameObject);
            }
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

        void AddPowerup(PowerUps.Powerup powerup)
        {

            if (curPowerups.ContainsKey(powerup.augmentComponent))
            {
                curPowerups[powerup.augmentComponent] = powerup;
            }
            else
            {
                curPowerups.Add(powerup.augmentComponent, powerup);
            }

            if (powerup.augmentComponent == "Weapon")
            {
                _weapon.SetBullet(bullets[powerup.Type.ToString()]);
            }
            else
            {
                _health.Heal(powerup.shieldHealth);
            }
        }

        public override void Destroy()
        {
            Level.SceneManager.Instance.RestartGame();
        }
    }
}
