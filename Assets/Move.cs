using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] float moveForce;
    [SerializeField] float rotationForce;
    Rigidbody rb;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * moveForce * Time.deltaTime);         
        }
    }


    void ProcessRotation()
    {


        if (Input.GetKey(KeyCode.A))
        {
            applyRotation(rotationForce);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            applyRotation(-rotationForce);
        }

        void applyRotation (float rotationvalue)
        {
            rb.freezeRotation = true; // freeze the rotation so we can manually rotate, without the physics being involved
            transform.Rotate(Vector3.forward * rotationvalue * Time.deltaTime);
            rb.freezeRotation = false; // unfreeze the rotation so the physics can take over;

            
        }


    }

}
