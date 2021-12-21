using Cysharp.Threading.Tasks;
using System.Linq;
using UniRx;
using UnityEngine;

public class GraphDrawingStartButtonPresenter : MonoBehaviour
{
    [SerializeField] private GraphDrawingStartButtonView _graphDrawingStartButtonView;
    [SerializeField] private FileLoadingModel _fileLoadingModel;
    [SerializeField] private DataConversionForGraphModel _dataConversionForGraphModel;

    private void Start()
    {
        // view -> model
        _graphDrawingStartButtonView.OnClickDrawingStartButton
            .Subscribe(async _ =>
            {
                await _fileLoadingModel.LoadCsv();
                _dataConversionForGraphModel.DivideCsvDataByColumn(_fileLoadingModel.CsvDataList);

                //����̐����^���̃��X�g����ɂ���B
                _dataConversionForGraphModel.EyeMovementLeftHorizontalList.Clear();

                //����̐����^���̃��X�g�ɒǉ�����B
                _dataConversionForGraphModel.EyeMovementLeftHorizontalList.Add(
                    //���ځi���E�jzx�́A�O���t�o�̓f�[�^���擾����B
                    _dataConversionForGraphModel.GetGraphData(_dataConversionForGraphModel.EyeRayLeftDirZList.ToList(), _dataConversionForGraphModel.EyeRayLeftDirXList.ToList()));

                //����̐����^���̃��X�g����ɂ���B
                _dataConversionForGraphModel.eyeMovementLeftVerticalList.Clear();

                //����̐����^���̃��X�g�ɒǉ�����B
                _dataConversionForGraphModel.eyeMovementLeftVerticalList.Add(
                    //���ځi�㉺�jzy�́A�O���t�o�̓f�[�^���擾����B
                    _dataConversionForGraphModel.GetGraphData(_dataConversionForGraphModel.EyeRayLeftDirZList.ToList(), _dataConversionForGraphModel.EyeRayLeftDirYList.ToList()));
            })
            .AddTo(this.gameObject);

        // model -> view 
    }
}
