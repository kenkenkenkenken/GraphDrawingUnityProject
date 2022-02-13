using UnityEngine;
using Zenject;
using static GraphDrawingSpaceViewType;

public class ViewInstaller : MonoInstaller
{
    [SerializeField] private GameObject _eyeMovementLeftHorizontalDrawingSpace;
    [SerializeField] private GameObject _eyeMovementLeftVerticalDrawingSpace;
    [SerializeField] private GameObject _eyeMovementLeftHorizontalCanvas;
    [SerializeField] private GameObject _eyeMovementLeftVerticalCanvas;

    public override void InstallBindings()
    {
        Debug.Log("ViewInstaller");
        Container.Bind<GraphDrawingStartButtonView>().FromComponentInHierarchy().AsCached();
        Container.Bind<RectTransform>().WithId("EyeMovementLeftHorizontalCanvas").FromComponentOn(_eyeMovementLeftHorizontalCanvas).AsCached();
        Container.Bind<RectTransform>().WithId("EyeMovementLeftVerticalCanvas").FromComponentOn(_eyeMovementLeftVerticalCanvas).AsCached();
        Container.Bind<IGraph>().WithId(EyeMovementLeftHorizontal).To<GraphDrawingSpaceView>().FromNewComponentOn(_eyeMovementLeftHorizontalDrawingSpace).AsCached();
        Container.Bind<IGraph>().WithId(EyeMovementLeftVertical).To<GraphDrawingSpaceView>().FromNewComponentOn(_eyeMovementLeftVerticalDrawingSpace).AsCached();
    }
}