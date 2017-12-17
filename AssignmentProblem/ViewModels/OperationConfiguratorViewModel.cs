using AssignmentProblem.Library;
using AssignmentProblem.Managers;
using Microsoft.Win32;
using System.Collections;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace AssignmentProblem.ViewModels
{
    class OperationConfiguratorViewModel : ViewModelBase
    {
        public OperationConfiguratorViewModel()
        {
            AddOperationCommand = new RelayCommand(p => AddOperation());
            RemoveOperationCommand = new RelayCommand(RemoveOperation, CanRemoveOperation);
        }

        public RelayCommand AddOperationCommand { get; private set; }
        public RelayCommand RemoveOperationCommand { get; private set; }

        public ObservableCollection<OperationViewModel> Operations { get { return OperationManager.Instance.Operations; } }

        private void AddOperation()
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "BMP | *.bmp | GIF | *.gif | JPG | *.jpg; *.jpeg | PNG | *.png | TIFF | *.tif; *.tiff | "
                           + "All Graphics Types|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff";
            dialog.FilterIndex = 1;
            dialog.Multiselect = true;
            dialog.RestoreDirectory = true;

            if(dialog.ShowDialog() != true) return;

            foreach(var item in dialog.FileNames)
                OperationManager.Instance.AddOperation(new FileInfo(item));
        }

        private void RemoveOperation(object items)
        {
            var collection = items as IList<object>;
            if(collection is null) return;

            foreach(var item in collection.ToArray())
                OperationManager.Instance.RemoveOperation((item as OperationViewModel).ID);
        }

        private bool CanRemoveOperation(object items)
        {
            if(items is null) return false;
            return (items as IList<object>).Any();
        }
    }
}
