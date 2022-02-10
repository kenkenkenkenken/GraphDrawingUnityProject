using UnityEngine;
using Zenject;

public class ViewInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Debug.Log("ViewInstaller");
        Container.Bind<GraphDrawingStartButtonView>().FromComponentInHierarchy().AsCached();
    }
}