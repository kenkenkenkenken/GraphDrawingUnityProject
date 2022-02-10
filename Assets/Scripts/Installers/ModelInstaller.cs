using UnityEngine;
using Zenject;

public class ModelInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Debug.Log("ModelInstaller");
        Container.Bind<IFileLoadingModel>().To<FileLoadingModel>().AsSingle();
        Container.Bind<IDataConversionForGraphModel>().To<DataConversionForGraphModel>().AsSingle();
    }
}
