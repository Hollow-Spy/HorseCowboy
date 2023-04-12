using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseMovement : MonoBehaviour
{
    //https://www.youtube.com/watch?v=8ZxVBCvJDWk

    [SerializeField] Rigidbody rb;
    [SerializeField] float Speed;
    [SerializeField] float MaxSpeed,MinSpeed;
    [SerializeField] float Acceleration;
    [SerializeField] float BreakingDrag;
    
    Vector3 _Input;
    public float SpeedMultiplier = 1;
    void GatherInput()
    {
        _Input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }
    private void Start()
    {
        Speed = MinSpeed;
    }



    void Move()
    {

        Vector3 NormalInput = _Input;

        NormalInput = Vector3.ClampMagnitude(NormalInput, 1);

        //Vector3 direction = transform.position + (transform.right * NormalInput.magnitude) * (Speed * SpeedMultiplier) * Time.deltaTime;

        //  rb.AddForce(direction * 5,ForceMode.Acceleration);
        if(_Input != Vector3.zero)
        {
            rb.AddForce(transform.right * Speed, ForceMode.Force);
            rb.drag = 0;
            Speed += Acceleration;
           

        }
        else
        {
            rb.drag = BreakingDrag;
            Speed -= Acceleration;
        }
        Speed = Mathf.Clamp(Speed, MinSpeed, MaxSpeed);
    }


    private void FixedUpdate()
    {
        Move();
    }

    void Look()
    {
    

        switch (_Input.x)
        {
            case 0:
                switch (_Input.z)
                {
                    case 1:
                        transform.eulerAngles = new Vector3(0, 315, 0);
                        break;
                    case -1:
                        transform.eulerAngles = new Vector3(0, 135, 0);
                        break;

                }
                break;
            case 1:
                switch (_Input.z)
                {
                    case 0:
                        transform.eulerAngles = new Vector3(0, 45, 0);
                        break;
                    case 1:
                        transform.eulerAngles = new Vector3(0, 0, 0);
                        break;
                    case -1:
                        transform.eulerAngles = new Vector3(0, 90, 0);
                        break;
                }
                break;
            case -1:
                switch (_Input.z)
                {
                    case 0:
                        transform.eulerAngles = new Vector3(0, 225, 0);
                        break;
                    case 1:
                        transform.eulerAngles = new Vector3(0, 270, 0);
                        break;
                    case -1:
                        transform.eulerAngles = new Vector3(0, 180, 0);
                        break;
                }
                break;
        }




    }



    // Update is called once per frame
    void Update()
    {
        GatherInput();

        Look();


    }
}
