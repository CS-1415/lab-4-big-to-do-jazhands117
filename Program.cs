using System.ComponentModel;

class TodoListApp {
    private TodoList _tasks; //TodoList is a separate class, built below//
    private bool _showHelp = true;
    private bool _insertMode = true;
    private bool _quit = false;

    public TodoListApp(TodoList tasks) {
        _tasks = tasks; 
    }

    public void Run() {
        while (!_quit) {
            Console.Clear();
            Display();
            ProcessUserInput();
        }
    }

    public void Display() {
        DisplayTasks();
        if (_showHelp) {
            DisplayHelp();
        }
    }

    public void DisplayBar() {
        Console.WriteLine("----------------------------");
    }

    public string MakeRow(int i) {
        Task task = _tasks.GetTask(i);
        string arrow = "  ";
        if (task == _tasks.CurrentTask) arrow = "->";
        string check = " ";
        if (task.Status == CompletionStatus.Done) check = "X";
        return $"{arrow} [{check}] {task.Title}";
    }

    public void DisplayTasks() {
        DisplayBar();
        Console.WriteLine("Tasks:");
        for (int i = 0; i < _tasks.Length; i++) {
            Console.WriteLine(MakeRow(i));
        }
        DisplayBar();
    }

    public void DisplayHelp() {
        Console.WriteLine(
@"Instructions:
   h: show/hide instructions
   ↕: select previous or next task (wrapping around at the top and bottom)
   ↔: reorder task (swap selected task with previous or next task)
   space: toggle completion of selected task
   e: edit title
   i: insert new tasks
   delete/backspace: delete task");
        DisplayBar();
    }

    private string GetTitle() {
        Console.WriteLine("Please enter task title (or [enter] for none): ");
        return Console.ReadLine()!;
    }

    public void ProcessUserInput() {
        if (_insertMode) {
            string taskTitle = GetTitle();
            if (taskTitle.Length == 0) {
                _insertMode = false;
            } else {
                //_tasks.Insert(taskTitle);
            }
        } else {
            switch (Console.ReadKey(true).Key) {
                case ConsoleKey.Escape:
                    _quit = true;
                    break;
                case ConsoleKey.UpArrow:
                    //_tasks.SelectPrevious();
                    break;
                case ConsoleKey.DownArrow:
                    //_tasks.SelectNext();
                    break;
                case ConsoleKey.LeftArrow:
                    //_tasks.SwapWithPrevious();
                    break;
                case ConsoleKey.RightArrow:
                    //_tasks.SwapWithNext();
                    break;
                case ConsoleKey.I:
                    _insertMode = true;
                    break;
                case ConsoleKey.E:
                    //_tasks.CurrentTask.Title = GetTitle();
                    break;
                case ConsoleKey.H:
                    _showHelp = !_showHelp;
                    break;
                case ConsoleKey.Enter:
                case ConsoleKey.Spacebar:
                    //_tasks.CurrentTask.ToggleStatus();
                    break;
                case ConsoleKey.Delete:
                case ConsoleKey.Backspace:
                    //_tasks.DeleteSelected();
                    break;
                default:
                    break;
            }
        }
    }
  }

//Building the actual list//
class TodoList
{
    //create list to store new tasks//
    private List<Task> _tasks = new List<Task>();
    public int Length => _tasks.Count;
    public Task CurrentTask //takes care of null possibility//
    {
        get
        {
            if (_tasks.Count > 0)
            {
                return _tasks[0];
            }
            else
            {
                return null;
            }
        }
    }
    public Task GetTask(int i) => _tasks[i];

    public void Insert(string title)
    {
        _tasks.Add(new Task(title));
    }
}

//tasks themselves//
class Task
{
    //using nomenclature from MakeRow//
    public string Title { get; set; }
    public CompletionStatus Status { get; set; } //enum built below Main//
    public Task(string title)
    {
        Title = title;
        Status = CompletionStatus.NotDone;
    }
    public void ToggleStatus()
    {
        if (Status == CompletionStatus.Done)
        {
            Status = CompletionStatus.NotDone;
        }
        else
        {
            Status = CompletionStatus.Done;
        }
    }
}

class Program
{
    static void Main()
    {
        //new TodoListApp(new TodoList()).Run();
    }
}

//completion enum, like the older labs :)//
enum CompletionStatus { NotDone, Done }