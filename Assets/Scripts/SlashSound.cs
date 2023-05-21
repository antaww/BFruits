using UnityEngine;

public class SlashSound : MonoBehaviour
{
    public AudioSource slashSound;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fruit"))
        {
            slashSound.Play();
        }
    }
}
