using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSounds : MonoBehaviour
{
    [SerializeField]
    bool BushSound, WoodSound, CouchSound, SackSound, WateringCanSound;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ProjectilePickup")
        {
            if (BushSound)
            {
                SoundPlay.PlaySound(SoundPlay.Sound.bush);
            }
            if (WoodSound)
            {
                SoundPlay.PlaySound(SoundPlay.Sound.wood);
            }
            if (CouchSound)
            {
                SoundPlay.PlaySound(SoundPlay.Sound.couch);
            }
            if (SackSound)
            {
                SoundPlay.PlaySound(SoundPlay.Sound.sack);
            }
            if (WateringCanSound)
            {
                SoundPlay.PlaySound(SoundPlay.Sound.wateringCan);
            }
        }
    }
}
