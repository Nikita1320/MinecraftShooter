using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputConroller : MonoBehaviour
{
    [SerializeField] private GameObject character;
    [SerializeField] private PlayerMovementController movementController;
    [SerializeField] private PlayerCombatSystem combatSystem;
    [SerializeField] private FirstPersonCameraController cameraController;
    [SerializeField] private Joystick movementJoystick;
    [SerializeField] private Joystick cameraJoystick;
    [SerializeField] private GameObject mobileControllerPanel;

    private void Start()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            mobileControllerPanel.SetActive(false);
        }
        else
        {
            mobileControllerPanel.SetActive(true);
        }
    }
    private void Update()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            var x = Input.GetAxis("Horizontal");
            var z = Input.GetAxis("Vertical");

            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            Vector2 direction = new Vector3(x, z);
            movementController.Move(direction);

            cameraController.RotateCamera(mouseX, mouseY);
            if (Input.GetKey(KeyCode.Mouse0))
            {
                combatSystem.Shoot();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                combatSystem.StartReload();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                movementController.Jump();
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                combatSystem.StartThrowGrenade();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
                combatSystem.SelectWeapon(0);
            if (Input.GetKeyDown(KeyCode.Alpha2))
                combatSystem.SelectWeapon(1);
            if (Input.GetKeyDown(KeyCode.Alpha3))
                combatSystem.SelectWeapon(2);
            if (Input.GetKeyDown(KeyCode.Alpha4))
                combatSystem.SelectWeapon(3);
            if (Input.GetKeyDown(KeyCode.Alpha5))
                combatSystem.SelectWeapon(4);
            if (Input.GetKeyDown(KeyCode.Alpha6))
                combatSystem.SelectWeapon(5);
            if (Input.GetKeyDown(KeyCode.Alpha7))
                combatSystem.SelectWeapon(6);
            if (Input.GetKeyDown(KeyCode.Alpha8))
                combatSystem.SelectWeapon(7);
            if (Input.GetKeyDown(KeyCode.Alpha9))
                combatSystem.SelectWeapon(8);
        }
        else
        {
            movementController.Move(movementJoystick.Direction);
            cameraController.RotateCamera(cameraJoystick.Direction.x, cameraJoystick.Direction.y);
        }
    }
    public void Init(GameObject character)
    {
        this.character = character;
        movementController = character.GetComponent<PlayerMovementController>();
        combatSystem = character.GetComponent<PlayerCombatSystem>();
    }
}
