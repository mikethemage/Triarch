using Microsoft.Win32;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using Triarch.BusinessLogic.Models.Definitions;
using Triarch.BusinessLogic.Services;
using Triarch.Dtos.Definitions;
using Triarch.Prototype.ViewModels.MainMenu;

namespace Triarch.Prototype.ViewModels.SystemEditor;

public class SystemEditorViewModel : ViewModelBase, IPageViewModel
{
    private readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

    public SystemEditorViewModel(RPGSystem rPGSystem, string filePath = "")
    {
        _rPGSystem = rPGSystem;    
        _filePath= filePath;        
        BackCommand = new RelayCommand(Back, CanBack);
        SaveCommand = new RelayCommand(Save, CanSave);
        SaveAsCommand = new RelayCommand(SaveAs, CanSaveAs);        
    }    

    public required MainWindowViewModel Parent { get; set; }

    public bool ChangesSaved { get; set; } = false;

    public RelayCommand? SaveCommand { get; set; }
    public RelayCommand? SaveAsCommand { get; set; }
    public RelayCommand? BackCommand { get; set; }    

    public string FileName
    {
        get
        {
            if (_filePath == "")
            {
                return "";
            }
            else
            {
                return Path.GetFileName(_filePath);
            }
        }
    }

    private readonly RPGSystem _rPGSystem;
    private string _filePath;   

    public void SaveAs()
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog
        {
            RestoreDirectory = false,
            Filter = "Triarch" + " Files(*.json)|*.json|All Files (*.*)|*.*",
            FilterIndex = 1
        };

        if (saveFileDialog.ShowDialog() ?? false)
        {
            WriteSystemToFile(saveFileDialog.FileName);
            _filePath = saveFileDialog.FileName;
        }
    }

    public void Save()
    {
        if (_filePath == "")
        {
            SaveAs();
        }
        else
        {
            WriteSystemToFile(_filePath);
        }
    }

    private void WriteSystemToFile(string filename)
    {        
        RPGSystemMapper rPGSystemMapper = new RPGSystemMapper();
        RPGSystemDto systemDto = rPGSystemMapper.Serialize(_rPGSystem);
        File.WriteAllText(filename, JsonSerializer.Serialize(systemDto, _serializerOptions));
        ChangesSaved = true;
    }

    public bool CanSave()
    {
        return true;
    }

    public bool CanSaveAs()
    {
        return true;
    }

    public void Back()
    {
        if (!ChangesSaved)
        {
            if (MessageBox.Show("Save changes before closing?", "Unsaved Changes", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Save();
                if (!ChangesSaved)
                {
                    return;
                }
            }
        }
        Parent.CurrentPage = new MainMenuViewModel { Parent = Parent };
    }

    public bool CanBack()
    {
        return true;
    }


    public string SystemName { 
        get{
            return _rPGSystem.SystemName;
        }
        set
        {
            _rPGSystem.SystemName = value;
            OnPropertyChanged(nameof(SystemName));
        }
    }
}
