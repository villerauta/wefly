using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeFlyNPC
{
    public enum NPCAction
    {
        None,
        Dance,
        Wave,
        Sit,
        Cry,
        Converse,
        NUM_OF_STATES
    }

    class NPCMethods
    {
        static public string NPCActionToState(NPCAction a)
        {
            string state = null;

            switch (a)
            {
                case NPCAction.None:
                    break;
                case NPCAction.Dance:
                    state = "dancing";
                    break;
                case NPCAction.Wave:
                    state = "waving";
                    break;
                case NPCAction.Sit:
                    state = "sitting";
                    break;
                case NPCAction.Cry:
                    state = "crying";
                    break;
                case NPCAction.Converse:
                    state = "conversing";
                    break;
            }

            return state;
        }

        static public List<string> GetAllNPCStates()
        {
            List<string> list = new List<string>();
            list.Add("dancing");
            list.Add("waving");
            list.Add("sitting");
            list.Add("crying");
            list.Add("conversing");

            if (list.Count+1 != (int) NPCAction.NUM_OF_STATES)
            {
                Debug.LogError("GetAllNPCStates does not return all states");
            }

            return list;
        }
    }

}




