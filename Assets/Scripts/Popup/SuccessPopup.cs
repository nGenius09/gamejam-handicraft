using UnityEngine;

public class SuccessPopup : FinishActionPopup
{
    [SerializeField] private GameObject[] results;

    public override void Show()
    {
        base.Show();

        var resultIndex = Random.Range(0, results.Length);

        for (int i = 0; i < results.Length; i++)
        {
            results[i].SetActive(i == resultIndex);
        }
    }
}