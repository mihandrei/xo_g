module manelbrot

open System.Numerics
open System.Drawing

let MAX_STEPS = 255

let coordinate_transformer (w:int, h:int) (center:Complex, complex_width:float)=     
    let complex_height = complex_width * float h / float w
    let box_upper_left = center - Complex(complex_width, complex_height)

    let rstep = 2.0 * complex_width / float w
    let hstep = 2.0 * complex_height / float h

    fun x y -> box_upper_left + Complex(float x * rstep, float y * hstep)

let escapesteps (c:Complex) = 
    let rec es (z:Complex) steps = 
        if z.Magnitude < 2.0 && steps < MAX_STEPS
        then
            es (z**2.0 + c) (steps+1)
        else
            steps 
    es Complex.Zero 0

let escapesteps_impure (c:Complex) = 
    let mutable z = Complex.Zero
    let mutable steps = 0
    while z.Magnitude < 2.0 && steps < MAX_STEPS do
        z <- z**2.0 + c
        steps <- steps + 1
    steps

let mandel_sequence (w,h) (center, complex_width) = 
    let transform_coordinates = coordinate_transformer (w, h) (center, complex_width)    
    seq {for x in 0..w-1 do
            for y in 0..h-1 do 
                yield async { return x,y,(transform_coordinates x y) |> escapesteps }
        }
    
let mandel_array2d (w,h) (center, complex_width) = 
    let transform_coordinates = coordinate_transformer (w, h) (center, complex_width)    
    Array2D.init w h (fun x y -> transform_coordinates x y |> escapesteps_impure)
    let p = async {return 4}
    
    let r = Async.RunSynchronously p
    32
let write_pix (bmp:Bitmap) x y v = 
    let v = 255 - v
    bmp.SetPixel(x, y, Color.FromArgb(v,v,v))

let array_to_bmp arr = 
    let bmp = new Bitmap(Array2D.length1 arr, Array2D.length2 arr)        
    arr |> Array2D.iteri (write_pix bmp)
    bmp

let seq_to_bmp (w:int) (h:int) seqv = 
    let bmp = new Bitmap(w, h)        
    seqv |> Seq.iter (fun (x,y,v) -> write_pix bmp x y v)
    bmp
   
let m_functional path (w,h) (center, complex_width) =     
    mandel_sequence (w,h) (center, complex_width) 
    |> seq_to_bmp w h
    |> (fun bmp-> bmp.Save(path))

let m_imperative path (w,h) (center, complex_width) =     
    mandel_array2d (w,h) (center, complex_width) 
    |> array_to_bmp     
    |> (fun bmp-> bmp.Save(path))

    

let timed func = 
    let sw = System.Diagnostics.Stopwatch()
    sw.Start ()    
    func ()
    printfn "%A" sw.ElapsedMilliseconds
    sw.Stop ()

let sz = (1920, 1080) 
let reg = (Complex(-0.740, -0.141), 0.009)

timed (fun () -> 
        process_sequential @"D:\Desktop\out.png" sz reg) 
|> ignore    
//
//timed (fun () -> 
//        process_pseq       @"D:\Desktop\out.png" sz reg)
//|> ignore    
