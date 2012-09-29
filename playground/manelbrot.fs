module manelbrot

open System.Numerics
open System.Drawing

let coordinate_transformer (w:int, h:int) (center:Complex, complex_width:float)=     
    let complex_height = complex_width * float h / float w
    let box_upper_left = center - Complex(complex_width, complex_height)

    let rstep = 2.0 * complex_width / float w
    let hstep = 2.0 * complex_height / float h

    fun x y -> box_upper_left + Complex(float x * rstep, float y * hstep)

let escapesteps max_steps (c:Complex) = 
    let rec es (z:Complex) steps = 
        if z.Magnitude < 2.0 && steps < max_steps
        then
            es (z**2.0 + c) (steps+1)
        else
            steps 
    es Complex.Zero 0

let escapesteps_impure max_steps (c:Complex) = 
    let mutable z = Complex.Zero
    let mutable steps = 0
    while z.Magnitude < 2.0 && steps < max_steps do
        z <- z**2.0 + c
        steps <- steps + 1
    steps

let seq_to_bmp (w:int) (h:int) seqv = 
    let bmp = new Bitmap(w, h)
    seqv 
    |> Seq.iter (fun (x,y,v) -> bmp.SetPixel(x, y, v))
    bmp

let array2d_to_bmp arr = 
    let bmp = new Bitmap(Array2D.length1 arr, Array2D.length2 arr)
    arr 
    |> Array2D.iteri (fun x y v -> bmp.SetPixel(x, y, v))
    bmp

let mandel_factory (pallete:float->Color) max_steps (w,h) (center, complex_width) = 
    let transform_coordinates = coordinate_transformer (w, h) (center, complex_width)    
    //impure
    let mandel_array2d () =
        Array2D.init w h 
            (fun x y -> transform_coordinates x y 
                       |> escapesteps_impure max_steps )
    //pure        
    let mandel_async_seq =
        seq {for x in 0..w-1 do
                for y in 0..h-1 do 
                    yield async { return x,y,(transform_coordinates x y) |> escapesteps max_steps}
            }
    
    let false_color v = 
        pallete (float v / float max_steps)
    //io!
    let compute_imperative_sync path =     
        mandel_array2d ()
        |> Array2D.map false_color
        |> array2d_to_bmp  
        |> (fun bmp-> bmp.Save(path))
    
    //io!
    let compute_functional_async path =
        mandel_async_seq
        |> Async.Parallel
        |> Async.RunSynchronously    
        |> Seq.map (fun (x,y,v) -> x, y, false_color v)
        |> seq_to_bmp w h 
        |> (fun bmp-> bmp.Save(path))

    compute_imperative_sync, compute_functional_async

let timed func arg = 
    let sw = System.Diagnostics.Stopwatch()
    sw.Start ()    
    let ret = func arg
    printfn "%A \n %A seconds" func sw.Elapsed.Seconds
    sw.Stop ()
    ret

//let path = @"D:\Desktop\out.png"
//let sz = (1920, 1080) 
//let reg = (Complex(-0.740, -0.141), 0.01)

//let MAX_STEPS = 18
//let SIZE = (400, 500) 
//let REGION = (Complex(-1.0, 0.0), 2.0)

let MAX_STEPS = 320
let SIZE = (1920, 1080) 
let REGION = (Complex(-0.740, -0.141), 0.01)

let load_pallete (path:string) = 
    use bmap = new Bitmap(path)
    let length = bmap.Width
    let pallete = Array.init length (fun x -> bmap.GetPixel(x,0))    
        
    fun f -> if f<0.0 || f>1.0 then pallete.[0]  
             else pallete.[int ( float (length - 1) * f)]

let pallete = load_pallete @"D:\Desktop\xo_g\playground\pallete.jpg"

let compute_imperative_sync, compute_functional_async = 
    timed (mandel_factory pallete MAX_STEPS SIZE) REGION

//timed compute_imperative_sync  @"D:\Desktop\out_sync.png"
timed compute_functional_async @"D:\Desktop\out_async.png"