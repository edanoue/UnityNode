#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Edanoue.Node.Interfaces;

namespace Edanoue.Node.Internal
{
    /// <summary>
    /// IPort 実装クラス
    /// </summary>
    public abstract class PortBase : IPort, IEquatable<PortBase>
    {
        readonly int _index;
        readonly INode _node;
        readonly HashSet<Connection> _connections = new();

        #region Constructors 

        internal PortBase(INode node, int index = 0)
        {
            _node = node;
            _index = index;
        }

        #endregion

        public abstract PortType PortType { get; }
        protected virtual bool IsConnectAllowedPort(PortBase other) => true;

        #region IPort implements

        public int Index => this._index;

        public INode Node => this._node;

        public IEnumerable<IPort> ConnectedPorts
        {
            get
            {
                return _connections.Select(c => c.Other(this));
            }
        }

        public bool IsConnected(IPort? other)
        {
            // If other port is null, return false
            if (other is null)
                return false;

            // Make new connection for check
            var connection = new Connection(this, other);
            return _connections.Contains(connection);
        }

        public bool Connect(IPort? other, bool doCycleCheck = true)
        {
            // If other port is null, return false
            if (other is null)
                return false;

            // If same ports, return false
            {
                if (other is PortBase pb)
                {
                    if (this == pb) return false;
                }
                else
                {
                    if ((this as IPort) == other) return false;
                }
            }

            // If already connected, return false
            if (IsConnected(other))
            {
                return false;
            }

            // If check Circuller dependency
            if (doCycleCheck)
            {
                if (IsCycleConnection(other, this, new()))
                {
                    return false;
                }
            }

            // Port type specific validation
            {
                if (other is PortBase pb)
                {
                    if (!IsConnectAllowedPort(pb))
                        return false;
                }
            }

            // Make new connection
            var connection = new Connection(this, other);
            this._connections.Add(connection);
            other.Connect(this, doCycleCheck);
            return true;
        }

        private static bool IsCycleConnection(IPort targetPort, IPort from, HashSet<IPort> arrived)
        {
            if (arrived.Contains(from))
            {
                return false;
            }
            arrived.Add(from);

            foreach (var connectedPort in from.ConnectedPorts)
            {
                if (connectedPort == targetPort)
                {
                    return true;
                }
                if (IsCycleConnection(targetPort, connectedPort, arrived))
                {
                    return true;
                }
            }
            return false;
        }

        public bool Disconnect(IPort? other)
        {
            // If other port is null, return false
            if (other is null)
                return false;

            // If not connected port, return false
            if (!IsConnected(other))
                return false;

            // Make new connection for remove
            var connection = new Connection(this, other);

            this._connections.Remove(connection);
            other.Disconnect(this);
            return true;
        }

        #endregion

        #region IEquatable implements

        public override bool Equals(object? obj) => this.Equals(obj as PortBase);

        public bool Equals(PortBase? p)
        {
            if (p is null)
                return false;

            // Optimization for a common success case.
            if (Object.ReferenceEquals(this, p))
                return true;

            // If run-time types are not exactly the same, return false.
            if (this.GetType() != p.GetType())
                return false;

            // Return true if the fields match.
            // Note that the base class is not invoked because it is
            // System.Object, which defines Equals as reference equality.
            return
                (_node == p._node) && (_index == p._index) && (PortType == p.PortType)
            ;
        }

        public override int GetHashCode() => (_node, _index, PortType).GetHashCode();

        public static bool operator ==(PortBase? lhs, PortBase? rhs)
        {
            if (lhs is null)
            {
                if (rhs is null)
                {
                    return true;
                }

                // Only the left side is null.
                return false;
            }
            // Equals handles case of null on right side.
            return lhs.Equals(rhs);
        }

        public static bool operator !=(PortBase? lhs, PortBase? rhs) => !(lhs == rhs);


        #endregion
    }
}
