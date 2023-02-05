using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ArrowController : MonoBehaviour
{
    public Transform player; // reference to the player's transform component
    public Transform objective; // reference to the objective's transform component

    Image image;

    private RectTransform arrowTransform; // reference to the arrow's rect transform component

    IEnumerator Start()
    {
        arrowTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        BirdRooting.OnGetItem += ShowImage;
        BirdRooting.OnDropItem += HideImage;
        image.enabled = false;
        yield return new WaitForSeconds(1f);
        player = FindObjectOfType<BirdRooting>().transform;
        objective = FindObjectOfType<NestContainer>(false).transform;
    }

    private void OnDestroy()
    {
        BirdRooting.OnGetItem -= ShowImage;
        BirdRooting.OnDropItem -= HideImage;
    }

    void Update()
    {
        if (image.enabled == false)
            return;
        // Calculate the direction between player and objective
        Vector2 direction = (objective.position - player.position).normalized;

        // Rotate the arrow in the direction of the objective
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrowTransform.rotation = Quaternion.Euler(0, 0, angle - 90);

        //// Calculate the side of the screen that the objective is on
        //Vector2 screenPoint = Camera.main.WorldToViewportPoint(objective.position);
        //if (screenPoint.x < 0.5f)
        //{
        //    // Objective is on the left side of the screen
        //    arrowTransform.anchorMin = new Vector2(0, 0.5f);
        //    arrowTransform.anchorMax = new Vector2(0, 0.5f);
        //}
        //else
        //{
        //    // Objective is on the right side of the screen
        //    arrowTransform.anchorMin = new Vector2(1, 0.5f);
        //    arrowTransform.anchorMax = new Vector2(1, 0.5f);
        //}
    }

    void ShowImage()
    {
        image.enabled = true;
    }

    void HideImage()
    {
        image.enabled = false;
    }
}
