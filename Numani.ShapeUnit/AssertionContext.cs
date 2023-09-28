using System.Reflection;
using NUnit.Framework;

namespace Numani.ShapeUnit;

/// <summary>
/// 単体テストのために、ある1つの値について検証する責任を持つクラス。
/// </summary>
/// <typeparam name="TActual">検証する値の型。</typeparam>
public sealed class AssertionContext<TActual>
{
    /// <summary>
    /// このコンテキストが検査する対象となる値。
    /// </summary>
    public required TActual Actual { get; init; }

    /// <summary>
    /// この値が指定した型であることを検査し、成功したらその型を持つコンテキストとして返します。
    /// </summary>
    /// <typeparam name="T">値がとるべき型。</typeparam>
    /// <returns>型情報が更新されたコンテキスト。</returns>
    /// <exception cref="Exception"></exception>
    public AssertionContext<T> Type<T>() where T : TActual
    {
        Assert.That(Actual, Is.TypeOf<T>());
        if (Actual is not T cast) throw new Exception("Assertion succeeded but casting failed.");
        return new AssertionContext<T>
        {
            Actual = cast
        };
    }

    /// <summary>
    /// 指定した値とコンテキストの値が等しいことを検査します。
    /// </summary>
    /// <param name="expected">等しいことが期待される値。</param>
    /// <returns>Fluent Interface用のコンテキスト オブジェクト。</returns>
    public AssertionContext<TActual> EqualsTo(TActual expected)
    {
        Assert.That(Actual, Is.EqualTo(expected));
        return this;
    }

    /// <summary>
    /// 指定した値が、コンテキストの値を射影した値に等しいことを検査します。
    /// </summary>
    /// <typeparam name="TExpected">期待される値の型。</typeparam>
    /// <param name="expected">等しいことが期待される値。</param>
    /// <param name="selector">コンテキストの値を射影するデリゲート。</param>
    /// <returns>Fluent Interface用のコンテキスト オブジェクト。</returns>
    public AssertionContext<TActual> AreEqual<TExpected>(
        TExpected expected,
        Func<TActual, TExpected> selector)
    {
        Assert.That(selector(Actual), Is.EqualTo(expected));
        return this;
    }

    /// <summary>
    /// このコンテキストの値を射影した新たな値を扱う新たなコンテキストを生成します。
    /// </summary>
    /// <typeparam name="T">新たなコンテキストの値の型。</typeparam>
    /// <param name="selector">このコンテキストの値を射影するデリゲート。</param>
    /// <returns>射影された新しいコンテキスト。</returns>
    public AssertionContext<T> Select<T>(Func<TActual, T> selector)
    {
        return new AssertionContext<T>()
        {
            Actual = selector(Actual)
        };
    }

    /// <summary>
    /// この値から別の配列へ変換し、その配列のコンテキストを返します。
    /// </summary>
    /// <typeparam name="TItem">配列の要素の型。</typeparam>
    /// <param name="selector">検査値から配列へ変換するデリゲート。</param>
    /// <returns>配列の要素ごとに検査するための<seealso cref="SequenceAssertionContext{T}"/>。</returns>
    public SequenceAssertionContext<TItem> Sequence<TItem>(Func<TActual, TItem[]> selector)
    {
        return new SequenceAssertionContext<TItem>()
        {
            Context = selector(Actual)
        };
    }

    /// <summary>
    /// この値から別の配列を抽出したとき、それが空であることを検査します。
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="selector"></param>
    /// <returns></returns>
    public AssertionContext<TActual> Empty<TItem>(Func<TActual, TItem[]> selector)
    {
        Sequence(selector).Dispose();
        return this;
    }

    /// <summary>
    /// このコンテキストを変数として定義するのに使用します。
    /// </summary>
    /// <param name="variable">出力先の変数参照。</param>
    public void Declare(out AssertionContext<TActual> variable)
    {
        variable = this;
    }
}