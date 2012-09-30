using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Alchemy.Common;

namespace Alchemy.Tests
{
    [TestClass]
    public class AlchemyEngineTests
    {
        [TestMethod]
        public void TestForReaction()
        {
            AlchemyEngine engine = new AlchemyEngine();
            
            var elements1 = engine.GetUsableElements();
            Assert.AreEqual(elements1.Count(), 4);

            var water = elements1.Where(_ => _.Name == "Water").First();
            var fire = elements1.Where(_ => _.Name == "Fire").First();
            
            var steam1 = engine.TestForReaction(water, fire);
            Assert.AreEqual(steam1.Name, "Steam");

            var elements2 = engine.GetUsableElements();
            Assert.AreEqual(elements2.Count(), 5);

            var steam2 = engine.TestForReaction(water, fire);
            Assert.AreEqual(steam2.Name, "Steam");

            var elements3 = engine.GetUsableElements();
            Assert.AreEqual(elements3.Count(), 5);
        }
    }
}
