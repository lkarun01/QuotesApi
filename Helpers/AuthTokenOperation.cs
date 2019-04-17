using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuotesApi.Helpers
{
    public class AuthTokenOperation : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            //swaggerDoc.Host = "quotesapilalanke.auth0.com";
            //throw new NotImplementedException();
            swaggerDoc.Paths.Add("/token", new PathItem
            {
                Post = new Operation
                {
                    Tags = new List<string> { "Auth" },
                    Consumes = new List<string>
                    {
                        "application/x-www-form-urlencoded"
                    },
                    Parameters = new List<IParameter>
                    {
                        new NonBodyParameter
                        {
                            Type="string",
                            Name="grant_type",
                            Required=true,
                            In="fromData"
                        },

                        new NonBodyParameter
                        {
                            Type="string",
                            Name="client_id",
                            Required=true,
                            In="fromData"
                        },
                        new NonBodyParameter
                        {
                            Type="string",
                            Name="audience",
                            Required=true,
                            In="fromData"
                        },
                        new NonBodyParameter
                        {
                            Type="string",
                            Name="username",
                            Required=true,
                            In="fromData"
                        },

                        new NonBodyParameter
                        {
                            Type="string",
                            Name="password",
                            Required=true,
                            In="fromData"
                        },
                        new NonBodyParameter
                        {
                            Type="string",
                            Name="client_secret",
                            Required=true,
                            In="fromData"
                        }

                    }
                }
            });
        }
    }
}
