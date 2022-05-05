using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWFA
{
    public class WFDoThings : CodeActivity
    {
        [Input("Cantidad"), RequiredArgument]
        public InArgument<Double> InQuantity
        {
            get;
            set;
        }

        [Input("Contacto"), RequiredArgument]
        [ReferenceTarget("contact")]
        public InArgument<EntityReference> Contact { get; set; }

        [Output("Amount")]
        public OutArgument<Double> OutAmount
        {
            get;
            set;
        }

        protected override void Execute(CodeActivityContext context)
        {
            
        }
    }
}
