using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triarch.Prototype.ViewModels.SystemEditor;
internal class ListItemViewModel<T> : ViewModelBase
{    
    private string _displayText = null!;

    public required string DisplayText
    {
        get
        {
            return _displayText;
        }
        set
        {
            _displayText = value;
            OnPropertyChanged(nameof(DisplayText));
        }
    }

    private bool _isSelected;

    public bool IsSelected
    {
        get
        {
            return _isSelected;
        }
        set
        {
            _isSelected = value;
            OnPropertyChanged(nameof(IsSelected));
        }
    }

    public required T Model { get; set; }
}
