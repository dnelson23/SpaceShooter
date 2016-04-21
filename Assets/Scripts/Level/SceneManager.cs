using System;
using UnityEngine;
using System.Collections.Generic;


// This class is responsible for handling game restart and spawning enemies

namespace Assets.Scripts.Level
{
    class SceneManager : MonoBehaviour
    {
        #region Singleton
        private static SceneManager _instance;
        public static SceneManager Instance
        {
            get
            {
                _instance = _instance ?? (_instance = GameObject.FindObjectOfType<SceneManager>());
                if(_instance == null)
                {
                    Debug.LogWarning("SceneManager not in scene but a script is attempting to access it");
                }
                return _instance;
            }
        }
        #endregion

        float enemySpawnLength = 5f;
        float spawnTimer = 0f;

        GameObject[] enemyPrefabs;
        List<GameObject> enemiesPresent = new List<GameObject>();

        GameObject PauseMenu;

        void Start()
        {
            enemyPrefabs = Resources.LoadAll<GameObject>("Prefabs/Enemies");
            if(enemyPrefabs.Length == 0)
            {
                Debug.LogWarning("Could not load enemy prefab resources");
            }

            PauseMenu = GameObject.Find("PauseMenu") ?? Resources.Load<GameObject>("Prefabs/PauseMenu");
            PauseMenu.SetActive(false);
        }

        void Update()
        {
            if(spawnTimer <= 0f)
            {
                SpawnRandomEnemy();
                spawnTimer = enemySpawnLength;
            }
            else
            {
                spawnTimer -= Time.deltaTime;
            }

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                PauseOrUnpause();
            }
        }

        void SpawnRandomEnemy()
        {
            if(enemyPrefabs.Length == 0)
            {
                return;
            }

            // determine spawn location
            float xPos;
            float zPos;

            if(UnityEngine.Random.Range(0, 2) == 0)
            {
                xPos = UnityEngine.Random.Range(-16f, 16f);
                zPos = UnityEngine.Random.Range(0, 2) == 0 ? 9f : -9f;
            }
            else
            {
                xPos = UnityEngine.Random.Range(0, 2) == 0 ? 16f : -16f;
                zPos = UnityEngine.Random.Range(-9f, 9f);
            }

            Vector3 newPos = new Vector3(xPos, 0f, zPos);

            // determine enemy to spawn
            int enemy = UnityEngine.Random.Range(0, enemyPrefabs.Length);
            GameObject newEnemy = Instantiate(enemyPrefabs[enemy], newPos, Quaternion.identity) as GameObject;
        }

        void PauseOrUnpause()
        {
            if (Time.timeScale == 0)
            {
                PauseMenu.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                PauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
        }

        public void RestartGame()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            //pplication.LoadLevel(Application.loadedLevel);
        }
    }
}
