using UnityEngine;

namespace utils
{
    public class Counter
    {
        private float _cycleTime;
        private float _countTime;

        public Counter(float cycleTime)
        {
            this._countTime = 0;
            this._cycleTime = cycleTime;
        }
        public Counter(float cycleTime,float countTime)
        {
            this._countTime = countTime;
            this._cycleTime = cycleTime;
        }

        public void Update()
        {
            if (_countTime < _cycleTime)
            {
                _countTime += Time.deltaTime;
            }
        }

        public bool IsTrigger()
        {
            if (!(_countTime >= _cycleTime)) return false;
            _countTime = 0;
            return true;
        }
    }
}