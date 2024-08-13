using Triarch.BusinessLogic.Models.Definitions;
using Triarch.BusinessLogic.Models.Entities;

namespace Triarch.Prototype.ViewModels;

public class LevelableDataViewModel : ViewModelBase
{
    private EntityEditorViewModel _parent;
    public LevelableDataViewModel(Levelable model, EntityEditorViewModel parent)
    {
        _parent = parent;
        _model = model;
       
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
            _parent.ChangesSaved = false;
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