using UnityEngine;
using UnityEngine.UI;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    [SerializeField] private AudioClip successSound;
    [SerializeField] private GameObject OnboardingUI;
    private Transform currentCheckpoint;//the lastest checkpoint
    private Health savedPlayerHealth;

    public void Awake()
    {
        savedPlayerHealth = GetComponent<Health>();
    }

    public void Respawn()
    {
        //position, health and reset animation
        transform.position = currentCheckpoint.position;
        savedPlayerHealth.PlayerRespawn();

        //move camera to checkpoint room
        //checkpoint has to be the child of the room object
        Camera.main.GetComponent<CameraMovement>().MoveToNewRoom(currentCheckpoint.parent);

    }

    //Activate checkpoints

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
            SoundFXManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false; //Deactivate checkpoint collider
            collision.GetComponent<Animator>().SetTrigger("appear");//play the animation
        }

        if(collision.transform.tag == "End")
        {
            SoundFXManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false; //Deactivate checkpoint collider
            collision.GetComponent<Animator>().SetTrigger("pressed");//play the animation
        }

        if(collision.transform.tag == "Finish")
        {
            OnboardingUI.SetActive(false);
        }
    }
}
