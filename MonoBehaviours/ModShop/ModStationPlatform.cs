using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModStationPlatform : MonoBehaviour
{
    private bool _rotateActive;
    private Vector2 _lookInput;
    private Transform platformTransform;

    private GameObject airplane;
    public InputMaster input;
    public float rotateSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        input = new InputMaster();
        input.Player.Enable();
        input.Airplane.Disable();
        platformTransform = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        _rotateActive = input.Player.ControlModifier.IsPressed(); // 0 = left mouse btn or 1 = right

        if (_rotateActive)
        {
            _lookInput = input.Player.LookAround.ReadValue<Vector2>();
            platformTransform.Rotate(0, -(_lookInput.x * Time.deltaTime * rotateSpeed) ,0);
            airplane.transform.Rotate(0, -(_lookInput.x * Time.deltaTime * rotateSpeed) ,0);
        }
    }

    public void SetAirplane(GameObject plane)
    {
        airplane = plane;
    }
}
