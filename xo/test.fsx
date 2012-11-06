#load "XoModel.fs"
#load "search.fs"
open XoModel
open Search
//facut teste din astea
emptyBoard
|> Map.add (1,1) X
|> print_board 

let b=
    [X;E;X 
     E;X;E
     X;X;E;] 
    |> board_of_list 

b|> print_board
b|> winning_ray
b|> evaluate
b|> expand O |> Seq.iter (fun x -> print_board x ; printfn "\n")

let c=
    [X;E;X 
     O;E;E
     E;O;E;] 
    |> board_of_list 

c|> print_board
c|> ai_move X |> print_board


let ioseq = Seq.initInfinite (fun _ -> System.Console.ReadLine())
                

let s = seq {for i in 1..4 ->i}

let readLines filePath = System.IO.File.ReadLines(filePath)

let lines = readLines @"D:\testFile.txt"


let doit secv=
    let g = Seq.groupBy (fun x->x="ana") secv
    
    let [(true, x); (false,y)] = List.ofSeq g
    Seq.append x [Seq.head y]

doit lines
doit ioseq

let rec consuma s = 
   printfn "%A" <| List.ofSeq s
   if not <| Seq.isEmpty s 
   then consuma <| Seq.skip 1 s    
   
let sr =Seq.skip 1 lines

printfn "%A" <| Seq.head sr

for i in [1..10] do
    printfn "skip   %A" <| Seq.skip 1 s
    printfn "s      %A" <| List.ofSeq s
