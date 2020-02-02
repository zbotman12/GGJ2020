using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FMODVolume : MonoBehaviour
{
    FMOD.Studio.EventInstance seTestEvent;

    FMOD.Studio.Bus Master;
    FMOD.Studio.Bus Bgm;
    FMOD.Studio.Bus Se;
    float bgmVolume = 0.5f;
    float seVolume = 0.5f;
    float masterVolume = 1f;

    void Awake()
    {
        Bgm = FMODUnity.RuntimeManager.GetBus("bus:/Master/Bgm");
        Se = FMODUnity.RuntimeManager.GetBus("bus:/Master/Se");
        Master = FMODUnity.RuntimeManager.GetBus("bus:/Master");
        seTestEvent = FMODUnity.RuntimeManager.CreateInstance("event:/sfx/mouseclick");
    }

    void Update()
    {
        Bgm.setVolume(bgmVolume);
        Se.setVolume(seVolume);
        Master.setVolume(masterVolume);
    }

    public void MasterVolumeLevel(float newMasterVolume)
    {
        masterVolume = newMasterVolume;
    }

    public void BgmVolumeLevel(float newBgmVolume)
    {
        bgmVolume = newBgmVolume;
    }

    public void SeVolumeLevel(float newSeVolume)
    {
        seVolume = newSeVolume;

        FMOD.Studio.PLAYBACK_STATE pbState;
        seTestEvent.getPlaybackState(out pbState);
        if (pbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            seTestEvent.start();
        }
    }
}
