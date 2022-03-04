#nullable enable
using System;
using Edanoue.Node.Interfaces;

namespace Edanoue.Node.Internal
{
    internal sealed class Connection : IConnection, IEquatable<Connection>
    {
        readonly IPort _a;
        readonly IPort _b;

        #region Constructors

        internal Connection(IPort a, IPort b)
        {
            _a = a;
            _b = b;
        }

        #endregion

        #region IConnection impls

        public IPort A => _a;
        public IPort B => _b;

        public IPort Other(IPort from)
        {
            if (from == _a)
            {
                return _b;
            }
            if (from == _b)
            {
                return _a;
            }
            throw new ArgumentOutOfRangeException(nameof(from), "specified port is no-assciation this connection");
        }

        #endregion

        public override bool Equals(object? obj) => this.Equals(obj as Connection);

        public bool Equals(Connection? p)
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
                (_a == p._a) && (_b == p._b) ||
                (_a == p._b) && (_b == p._a)
            ;
        }

        public override int GetHashCode() => (_a, _b).GetHashCode();

        public static bool operator ==(Connection? lhs, Connection? rhs)
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

        public static bool operator !=(Connection? lhs, Connection? rhs) => !(lhs == rhs);
    }
}
