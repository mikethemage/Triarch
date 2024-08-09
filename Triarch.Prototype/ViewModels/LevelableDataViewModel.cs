using Triarch.BusinessLogic.Models.Entities;

namespace Triarch.Prototype.ViewModels;

public class LevelableDataViewModel
{
    public LevelableDataViewModel(Levelable model)
    {
        _model = model;
        Level = _model.Level;
    }

    private readonly Levelable _model;

    public int Level { get; set; }
    public int PointsPerLevel { get { return _model.PointsPerLevel; } }
    public int Points { get { return _model.Points; } }

    public string Description { get { return _model.Description; } }

}