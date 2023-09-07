using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using WeFlyNPC;
using DistantLands.Cozy;
using PixelCrushers.DialogueSystem;

public class NavAgentPatrol : NavAgentOperator
{

    public bool goesToSleep = false;
    public bool randomizeDestinationsAndActions = true;
    public List<Transform> randomDestinations = new List<Transform>();
    public Transform sleepDestination = null;
    public float sleepTime = 20f;
    public float awakeTime = 8f;
    //private Cozy calendar = null;
    private CozyWeather weather = null;

    private bool _sleeping = false;
    private bool _goingToSleep = false;

    private NavAgentPatrol _targetPatrol = null;

    [System.Serializable]
    public class NavAgentDestination
    {
        public GameObject target;
        public float stoppingDistance = 1f;

        public NPCAction action;
        public float actionDuration;

        public float movingSpeed = 1f;
    }

    [NonReorderable]
    public NavAgentDestination[] destinations;
    private int index = 0;
    private bool _coroutineOn = false;

    NavAgentDestination currentOperation = null;
    Transform nextDest;

    InteractionController currentController = null;
    NPCAction currentAction = NPCAction.None;

    public void Start()
    {
        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).

        //agent.autoBraking = false;
        //agent.updateRotation = false;

        if (destinations.Length == 0) return;



        GotoNextPoint();
    }

    public void OnEnable()
    {
        InvokeRepeating("CheckWeatherAndTime", 1f, 1f);
        Lua.RegisterFunction("GetStatusString", this, SymbolExtensions.GetMethodInfo(() => GetStatusString()));

    }

    public void OnDisable()
    {
        Lua.UnregisterFunction("GetStatusString");
    }

    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (destinations.Length == 0) return;

        if (currentController != null) return;

        if (!agent.gameObject.activeInHierarchy) return;

        if (_sleeping) return;

        if (agent.enabled && agent.isOnNavMesh)
        {
            if (!agent.pathPending && agent.remainingDistance < agent.stoppingDistance && !_coroutineOn)
            {
                if (_goingToSleep && !_sleeping)
                {
                    _sleeping = true;
                    agent.gameObject.SetActive(false);
                    return;
                }
                EvaluateActionStart();
            }
        }

    }

    void LateUpdate()
    {
        //.rotation = Quaternion.LookRotation(agent.velocity.normalized);
    }

    void CheckWeatherAndTime()
    {
        /*
        if (!goesToSleep) return;

        if (!calendar) calendar = FindObjectOfType<CozyCalendar>();

        if (calendar)
        { 
            if (calendar.GetCurrentTick() < sleepTime && calendar.GetCurrentTick() > awakeTime)
            {
                if (_sleeping)
                {
                    Debug.Log("NPC waking up");
                    agent.gameObject.transform.position = sleepDestination.position;
                    _goingToSleep = false;
                    _sleeping = false;
                    agent.gameObject.SetActive(true);
                    GotoNextPoint();
                }
            }
            else
            {
                if (!_sleeping)
                {
                    agent.destination = sleepDestination.position;
                    _goingToSleep = true;
                }
                
            }
        }
        else 
        {
            Debug.LogError("NPC cannot find calendar");
        }
        */
    }

    void EvaluateActionStart()
    {
        switch (currentOperation.action)
        {
            case NPCAction.Converse:
                NavAgentPatrol patrol = currentOperation.target.GetComponentInChildren<NavAgentPatrol>();
                if (patrol)
                {
                    if (patrol.GetController())
                    {
                        // New destination
                        //agent.stoppingDistance = 0.7f;
                        agent.SetDestination(patrol.GetController().transform.position);
                        currentOperation.target = patrol.GetController().gameObject;
                        //Debug.Log(gameObject.name + " being assigned new conversation destination");
                        return;
                    }
                    else
                    {
                        //Debug.Log(gameObject.name + " trying to create conversation");
                        GameObject controller = new GameObject("Conversation." + gameObject.name);
                        ConversationController conversation = controller.AddComponent<ConversationController>();
                        conversation.Initialize(this, patrol);
                    }
                }

                ConversationController onGoingConversation = currentOperation.target.GetComponent<ConversationController>();
                if (onGoingConversation)
                {
                    //Debug.Log(gameObject.name + " trying to join conversation");
                    onGoingConversation.AddParticipant(this);
                }
                break;
        }

        StartCoroutine(WaitAWhile());
    }

    IEnumerator WaitAWhile()
    {
        //Debug.Log(gameObject.name + " started a coroutine");
        _coroutineOn = true;
        OnActionStart();

        yield return new WaitForSeconds(currentOperation.actionDuration);

        OnActionEnd();
        _coroutineOn = false;
        GotoNextPoint();
    }

    void GotoNextPoint()
    {

        if (_goingToSleep)
        {
            return;
        }

        currentOperation = destinations[index];
        //agent.avoidancePriority = Random.Range(10,50);
        // Set the agent to go to the currently selected destination.
        //nextDest = points[destPoint];
        agent.destination = currentOperation.target.transform.position;
        agent.stoppingDistance = currentOperation.stoppingDistance;
        agent.speed = currentOperation.movingSpeed;
        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        index = (index + 1) % destinations.Length;


    }

    void OnActionStart()
    {
        #region Global
        agent.updateRotation = false;
        #endregion

        if (!GetController()) SetActionState(currentOperation.action);

    }

    void OnActionEnd()
    {
        #region Global
        agent.updateRotation = true;
        #endregion

        switch (currentOperation.action)
        {
            case NPCAction.Converse:
                if (currentController)
                {
                    currentController.RemoveParticipant(this);
                }
                break;
        }

        ResetActionState();
    }

    public void SetActionState(WeFlyNPC.NPCAction action)
    {
        if (currentAction != NPCAction.None)
        {
            string current = NPCMethods.NPCActionToState(currentAction);
            animator.SetBool(current, false);
            currentAction = NPCAction.None;
        }

        string state = WeFlyNPC.NPCMethods.NPCActionToState(action);
        if (!string.IsNullOrEmpty(state))
        {
            animator.SetBool(state, true);
            currentAction = action;
        }
    }

    public void ResetActionState()
    {
        List<string> states = NPCMethods.GetAllNPCStates();

        foreach (string state in states)
        {
            animator.SetBool(state, false);
        }
    }

    public void SetController(InteractionController controller)
    {
        currentController = controller;

        if (controller == null)
        {
            agent.isStopped = false;
            ResetActionState();
        }
        else
        {
            agent.isStopped = true;
        }
    }

    public InteractionController GetController()
    {
        return currentController;
    }

    public IEnumerator LookAtTarget(Transform target)
    {
        Quaternion lookTarget = Quaternion.LookRotation(target.position - transform.position);

        float time = 0;

        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookTarget, time);
            time += Time.deltaTime;
            yield return null;
        }

    }

    void OnConversationStart()
    {
        agent.isStopped = true;
    }

    void OnConversationEnd()
    {
        agent.isStopped = false;
    }

    public string GetStatusString()
    {
        if (_goingToSleep) return "sleeping";

        if (GetController()) return "conversing";

        return string.Empty;
    }


}
