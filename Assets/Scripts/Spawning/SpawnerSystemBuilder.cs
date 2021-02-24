using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Comburo
{
    public class SpawnerSystemBuilder : MonoBehaviour
    {
        #region Public Variables
        public Spawner spawner;
        public GameObject target;
        public SpawnerManager spawnerManager;
        public SpawningModeManager spawningModeManager;
        
        
        public float systemRadius = 22.0f;
        #endregion

        #region Private Variables
        private int numberOfSpawners = 46;
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            BuildSpawnerSystem();
            transform.Rotate(new Vector3(0,-90,0));
        }

        public void BuildSpawnerSystem()
        {

            for (int i = 0; i < numberOfSpawners; i++)
            {
                
                float radians = 2 * Mathf.PI / numberOfSpawners * i;
                float vertical = Mathf.Sin(radians);
                float horizontal = Mathf.Cos(radians);

                Vector3 spawnDir = new Vector3(horizontal,0,vertical);
                Vector3 spawnPos = transform.position + spawnDir * systemRadius;

                GameObject instance = Instantiate(spawner.gameObject,spawnPos, Quaternion.identity);
                instance.transform.SetParent(transform);

                instance.GetComponent<Spawner>().Initialize(i,spawnerManager, target.transform);


                spawnerManager.spawnersPool.Add(instance.GetComponent<Spawner>());


            }

        }


    }

}
