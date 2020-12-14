using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PushSound : MonoBehaviour
{
    public AudioSource audioPlayer;

    public AudioClip[] audioTracks;
    public bool pauseAudioOnPush;

    // Start is called before the first frame update
    void Start()
    {
        if (!audioPlayer)
        {
            audioPlayer = GetComponent<AudioSource>();
        }

        audioPlayer.clip = audioTracks[0];
    }

    // Update is called once per frame
    void Update() { }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !audioPlayer.isPlaying)
        {
            audioPlayer.Play();
            Debug.Log("PUSHING");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (pauseAudioOnPush)
            {
                audioPlayer.Pause();
            }
            else
            {
                audioPlayer.Stop();
            }

            Debug.Log("STOPPED PUSHING");
        }
    }
}
