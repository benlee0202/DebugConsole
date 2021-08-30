using System;

namespace DebugConsole.Interfaces
{
    public interface IFactory<in TIn, out TOut>
    {
        TOut GetProduct(TIn request);
    }
}
