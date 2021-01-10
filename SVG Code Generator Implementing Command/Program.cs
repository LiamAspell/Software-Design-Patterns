/*
    IDE Used : VSCode     
    OS : Windows 10 Home Edition            
    Svg Output : Used the chrome browser for testing, works also with Edge Browser and with Internet Explorer :)

    Extra Credit I attempted : 
                                (ii) include commands for applying	basic styles for generated shapes
                                (iv) include commands to add styled and formatted text to the canvas


*/
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
/*
References : 
                https://docs.microsoft.com/en-us/dotnet/api/system.io.filestream?view=netcore-3.1
                https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-write-to-a-text-file
*/

namespace Assignment3
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = new User();

           
            Canvas c1 = new Canvas();
            
            Stack<Shape> canvas = new Stack<Shape>();
            Interface(c1, user);
        }

        public static void Interface( Canvas c1, User user)
        {
            //switch statement for the user interface
            Console.WriteLine("Welcome to SVG Generator 2.0! Please Enter a value");
            Console.WriteLine("If you need help, type h");
            

            
            while (true)
            {
                string input = Console.ReadLine();
                string [] inputParts = input.Split(' ');
                switch(inputParts[0].ToUpper())
                {
                    case "H":
                    Console.WriteLine("Commands : ");
                    Console.WriteLine("H    -    Help - Displays this Message");
                    Console.WriteLine("A    -    Add <shape> to Canvas Example : 'A rectangle' or 'A circle'");
                    Console.WriteLine("U    -    Undo Last Operation");
                    Console.WriteLine("R    -    Redo Last Operation");
                    Console.WriteLine("C    -    Clears Canvas");
                    Console.WriteLine("P    -    Prints SVG File to Console");
                    Console.WriteLine("W    -    Writes Shapes to SVG File");
                    Console.WriteLine("Q    -    Quit Application");
                    break;

                    case "A":
                    addShape(input, c1, user);
                    break;

                    case "U":
                    Console.WriteLine("Undo");
                    user.Undo();
                    break;

                    case "R":
                    Console.WriteLine("Redo");
                    user.Redo();
                    break;

                    case "C":
                    Console.WriteLine("Clearing Canvas");
                    c1.canvasClear();
                    break;

                    case "P":
                    Console.WriteLine("Canvas : ");
                    Console.WriteLine(c1);
                    Console.WriteLine("");
                    Console.WriteLine("If nothing appears, your canvas is empty! Add a shape by typing a <shape> ");
                    break;

                    case "W":
                    Console.WriteLine("Writing to SVG File");
                    buildFile(c1);
                    break;

                    case "Q": 
                    quitApp();
                    return;

                    default: 
                    Console.WriteLine("Incorrect Input, Please Remember to Use Captial Letters and Type H for a List of Commands and to type Q to leave");
                    break;
                }
            
            }
               
                
            
        }

        //Controls Input A
        
        public static void addShape(string input, Canvas canvas, User user)
        {       
            int x = input.Length - 2;
            string type = input.Substring(2, x);
            
            Random rnd = new Random();

            switch(type.ToLower())
            {
                case "rectangle":
                user.Action(new AddShapeCommand(new Rectangle (rnd.Next(1, 1000), rnd.Next(1, 1000), rnd.Next(1, 1000), rnd.Next(1, 1000), "red"), canvas));
                break;

                case "circle":
                user.Action(new AddShapeCommand(new Circle (rnd.Next(1, 1000), rnd.Next(1, 1000), rnd.Next(1, 1000), "blue"), canvas));
                break;

                case "ellipse":
                user.Action(new AddShapeCommand(new Ellipse (rnd.Next(1, 1000), rnd.Next(1, 1000), rnd.Next(1, 1000), rnd.Next(1, 1000), "orange"), canvas));
                break;

                case "line":
                user.Action(new AddShapeCommand(new Line (rnd.Next(1, 1000), rnd.Next(1, 1000), rnd.Next(1, 1000), rnd.Next(1, 1000), "green"), canvas));
                break;

                case "polyline":
                List <int> points = randomList();               //for generating a random list of a random length
                user.Action(new AddShapeCommand(new Polyline(points, "blue"), canvas));
                break;

                case "polygon":
                List <int> pPoints = randomList();              //for generating a random list of a random length
                user.Action(new AddShapeCommand(new Polygon(pPoints, "black", "red"), canvas));
                break;

                case "path":
                user.Action(new AddShapeCommand(new Path("M150 0 L75 200 L225 200 Z", "red"), canvas));
                break;

                case "text":
                user.Action(new AddShapeCommand(new Text(rnd.Next(1,1000), rnd.Next(1, 1000), "I love SVG"), canvas));
                break;


            }
            

        }

        public static List<int> randomList()
        {
            Random rnd = new Random();
            List<int> l = new List<int>();
            int y = rnd.Next(2, 12);
            if(y % 2 == 1)
            {
                y--;                //Ensures every list length is even for the polyline and polygon
            }

            for(int i = 0; i < y; i++)
            {
                l.Add(rnd.Next(1,1000));
            }
            return l;
        } 



        

        //Controls Input P
        public static void printList(List<string> lines)            
        {
            Console.WriteLine("Writing SVG File to Console...");
            lines.ForEach(Console.WriteLine);
        }

        //Controls Input W
        public static void buildFile(Canvas c)
        {
            string path = @"C:\Users\liama\VSCodeProjects\Assignment3\output.svg"; //https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-write-to-a-text-file       
            //string path = @"C:\Users\Liam Aspell\VSCodeProjects\Assignment3\Assignment3\output.svg";
            List<string> lines = new List<string>();
            lines.Add("<svg version='1.1' xmlns = 'http://www.w3.org/2000/svg' height= '1000' width= '1000'> \n");

            //loop canvas toString methods to string list
            string x = c.ToString();
            
            lines.Add(x);
            lines.Add("</svg>");
            System.IO.File.WriteAllLines(path, lines);
            Console.WriteLine("Written to File : " +path);
        }

        //Controls Input Q
        public static void quitApp()
        {
            Console.WriteLine("Goodbye!");
            System.Environment.Exit(0);
        }


            

   

        public abstract class Command
        {
            public abstract void Do();     // what happens when we execute (do)
            public abstract void Undo();   // what happens when we unexecute (undo)
        }

        public class AddShapeCommand : Command
        {
            Shape shape;
            Canvas canvas;

            public AddShapeCommand(Shape s, Canvas c)
            {
                shape = s;
                canvas = c;
            }

            // Adds a shape to the canvas as "Do" action
            public override void Do()
            {
                canvas.Add(shape);
            }
            // Removes a shape from the canvas as "Undo" action
            public override void Undo()
            {
                shape = canvas.Remove();
            }
        }

        public class DeleteShapeCommand : Command
        {

            Shape shape;
            Canvas canvas;

            public DeleteShapeCommand(Canvas c)
            {
                canvas = c;
            }

            // Removes a shape from the canvas as "Do" action
            public override void Do()
            {
                shape = canvas.Remove();
            }

            // Restores a shape to the canvas a an "Undo" action
            public override void Undo()
            {
                canvas.Add(shape);
            }
        }



    public class User
    {
        private Stack<Command> undo;
        private Stack<Command> redo;

        public int UndoCount { get => undo.Count; }
            public int RedoCount { get => undo.Count; }
            public User()
            {
                Reset();
                Console.WriteLine("Created a new User!"); Console.WriteLine();
            }
            public void Reset()
            {
                undo = new Stack<Command>();
                redo = new Stack<Command>();
            }

            public void Action(Command command)
            {
                // first update the undo - redo stacks
                undo.Push(command);  // save the command to the undo command
                redo.Clear();        // once a new command is issued, the redo stack clears

                // next determine  action from the Command object type
                // this is going to be AddShapeCommand or DeleteShapeCommand
                Type t = command.GetType();
                if (t.Equals(typeof(AddShapeCommand)))
                {
                    Console.WriteLine("Command Received: Add new Shape!" + Environment.NewLine);
                    command.Do();
                }
                if (t.Equals(typeof(DeleteShapeCommand)))
                {
                    Console.WriteLine("Command Received: Delete last Shape!" + Environment.NewLine);
                    command.Do();
                }
            }

        public void Undo()
        {
            Console.WriteLine("Undoing operation!"); Console.WriteLine();
            if (undo.Count > 0)
            {
                Command c = undo.Pop(); c.Undo(); redo.Push(c);
            }
        }



        public void Redo()
        {
            Console.WriteLine("Redoing operation!"); Console.WriteLine();
            if (redo.Count > 0)
            {
                Command c = redo.Pop(); c.Do(); undo.Push(c);
            }
        }
    }
    


