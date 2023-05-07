#if SCOR_ENABLE_VISUALSCRIPTING
using StudioScor.Utilities.VisualScripting;
using System;
using Unity.VisualScripting;

namespace StudioScor.StatSystem.VisualScripting
{
    public abstract class StatEventUnit<T> : CustomInterfaceEventUnit<IStatSystem,T>
    {
        public override Type MessageListenerType => typeof(StatSystemMessageListener);
    }
}

#endif