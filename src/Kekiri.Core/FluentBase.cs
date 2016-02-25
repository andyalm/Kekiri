using System;
using System.Collections.Generic;
using System.Linq;
using System.Dynamic;
using System.Reflection;
using Kekiri.Exceptions;
using Kekiri.Impl;
using Kekiri.Reporting;

namespace Kekiri
{
    public abstract class FluentBase : IContextAccessor
    {
        private readonly ScenarioRunner _scenarioRunner;

        protected FluentBase()
        {
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            var reportTarget = CreateReportTarget();
            _scenarioRunner = new ScenarioRunner(this, reportTarget);
        }

        public virtual void RunScenario()
        {
            try
            {
                Before();
                _scenarioRunner.Run();
            }
            finally
            {
                After();
            }
        }

        protected TException Catch<TException>() where TException : Exception
        {
            return _scenarioRunner.Catch<TException>();
        }
        
        public abstract class FluentOptionsForStep
        {
            protected readonly FluentBase Scenario;

            protected FluentOptionsForStep(FluentBase scenario)
            {
                Scenario = scenario;
            }

            protected abstract StepType StepType { get; }
        }

        #region Base
        public abstract class FluentOptionsForStepWithNesting : FluentOptionsForStep
        {
            protected FluentOptionsForStepWithNesting(FluentBase scenario) : base(scenario)
            {
            }

            #region And
            public FluentOptionsForStepWithNesting And(Action action)
            {
                Scenario.AddStepMethod(StepType, action);
                return this;
            }

            public FluentOptionsForStepWithNesting And<T>(Action<T> action, T a)
            {
                Scenario.AddStepMethod(StepType, action.GetMethodInfo(), a);
                return this;
            }

            public FluentOptionsForStepWithNesting And<T1, T2>(Action<T1, T2> action, T1 a, T2 b)
            {
                Scenario.AddStepMethod(StepType, action.GetMethodInfo(), a, b);
                return this;
            }

            public FluentOptionsForStepWithNesting And<T1, T2, T3>(Action<T1, T2, T3> action, T1 a, T2 b, T3 c)
            {
                Scenario.AddStepMethod(StepType, action.GetMethodInfo(), a, b, c);
                return this;
            }

            public FluentOptionsForStepWithNesting And<TStep>(params object[] parameterValues) where TStep : Step
            {
                Scenario.AddStepClass<TStep>(StepType, parameterValues);
                return this;
            }
            #endregion

            #region But
            public FluentOptionsForStepWithNesting But(Action action)
            {
                Scenario.AddStepMethod(StepType, action);
                return this;
            }

            public FluentOptionsForStepWithNesting But<T>(Action<T> action, T a)
            {
                Scenario.AddStepMethod(StepType, action.GetMethodInfo(), a);
                return this;
            }

            public FluentOptionsForStepWithNesting But<T1, T2>(Action<T1, T2> action, T1 a, T2 b)
            {
                Scenario.AddStepMethod(StepType, action.GetMethodInfo(), a, b);
                return this;
            }

            public FluentOptionsForStepWithNesting But<T1, T2, T3>(Action<T1, T2, T3> action, T1 a, T2 b, T3 c)
            {
                Scenario.AddStepMethod(StepType, action.GetMethodInfo(), a, b, c);
                return this;
            }

            public FluentOptionsForStepWithNesting But<TStep>(params object[] parameterValues) where TStep : Step
            {
                Scenario.AddStepClass<TStep>(StepType, parameterValues);
                return this;
            }
            #endregion
        }
        #endregion

        #region Given
        public class FluentOptionsForGiven : FluentOptionsForStepWithNesting
        {
            public FluentOptionsForGiven(FluentBase scenario)
                : base(scenario)
            {
            }

            protected override StepType StepType
            {
                get { return StepType.Given; }
            }
        }

        protected FluentOptionsForStepWithNesting Given(Action action)
        {
            return new FluentOptionsForGiven(this).And(action);
        }

        protected FluentOptionsForStepWithNesting Given<T>(Action<T> action, T a)
        {
            return new FluentOptionsForGiven(this).And(action, a);
        }

        protected FluentOptionsForStepWithNesting Given<T1, T2>(Action<T1, T2> action, T1 a, T2 b)
        {
            return new FluentOptionsForGiven(this).And(action, a, b);
        }

        protected FluentOptionsForStepWithNesting Given<T1, T2, T3>(Action<T1, T2, T3> action, T1 a, T2 b, T3 c)
        {
            return new FluentOptionsForGiven(this).And(action, a, b, c);
        }

        protected FluentOptionsForStepWithNesting Given<TStep>(params object[] parameterValues) where TStep : Step
        {
            return new FluentOptionsForGiven(this).And<TStep>(parameterValues);
        }

        #endregion

        #region When
        public class FluentOptionsForWhen : FluentOptionsForStep
        {
            public FluentOptionsForWhen(FluentBase scenario) : base(scenario)
            {
            }

            protected override StepType StepType
            {
                get { return StepType.When; }
            }

            public void Throws()
            {
                Scenario._scenarioRunner.ExpectException();
            }

