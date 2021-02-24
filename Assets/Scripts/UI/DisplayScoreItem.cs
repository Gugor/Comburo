using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace Comburo
{
    public class DisplayScoreItem : MonoBehaviour
    {
        public TextMeshProUGUI scoreDisplay;
        public ScoreManager scoreManager;
        // Update is called once per frame
        void Update()
        {
            scoreDisplay.text = scoreManager.Score.ToString();
        }


    }
}

