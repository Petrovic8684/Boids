using UnityEngine;

public class BoidUIController : MonoBehaviour
{
    [SerializeField] private MonoBehaviour weightControllerBehaviour;

    private IBoidWeightController weightController;

    private void Awake()
    {
        weightController = weightControllerBehaviour as IBoidWeightController;
    }

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