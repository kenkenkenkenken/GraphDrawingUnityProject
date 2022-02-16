using Zenject;

public class PresenterInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind(typeof(GraphDrawingStartButtonPresenter), typeof(IInitializable)).To<GraphDrawingStartButtonPresenter>().AsSingle();
        Container.Bind(typeof(GraphDrawingSpacePresenter), typeof(IInitializable)).To<GraphDrawingSpacePresenter>().AsSingle();
    }
}