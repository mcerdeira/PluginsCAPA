using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFA_Demo
{
    public class PnetDoAction : CodeActivity
    {
        [Input("Apellido"), RequiredArgument]
        public InArgument<String> InApellido
        {
            get;
            set;
        }

        [Output("Contacto")]
        [ReferenceTarget("contact")]
        public OutArgument<EntityReference> OutContacto
        {
            get;
            set;
        }

        protected override void Execute(CodeActivityContext executionContext)
        {
            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
            OrganizationServiceContext mycontext = new OrganizationServiceContext(service);

            int step = 0;

            try
            {
                step++;
                String apellido = InApellido.Get<String>(executionContext);
                step++;
                Guid me = context.PrimaryEntityId;
                step++;

                Entity e = (from c in mycontext.CreateQuery("contact")
                            where (string)c["lastname"] == apellido
                            && (Guid)c["contactid"] != me
                            select c).FirstOrDefault();
                step++;
                if (e != null)
                {
                    step++;
                    OutContacto.Set(executionContext, e.ToEntityReference());
                }
                step++;
            }
            catch(Exception ex)
            {
                throw new Exception(String.Format("{0} - {1}", step.ToString(), ex.Message));
            }
        }
    }
}
