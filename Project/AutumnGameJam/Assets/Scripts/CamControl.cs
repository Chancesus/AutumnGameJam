using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    private float rotationAmountX;
    private float rotationAmountY;
    [SerializeField] private float _MAX_Y_ROTATION = 45f;
    [SerializeField] private float _MIN_Y_ROTATION = 45f;

    private bool _escToggle;

    private System.Action _escPressed;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rotationAmountX = 0;
        rotationAmountY = 0;
        _escToggle = false;
        _escPressed += EscPressedCallback;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            _escPressed?.Invoke();
        }
        rotationAmountX = Input.GetAxisRaw("Mouse X");
        transform.Rotate(new Vector3(0, rotationAmountX, 0));
    }

    void EscPressedCallback(){
        if(_escToggle){
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _escToggle = false;
        }else{
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _escToggle = true;
        }
    }
}
