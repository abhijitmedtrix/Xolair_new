using System.Collections.Generic;
using UnityEngine;

public class BodyPartView : MonoBehaviour
{
    [Header("Hives objects")] [SerializeField]
    protected GameObject _hivesContainer;

    [SerializeField] protected GameObject[] _hivesOption0;
    [SerializeField] protected GameObject[] _hivesOption1;
    [SerializeField] protected GameObject[] _hivesOption2;
    [SerializeField] protected GameObject[] _hivesOption3;

    protected List<GameObject[]> _hives;

    [Header("Itches objects")] [SerializeField]
    protected GameObject _itchesContainer;

    [SerializeField] protected GameObject[] _itchesOption0;
    [SerializeField] protected GameObject[] _itchesOption1;
    [SerializeField] protected GameObject[] _itchesOption2;
    [SerializeField] protected GameObject[] _itchesOption3;
    protected List<GameObject[]> _itches;

    protected List<GameObject[]> _activeObjects;

    public BodyPart bodyPart;

    private void Awake()
    {
        _hives = new List<GameObject[]>
        {
            _hivesOption0,
            _hivesOption1,
            _hivesOption2,
            _hivesOption3
        };

        _itches = new List<GameObject[]>
        {
            _itchesOption0,
            _itchesOption1,
            _itchesOption2,
            _itchesOption3
        };
    }

    public void UpdateView(int optionIndex)
    {
        HideAll();

        for (int i = 0; i < _activeObjects[optionIndex].Length; i++)
        {
            _activeObjects[optionIndex][i].SetActive(true);
        }
    }

    public void SetHives()
    {
        // HideAll();
        _itchesContainer.SetActive(false);
        _hivesContainer.SetActive(true);
        _activeObjects = _hives;
    }

    public void SetItches()
    {
        // HideAll();
        
        // keep hives visible and add itches to previous setup
        _itchesContainer.SetActive(true);
        
        _activeObjects = _itches;
    }

    public void ResetView()
    {
        HideAll();
        
        for (int i = 0; i < _hives.Count; i++)
        {
            for (int j = 0; j < _hives[i].Length; j++)
            {
                _hives[i][j].SetActive(false);
            }
        }

        for (int i = 0; i < _itches.Count; i++)
        {
            for (int j = 0; j < _itches[i].Length; j++)
            {
                _itches[i][j].SetActive(false);
            }
        }
        
        _activeObjects = null;
    }

    private void HideAll()
    {
        if (_activeObjects == null) return;

        for (int i = 0; i < _activeObjects.Count; i++)
        {
            for (int j = 0; j < _activeObjects[i].Length; j++)
            {
                _activeObjects[i][j].SetActive(false);
            }
        }
    }
}