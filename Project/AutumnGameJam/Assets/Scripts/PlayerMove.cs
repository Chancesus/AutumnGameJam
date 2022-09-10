using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Collider _collider;
    [SerializeField] private float _speedModifier;
    [SerializeField] private float _jumpModifier;
    [SerializeField] private float _MAX_SPEED = 5f;
    private bool _grounded = false;
    private bool _canJump = true;
    private Vector3 _playerInput;
    [SerializeField] LayerMask _groundMask;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        _playerInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if(Input.GetButtonDown("Jump") && _canJump && _grounded){
            _rigidbody.AddForce(transform.up*_jumpModifier, ForceMode.VelocityChange);
            _canJump = false;
            _grounded = false;
            StartCoroutine(resetCanJump());
        }
    }

    void FixedUpdate(){
        if(_canJump){

            Debug.DrawRay(transform.position, Vector3.down*1.5f);

            if(Physics.Raycast(transform.position, Vector3.down, 1.25f, _groundMask)){
                _grounded = true;
            }
        }
        if(!_grounded) return;
        //player let go and therefore should slow down
        if(_playerInput == Vector3.zero){
            if(_rigidbody.velocity.magnitude <= 1){
                _rigidbody.velocity = Vector3.zero;
            }else{
                _rigidbody.velocity = _rigidbody.velocity/1.1f;
            }
        }
        //Picking up speed
        if(_rigidbody.velocity.magnitude < _MAX_SPEED){
            _rigidbody.AddForce(transform.TransformDirection(_playerInput).normalized * _speedModifier, ForceMode.VelocityChange);
        }
        //At MAX speed
        if(_rigidbody.velocity.magnitude >= _MAX_SPEED){
            if((_rigidbody.velocity + transform.TransformDirection(_playerInput).normalized*_speedModifier).magnitude < _MAX_SPEED){
                _rigidbody.AddForce(transform.TransformDirection(_playerInput).normalized * _speedModifier, ForceMode.VelocityChange);
            }
        }
    }

    IEnumerator resetCanJump(){
        yield return new WaitForSeconds(0.5f);
        _canJump = true;
    }
}
