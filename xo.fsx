type Coord = int*int
type Token = X|O
type CellContent = Token option 

type Board = Map<Coord , CellContent>    

//precompute all rays
//as they are a few for this game
//matlab style: rows = meshgrid(1..3 1..3)  columns = rows'; diags = toepl [1]; toepl[1]'
let rows =    [for c in 1..3 -> [for r in 1..3 -> (r,c)]]              
let columns = [for r in 1..3 -> [for c in 1..3 -> (r,c)]]                  
let diags =   [[for r in 1..3 -> (r,r)]; [for r in 1..3 -> (r,4-r)]]
//I suppose this does not allocate a new list
let rays = Seq.concat [rows; columns ; diags]

//let zipWithConst s c = Seq.zip s (Seq.initInfinite (fun _ -> c))

let board_of_list l : Board = 
    let coords = List.concat rows    
    List.zip coords l |> Map.ofList

let emptyBoard  = 
    board_of_list (List.replicate (3*3) None)

let move c cell (board:Board)= 
    board.Add (c , cell)

let str_board (board:Board) =     
    let cellstr = function None -> "_" | Some O -> "0" | Some X -> "X"

    [for r in rows do
        yield [for c in r -> cellstr board.[c]] |> String.concat ""
    ] |> String.concat "\n" 

emptyBoard
|> move (1,1) (Some X)
|> str_board 
