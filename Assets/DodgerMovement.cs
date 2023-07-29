using ICO321;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ICO321
{
    public class DodgerMovement : MonoBehaviour
    {
        public float movementWidth;
        public float speed;

        private float timeIndex;

        private Vector3 leftPoint;
        private Vector3 rightPoint;

        private Rigidbody2D rb;

        // Start is called before the first frame update
        void Start()
        {
            timeIndex = 0.5f;
            leftPoint = transform.position + (transform.right * (movementWidth * -1 / 2));
            rightPoint = transform.position + (transform.right * (movementWidth / 2));
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            timeIndex += Time.deltaTime;
            rb.MovePosition(Vector3.Lerp(leftPoint, rightPoint, Mathf.PingPong(timeIndex, 1.0f)));
        }
    }
}