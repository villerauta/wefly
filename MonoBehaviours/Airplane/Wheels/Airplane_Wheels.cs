using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeFly {
    [RequireComponent(typeof(WheelCollider))]
    public class Airplane_Wheels : MonoBehaviour
    {

        [Header("Wheel Properties")]
        public Transform WheelGraphic;
        public bool isBraking = false;
        public float brakePower = 500f;
        public bool isSteering = false;
        public float steeringAngle = 20f;

        public WheelCollider WheelCol;
        private Vector3 worldPos;
        private Quaternion worldRot;

        private float originalStiffness;

        private float finalBrakeForce;

        public AirplaneWheelFX fX;

        float originalSusDistance;

        void Awake()
        {
            WheelCol = GetComponent<WheelCollider>();
            originalStiffness = WheelCol.forwardFriction.stiffness;
            originalSusDistance = WheelCol.suspensionDistance;
        }

        /*
        void Start() {
            WheelCol = GetComponent<WheelCollider>();
            originalStiffness = WheelCol.forwardFriction.stiffness;
        }
        */

        public void PrintDebug()
        {
            if (WheelCol)
            {
                Debug.Log(gameObject.name+ " breakTorque: " + WheelCol.brakeTorque);
                Debug.Log(gameObject.name+ " motorTorque: " + WheelCol.motorTorque);
                Debug.Log(gameObject.name+ " breakTorque: " + WheelCol.brakeTorque);
            }
        }

        public void InitWheel() {
            if (WheelCol) {
                Debug.Log("Wheel " + gameObject.name + " initialized");
                WheelCol.motorTorque = 0.001f;
            }
            else
            {
                Debug.LogError("No wheel collider found for initialization!");
            }
        }

        public void HandleWheel(BaseAirplane_Input input, bool handbrake) {
            if (WheelCol) {
                //Debug.Log("Motor torque is " + WheelCol.motorTorque);
                WheelCol.GetWorldPose(out worldPos, out worldRot);
                if (WheelGraphic) {
                    WheelGraphic.rotation = worldRot;
                    WheelGraphic.position = worldPos;
                }
                
                if (isBraking)
                {   
                    if (WheelCol.isGrounded)
                    {
                        fX.HandleBreakingFX(true);
                        fX.HandleRollingFX(true);
                    }
                    else
                    {
                        fX.HandleBreakingFX(false);
                        fX.HandleRollingFX(false);
                    }

                    WheelFrictionCurve curve = WheelCol.forwardFriction;
                    if (handbrake) {
                        curve.stiffness = 0.5f;
                        finalBrakeForce = Mathf.Lerp(finalBrakeForce, 1f * brakePower, Time.deltaTime);
                        WheelCol.brakeTorque = finalBrakeForce;
                        WheelCol.motorTorque = 0f;
                    }
                    else if (input.Brake > 0.1f) 
                    {
                        curve.stiffness = 0.5f;
                        finalBrakeForce = Mathf.Lerp(finalBrakeForce, input.Brake * brakePower, Time.deltaTime);
                        WheelCol.brakeTorque = finalBrakeForce;
                        WheelCol.motorTorque = 0f;
                    } 
                    else if (input.StickyThrottle > 0.5f){
                        curve.stiffness = originalStiffness;
                        finalBrakeForce = 0f;
                        WheelCol.brakeTorque = 0f;
                        WheelCol.motorTorque = 10f;
                    }
                    else if (input.Reverse > 0.5f)
                    {
                        WheelCol.motorTorque = -0.1f;
                        WheelCol.wheelDampingRate = 1f;
                    }
                    else
                    {
                        curve.stiffness = originalStiffness;
                        finalBrakeForce = 0f;
                        WheelCol.brakeTorque = 0f;
                        WheelCol.motorTorque = 0.001f;
                    }
                }
                

                if(isSteering) {
                    WheelFrictionCurve curve = WheelCol.forwardFriction;
                    float originalStiffness = curve.stiffness;
                    WheelCol.steerAngle = -input.Yaw * steeringAngle;

                    if (handbrake) {

                        finalBrakeForce = Mathf.Lerp(finalBrakeForce, 1f * brakePower, Time.deltaTime);
                        WheelCol.brakeTorque = finalBrakeForce;
                        WheelCol.motorTorque = 0f;
                    }
                    else if (input.Brake > 0.1f) 
                    {

                        finalBrakeForce = Mathf.Lerp(finalBrakeForce, input.Brake * brakePower, Time.deltaTime);
                        WheelCol.brakeTorque = finalBrakeForce;
                        WheelCol.motorTorque = 0f;
                    } 
                    else if (input.StickyThrottle > 0.5f){

                        finalBrakeForce = 0f;
                        WheelCol.brakeTorque = 0f;
                        WheelCol.motorTorque = 10f;
                    }
                    else
                    {
                        curve.stiffness = 1f;
                        finalBrakeForce = 0f;
                        WheelCol.brakeTorque = 0f;
                        WheelCol.motorTorque = 0.001f;
                    }
                }
            }
        }

        public void HandleHandbrake() {
            Debug.Log("Handle handbrake");
            finalBrakeForce = Mathf.Lerp(finalBrakeForce, 1f * brakePower, Time.deltaTime);
            WheelCol.brakeTorque = finalBrakeForce;
        }
        
    }
}