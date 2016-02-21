namespace Kekiri.Impl
{
    public interface IStepInvoker
    {
        void Invoke(object test);

        bool ExceptionExpected { get; set; }

        StepName Name { get; }

        StepType Type { get; }

        string SourceDescription { get; }
    }
}