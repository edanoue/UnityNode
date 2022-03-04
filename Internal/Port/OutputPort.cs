#nullable enable
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Edanoue.Node.Interfaces;

namespace Edanoue.Node.Internal
{
    public class OutputPort : PortBase
    {
        public sealed override PortType PortType => PortType.Output;
        protected sealed override bool IsConnectAllowedPort(PortBase other)
        {
            return other.PortType == PortType.Input;
        }


        #region Constructors 

        internal OutputPort(INode node, int index = 0) : base(node, index) { }

        #endregion
    }
}
