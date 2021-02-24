using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Comburo
{
    [RequireComponent(typeof(Rigidbody), typeof(Animator))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;
        public ScoreManager scoreManager;
        public float speed;
        public float growRate = 0.1f;
        public GameObject fireHalo;

        public UnityEvent onPlayerDie;

        public void Die()
        {
            onPlayerDie.Invoke();
            //GameManager.Instance.GameOver();
        }
    }
}
