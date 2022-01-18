using Sitecore.Data.Fields;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Sitecore.Foundation.SXAExtensions.Scriban.Model
{
    public class NameValueInfo
    {

        public string name { get; set; }

        public IEnumerable<string> value { get; set; }


        public NameValueInfo(ReferenceField reference)
        {

            if (reference != null)
            {

                string values = reference.InnerField.Value;

                NameValueCollection nvc = new NameValueCollection();

                foreach (string vp in Regex.Split(values, "&"))
                {
                    string[] singlePair = Regex.Split(vp, "=");
                    if (singlePair.Length == 2)
                    {
                        nvc.Add(singlePair[0], singlePair[1]);
                    }
                }

                value = nvc.Cast<string>().Select(e => nvc[e]);
            }
        }
       
    }
}
