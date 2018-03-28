﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UnityEvents
{
    [System.Serializable]
    public class UnityEventString : UnityEvent<string> { }
    [System.Serializable]
    public class UnityEventBool : UnityEvent<bool> { }
    [System.Serializable]
    public class UnityEventGameObject : UnityEvent<GameObject> { }
    [System.Serializable]
    public class UnityEventGameObject2 : UnityEvent<GameObject, GameObject> { }
    [System.Serializable]
    public class UnityEventGameObjectBool : UnityEvent<GameObject, bool> { }
    [System.Serializable]
    public class UnityEventGameObjectArray : UnityEvent<GameObject[]> { }
}
