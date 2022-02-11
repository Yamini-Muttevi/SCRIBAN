using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Layouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Foundation.SXAExtensions.Scriban.Model
{
    public static class ItemExtensions
    {
        public static RenderingReference[] GetRenderingReferences(this Item i)
        {
            if (i == null)
            {
                return new RenderingReference[0];
            }
            return i.Visualization.GetRenderings(Sitecore.Context.Device, false);
        }

        public static List<Item> GetDataSourceItems(this Item i)
        {
            List<Item> list = new List<Item>();
            foreach (RenderingReference reference in i.GetRenderingReferences())
            {
                Item dataSourceItem = reference.GetDataSourceItem();
                if (dataSourceItem != null)
                {
                    list.Add(dataSourceItem);
                }
            }
            return list;
        }

        public static Item GetDataSourceItem(this RenderingReference reference)
        {
            if (reference != null)
            {
                return GetDataSourceItem(reference.Settings.DataSource, reference.Database);
            }
            return null;
        }

        private static Item GetDataSourceItem(string id, Database db)
        {
            Guid itemId;
            return Guid.TryParse(id, out itemId)
            ? db.GetItem(new ID(itemId))
            : db.GetItem(id);
        }
    }
}