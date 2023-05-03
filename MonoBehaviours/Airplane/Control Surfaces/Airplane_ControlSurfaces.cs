using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane_ControlSurfaces : MonoBehaviour
{
    public enum ControlSurfaceType
    {
        Rudder,
        Elevator,
        Flap,
        Aileron
    }

    #region Variables
    [Header("Control Surface Properties")]
    public ControlSurfaceType type = ControlSurfaceType.Rudder;
    public float maxAngle = 30f;
    //public Transform controlSurfaceGraphic;
    public Vector3 axis = Vector3.right;
    public float smoothSpeed = 1f;

    private float wantedAngle;
    private Vector3 initialAngle;
    private Quaternion initRotation;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        initialAngle = this.transform.localRotation.eulerAngles;
        initRotation = this.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 finalAngleAxis = initialAngle + (axis * wantedAngle);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(finalAngleAxis), Time.deltaTime * smoothSpeed);
    }

    public void HandleControlSurface(WeFly.BaseAirplane_Input input)
    {
        float inputValue = 0f;

        switch (type)
        {
            case ControlSurfaceType.Rudder:
                inputValue = input.Yaw;
                break;
            case ControlSurfaceType.Elevator:
                inputValue = input.Pitch;
                break;
            case ControlSurfaceType.Flap:
                inputValue = input.Flaps;
                break;
            case ControlSurfaceType.Aileron:
                inputValue = input.Roll;
                break;
            default:
                break;
        }
        wantedAngle = maxAngle * inputValue;                        
    }
}
