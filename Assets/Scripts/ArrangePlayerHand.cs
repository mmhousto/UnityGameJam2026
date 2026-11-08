using UnityEngine;

public class ArrangePlayerHand : MonoBehaviour
{
    public float spacing = 2.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ArrangeChildren();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ArrangeChildren()
    {
        int childCount = transform.childCount;
        if (childCount == 0) return;

        // Calculate the total width of the layout to center the pivot
        float totalWidth = (childCount - 1) * spacing;
        float startX = -totalWidth / 2f;

        for (int i = 0; i < childCount; i++)
        {
            Transform child = transform.GetChild(i);

            // Calculate the target local position along the X axis
            Vector3 targetLocalPos = new Vector3(startX + (i * spacing), 0, 0);

            // Only update if the position changed to prevent constant dirtying of the scene
            if (child.localPosition != targetLocalPos)
            {
                child.localPosition = targetLocalPos;
            }
        }
    }
}
