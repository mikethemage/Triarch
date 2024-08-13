﻿using Triarch.BusinessLogic.Models.Entities;

namespace Triarch.Prototype.ViewModels;

public class LevelableDataViewModel : ViewModelBase
{
    public LevelableDataViewModel(Levelable model, EntityEditorViewModel parent)
    {
        _parent = parent;
        _model = model;
    }

    private EntityEditorViewModel _parent;    

    private readonly Levelable _model;

    public Levelable Model { get { return _model; } }

    public int Level
    {
        get
        {
            return _model.Level;
        }
        set
        {
            _model.Level = value;
            OnPropertyChanged(nameof(Level));
            OnPropertyChanged(nameof(Points));
            OnPropertyChanged(nameof(Description));
            _parent.ChangesSaved = false;
        }
    }
    
    public int PointsPerLevel { get { return _model.PointsPerLevel; } }

    public int Points { get { return _model.Points; } }    

    public string Description { get { return _model.Description; } }

    public void RefreshProperties()
    {
        OnPropertyChanged(nameof(PointsPerLevel));
        OnPropertyChanged(nameof(Level));
        OnPropertyChanged(nameof(Points));
        OnPropertyChanged(nameof(Description));
    }
}