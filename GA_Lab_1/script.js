var canvas;      
var ctx;
        
function draw() {         
    canvas = document.getElementById('myCanvas'); 
    ctx = canvas.getContext('2d');        
    
    if (canvas.getContext){
   
        for (var i = 75; i < 400; i += 70){
            for(var j = 75; j < 400; j += 70){

                var gradient = ctx.createRadialGradient(i, j, 1, i, j, 50);
                gradient.addColorStop(0, "white");
                gradient.addColorStop(1, "blue") 
                                
                ctx.beginPath();
                ctx.arc(i, j, 30, 40, Math.PI * 2, true);
                ctx.closePath(); 
                if(i==j || (430-i)==j){
                    ctx.fillStyle = gradient;
                    ctx.fill();   
                }else
                {
                ctx.stroke();
                }                                                                                         
            }
        }     
    }       
}     
