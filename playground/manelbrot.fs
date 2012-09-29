module mandelbrot

open System.Numerics

(* The history contains a good mutable variant *)

// Creates a function that transforms from picture space into the complex plane
let coordinate_transformer (grid_width:int, grid_height:int) (center:Complex, complex_width:float)=     
    let complex_height = complex_width * float grid_height / float grid_width
    let box_upper_left = center - Complex(complex_width, complex_height)

    let rstep = 2.0 * complex_width / float grid_width
    let hstep = 2.0 * complex_height / float grid_height

    fun x y -> box_upper_left + Complex(float x * rstep, float y * hstep)

let escapesteps max_steps (c:Complex) = 
    let rec loop (z:Complex) steps = 
        if z.Magnitude < 2.0 && steps < max_steps then
            loop (z**2.0 + c) (steps + 1)
        else
            steps 
    loop Complex.Zero 0

//computes region of the mandelbrot set centered at @center 
//with the width in the complex plane @complex_width
//the computation is done on a grid_width by grid_height grid
let mandelbrot max_steps (grid_width,grid_height) (center, complex_width) = 
    let transform_coordinates = 
        coordinate_transformer (grid_width, grid_height) (center, complex_width)    
            
    let mandel_async_seq =
        seq {for x in 0..grid_width-1 do
                for y in 0..grid_height-1 do 
                    yield async { return x,y,(transform_coordinates x y) |> escapesteps max_steps}
            }    

    let grid = Array2D.zeroCreate grid_width grid_height        

    mandel_async_seq
    |> Async.Parallel
    |> Async.RunSynchronously    
    |> Seq.iter (fun (x,y,v) -> grid.[x, y] <- v)

    grid
