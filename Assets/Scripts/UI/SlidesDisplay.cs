using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Comburo
{

    public class SlidesDisplay : MonoBehaviour
    {
        public GameManager gameManager;
        public List<GameObject> slides;

        public float slidingSpeed;
        public float relativeSlideWidth; //Width relative to parent. Distance to be moved * 2
        public RectTransform slideRef1;
        public RectTransform slideRef2;

        private RectTransform recPos;

        private Vector3 boundaries;
        private float maxLeftPos;
        private float maxRightPos;

        private bool isSlidingLeft = false;
        private bool isSlidingRight = false;

        private int numOfSlides;
        private int indexCurrentSlide = 1;
        private GameObject currentSlide;
        // Start is called before the first frame update
        void Start()
        {
            numOfSlides = slides.Count;
            currentSlide = slides[0];
            recPos = transform.GetComponent<RectTransform>();

            //Get de distance of the centers of the slides => Slide width.
            relativeSlideWidth = Mathf.Abs(slideRef1.localPosition.x - slideRef2.localPosition.x);

            maxRightPos = relativeSlideWidth * numOfSlides;
            maxLeftPos = -maxLeftPos;

            gameManager.Pause();
            //boundaries = Camera.main.ScreenToViewportPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        }

        public void LeftSliding()
        {
            Debug.Log("Sliding next...");
            if (isSlidingLeft || isSlidingRight){ return; }
                StartCoroutine(SlideLeft());
        }

        public IEnumerator SlideLeft()
        {
            if (indexCurrentSlide >= slides.Count)
            {
                yield break;
            }

            isSlidingLeft = true;
            isSlidingRight = false;
            float targetPosX = transform.localPosition.x + (relativeSlideWidth);// * indexCurrentSlide);


            while (transform.localPosition.x >  -targetPosX)
            {
                recPos.localPosition -= new Vector3(targetPosX * slidingSpeed * Time.fixedUnscaledDeltaTime, 0,0);

                //float clampSliderX = Mathf.Clamp(recPos.position.x, maxLeftPos, maxRightPos);
                //recPos.position = new Vector3(clampSliderX, recPos.position.y);
                Debug.Log("Intial pos: " + transform.localPosition.x + " | target pos: " + targetPosX);
                yield return null;
            }
            indexCurrentSlide++;
            Debug.Log("Sliding next..." + indexCurrentSlide);

            isSlidingLeft = false;
            isSlidingRight = false;
            yield return null;
        }

        public void RightSliding()
        {
            Debug.Log("Sliding back..." + indexCurrentSlide);
            if (isSlidingLeft || isSlidingRight){ return; }
                StartCoroutine(SlideRight());
        }

        public IEnumerator SlideRight()
        {
            if (indexCurrentSlide <= 1)
            {
                StopCoroutine(SlideLeft());
                yield break;
            }

            isSlidingRight = true;
            isSlidingLeft = false;
            float targetPosX = transform.localPosition.x + (relativeSlideWidth);// * indexCurrentSlide);


            while (transform.localPosition.x < targetPosX)
            {
                recPos.localPosition -= new Vector3(targetPosX * slidingSpeed * 30 * Time.fixedUnscaledDeltaTime, 0, 0);

                //float clampSliderX = Mathf.Clamp(recPos.position.x, maxLeftPos, maxRightPos);
                //recPos.position = new Vector3(clampSliderX,recPos.position.y);
                yield return null;
            }
            indexCurrentSlide--;
            Debug.Log("Sliding next..." + indexCurrentSlide);

            isSlidingRight = false;
            isSlidingLeft = false;
            yield return null;

        }
    }
}
