using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    private float rotationAmountX;
    private float rotationAmountY;
    [SerializeField] private float _mouseSensitivity = 1;
    [SerializeField] private float _MAX_Y_ROTATION;
    [SerializeField] private float _MIN_Y_ROTATION;
    private Camera _playerCam;
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

        _playerCam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            _escPressed?.Invoke();
        }
        rotationAmountX = Input.GetAxisRaw("Mouse X");
        rotationAmountY =  Input.GetAxisRaw("Mouse Y");

        transform.Rotate(new Vector3(0, rotationAmountX, 0));
        float currentRotationY = _playerCam.transform.localEulerAngles.x;

        if(currentRotationY > 180){
            currentRotationY -= 360;
        }
        //print($"Rotation Vertical is {(int)currentRotationY}");
        if(currentRotationY > _MIN_Y_ROTATION && currentRotationY < _MAX_Y_ROTATION){
            //print("inside bounds");
            _playerCam.transform.Rotate(new Vector3(-rotationAmountY*_mouseSensitivity, 0, 0), Space.Self);

        }else if(currentRotationY <= _MIN_Y_ROTATION){
            //print("Above max angle");
            if(rotationAmountY < 0){
                _playerCam.transform.Rotate(new Vector3(-rotationAmountY*_mouseSensitivity, 0, 0), Space.Self);
            }
            
        }else if(currentRotationY >= _MAX_Y_ROTATION){
            //print("Below min angle");
            if(rotationAmountY > 0){
                _playerCam.transform.Rotate(new Vector3(-rotationAmountY*_mouseSensitivity, 0, 0), Space.Self);
            }
        }
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
