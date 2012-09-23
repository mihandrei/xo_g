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