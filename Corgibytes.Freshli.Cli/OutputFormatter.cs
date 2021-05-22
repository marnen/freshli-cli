using System.Collections.Generic;
using System.IO;
using Corgibytes.Freshli.Lib;
using System.Linq;
using ConsoleTables;

namespace Corgibytes.Freshli.Cli {
  public class OutputFormatter {
    private readonly TextWriter _writer;

    public OutputFormatter(TextWriter writer) {
      _writer = writer;
    }

    public void Write(string filename, IList<MetricsResult> results) {
      _writer.WriteLine(
        $"Project filename: {filename}"
      );

      var currentResult = results.Last();
      _writer.WriteLine(
        $"LibYear: {currentResult.LibYear.Total}"
      );

      if (!currentResult.LibYear.Any()) { return; }
      var table = new ConsoleTable(
        new ConsoleTableOptions
            {
                Columns = new[] { "Package", "LibYear", "Current", "Latest" },
                EnableCount = false
            }      
      );

      foreach (var packageResult in currentResult.LibYear) {
        table.AddRow(
          packageResult.Name,
          packageResult.Value,
          packageResult.Version,
          packageResult.LatestVersion
        );
      }

      table.Write();
      _writer.WriteLine("");
    }
  }
}
