using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EdcRuleEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Rule> rules = new List<Rule>{
              // Create some rules using LINQ.ExpressionTypes for the comparison operators
                new Rule ( "Year", ExpressionType.GreaterThan, "2012"),
                new Rule ( "Make", ExpressionType.Equal, "El Diablo"),
                new Rule ( "Model", ExpressionType.Equal, "Torch" )
            };

            var compiledMakeModelYearRules = PrecompiledRules.CompileRule(new List<Car>(), rules);

            // Create a list to house your test cars
            List<Car> cars = new List<Car>();

            // Create a car that's year and model fail the rules validations      
            Car car1_Bad = new Car
            {
                Year = 2011,
                Make = "El Diablo",
                Model = "Torche"
            };

            // Create a car that meets all the conditions of the rules validations
            Car car2_Good = new Car
            {
                Year = 2015,
                Make = "El Diablo",
                Model = "Torch"
            };

            // Add your cars to the list
            cars.Add(car1_Bad);
            cars.Add(car2_Good);

            // Iterate through your list of cars to see which ones meet the rules vs. the ones that don't
            cars.ForEach(car => {
                if (compiledMakeModelYearRules.TakeWhile(rule => rule(car)).Count() > 0)
                {
                    Console.WriteLine(string.Concat("Car model: ", car.Model, " Passed the compiled rules engine check!"));
                }
                else
                {
                    Console.WriteLine(string.Concat("Car model: ", car.Model, " Failed the compiled rules engine check!"));
                }
            });

            Console.WriteLine(string.Empty);
            Console.WriteLine("Press any key to end...");
            Console.ReadKey();
        }
    }
}
