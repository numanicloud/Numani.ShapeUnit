namespace Numani.ShapeUnit.Test;

public class Tests
{
    [Test]
    public void �l��null�̂Ƃ��Ƀ��b�h���o����()
    {
        MyHoge? hoge = null;

        Assert.Throws<AssertionException>(() =>
        {
            hoge.BeginAssertion()
                .NotNull();
        });
    }

    [Test]
    public void �l��null�łȂ��Ƃ��Ƀ��b�h���o����()
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
    public void First�v���p�e�B�̒l�܂œ����Ƃ��ɃO���[�����o��()
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
    public void First�v���p�e�B�̒l���Ⴄ�Ƃ��Ƀ��b�h���o����()
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
    public void �R���e�L�X�g�𒊏o����First�����������b�h���o����()
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
    public void �R���e�L�X�g�𒊏o����First���������O���[�����o����()
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
    public void �z�񂪋�ł��邱�Ƃ����؂��ă��b�h���o����()
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
    public void �z�񂪋�ł��邱�Ƃ����؂��ăO���[�����o����()
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
    public void �z��̒l�������܂߈�v���Ă��邱�Ƃ����؂��ă��b�h���o����()
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
    public void �z��̒l�������܂߈�v���Ă��邱�Ƃ����؂��ăO���[�����o����()
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
    public void �z��̒l����ł��邱�Ƃ�Sequence��p���Č��؂����b�h���o����()
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
    public void �z�񂪋�ł��邱�Ƃ�Sequence��p���Č��؂��O���[�����o����()
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
    public void �l��Select�Ŏˉe���Ă��瓙���������؂����b�h���o����()
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
    public void �^�`�F�b�N�����ă��b�h���o����()
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