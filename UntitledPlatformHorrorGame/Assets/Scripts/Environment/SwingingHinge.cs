using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Envrionment
{

    public class SwingingHinge : MonoBehaviour
    {
        public float maxMotorSpeed;
        public int startDirection = 1;
        public AnimationCurve motorSpeedCurve;
        public HingeJoint2D HingeJoint2D;

        float currentTargetMotorSpeed;
        float currentTargetAngle;
        JointMotor2D motor;

        private void Start()
        {
            currentTargetMotorSpeed = maxMotorSpeed*startDirection;
            currentTargetAngle = HingeJoint2D.limits.max;
        }

        // Update is called once per frame
        void Update()
        {
            if (HingeJoint2D.limits.max - HingeJoint2D.jointAngle < 0.1)
            {
                currentTargetMotorSpeed = -maxMotorSpeed;
                currentTargetAngle = HingeJoint2D.limits.min;
            }

            if (HingeJoint2D.limits.min - HingeJoint2D.jointAngle > -0.1)
            {
                currentTargetMotorSpeed = maxMotorSpeed;
                currentTargetAngle = HingeJoint2D.limits.max;
            }

            motor = HingeJoint2D.motor;
            motor.motorSpeed = motorSpeedCurve.Evaluate(1 - Mathf.Abs(currentTargetAngle - HingeJoint2D.jointAngle) / (HingeJoint2D.limits.max + Mathf.Abs(HingeJoint2D.limits.min))) * currentTargetMotorSpeed;
            HingeJoint2D.motor = motor;

        }
    }

}
