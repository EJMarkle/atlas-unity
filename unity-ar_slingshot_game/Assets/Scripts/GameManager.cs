using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using TMPro;
using System.Collections;
using NUnit.Framework;

namespace GameManagement 
{
    public class GameManager : MonoBehaviour
    {
        public MenuManager menuManager;
        public GameObject gameUI;
        public GameObject slingshot;
        public GameObject ball;
        public GameObject worldSpawner;
        public ObjectSpawner objectSpawner; 
        public ScoreManager scoreManager;
        public SlingshotInteraction slingshotInteraction;
        public string currentLevel = "Level01";
        public int winScore = 300;
        private bool progressUpdated = false;
        public GameObject levelEnd;
        public TextMeshProUGUI score;
        public TextMeshProUGUI par;
        public GameObject win;
        public GameObject lose;
        public GameObject highScoreMessage;
        public int par1 = 300;
        public int par2 = 600;
        public int par3 = 1000;
        private GameObject audioManager; 
        private AudioSource[] audioSources;
    

        private Dictionary<string, int> levelWinScores => new Dictionary<string, int>()
        {
            { "Level01", par1 },
            { "Level02", par2 },
            { "Level03", par3 } 
        };


        private void Start()
        {
            audioManager = GameObject.Find("AudioManager");
            audioSources = audioManager.GetComponents<AudioSource>();

            if (menuManager != null)
            {
                menuManager.OnGameStart.AddListener(BeginGame);
            }
            else
            {
                Debug.LogError("[GameManager] MenuManager is not assigned!");
            }

            if (slingshot != null)
            {
                slingshot.SetActive(false);
            }
            else
            {
                Debug.LogError("[GameManager] Slingshot is not assigned!");
            }

            if (ball != null)
            {
                ball.SetActive(false);
            }
            else
            {
                Debug.LogError("[GameManager] Ball is not assigned!");
            }

            if (worldSpawner != null)
            {
                worldSpawner.SetActive(false);
            }
            else
            {
                Debug.LogError("[GameManager] WorldSpawner is not assigned!");
            }

            if (objectSpawner != null)
            {
                AssignCorrectLevelPrefab();
                objectSpawner.ResetSpawner();
                Debug.Log("[GameManager] ObjectSpawner reset and updated with correct level prefab.");
            }
            else
            {
                Debug.LogError("[GameManager] ObjectSpawner is missing!");
            }
        }

        private void Update()
        {
            if (scoreManager.ammo == 0 && !progressUpdated)
            {
                StartCoroutine(UpdateProgress());
                progressUpdated = true;
            }
        }

        private void BeginGame()
        {
            Debug.Log("[GameManager] Starting game...");
            progressUpdated = false;

            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
            foreach (GameObject obj in allObjects)
            {
                if (obj.name.Contains("(Clone)"))
                {
                    Debug.Log("[GameManager] Destroying clone: " + obj.name);
                    Destroy(obj);
                }
            }

            scoreManager.score = 0;
            scoreManager.ammo = 5;

            AssignCorrectLevelPrefab();

            if (menuManager != null && menuManager.mainMenu != null)
            {
                menuManager.mainMenu.SetActive(false);
            }
            else
            {
                Debug.LogError("[GameManager] Main menu reference missing in MenuManager!");
            }
            gameUI.SetActive(true);

            if (slingshot != null)
            {
                slingshot.SetActive(true);
                Debug.Log("[GameManager] Slingshot activated.");

                if (ball != null)
                {
                    ball.SetActive(true);
                    
                    if (slingshotInteraction == null)
                    {
                        slingshotInteraction = ball.GetComponent<SlingshotInteraction>();
                        Debug.Log("[GameManager] SlingshotInteraction found on ball: " + (slingshotInteraction != null));
                    }
                    
                    if (slingshotInteraction != null)
                    {
                        StartCoroutine(ResetBallStateNextFrame());
                    }
                    else
                    {
                        Debug.LogError("[GameManager] SlingshotInteraction not found on ball!");
                    }
                    
                    Debug.Log("[GameManager] Ball activated after slingshot.");
                }
            }

            if (worldSpawner != null)
            {
                if (objectSpawner != null)
                {
                    objectSpawner.ResetSpawner();
                    Debug.Log("[GameManager] ObjectSpawner reset.");
                }
                
                worldSpawner.SetActive(true);
                Debug.Log("[GameManager] WorldSpawner activated.");
            }
        }

        private IEnumerator ResetBallStateNextFrame()
        {
            yield return null;
            
            slingshotInteraction.ResetBallState();
            Debug.Log("[GameManager] Ball state reset after delay.");
        }

        private void AssignCorrectLevelPrefab()
        {
            if (objectSpawner == null || objectSpawner.objectPrefabs == null)
            {
                Debug.LogError("[GameManager] ObjectSpawner or its objectPrefabs list is null!");
                return;
            }

            GameObject levelPrefab = Resources.Load<GameObject>(currentLevel);

            if (levelPrefab == null)
            {
                Debug.LogError("[GameManager] Could not find level prefab: " + currentLevel);
                return;
            }

            objectSpawner.objectPrefabs.Clear();
            objectSpawner.objectPrefabs.Add(levelPrefab);

            Debug.Log("[GameManager] Updated ObjectSpawner with level prefab: " + currentLevel);
        }
        
        public void SetLevelFromButton(GameObject button)
        {
            if (button == null)
            {
                Debug.LogError("[GameManager] Button reference is null!");
                return;
            }

            currentLevel = button.name;

            
            if (levelWinScores.TryGetValue(currentLevel, out int score))
            {
                winScore = score;
                Debug.Log($"[GameManager] Level set to: {currentLevel}, Win Score set to: {winScore}");
            }
            else
            {
                Debug.LogWarning($"[GameManager] No win score found for {currentLevel}, defaulting to 0.");
                winScore = 0;
            }
        }

        public IEnumerator UpdateProgress()
        {
            yield return new WaitForSeconds(1.5f);
            Debug.Log("Updating progress...");

            int currentScore = scoreManager.score;
            int highScore = PlayerPrefs.GetInt(currentLevel + "_highscore", 0);

            if (currentScore >= winScore)
            {
                PlayerPrefs.SetInt(currentLevel + "_completed", 1);
                Debug.Log($"[GameManager] Level {currentLevel} passed!");
            }


            levelEnd.SetActive(true);
            score.text = scoreManager.score.ToString();
            par.text = winScore.ToString();

            if (currentScore > highScore)
            {
                PlayerPrefs.SetInt(currentLevel + "_highscore", currentScore);
                audioSources[7].Play();
                highScoreMessage.SetActive(true);
            }
            else if (currentScore <= highScore)
            {
                highScoreMessage.SetActive(false);
            }


            if (currentScore >= winScore)
            {
                audioSources[5].Play();
                win.SetActive(true);
                lose.SetActive(false);
            }
            else if (currentScore < winScore)
            {
                audioSources[6].Play();
                lose.SetActive(true);
                win.SetActive(false);
            }

            PlayerPrefs.Save();
        }

        public void ClearProgress()
        {
            Debug.Log("[GameManager] Clearing all progress...");

            string[] levels = { "Level01", "Level02", "Level03" };

            foreach (string level in levels)
            {
                PlayerPrefs.DeleteKey(level + "_completed");
                PlayerPrefs.DeleteKey(level + "_highscore");
                Debug.Log($"[GameManager] Cleared progress for {level}");
            }

            PlayerPrefs.Save();
            Debug.Log("[GameManager] All progress cleared!");
        }

    }
}