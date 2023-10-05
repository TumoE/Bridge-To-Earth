using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Script;

    [SerializeField] private Camera _camera;
    [SerializeField] private Animator _animator;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _gravityForce;
    [SerializeField] private float _rotationSpeed;

    private CharacterController _character;
    private PlayerAudioManager _playerAudio;

    private Vector3 _moveVector;

    private float _gravity;
    private float _vertical;
    private float _horizontal;
    private float _slowMoveStep = 1;

    float angle;

    private void Awake()
    {
        Script = this;
    }

    private void Start()
    {
        ScriptInit();
    }

    private void Update()
    {
        GetDirection();
        
        Rotation();
        Gravity();
    }


    private void GetDirection()
    {
        if (!Values.CanMove || !Values.CanPlay) return;

        if (Input.GetKeyDown(KeyCode.W))
        {
            _playerAudio.SetActiveWalkSound(true);
        }
        if (Input.GetKey(KeyCode.W))
        {
            _moveVector = Quaternion.Euler(0, transform.rotation.y, 0) * (transform.forward + transform.right * Input.GetAxisRaw("Horizontal") / 2);
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            _playerAudio.SetActiveWalkSound(false);
            _moveVector.x = 0;
            _moveVector.z = 0;
        }

        Move();
    }
    private void Move()
    {
        _moveVector.Normalize();
        _moveVector.y = _gravity;

        _animator.SetBool(Values.RunTag,  Mathf.Abs(_moveVector.x) + Mathf.Abs(_moveVector.z) > 0.01);
        _character.Move(_moveVector * _moveSpeed * Time.deltaTime * _slowMoveStep);
    }
    private void Rotation()
    {
        if (!Values.CanRotation) return;

        Ray mouseRay = _camera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position);
        
        if (plane.Raycast(mouseRay, out float hitDist))
        {
            Vector3 hitPoint = mouseRay.GetPoint(hitDist);
            transform.LookAt(hitPoint);
        }
    }
    
    public void SlowDown(bool isSlowing)
    {
        if (isSlowing)
        {
            _slowMoveStep = 0.6f;
        }
        else _slowMoveStep = 1;
    }
    public void Stop()
    {
        _playerAudio.SetActiveWalkSound(false);
        _moveVector = Vector3.zero;
        _character.Move(Vector3.zero);
    }
    
    private void Gravity()
    {
        if (_character.isGrounded) _gravity = -1;
        else _gravity += _gravityForce * Time.deltaTime;
    }

    private void ScriptInit()
    {
        _playerAudio = PlayerAudioManager.Script;
        _character = GetComponent<CharacterController>();
    }
}
