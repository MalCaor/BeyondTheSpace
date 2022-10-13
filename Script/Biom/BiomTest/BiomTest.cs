using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using BiomBeyondTheSpace;

public class BiomTest
{
    // Test simple Instantiation
    [Test]
    public void BiomTestSimplePasses()
    {
        // clear to be sure nothing left from previous test
        Biom.allBioms.Clear();
        Biom biom = new Biom(Color.black, "Test");
    }

    // Test no duplicated Biom (two biom with the same color)
    [Test]
    public void BiomTestNoDuplicateCheck()
    {
        // clear to be sure nothing left from previous test
        Biom.allBioms.Clear();
        Biom biom = new Biom(Color.black, "Test");
        try
        {
            Biom biom2 = new Biom(Color.black, "Test");
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
