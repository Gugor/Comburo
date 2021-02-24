using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Comburo
{
    public class SecuencialSpawningMachine : AbstractSpawningMachine
    {
        public Tools.CallBackTimer<Action> timer;
        public List<Spawner> spawners = new List<Spawner>();
        //public AbstractSpawnerManager<Spawner> spawnerManager;

        private List<Weighted> spawneablesPool = new List<Weighted>();
        private float timeToSpawn;
        private int spawnerIndex = 0;
        private Action spawn;

        public override float RemainingTimeToSpawn { get => timer.RemainingTime; }

        public override void Init()
        {
            //Iicialización de valores
            spawn = Spawn;
            timer = new Tools.CallBackTimer<Action>(timeToSpawn,Time.deltaTime,true,spawn);
            Spawn(); //Spawn as it starts
        }

        public override void Use()
        {
           // Debug.Log("Secuencial: Remaining time to spawn:" + timer.RemainingTime + ". Timer status: isStopped " + timer.isStopped);
           // Debug.Log("Secuencial: Spawner index:" + spawnerIndex);
            timer.UpdateTime(Time.deltaTime);
            RemainingTimeToSpawn = timer.RemainingTime;
        }

        public SecuencialSpawningMachine(List<Spawner> spawners, List<Weighted> spawneablesPool ,float time)
        {
            // this.spawnerManager = spm.GetComponent<AbstractSpawnerManager<Spawner>>();
            this.spawneablesPool = spawneablesPool;
            this.spawners = spawners;
            this.timeToSpawn = time;
        }

        public void Spawn()
        {
            Spawner spawner;

            if (spawnerIndex >= spawners.Count) { return; }

            spawner = spawners[spawnerIndex];
            //Debug.Log("Checking if can spawn secuencial...");
            if (spawner.canSpawn)
            {
               // Debug.Log("Secuencial: Spaner index " + spawnerIndex + " || Allspawners: " + spawnerManager.allSpawners.Count);
                if (spawnerIndex >= spawners.Count - 1)
                {
                    spawnerIndex = 0;
                }
                else
                {
                    spawnerIndex++;

                    ISpawneable spawneable = Weight.GetRandomWeighted(spawneablesPool);
                    if (spawneable == null)
                    {
                        Debug.LogWarningFormat("Spawner [0] can't spawn. Spawning mode has no spawneables weighted", spawnerIndex);
                        return;
                    }
                    
                    spawner.SpawnObject(spawneable);

                }

            }
            else
            {
                if (spawnerIndex >= spawners.Count - 1)
                {
                    spawnerIndex = 0;
                }
                else
                {
                    spawnerIndex++;
                }
            }       
                
        }
    }
}
