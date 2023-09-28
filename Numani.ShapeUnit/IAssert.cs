using System.Numerics;

namespace Numani.ShapeUnit;

public interface IAssert
{
    void AreEqual<T>(T expected, T actual);
    void IsType<T>(object  actual);
    void IsNotNull<T>(T actual);
    void IsNull<T>(T actual);
    void IsLessThanOrEquals<T>(T expected, T actual) where T : INumber<T>;
}