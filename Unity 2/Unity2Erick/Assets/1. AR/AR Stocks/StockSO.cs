using UnityEngine;


[CreateAssetMenu(fileName = "Stock", menuName = "Create Stock")]
public class StockSO : ScriptableObject
{
    public Sprite icon;
    public Sprite graph;

    public float oldPrice;
    public float currentPrice;

    public float peRatio;
    public float dividendYield;
    public float eps;

}
