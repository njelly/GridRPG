using System;
using UnityEngine;

namespace Tofunaut.Core
{
    public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T _instance;

        public virtual bool DontDestroyOnLoad => true;

        protected virtual void Awake()
        {
            if (_instance)
            {
                Destroy(_instance.gameObject);
                return;
            }

            _instance = this as T;
        }
    }
}