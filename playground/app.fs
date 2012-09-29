module app
open System.Numerics
open mandelbrot
open plot_matrix
//let path = @"D:\Desktop\out.png"
//let sz = (1920, 1080) 
//let reg = (Complex(-0.740, -0.141), 0.01)

//let MAX_STEPS = 18
//let SIZE = (400, 500) 
//let REGION = (Complex(-1.0, 0.0), 2.0)

let MAX_STEPS = 320
let SIZE = (1920, 1080) 
let REGION = (Complex(-0.740, -0.141), 0.01)

let timed func arg = 
    let sw = System.Diagnostics.Stopwatch()
    sw.Start ()    
    let ret = func arg
    printfn "%A \n %A seconds" func sw.Elapsed.Seconds
    sw.Stop ()
    ret

let save_to_file pallete path grid =
    use bmp = array2d_to_bmp pallete grid
    bmp.Save(path)

let grid = mandelbrot 255 (800,600) (Complex(-0.740, -0.141), 0.01)

let pallete = load_pallete @"D:\Desktop\xo_g\playground\pallete.jpg"


show default_pallete grid |> ignore
show pallete grid |> ignore

//save_to_file pallete @"D:\Desktop\out.png" grid

        
    
