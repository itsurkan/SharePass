namespace SharePass.Helpers
{
    public interface IGenerator
    {
        string Generate();

    }
    public interface ISaltGenerator: IGenerator
    {
    }
    public interface ILinkGenerator: IGenerator
    {
    }
}