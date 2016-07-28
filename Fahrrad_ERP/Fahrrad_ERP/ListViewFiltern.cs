using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fahrrad_ERP
{
    class ListViewFiltern
    {
        public List<List<string>> LookFor(List<List<string>> dataList, string value)
        {
            value = value.ToLower();
            List<List<string>> dataFilter = new List<List<string>>();
            foreach (List<string> item in dataList) {
                for(int i = 0; i < item.Count; i++)
                {
                    if (item[i].ToString().ToLower().Contains(value))
                    {
                        dataFilter.Add(item);
                        break;
                    }
                }
            }
            return dataFilter;
        }
    }
}
