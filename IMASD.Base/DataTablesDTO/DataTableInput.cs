using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMASD.Base.DataTablesDTO
{
    public class DataTableInput
    {
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public List<Column> columns { get; set; }
        public Search search { get; set; }
        public List<Order> order { get; set; }
        public Dictionary<String,Column> getColumnsAsDictionary()
        {
            Dictionary<String, Column> map = new Dictionary<string, Column>();
            foreach (var item in columns)
            {
                map.Add(item.data,item);
            }
            return map;
        }
        public Column getColumn(string columName)
        {
            if (columName==null)
            {
                return null;

            }
            foreach (var item in columns)
            {
                if (columName.Equals(item.data)){
                    return item;
                }
            }
            return null;
        }
    }
}