using UnityEngine;

public class BoidUIAdjuster : MonoBehaviour
{
    [SerializeField] private MonoBehaviour weightAdjusterBehavior;

    private IBoidWeightAdjuster weightAdjuster;

    private void Awake()
    {
        weightAdjuster = weightAdjusterBehavior as IBoidWeightAdjuster;
    }

    public void OnAlignCheckboxChanged(bool isChecked)
    {
        weightAdjuster.SetAlignWeight(isChecked);
    }

    public void OnCohesionCheckboxChanged(bool isChecked)
    {
        weightAdjuster.SetCohesionWeight(isChecked);
    }

    public void OnSeparationCheckboxChanged(bool isChecked)
    {
        weightAdjuster.SetSeparationWeight(isChecked);
    }

}