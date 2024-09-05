using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MenuController : MonoBehaviour
{

    public VideoPlayer player;
    public GameObject menuOpcoes, rawImage;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rawImage.SetActive(false);
        animator = rawImage.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.isPlaying && Input.anyKeyDown)
        {
            Debug.Log("funfo");

            player.Play();
            animator.SetTrigger("Fade");
            rawImage.SetActive(true);
            menuOpcoes.SetActive(true);
        }
    }
}
