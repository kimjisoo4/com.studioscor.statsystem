#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.StatSystem.VisualScripting
{
    [UnitTitle("Remove All Stat Modifiers")]
    [UnitSubtitle("StatSystem Unit")]
    public class StatSystemRemoveAllStatModifiersUnit : StatSystemFlowUnit
    {
        protected override ControlOutput OnFlow(Flow flow)
        {
            var statSystem = GetStatSystem(flow);

            statSystem.RemoveAllStatModifier();

            return Exit;
        }
    }
}

#endif