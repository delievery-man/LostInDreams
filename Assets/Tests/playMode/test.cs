using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class test
{
    // A Test behaves as an ordinary method
    [Test]
    public void testSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator PlayerTakeDamageWhileNotInvulnerable()
    {

                 GameObject player = 
                     MonoBehaviour.Instantiate(Resources.Load<GameObject>("Player"));
                 player.GetComponent<DealDamage>().health = 3;
                 player.GetComponent<DealDamage>().shieldTimer = player.AddComponent<Shield>();
                 player.GetComponent<DealDamage>().isInvulnerable = false;
                 player.GetComponent<DealDamage>().shieldTimer.isTest = true;
                 player.GetComponent<DealDamage>().PlayerDealDamage(1);
                 Assert.AreEqual(player.GetComponent<DealDamage>().health, 2);
                 
                 yield return null;
    }
    
    [UnityTest]
    public IEnumerator PlayerTakeDamageWhileInvulnerable()
    {

        GameObject player = 
            MonoBehaviour.Instantiate(Resources.Load<GameObject>("Player"));
        player.GetComponent<DealDamage>().health = 3;
        player.GetComponent<DealDamage>().shieldTimer = player.AddComponent<Shield>();
        player.GetComponent<DealDamage>().isInvulnerable = true;
        player.GetComponent<DealDamage>().shieldTimer.isTest = true;
        player.GetComponent<DealDamage>().PlayerDealDamage(1);
        Assert.AreEqual(player.GetComponent<DealDamage>().health, 3);
                 
        yield return null;
    }
    [UnityTest]
    public IEnumerator PlayerTakeDamageAndDie()
    {

        GameObject player = 
            MonoBehaviour.Instantiate(Resources.Load<GameObject>("Player"));
        player.GetComponent<DealDamage>().health = 3;
        player.GetComponent<DealDamage>().shieldTimer = player.AddComponent<Shield>();
        player.GetComponent<DealDamage>().isInvulnerable = false;
        player.GetComponent<DealDamage>().shieldTimer.isTest = true;
        player.GetComponent<DealDamage>().PlayerDealDamage(3);
       Assert.AreEqual(player.GetComponent<DealDamage>().isDead, true);
        yield return null;
    }
}
