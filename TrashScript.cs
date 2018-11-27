using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashScript : MonoBehaviour {
    public AudioSource colissionSound;
    public ParticleSystem effect;

    //Make it rotate
    void Update () {
        transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime * 10);
    }

    private void OnCollisionEnter(Collision collision)
    {
        colissionSound.Play();
        effect.Play();
    }
}
