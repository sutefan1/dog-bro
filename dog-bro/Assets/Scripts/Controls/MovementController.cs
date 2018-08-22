﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    public SteamVR_TrackedController controller;

    public Transform cameraRigTransform;
    public Transform headTransform;

    private SteamVR_Controller.Device device;
    private GameController gameController;

    private void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void OnEnable()
    {
        //controller.TriggerClicked -= HandleTriggerClicked;
        //controller.TriggerClicked += HandleTriggerClicked;

        device = SteamVR_Controller.Input((int)controller.controllerIndex);
    }

    void HandleTriggerClicked(object sender, ClickedEventArgs e)
    {
        gameController.RightTriggerClicked();
    }

    void FixedUpdate()
    {
          device = SteamVR_Controller.Input((int)controller.controllerIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
        float movementSpeed = 1f;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            cameraRigTransform.Translate(new Vector3(movementSpeed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            cameraRigTransform.Translate(new Vector3(-movementSpeed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            cameraRigTransform.Translate(new Vector3(0, 0, -movementSpeed * Time.deltaTime));
            //transform.Translate(new Vector3(0, movementSpeed * Time.deltaTime, 0));
            //transform.Translate(new Vector3(0, -movementSpeed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            cameraRigTransform.Translate(new Vector3(0, 0, movementSpeed * Time.deltaTime));
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            gameController.RightTriggerClicked();
        }

        /*
        if (controller.padPressed) {

            float yAxis = device.GetAxis().y;

            float movementSpeed = 1f;

            if(yAxis < -0.5)
            {
                movementSpeed *= -1;
            }

            Vector3 headForward = headTransform.forward;
            headForward.y = 0;

            cameraRigTransform.position += headForward * movementSpeed * Time.deltaTime;

        }*/
        
    }
}
