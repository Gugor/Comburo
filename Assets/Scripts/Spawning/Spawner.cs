using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Comburo
{
    public class Spawner : SpawnerBase<SpawnerManager>
    {
        public override void Initialize(int _indexID,SpawnerManager _spm,  Transform _target)
        {
            indexID = _indexID;
            spawnerManager = _spm;
            target = _target;   
        }

        /// <summary>
        /// Spawns single ISpawneable object at this spawner
        /// </summary>
        /// <param name="go">Object to be spawned</param>
        /// <returns></returns>
        public override GameObject SpawnObject(ISpawneable go)
        {
            Debug.Log("Spawn object: " + go.iTransform.name);

            GameObject instance = Instantiate(go.iGameObject, transform.position, go.iTransform.rotation);

            instance.GetComponent<ISpawneable>().Initialize(name, target, spawnerManager.elementalsParent, transform.position, spawnerManager.spawnedPool);

            return instance;
        }
        /// <summary>
        /// Spawns a random ISpawneable from current mode ObjectToSpawn
        /// </summary>
        /// <returns></returns>
        public void SpawnRandom()
        {

            nextToSpawn = SelectRandomSpawneable().iGameObject;
            if (nextToSpawn == null) { return; }
            GameObject instance = Instantiate(nextToSpawn, transform.position, nextToSpawn.transform.rotation);

            ISpawneable spawneable = instance.GetComponent<ISpawneable>();
            spawneable.Initialize(name, target, spawnerManager.elementalsParent, transform.position, spawnerManager.spawnedPool);
            spawnerManager.ObjectsSpawnedPlusOne();
        }
        public void SpawnRandomFromPool(List<Weighted> weighteds)
        {
            ISpawneable go = Weight.GetRandomWeighted(weighteds);
            nextToSpawn = go.iGameObject;
            GameObject instance = Instantiate(nextToSpawn, transform.position, nextToSpawn.transform.rotation);

            ISpawneable spawneable = instance.GetComponent<ISpawneable>();
            spawneable.Initialize(name, target, spawnerManager.elementalsParent, transform.position, spawnerManager.spawnedPool);
            spawnerManager.ObjectsSpawnedPlusOne();
        }

        public ISpawneable SelectRandomSpawneable()
        {
            if (spawnerManager.currentMode.objectsToSpawnInCentripedSpawningSystem.Count <= 0)
            {
                Debug.Log("Tying to get spawnables in mode {0} but it was empty.", spawnerManager.currentMode);
                //This spawning mode has no spawneables it shouldn't be picked up.
                //We should call getNextSpawningMode() in SpawningModeManager
                spawningModeManager.selectNextSpawningModeManager(spawningModeManager.CurrentSpawningModePool);
            }

            ISpawneable go = Weight.GetRandomWeighted(spawnerManager.currentMode.objectsToSpawnInCentripedSpawningSystem);

            while (go == null)
            {
                go = Weight.GetRandomWeighted(spawnerManager.currentMode.objectsToSpawnInCentripedSpawningSystem);
            }
            nextToSpawn = go.iGameObject;

            return go;
        }



#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (gizmosSO == null) { return; }
            //Gizmos.color = gizmosSO.color;
            //Gizmos.DrawCube(transform.position, new Vector3(gizmosSO.size, gizmosSO.size, gizmosSO.size));
            Gizmos.color = gizmosSO.color;
            Gizmos.DrawSphere(transform.position, gizmosSO.size);
        }

        #endif


    }
}
