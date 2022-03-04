#nullable enable
using System;

namespace Edanoue.Node.Interfaces
{
    /// <summary>
    /// </summary>
    internal interface IConnection
    {
        public IPort A { get; }
        public IPort B { get; }

        public IPort? Other(IPort from);
    }
}