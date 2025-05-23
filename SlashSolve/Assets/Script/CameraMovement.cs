//カメラの動き　増田

using UnityEngine;
using System.Collections.Generic;

public class CameraMovement : MonoBehaviour
{
    [Header("Camera Settings")]
    public Transform target;
    public float distance = 5.0f;
    public float height = 3.0f;
    public float rotationSpeed = 90.0f;
    public float smoothSpeed = 5.0f;
    public float initialRotation = 0f;
    public LayerMask obstructionMask;

    [Header("Transparency Area Settings")]
    [SerializeField] private float boxWidth = 2f;               // 横幅
    [SerializeField] private float boxHeight = 2f;              // 高さ
    [SerializeField] private float boxDepthMultiplier = 0.5f;   // プレイヤーまでの距離に対する奥行き倍率

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
        // 元に戻す
        foreach (var renderer in currentlyTransparent)
        {
            if (renderer != null && originalMaterials.ContainsKey(renderer))
            {
                renderer.materials = originalMaterials[renderer];
            }
        }

        currentlyTransparent.Clear();
        originalMaterials.Clear();

        Vector3 start = transform.position;
        Vector3 end = target.position + Vector3.up;
        Vector3 center = (start + end) / 2f;
        float distanceToTarget = Vector3.Distance(start, end);

        Vector3 boxSize = new Vector3(
            boxWidth,
            boxHeight,
            distanceToTarget * boxDepthMultiplier / 2f // OverlapBoxのsizeは「半分の長さ」
        );

        Quaternion rotation = Quaternion.LookRotation(end - start);

        Collider[] colliders = Physics.OverlapBox(center, boxSize, rotation, obstructionMask);

        foreach (Collider col in colliders)
        {
            GameObject hitObj = col.gameObject;

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

        newMat.shader = Shader.Find("Universal Render Pipeline/Lit");

        newMat.SetFloat("_Surface", 1);
        newMat.SetFloat("_Blend", (float)UnityEngine.Rendering.BlendMode.SrcAlpha);
        newMat.SetFloat("_SrcBlend", (float)UnityEngine.Rendering.BlendMode.SrcAlpha);
        newMat.SetFloat("_DstBlend", (float)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        newMat.SetFloat("_BlendOp", (float)UnityEngine.Rendering.BlendOp.Add);
        newMat.SetInt("_ZWrite", 0);
        newMat.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
        newMat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

        Color c = newMat.color;
        c.a = 0.3f;
        newMat.color = c;

        return newMat;
    }

    void OnDrawGizmosSelected()
    {
        if (target == null) return;

        Vector3 start = transform.position;
        Vector3 end = target.position + Vector3.up;
        Vector3 center = (start + end) / 2f;
        float distanceToTarget = Vector3.Distance(start, end);

        Vector3 boxSize = new Vector3(
            boxWidth,
            boxHeight,
            distanceToTarget * boxDepthMultiplier / 2f
        );

        Quaternion rotation = Quaternion.LookRotation(end - start);

        Gizmos.color = Color.green;
        Gizmos.matrix = Matrix4x4.TRS(center, rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, boxSize * 2);
    }
}
