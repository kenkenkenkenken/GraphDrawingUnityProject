using System;
using Zenject;

public class PresenterInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind(typeof(GraphDrawingStartButtonPresenter), typeof(IInitializable), typeof(IDisposable)).To<GraphDrawingStartButtonPresenter>().AsSingle();
        Container.Bind(typeof(GraphDrawingSpacePresenter), typeof(IInitializable), typeof(IDisposable)).To<GraphDrawingSpacePresenter>().AsSingle();
    }
}