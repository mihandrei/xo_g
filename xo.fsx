type coord = {r:int; c:int}
type token = X|O
type CellContent = token option 

//type systemu e aproape complet in cazu asta. board mai poate fi invalid daca are lungimea != 9
//desi o reprezentare ca stringuri "A1" e mai flexibila si impreuna cu validare la constructie mai robusta
type Board = Map<coord , CellContent>    

let emptyBoard : Board = 
    [for j in 1..3 do
        for i in 1..3 do
            yield {r=i; c=j}, None]
    |> Map.ofList

let move (board:Board) c cell = 
    board.Add (c , cell)

let newb = move emptyBoard {r=1; c=1} (Some X)

let zm = Map.ofList [(1, 'a'); (2, 'b'); (3, 'c')]
zm.Add (1,'b')

type board = Map<int , token option>

let display_board (board:Board) =     
    let cellstr = function X->'X' | O->'0' | E->'_'
    board 
    |> Map.fold (fun s _ v -> s + (cellstr v)) 

    sprintf "%A%A%A\n%A%A%A\n%A%A%A" 2 3    
    let pa = "%A"
    sprintf pa 43
    ()
        
let lines = [//linii
                [ R1,C1 ; R1,C2 ; R1,C3]
                [ R2,C1 ; R2,C2 ; R2,C3]
                [ R3,C1 ; R3,C2 ; R3,C3]
                //coloane
                [ R1,C1 ; R2,C1 ; R3,C1]
                [ R2,C2 ; R2,C2 ; R3,C2]
                [ R2,C3 ; R2,C3 ; R3,C3]
                //diag
                [ R1,C1 ; R2,C2 ; R3,C3]
                [ R1,C3 ; R2,C2 ; R3,C1]
            ]

let goal board = 
    let eval_line line = 
        let z = line |> List.map |> Map.find
        z

type state  = 
    {
     children : node<'a> list
     value : 'a
    }


let kd = '_'*4