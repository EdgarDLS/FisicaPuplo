using UnityEngine;

[ExecuteInEditMode]
public class DepthTexture : MonoBehaviour
{

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        cam.depthTextureMode = DepthTextureMode.Depth;
    }

}