using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Collider _collider;
    [SerializeField] private float _speedModifier;
    [SerializeField] private float _jumpModifier;
    [SerializeField] private float _MAX_SPEED;
    private bool _grounded = false;
    private bool _canJump = true;
    private Vector3 _playerInput;
    private Camera _camera;
    private RaycastHit _lookingAtObject;
    Material _mat;
    [SerializeField] LayerMask _groundMask;
    [SerializeField] LayerMask _gatherableMask;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _camera = GetComponentInChildren<Camera>();
        Physics.gravity = new Vector3(0, -15.0f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        _playerInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if(Input.GetButtonDown("Jump") && _canJump && _grounded)
        {
            _rigidbody.AddForce(transform.up*_jumpModifier, ForceMode.VelocityChange);
            _canJump = false;
            _grounded = false;
            StartCoroutine(resetCanJump());
        }

        if(Input.GetButtonDown("Fire1"))
        {
            if(_lookingAtObject.collider != null)
            {
                Gatherable obj;
                if(_lookingAtObject.collider.TryGetComponent<Gatherable>(out obj))
                {
                    obj.PlayerFoundObject();
                }
            }
        }
        
    }

    void FixedUpdate()
    {
        playerMove();
        UpdateLookingAt();
    }

    private void playerMove()
    {
        if(_canJump)
        {
            Debug.DrawRay(transform.position, Vector3.down*1.5f);
            if(Physics.Raycast(transform.position, Vector3.down, 1.25f, _groundMask))
            {
                _grounded = true;
            }
        }
        if(_grounded) _rigidbody.useGravity = false;
        if(!_grounded)
        {
            _rigidbody.useGravity = true;
            return;
        }
        //player let go and therefore should slow down
        if(_playerInput == Vector3.zero)
        {
            if(_rigidbody.velocity.magnitude <= 1)
            {
                _rigidbody.velocity = Vector3.zero;
            }
            else
            {
                _rigidbody.velocity = _rigidbody.velocity/1.1f;
            }
        }
        //Picking up speed
        if(_rigidbody.velocity.magnitude < _MAX_SPEED)
        {
            _rigidbody.AddForce(transform.TransformDirection(_playerInput).normalized * _speedModifier, ForceMode.VelocityChange);
        }
        //At MAX speed
        if(_rigidbody.velocity.magnitude >= _MAX_SPEED)
        {
            _rigidbody.velocity = transform.TransformDirection(_playerInput).normalized*_MAX_SPEED;
        }
    }
    private void UpdateLookingAt()
    {
        Debug.DrawRay(_camera.transform.position, _camera.transform.forward * 10, Color.magenta, 1);
        if(Physics.Raycast(_camera.transform.position, _camera.transform.forward, out _lookingAtObject, 10, _gatherableMask))
        {
            Renderer _renderer;
            if(_lookingAtObject.collider.TryGetComponent<Renderer>(out _renderer))
            {
                _mat = _renderer.material;
                _mat.SetColor("_EmissionColor", Color.green);
                _mat.EnableKeyword("_EMISSION");
            }
        }else
        {
            if(_mat != null)
            {
                
                _mat.SetColor("_EmissionColor", Color.black);
                _mat.EnableKeyword("_EMISSION"); //This is neccesary due to Unity bug
                _lookingAtObject = new RaycastHit();
                _mat = null;
            }
        }
    }

    IEnumerator resetCanJump()
    {
        yield return new WaitForSeconds(0.5f);
        _canJump = true;
    }
}
