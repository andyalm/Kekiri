using System;

namespace Kekiri.Exceptions
{
    public class ScenarioStepMethodsShouldNotHaveParameters : ScenarioTestException
    {
        public ScenarioStepMethodsShouldNotHaveParameters(Type type, string message) :
            base(type, message)
        {
        }
    }
}