using System.Collections.Generic;

public interface IBoidComputeHandler
{
    void RegisterBoid(IBoid boid);
    IEnumerable<IBoid> AllBoids { get; }
}
