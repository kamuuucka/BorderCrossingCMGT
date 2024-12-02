using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStartOffset : MonoBehaviour
{
    public Animator animator;
    public float startOffset = 0.5f; // Offset in normalized time (0 to 1)

    void Start()
    {
        if (animator != null)
        {
            animator.Play("BG_cloud_move", 0, startOffset); // Play animation with offset
        }
    }
}
