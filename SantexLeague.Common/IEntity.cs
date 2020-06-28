namespace SantexLeague.Common
{
    public interface IEntity : IEntity<long>
    {
    }
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}