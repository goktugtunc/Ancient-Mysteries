using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class CharacterController2 : MonoSingleton<CharacterController2>
{
    public FixedJoystick Joystick;
    public CharacterController _cc;
    public NavMeshAgent Agent;
    private float _horizontal;
    private float _vertical;
    public float Gravity = -9.81f;
    public float speed;
    float velocity;
    public float Mass=1f;
    Vector3 move = Vector3.zero;
    public Transform Cam;
    public AudioSource WalkSound;
    private Vector3 previousMove;
    private bool PlaySound;

    void Start()
    {
        WalkSound.volume = PlayerPrefs.GetFloat("audioVolume");
        PlaySound = false;
        _cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        _horizontal = Joystick.Horizontal;
        _vertical = Joystick.Vertical;

        GravityCharacter();
        RotationCharacter();
        MoveCharacter();
    }

    private void GravityCharacter()
    {
        if(_cc.isGrounded && velocity<=0.1f)
        {
            velocity=-1f;
        }
        else
        {
            velocity += Gravity * Mass * Time.deltaTime;
        }
    }

    IEnumerator WalkSoundPlayer()
    {
        WalkSound.Play();
        yield return new WaitForSeconds(11f);
        PlaySound = false;
    }

    IEnumerator StopSound()
    {
        while (WalkSound.volume > 0)
        {
            WalkSound.volume -= 0.1f;
            yield return null;
        }
        WalkSound.Stop();
        WalkSound.volume = PlayerPrefs.GetFloat("audioVolume");
    }

    private void MoveCharacter(){
        if (_horizontal != 0 && _vertical != 0 && PlaySound == false)
        {
            PlaySound = true;
            StartCoroutine(WalkSoundPlayer());
        }
        if (_horizontal == 0 && _vertical == 0 && PlaySound == true)
        {
            StartCoroutine(StopSound());
            PlaySound = false;
        }
        move=new Vector3(_horizontal,velocity,_vertical)*Time.deltaTime*speed;
        _cc.Move(transform.TransformDirection(move));
    }

    private void RotationCharacter()
    {
        if(_horizontal!=0 || _vertical!=0){
            Quaternion target = Quaternion.Euler(0, Cam.transform.rotation.eulerAngles.y, 0);
            transform.rotation=Quaternion.Lerp(transform.rotation,target,0.3f);
        }
    }
}