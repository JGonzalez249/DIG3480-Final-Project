using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public PlayerController playerController;

    public GameObject poweredShot;
    public float duration;

    public Transform shotSpawn;
    private float nextFire;
    private AudioSource sfx;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        if (playerControllerObject != null)
        {
            playerController = playerControllerObject.GetComponent<PlayerController>();
        }
        if (playerController == null)
        {
            Debug.Log("Cannot find 'PlayerController' script");
        }

        sfx = playerController.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

   /* void PoweredUp()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            sfx.Play();
        }
    }*/
}
