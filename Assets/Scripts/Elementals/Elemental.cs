using System;
using System.Collections;
using System.Collections.Generic;
using Comburo.Tools;
using UnityEngine;

namespace Comburo
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Elemental : MonoBehaviour, IScorable, ISpawneable
    {
        #region Public Variable
        [Header("Dependencies")]
        [SerializeField] private GameObject prefab;
        public SpawnerManager spawnerManager;

        [Header("Settings")]
        public Transform target;
        [Range(0f,5f)] public float speedModifier;
        public int score;
        public int spawningWeight;
        public Weighted<Elemental> weighted;
        public float minScoreRadius;

        [Header("Effects")]
        public GameObject explotion;
        [HideInInspector]public bool IsMoving = true;
        #endregion

        #region Private Variables
        private SpawnedPoolManager spawnedPool;
        private float spawnedSpeed;
        private bool minScoreChanged;
        private GameObject bodyGO;
        #endregion

        #region Properties
        public Transform iTransform { get => transform; }
        public GameObject iGameObject { get => gameObject; }
        public int SpawningWeight { get => spawningWeight; set => spawningWeight = value; }
        #endregion

        public virtual void Awake()
        {
            spawnerManager = FindObjectOfType<SpawnerManager>();
            spawnedSpeed = spawnerManager.elementalsSpeed;
        }

        public void Start ()
        {
            weighted = new Weighted<Elemental>(this, 0);
            bodyGO = Instantiate(prefab,transform);
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, target.position) <= minScoreRadius)
            {
                ChangeScoreValue();
            }
        }

        //ISpawneable
        public virtual void Initialize(string name,Transform target, Transform parent ,Vector3 spawningPosition, SpawnedPoolManager spawnedPool)
        {
            this.name = name;
            this.target = target;
            this.spawnedPool = spawnedPool;

            transform.SetParent(parent);
            transform.position = spawningPosition;

            spawnedPool.Add(this);
        }


        private void ChangeScoreValue()
        {
            if(!minScoreChanged) 
            score = score / 2;
            minScoreChanged = true;
        }

        public virtual void FixedUpdate()
        {
            Move();
        }

        public abstract void Desapear();

        public virtual void Stop()
        {
            IsMoving = false;
        }

        public virtual void ResetMovement()
        {
            IsMoving = true;
        }

        protected virtual void Move()
        {
            if (IsMoving)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position,spawnedSpeed * speedModifier * Time.fixedDeltaTime);
                transform.LookAt(target);
            }
        }

        public int getScore()
        {
            return score;
        }

    }

}
