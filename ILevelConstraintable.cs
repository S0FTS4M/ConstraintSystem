namespace Forge.Models.Constraint
{
    public interface ILevelConstraintable : IConstraintable
    {
        int RequiredLevel { get; }
    }
}