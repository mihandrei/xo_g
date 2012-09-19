type board = Map<int*int, char>    

let emptyBoard:board = Map.ofList [ (1,1) , '_' ; (1,2) , '_' ; (1,3) , '_'
                                    (2,1) , '_' ; (2,2) , '_' ; (2,3) , '_'
                                    (3,1) , '_' ; (3,2) , '_' ; (3,3) , '_'
                                  ]

                                                       
let line_indices = [//linii
                    [ 1,1 ; 1,2 ; 1,3]
                    [ 2,1 ; 2,2 ; 2,3]
                    [ 3,1 ; 3,2 ; 3,3]
                    //coloane
                    [ 1,1 ; 2,1 ; 3,1]
                    [ 2,2 ; 2,2 ; 3,2]
                    [ 2,3 ; 2,3 ; 3,3]
                    //diag
                    [ 1,1 ; 2,2 ; 3,3]
                    [ 1,3 ; 2,2 ; 3,1]
                   ]

let goal board = 
    let eval_line l = 
        List.

type state  = 
    {
     children : node<'a> list
     value : 'a
    }


