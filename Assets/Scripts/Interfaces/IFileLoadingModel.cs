using Cysharp.Threading.Tasks;
using System.Collections.ObjectModel;

public interface IFileLoadingModel
{
    public UniTask LoadCsv();

    public ReadOnlyCollection<string[]> CsvDataList { get; }
}
