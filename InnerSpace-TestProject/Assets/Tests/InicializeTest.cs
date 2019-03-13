using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests
{
    public class InitializeTest
    {
        [Test]
        public void VerifyIfNonExistComponent()
        {
            GameObject gameObject = GameObject.Find("MainText");

            Assert.Throws<MissingComponentException>(
                () => gameObject.GetComponent<Rigidbody>().velocity = Vector3.one
                );
        }

        [UnityTest]
        public IEnumerator VerifyMainText()
        {
            GameObject gameObject = GameObject.Find("MainText");
            var t = gameObject.GetComponent<Text>().text;
            Assert.IsNotNull(t);

            yield return null;
        }

        [UnityTest]
        public IEnumerator TestLoadJson()
        {
            var json = Resources.Load<TextAsset>("Json/tips");
            var t = System.Text.Encoding.ASCII.GetString(json.bytes);
            Assert.DoesNotThrow( ()=> JsonConvert.DeserializeObject<TipModelTest>(t));

            yield return null;
        }
    }
}
