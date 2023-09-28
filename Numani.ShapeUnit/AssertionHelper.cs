using NUnit.Framework;

namespace Numani.ShapeUnit;

public static class AssertionHelper
{
    /// <summary>
    /// 単体テストで検証すべき値を、検証コンテキストで包みます。
    /// </summary>
    /// <typeparam name="TActual">検証すべき値の型。</typeparam>
    /// <param name="obj">検証すべき値。</param>
    /// <returns>検証コンテキスト。</returns>
    public static AssertionContext<TActual?> BeginAssertion<TActual>(this TActual? obj)
    {
        return new AssertionContext<TActual?>()
        {
            Actual = obj
        };
    }

    /// <summary>
    /// 検証コンテキストが指している値が非nullであることを検査し、非nullが保証された新しいコンテキストを返します。
    /// </summary>
    /// <typeparam name="TActual">検証すべき値の型。</typeparam>
    /// <param name="context">検証コンテキスト。</param>
    /// <returns>非nullであることが保証された新しいコンテキスト。</returns>
    /// <exception cref="Exception"></exception>
    public static AssertionContext<TActual> NotNull<TActual>(
        this AssertionContext<TActual?> context)
    {
        Assert.That(context.Actual, Is.Not.Null);
        if (context.Actual is null) throw new Exception("Assertion succeeded but pattern match failed.");
        return new AssertionContext<TActual>()
        {
            Actual = context.Actual
        };
    }

    public static void Null<TActual>(this AssertionContext<TActual?> context)
    {
        Assert.That(context.Actual, Is.Null);
    }
}