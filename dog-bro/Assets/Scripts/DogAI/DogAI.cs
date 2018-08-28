using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using StateNameSpace;

public class BasicAI: MonoBehaviour {
    public StateMachine<BasicAI> stateMachine { get; set; }
}

public class DogAI : BasicAI, IFollowState, IWarnState, IIndicateSafteyState, IDangerZoneState {

    public Transform characterTransform;
    public Transform dogTransform;
    public float movementSpeed;


    // Audio
    public AudioSource warningBarkAudioSource;
    public AudioSource safetyBarkAudioSource;

    public AudioSource dangerZoneBarkingAudioSource;
    public AudioSource growlAudioSource;

    private float distanceForDangerZone = 2.5f;

    public bool shouldFollow 
    {
        get 
        {
            if (Vector3.Distance(characterTransform.position, dogTransform.position) < 0.05)
            {
                return false;
            }
            else
            {
                return true;
            }
        }    
    }

    private bool _dangerApparent = false;
    public bool dangerApparent
    {
        get
        {
            return _dangerApparent;
        }
    }

    private bool _immediateDangerApparent = false;
    public bool immediateDangerApparent
    {
        get
        {
            return _immediateDangerApparent;
        }
    }


    private bool _safteyTrigger = false;
    public bool safteyTrigger
    {
        get
        {
            return _safteyTrigger;
        }
    }

    private void Start()
    {
        // Create Statemachine and set inital state
        stateMachine = new StateMachine<BasicAI>(this, FollowState.Instance);
    }

    void Update()
    {
        stateMachine.Update();
    }

    //////////////////
    // Collider Stuff
    //////////////////
    private void OnTriggerEnter(Collider other)
    {
        HandleCollider(other);
    }

    private void OnTriggerStay(Collider other)
    {
        HandleCollider(other);
    }

    // On Trigger Exit is not called!
    private void OnTriggerExit(Collider other)
    {
        _dangerApparent = false;
        _immediateDangerApparent = false;
    }

    private void HandleCollider(Collider other) {
        TrafficController trafficController = other.gameObject.GetComponent<TrafficController>();

        if (trafficController != null)
        {
            _dangerApparent = trafficController.isCurrentlyRed;

            float distance = Vector3.Distance(dogTransform.position, other.transform.position);

            _immediateDangerApparent = _dangerApparent && distance <= distanceForDangerZone;
        }
    }


    //////////////////
    // Interface implementations
    //////////////////

    // Implementation of IFollowState
    public void Move()
    {
        float step = movementSpeed * Time.deltaTime;
        dogTransform.position = Vector3.MoveTowards(dogTransform.position, characterTransform.position, step);
        dogTransform.rotation = characterTransform.rotation;
    }

    // Implementation of IWarnState
    public void IndicateDanger()
    {
        //warningBarkAudioSource.Play();
        growlAudioSource.Play();
    }

    // Implementation of IDangerZoneState
    public void IndicateImmediateDanger()
    {
        dangerZoneBarkingAudioSource.Play();
    }

    public void IndicateImmediateDangerIsGone()
    {
        dangerZoneBarkingAudioSource.Stop();
    }

    // Implementation of IIndicateSafteyState
    public void IndicateSafety()
    {
        safetyBarkAudioSource.Play();
        growlAudioSource.Stop();
        _safteyTrigger = false;
    }
}
