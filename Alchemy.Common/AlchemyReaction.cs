using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alchemy.Common
{
    public class AlchemyReaction
    {
        public List<AlchemyElement> Source { get; set; }
        public AlchemyElement Result { get; set; }

        public static AlchemyReaction Create(string s1, string s2, string result)
        {
            return new AlchemyReaction()
            {
                Source = new List<AlchemyElement>()
                        {
                            AlchemyElement.Create(s1),
                            AlchemyElement.Create(s2) 
                        },
                Result = AlchemyElement.Create(result)
            };
        }
    }
}
