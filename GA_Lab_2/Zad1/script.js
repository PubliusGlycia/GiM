var ballX = 150;
var ballY = 150;
var ballDX = 5;
var ballDY = 5;
var boardX; 
var boardY;
var ballR = 15;
var caseValue = 0;
var canvas; 
var ctx;
var gameLoop;
var cancelGame = "";
var btnPlay;
var btnText = "Pause";

function playStopAnimation(event){
    if(this.textContent == "Pause"){
        cancelAnimationFrame(cancelGame);
        this.textContent = "Start";
    }
    else{
        requestAnimationFrame(drawBall);
        this.textContent = "Pause";
    }
}

function drawBallCanvas(){
    canvas = document.getElementById("myCanvas");
    btnPlay = document.getElementById("stpBtn");
    btnPlay.innerHTML = btnText;

    if(canvas.getContext){
        ctx = canvas.getContext("2d");
        drawBall();
        btnPlay.addEventListener('click', playStopAnimation);
    }
}

function drawBall(){
    boardX = canvas.width;
    boardY = canvas.height;

    ctx.clearRect(0,0,boardX,boardY);

    ctx.fillStyle = "red";
    ctx.beginPath();
    ctx.arc(ballX, ballY, ballR, 0, Math.PI * 2, true);
    ctx.closePath();
    ctx.fill();
    
    switch(caseValue){
        case 0:
            ballX -= ballDX;
            if(ballX < ballR){
                caseValue = 1;
            }
            break;
        case 1:
        ballY -= ballDY;
        ballX = 0 + ballR;
            if(ballY < ballR){
                caseValue = 2;
            }
            break;
        case 2:
        ballX += ballDX;
        ballY = 0 + ballR;
            if(ballX >= boardX - ballR){
                caseValue = 3;
            }
            break;
        case 3:
        ballY += ballDY;
        ballX = boardX - ballR;
            if(ballY >= boardY - ballR){
                caseValue = 0;
            }        
            break;
    }

    cancelGame = requestAnimationFrame(drawBall);
    
}





