using System;
using UnityEngine;

namespace AVA.Movement
{
    public class RotateTowardsDirection : MonoBehaviour
    {
        public Vector3 targetDirection;
        public float rotationSpeed = 10f;

        public float angle;

        void Update()
        {
            Ray mouseRay = GetMouseRay();
            Debug.DrawRay(mouseRay.origin, mouseRay.direction * 100f, Color.red);

            targetDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            // Calculate the angle between the current forward direction and the target direction
            angle = Vector3.SignedAngle(transform.forward, targetDirection, Vector3.up);

            // Rotate the character towards the target direction
            transform.Rotate(Vector3.up, angle * Time.deltaTime * rotationSpeed);
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

    }
}