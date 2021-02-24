using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Comburo.Devellopment
{
    public class ScrptPanel : MonoBehaviour
    {
        public GameObject devPanel;
        public Brain brain;
        public SpawnerManager spawnerManager;
        public SpawningModeManager spawningModeManager;
        public Transform anam;
        [Header("Difficulty Mode Section")]
        public Text difficultyModeDisplay;
        public Text poolSizeDisplay;
        public Text difficultyModeNameDispaly;
        [Header("Spawning Mode Section")]
        public Text spawningModeDisplay;
        public Text lifeTimeSpawningModeDisplay;
        public Text spawnedObjectsTextDisplay;
        public Text spawnedObjectsValueDisplay;
        [Header("Spawner Settings")]
        public Text spawnerRemainingTimedisplay;
        [Header("Anam's Settings")]
        public Text anamsSizeDispaly;
        public Text anamsGrowRatioDispaly;
        public Text anamsAutoGrowDisplay;
        public Text anamsTimeToGrowDisplay;

        //public Text lifeTimeSpawningMode;
        // Start is called before the first frame update
        void Start()
        {
            if (difficultyModeDisplay != null)
            {
                difficultyModeDisplay.text = spawnerManager.modesPoolData.difficulty.ToString();
            }
            if (poolSizeDisplay != null)
            {
                poolSizeDisplay.text = spawnerManager.modesPoolData.spawningModesManagersPool.Length.ToString() + " mods.";
            }
            if (difficultyModeNameDispaly != null)
            {
                difficultyModeNameDispaly.text = spawnerManager.modesPoolData.name;
            }
            if (spawningModeDisplay != null && spawnerManager.spawningModeManager != null)
            {
                spawningModeDisplay.text = spawnerManager.spawningModeManager.name;
            }
            if (lifeTimeSpawningModeDisplay != null)
            {
                //Debug.Log("Dev Pa. Time to  change mode" + spawningModeManager.RemainingTimeToChangeMode);
                //lifeTimeSpawningModeDisplay.text = Mathf.RoundToInt(spawningModeManager.RemainingTimeToChangeMode).ToString().PadLeft(4, '0');
            }
            if (spawnedObjectsTextDisplay != null)
            {
                spawnedObjectsTextDisplay.text = "SpawnedObjects:";
            }
            if (spawnedObjectsValueDisplay != null)
            {
                spawnedObjectsValueDisplay.text = spawnerManager.SpawnedObjects.ToString();
            }
            if (spawnerRemainingTimedisplay != null && spawnerManager.spawnerMachine != null)
            {
                spawnerRemainingTimedisplay.text = Mathf.RoundToInt(spawnerManager.spawnerMachine.RemainingTimeToSpawn).ToString().PadLeft(4,'0');
            }
            if (anamsSizeDispaly != null)
            {
                anamsSizeDispaly.text = "X:" + anam.GetComponentInChildren<Grow>().transform.lossyScale.x.ToString();
            }
            if (anamsGrowRatioDispaly != null)
            {
                anamsGrowRatioDispaly.text = "X:" + Mathf.Round(anam.GetComponent<Player>().growRate).ToString();
            }
            if (anamsAutoGrowDisplay != null)
            {
                anamsAutoGrowDisplay.text = anam.GetComponentInChildren<Grow>().CanGrow.ToString();
            }
            if (anamsTimeToGrowDisplay != null)
            {
                anamsTimeToGrowDisplay.text = System.Math.Round(anam.GetComponentInChildren<Grow>().ElapsedTime,2).ToString() + "s";
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                Toggle(devPanel);
            }
            if (spawningModeDisplay != null && spawnerManager.spawningModeManager != null)
            {
                spawningModeDisplay.text = spawnerManager.spawningModeManager.name;
            }
            if (lifeTimeSpawningModeDisplay != null)
            {
               // lifeTimeSpawningModeDisplay.text = Mathf.RoundToInt(spawningModeManager.RemainingTimeToChangeMode).ToString().PadLeft(4, '0');
            }
            if (spawnerRemainingTimedisplay != null)
            {
              //  spawnerRemainingTimedisplay.text = Mathf.RoundToInt(spawnerManager.spawnerMachine.RemainingTimeToSpawn).ToString().PadLeft(4, '0');
            }
            if (anamsSizeDispaly != null)
            {
                anamsSizeDispaly.text = "X:" + System.Math.Round(anam.GetComponentInChildren<Grow>().transform.lossyScale.x,2).ToString();
            }
            if (anamsGrowRatioDispaly != null)
            {
                anamsGrowRatioDispaly.text = "+" + anam.GetComponent<Player>().growRate.ToString();
            }
            if (anamsAutoGrowDisplay != null)
            {
                anamsAutoGrowDisplay.text = anam.GetComponentInChildren<Grow>().CanGrow.ToString();
            }
            if (anamsTimeToGrowDisplay != null)
            {
                anamsTimeToGrowDisplay.text = System.Math.Round(anam.GetComponentInChildren<Grow>().ElapsedTime, 2).ToString() + "s";
            }
        }


        void Toggle(GameObject panel)
        {
            panel.SetActive(!panel.activeSelf);
        }
    }
}
