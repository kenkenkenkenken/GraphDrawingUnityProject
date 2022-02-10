using UnityEngine;
using Zenject;

public class PresenterInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Debug.Log("PresenterInstaller");
        Container.Bind(typeof(GraphDrawingStartButtonPresenter), typeof(IInitializable)).To<GraphDrawingStartButtonPresenter>().AsSingle();
        Container.Bind(typeof(GraphDrawingSpacePresenter), typeof(IInitializable)).To<GraphDrawingSpacePresenter>().AsSingle();
    }
}