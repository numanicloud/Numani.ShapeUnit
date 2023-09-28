using NUnit.Framework;

namespace Numani.ShapeUnit;

public class SequenceAssertionContext<T> : IDisposable
{
    private int _index = 0;

    public required T[] Context { get; init; }

    public AssertionContext<T> Next()
    {
        Assert.That(_index + 1, Is.LessThanOrEqualTo(Context.Length));

        var result = Context[_index];
        _index++;
        return new AssertionContext<T>
        {
            Actual = result
        };
    }

    public void Dispose()
    {
        Assert.That(_index, Is.EqualTo(Context.Length));
    }
}