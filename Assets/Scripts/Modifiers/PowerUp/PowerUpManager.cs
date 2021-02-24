using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using PathCreation.Examples;

namespace Comburo
{
    public class PowerUpManager : MonoBehaviour
    {
        public SpawnerManager spawnerManager;
        public GameObject powerUpBtn;

        public PowerUpDisplayImage powerUpDisplayImage;
        public GameObject target;
        
        [Range(5,30)]
        public float minSpawnTime;
        [Range(10, 60)]
        public float maxSpawnTime;
        public List<GameObject> powerUpsPrefabs = new List<GameObject>();

        [Header("Auto filled references")]
        public AbstractPowerUpMachine currentPower;
        public AbstractPowerUpMachine previousPower;
        public GameObject currentPowerUpCreatureInWorld;

        public bool isPowerUpInWorld = false; //Shows if there is power Up is in world.
        public bool isWorldClearToSpawn = false; //Shows if world has no power up creatures in it.
        public bool startCoolDown = false; //Allows to startCoolDown.
        private bool hasCoolDown = false; //Shows is coolDown has arrived to 0;

        private float remainingTimeToSpawn;

        private AbstractPowerUpMachine[] powerUpMachines;


        //private Vector3 boundaries;

        void Start()
        {
            powerUpMachines = GetComponentsInChildren<AbstractPowerUpMachine>();
            remainingTimeToSpawn = getRandomTime();

            //Set flags
            isWorldClearToSpawn = true;
            isPowerUpInWorld = false;
            startCoolDown = true;
            hasCoolDown = false;

            //boundaries = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,Camera.main.transform.position.z));
        }

        void Update()
        {
            //Counting time to next spawn;
            if (startCoolDown)
            {
                CoolDown();
            }

            if (currentPowerUpCreatureInWorld == null)
            {
                isPowerUpInWorld = false;
                
            }

            if (hasCoolDown)
            {
                if (currentPower != null)
                {
                    if (!currentPower.isActive && !currentPower.isBeingUsed)
                    {
                        isWorldClearToSpawn = true;
                    }
                    else
                    {
                        isWorldClearToSpawn = false;
                    }
                }
            }


            if (!isPowerUpInWorld && hasCoolDown && isWorldClearToSpawn)
            {
                Spawn();
            }
        }

        public void Use()
        {
            if (currentPower == null) { return; }
            if (currentPowerUpCreatureInWorld != null)
            {
                currentPowerUpCreatureInWorld.GetComponent<PowerUp>().Desapear();
            }

            Debug.Log("Power Up is being used: " + currentPower.isBeingUsed);

            if (!currentPower.isBeingUsed)
            {
                startCoolDown = false;
                previousPower = currentPower;
                powerUpBtn.SetActive(false);
                StartCoroutine(currentPower.Use());

            }

        }

        public void CoolDown()
        {

            if (remainingTimeToSpawn <= 0)
            {
                remainingTimeToSpawn = getRandomTime();
                hasCoolDown = true;
            }
            else
            {
                remainingTimeToSpawn -= Time.deltaTime;
            }
        }

        private float getRandomTime()
        {
            return Random.Range(minSpawnTime, maxSpawnTime);
        }

        private GameObject ChooseRandomPowerUP()
        {
            //Debug.Log("Number of spawners: " + powerUpsPrefabs.Count);
            int randomIndex = Mathf.RoundToInt(Random.Range(0, powerUpsPrefabs.Count));
            return powerUpsPrefabs[randomIndex];
        }

        public void Spawn()
        {
            Debug.Log(this.name + "Can Spawn: " + hasCoolDown);

            //Choose random powerup creature to spawn
            PowerUp randPU = ChooseRandomPowerUP().GetComponent<PowerUp>(); //Get a random power up from the list of prefabs

            //Choose the machine to use this random powerup
            AbstractPowerUpMachine powerUpMachine = GetPowerUPMachine(randPU.powerUPType);

            //Pick up a random path to be followed by the power up creature
            PathFollower pathFollower = randPU.GetComponent<PathFollower>();
            pathFollower.pathCreator = powerUpMachine.GetRandomPath();

            //Instantiate creature in the world
            GameObject spawned = Instantiate(randPU.gameObject,pathFollower.pathCreator.path.GetPoint(0),Quaternion.identity); //You spawning something... May be.
            currentPowerUpCreatureInWorld = spawned.gameObject;
            spawned.transform.position = pathFollower.pathCreator.path.localPoints[0];
            Debug.Log("Power Up => " + spawned.name);

            spawned.GetComponent<PowerUp>().puManager = this;
            
            hasCoolDown = false; 
            isPowerUpInWorld = true;
        }

        //<summary>Sets the current Power Up to be used.
        //<para></para>
        //</summary>
        public void setCurrentPowerUPMachine(PowerUps power)
        {
            AbstractPowerUpMachine[] powers = GetComponentsInChildren<AbstractPowerUpMachine>();

            switch (power)
            {
                case PowerUps.FREEZE:
                    previousPower = currentPower;
                    currentPower = System.Array.Find<AbstractPowerUpMachine>(powers,powerUP => powerUP.GetComponent<FreezePU>());
                    break;
                case PowerUps.CALM_WATERS:
                    previousPower = currentPower;
                    currentPower = System.Array.Find<AbstractPowerUpMachine>(powers, powerUP => powerUP.GetComponent<CalmWatersPU>());
                    break;
                case PowerUps.ANAMSRAGE:
                    previousPower = currentPower;
                    currentPower = System.Array.Find<AbstractPowerUpMachine>(powers, powerUP => powerUP.GetComponent<AnamsRagePU>());
                    break;
                case PowerUps.PEACE_OF_MIND:
                    previousPower = currentPower;
                    currentPower = System.Array.Find<AbstractPowerUpMachine>(powers, powerUP => powerUP.GetComponent<PeaceOfMindPU>());
                    break;
            }

        }

        public AbstractPowerUpMachine GetPowerUPMachine(PowerUps power)
        {
            AbstractPowerUpMachine[] powers = GetComponentsInChildren<AbstractPowerUpMachine>();

            switch (power)
            {
                case PowerUps.FREEZE:
                    return System.Array.Find<AbstractPowerUpMachine>(powers, powerUP => powerUP.GetComponent<FreezePU>());
                    
                case PowerUps.CALM_WATERS:
                    return System.Array.Find<AbstractPowerUpMachine>(powers, powerUP => powerUP.GetComponent<CalmWatersPU>());
                    
                case PowerUps.ANAMSRAGE:
                    return System.Array.Find<AbstractPowerUpMachine>(powers, powerUP => powerUP.GetComponent<AnamsRagePU>());
                    
                case PowerUps.PEACE_OF_MIND:
                    return System.Array.Find<AbstractPowerUpMachine>(powers, powerUP => powerUP.GetComponent<PeaceOfMindPU>());
                    
            }

            return null;
        }
    }
}
