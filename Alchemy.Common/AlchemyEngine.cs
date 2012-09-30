using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alchemy.Common
{
    public class AlchemyEngine
    {
        List<AlchemyElement> elements = new List<AlchemyElement>();
        List<AlchemyReaction> reactions = new List<AlchemyReaction>();

        public AlchemyEngine()
        {
            elements.Add(AlchemyElement.Create("Water"));
            elements.Add(AlchemyElement.Create("Fire"));
            elements.Add(AlchemyElement.Create("Earth"));
            elements.Add(AlchemyElement.Create("Air"));

            reactions.Add(AlchemyReaction.Create("Water", "Fire", "Steam"));
            reactions.Add(AlchemyReaction.Create("Water", "Earth", "Mud"));
            reactions.Add(AlchemyReaction.Create("Water", "Air", "Rain"));
            reactions.Add(AlchemyReaction.Create("Water", "Fire", "Steam"));
            reactions.Add(AlchemyReaction.Create("Water", "Water", "Sea"));
        }

        public List<AlchemyElement> GetUsableElements()
        {
            return elements;
        }

        public AlchemyElement TestForReaction(AlchemyElement e1, AlchemyElement e2)
        {
            if (e1.Name != e2.Name)
            {
                var reaction = from r in reactions
                               where
                                    r.Source.Where(_ => _.Name == e1.Name).Any() &&
                                    r.Source.Where(_ => _.Name == e2.Name).Any()
                               select r;

                if (reaction.Any())
                {
                    var element = reaction.First().Result;
                    if (!elements.Where(_ => _.Name == element.Name).Any())
                    {
                        elements.Add(element);
                    }
                    return element;
                }
                return null;
            }
            else
            {
                var reaction = from r in reactions
                               where
                                    r.Source.Where(_ => _.Name == e1.Name).Count() == 2
                               select r;

                if (reaction.Any())
                {
                    var element = reaction.First().Result;
                    if (!elements.Where(_ => _.Name == element.Name).Any())
                    {
                        elements.Add(element);
                    }
                    return element;
                }
                return null;
            }
        }
    }
}
