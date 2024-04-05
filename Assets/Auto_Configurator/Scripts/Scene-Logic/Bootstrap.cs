using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private FreeLook _freeLookCameraInput;
    [SerializeField] private FreeLookZoom _freeLookZoom;
    [SerializeField] private POV _POVCameraInput;
    [SerializeField] private CameraStateChangerObserver _changerObserver;
    [SerializeField] private CameraStateController _cameraStateController;
    [SerializeField] private CarSubject _carSubject;
    [SerializeField] private OutlineEnablerObserver _outlineEnablerObserver;
    [SerializeField] private UserMovement _userMovement;
    [SerializeField] private SurfaceToMove _surfaceToMove;
    [SerializeField] private MousePositionListener _mousePositionListener;
    [SerializeField] private UIManager _uIManager;
    [SerializeField] private UIAnimation _uIAnimation;
    [SerializeField] private Configurator _configurator;
    [SerializeField] private MainInteractableUIElements _mainInteractableUIElements;
    private UserObserver _userObserver;

    private void Awake()
    {
        InitializeComponents(); // tyt
    }

    private void InitializeComponents()
    {
        _freeLookCameraInput.Initialize();
        _freeLookZoom.Initialize();
        _POVCameraInput.Initialize();

        _cameraStateController.Initialize();
        _changerObserver.Initialize(_carSubject);
        _outlineEnablerObserver.Initialize();
        _userObserver = new UserObserver(_carSubject);

        _surfaceToMove.Initialize(_mousePositionListener);
        _userMovement.Initialize(_userObserver, _surfaceToMove, _mousePositionListener);
        _uIManager.Initialize(_carSubject, _uIAnimation);
        _configurator.Initialize(_carSubject);
        _mainInteractableUIElements.Initialize(_configurator); // TUT
    }
}
