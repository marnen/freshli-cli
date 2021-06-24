using Corgibytes.Freshli.Lib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Xunit;

namespace Corgibytes.Freshli.Cli.Test
{
    public class OutputFormatterTest
    {
        private static IEnumerable<(DateTime Date, double Value, bool UpgradeAvailable, bool Skipped)> DatesAndValues
        {
            get
            {
                return new List<(DateTime Date, double Value, bool UpgradeAvailable, bool Skipped)>
                {
                    (new DateTime(2010, 01, 01), 1.1010, false, false),
                    (new DateTime(2010, 02, 01), 2.2020, false, false),
                    (new DateTime(2010, 03, 01), 3.3030, false, false),
                    (new DateTime(2010, 04, 01), 4.4040, false, false),
                    (new DateTime(2010, 05, 01), 5.5050, false, false),
                    (new DateTime(2010, 06, 01), 6.6060, false, false),
                    (new DateTime(2010, 07, 01), 7.7070, false, false),
                    (new DateTime(2010, 08, 01), 8.8080, false, false),
                    (new DateTime(2010, 09, 01), 9.9090, false, false),
                    (new DateTime(2010, 10, 01), 10.0101, false, false),
                    (new DateTime(2010, 11, 01), 11.1111, true, false),
                    (new DateTime(2010, 12, 01), 12.2121, true, false),
                    (new DateTime(2011, 01, 01), 13.3333, false, true)
                };
            }
        }

        private static IList<MetricsResult> CreateResults()
        {
            IList<MetricsResult> results = new List<MetricsResult>();
            foreach (var dateAndValue in DatesAndValues)
            {
                var result = new LibYearResult
                {
                    new LibYearPackageResult
                    (
                        "test_package",
                        "1.0",
                        dateAndValue.Date,
                        "2.0",
                        DateTime.Today,
                        dateAndValue.Value,
                        dateAndValue.UpgradeAvailable,
                        dateAndValue.Skipped
                    )
                };
                results.Add(new MetricsResult(dateAndValue.Date, "N/A", result));
            }

            return results;
        }

        private static string EnglishHeader = "Date\tLibYear\tUpgradesAvailable\tSkipped";
        private static string SpanishHeader = "Fecha\tAñoLib\tActualizaciónesDisponibles\tOmitida";

        private static string BoolToString(bool b)
        {
            return Convert.ToInt32(b).ToString();
        }

        private static string ExpectedDatesAndValues(string header)
        {
            var expected = new StringWriter();
            expected.WriteLine(header);

            foreach (var dv in DatesAndValues)
            {
                string date = dv.Date.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern);
                double value = dv.Skipped ? 0 : dv.Value;  // Skipped results should result in a zero value displayed.
                string upgrade = BoolToString(dv.UpgradeAvailable);
                string skipped = BoolToString(dv.Skipped);

                expected.WriteLine($"{date}\t{value:F4}\t{upgrade}\t{skipped}");
            }

            return expected.ToString();
        }

        [Fact]
        public void EnglishLanguage()
        {
            CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en");

            var results = CreateResults();

            var actual = new StringWriter();
            var formatter = new OutputFormatter(actual);
            formatter.Write(results);

            Assert.Equal(ExpectedDatesAndValues(EnglishHeader), actual.ToString());
        }

        [Fact]
        public void InvariantLanguage()
        {
            CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;

            var results = CreateResults();

            var actual = new StringWriter();
            var formatter = new OutputFormatter(actual);
            formatter.Write(results);

            Assert.Equal(ExpectedDatesAndValues(EnglishHeader), actual.ToString());
        }

        [Fact]
        public void SpanishLanguage()
        {
            CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("es");

            var results = CreateResults();

            var actual = new StringWriter();
            var formatter = new OutputFormatter(actual);
            formatter.Write(results);

            Assert.Equal(ExpectedDatesAndValues(SpanishHeader), actual.ToString());
        }

        [Fact]
        public void UnsupportedLanguage()
        {
            CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("de"); //This will have to change if we ever support German

            var results = CreateResults();

            var actual = new StringWriter();
            var formatter = new OutputFormatter(actual);
            formatter.Write(results);

            Assert.Equal(ExpectedDatesAndValues(EnglishHeader), actual.ToString());
        }
    }


}
