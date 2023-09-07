using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionController : MonoBehaviour
{
    public abstract void AddParticipant(NavAgentPatrol patrol);
    public abstract void RemoveParticipant(NavAgentPatrol patrol);
}
