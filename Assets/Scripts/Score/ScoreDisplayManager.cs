using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Comburo
{
    public class ScoreDisplayManager : MonoBehaviour
    {
        public int score;
        public IScorable sorable;
        public bool isUnScore;
        // Start is called before the first frame update
        void Start()
        {
            if (sorable != null)
            {
                score = sorable.getScore();
            }
            StartCoroutine(DestroyAfter(0.7f));
        }

        IEnumerator DestroyAfter(float s)
        {
            yield return new WaitForSeconds(s);
            Destroy(gameObject);
        }
    }
}
