#nullable enable

namespace Edanoue.Node.Interfaces
{
    /// <summary>
    ///
    /// </summary>
    public interface INetworkItem
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string Path { get; }

        /// <summary>
        ///
        /// </summary>
        /// <value></value>
        public INetworkItem? Parent { get; }

    }
}
