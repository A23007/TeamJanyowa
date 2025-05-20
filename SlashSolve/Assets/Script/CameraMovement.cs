//カメラの動き　増田

using UnityEngine;
using System.Collections.Generic;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float distance = 5.0f;
    public float height = 3.0f;
    public float rotationSpeed = 90.0f;
    public float smoothSpeed = 5.0f;
    public float initialRotation = 0f;
    public LayerMask obstructionMask;

    private float currentRotation;

    private Dictionary<Renderer, Material[]> originalMaterials = new Dictionary<Renderer, Material[]>();
    private List<Renderer> currentlyTransparent = new List<Renderer>();

    void Start()
    {
        currentRotation = initialRotation;
    }

    void LateUpdate()
    {
        HandleRotationInput();
        MoveCamera();
        HandleObstruction();
    }

    void HandleRotationInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            currentRotation -= rotationSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.RightArrow))
            currentRotation += rotationSpeed * Time.deltaTime;
    }

    void MoveCamera()
    {
        Quaternion rotation = Quaternion.Euler(0, currentRotation, 0);
        Vector3 offset = rotation * new Vector3(0, 0, -distance);
        Vector3 desiredPosition = target.position + Vector3.up * height + offset;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, smoothSpeed * Time.deltaTime);
    }

    void HandleObstruction()
    {
        // 透明化解除（元に戻す）
        foreach (var renderer in currentlyTransparent)
        {
            if (renderer != null && originalMaterials.ContainsKey(renderer))
            {
                renderer.materials = originalMaterials[renderer];
            }
        }

        currentlyTransparent.Clear();
        originalMaterials.Clear();

        Vector3 direction = (target.position + Vector3.up) - transform.position;
        float dist = direction.magnitude;

        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction.normalized, dist, obstructionMask);

        foreach (RaycastHit hit in hits)
        {
            GameObject hitObj = hit.collider.gameObject;

            // タグが "Untagged" のオブジェクトのみ透明化
            if (hitObj.tag == "Untagged")
            {
                Renderer rend = hitObj.GetComponent<Renderer>();
                if (rend != null && !currentlyTransparent.Contains(rend))
                {
                    originalMaterials[rend] = rend.materials;

                    Material[] transparentMats = new Material[rend.materials.Length];
                    for (int i = 0; i < rend.materials.Length; i++)
                    {
                        transparentMats[i] = CreateTransparentMaterial(rend.materials[i]);
                    }

                    rend.materials = transparentMats;
                    currentlyTransparent.Add(rend);

                    Debug.Log("Transparent applied to: " + rend.name);
                }
            }
        }
    }

    Material CreateTransparentMaterial(Material baseMat)
    {
        Material newMat = new Material(baseMat);

        // URP用 Litシェーダーを指定
        newMat.shader = Shader.Find("Universal Render Pipeline/Lit");

        // 透過設定
        newMat.SetFloat("_Surface", 1); // 0=Opaque, 1=Transparent

        // Blend設定（URPのLitの透過用）
        newMat.SetFloat("_Blend", (float)UnityEngine.Rendering.BlendMode.SrcAlpha);
        newMat.SetFloat("_SrcBlend", (float)UnityEngine.Rendering.BlendMode.SrcAlpha);
        newMat.SetFloat("_DstBlend", (float)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        newMat.SetFloat("_BlendOp", (float)UnityEngine.Rendering.BlendOp.Add);

        // ZWrite無効（深度書き込みOFF）
        newMat.SetInt("_ZWrite", 0);

        // 透過キーワード有効化
        newMat.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");

        // レンダーキューを透過用に
        newMat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

        // 透明度設定
        Color c = newMat.color;
        c.a = 0.3f; // 透明度30%
        newMat.color = c;

        return newMat;
    }
}
