using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
[ExecuteInEditMode]
public class TextureMaker : MonoBehaviour
{
    [SerializeField]private Camera camera;
    [SerializeField]private string TargetPrefabsPath;
    private string path;
    private RenderTexture rTex;

    public void MakeTexture()
    {
        string directory = Directory.GetCurrentDirectory();
        path = Path.Combine(directory, "Assets", "Art", "Textures");
        path = Path.Combine(path, $"Texture{Directory.GetFiles(path).Length}.png");
        
        rTex = camera.targetTexture;

        
        StartCoroutine(Making());

    }

    private IEnumerator Making()
    {
        GameObject[] obj = Resources.LoadAll<GameObject>(Path.Combine("", TargetPrefabsPath));

        foreach (var ob in obj)
        {
            
            yield return 
        }
    }

    private IEnumerator SaveTexture(GameObject)
    {
        Texture2D texture = new Texture2D(rTex.width, rTex.height);
        RenderTexture.active = rTex;
        texture.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        texture.Apply();

        // Texture2D를 바이트 배열로 변환
        byte[] bytes = texture.EncodeToPNG();

        // 파일로 저장
        System.IO.File.WriteAllBytes(path, bytes);

        // 메모리에서 해제
        RenderTexture.active = null;
        DestroyImmediate(texture);

        Debug.Log("Render Texture saved to: " + path);
    }
    
}
