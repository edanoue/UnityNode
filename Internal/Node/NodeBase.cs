#nullable enable
using Edanoue.Node.Interfaces;

namespace Edanoue.Node.Internal
{
    /// <summary>
    ///
    /// </summary>
    public abstract class NodeBase : INetworkItem, INode
    {
        readonly NodeBase? _parent;
        static readonly string _pathSepareter = "/";
        string? _pathCache;

        #region Constructors

        public NodeBase()
        {
            _parent = null;
        }

        public NodeBase(NodeBase parent)
        {
            _parent = parent;
        }

        #endregion

        #region abstract Methods

        /// <summary>
        /// Get node name
        /// </summary>
        /// <value></value>
        public abstract string Name { get; }

        /// <summary>
        /// Get node inputport
        /// </summary>
        /// <param name="index">port index</param>
        /// <returns></returns>
        public abstract IPort InputPort(int index = 0);

        /// <summary>
        /// Get node outputport
        /// </summary>
        /// <param name="index">port index</param>
        /// <returns></returns>
        public abstract IPort OutputPort(int index = 0);

        #endregion

        #region INetworkItem impls

        /// <summary>
        /// 
        /// </summary>
        string INetworkItem.Name => this.Name;

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

        IPort INode.InputPort(int index) => this.InputPort(index);
        IPort INode.OutputPort(int index) => this.OutputPort(index);

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
