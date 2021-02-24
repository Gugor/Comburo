using System;
using UnityEngine;

namespace Comburo.Tools
{
    public class CallBackTimer<T>
    {

        private Action callBack;

        private float deltaTime;
        private float time;
        private float currentTime;

        private bool isRecursive;
        public bool isStopped;

        public float RemainingTime => currentTime;
        public float Time
        {
            get { return time; }
            set { time = value; }
        }
        public CallBackTimer(float _seconds,float _deltaTime, bool _recursive, Action _callBack, bool _stop = false)
        {
           // Debug.Log("Constructing CallBackTimer: " + _callBack.Method.ToString());
            this.time = _seconds; 
            this.deltaTime = _deltaTime;
            this.isRecursive = _recursive;
            this.callBack = _callBack;
            this.isStopped = _stop;

            currentTime = time;
        }

        // Update is called once per frame
        public void UpdateTime(float delTime)
        {
            this.deltaTime = delTime;
            //Choose
            if (!isStopped)
            {
               // Debug.Log("Updating Time: " + RemainingTime);
                if (isRecursive)
                {
                
                    RecursiveCountDown();
                    return;
                }
                else
                {
                    CountDown();
                    return;
                }
            }
            else
            {
               // Debug.Log(this.callBack.ToString() + " Is stopped");
            }
        }

        //Single use of the timer
        //Counts from maxTime to 0 and stops counting
        void CountDown()
        {
           // Debug.Log("Counting NOT recursive... | Current time: " + currentTime + " Delta time: " + deltaTime);
            if (currentTime <= 0f)
            {
               // Debug.Log("Invoking count down from timer");
                callBack?.Invoke();
                return;
            }
            else
            {
                currentTime -= deltaTime;
                return;
            }
        }

        //Recursive use of the timer
        //When come to 0 reset the timer to maxTime
        void RecursiveCountDown()
        {
            //Debug.Log("Counting recursive... | Current time: " + currentTime + " Delta time: " + deltaTime);
            if (currentTime <= 0f)
            {
               // Debug.Log("Invoking count down from timer");
                currentTime = time;
                callBack?.Invoke();
                return;
            }
            else
            {
                currentTime -= deltaTime;
                return;
            }
        }
    }
}