//CLASSES DEFAULT CONSTRUCTOR public circle(){}


    public class Canvas
    {
        public int x{get; set; }
        public int y{get; set; }

         private Stack <Shape> canvas = new Stack<Shape>();

        public void Add(Shape s)
        {
            canvas.Push(s);
            Console.WriteLine("Added Shape to Canvas : {0}" + Environment.NewLine, s);
        }

        public Shape Remove()
        {
            Shape s = canvas.Pop();
            Console.WriteLine("Removed Shape from Canvas : {0}" + Environment.NewLine, s);
            return s;
        }

        public Canvas()
        {
            Console.WriteLine("\nCreated a New Canvas!"); Console.WriteLine();
        }

        public void canvasClear()
        {
            canvas.Clear();
        }

        public override string ToString()
            {
                string str = "";
                foreach (Shape s in canvas)
                {
                    str +=  s +"\r\n"; 
                    
                }
                return str;
            }
    }

    public class Shape 
    {
        public string fill{get; set; }
        public string stroke{get; set; }
        public string strokeWidth{get; set; }

        public string style(string fill, string stroke, string strokeWidth)
        {
            return "style='fill: '" +fill +"' stroke: '" +stroke +"' stroke-width: '" +strokeWidth +"' />";
        }

    }

    class Rectangle : Shape     //Inherits its styling parameters from base class Shapes
    {           
        public int X {get; private set;}
        public int Y {get; private set;}
        public int W {get; private set;}
        public int H {get; private set;}

        public Rectangle(int x, int y, int w, int h, string f)
        {
            X = x; Y = y; W = w; H = h; fill = f;
        }

        public override string ToString()
        {
            return "<rect x= '" +X +"' y= '" +Y +"' width= '" +W +"' height= '" +H +"' fill= '" +fill +"' />";                
        }
    }

    public class Circle : Shape
    {
        public int X {get; set;}
        public int Y {get; set;}
        public int R {get; set;}

        public Circle(int x, int y, int r, string f)
        {
            X = x; Y = y; R = r; fill = f;
        }

        public override string ToString()
        {
            return "<circle cx='" +X +"' cy='" +Y +"' r='" +R +"' fill= '" + fill  +"' />"; 
        }
    }

    class Ellipse : Shape 
    {
        public int xRadius {get; set;}
        public int yRadius {get; set;}
        public int xCentre {get; set;}
        public int yCentre {get; set;}

        public Ellipse(int xr, int yr, int xc, int yc, string f)
        {
            xRadius = xr; yRadius = yr; xCentre = xc; yCentre = yc; fill = f;
        }

        public override string ToString()
        {
            return "<ellipse cx='" +xCentre +"' cy='" +yCentre +"' rx='" +xRadius +"' ry='" +yRadius +"' fill= '" +fill +"' />";
        }
    }

    class Line :Shape      //polyline extends
    {
        public int xPos1 {get; set;}
        public int yPos1 {get; set;}
        public int xPos2 {get; set;}
        public int yPos2 {get; set;}

        public Line(int x1, int y1, int x2, int y2, string s)
        {
            xPos1 = x1; yPos1 = y1; xPos2 = x2; yPos2 = y2; stroke = s;
        }

        public override string ToString()
        {
            return "<line x1= '" +xPos1 +"' y1= '" +yPos1 +"' x2= '" +xPos2 +"' y2= '" +yPos2 +"' style= 'stroke:" +stroke +";' />";
        }
    }

    class Polyline : Shape
    {
        public List<int> PolyLinePoints{get; set;}

        public Polyline(List <int> Ppoints, string s)
        {
            PolyLinePoints = Ppoints;   stroke = s;
        }

        public override string ToString()
        {
            //int count = 0;
            string  x = "<polyline points = '";               //<polyline points = '30, 40, 30, 40, 30, 20, ' /> need to get rid of the space at the end of the list before friday
            int count = 0;
            foreach(int item in PolyLinePoints)
            {
                x += item;
                if(count % 2 == 0)
                {
                    x+=",";
                }
                else
                {
                    x+=" ";
                }
                count++;
            }  
            x += "' style= 'stroke:" +stroke +"' />";
            return x;
        }
    }

    class Polygon :Shape
    {
        public List<int> PolygonPoints{get; set;}
        
        public Polygon(List <int> pPoints, string s, string f)
        {
            PolygonPoints = pPoints; stroke = s; fill = f;
        }

        public override string ToString()
        {
            int count = 0;
            string  x = "<polygon points = '";               //<polygon points = '200,10 250,190 160,210 ' /> need to get rid of the space at the end of the list before friday
            foreach(int item in PolygonPoints)
            {
                x += item;
                if(count % 2 == 0)
                {
                    x+=",";
                }
                else
                {
                    x+=" ";
                }
                count++;
            }
            x += "' style= 'stroke:" +stroke +" fill:" +fill +"' />";
            return x;
        }

    }

    class Path :Shape
    {
        public string input{get; set; }
        
        public Path(string i, string f)
        {
            input = i; fill = f;
        }

        public override string ToString()
        {
            return "<path d= '" +input +"' fill= '" +fill +"' />";                
        }
           
    }

    class Text : Shape
    {
        public int xPos {get; set;}
        public int yPos {get; set;}
        public string text {get; set;}

        public Text(int x, int y, string t)
        {
            xPos = x; yPos = y; text = t;
        }

        public override string ToString()
        {
            return "<text x='" +xPos +"' y='" +yPos +"'>" +text +"</text>"; 
        }

    
        
    }

    }

}