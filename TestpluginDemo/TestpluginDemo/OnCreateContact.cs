using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestpluginDemo
{
    public class OnCreateContact : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
            OrganizationServiceContext mycontext = new OrganizationServiceContext(service);

            if (context.Depth != 1)
                return;

            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
            {
                Entity entity = (Entity)context.InputParameters["Target"];
                entity.Attributes["jobtitle"] = "Prueba";
                /////////////////////////////////////////////////////////////////////////////


                // Similar al retrieve de Xrm.WebApi de javascript
                Entity em = service.Retrieve("contact", new Guid(), new Microsoft.Xrm.Sdk.Query.ColumnSet("name, etc"));

                // RetrieveMultiple se puede usar tanto con una query expression...
                QueryExpression q = new QueryExpression();
                EntityCollection ec = service.RetrieveMultiple(q); 
                //... o un fetchXML
                EntityCollection ecc = service.RetrieveMultiple(new FetchExpression("fetchXML"));

                // Sintaxis linQ
                Entity e = (from contact in mycontext.CreateQuery("contact")
                            where (int)contact["dni"] == 2946456
                            select contact).FirstOrDefault();

                //Con la entidad obtenida en el query, se puede hacer un Update:

                e.Attributes["name"] = "Hola";

                service.Update(e); // Existen otro métodos como Create y Delete, pero se usan distinto
            }
        }
    }
}
