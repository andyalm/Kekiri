namespace Kekiri
{
    public interface IFeatureDescriptionAttribute
    {
        string[] Details { get; }
        string Summary { get; }
    }
}