using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Utilities;

namespace Solab.EntityFramework.DateTable
{
    public class PopulateDateTable : MigrationOperation, IMigrationAction
    {
        public override bool IsDestructiveChange => false;

        public Func<IndentedTextWriter> WriterDelegate { get; private set; }

        public Action<IndentedTextWriter> StatementDelegate { get; private set; }

        public readonly string Table;

        public readonly DateTime Start;

        public readonly DateTime End;

        public PopulateDateTable(string table, DateTime start, DateTime end) : base(null)
        {
            Table = table;
            Start = start;
            End = end;
        }

        public void Invoke(Func<IndentedTextWriter> writerDelegate, Action<IndentedTextWriter, bool> statementDelegate)
        {
            WriterDelegate = writerDelegate;
            StatementDelegate = x => statementDelegate.Invoke(x, false);
            EmptyTable();
            PopulateDateDimension();
        }

        private void PopulateDateDimension()
        {
            using (var writer = WriterDelegate.Invoke())
            {
                foreach (var dimension in GenerateDateDimensions())
                {
                    string sql =
                        $@"INSERT INTO {Table} 
                        (
                            [Date], 
                            [Year],
                            [Month],
                            [Day],
                            [DayOfWeek],
                            [DayOfYear],
                            [DayOfWeekName],
                            [MonthName],
                            [Week],
                            [Suffix]
                        ) VALUES (
                            '{dimension.Date.ToString("yyyy-MM-dd")}',
                            {dimension.Year},
                            {dimension.Month},
                            {dimension.Day},
                            {dimension.DayOfWeek},
                            {dimension.DayOfYear},
                            '{dimension.DayOfWeekName}',
                            '{dimension.MonthName}',
                            {dimension.Week},
                            '{dimension.Suffix}'
                        )";

                    writer.WriteLine(sql, Table, dimension.Date);
                }

                StatementDelegate.Invoke(writer);
            }
        }

        private void EmptyTable()
        {
            using (var writer = WriterDelegate.Invoke())
            {
                writer.WriteLine($"DELETE FROM {Table}");
                StatementDelegate.Invoke(writer);
            }
        }

        public IEnumerable<DateDimension> GenerateDateDimensions()
        {
            var dates = new List<DateDimension>();
            var timespan = (End - Start).Days;
            var start = Start.Date;

            for (var i = 0; i <= timespan; i++)
            {
                var current = start.Date.AddDays(i);
                dates.Add(new DateDimension(current));
            }

            return dates;
        }
    }
}
