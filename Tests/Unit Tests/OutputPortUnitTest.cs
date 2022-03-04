#nullable enable
using NUnit.Framework;
using Edanoue.Node.Interfaces;
using Edanoue.Node.Internal;

namespace UnitTests
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class OutputPortUnitTest
    {
        // Make nodes
        readonly INode A = new Node("A");
        readonly INode B = new Node("B");

        [Test]
        public void Connect()
        {
            var A_IN = new InputPort(A);
            var B_OUT = new OutputPort(B);

            // Success to connect
            Assert.That(B_OUT.Connect(A_IN), Is.True);

            // Failed to connect
            var A_OUT = new OutputPort(A);
            Assert.That(B_OUT.Connect(A_OUT), Is.False);
        }
    }
}