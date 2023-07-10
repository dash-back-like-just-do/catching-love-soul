using UnityEngine;

namespace utils
{
    public class Counter
    {
        public float cycleTime;
        public float countTime;
        public bool trigger;

        Counter(float cycleTime,float countTime)
        {
            this.countTime = countTime;
            this.cycleTime = cycleTime;
            trigger = false;
        }

        public void Update()
        {
            if (countTime >= cycleTime && !trigger )
            {
                countTime = 0;
                trigger = true;
            }
            else
            {
                countTime += Time.deltaTime;
            }
        }

        public bool IsTrigger()
        {
            var result = trigger;
            trigger = false;
            return result;
        }
    }
}