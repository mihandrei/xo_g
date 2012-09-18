(* incercare de implementat un xo in f# *)
(* ineleganta F# - ului 

notatia multipla pt tipuri
 
let hh(c:int) : int = c+4

val hh : int -> int 

type z =
    | Ha of int
    | Hy of string

de ce nu (Ha : int)
 
*)


type node<'a>  = 
    {
     children : node<'a> list
     value : 'a
    } 