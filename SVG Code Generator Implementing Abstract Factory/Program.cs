//Pattern Used : Abstract Factory 
//Operating System : Windows 10 Home Ed.
//Browser Support : Google Chrome / Microsoft Edge

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyFactoryMethod
{
    

     abstract class ShapeCreator
    {
        public abstract IShape FactoryMethod();
        public string declareShape()                
        {
        
            var shape = FactoryMethod();
            var result = shape.Operation();

            return result;
        }

    }

    
    class RectangleFactory : ShapeCreator           //Build Class Returns the Interface Shape Type
    {
        public override IShape FactoryMethod()
        {
            return new RectangleShape();
        }

       
    }

    class CircleFactory : ShapeCreator
    {
        public override IShape FactoryMethod()
        {
            return new CircleShape();
        }
    }

    class LineFactory : ShapeCreator
    {
        public override IShape FactoryMethod()
        {
            return new LineShape();
        }
    }

    class EllipseFactory : ShapeCreator
    {
        public override IShape FactoryMethod()
        {
            return new EllipseShape();
        }
    }

    class PolygonFactory : ShapeCreator
    {
        public override IShape FactoryMethod()
        {
            return new PolygonShape();
        }
    }

    class PolylineFactory : ShapeCreator
    {
        public override IShape FactoryMethod()
        {
            return new PolylineShape();
        }
    }

    class PathFactory : ShapeCreator
    {
        public override IShape FactoryMethod()
        {
            return new PathShape();
        }
    }


    public interface IShape             //Contains general structure of the subclasses, in this instance Operation() Returns a string containing the Shapes SVG Code
    {
        public String Operation();
    }
     
    
   
    class RectangleShape : IShape
    {
        
        Random rnd = new Random();
        
        public int X {get; private set;}
        public int Y {get; private set;}
        public int W {get; private set;}
        public int H{get; private set;}
        
        public String fill{get; private set;}
        public String stroke{get; private set;}
        public int strokeWidth{get; private set;}
        
        public String [] generateColour = {"red", "blue", "orange", "yellow", "green", "pink", "black", "white"};
        
        
        public string Operation()
        {
            X = rnd.Next(1,1000);
            Y = rnd.Next(1,1000);
            W = rnd.Next(1,1000);
            H = rnd.Next(1,1000);
    

            strokeWidth = rnd.Next(0,10);
            fill = generateColour[rnd.Next(0,7)];
            stroke = generateColour[rnd.Next(0,7)];

            return "<rect x= '" +X +"' y= '" +Y +"' width= '" +W +"' height= '" +H  +"' stroke-width= '" +strokeWidth +"' fill= '" +fill +"' stroke= '"+stroke +"' />";
        }
    }


    class CircleShape : IShape
    {
      
        Random rnd = new Random();
        public int X {get; private set;}
        public int Y {get; private set;}
        public int R {get; private set;}

        public String fill{get; private set;}
        public String stroke{get; private set;}
        public int strokeWidth{get; private set;}
        
        public String [] generateColour = {"red", "blue", "orange", "yellow", "green", "pink", "black", "white"};
        public String Operation()
        {
            X = rnd.Next(1,1000);
            Y = rnd.Next(1,1000);
            R = rnd.Next(1,1000);

            strokeWidth = rnd.Next(0,10);
            fill = generateColour[rnd.Next(0,7)];
            stroke = generateColour[rnd.Next(0,7)];
    
          
            return "<circle cx='" +X +"' cy='" +Y +"' r='" +R  +"' stroke-width= '" +strokeWidth +"' fill= '" +fill +"' stroke= '"+stroke +"' />";
        }
    }

    class EllipseShape : IShape
    {
        Random rnd = new Random();
        public int xRadius {get; set;}
        public int yRadius {get; set;}
        public int xCentre {get; set;}
        public int yCentre {get; set;}
        public String fill{get; private set;}
        public String stroke{get; private set;}
        public int strokeWidth{get; private set;}
        
        public String [] generateColour = {"red", "blue", "orange", "yellow", "green", "pink", "black", "white"};

        public string Operation()
        {
            xRadius = rnd.Next(1,1000);
            yRadius = rnd.Next(1,1000);
            xCentre = rnd.Next(1,1000);
            yCentre = rnd.Next(1,1000);

            strokeWidth = rnd.Next(0,10);
            fill = generateColour[rnd.Next(0,7)];
            stroke = generateColour[rnd.Next(0,7)];

            return "<ellipse cx='" +xCentre +"' cy='" +yCentre +"' rx='" +xRadius +"' ry='" +yRadius+"' stroke-width= '" +strokeWidth +"' fill= '" +fill +"' stroke= '"+stroke +"' />";
        }
    }

    class LineShape : IShape
    {
        Random rnd = new Random();
        public int x1 {get; set;}
        public int y1 {get; set;}
        public int x2 {get; set;}
        public int y2 {get; set;}

        public String fill{get; private set;}
        public String stroke{get; private set;}
        public int strokeWidth{get; private set;}
        
        public String [] generateColour = {"red", "blue", "orange", "yellow", "green", "pink", "black", "white"};

        public string Operation()
        {
            x1 = rnd.Next(1,1000);
            y1 = rnd.Next(1,1000);
            x2 = rnd.Next(1,1000);
            y2 = rnd.Next(1,1000);


            strokeWidth = rnd.Next(0,10);
            fill = generateColour[rnd.Next(0,7)];
            stroke = generateColour[rnd.Next(0,7)];

            return "<line x1= '" +x1 +"' y1= '" +y1 +"' x2= '" +x2 +"' y2= '" +y2 +"' stroke-width= '" +strokeWidth +"' fill= '" +fill +"' stroke= '"+stroke +"' />";
        }
    }

    

    class PolygonShape : IShape
    {
        Random rnd = new Random();
        public String fill{get; private set;}
        public String stroke{get; private set;}
        public int strokeWidth{get; private set;}
        
        public String [] generateColour = {"red", "blue", "orange", "yellow", "green", "pink", "black", "white"};
        
        
        public string Operation()
        {
            strokeWidth = rnd.Next(0,10);
            fill = generateColour[rnd.Next(0,7)];
            stroke = generateColour[rnd.Next(0,7)];

            int x = rnd.Next(2,10);
            int [] ar = new int[x];
            String r = "<polygon points = '";
            
            for(int i = 0; i < x; i++)
            {
                ar[i] = rnd.Next(1,1000); 

            }

            for(int i = 0; i < x; i++)
            {
                if(i % 2 == 0)
                {
                    r += ar[i] +",";
                }
                else
                {
                    r += ar[i] +" ";
                }

            }
            
            r +="' stroke-width= '" +strokeWidth +"' fill= '" +fill +"' stroke= '"+stroke +"' />";

            return r;
        }
    }


    class PolylineShape : IShape
    {
        Random rnd = new Random();
        String r = "";
        public String fill{get; private set;}
        public String stroke{get; private set;}
        public int strokeWidth{get; private set;}
        
        public String [] generateColour = {"red", "blue", "orange", "yellow", "green", "pink", "black", "white"};
        public string Operation()
        {
            strokeWidth = rnd.Next(0,10);
            fill = generateColour[rnd.Next(0,7)];
            stroke = generateColour[rnd.Next(0,7)];
            int x = rnd.Next(2,10);
            if(x % 2 != 0)
            {
                x++;
            }
            int [] ar = new int[x];
            r = "<polyline points = '";
            
            for(int i = 0; i < x; i++)
            {
                ar[i] = rnd.Next(1,1000); 

            }

            for(int i = 0; i < x; i++)
            {
                if(i % 2 == 0)
                {
                    r += ar[i] +",";
                }
                else
                {
                    r += ar[i] +" ";
                }

            }
            
            r +="' stroke-width= '" +strokeWidth +"' fill= '" +fill +"' stroke= '"+stroke +"' />";


            return r;
        }
    }

    class PathShape : IShape
    {
        Random rnd = new Random();
        public String fill{get; private set;}
        public String stroke{get; private set;}
        public int strokeWidth{get; private set;}
        String r = "";
        public String [] generateColour = {"red", "blue", "orange", "yellow", "green", "pink", "black", "white"};
        public string Operation()
        {
            strokeWidth = rnd.Next(0,10);
            fill = generateColour[rnd.Next(0,7)];
            stroke = generateColour[rnd.Next(0,7)];

            r = "<path  d='M 100 350 q 150 -300 300 0";

            r +="' stroke-width= '" +strokeWidth +"' fill= '" +fill +"' stroke= '"+stroke +"' />";

            return r;
            
        }
    }
    
    class ShapeClient
    {
        public String ClientCode(ShapeCreator creator)                  //Method used to display information to console
        {
            Console.WriteLine("Creating Shape");

            return creator.declareShape();

        }
        public String Main(string type)
        {
            String s = "";
        
            switch(type.ToLower())
            {
                case "rectangle":
                s = ClientCode(new RectangleFactory());                //Calls Client Code Method above and returns a string containing the shapes svg code
                return s;

                case "circle":
                s = ClientCode(new CircleFactory());
                return s;

                case "ellipse":
                s = ClientCode(new EllipseFactory());
                return s;

                case "line":
                s = ClientCode(new LineFactory());
                return s;

                case "polygon":
                s = ClientCode(new PolygonFactory());
                return s;

                case "polyline":
                s = ClientCode(new PolylineFactory());
                return s;

                case "path":
                s = ClientCode(new PathFactory());
                return s;

                default: 
                Console.WriteLine("Incorrect Input, Please Try again and type 'h' for help");
                return null;

            }
           
            
        }  

    }

    class Program
    {   //handles user input
        static void Main(string[] args)
        {
            
            Console.WriteLine("Welcome to SVG Generator 5.0! Please Enter a value");
            Console.WriteLine("If you need help, type h");
            Stack<String> Canvas = new Stack<String>();
            Stack<String> Dump = new Stack<String>();
            while(true)
            {
               String input = Console.ReadLine();
               String [] inputParts = input.Split(' ');
               String s = "";
               String r = " ";
               
               switch(inputParts[0].ToUpper())
               {
                   case "H":
                    Console.WriteLine("Commands : ");
                    Console.WriteLine("H    -    Help - Displays this Message");
                    Console.WriteLine("A    -    Add <shape> to Canvas Example : 'A rectangle' or 'A circle'");
                    Console.WriteLine("U    -    Undo Last Operation");
                    Console.WriteLine("R    -    Redo Last Operation");
                    Console.WriteLine("P    -    Prints SVG File to Console");
                    Console.WriteLine("W    -    Writes Shapes to SVG File");
                    Console.WriteLine("Q    -    Quit Application");
                    break;

                    case "A":
                    s = new ShapeClient().Main(inputParts[1]);
                    Canvas.Push(s);
                    break;

                    case "U":
                    Console.WriteLine("Undoing Operation");
                    if(Canvas.Count() != 0)
                    {
                        String a = Canvas.Pop();
                        Dump.Push(a);
                    }
                    else{
                        Console.WriteLine("Nothing to Undo!");
                    }
                    
                    break;

                    case "R":
                    Console.WriteLine("Re-Doing Last Operation");
                    if(Dump.Count() > 0)
                    {
                        r = Dump.Pop();
                        Canvas.Push(r);
                    }
                    else{
                        Console.WriteLine("Nothing to Re-Do!");
                    }
                    break;

                    case "P":
                    Console.WriteLine("Printing Canvas");
                    foreach(String l in Canvas)
                    {
                        Console.WriteLine(l);
                    }
                    break;

                    case "W":
                    writeFile(Canvas);
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

            //Some Methods to Control the CLI functionality
        public static void quitApp()
        {
            Console.WriteLine("Goodbye!");
            System.Environment.Exit(0);
        }

        public static void writeFile(Stack<String> Canvas)
        {
            //string path = @"C:\Users\liama\VSCodeProjects\Assignment5\output.svg"; //https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-write-to-a-text-file       
            string path = @"C:\Users\Liam Aspell\Desktop\Assignment5Sub\output.svg";
           
            List<String> lines = new List<String>();
            lines.Add("<svg version='1.1' xmlns = 'http://www.w3.org/2000/svg' height= '1000' width= '1000'>");

            foreach(String l in Canvas)
            {
                    lines.Add(l);
            }
            
            lines.Add("</svg>");
            System.IO.File.WriteAllLines(path, lines);
            Console.WriteLine("Written to File : " +path);
        }
    }   
}


