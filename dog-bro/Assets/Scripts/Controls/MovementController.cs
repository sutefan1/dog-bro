using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    public SteamVR_TrackedController controller;

    public Transform cameraRigTransform;
    public Transform headTransform;

    private SteamVR_Controller.Device device;
    private GameController gameController;
    private PlayerController playerController;

    private void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        controller.TriggerClicked -= HandleTriggerClicked;
        controller.TriggerClicked += HandleTriggerClicked;

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
        //Disable player movement when the Audio is playing
        if (gameController.isIntroPlaying)
        {
            return;
        }

        float movementSpeed = 1f;

        bool characterHasMoved = false;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            cameraRigTransform.Translate(new Vector3(movementSpeed * Time.deltaTime, 0, 0));
            characterHasMoved = true;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            cameraRigTransform.Translate(new Vector3(-movementSpeed * Time.deltaTime, 0, 0));
            characterHasMoved = true;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            cameraRigTransform.Translate(new Vector3(0, 0, -movementSpeed * Time.deltaTime));
            characterHasMoved = true;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            cameraRigTransform.Translate(new Vector3(0, 0, movementSpeed * Time.deltaTime));
            characterHasMoved = true;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            gameController.RightTriggerClicked();
        }

        
        if (controller.padPressed) {

            float yAxis = device.GetAxis().y;

            if(yAxis < -0.5)
            {
                movementSpeed *= -1;
            }

            Vector3 headForward = headTransform.forward;
            headForward.y = 0;

            cameraRigTransform.position += headForward * movementSpeed * Time.deltaTime;
            characterHasMoved = true;
        }

        playerController.CharacterIsMoving(characterHasMoved);
        
    }
}
