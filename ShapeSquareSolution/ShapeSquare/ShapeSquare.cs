namespace ShapeSquare
{
    // Интерфейс для всех фигур 
    // Фигуры должны реализовать метод CalcSquare(), который считает площадь фигуры
    public interface IShape
    {
        public double CalcSquare();
    }

    // Класс круга 
    // Конструктор круга принимает один параметр radius
    public class Circle : IShape
    {
        private double _radius;
        public Circle(double radius)
        {
            if (radius < 0)
                throw new ArgumentException("Radius can not be negative");
            _radius = radius;
        }

        public double CalcSquare()
        {
            try
            {
                Console.WriteLine("Calc circle square");
                return Math.PI * Math.Pow(_radius, 2);
            }
            catch (Exception ex)
            {
                //Possible stack overflow
                Console.WriteLine(ex.Message);
                return -1;
            }
        }
    }

    // Класс треугольника 
    // Конструктор треугольника принимает три параметрами с длинами сторон
    public class Triangle : IShape
    {
        private List<double> sides;

        // Свойство "Прямоугольный треугольник"
        public bool RightTriangle 
        { 
            get 
            { 
                return sides.Sum(x => x * x) - 2 * Math.Pow(sides.Max(), 2) == 0; 
            } 
        }

        public Triangle(double a, double b, double c)
        {
            sides = new List<double> { a, b, c };

            // Длина стороны не может быть меньше 0
            if (sides.Where(x => x < 0).Any())
                throw new ArgumentException("Parameter can not be negative");

            // Сумма длин двух любых сторон не может быть меньше длины третьей стороны
            foreach (int side in sides)
            {
                if ((sides.Sum(x => x) - 2 * side) < 0)
                    throw new ArgumentException("Parameter values inconsistence");
            }
        }

        public double CalcSquare()
        {
            try
            {
                Console.WriteLine("Calc triangle square");
                double perim = sides.Sum(x => x) / 2;
                return Math.Sqrt(sides.Aggregate(perim, (res, nextval) => res * (perim - nextval)));
            }
            catch (Exception ex)
            {
                //Possible stack overflow
                Console.WriteLine(ex.Message);
                return -1;
            }
        }
    }

    // Фабрика кругов
    public class CircleCreator
    {
        private static CircleCreator? Instance;

        private CircleCreator() { }
        public static CircleCreator GetInstance()
        {
            if (Instance == null)
                Instance = new CircleCreator();

            return Instance;
        }
        public IShape Create(int radius)
        {
            return new Circle(radius);
        }
    }

    // Фабрика треугольников
    public class TriangleCreator
    {
        private static TriangleCreator? Instance;

        private TriangleCreator() { }
        public static TriangleCreator GetInstance()
        {
            if (Instance == null)
                Instance = new TriangleCreator();

            return Instance;
        }

        public IShape Create(double a, double b, double c)
        {
            return new Triangle(a, b, c);
        }
    }
}
