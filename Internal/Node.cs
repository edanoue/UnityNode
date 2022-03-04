#nullable enable
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Edanoue.Node.Interfaces;

namespace Edanoue.Node.Internal
{
    /// <summary>
    ///
    /// /// </summary>
    public class Node : INetworkItem, INode
    {
        string _name;
        readonly Node? _parent;
        readonly List<InputPort> _inputPorts;
        readonly List<OutputPort> _outputPorts;
        static readonly string _pathSepareter = "/";
        string? _pathCache;

        public Node(string name, Node? parent = null)
        {
            _name = name;
            _parent = parent;
            _inputPorts = new();
            _outputPorts = new();
            _inputPorts.Add(new InputPort(this, 0));
            _outputPorts.Add(new OutputPort(this, 0));
            // make In -> Out connection
            _inputPorts[0].Connect(_outputPorts[0]);
        }

        #region INetworkItem impls

        public string Name => _name;

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

        public IPort InputPort(int index = 0)
        {
            if (_inputPorts.Count > index)
            {
                return _inputPorts[index];
            }
            throw new System.IndexOutOfRangeException();
        }
        public IPort OutputPort(int index = 0)
        {
            if (_outputPorts.Count > index)
            {
                return _outputPorts[index];
            }
            throw new System.IndexOutOfRangeException();
        }

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
