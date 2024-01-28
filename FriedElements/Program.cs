using FriedElements;
using FriedElements.Elements;
using FriedPixelWindow;
using GameOfLife;
using SFML.Graphics;
using SFML.Window;
using System.Collections;

internal class Program
{
    public static byte[] data2;
    public static List<Element> Elements = new List<Element>() 
    {
        new Sand(),
        new Stone(),
        new Water(),
        new Oil(),

        new Steam(),
        new Smoke(),

        new Empty(),
    };
    //public const int waitTime = 250;
    public const int waitTime = 1;
    public const int gridSize = 200;
    public const int Scale = 800;
    public const int size = gridSize * gridSize;
    //public static Grid Grid = new Grid(gridSize);
    public static CellularMatrix Matrix = new CellularMatrix(gridSize);
    public static bool running = false;
    //public static Element SelectedElement = new Sand();
    public static Type SelectedElement = typeof(Sand);
    public static int SelectedSize = 5;
    public const int SelectionRows = 2;
    public static bool busy = true;

    private static void Main(string[] args)
    {

        data2 = new byte[(Elements.Count * 4) * SelectionRows];
        var t16 = (Elements.Count *  (4));
        for (int i = 0; i < t16; i += 4)
        {
            var color = Elements[i / 4].Color;
            data2[i + 0] = color.R;
            data2[i + 1] = color.G;
            data2[i + 2] = color.B;
            data2[i + 3] = color.A;
        }
        //Matrix.ReadData();

        //stepper
        var appManager = new PixelManager();
        //main window
        var simulationWindow = new PixelWindow(gridSize, gridSize, Scale / gridSize, "FriedElements - Playground", appManager,
            fixedTimestep: 20, framerateLimit: 300, debugInfo:false);

        var selectionWindow = new PixelWindow((uint)Elements.Count, SelectionRows, Scale / (uint)data2.Length* SelectionRows, "FriedElements - Select Element", new PixelManager2(),
            fixedTimestep: 20, framerateLimit: 300, debugInfo: false);

        var selectionRW = selectionWindow.GetRenderWindow();
        selectionRW.MouseButtonPressed += SelectionRW_MouseButtonPressed;

        var simulationRW = simulationWindow.GetRenderWindow();
        simulationRW.MouseButtonPressed += SimulationRW_MouseButtonPressed;

        Matrix.Set(0, 0, new Stone());
        Matrix.Set(19, 19, new Sand());
        Matrix.Set(5, 5, new Sand());
        Matrix.Set(4, 2, new Stone());
        Matrix.Set(5, 2, new Stone());
        Matrix.Set(6, 2, new Stone());

        running = true;
        Thread simulationThread = new Thread(new ThreadStart(Stepper));
        simulationThread.Start();

        selectionRW.SetActive(false);
        Thread selectionThread = new Thread(() => selectionWindow.Run());
        selectionThread.Start();

        simulationWindow.Run();
    }

    private static void SelectionRW_MouseButtonPressed(object? sender, MouseButtonEventArgs e)
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;

        var newX = e.X / (Scale / (data2.Length / SelectionRows));
        var newY = e.Y / (Scale / (data2.Length / SelectionRows));
        //newY = (gridSize - newY) - 1;
        Console.WriteLine($"MouseDown X:{newX}, Y:{newY}");



