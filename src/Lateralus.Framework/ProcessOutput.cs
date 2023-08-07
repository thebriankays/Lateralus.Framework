namespace Lateralus.Framework;

public sealed class ProcessOutput
{
    internal ProcessOutput(ProcessOutputType type, string text)
    {
        Type = type;
        Text = text;
    }

    public ProcessOutputType Type { get; }
    public string Text { get; }

    public void Deconstruct(out ProcessOutputType type, out string text)
    {
        type = Type;
        text = Text;
    }

    public override string ToString() => Type switch
    {
        ProcessOutputType.StandardError => "error: " + Text,
        _ => Text
    };
}
