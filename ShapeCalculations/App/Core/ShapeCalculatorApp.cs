using ShapeCalculations.App.Factories;
using ShapeCalculations.App.Input;
using ShapeCalculations.App.UI;
using ShapeCalculations.App.Utilities;
using ShapeCalculations.Domain;

namespace ShapeCalculations.App.Core
{
    /// <summary>
    /// Entry point for the Shape Calculations application.
    /// Wires dependencies and runs the main UI loop.
    /// </summary>
    public class ShapeCalculatorApp
    {
        /// <summary>
        /// Application entry method.
        /// </summary>
        public static void Main(string[] _)
        {
            // Composition root: create the real console and UI, then run.
            var realConsole = new RealConsole();
            var ui = new ConsoleUI(realConsole);
            var inputHandler = new ShapeInputHandler(realConsole);

            ui.ShowHeader();

            bool again = true;

            do
            {
                ui.ShowShapeMenu();

                ShapeType? type = inputHandler.GetShapeType();

                if (type == null)
                {
                    ui.ShowInvalidSelectionMessage();
                    continue;
                }

                double length = inputHandler.GetLength();
                Shape shape = ShapeFactory.CreateShape(type.Value, length);
                ui.ShowShapeResults(shape);

                again = ui.AskToContinue();

            } while (again);

            System.Console.WriteLine();
            ui.ShowFooter();
        }
    }
}