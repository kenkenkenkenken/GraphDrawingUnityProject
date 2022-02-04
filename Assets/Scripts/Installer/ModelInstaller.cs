using UnityEngine;
using Zenject;

public class ModelInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IFileLoadingModel>().To<FileLoadingModel>().AsSingle();
    }
}
