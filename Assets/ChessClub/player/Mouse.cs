using UnityEngine;

public class Mouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = ( - new Vector3(Screen.width,Screen.height,0)/2);
        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        p.z = 0;

        var trash = Vector2.zero;
            var mouseNewPosition =
                Vector2.SmoothDamp(
                    transform.position,
                    p,
                    ref trash,
                    0.0f);
        transform.position = mouseNewPosition;
    }
}
