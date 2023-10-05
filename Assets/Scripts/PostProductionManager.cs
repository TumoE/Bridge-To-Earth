using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProductionManager : MonoBehaviour
{
    [SerializeField] private PostProcessVolume _volium;
    private Vignette _vignette;

    private void Start()
    {
        _volium.profile.TryGetSettings<Vignette>(out _vignette);
    }

    public void ChangeColorToRed(bool makeRed)
    {
        if (makeRed)
        {
            _vignette.color.value = Color.red;
        }
        else
        {
            _vignette.color.value = Color.black;
        }
    }
}
