#nullable enable
using Edanoue.Node.Interfaces;
using Edanoue.Node.Internal;

internal class TestNode : NodeBase
{
    InputPort _input;
    OutputPort _output;

    internal TestNode(string name, NodeBase? parent = null) : base(name, parent)
    {
        _input = new(this, 0);
        _output = new(this, 0);
        _input.Connect(_output);
    }

    public override IPort InputPort(int index = 0)
    {
        return _input;
    }

    public override IPort OutputPort(int index = 0)
    {
        return _output;
    }
}
