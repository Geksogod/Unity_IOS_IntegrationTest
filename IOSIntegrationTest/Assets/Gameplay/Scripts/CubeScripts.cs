#if UNITY_IOS && !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

public class CubeScripts : MonoBehaviour
{
    public GameObject Cube;
    public Slider Slider;
    public Vector3 Rotate;
    public GUIStyle msgStyle;
    
    private Rect _rect;
    private string _msg;
    private bool _isRotate = true;
    
#if UNITY_IOS && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void UnityOnStart();
#endif
    private void Start()
    {
        _rect = new Rect(0, 0, Screen.width, Screen.height);
        _msg = "";
#if UNITY_IOS && !UNITY_EDITOR
        UnityOnStart();
#endif
    }

    private void Update()
    {
        if (!_isRotate)
        {
            return;
        }
        
        Cube.transform.Rotate(Rotate * (Slider.value * Time.deltaTime));
    }

    public void StartRotate()
    {
        _isRotate = true;
    }
    
    public void StopRotate()
    {
        _isRotate = false;
    }
    
    private void OnMessageReceived(string msg)
    {
        _msg = msg;
    }

    private void OnGUI()
    {
        GUI.Label(_rect, string.IsNullOrEmpty(_msg) ? "Waiting for message..." : _msg, msgStyle);
    }
}
