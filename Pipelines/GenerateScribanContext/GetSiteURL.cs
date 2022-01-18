using Sitecore.XA.Foundation.Abstractions;
using Sitecore.XA.Foundation.Scriban.Pipelines.GenerateScribanContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using Scriban.Runtime;

namespace Sitecore.Foundation.SXAExtensions.Scriban.Pipelines.GenerateScribanContext
{
    public class GetSiteURL : IGenerateScribanContextProcessor
    {
        protected readonly IPageMode PageMode;
        private readonly IContext context;

        private delegate string SiteURLDelegate(Item item);

        public GetSiteURL(IPageMode pageMode, IContext context)
        {
            PageMode = pageMode;
            this.context = context;

        }

        public void Process(GenerateScribanContextPipelineArgs args)
        {
            var siteUrl = new SiteURLDelegate(GetSiteURLImpl);
            args.GlobalScriptObject.Import("sc_site_url", (Delegate)siteUrl);
        }
        public string  GetSiteURLImpl(Item item)
        {

            if (item == null)
            {
                return null;
            }


            string url = "";
           url = Sitecore.Links.LinkManager.GetItemUrl(item);

            return url;

        }
    }

}
