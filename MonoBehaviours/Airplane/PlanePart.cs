using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeFly
{
    public enum EMaterial
    {
        OldMetal,
        Wood,
        Aluminum,
        NULL
    }

    public class PlanePart : MonoBehaviour
    {
        public EMaterial material = EMaterial.NULL;
    }
}

