using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UniRx;

public interface IDataConversionForGraphModel
{
    public void SetDataForCsvColumn(List<string[]> csvDataList);

    public IObservable<Unit> OnSetCsvDataForEachColumn { get; }
    public ReadOnlyCollection<float> EyeRayLeftDirZList { get; }

    public ReadOnlyCollection<float> EyeRayLeftDirYList { get; }
    public ReadOnlyCollection<float> EyeRayLeftDirXList { get; }

    public ReadOnlyCollection<float> ApplicationTimeList { get; }

    public List<float> GetAngleList(List<float> coordinateList1, List<float> coordinateList2);
}
