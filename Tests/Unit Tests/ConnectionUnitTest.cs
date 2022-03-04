#nullable enable
using System;
using NUnit.Framework;
using Edanoue.Node.Interfaces;
using Edanoue.Node.Internal;

namespace UnitTests
{
    /// <summary>
    /// Connection internal class unit test case
    /// </summary>
    public sealed class ConnectionUnitTest
    {
        // Make nodes
        readonly INode A = new TestNode("A");
        readonly INode B = new TestNode("B");
        IPort A_IN => A.InputPort();
        IPort A_OUT => A.OutputPort();
        IPort B_IN => B.InputPort();
        IPort B_OUT => B.OutputPort();

        [Test]
        public void PropertyA()
        {
            // Make connections
            var connectionAB = new Connection(A_IN, B_OUT);

            // check two connections is same
            Assert.That(connectionAB.A, Is.EqualTo(A_IN));
        }

        [Test]
        public void PropertyB()
        {
            // Make connections
            var connectionAB = new Connection(A_IN, B_OUT);

            // check two connections is same
            Assert.That(connectionAB.B, Is.EqualTo(B_OUT));
        }

        [Test]
        public void Equability()
        {
            // Make connections
            var connectionAB = new Connection(A_IN, B_OUT);
            var connectionBA = new Connection(B_OUT, A_IN);

            // Basically check
            {
                Assert.That(connectionAB == null, Is.False);
                Connection? nullableConnectionA = null;
                Connection? nullableConnectionB = null;
                Assert.That(nullableConnectionA == nullableConnectionB, Is.True);
                Assert.That(nullableConnectionA == connectionAB, Is.False);
                nullableConnectionA = connectionAB;
                Assert.That(connectionAB == nullableConnectionA, Is.True);
            }

            // check two connections is same
            Assert.That(connectionAB, Is.EqualTo(connectionBA));
            Assert.That(connectionAB == connectionBA, Is.True);
            Assert.That(connectionAB != connectionBA, Is.False);
        }

        [Test]
        public void NotEquability()
        {
            // Make connections
            var connectionAB = new Connection(A_IN, B_OUT);
            var connectionBA = new Connection(B_IN, A_OUT);

            // check two connections is different
            Assert.That(connectionAB, Is.Not.EqualTo(connectionBA));
        }

        [Test]
        public void Other()
        {
            // Make connections
            var connectionAB = new Connection(A_IN, B_OUT);

            // Get other port
            Assert.That(connectionAB.Other(A_IN), Is.EqualTo(B_OUT));
            Assert.That(connectionAB.Other(B_OUT), Is.EqualTo(A_IN));

            // non asscosiation port raise error
            Assert.Throws<System.ArgumentOutOfRangeException>(() =>
            {
                connectionAB.Other(A_OUT);
            });
        }
    }
}