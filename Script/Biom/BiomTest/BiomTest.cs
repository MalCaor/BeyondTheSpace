using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BiomTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void BiomTestSimplePasses()
    {
        // Use the Assert class to test conditions
        BiomTestClass biom = new BiomTestClass(Color.black, "Test");
    }

    // A Test behaves as an ordinary method
    [Test]
    public void BiomTestNoDuplicateCheck()
    {
        // Use the Assert class to test conditions
        BiomTestClass biom = new BiomTestClass(Color.black, "Test");
        try
        {
            BiomTestClass biom2 = new BiomTestClass(Color.black, "Test");
            Assert.Fail("BiomTestNoDuplicateCheck : Duplicata possible");
        }
        catch (System.ArgumentException)
        {
            // OK
        }
        catch (System.Exception e)
        {
            Assert.Fail(e.Message);
        }
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator BiomTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
