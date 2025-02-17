using UnityEngine;

public class Path : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private GameObject tower;
    private Color startColor;

    private void OnMouseEnter() {
        sr.color = hoverColor;
    }

    private void OnMouseExit() {
        sr.color = startColor;
    }

    private void OnMouseDown() {
        if (tower != null || GameManager.main.money < 10) return;

        GameObject towerToBuild = BuildManager.main.GetSelectedTower();
        tower = Instantiate (towerToBuild, transform.position, Quaternion.identity);
        GameManager.main.IncScore(-10);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startColor = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
