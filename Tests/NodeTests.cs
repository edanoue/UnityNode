#nullable enable
using System;
using NUnit.Framework;
using Edanoue.Node.Internal;

public sealed class NodeTests
{
    [Test]
    public void ParentingTest()
    {
        var A = new Node("foo");
        Assert.That(A.Path, Is.EqualTo("/foo"));

        var B = new Node("bar", A);
        Assert.That(B.Path, Is.EqualTo("/foo/bar"));
    }

    [Test]
    public void Connect()
    {
        var A = new Node("foo");
        var B = new Node("bar");
        var A_Out = A.OutputPort();
        var B_In = B.InputPort();

        // Connect A -> B
        Assert.That(A_Out.Connect(B_In), Is.True);

        // Check connections
        Assert.That(A_Out.IsConnected(B_In), Is.True);
        Assert.That(B_In.IsConnected(A_Out), Is.True);
    }

    [Test]
    public void DisconnectNormal()
    {
        var A = new Node("foo");
        var B = new Node("bar");
        var A_Out = A.OutputPort();
        var B_In = B.InputPort();

        // Disconnect A -> B failed
        Assert.That(A_Out.Disconnect(B_In), Is.False);

        // Connect A -> B
        A_Out.Connect(B_In);

        // Disconnect A -> B succeed
        Assert.That(A_Out.Disconnect(B_In), Is.True);

        // Check connections
        Assert.That(A_Out.IsConnected(B_In), Is.False);
        Assert.That(B_In.IsConnected(A_Out), Is.False);
    }

    [Test]
    public void DisconnectInverse()
    {
        var A = new Node("foo");
        var B = new Node("bar");
        var A_Out = A.OutputPort();
        var B_In = B.InputPort();

        // Connect A -> B
        A_Out.Connect(B_In);

        // Disconnect B -> A succeed
        Assert.That(B_In.Disconnect(A_Out), Is.True);

        // Check connections
        Assert.That(A_Out.IsConnected(B_In), Is.False);
        Assert.That(B_In.IsConnected(A_Out), Is.False);
    }
}
