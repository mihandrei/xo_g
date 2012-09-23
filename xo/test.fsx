#load "XoModel.fs"
open XoModel

//facut teste din astea
emptyBoard
|> move (1,1) X
|> print_board 

let b=
    [X;E;X 
     E;X;E
     X;X;E;] 
    |> board_of_list 

b|> winning_ray
b|> print_board

b|> evaluate

emptyBoard |> goal

b|> expand O |> Seq.iter (fun x -> print_board x ; printfn "\n")