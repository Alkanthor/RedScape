using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class CarController : MonoBehaviour {

    public UnityEvents.UnityEventBoolTransform IsCardGrabbed;

    public float maxAcceleration = 3f;
    public Transform GrabCardPoint; 

    private float acceleration = 0.05f;
    private float movementSpeed = 0f;
    private float rotationSpeed = 180f;
    private Vector2 touchAxis;
    private float triggerAxis;

    private Rigidbody rb;
    private Vector3 defaultPosition;
    private Quaternion defaultRotation;

    private ParentCollision _parentCollision;
    private bool isOnGround = true;
    private GameObject _idCard;
    public void SetCarAxis(object sender, ControllerInteractionEventArgs e)
    {
        touchAxis = e.touchpadAxis;
    }

    public void StopCar(object sender, ControllerInteractionEventArgs e)
    {
        touchAxis = Vector2.zero;
    }

    public void ResetCar(object sender, ControllerInteractionEventArgs e)
    {
        transform.position = defaultPosition;
        transform.rotation = defaultRotation;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        defaultPosition = transform.position;
        defaultRotation = transform.rotation;
    }

    private void Start()
    {
        if (IsCardGrabbed == null) IsCardGrabbed = new UnityEvents.UnityEventBoolTransform();
        _parentCollision = this.GetComponent<ParentCollision>();
        _parentCollision.OnChildTriggerEnter.AddListener(CardGrabbed);
        _parentCollision.OnChildTriggerExit.AddListener(CardUngrabbed);
        PlayerManager.Instance.OnCarMove.AddListener(SetCarAxis);
        PlayerManager.Instance.OnCarReset.AddListener(ResetCar);
        PlayerManager.Instance.OnCarStop.AddListener(StopCar);

        //for testing
        PlayerManager.Instance.LeftControllerType = UnityEnums.ControllerType.CAR_CONTROLLER;
    }

    public void SetIdCard(GameObject idCard)
    {
        Debug.Log("Id card Set " + idCard);
        _idCard = idCard;
    }
    private void CardGrabbed(GameObject child, Collider collider)
    {
        if(collider.gameObject.name == _idCard.name)
        {
            IsCardGrabbed.Invoke(true, GrabCardPoint);
        }
    }

    private void CardUngrabbed(GameObject child, Collider collider)
    {
        if (collider.gameObject.name == _idCard.name)
        {
            IsCardGrabbed.Invoke(false, GrabCardPoint);
        }
    }
    private void FixedUpdate()
    {
        if(!isOnGround)
        {
           // touchAxis = Vector2.zero;
        }

        CalculateSpeed();
        Move();
        Turn();
    }

    private void CalculateSpeed()
    {
        if (touchAxis.y != 0f)
        {
            movementSpeed += (acceleration * touchAxis.y);
            movementSpeed = Mathf.Clamp(movementSpeed, -maxAcceleration, maxAcceleration);
        }
        else
        {
            Decelerate();
        }
    }

    private void Decelerate()
    {
        if (movementSpeed > 0)
        {
            movementSpeed -= Mathf.Lerp(acceleration, maxAcceleration, 0f);
        }
        else if (movementSpeed < 0)
        {
            movementSpeed += Mathf.Lerp(acceleration, -maxAcceleration, 0f);
        }
        else
        {
            movementSpeed = 0;
        }
    }

    private void Move()
    {
        Vector3 movement = transform.forward * movementSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }

    private void Turn()
    {
        float turn = touchAxis.x * rotationSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    private void OnTriggerStay(Collider collider)
    {
        isOnGround = true;
    }

    private void OnTriggerExit(Collider collider)
    {
        isOnGround = false;
    }
}
