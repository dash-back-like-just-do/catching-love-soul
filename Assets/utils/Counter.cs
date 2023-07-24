using UnityEngine;

namespace utils
{
    public class Counter
    {
        private float _countTime;
        private float _cycleTime;

        public Counter(float cycleTime)
        {
            _countTime = 0;
            _cycleTime = cycleTime;
        }

        public Counter(float cycleTime, float countTime)
        {
            _countTime = countTime;
            _cycleTime = cycleTime;
        }

        public void Update()
        {
            if (_countTime < _cycleTime) _countTime += Time.deltaTime;
        }

        public bool IsTrigger()
        {
            return _countTime >= _cycleTime;
        }

        public void Reset()
        {
            _countTime = 0;
        }

        public void Reset(float cycle)
        {
            _countTime = 0;
            _cycleTime = cycle;
        }

        public void SetCycle(float cycle)
        {
            _cycleTime = cycle;
        }
    }
}