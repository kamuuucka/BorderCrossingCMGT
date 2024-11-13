using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ShadowEffect : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private Sprite shadow;
    [Range(0, 1)] [SerializeField] private float opacity = 1;
    
    private Material _material;
    private GameObject _shadow;
    private SpriteRenderer _shadowRenderer;

    private void Start()
    {
        CreateMaterial();
        SpawnShadow();
    }

    private void LateUpdate()
    {
        _shadow.transform.localPosition = offset;
    }

    /// <summary>
    /// Spawns the shadow on the set offset.
    /// </summary>
    private void SpawnShadow()
    {
        _shadow = new GameObject();
        _shadow.transform.SetParent(transform);
        _shadow.transform.localPosition = offset;
        _shadow.transform.localRotation = Quaternion.identity;

        var objectRenderer = GetComponent<SpriteRenderer>();
        _shadowRenderer = _shadow.AddComponent<SpriteRenderer>();
        _shadowRenderer.sprite = shadow;
        _shadowRenderer.material = _material;

        _shadowRenderer.sortingLayerName = objectRenderer.sortingLayerName;
        _shadowRenderer.sortingOrder = objectRenderer.sortingOrder - 1;
    }

    /// <summary>
    /// Creates the material with the transparency chosen by the user.
    /// </summary>
    private void CreateMaterial()
    {
        _material = new Material(Shader.Find("Standard"))
        {
            color = new Color(0f, 0f, 0f, opacity),
        };
        _material.SetFloat("_Mode", 3); // Set to Transparent mode
        _material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        _material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        _material.SetInt("_ZWrite", 0);
        _material.DisableKeyword("_ALPHATEST_ON");
        _material.EnableKeyword("_ALPHABLEND_ON");
        _material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        _material.renderQueue = 3000;
    }
}