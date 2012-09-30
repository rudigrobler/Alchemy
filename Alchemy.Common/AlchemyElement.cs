using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alchemy.Common
{
    public class AlchemyElement
    {
        private static Uri _baseUri = new Uri("ms-appx:///");

        public string Name { get; set; }

        public Uri Image
        {
            get
            {
                return new Uri(_baseUri, "Elements/" + Name + ".png");
            }
        }

        public static AlchemyElement Create(string name)
        {
            return new AlchemyElement() { Name = name };
        }
    }
}
