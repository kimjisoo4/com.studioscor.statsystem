#if SCOR_ENABLE_VISUALSCRIPTING
using StudioScor.Utilities.VisualScripting;
using System;
using Unity.VisualScripting;

namespace StudioScor.StatSystem.VisualScripting
{

    [UnitCategory("Events\\StudioScor\\StatSystem")]
    public abstract class StatSystemEventUnit<T> : CustomInterfaceEventUnit<IStatSystem,T>
    {
        public override Type MessageListenerType => typeof(StatSystemMessageListener);
    }
}

#endif