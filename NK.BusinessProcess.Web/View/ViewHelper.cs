using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NK.BusinessProcess.Web.View
{
    public class ViewHelper
    {
        public static List<SelectListItem> EnumToSelectList<T>()
        {
            return (Enum.GetValues(typeof(T)).Cast<T>().Select(e => new SelectListItem() { Text = e.ToString(), Value = ((int)((object)e)).ToString() })).OrderBy(x => x.Text).ToList();
        }

        public static List<T> EnumToList<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().OrderBy(x => x.ToString()).ToList();
        }

        public static List<SelectListItem> EnumListToSelectList<T>(List<T> list)
        {
            return list.Select(e => new SelectListItem() { Text = e.ToString(), Value = ((int)((object)e)).ToString() }).OrderBy(x => x.Text).ToList();
        }
             
        public static List<SelectListItem> GetSelectList<T>(List<int> selectedItems)
        {
            List<SelectListItem> items = EnumToSelectList<T>();

            foreach (var item in items)
            {
                item.Selected = selectedItems != null && selectedItems.Contains(int.Parse(item.Value));
            }

            return items;
        }
    }
}
