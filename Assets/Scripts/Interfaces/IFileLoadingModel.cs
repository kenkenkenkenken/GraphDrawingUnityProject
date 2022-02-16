using Cysharp.Threading.Tasks;
using System.Collections.ObjectModel;

public interface IFileLoadingModel
{
    /// <summary>
    /// CSVのデータのリストのプロパティ
    /// </summary>
    /// 
    public ReadOnlyCollection<string[]> CsvDataList { get; }

    /// <summary>
    /// CSVファイルを読んでリストに追加する
    /// </summary>
    public UniTask LoadCsv();
}
