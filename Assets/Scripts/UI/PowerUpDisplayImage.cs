using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Comburo
{
    public class PowerUpDisplayImage : MonoBehaviour
    {   
        public Image imageDisplay;

        public void setImage(Sprite sprite)
        {
            imageDisplay.enabled = true;
            imageDisplay.sprite = sprite;
        }

        public void unsetImage()
        {
            imageDisplay.sprite = null;
            imageDisplay.enabled = false;
        }
    }
}
