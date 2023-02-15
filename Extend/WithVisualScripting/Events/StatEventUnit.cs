#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.StatSystem.VisualScripting
{
    public abstract class StatEventUnit<T> : EventUnit<T>
    {
        protected override bool register => true;
        protected abstract string HookName { get; }
        public override EventHook GetHook(GraphReference reference)
        {
            if (!reference.hasData)
            {
                return HookName;
            }

            var data = reference.GetElementData<Data>(this);

            return new EventHook(HookName, data.Stat);
        }

        [DoNotSerialize]
        [PortLabel("StatSystem")]
        [NullMeansSelf]
        [PortLabelHidden]
        public ValueInput StatSystem { get; private set; }
        [DoNotSerialize]
        [PortLabel("StatTag")]
        [PortLabelHidden]
        public ValueInput StatTag { get; private set; }


        public override IGraphElementData CreateData()
        {
            return new Data();
        }
        public new class Data : EventUnit<T>.Data
        {
            public StatSystemComponent StatSystem;
            public StatTag StatTag;
            public Stat Stat;
        }

        protected override void Definition()
        {
            base.Definition();

            StatSystem = ValueInput<StatSystemComponent>(nameof(StatSystem), null).NullMeansSelf();
            StatTag = ValueInput<StatTag>(nameof(StatTag), null);
        }

        private void UpdateTarget(GraphStack stack)
        {
            var data = stack.GetElementData<Data>(this);

            var wasListening = data.isListening;

            var statSystem = Flow.FetchValue<StatSystemComponent>(StatSystem, stack.ToReference());
            var statTag = Flow.FetchValue<StatTag>(StatTag, stack.ToReference());

            if (statSystem != data.StatSystem || statTag != data.StatTag)
            {
                if (wasListening)
                {
                    StopListening(stack);
                }

                data.StatSystem = statSystem;
                data.StatTag = statTag;
                data.Stat = statSystem.GetOrCreateValue(statTag);

                if (wasListening)
                {
                    StartListening(stack, false);
                }
            }
        }

        protected void StartListening(GraphStack stack, bool updateTarget)
        {
            if (updateTarget)
            {
                UpdateTarget(stack);
            }

            var data = stack.GetElementData<Data>(this);

            if (data.Stat is null)
            {
                return;
            }

            if (UnityThread.allowsAPI)
            {
                var target = data.StatSystem.gameObject;

                if (!target.TryGetComponent(out StatMessageListener messageListener))
                {
                    messageListener = target.AddComponent<StatMessageListener>();
                }

                messageListener.TryAddEventBus(data.Stat.Tag);
            }

            base.StartListening(stack);
        }

        public override void StartListening(GraphStack stack)
        {
            StartListening(stack, true);
        }
    }
}

#endif