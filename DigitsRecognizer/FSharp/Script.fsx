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