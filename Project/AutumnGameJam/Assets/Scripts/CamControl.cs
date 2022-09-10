using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    private float rotationAmountX;
    private float rotationAmountY;
    private float _MAX_Y_ROTATION = 45f;
    private float _MIN_Y_ROTATION = 45f;
    // Start is called before the first frame update
    void Start()
    {
        rotationAmountX = 0;
        rotationAmountY = 0;
    }

    // Update is called once per frame
    void Update()
    {
        rotationAmountX = Input.GetAxisRaw("MouseX");
        transform.Rotate(new Vector3(0, rotationAmountX, 0));
    }
}
