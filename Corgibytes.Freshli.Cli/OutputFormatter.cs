using Freshli;
using System.Collections.Generic;
using System.IO;
using Corgibytes.Freshli.Lib;

namespace Corgibytes.Freshli.Cli {
  public class OutputFormatter {
    private readonly TextWriter _writer;

    public OutputFormatter(TextWriter writer) {
      _writer = writer;
    }

    public void Write(string filename, IList<MetricsResult> results) {
      _writer.WriteLine(
        filename
      );
      _writer.WriteLine(
        "Date\t" +
        "LibYear\t" +
        "UpgradesAvailable\t" +
        "Skipped"
      );

      foreach (var resultSet in results) {
        _writer.WriteLine(
          $"{resultSet.Date.ToString("yyyy/MM/dd")}\t" +
          $"{resultSet.LibYear.Total:F4}\t" +
          $"{resultSet.LibYear.UpgradesAvailable}\t" +
          $"{resultSet.LibYear.Skipped}"
        );
      }
    }
  }
}
