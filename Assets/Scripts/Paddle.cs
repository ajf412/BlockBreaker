using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    // Configuration Parameters
    [SerializeField] float screenWidthInUnits= 16;
    [SerializeField] float minPaddlePos = .64f, maxPaddlePos = 15.36f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mousePosInUnits = Input.mousePosition.x / Screen.width * screenWidthInUnits;
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        paddlePos.x = Mathf.Clamp(mousePosInUnits, minPaddlePos, maxPaddlePos);
        transform.position = paddlePos;
    }
}
