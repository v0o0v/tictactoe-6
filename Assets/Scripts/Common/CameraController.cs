using UnityEngine;

namespace Tictactoe {

    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour {

        [SerializeField] private float widthUnit = 6f;
        Camera _camera;

        private void Start(){
            _camera = GetComponent<Camera>();
            _camera.orthographicSize = widthUnit / _camera.aspect / 2f;
        }

    }

}