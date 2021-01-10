//OS : Windows 10  
//Browsers Supported : Chrome, Edge 
/*
References : 
                https://www.dofactory.com/net/memento-design-pattern
                https://www.youtube.com/watch?v=8hvvyJPNaBE&ab_channel=BinarySymphony           Implemented the Memento Design Pattern in Java, Was a key reference for building the pattern in C# and implementing it with my existing classes
                https://refactoring.guru/design-patterns/memento
*/
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace CS264_Assignment_4_17300046
{
    class Program
    {
        static void Main(string[] args)
        {
            String Canvas = "";
            Originator originator = new Originator();
            CareTaker careTaker = new CareTaker();
            int count = 0;
    
            
            Console.WriteLine("Welcome to SVG Generator 2.0! Please Enter a value");
            Console.WriteLine("If you need help, type h");
           
            while (true)
            {   
                
                //careTaker.addMemento(originator.save());
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
                    Canvas += addShape(input ,Canvas);
                    originator.setArticle(Canvas);
                    careTaker.addMemento(originator.save());
                    count++;
                    break;

                    case "U":
                    originator.restore(careTaker.undo());               //Code for Undoing an operation
                    careTaker.addMemento(originator.save());
                    count++;
                    break;

                    case "R":
                    originator.restore(careTaker.redo());               //Code for Redoing an Operation
                    careTaker.addMemento(originator.save());
                    break;

                    case "C":
                    Canvas = " ";
                    careTaker.addMemento(originator.save());
                    Console.WriteLine("Clearing Canvas");
                    break;

                    case "P":
                    Console.WriteLine("Canvas : ");
                    printState(originator);
                    Console.WriteLine("If nothing appears, your canvas is empty! Add a shape by typing a <shape> ");
                    break;

                    case "W":
                    Console.WriteLine("Writing to SVG File");
                    buildFile(originator);
                    break;

                    case "Q": 
                    quitApp();
                    return;

                    default: 
                    Console.WriteLine("Incorrect Input, Please Remember to Use Captial Letters and Type H for a List of Commands and to type Q to Quit Application");
                    break;

                }

            }
          
        }

       
        public static String addShape(String input,String Canvas)
        {   
            int x = input.Length - 2;
            string type = input.Substring(2, x);
            
            Random rnd = new Random();

            switch(type.ToLower())
            {
                case "rectangle":
                Console.WriteLine("Creating Rectangle");
                Rectangle r1 = new Rectangle(rnd.Next(1, 1000), rnd.Next(1, 1000), rnd.Next(1, 1000), rnd.Next(1, 1000), "blue");           //Code for Adding an Object to a canvas
                return r1.ToString() +"\n\r";

                case "circle":
                Console.WriteLine("Creating Circle");
                Circle c1 = new Circle(rnd.Next(1, 1000), rnd.Next(1, 1000), rnd.Next(1, 1000),"blue");           //Code for Adding an Object to a canvas
                return c1.ToString() +"\r\n";
                
                case "ellipse":
                Console.WriteLine("Creating Ellipse");
                Ellipse e1 = new Ellipse(rnd.Next(1, 1000), rnd.Next(1, 1000), rnd.Next(1, 1000), rnd.Next(1, 1000),"blue");           //Code for Adding an Object to a canvas
                return e1.ToString() +"\r\n";

                case "line":
                Console.WriteLine("Creating Line");
                Line l1 = new Line(rnd.Next(1, 1000), rnd.Next(1, 1000), rnd.Next(1, 1000), rnd.Next(1, 1000), "blue");           //Code for Adding an Object to a canvas
                return l1.ToString() +"\r\n";    

                case "polyline":
                List <int> points = randomList();               //for generating a random list of a random length
                Console.WriteLine("Creating PolyLine");
                Polyline p1 = new Polyline(points, "blue");           //Code for Adding an Object to a canvas
                return p1.ToString() +"\r\n";
               
               
                case "polygon":
                List <int> pPoints = randomList();              //for generating a random list of a random length
                Console.WriteLine("Creating Polygon");
                Polygon pg1 = new Polygon(pPoints,"red","blue");           //Code for Adding an Object to a canvas
                return pg1.ToString() +"\r\n";

                case "path":
                Console.WriteLine("Creating Path");
                Path ph1 = new Path("M150 0 L75 200 L225 200 Z", "red");           //Code for Adding an Object to a canvas
                return ph1.ToString() +"\r\n";
                
            }
            return Canvas;
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

        public static void buildFile(Originator o)
        {
             string path = @"C:\Users\liama\Desktop\CS264 Assignment 4 17300046/output.svg";

            List<String> lines = new List<string>();
            lines.Add("<svg version='1.1' xmlns = 'http://www.w3.org/2000/svg' height= '1000' width= '1000'>");
            lines.Add(o.getArticle());
            lines.Add("</svg>");

            System.IO.File.WriteAllLines(path, lines);
            Console.WriteLine("Written to File : " +path);
        }

        public static void printState(Originator o)
        {
            Console.WriteLine(o.getArticle());
        }

        public static void quitApp()
        {
            Console.WriteLine("Goodbye!");
            System.Environment.Exit(0);
        }

        //Caretaker
        public class CareTaker 
        {
            private List<Memento> history;             //trying as a list may change

            private int currState = -1;

            public CareTaker()                          //Default Construction for the List of Memento's
            {
                this.history = new List<Memento>();
            }

            public void addMemento(Memento m)           //Method to Add memento into the list
            {
                this.history.Add(m);
                currState = this.history.Count() - 1;
            }

            public Memento getMemento(int index)       //Gets a memento stored at a paticular index
            {
    
                return history[index];
            }

            public Memento undo()                       //Works with an int Current State that has a failsafe to it in bounds; this undo method fetches the second last memento in the list and returns it 
            {
                Console.WriteLine("Undoing State");
                if(currState <= 0)
                {
                    currState = 0;
                    return getMemento(0);
                }

                currState--;
                return getMemento(currState);
            }

            public Memento redo()                       //This method works effectively when an undo call has previously been made; it restores the memento that was undone; if nothing was undone it also contains a failsafe to prevent an out of bounds error
            {
                Console.WriteLine("Redoing State");
                if(currState >= history.Count - 1)
                {
                    currState = history.Count -1;
                    return getMemento(currState);
                }

                currState++;
                return getMemento(currState);
            }


        }

        //Originator 
        public class Originator  //Creates a memento to be stored in the list of mementos 
        {
            private String article;

            public Originator()
            {

            }

            public void setArticle(String article)
            {
                this.article = article;
            }

            public String getArticle()
            {
                return this.article;
            }

            public Memento save()
            {
                return new Memento(article);
            }

            public void restore(Memento m)                 
            {
                this.article = m.getState();
            }
        }


        //Memento
        public class Memento
        {
            public string state;

            public Memento(String state)
            {
                this.state = state;
            }

            public String getState()
            {
                return this.state;
            }

            public void setState(String state)
            {
                this.state = state;
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
   