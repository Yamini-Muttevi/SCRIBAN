using Sitecore.Data;
using Sitecore.Data.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sitecore.Foundation.SXAExtensions.Scriban.Model
{
    public class MultirootTreelistInfo
    {
        public string value { get; set; }
        public MultirootTreelistInfo(ReferenceField reference)
        {

            if (reference != null)
            {
                value = reference.TargetItem.Fields["ID"].Value.ToString();

            }
        }
    }
}