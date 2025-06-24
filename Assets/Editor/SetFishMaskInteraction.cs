using UnityEngine;
using UnityEditor;

//This tools allows developers to easily change hte MaskInteraction of a selected GameObject and all it's children.
//Usage guide:
//Select the object
//Go to Tools -> Set Mask Interaction -> Visible Inside Mask/Visible Outside Mask/None (Disable Masking)
//Done! A message will be displayed in the console when the process is done

public class SetMaskInteraction : MonoBehaviour
{
    [MenuItem("Tools/Set Mask Interaction/Visible Inside Mask")]
    private static void SetVisibleInsideMask()
    {
        ApplyMaskInteraction(SpriteMaskInteraction.VisibleInsideMask, "Visible Inside Mask");
    }

    [MenuItem("Tools/Set Mask Interaction/Visible Outside Mask")]
    private static void SetVisibleOutsideMask()
    {
        ApplyMaskInteraction(SpriteMaskInteraction.VisibleOutsideMask, "Visible Outside Mask");
    }

    [MenuItem("Tools/Set Mask Interaction/None (Disable Masking)")]
    private static void SetNone()
    {
        ApplyMaskInteraction(SpriteMaskInteraction.None, "No Mask Interaction");
    }

    private static void ApplyMaskInteraction(SpriteMaskInteraction interaction, string label)
    {
        if (Selection.activeGameObject == null)
        {
            Debug.LogWarning("No GameObject selected.");
            return;
        }

        int count = 0;
        foreach (var renderer in Selection.activeGameObject.GetComponentsInChildren<SpriteRenderer>(true))
        {
            Undo.RecordObject(renderer, $"Set Mask Interaction to {interaction}");
            renderer.maskInteraction = interaction;
            EditorUtility.SetDirty(renderer);
            count++;
        }

        Debug.Log($"✅ Set {count} SpriteRenderers to {label}.");
    }
}