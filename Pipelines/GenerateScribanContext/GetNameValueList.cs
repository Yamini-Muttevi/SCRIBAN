using Scriban.Runtime;
using Sitecore.XA.Foundation.Abstractions;
using Sitecore.XA.Foundation.Scriban.Pipelines.GenerateScribanContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using Sitecore.Foundation.SXAExtensions.Scriban.Model;
using Sitecore.Data.Fields;

namespace Sitecore.Foundation.SXAExtensions.Scriban.Pipelines.GenerateScribanContext
{
    public class GetNameValueList
    {

        protected readonly IPageMode PageMode;
        private readonly IContext context;
        private delegate  NameValueInfo NameValueDelegate(Item item, object linkFieldName);
        public GetNameValueList(IPageMode pageMode, IContext context)
        {
            PageMode = pageMode;
            this.context = context;

        }

        public void Process(GenerateScribanContextPipelineArgs args)
        {
            var nameValueInfo = new NameValueDelegate(GetNameValueInfo);
            args.GlobalScriptObject.Import("sc_namevaluelist_info", (Delegate)nameValueInfo);
        }

        public NameValueInfo GetNameValueInfo(Item item, object field )
        {
            if (item == null || field == null)
            {
                return null;
            }
           ReferenceField list  = (ReferenceField)item.Fields[(string)field];


            return new NameValueInfo(list);


        }

    }
}
