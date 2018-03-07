namespace Solab.EntityFramework.Core.TemporalTables
{
    public class TemporalTableSettings
    {
        public TemporalTableSettings(string table, string startPeriodColumnName, string endPeriodColumnName, bool createPeriodColumns, string schema)
        {
            Table = table;
            StartPeriodColumnName = startPeriodColumnName;
            EndPeriodColumnName = endPeriodColumnName;
            CreatePeriodColumns = createPeriodColumns;
            Schema = schema;
        }

        public bool CreatePeriodColumns { get; set; }

        private string _schema;
        public string Schema
        {
            get
            {
                return _schema;
            }

            private set
            {
                _schema = WrapInSquareBrackets(value);
            }
        }

        private string _table;
        public string Table
        {
            get
            {
                return $"{Schema}.{_table}";
            }
            private set
            {
                _table = WrapInSquareBrackets(value);
            }
        }

        public string HistoryTable
        {
            get
            {
                return Table.TrimEnd(']') + "_History]";
            }
        }

        private string _startPeriodColumnName;
        public string StartPeriodColumnName
        {
            get
            {
                return _startPeriodColumnName;
            }
            private set
            {
                _startPeriodColumnName = WrapInSquareBrackets(value);
            }
        }

        private string _endPeriodColumnName;
        public string EndPeriodColumnName
        {
            get
            {
                return _endPeriodColumnName;
            }

            private set
            {
                _endPeriodColumnName = WrapInSquareBrackets(value);
            }
        }

        private string WrapInSquareBrackets(string identifier)
        {
            var parts = identifier.Split('.');

            for (var i = 0; i < parts.Length; i++)
            {
                if (!parts[i].StartsWith("["))
                {
                    parts[i] = "[" + parts[i];
                }

                if (!parts[i].EndsWith("]"))
                {
                    parts[i] = parts[i] + "]";
                }
            }

            return string.Join(".", parts);
        }
    }
}
