using UnityEngine;

namespace player
{
    public class LoveMessage : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
    }
}