            internal FluentOptionsForWhen That(Action action)
            {
                Scenario.AddStepMethod(StepType, action);
                return this;
            }

            internal FluentOptionsForWhen That<T>(Action<T> action, T a)
            {
                Scenario.AddStepMethod(StepType, action.GetMethodInfo(), a);
                return this;
            }

            internal FluentOptionsForWhen That<T1, T2>(Action<T1, T2> action, T1 a, T2 b)
            {
                Scenario.AddStepMethod(StepType, action.GetMethodInfo(), a, b);
                return this;
            }

            internal FluentOptionsForWhen That<T1, T2, T3>(Action<T1, T2, T3> action, T1 a, T2 b, T3 c)
            {
                Scenario.AddStepMethod(StepType, action.GetMethodInfo(), a, b, c);
                return this;
            }

            internal FluentOptionsForWhen That<TStep>(params object[] parameterValues) where TStep : Step
            {
                Scenario.AddStepClass<TStep>(StepType, parameterValues);
                return this;
            }
        }

        protected FluentOptionsForWhen When(Action action)
        {
            return new FluentOptionsForWhen(this).That(action);
        }

        protected FluentOptionsForWhen When<T>(Action<T> action, T a)
        {
            return new FluentOptionsForWhen(this).That(action, a);
        }

        protected FluentOptionsForWhen When<T1, T2>(Action<T1, T2> action, T1 a, T2 b)
        {
            return new FluentOptionsForWhen(this).That(action, a, b);
        }

        protected FluentOptionsForWhen When<T1, T2, T3>(Action<T1, T2, T3> action, T1 a, T2 b, T3 c)
        {
            return new FluentOptionsForWhen(this).That(action, a, b, c);
        }

        protected FluentOptionsForWhen When<TStep>(params object[] parameterValues) where TStep : Step
        {
            return new FluentOptionsForWhen(this).That<TStep>(parameterValues);
        }
        #endregion

        #region Then
        public class FluentOptionsForThen : FluentOptionsForStepWithNesting
        {
            public FluentOptionsForThen(FluentBase scenario)
                : base(scenario)
            {
            }

            protected override StepType StepType
            {
                get { return StepType.Then; }
            }
        }

        protected FluentOptionsForStepWithNesting Then(Action action)
        {
            return new FluentOptionsForThen(this).And(action);
        }

        protected FluentOptionsForStepWithNesting Then<T>(Action<T> action, T a)
        {
            return new FluentOptionsForThen(this).And(action, a);
        }

        protected FluentOptionsForStepWithNesting Then<T1, T2>(Action<T1, T2> action, T1 a, T2 b)
        {
            return new FluentOptionsForThen(this).And(action, a, b);
        }

        protected FluentOptionsForStepWithNesting Then<T1, T2, T3>(Action<T1, T2, T3> action, T1 a, T2 b, T3 c)
        {
            return new FluentOptionsForThen(this).And(action, a, b, c);
        }

        protected FluentOptionsForStepWithNesting Then<TStep>(params object[] parameterValues) where TStep : Step
        {
            return new FluentOptionsForThen(this).And<TStep>(parameterValues);
        }
        #endregion

        private void AddStepMethod(StepType stepType, Action action)
        {
            _scenarioRunner.AddStep(new StepMethodInvoker(stepType, action.GetMethodInfo()));
        }

        private void AddStepMethod(StepType stepType, MethodInfo method, params object[] parameterValues)
        {
            var parameters = ExtractParameters(method, parameterValues);
            _scenarioRunner.AddStep(new StepMethodInvoker(stepType, method, parameters));
        }

        private void AddStepClass<TStep>(StepType stepType, params object[] parameterValues) where TStep : Step
        {
            var stepClass = typeof(TStep);
            var ctor = stepClass.GetConstructors()
                .SingleOrDefault();
            if (ctor == null)
            {
                throw new StepNotFound(string.Format("Could not find a constructor for {0} {1} ({2})", stepType, stepClass.Name, 
                    string.Join(", ", 
                    parameterValues.Select(p => string.Format("{0} {1}", p.GetType().Name, p)))));
            }
            var parameters = ExtractParameters(ctor, parameterValues);
            _scenarioRunner.AddStep(new StepClassInvoker(stepType, stepClass.GetTypeInfo(), parameters, _scenarioRunner));
        }

        private KeyValuePair<string, object>[] ExtractParameters(MethodBase method, object[] parameterValues)
        {
            return method.GetParameters()
                .Select((p, index) => new KeyValuePair<string, object>(p.Name, parameterValues[index]))
                .ToArray();
        }

        private object _context;
        protected internal dynamic Context
        {
            get { return _context ?? (_context = CreateContextObject()); }
        }

        dynamic IContextAccessor.Context
        {
            get { return Context; }
        }

        protected virtual void Before() { }
        protected virtual void After() { }

        protected virtual object CreateContextObject()
        {
            return new ExpandoObject();
        }

        internal virtual IReportTarget CreateReportTarget()
        {
            return CompositeReportTarget.GetInstance();
        }
    }
}