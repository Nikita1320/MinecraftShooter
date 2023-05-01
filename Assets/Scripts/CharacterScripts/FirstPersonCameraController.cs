using UnityEngine;

public class FirstPersonCameraController : MonoBehaviour
{
    [SerializeField] private float mouseSensivity = 1f;
    [SerializeField] private GameObject character;
    [SerializeField] private UnityEngine.UI.Slider mouseSensivitySlider;
    private float xRotation = 0;

    public void RotateCamera(float mouseX, float mouseY)
    {
        xRotation -= mouseY * (mouseSensivity + mouseSensivity * mouseSensivitySlider.value * 3 * Time.deltaTime);
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        character.transform.Rotate(Vector3.up * mouseX * (mouseSensivity + mouseSensivity * mouseSensivitySlider.value * 3 * Time.deltaTime));
    }
}
