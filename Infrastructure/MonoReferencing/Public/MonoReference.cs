using EventBus.Events;
using UnityEngine;

namespace MonoReferencing.Public
{
    public abstract class MonoReference : MonoBehaviour
    {
        public abstract object Data { get; }
        public virtual bool AllowMultiple => false;
    }
    
    public abstract class MonoReference<T> : MonoReference, IEvent where T : IMonoReferenceData
    {
        [SerializeField] private T _data;
        
        public override object Data => _data;
    }
}