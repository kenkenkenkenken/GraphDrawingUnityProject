using Cysharp.Threading.Tasks;
using System.Collections.ObjectModel;

public interface IFileLoadingModel
{
    UniTask LoadCsv();

    ReadOnlyCollection<string[]> CsvDataList { get; }
}
