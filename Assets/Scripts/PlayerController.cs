using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private KeyCode leftInput, rightInput, leftInput2, rightInput2;
    [SerializeField] private float acceleration = 100, turnspeed = 100, minSpeed = 0, maxSpeed = 500, minAcceleration = 200, maxAcceleration = 500;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private Transform groundTransform;
    [SerializeField] private TakeDamage takeDamage;
    private float speed = 0;
    private Rigidbody rb;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        bool isGrounded = Physics.Linecast(transform.position, groundTransform.position, groundLayers);
        if (isGrounded && !takeDamage.isHurt)
        {
            float currentY = transform.eulerAngles.y;
            if (currentY > 180) currentY -= 360;

            if (Input.GetKey(leftInput) && currentY < 89 || Input.GetKey(leftInput2) && currentY < 89)
            {
                transform.Rotate(new Vector3(0, turnspeed * Time.deltaTime, 0), Space.Self);
            }
            if (Input.GetKey(rightInput) && currentY > -89 || Input.GetKey(rightInput2) && currentY > -89)
            {
                transform.Rotate(new Vector3(0, -turnspeed * Time.deltaTime, 0), Space.Self);
            }
        }

    }

    private float Remap(float oldMin, float oldMax, float newMin, float newMax, float oldValue)
    {
        float oldRange = (oldMax - oldMin);
        float newRange = (newMax - newMin);
        float newValue = (((oldValue - oldMin) / oldRange) * newRange + newMin);
        return newValue;
    }

    private void FixedUpdate()
    {
        if (takeDamage.isHurt)
        {
            return;
        }
        float angle = Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, 0));
        acceleration = Remap(0, 90, maxAcceleration, minAcceleration, angle);
        speed +=  acceleration * Time.fixedDeltaTime;
        speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
        animator.SetFloat("playerSpeed", speed);
        Vector3 velocity = transform.forward * speed * Time.fixedDeltaTime;
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
    }
}
