namespace Numani.ShapeUnit.Test;

public class Tests
{
    [Test]
    public void 値がnullのときにレッドを出せる()
    {
        MyHoge? hoge = null;

        Assert.Throws<AssertionException>(() =>
        {
            hoge.BeginAssertion()
                .NotNull();
        });
    }

    [Test]
    public void 値がnullでないときにレッドを出せる()
    {
        MyHoge? hoge = new MyHoge()
        {
            First = 9,
            Second = 10,
        };

        Assert.Throws<AssertionException>(() =>
        {
            hoge.BeginAssertion()
                .Null();
        });
    }

    [Test]
    public void Firstプロパティの値まで同じときにグリーンを出す()
    {
        object actual = new MyHoge()
        {
            First = 11,
            Second = 12,
        };

        actual.BeginAssertion()
            .NotNull()
            .Type<MyHoge>()
            .AreEqual(11, x => x.First);
    }

    [Test]
    public void Firstプロパティの値が違うときにレッドを出せる()
    {
        object actual = new MyHoge()
        {
            First = 11,
            Second = 12,
        };

        Assert.Throws<AssertionException>(() =>
        {
            actual.BeginAssertion()
                .NotNull()
                .Type<MyHoge>()
                .AreEqual(13, x => x.First);
        });
    }

    [Test]
    public void コンテキストを抽出してFirstを検査しレッドを出せる()
    {
        object actual = new MyHoge()
        {
            First = 11,
            Second = 12,
        };

        Assert.Throws<AssertionException>(() =>
        {
            actual.BeginAssertion()
                .NotNull()
                .Type<MyHoge>()
                .Declare(out var obj);

            obj.AreEqual(13, x => x.First);
        });
    }

    [Test]
    public void コンテキストを抽出してFirstを検査しグリーンを出せる()
    {
        object actual = new MyHoge()
        {
            First = 11,
            Second = 12,
        };

        actual.BeginAssertion()
            .NotNull()
            .Type<MyHoge>()
            .Declare(out var obj);

        obj.AreEqual(11, x => x.First);
    }

    [Test]
    public void 配列が空であることを検証してレッドが出せる()
    {
        object actual = new MyFuga()
        {
            Ints = new[] { 1, 2, 3 },
        };

        Assert.Throws<AssertionException>(() =>
        {
            actual.BeginAssertion()
                .NotNull()
                .Type<MyFuga>()
                .Empty(x => x.Ints);
        });
    }

    [Test]
    public void 配列が空であることを検証してグリーンを出せる()
    {
        object actual = new MyFuga()
        {
            Ints = Array.Empty<int>(),
        };

        actual.BeginAssertion()
            .NotNull()
            .Type<MyFuga>()
            .Empty(x => x.Ints);
    }

    [Test]
    public void 配列の値が順序含め一致していることを検証してレッドを出せる()
    {
        object actual = new MyFuga()
        {
            Ints = new[] { 1, 2 }
        };

        Assert.Throws<AssertionException>(() =>
        {
            using var sequence = actual.BeginAssertion()
                .NotNull()
                .Type<MyFuga>()
                .Sequence(x => x.Ints);

            sequence.Next().EqualsTo(10);
            sequence.Next().EqualsTo(20);
        });
    }

    [Test]
    public void 配列の値が順序含め一致していることを検証してグリーンを出せる()
    {
        object actual = new MyFuga()
        {
            Ints = new[] { 1, 2 }
        };

        using var sequence = actual.BeginAssertion()
            .NotNull()
            .Type<MyFuga>()
            .Sequence(x => x.Ints);

        sequence.Next().EqualsTo(1);
        sequence.Next().EqualsTo(2);
    }

    [Test]
    public void 配列の値が空であることをSequenceを用いて検証しレッドを出せる()
    {
        object actual = new MyFuga()
        {
            Ints = new[] { 1, 2 }
        };

        Assert.Throws<AssertionException>(() =>
        {
            using var sequence = actual.BeginAssertion()
                .NotNull()
                .Type<MyFuga>()
                .Sequence(x => x.Ints);
        });
    }

    [Test]
    public void 配列が空であることをSequenceを用いて検証しグリーンを出せる()
    {
        object actual = new MyFuga()
        {
            Ints = Array.Empty<int>()
        };

        using var sequence = actual.BeginAssertion()
            .NotNull()
            .Type<MyFuga>()
            .Sequence(x => x.Ints);
    }

    [Test]
    public void 値をSelectで射影してから等しさを検証しレッドを出せる()
    {
        object actual = new MyHoge()
        {
            First = 9,
            Second = 10,
        };

        Assert.Throws<AssertionException>(() =>
        {
            actual.BeginAssertion()
                .NotNull()
                .Type<MyHoge>()
                .Select(x => x.First)
                .EqualsTo(91);
        });
    }

    [Test]
    public void 型チェックをしてレッドを出せる()
    {
        object actual = new MyHoge()
        {
            First = 1,
            Second = 2,
        };

        Assert.Throws<AssertionException>(() =>
        {
            actual.BeginAssertion()
                .NotNull()
                .Type<MyFuga>();
        });
    }

    private sealed class MyHoge
    {
        public int First { get; set; }
        public int Second { get; set; }
    }

    private sealed class MyFuga
    {
        public int[] Ints { get; set; }
    }
}