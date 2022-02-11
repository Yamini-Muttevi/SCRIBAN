using Scriban.Runtime;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using Sitecore.XA.Foundation.Abstractions;
using Sitecore.XA.Foundation.Scriban.Pipelines.GenerateScribanContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Foundation.SXAExtensions.Scriban.Model

namespace Sitecore.Foundation.Scriban.Pipelines.GenerateScribanContext
{
    public class GetDataSourceFromItem : IGenerateScribanContextProcessor
    {

        private readonly IContext context;
        protected readonly IPageMode PageMode;
        private delegate List<string> Delegate(Item item);

        public GetDataSourceFromItem(IPageMode pageMode, IContext context)
        {
            PageMode = pageMode;
            this.context = context;
        }

        public void Process(GenerateScribanContextPipelineArgs args)
        {
            args.GlobalScriptObject.Import("sc_getdatasource", new Delegate(GetDataSourceFromItemImpl));
        }

        public List<string> GetDataSourceFromItemImpl(Item item)
        {
            List<string> tagList = new List<string>();

            if (item != null)
            {
                
                foreach (Item dataSourceItem in Sitecore.Context.Item.GetDataSourceItems())
                {
                    if(dataSourceItem.Fields["Tag"] != null)
                    {
                        tagList.Add(dataSourceItem.Fields["Tag"].Value.ToString());

                    }
                }
                return tagList;

            }
            return tagList;
     
        }
    }
}