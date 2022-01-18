using Scriban.Runtime;
using Sitecore.XA.Foundation.Abstractions;
using Sitecore.XA.Foundation.Scriban.Pipelines.GenerateScribanContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using Sitecore.Data.Fields;

namespace Sitecore.Foundation.SXAExtensions.Scriban.Pipelines.GenerateScribanContext
{
    public class GetDroplinkValue
    {
        private readonly IContext context;
        private delegate string Delegate(Item item , string fieldName);

        public GetDroplinkValue(IContext context)
        {
            this.context = context;
        }

        public void Process(GenerateScribanContextPipelineArgs args)
        {
            args.GlobalScriptObject.Import("sc_droplink_value", new Delegate(GetDroplinkValueImpl));
        }

        public string GetDroplinkValueImpl(Item item , string field)
        {

            if (item == null || field == null)
            {
                return null;
            }
          ReferenceField  itm = ((ReferenceField)item.Fields["Product"]).TargetItem.Fields[field];
            string result = itm.TargetItem.Fields["Style Name"].Value;
            return result;
        }


    }

}

