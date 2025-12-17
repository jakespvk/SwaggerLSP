using System;

namespace SwaggerLSP.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
        Console.WriteLine("setup test");
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
}
