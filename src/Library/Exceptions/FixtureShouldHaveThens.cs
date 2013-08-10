namespace Kekiri.Exceptions
{
    public class FixtureShouldHaveThens : ScenarioTestException
    {
        public FixtureShouldHaveThens(ScenarioTest test)
            : base(test, string.Format("No thens found; thens should be parameterless public methods that start use [Then] attribute"))
        {
        }
    }
}