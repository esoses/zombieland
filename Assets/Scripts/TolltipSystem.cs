using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TolltipSystem : MonoBehaviour
{
    private static TolltipSystem current;
    public Tooltip tooltip;

    public void Awake()
    {
        current = this;
    }
    public static void Show(string content, string header)
    {
        current.tooltip.SetText(content, header);
        current.tooltip.gameObject.SetActive(true);
    }
    public static void Hide()
    {
        current.tooltip.gameObject.SetActive(false);
    }
}
