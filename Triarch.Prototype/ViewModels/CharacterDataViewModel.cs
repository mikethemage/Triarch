using Triarch.BusinessLogic.Models.Entities;

namespace Triarch.Prototype.ViewModels;

public class CharacterDataViewModel : ViewModelBase
{
    public CharacterDataViewModel(Character model, EntityEditorViewModel parent)
    {
        _model = model;
        _parent = parent;
    }

    private Character _model;

    private EntityEditorViewModel _parent;

    public int Body
    {
        get
        {
            return _model.Body;
        }
        set
        {
            _model.Body = value;
            OnPropertyChanged(nameof(Body));
            OnPropertyChanged(nameof(Health));
            OnPropertyChanged(nameof(ACV));
            OnPropertyChanged(nameof(DCV));
            _parent.ChangesSaved = false;
        }
    }

    public int Mind
    {
        get
        {
            return _model.Mind;
        }
        set
        {
            _model.Mind = value;
            OnPropertyChanged(nameof(Mind));
            OnPropertyChanged(nameof(Energy));
            OnPropertyChanged(nameof(ACV));
            OnPropertyChanged(nameof(DCV));
            _parent.ChangesSaved = false;
        }
    }

    public int Soul
    {
        get
        {
            return _model.Soul;
        }
        set
        {
            _model.Soul = value;
            OnPropertyChanged(nameof(Soul));
            OnPropertyChanged(nameof(Health));
            OnPropertyChanged(nameof(Energy));
            OnPropertyChanged(nameof(ACV));
            OnPropertyChanged(nameof(DCV));
            _parent.ChangesSaved = false;
        }
    }    

    public int ACV
    {
        get { return _model.ACV; }
    }

    public int DCV { get { return _model.DCV; } }

    public int Health { get { return _model.Health; } }

    public int Energy { get { return _model.Energy; } }    

    public void RefreshProperties()
    {
        OnPropertyChanged(nameof(Soul));
        OnPropertyChanged(nameof(Health));
        OnPropertyChanged(nameof(Energy));
        OnPropertyChanged(nameof(ACV));
        OnPropertyChanged(nameof(DCV));
    }
}