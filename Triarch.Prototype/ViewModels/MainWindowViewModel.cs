using Triarch.Prototype.ViewModels.EntityEditor;
using Triarch.Prototype.ViewModels.MainMenu;

namespace Triarch.Prototype.ViewModels;
public class MainWindowViewModel : ViewModelBase
{
    private IPageViewModel _currentPage;

    public IPageViewModel CurrentPage
    {
        get
        {
            return _currentPage;
        }
        set
        {
            _currentPage = value;
            OnPropertyChanged(nameof(CurrentPage));
        }
    }

    public MainWindowViewModel()
    {
        _currentPage = new MainMenuViewModel { Parent = this };
    }

    public Action? CloseAction { get; set; }

    internal void Exit()
    {
        CloseAction?.Invoke();
    }
}
