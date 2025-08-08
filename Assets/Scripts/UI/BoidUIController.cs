using UnityEngine;

public class BoidUIController : MonoBehaviour
{
    public BoidWeightController weightController;

    public void OnAlignCheckboxChanged(bool isChecked)
    {
        weightController.SetAlignWeight(isChecked);
    }

    public void OnCohesionCheckboxChanged(bool isChecked)
    {
        weightController.SetCohesionWeight(isChecked);
    }

    public void OnSeparationCheckboxChanged(bool isChecked)
    {
        weightController.SetSeparationWeight(isChecked);
    }

}