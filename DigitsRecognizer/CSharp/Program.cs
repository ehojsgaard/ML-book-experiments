using System;

class Program
{
    static void Main(string[] args)
    {
        var distance = new ManhattanDistance();
        var classifier = new BasicClassifier(distance);

        var dataPath = @"/Users/espen/git/ML-book-experiments/DigitsRecognizer/data/";
        var trainingPath = dataPath + "trainingsample.csv";
        var trainingSet = DataReader.ReadObservations(trainingPath);

        classifier.Train(trainingSet);

        var validationPath = dataPath + "validationsample.csv";
        var validationSet = DataReader.ReadObservations(validationPath);

        var correct = Evaluator.Correct(validationSet, classifier);
        Console.WriteLine($"Correctly classified: {correct:P2}");

        Console.WriteLine("Press enter to exit.");
        Console.ReadLine();
    }
}
