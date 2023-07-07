using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] float moveForce;
    [SerializeField] float rotationForce;
    [SerializeField] float audioVolume;
    [SerializeField] AudioClip engine;
    [SerializeField] ParticleSystem enginep;
    [SerializeField] ParticleSystem sidepleft;
    [SerializeField] ParticleSystem sidepright;

    Rigidbody rb;
    AudioSource audioSource;
   
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrust();
        }
        else
        {
            StopThrust();
        }

    }

    private void StopThrust()
    {
        enginep.Stop();
        audioSource.Stop();
    }

    private void StartThrust()
    {
        rb.AddRelativeForce(Vector3.up * moveForce * Time.deltaTime);
        if (!audioSource.isPlaying)
        { audioSource.PlayOneShot(engine, audioVolume); }
        if (!enginep.isPlaying)
        { enginep.Play(); }
    }

    void ProcessRotation()
    {


        if (Input.GetKey(KeyCode.A))
        {              
            applyRotation(rotationForce);
            
            if (!sidepleft.isPlaying)
            {
                sidepleft.Play();
            }                      
        }
        
        else if (Input.GetKey(KeyCode.D))
        {            
            if(!sidepright.isPlaying)
            { sidepright.Play();}
            applyRotation(-rotationForce);           
        }

        else
        {
            StopRotation();
        }

        void applyRotation (float rotationvalue)
        {
            rb.freezeRotation = true; // freeze the rotation so we can manually rotate, without the physics being involved
            transform.Rotate(Vector3.forward * rotationvalue * Time.deltaTime);
            rb.freezeRotation = false; // unfreeze the rotation so the physics can take over;

            
        }


    }

    private void StopRotation()
    {
        sidepleft.Stop();
        sidepright.Stop();
    }
}
