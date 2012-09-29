module questions

//is there a better way to compute min and max on a sequence 
//while consuming it only once?

//fixme this should blow on empty sequences
//HOW DO YOU ADVANCE THE SEQ ITERATOR AND PRODUCE A head, tail pai ??????
let min_and_max seqv =    
    seqv|>
    Seq.fold (fun (m,M) t -> (min m t) , (max M t)) 
             (infinity,-infinity)



    