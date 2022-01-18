using Sitecore.XA.Foundation.Abstractions;
using Sitecore.Foundation.SXAExtensions.Scriban.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using Sitecore.XA.Foundation.Scriban.Pipelines.GenerateScribanContext;
using Scriban.Runtime;
using Sitecore.Data.Fields;

namespace Sitecore.Foundation.SXAExtensions.Scriban.Pipelines.GenerateScribanContext
{
    public class GetMultirootTreelistValue
    {
        protected readonly IPageMode PageMode;
        private readonly IContext context;
        private delegate MultirootTreelistInfo MultirootTreelistDelegate(Item item, object linkFieldName);
        public GetMultirootTreelistValue(IPageMode pageMode, IContext context)
        {
            PageMode = pageMode;
            this.context = context;

        }

        public void Process(GenerateScribanContextPipelineArgs args)
        {
            var multirootTreelistInfo = new MultirootTreelistDelegate(GetMultirootTreelistValueImpl);
            args.GlobalScriptObject.Import("sc_multiroottreelist_info", (Delegate)multirootTreelistInfo);
        }

        public MultirootTreelistInfo GetMultirootTreelistValueImpl(Item item, object field)
        {
            if (item == null || field == null)
            {
                return null;
            }
            ReferenceField list = (ReferenceField)item.Fields[(string)field];


            return new MultirootTreelistInfo(list);



        }
    }
}