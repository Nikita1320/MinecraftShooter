using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputConroller : MonoBehaviour
{
    [SerializeField] private GameObject character;
    [SerializeField] private PlayerMovementController movementController;
    [SerializeField] private PlayerCombatSystem combatSystem;
    [SerializeField] private FirstPersonCameraController cameraController;

    private void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 direction = new Vector3(x, 0, z);
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
    public void Init(GameObject character)
    {
        this.character = character;
        movementController = character.GetComponent<PlayerMovementController>();
        combatSystem = character.GetComponent<PlayerCombatSystem>();
    }
}
