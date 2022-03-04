#nullable enable
using System;
using System.Collections.Generic;

namespace Edanoue.Node.Interfaces
{
    /// <summary>
    ///
    /// </summary>
    public interface INode
    {
        public IPort InputPort(int index = 0);

        public IPort OutputPort(int index = 0);
    }
}
