module Search
//trei cutite stau 
//unu rade unu plange si-altu zice c-ar bea sange
open XoModel

let rec minmax side board = 
    board |> print_board
    printfn ""
    match evaluate board with 
    | Some score -> score , board
    | None ->  
        let children = board |> expand side 
        let scores = [for c in children -> minmax (opponent side) c]
        match side with
        |X-> List.maxBy fst scores
        |O-> List.minBy fst scores
        |E-> failwith "invalid side"

let ai_move side board = 
    minmax side board |> snd

let usr_move side c (board:Board) =
    Map.add c side board