        if (newY == 0)
        { 
            Element element = Elements[newX];
            Console.WriteLine($"Selected Element:{element}");
            SelectedElement = element.GetType();
        }
        else if (newY == 1)
        {
            SelectedSize = newX * 5;
            Console.WriteLine($"Selected Size:{SelectedSize}");
        }
        Console.ResetColor();
    }

    //public static void Input() 
    //{
    //    while (true)
    //    {
    //        Thread.Sleep(1);
    //        bool spacedown = Keyboard.IsKeyPressed(Keyboard.Key.Space);
    //        if (spacedown)
    //        {
    //            running = !running;
    //            Console.WriteLine($"Game {(running ? "Resumed" : "Paused")}!");
    //            Thread.Sleep(200);
    //        }
    //        bool Rdown = Keyboard.IsKeyPressed(Keyboard.Key.R);
    //        if (Rdown)
    //        {
    //            bool wasdown = running;
    //            running = false;
    //            Console.WriteLine($"Importing data..");
    //            Matrix.ReadData();
    //            Thread.Sleep(10);
    //            Console.WriteLine($"Data has been imported");
    //            if (wasdown)
    //                running = true;
    //            Thread.Sleep(400);
    //        }
    //        bool Edown = Keyboard.IsKeyPressed(Keyboard.Key.E);
    //        if (Edown)
    //        {
    //            bool wasdown = running;
    //            running = false;
    //            Console.WriteLine($"Exporting data..");
    //            Grid.WriteData();
    //            Thread.Sleep(10);
    //            Console.WriteLine($"Data has been exported");
    //            if (wasdown)
    //                running = true;
    //            Thread.Sleep(400);
    //        }
    //        bool Cdown = Keyboard.IsKeyPressed(Keyboard.Key.C);
    //        if (Cdown)
    //        {
    //            running = false;
    //            Grid.Fill(0);
    //            Console.WriteLine($"Screen Cleared!");
    //            Thread.Sleep(400);
    //        }
    //        bool Fdown = Keyboard.IsKeyPressed(Keyboard.Key.F);
    //        if (Fdown)
    //        {
    //            running = false;
    //            Grid.Fill(1);
    //            Console.WriteLine($"Screen Filled!");
    //            Thread.Sleep(400);
    //        }
    //        bool Sdown = Keyboard.IsKeyPressed(Keyboard.Key.S);
    //        if (Sdown)
    //        {
    //            running = false;
    //            Console.WriteLine($"Stepping");
    //            Thread.Sleep(10);
    //            Grid.Step();
    //            Thread.Sleep(200);
    //        }
    //        bool Ddown = Keyboard.IsKeyPressed(Keyboard.Key.D);
    //        if (Ddown)
    //        {
    //            Grid.Debug = !Grid.Debug;
    //            Console.WriteLine($"Debug {(running ? "Enabled" : "Disabled")}!");
    //            Thread.Sleep(200);
    //        }
    //        bool Vdown = Keyboard.IsKeyPressed(Keyboard.Key.V);
    //        if (Vdown)
    //        {
    //            Grid.PreCalc();
    //            Grid.Debug = true;
    //            Console.WriteLine($"Debug Enabled");
    //            Console.WriteLine("Calculated the cells");
    //            Thread.Sleep(200);
    //        }
    //    }
    //}

    private static void SimulationRW_MouseButtonPressed(object? sender, MouseButtonEventArgs e)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;

        var newX = e.X / (Scale / gridSize);
        var newY = e.Y / (Scale / gridSize);
        newY = (gridSize - newY)-1;
        Console.WriteLine($"MouseDown X:{newX}, Y:{newY}");
        Console.ResetColor();

        //Element element = (e.Button == Mouse.Button.Left ? new Stone() : new Sand());
        //Element element = e.Button switch
        //{
        //    Mouse.Button.Left => new Stone(),
        //    Mouse.Button.Right => new Sand(),
        //    Mouse.Button.Middle => new Empty(),
        //};
        // Set pixels in a square area around the selected point
        while (busy) { Thread.Sleep(0); }
        for (int i = -SelectedSize / 2; i <= SelectedSize / 2; i++)
        {
            for (int j = -SelectedSize / 2; j <= SelectedSize / 2; j++)
            {
                Element element = Activator.CreateInstance(SelectedElement) as Element;
                Matrix.Set(newX + i, newY + j, element);
            }
        }

        //Matrix.Set(newX, newY, element);
    }

    public static void Stepper()
    {
        while (true)
        {
            Thread.Sleep(waitTime);
            if (running)
            { 
                busy = true;
                Matrix.StepAll();
                busy = false;
            }
        }
    }    
}
class PixelManager : IPixelWindowAppManager
{
    public void OnLoad(RenderWindow renderWindow)
    {
        // On load function - runs once at start.
        // The SFML render window provides ability to set up events and input (maybe store a reference to it for later use in your update functions)
    }

    public void Update(float frameTime)
    {
        // Update function - process update logic to run every frame here
    }

    public void FixedUpdate(float timeStep)
    {
        // Fixed update function - process logic to run every fixed timestep here
    }

    public void Render(PixelData pixelData, float frameTime)
    {
        pixelData.Clear();
        pixelData.SetBearRawData(Program.Matrix.data);
    }
}
class PixelManager2 : IPixelWindowAppManager
{
    public void OnLoad(RenderWindow renderWindow){}
    public void Update(float frameTime) { }
    public void FixedUpdate(float timeStep) { }
    public void Render(PixelData pixelData, float frameTime)
    {
        pixelData.Clear();
        pixelData.SetBearRawData(Program.data2);
    }
}


