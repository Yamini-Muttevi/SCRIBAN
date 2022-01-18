using Scriban.Runtime;
using Sitecore.XA.Foundation.Abstractions;
using Sitecore.XA.Foundation.Scriban.Pipelines.GenerateScribanContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using Sitecore.Data.Fields;
using Sitecore.Data;

namespace Sitecore.Foundation.SXAExtensions.Scriban.Pipelines.GenerateScribanContext
{
    public class GetModelNames
    {
        private readonly IContext context;
        private delegate string Delegate(Item item ,string field);

        public GetModelNames(IContext context)
        {
            this.context = context;
        }

        public void Process(GenerateScribanContextPipelineArgs args)
        {
            args.GlobalScriptObject.Import("sc_modelnames_info", new Delegate(GetModelNamesImpl));
        }

        public string GetModelNamesImpl(Item item, string field)
        {

            string fieldNames = item.Fields[field].Value;

            List<Item> itmList = new List<Item>();
            List<string> res = new List<string>();
            string result;
            string[] model = fieldNames.Split('|').ToArray();

          foreach(var i in model)
           itmList.Add(Sitecore.Context.Database.Items.GetItem(i));

            foreach (var name in itmList)
             res.Add(name.Fields["Model Name"].Value);

          result =  string.Join("/", res);
            return result; 
        }     
       

    }
}
