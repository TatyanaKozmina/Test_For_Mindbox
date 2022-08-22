using ShapeSquare;

// Переменная, реализующая интерфейс IShape
// Она может быть и кругом и треугольником
IShape anyShape;

// Получаем экземпляр фабрики кругов
var circleCreator = CircleCreator.GetInstance();
// Фабрика кругов создаёт круг
anyShape = circleCreator.Create(10);
Console.WriteLine($"{anyShape} square = {anyShape.CalcSquare()}");

// Получаем экземпляр фабрики треугольников
var triangleCreator = TriangleCreator.GetInstance();
// Фабрика треугольников создаёт треугольник
anyShape = triangleCreator.Create(30, 40, 50);
Console.WriteLine($"{anyShape} square = {anyShape.CalcSquare()}");

Console.ReadLine();
