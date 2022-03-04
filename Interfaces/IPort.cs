#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace Edanoue.Node.Interfaces
{
    /// <summary>
    /// Node 同士の接続元 をモデル化したインタフェース
    /// 内部向けの実装でのみ 使用されるため, internal なInterface です
    /// </summary>
    public interface IPort
    {
        /// <summary>
        /// Returns the index of this port inside its node.
        /// </summary>
        /// <value></value>
        public int Index { get; }

        /// <summary>
        /// Returns the Node that owns this port.
        /// </summary>
        /// <value></value>
        public INode Node { get; }

        /// <summary>
        /// Returns a list of all ports connected to this port.
        /// </summary>
        /// <value></value>
        public IEnumerable<IPort> ConnectedPorts { get; }

        /// <summary>
        /// Returns True if this port is connected to the given port.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool IsConnected(IPort? other);

        /// <summary>
        /// <para>
        /// Connects other to this port. If the nodes to which the ports belong are not part of the same parent node, in-between connections along the way are created.
        /// </para>
        /// Returns True if the port connection was successfully established, or False if recursion was detected, nodes were locked or if another error occurred.
        /// <para>
        /// </para>
        /// </summary>
        /// <param name="other">接続先となる INodePort の参照</param>
        public bool Connect(IPort? other, bool doCycleCheck = true);

        /// <summary>
        /// Disconnects port other from this port. This does nothing if the ports are not directly connected. Returns True if the ports were successfully disconnected.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Disconnect(IPort? other);

    }
}
