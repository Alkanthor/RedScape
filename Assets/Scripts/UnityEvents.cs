using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UnityEvents
{
    [System.Serializable]
    public class UnityEventString : UnityEvent<string> { }
    [System.Serializable]
    public class UnityEventBool : UnityEvent<bool> { }
}
