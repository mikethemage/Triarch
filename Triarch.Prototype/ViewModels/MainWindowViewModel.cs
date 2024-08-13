using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public Action CloseAction { get; set; }

    internal void Exit()
    {
        CloseAction?.Invoke();
    }
}
