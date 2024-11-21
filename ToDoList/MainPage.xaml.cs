using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ToDoList
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MyViewModel();
        }
        // None
        // Strikethrough
        public class MyViewModel : INotifyPropertyChanged {
            public ObservableCollection<MyTask> Items { get; set; }
            public ICommand AddCommand { get; set; }
            public ICommand AddStrokeCommand { get; set; }
            public ICommand RemoveCommand { get; set; }

            private string _entryText;

            public string EntryText
            {
                get => _entryText;
                set
                {
                    _entryText = value;
                    OnPropertyChanged(nameof(EntryText));
                }
            }

            public MyViewModel() {
                Items = new ObservableCollection<MyTask>();
                AddCommand = new Command<object>(addTask);
                AddStrokeCommand = new Command<object>(ToggleTask);
                RemoveCommand = new Command<MyTask>(removeFromItems);

            }

            private void addTask(object parametr)
            {
                if (parametr is string taskName && !string.IsNullOrWhiteSpace(taskName))
                {
                    Items.Add(new MyTask { Name = taskName });
                    EntryText = string.Empty; 
                }
            }

            private void removeFromItems (MyTask parametr)
            {
                if (parametr != null)
                {
                    Items.Remove(parametr);
                }
            }

            private void ToggleTask(object parametr)
            {
                if (parametr is MyTask task)
                {
                    task.IsDone = !task.IsDone;
                }
            }

            public event PropertyChangedEventHandler? PropertyChanged;
            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public class MyTask : INotifyPropertyChanged
        {
            private bool _is_done;
            private string _name;
            public string Name
            {
                get => _name;
                set
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
            public bool IsDone
            {
                get => _is_done;
                set
                {
                    _is_done = value;
                    OnPropertyChanged(nameof(IsDone));
                    OnPropertyChanged(nameof(Stroke));
                }
            }

            public TextDecorations Stroke => IsDone ? TextDecorations.Strikethrough : TextDecorations.None;
            


            public event PropertyChangedEventHandler? PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

}
