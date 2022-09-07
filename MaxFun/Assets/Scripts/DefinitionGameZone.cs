using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class DefinitionGameZone : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        ChangeSize();
    }
    
    public void ChangeSize()
    {
        var scale = _camera.ViewportToWorldPoint(new Vector2(1, 1));
        transform.localScale = new Vector3(scale.x * 2, scale.y * 2, 1);
    }
}

[CustomEditor(typeof(DefinitionGameZone))]
public class ChangeGizmos : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var border = (DefinitionGameZone)target;
        if(GUILayout.Button("ChangeSize"))
            border.ChangeSize();
    }
}
