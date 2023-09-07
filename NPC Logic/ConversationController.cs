using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ConversationController : InteractionController
{
    private List<NavAgentPatrol> participants = new List<NavAgentPatrol>();
    private int index = 0;

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.2f);
    }

    public void Initialize(NavAgentPatrol patrol1, NavAgentPatrol patrol2)
    {
        //Debug.Log("Conversation initialized");

        participants.Add(patrol1);
        patrol1.SetController(this);
        participants.Add(patrol2);
        patrol2.SetController(this);

        InvokeRepeating("NextCycle", 0f, Random.Range(4,7));
    }

    void RepositionConversation()
    {
        Vector3 centerPosition = new Vector3();
        foreach(NavAgentPatrol patrol in participants)
        {
            centerPosition += patrol.transform.position;
        }
        gameObject.transform.position = centerPosition / participants.Count;
    }


    public override void AddParticipant(NavAgentPatrol patrol)
    {
        if (participants.Contains(patrol))
        {
            //Debug.LogError("Trying to add participant when participant is already on the list");
        }
        else
        {
            participants.Add(patrol);
            patrol.SetController(this);
            NextCycle();
        }
    }

    public override void RemoveParticipant(NavAgentPatrol patrol)
    {
        if (participants.Remove(patrol))
        {
            //Debug.Log("Participant removed from conversation");
            patrol.SetController(null);
            index = 0;
            NextCycle();
            
        }
        
    }

    void NextCycle()
    {
        if (participants.Count < 2)
        {
            foreach(NavAgentPatrol patrol in participants)
            {
                patrol.SetController(null);
            }
            //Debug.Log("Destroying conversation");
            Destroy(this);
        }

        RepositionConversation();

        foreach(NavAgentPatrol patrol in participants)
        {
            if (participants[index] == patrol)
            {
                // Talking
                patrol.SetActionState(WeFlyNPC.NPCAction.Dance);
            }
            else
            {
                // Listening
                patrol.SetActionState(WeFlyNPC.NPCAction.Cry);
                StartCoroutine(patrol.LookAtTarget(participants[index].transform));
                //patrol.transform.LookAt(participants[index].transform);
            }
        }

        index++;

        if (index > participants.Count-1) index = 0;
    }

}
