using UnityEngine;

namespace utils
{
    public class Counter
    {
        private float _countTime;
        private readonly float _cycleTime;

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
            if (!(_countTime >= _cycleTime)) return false;
            _countTime = 0;
            return true;
        }

        public void Reset()
        {
            _countTime = 0;
        }
    }
}