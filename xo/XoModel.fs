module XoModel

type Coord = int*int
type Cell  = X|O|E

type Board = Map<Coord , Cell>    

let opponent = function X -> O | O -> X | E -> E

//precompute all rays
//as they are a few for this game
//matlab style: rows = meshgrid(1..3 1..3)  columns = rows'; diags = toepl [1]; toepl[1]'
let rows    =  [for r in 1..3 -> [for c in 1..3 -> (r,c)]]                  
let columns =  [for c in 1..3 -> [for r in 1..3 -> (r,c)]]              
let diags   = [[for r in 1..3 -> (r,r)]; [for r in 1..3 -> (r,4-r)]]
//I suppose this does not allocate a new list
let rays = Seq.concat [rows; columns ; diags]

//let zipWithConst s c = Seq.zip s (Seq.initInfinite (fun _ -> c))

let board_of_list l : Board = 
    let coords = List.concat rows    
    List.zip coords l |> Map.ofList

let emptyBoard  = board_of_list (List.replicate (3*3) E)

let str_board (board:Board) =     
    let cellstr = function E -> "_" | O -> "0" | X -> "X"
    [for r in rows do
        yield [for c in r -> cellstr board.[c]] |> String.concat " "
    ] |> String.concat "\n" 

let print_board = str_board >> printfn "%s"     

let winning_ray (board:Board) =
    let value_of_ray ray = [for c in ray -> board.[c]]
    
    let is_winning_ray =  
        value_of_ray >> fun a -> a = [X;X;X] || a = [O;O;O]
        //sau fun a-> set [[X;X;X]; [O;O;O]] |> Set.contains a        

    Seq.tryFind is_winning_ray rays

let evaluate (board:Board) =
    let eval_ray (h::_) = 
        match board.[h] with 
        | X -> 1 | O -> -1 

    let is_blocked = 
        not <| Map.exists (fun _ v -> v=E) board

    winning_ray board 
    |> function
        | Some r -> Some (eval_ray r)
        | None -> if is_blocked  then Some 0 else None 
    
let expand cell (board:Board) : seq<Board> = 
    //rahat cand iterezi un Map nu primesti tuple ci KeyValuePair
    seq {for kw in board do
            if kw.Value=E then 
                yield Map.add kw.Key cell board
        }
