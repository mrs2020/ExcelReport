﻿using ExcelReport.Driver.CSV.Extends;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ExcelReport.Driver.CSV
{
    public class Workbook : IWorkbook, ICsvBuilder
    {
        private readonly Dictionary<string, Sheet> _sheetDic = new Dictionary<string, Sheet>();

        public ISheet this[string sheetName]
        {
            get
            {
                if (_sheetDic.ContainsKey(sheetName))
                {
                    return _sheetDic[sheetName];
                }
                var sheet = new Sheet(sheetName);
                _sheetDic[sheetName] = sheet;
                return sheet;
            }
        }

        public IEnumerator<ISheet> GetEnumerator()
        {
            return _sheetDic.Values.GetEnumerator();
        }

        public byte[] SaveToBuffer()
        {
            var builder = new StringBuilder();
            AppendTo(builder);
            return Encoding.GetEncoding("GB2312").GetBytes(builder.ToString());
            // return builder.ToString().ToBytesBy<UTF8Encoding>();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _sheetDic.Values.GetEnumerator();
        }

        public void AppendTo(StringBuilder builder)
        {
            foreach (Sheet sheet in this)
            {
                sheet.AppendTo(builder);
            }
        }
    }
}