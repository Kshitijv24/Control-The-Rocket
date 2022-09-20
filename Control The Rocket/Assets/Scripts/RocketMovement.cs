using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    [SerializeField] private float mainThrust;
    [SerializeField] private float rotationThrust;

    private Rigidbody rb;
    private AudioSource audioSource;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

            if (!audioSource.isPlaying)
            {
                PlayAudio();
            }
        }
        else
        {
            StopAudio();
        }
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate the rocket.

        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);

        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over.
    }

    private void PlayAudio()
    {
        audioSource.Play();
    }

    private void StopAudio()
    {
        audioSource.Stop();
    }
}
