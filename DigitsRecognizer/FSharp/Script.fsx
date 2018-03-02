open System.IO

type Observation = { Label:string; Pixels: int[] }
type DistanceMeasure = int[] -> int[] -> int

let toObservation (csvData:string) =
    let columns = csvData.Split(',')
    let label = columns.[0]
    let pixels = columns.[1..] |> Array.map int
    { Label = label; Pixels = pixels }

let reader path =
    let data = File.ReadAllLines path
    data.[1..]
    |> Array.map toObservation

let dataPath = @"/Users/espen/git/ML-book-experiments/DigitsRecognizer/data/"
let trainingPath = dataPath + "trainingsample.csv"
let trainingSet = reader trainingPath
let validationPath = dataPath + "validationsample.csv";
let validationSet = reader validationPath


let manhattanDistance (a1:int[]) (a2:int[]) : int =
    Array.zip a1 a2
    |> Array.map (fun (i1, i2) -> abs (i1 - i2))
    |> Array.sum

let euclideanDistance (a1:int[]) (a2:int[]) : int =
    Array.zip a1 a2
    |> Array.map (fun (i1, i2) -> pown (i1 - i2) 2)
    |> Array.sum


let train (trainingSet:Observation[]) (distanceMeasure:DistanceMeasure) =
    let classify (pixels:int[]) = 
        trainingSet
        |> Array.minBy (fun x -> distanceMeasure x.Pixels pixels)
        |> fun x -> x.Label
    classify    

let classifierManhattan = train trainingSet manhattanDistance
let classifierEuclidean = train trainingSet euclideanDistance

let correctnessPctManhattan =
    validationSet
    |> Array.averageBy (fun x -> if classifierManhattan x.Pixels = x.Label then 1. else 0.)

printfn "Manhattan Correct: %.3f%%" correctnessPctManhattan

let correctnessPctEuclidean =
    validationSet
    |> Array.averageBy (fun x -> if classifierEuclidean x.Pixels = x.Label then 1. else 0.)
printfn "Euclidean Correct: %.3f%%" correctnessPctEuclidean
