using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class editTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void editTestsSimplePasses()
    {

    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator editTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        GameObject player = 
            MonoBehaviour.Instantiate(Resources.Load<GameObject>("Player"));
        player.GetComponent<DealDamage>().health = 1;
        player.GetComponent<DealDamage>().PlayerDealDamage(1);
        Assert.AreEqual(player.GetComponent<DealDamage>().health, 0);
        
        yield return null;
    }
}
