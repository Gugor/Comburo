using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Comburo
{
    public class ParalelSpawningMachine : AbstractSpawningMachine
    {
        public Tools.CallBackTimer<Action> timer;
        public SpawnerManager spawnerManager;
        private float timeToSpawn;
        private float remainingTime;
        Action spawn;

        public override float RemainingTimeToSpawn => remainingTime;

        public override void Init()
        {
            Debug.Log("Initializing Paralel machine type...");
            spawn = Spawn;
            timer = new Tools.CallBackTimer<Action>(timeToSpawn, Time.deltaTime, true,spawn);
            //Initialize firs spawn.
            Spawn();
        }

        public override void Use()
        {
            remainingTime = timer.RemainingTime;
            timer.UpdateTime(Time.deltaTime);
            
        }

        public ParalelSpawningMachine(SpawnerManager _sawnerManager, float time)
        {
            this.spawnerManager = _sawnerManager;
            this.timeToSpawn = time;
        }

        // Start is called before the first frame update


        private void Spawn()
        {
            Debug.Log("Paralel spawning...");
            spawnerManager.refreshSpawningDelay();
            spawnerManager.onSpawning.Invoke();
            timer.Time = timeToSpawn;
        } 
    }
}
