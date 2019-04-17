using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuotesApi.Helpers
{
    public class Parameter : IParameter
    {
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string In { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Description { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool Required { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Dictionary<string, object> Extensions => throw new NotImplementedException();

        public string Type { get; set; }
    }
}
