using UnityEngine;

public class AudioTrigger : MonoBehaviour {
    [SerializeField] AudioClip clip;
    [SerializeField] int layerFilter;
    [SerializeField] float playerDistanceThreshold = 2f;
    [SerializeField] bool isOneTimeOnly;

    bool hasPlayed;
    AudioSource audioSource;
    GameObject player;
    
    void Start() {

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = clip;

        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= playerDistanceThreshold)
        {
            RequestPlayAudioClip();
        }
    }

    void RequestPlayAudioClip()
    {
        if (isOneTimeOnly && hasPlayed)
        {
            return;
        }
        else
        {
            if (audioSource.isPlaying == false)
            {
                audioSource.Play();
                hasPlayed = true;
            }
        }
    }
    
    void OnDrawGizmos () {
        Gizmos.color = new Color(0, 255, 0, 0.5f);
        Gizmos.DrawWireSphere(transform.position, playerDistanceThreshold);
	}
}
