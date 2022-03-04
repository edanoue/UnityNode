#nullable enable
using System;
using System.Linq;
using NUnit.Framework;
using Edanoue.Node.Interfaces;
using Edanoue.Node.Internal;


namespace UnitTests
{
    /// <summary>
    /// Connection internal class unit test case
    /// </summary>
    public sealed class PortUnitTest
    {
        // Mock Port Class
        sealed class TestPort : PortBase
        {
            public sealed override PortType PortType => PortType.Input;
            public TestPort(INode node, int index = 0) : base(node, index) { }
        }

        // Make nodes
        readonly INode A = new Node("A");
        readonly INode B = new Node("B");

        [Test]
        public void Index()
        {
            // Default index is 0
            var portA = new TestPort(A);
            Assert.That(portA.Index, Is.EqualTo(0));

            // index specification
            var portB = new TestPort(A, 12);
            Assert.That(portB.Index, Is.EqualTo(12));
        }

        [Test]
        public void Node()
        {
            var portA = new TestPort(A);
            Assert.That(portA.Node, Is.EqualTo(A));
        }

        [Test]
        public void Connect()
        {
            var portA0 = new TestPort(A, 0);
            var portA1 = new TestPort(A, 1);
            var portB = new TestPort(B, 0);

            // failed to same port connect
            Assert.That(portA0.Connect(portA0), Is.False);

            // failed to null connect
            Assert.That(portA0.Connect(null), Is.False);

            // success to connect
            Assert.That(portA0.Connect(portB), Is.True);

            // failed to re-connect
            Assert.That(portA0.Connect(portB), Is.False);
        }

        [Test]
        public void CirculerConnect()
        {
            var portA0 = new TestPort(A, 0);
            var portA1 = new TestPort(A, 1);
            var portA2 = new TestPort(A, 2);

            // make basic connection
            Assert.That(portA0.Connect(portA1), Is.True);
            Assert.That(portA1.Connect(portA2), Is.True);

            // Failed circular connection
            Assert.That(portA2.Connect(portA0), Is.False);
        }

        [Test]
        public void Equability()
        {
            {
                var pA = new TestPort(A, 0);
                var pB = new TestPort(A, 0);
                Assert.That(pA, Is.EqualTo(pB));
            }

            {
                var pA = new TestPort(A, 0);
                var pB = new TestPort(A, 1);
                Assert.That(pA, Is.Not.EqualTo(pB));
            }
        }

        [Test]
        public void Disconnect()
        {
            var portA = new TestPort(A);
            var portB = new TestPort(B);

            // failed to disconnect port has never connection
            Assert.That(portA.Disconnect(portA), Is.False);
            Assert.That(portA.Disconnect(portB), Is.False);

            // make connection
            portA.Connect(portB);

            // failed to null disconnect
            Assert.That(portA.Disconnect(null), Is.False);

            // success to disconnect
            Assert.That(portA.Disconnect(portB), Is.True);
            // already disconnected from other node
            Assert.That(portB.Disconnect(portA), Is.False);

            // make re connection
            portA.Connect(portB);

            // success to disconnect
            Assert.That(portB.Disconnect(portA), Is.True);
            // already disconnected from other node
            Assert.That(portA.Disconnect(portB), Is.False);
        }

        [Test]
        public void IsConnected()
        {
            var portA = new TestPort(A);
            var portB = new TestPort(B);

            // make connection
            portA.Connect(portB);

            Assert.That(portA.IsConnected(null), Is.False);
            Assert.That(portA.IsConnected(portB), Is.True);

            // make other ports at A
            var portA1 = new TestPort(A, 1);
            Assert.That(portA.IsConnected(portA1), Is.False);
        }

        [Test]
        public void ConnectedPorts()
        {
            var portA = new TestPort(A);
            var portB = new TestPort(B);

            // connected ports counts is zero
            Assert.That(portA.ConnectedPorts.Count(), Is.EqualTo(0));

            // make connection
            portA.Connect(portB);

            // connected ports counts is one (port A and B)
            Assert.That(portA.ConnectedPorts.Count(), Is.EqualTo(1));
            Assert.That(portB.ConnectedPorts.Count(), Is.EqualTo(1));

            // portA has only portB
            foreach (var p in portA.ConnectedPorts)
            {
                if (p != portB)
                    Assert.Fail();
            }

            // portB has only portA
            foreach (var p in portB.ConnectedPorts)
            {
                if (p != portA)
                    Assert.Fail();
            }
        }

        [Test]
        public void INodeEnumeration()
        {
            var portA = new TestPort(A);
            var portB = new TestPort(B);

            // make connection
            portA.Connect(portB);

            // portA enumeration check
            Assert.That(portA.Count(), Is.EqualTo(1));
            Assert.That(portA.Count(node => node == B), Is.EqualTo(1));

            // portB enumeration check
            Assert.That(portB.Count(), Is.EqualTo(1));
            Assert.That(portB.Count(node => node == A), Is.EqualTo(1));
        }
    }
}
