using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsSound : MonoBehaviour
{
    public AudioSource buttonClickSound;
    public AudioClip clickSound;

    public void ClickSound()
    {
        buttonClickSound.PlayOneShot(clickSound);
    }
}
