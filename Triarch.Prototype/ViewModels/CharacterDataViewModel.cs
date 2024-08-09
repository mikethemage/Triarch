using Triarch.BusinessLogic.Models.Entities;

namespace Triarch.Prototype.ViewModels;

public class CharacterDataViewModel : ViewModelBase
{
    public CharacterDataViewModel(Character model)
    {
        _model = model;
        Body = _model.Body;
        Mind = _model.Mind;
        Soul = _model.Soul;
    }

    public int Body { get; set; }
    public int Mind { get; set; }
    public int Soul { get; set; }

    public int ACV { get; set; }
    public int DCV { get; set; }
    public int Health { get; set; }
    public int Energy { get; set; }

    private Character _model;

}