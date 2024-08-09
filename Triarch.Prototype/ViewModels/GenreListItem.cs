using Triarch.BusinessLogic.Models.Definitions;

namespace Triarch.Prototype.ViewModels;

public class GenreListItem
{
    public string DisplayText {  get; set; } = string.Empty;
    public bool IsSelected { get; set; } = false;
    public Genre Model { get; set; } = null!;
}