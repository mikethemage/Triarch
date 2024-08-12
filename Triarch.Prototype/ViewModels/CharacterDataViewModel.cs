using Triarch.BusinessLogic.Models.Entities;

namespace Triarch.Prototype.ViewModels;

public class CharacterDataViewModel : ViewModelBase
{
    public CharacterDataViewModel(Character model)
    {
        _model = model;

    }

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
        }
    }

    public void RefreshProperties()
    {
        OnPropertyChanged(nameof(Soul));
        OnPropertyChanged(nameof(Health));
        OnPropertyChanged(nameof(Energy));
        OnPropertyChanged(nameof(ACV));
        OnPropertyChanged(nameof(DCV));
    }

    public int ACV
    {
        get { return _model.ACV; }
    }
    public int DCV { get { return _model.DCV; } }
    public int Health { get { return _model.Health; } }
    public int Energy { get { return _model.Energy; } }

    private Character _model;

}