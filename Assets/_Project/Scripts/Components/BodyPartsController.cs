using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BodyPartsController : MonoBehaviour
{
    public BodyPart currentBodyPart;

    protected BodyPartView _activeBodyPart;
    protected List<BodyPartView> _bodyPartViews;

    private void Awake()
    {
        _bodyPartViews = GetComponentsInChildren<BodyPartView>(true).ToList();
    }

    public void SetBodyPart(BodyPart bodyPart)
    {
        currentBodyPart = bodyPart;

        _activeBodyPart = _bodyPartViews.Find(x => x.bodyPart == currentBodyPart);

        HideAll();
        
        _activeBodyPart.gameObject.SetActive(true);
    }

    public void SetHives()
    {
        _activeBodyPart.SetHives();
    }

    public void SetItches()
    {
        _activeBodyPart.SetItches();
    }

    public void UpdateView(int optionIndex)
    {
        _activeBodyPart.UpdateView(optionIndex);
    }

    public void ResetView()
    {
        HideAll();
        
        // reset body part previous setup
        if (_activeBodyPart != null)
        {
            _activeBodyPart.ResetView();
        }
        
        _activeBodyPart = null;
    }

    private void HideAll()
    {
        for (int i = 0; i < _bodyPartViews.Count; i++)
        {
            _bodyPartViews[i].gameObject.SetActive(false);
        }
    }
}