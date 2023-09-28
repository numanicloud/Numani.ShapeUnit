namespace Numani.ShapeUnit;

public class SequenceAssertionContext<T> : IDisposable
{
    private int _index = 0;

    public required T[] Context { get; init; }
    public required IAssert Assert { get; init; }

    public AssertionContext<T> Next()
    {
        Assert.IsLessThanOrEquals(Context.Length, _index + 1);

        var result = Context[_index];
        _index++;
        return new AssertionContext<T>
        {
            Actual = result,
            Assert = Assert
        };
    }

    public void Dispose()
    {
        Assert.AreEqual(Context.Length, _index);
    }
}