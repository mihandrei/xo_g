module plot_matrix
open System.Drawing
open System.Windows.Forms
//annoyingly there is no function to do this
//unflatten array along_dimension
let unflatten_array2D arr =
   seq {for i in 0..Array2D.length1 arr - 1 do
            for j in 0..Array2D.length2 arr - 1 do 
                yield arr.[i,j]}
 
let array2d_to_bmp (pallete:float->Color) arr =     
    //we compute the range of the values so that we can normalize them to 0..1
    //and index in the pallete
    let min_value, max_value = 
        let s = arr |> unflatten_array2D
        Seq.min s , Seq.max s //reusing seq would break on file seq etc                    
        
    let false_color value =         
        let range = max_value - min_value        
        if range = 0 then 
            0.0 
        else 
            float (value - min_value) / (float range)
        |> pallete 
                   
    let bmp = new Bitmap(Array2D.length1 arr, Array2D.length2 arr)
    arr 
    |> Array2D.map false_color
    |> Array2D.iteri (fun x y v -> bmp.SetPixel(x, y, v))
    bmp
    
let load_pallete (path:string) = 
    use bmap = new Bitmap(path)
    let length = bmap.Width
    let pallete = Array.init length (fun x -> bmap.GetPixel(x,0))    
        
    fun f -> if f<0.0 || f>1.0 then pallete.[0]  
             else pallete.[int ( float (length - 1) * f)]

let default_pallete f = 
    let v = int (f*255.0)
    Color.FromArgb(v,v,v)

let show pallete matrix = 
    use bmp = array2d_to_bmp pallete matrix
    use form = new Form(Text = "Visualize matrix", 
                        Size = bmp.Size,
                        FormBorderStyle = FormBorderStyle.FixedSingle,
                        MaximizeBox=false)
    let pic_box = new PictureBox(
                       Image = bmp,
                       Size = bmp.Size,
                       Dock = DockStyle.Fill,
                       SizeMode = PictureBoxSizeMode.StretchImage)
    form.Controls.Add( pic_box )
    form.ShowDialog()
