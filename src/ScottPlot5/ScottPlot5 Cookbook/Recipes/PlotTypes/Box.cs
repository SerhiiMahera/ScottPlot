﻿namespace ScottPlotCookbook.Recipes.PlotTypes;

public class Box : ICategory
{
    public string Chapter => "Plot Types";
    public string CategoryName => "Box Plot";
    public string CategoryDescription => "Box plots show a distribution at a glance";

    public class BoxPlotQuickstart : RecipeBase
    {
        public override string Name => "Box Plot Quickstart";
        public override string Description => "Box plots can be added from a series of values.";

        [Test]
        public override void Execute()
        {
            // TODO: move this functionality to the RandomDataGenerator class
            Random rand = new(0);
            ScottPlot.Plottables.Box CreateRandomBox()
            {
                int N = 50;
                double mean = rand.NextDouble() * 3;
                double stdDev = rand.NextDouble() * 3;

                double[] values = ScottPlot.Generate.RandomNormal(N, mean, stdDev);
                Array.Sort(values);
                double min = values[0];
                double q1 = values[N / 4];
                double median = values[N / 2];
                double q3 = values[3 * N / 4];
                double max = values[N - 1];

                return new ScottPlot.Plottables.Box
                {
                    WhiskerMin = min,
                    BoxMin = q1,
                    BoxMiddle = median,
                    BoxMax = q3,
                    WhiskerMax = max,
                };
            }

            // TODO: construct 3 boxes manually with hard-coded numerical values for simplicity
            List<ScottPlot.Plottables.Box> boxes = Enumerable.Range(0, 5)
                .Select(x => CreateRandomBox())
                .ToList();

            myPlot.Add.Box(boxes);
        }
    }

    public class IndividualBox : RecipeBase
    {
        public override string Name => "Individual Box Plots";
        public override string Description => "One can easily create a box plot with only a single box.";

        [Test]
        public override void Execute()
        {
            Random rand = new(0);

            ScottPlot.Plottables.Box CreateRandomBox()
            {
                int N = 50;
                double mean = rand.NextDouble() * 3;
                double stdDev = rand.NextDouble() * 3;

                double[] values = ScottPlot.Generate.RandomNormal(N, mean, stdDev);
                Array.Sort(values);
                double min = values[0];
                double q1 = values[N / 4];
                double median = values[N / 2];
                double q3 = values[3 * N / 4];
                double max = values[N - 1];

                return new ScottPlot.Plottables.Box
                {
                    WhiskerMin = min,
                    BoxMin = q1,
                    BoxMiddle = median,
                    BoxMax = q3,
                    WhiskerMax = max,
                };
            }

            // TODO: construct 3 boxes manually with hard-coded numerical values for simplicity
            List<ScottPlot.Plottables.Box> boxes = new() { CreateRandomBox() };

            myPlot.Add.Box(boxes);
        }
    }

    public class HorizontalBox : RecipeBase
    {
        public override string Name => "Horizontal Box Plots";
        public override string Description => "Box plots can be oriented horizontally, similarly to bar plots.";

        [Test]
        public override void Execute()
        {
            Random rand = new(0);

            ScottPlot.Plottables.Box CreateRandomBox()
            {
                int N = 50;
                double mean = rand.NextDouble() * 3;
                double stdDev = rand.NextDouble() * 3;

                double[] values = ScottPlot.Generate.RandomNormal(N, mean, stdDev);
                Array.Sort(values);
                double min = values[0];
                double q1 = values[N / 4];
                double median = values[N / 2];
                double q3 = values[3 * N / 4];
                double max = values[N - 1];

                return new ScottPlot.Plottables.Box
                {
                    WhiskerMin = min,
                    BoxMin = q1,
                    BoxMiddle = median,
                    BoxMax = q3,
                    WhiskerMax = max,
                };
            }

            // TODO: construct 3 boxes manually with hard-coded numerical values for simplicity
            List<ScottPlot.Plottables.Box> boxes = Enumerable.Range(0, 5)
                .Select(x => CreateRandomBox())
                .ToList();

            var boxPlot = myPlot.Add.Box(boxes);

            boxPlot.Groups.Orientation = Orientation.Horizontal;
        }
    }

    public class BoxSeries : RecipeBase
    {
        public override string Name => "Box Plot Series";
        public override string Description => "Similarly to bar charts, box plots can be compared across multiple categories.";

        [Test]
        public override void Execute()
        {
            Random rand = new(0);

            ScottPlot.Plottables.Box CreateBox()
            {
                int N = 50;
                double mean = rand.NextDouble() * 3;
                double stdDev = rand.NextDouble() * 3;

                double[] values = ScottPlot.Generate.RandomNormal(N, mean, stdDev);
                Array.Sort(values);
                double min = values[0];
                double q1 = values[N / 4];
                double median = values[N / 2];
                double q3 = values[3 * N / 4];
                double max = values[N - 1];

                return new ScottPlot.Plottables.Box
                {
                    WhiskerMin = min,
                    BoxMin = q1,
                    BoxMiddle = median,
                    BoxMax = q3,
                    WhiskerMax = max,
                };
            }

            int numBoxesPerSeries = 3;
            int numSeries = 2;
            ScottPlot.Plottables.BoxGroup[] series = new ScottPlot.Plottables.BoxGroup[numSeries];
            var colorPalette = new ScottPlot.Palettes.Category10();
            for (int i = 0; i < series.Length; i++)
            {
                series[i] = new ScottPlot.Plottables.BoxGroup
                {
                    Fill = new FillStyle
                    {
                        Color = colorPalette.Colors[i % colorPalette.Colors.Length]
                    },
                    Boxes = new ScottPlot.Plottables.Box[numBoxesPerSeries],
                };

                for (int j = 0; j < series[i].Boxes.Count; j++)
                {
                    var box = CreateBox();
                    box.Position = j;
                    series[i].Boxes[j] = box;
                }
            }

            var boxPlot = myPlot.Add.Box(series);
        }
    }
}
