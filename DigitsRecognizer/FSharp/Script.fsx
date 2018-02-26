open System.IO

type Observation = { Label:string; Pixels: int[] }

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


let manhattanDistance (a1:int[]) (a2:int[]) =
    Array.zip a1 a2
    |> Array.map (fun (i1, i2) -> abs (i1 - i2))
    |> Array.sum

let train (trainingSet:Observation[]) =
    let classify (pixels:int[]) = 
        trainingSet
        |> Array.minBy (fun x -> manhattanDistance x.Pixels pixels)
        |> fun x -> x.Label
    classify    

let classifier = train trainingSet

let correctnessPct =
    validationSet
    |> Array.averageBy (fun x -> if classifier x.Pixels = x.Label then 1. else 0.)

printfn "Correct: %.3f"
