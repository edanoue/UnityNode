#nullable enable
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Edanoue.Node.Interfaces;

namespace Edanoue.Node.Internal
{
    public class InputPort : PortBase
    {
        public sealed override PortType PortType => PortType.Input;
        protected sealed override bool IsConnectAllowedPort(PortBase other)
        {
            return other.PortType == PortType.Output;
        }

        #region Constructors 

        public InputPort(INode node, int index = 0) : base(node, index) { }

        #endregion
    }
}
