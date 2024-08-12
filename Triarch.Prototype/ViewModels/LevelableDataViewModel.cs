using Triarch.BusinessLogic.Models.Definitions;
using Triarch.BusinessLogic.Models.Entities;

namespace Triarch.Prototype.ViewModels;

public class LevelableDataViewModel : ViewModelBase
{
    public LevelableDataViewModel(Levelable model)
    {
        _model = model;
        Level = _model.Level;
    }

    private readonly Levelable _model;
    

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
        }
    }

    public void RefreshProperties()
    {
        OnPropertyChanged(nameof(PointsPerLevel));
        OnPropertyChanged(nameof(Level));
        OnPropertyChanged(nameof(Points));
        OnPropertyChanged(nameof(Description));
    }
    public int PointsPerLevel { get { return _model.PointsPerLevel; } }
    public int Points { get { return _model.Points; } }    

    public Levelable Model { get { return _model; } }

    public string Description { get { return _model.Description; } }

}