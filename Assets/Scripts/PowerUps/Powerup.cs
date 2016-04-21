using System;
using UnityEngine;

namespace Assets.Scripts.PowerUps
{
    public abstract class Powerup : MonoBehaviour
    {
        public enum Types { Blaster, Spread, Shield, Default }

        // type determines what type of powerup this will be, self explanatory
        protected Types _type = Types.Blaster;
        public Types Type
        {
            get { return _type; }
        }

        public static GameObject[] PowerupObjects;

        // this variable is what part of the player will it modify; eg. weapon, health, speed etc.
        public string augmentComponent;

        // tier can be used to not override a more powerful powerup with a lesser one
        public int tier;

        // health for shield augments
        public float shieldHealth;

        public const string POWERUPRESOURCEPATH = "Prefabs/Powerups";

        float newRotationTime = 0.5f;
        float rotationCounter = 0f;
        Quaternion newRot;

        void Start()
        {
            LoadPowerupPrefabs();
        }

        // Give the powerup a nice random rotation
        void Update()
        {
            if (rotationCounter <= 0f)
            {
                newRot = UnityEngine.Random.rotation;
                rotationCounter = newRotationTime;
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, newRot, 0.02f);
            rotationCounter -= Time.deltaTime;
        }

        public static void DropRandomPowerup(Vector3 pos)
        {
            Types pUpType = GetRandomPowerupType();
            GameObject powerup = Resources.Load<GameObject>(POWERUPRESOURCEPATH + "/" + pUpType.ToString());

            if(powerup == null)
            {
                Debug.LogWarning("Powerup " + pUpType.ToString() + " was not loaded from resources");
                return;
            }

            GameObject.Instantiate(powerup, pos, Quaternion.identity);
            /*if(Powerup.PowerupObjects.Length == 0)
            {
                Debug.Log("No Powerups to drop");
                return;
            }

            int pUpIndex = UnityEngine.Random.Range(0, PowerupObjects.Length);
            GameObject powerup = PowerupObjects[pUpIndex];

            if (powerup == null)
            {
                Debug.LogWarning("Did not load powerup resource " + ((Powerup.Types)pUpIndex).ToString());
                return;
            }*/

            //GameObject obj = GameObject.Instantiate(powerup, pos, Quaternion.identity) as GameObject;
        }

        public static Types GetRandomPowerupType()
        {
            return (Types)UnityEngine.Random.Range(0, 3);
        }

        void LoadPowerupPrefabs()
        {
            PowerupObjects = Resources.LoadAll<GameObject>(POWERUPRESOURCEPATH);
            if (PowerupObjects == null || PowerupObjects.Length == 0)
            {
                Debug.LogWarning("Powerup Resources were not loaded");
            }
            PowerupObjects = new GameObject[0];
        }
    }
}
