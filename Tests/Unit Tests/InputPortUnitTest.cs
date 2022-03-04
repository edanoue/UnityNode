#nullable enable
using NUnit.Framework;
using Edanoue.Node.Interfaces;
using Edanoue.Node.Internal;

namespace UnitTests
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class InputPortUnitTest
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
            Assert.That(A_IN.Connect(B_OUT), Is.True);

            // Failed to connect
            var B_IN = new InputPort(B);
            Assert.That(A_IN.Connect(B_IN), Is.False);
        }
    }
}