using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public float moveSpeed = 5f;
    public float rotationSpeed = 360f;
    public int stage = 0;
    public int score = 0;
    
    private CharacterController charCtrl = null;
    private Animator anim = null;
    private void Start()
    {
        charCtrl = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        
        if (Instance == null) 
        {
            Instance = this;  
            DontDestroyOnLoad(gameObject);  
        }
        else  
            Destroy(this.gameObject); 
    }
    
    private void Update()
    {
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (dir.sqrMagnitude > 0.01f)
        {
            Vector3 forward = Vector3.Slerp(transform.forward, dir,
                rotationSpeed * Time.deltaTime / Vector3.Angle(transform.forward, dir));
            transform.LookAt(transform.position + forward);
        }

        charCtrl.Move(dir * moveSpeed * Time.deltaTime);
        anim.SetFloat("Speed", charCtrl.velocity.magnitude);
        if (GameObject.FindGameObjectsWithTag("Dot").Length < 1 && stage == 0)
        {
            SceneManager.LoadScene("SampleScene 1");
            stage = 1;
        }
        else if (GameObject.FindGameObjectsWithTag("Dot").Length < 1 && stage == 1)
        {
            
            SceneManager.LoadScene("Win");
            
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Dot":
                score++;
                Destroy(other.gameObject);
                break;
            case "Enemy":
                SceneManager.LoadScene("Lose");
                score = 0;
                break;
        }
    }
}
