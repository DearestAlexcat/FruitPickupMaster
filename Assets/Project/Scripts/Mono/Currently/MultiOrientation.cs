using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class DataOrientation
{
    public string name;
    public Vector3 positon;
    public Vector3 rotation;
}

public class MultiOrientation : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] List<DataOrientation> datas;

    CancellationTokenSource cts = new();

    private void OnDestroy()
    {
        cts.Cancel();
    }

    DataOrientation Get(string name)
    {
        for (int i = 0; i < datas.Count; i++)
        {
            if (datas[i].name == name)
            {
                return datas[i]; 
            }
        }

        return null;
    }

    async public UniTaskVoid SetOrientationTo(string to)
    {
        DataOrientation t = Get(to);

        float k = 0f;

        Vector3 startPosition = transform.localPosition;
        Quaternion startRotation = transform.localRotation;
        Quaternion endRotation = Quaternion.Euler(t.rotation);

        while (k < 1f)
        {
            if (cts.Token.IsCancellationRequested) return;

            k += Time.deltaTime / duration;
            transform.localPosition = Vector3.Lerp(startPosition, t.positon, k);
            transform.localRotation = Quaternion.Lerp(startRotation, endRotation, k);
            await UniTask.NextFrame();
        }
    }

    async public UniTaskVoid RotateTo(string to)
    {
        DataOrientation t = Get(to);

        float k = 0f;

        Quaternion startRotation = transform.localRotation;
        Quaternion endRotation = Quaternion.Euler(t.rotation);

        while (k < 1f)
        {
            if (cts.Token.IsCancellationRequested) return;

            k += Time.deltaTime / duration;
            transform.localRotation = Quaternion.Lerp(startRotation, endRotation, k);
            await UniTask.NextFrame();
        }
    }

#if UNITY_EDITOR
    [SerializeField] string keyPosition;
    public void MoveTo()
    {
        DataOrientation t = Get(keyPosition);
        transform.localPosition = t.positon;
        transform.localRotation = Quaternion.Euler(t.rotation);
    }
#endif

}

[CustomEditor(typeof(MultiOrientation))]
public class MyComponentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var myComponent = (MultiOrientation)target;

        if (GUILayout.Button("Set position"))
        {
            myComponent.MoveTo();
        }
    }
}
