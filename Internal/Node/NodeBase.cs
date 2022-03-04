#nullable enable
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Edanoue.Node.Interfaces;

namespace Edanoue.Node.Internal
{
    /// <summary>
    ///
    /// </summary>
    public abstract class NodeBase : INetworkItem, INode
    {
        string _name;
        readonly NodeBase? _parent;
        static readonly string _pathSepareter = "/";
        string? _pathCache;

        public NodeBase(string name, NodeBase? parent = null)
        {
            _name = name;
            _parent = parent;
        }

        #region INetworkItem impls

        /// <summary>
        /// 
        /// </summary>
        public string Name => _name;

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string Path
        {
            get
            {
                if (_pathCache is null)
                {
                    return UpdatePath();
                }
                return _pathCache;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <value></value>
        public INetworkItem? Parent => _parent;

        #endregion

        #region INode impls

        public abstract IPort InputPort(int index = 0);

        public abstract IPort OutputPort(int index = 0);

        #endregion

        string UpdatePath()
        {
            if (Parent is null)
            {
                _pathCache = $"{_pathSepareter}{this.Name}";
            }
            else
            {
                _pathCache = $"{Parent.Path}{_pathSepareter}{this.Name}";
            }
            return _pathCache;
        }

    }
}
