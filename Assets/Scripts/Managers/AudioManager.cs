using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Linq;

public class AudioManager : MonoBehaviour
{

    [SerializeField] EventReference mainMusicRef;
    [SerializeField] EventReference paperCrumplingRef;
    [SerializeField] EventReference reflectSoundRef;
    [SerializeField] EventReference hurtSoundRef;
    [SerializeField] EventReference fireSoundRef;
    [SerializeField] EventReference uiClickRef;
    [SerializeField] EventReference confirmUIRef;

    
    private Transform PlayerTransform; // Needed?

    // Event Instances.

    EventInstance mainMusicInst;

    void Awake()
    {
        // Create Instances.
        mainMusicInst = RuntimeManager.CreateInstance(mainMusicRef);
        // PlayMainMusic();
    }



    void OnDestroy()
    {
        // Kill Eventinstances?

    }

    //  public void Initialize(GlobalEventManager globalEventManager, InkManager inkManager)
    // {
    //     SetupListeners(globalEventManager);
    //     // Init/Update the buses.
    //     UpdateBusses();
    // }

    //  private void SetupListeners(GlobalEventManager globalEventManager) 
    // {

    //     // Reference the Global event manager to reduce access typing.
    //     GlobalEventManager eventManagerRef =  globalEventManager;

    //     eventManagerRef.AddListener(GlobalEventManager.EventTypes.MainLevelLoaded, PlayWindAmbience);
    //     eventManagerRef.AddListener(GlobalEventManager.EventTypes.MainLevelQuit, CutWorldWorldAmbience);
    //     eventManagerRef.AddListener(GlobalEventManager.EventTypes.PlayerCreated, HandleSetBudManager);
    //     eventManagerRef.AddListener(GlobalEventManager.EventTypes.NewNotification, HandleUINotification);
    //     eventManagerRef.AddListener(GlobalEventManager.EventTypes.UpgradeOccured, PlayUpgradeSound);
    //     eventManagerRef.AddListener(GlobalEventManager.EventTypes.WaterFull, PlayResourceFullSound);
    //     eventManagerRef.AddListener(GlobalEventManager.EventTypes.SolarFull, PlayResourceFullSound);
    // }


    // private void Update()
    // {
    //     // TODO -- Update Events that need continual updating :
    //     // - Ambient, generative soundtrack based on _budManager.currentMovementState and _budManager.biomeCurrentlyIn
    //     // 
    //     UpdatePlayerMovementState();
    //     UpdateBiomePlayerIsIn();
    //     CheckCurrentVoiceEventStatus();
    // }


    // private void UpdateBiomePlayerIsIn()
    // {
    //     if (_budManager == null)
    //     {
    //         return;
    //     }

    //     string biomeCurrentlyIn;

    //     switch (_budManager.biomeCurrentlyIn)
    //     {
    //         case WorldManager.Biomes.OpenLand:
    //             biomeCurrentlyIn = "OpenLand";
    //             break;
    //         case WorldManager.Biomes.DarkForest:
    //             biomeCurrentlyIn = "DarkForest";
    //             break;
    //         case WorldManager.Biomes.LightForest:
    //             biomeCurrentlyIn = "LightForest";
    //             break;
    //         case WorldManager.Biomes.Bog:
    //             biomeCurrentlyIn = "Bog";
    //             break;
    //         default:
    //             biomeCurrentlyIn = "OpenLand";
    //             break;
    //     }
    //     SetGlobalLabelledParam("BiomePlayerIsIn", biomeCurrentlyIn);
    // }

    //  public void PlayInstance(string eventName)
    // {
    //     if (audioEventInstances.ContainsKey(eventName) == false) {
    //         Debug.LogWarning($"AudioManager:: Event instance for {eventName} does not exist.");
    //         return;
    //     }
    //     if (audioEventInstances[eventName].isValid())
    //     {
    //         audioEventInstances[eventName].start();
    //         Debug.Log($"AudioManager:: Playing {eventName}");
    //     }
    //     else
    //     {
    //         Debug.LogError($"AudioManager:: Event instance for {eventName} is not valid.");
    //     }
    // }

    // public void StopInstance(string eventName, bool allowFadeOut)
    // {
    //     if (audioEventInstances.ContainsKey(eventName) == false) {
    //         Debug.LogWarning($"AudioManager:: Event instance for {eventName} does not exist.");
    //         return;
    //     }
    //     if (audioEventInstances[eventName].isValid())
    //     {
    //         if (allowFadeOut)
    //         {
    //             audioEventInstances[eventName].stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    //         } else {
    //             audioEventInstances[eventName].stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    //         }
    //         Debug.Log($"AudioManager::Stopping {eventName}");
    //     }
    //     else
    //     {
    //         Debug.LogError($"AudioManager::Event instance for {eventName} is not valid.");
    //     }
    // }

    public void StopAllInstances()
    {

    }

    

    


    public void SetGlobalFloatParam(string paramName, float value)
    {
        RuntimeManager.StudioSystem.setParameterByName(paramName, value);
    }
    public void SetGlobalLabelledParam(string paramName, string value)
    {
        RuntimeManager.StudioSystem.setParameterByNameWithLabel(paramName, value);
    }

    // public float GetParam(string eventName, string paramName)
    // {
    //     if (audioEventInstances.ContainsKey(eventName) == false) {
    //         Debug.LogWarning($"Event instance for {eventName} does not exist.");
    //         return -1f;
    //     }
    //     if (audioEventInstances[eventName].isValid())
    //     {
    //         audioEventInstances[eventName].getParameterByName(paramName, out float value);
    //         return value;
    //     }
    //     else
    //     {
    //         Debug.LogError($"Event instance for {eventName} is not valid.");
    //     }
    //     return -1f;
    // }

    // public float GetParam(string paramName)
    // {
    //     return RuntimeManager.StudioSystem.getParameterByName(paramName, out float value) == FMOD.RESULT.OK ? value : -1f;
    // }


    public void playOneShot (EventReference eventRef)
    {
        RuntimeManager.PlayOneShot(eventRef);
    }
    public void playOneShotAtPosition (EventReference eventRef, Vector3 position)
    {
        RuntimeManager.PlayOneShot(eventRef, position);
    }


    // Helper Functions to Play Specific Audio Events:

    // public void PlayMainMusic (object arg0)
    public void PlayMainMusic ()
    {
        // TODO REMOVE - This is just for testing:
        // PlayInstance(Base_World_Drones);
        mainMusicInst.start();
    }
    public void StopMainMusic ()
    {
        // TODO REMOVE - This is just for testing:
        // PlayInstance(Base_World_Drones);
        mainMusicInst.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void OnGameOver()
    {
        StopMainMusic();
        playOneShot(paperCrumplingRef);

    }

    public void PlayFireSound()
    {
        playOneShot(fireSoundRef);
    }
    public void PlayReflectSound()
    {
        playOneShot(reflectSoundRef);
    }
    public void PlayHurtSound()
    {
        playOneShot(hurtSoundRef);
    }
    public void PlayUIClick()
    {
        playOneShot(uiClickRef);
    }
    public void PlayConfirmUI()
    {
        playOneShot(confirmUIRef);
    }

   


   


   
}
