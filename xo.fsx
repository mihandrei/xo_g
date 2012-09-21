type Coord = int*int
type Cell  = X|O|E

type Board = Map<Coord , Cell>    

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

let emptyBoard  = board_of_list (List.replicate (3*3) E)

let move c cell (board:Board) = board.Add (c , cell)

let str_board (board:Board) =     
    let cellstr = function E -> "_" | O -> "0" | X -> "X"
    [for r in rows do
        yield [for c in r -> cellstr board.[c]] |> String.concat ""
    ] |> String.concat "\n" 
    
emptyBoard
|> move (1,1) X
|> str_board 

let goal (board:Board) =
    let value_of_ray ray = [for c in ray -> board.[c]]
    
    let winning_ray =  
        value_of_ray >> fun a -> a = [X;X;X] || a = [O;O;O]
        //sau fun a-> set [[X;X;X]; [O;O;O]] |> Set.contains a        

    Seq.tryFind winning_ray rays
    |> Option.map (fun ray -> board.[ray.Head], ray)
    

let expand (board:Board) = 
    ()

let minmax (board:Board) side = 
    ()

[X;E;X 
 E;X;E
 X;E;E;] 
|> board_of_list 
|> goal