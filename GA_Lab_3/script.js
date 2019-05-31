var canvas = document.getElementById('myCanvas');
var ctx = canvas.getContext('2d');
var pong = {};

//ball constructor
function ball(radius, color) {
    this.x       = 0,
    this.y       = 0,
    this.offsetX = 0,
    this.offsetY = 0,
    this.radius = radius,
    this.color   = color,
    this.create   = function() {
        ctx.beginPath();
        ctx.arc(this.x, this.y, this.radius, 0, Math.PI*2);
        ctx.fillStyle = this.color;
        ctx.fill();
        ctx.closePath();
    }
}

// paddle constructor
function paddle(name, width, length, offset, color) {
    this.x         = 0;
    this.y         = 0;
    this.score     = 0;
    this.name      = name;
    this.width     = width;
    this.length    = length;
    this.offset    = offset;
    this.color     = color;
    this.up        = false;
    this.down      = false;
    this.create    = function() {
        ctx.beginPath();
        ctx.rect(this.x, this.y, this.width, this.length);
        ctx.fillStyle = this.color;
        ctx.fill();
        ctx.closePath();
    }
}

//initialization
function initGame() {
    pong.state      = 0;  // 0: start 1: game, 2: point, 3: end
    pong.pause     = true;
    pong.ball     = new ball(10, 'orange');
    pong.players    = [];
    pong.players[0] = new paddle('Gracz 1',  15, 60, 7, 'cornflowerblue');
    pong.players[1] = new paddle('Gracz 2', 15, 60, 7, 'cornflowerblue');
    pong.topScore = 10;
    pong.winner = 0;
    resetGame();
}

// restart after point
function resetGame() {
    pong.ball.x           = canvas.width/2;
    pong.ball.y           = canvas.height/2;
    pong.pause            = true;
    pong.ball.offsetX     = 6;
    pong.ball.offsetY     = 2;
    pong.players[0].x     = 0;
    pong.players[1].x     = canvas.width - pong.players[1].width;
    pong.players[0].y     = (canvas.height - pong.players[0].length)/2;
    pong.players[1].y     = (canvas.height - pong.players[1].length)/2;
}
	

//keyboard handler
function keyDownHandler(e) {
    if (e.keyCode == 65) { pong.players[0].up = true; } else
    if (e.keyCode == 75) { pong.players[1].up = true; } else
    if (e.keyCode == 90) { pong.players[0].down = true; } else
    if (e.keyCode == 77) { pong.players[1].down = true; } else
    if (e.keyCode == 32) { pong.pause = !pong.pause }
}

function keyUpHandler(e) {
    if (e.keyCode == 65) { pong.players[0].up = false; } else
    if (e.keyCode == 75) { pong.players[1].up = false; } else
    if (e.keyCode == 90) { pong.players[0].down = false; } else
    if (e.keyCode == 77) { pong.players[1].down = false; }
}
 
document.addEventListener("keydown", keyDownHandler);
document.addEventListener("keyup", keyUpHandler);

//ball hitting paddle
function isBounced(rect,circle){
    var dx=Math.abs(circle.x-(rect.x+rect.width/2));
    var dy=Math.abs(circle.y-(rect.y+rect.length/2));
    if( dx > circle.radius+rect.width/2 ){ return(false); }
    if( dy > circle.radius+rect.length/2 ){ return(false); }
    if( dx <= rect.width ){ return(true); }
    if( dy <= rect.length ){ return(true); }
    var dx=dx-rect.width;
    var dy=dy-rect.length
    return(dx*dx+dy*dy<=circle.radius*circle.radius);
}

function showScore() {
    ctx.font = "16px Verdana";
    ctx.fillStyle = "red";
    ctx.textAlign = "left";
    ctx.fillText(pong.players[0].name + ": " + pong.players[0].score, 20, 20);
    ctx.textAlign = "right";
    ctx.fillText(pong.players[1].name + ": " + pong.players[1].score, canvas.width - 20, 20);
}

function showTitle(text) {
    ctx.font = '20px Verdana';
    ctx.fillStyle = 'black';
    ctx.textAlign = 'center';
    ctx.fillText(text, canvas.width/2, 60);
}

function gameDraw() {
    pong.ball.create();
    pong.players[0].create();
    pong.players[1].create();
    showScore();
}

function gameTransform() {
    
    pong.ball.x += pong.ball.offsetX;
    pong.ball.y += pong.ball.offsetY;

    // ball hitting horizontal walls 
    if (pong.ball.y + pong.ball.radius/2 >= canvas.height || pong.ball.y - pong.ball.radius/2 <= 0) {
        pong.ball.offsetY = -pong.ball.offsetY;
    }

    // player handler
    for (i = 0; i < pong.players.length; i++) {
        // paddle up
        if (pong.players[i].up && pong.players[i].y > 0) {
            pong.players[i].y -= pong.players[i].offset;
        }

        // paddle down
        if (pong.players[i].down && pong.players[i].y + pong.players[i].length < canvas.height) {
            pong.players[i].y += pong.players[i].offset;
        }

        // reflecting ball
        if (isBounced(pong.players[i], pong.ball)) {
            pong.ball.offsetX = -pong.ball.offsetX;

            // if paddle is in movement - ball changes angle
            if (pong.players[i].up) { pong.ball.offsetY--; }
            if (pong.players[i].down) { pong.ball.offsetY++; }
        }
    }

    // score for player 1
    if (pong.ball.x < pong.players[0].width) {
        pong.players[1].score++;
        pong.state = 2;
        pong.pause = true;
    }

    // score for player 2
    if (pong.ball.x > canvas.width - pong.players[1].width) {
        pong.players[0].score++;
        pong.state = 2;
        pong.pause = true;
    }

    // winning
    for (i = 0; i < pong.players.length; i++) {
        if (pong.players[i].score == pong.topScore) {
            pong.state = 3;
            pong.pause = true;
            pong.winner = i;
        }
    }
}

// game logic
function play() {
    ctx.clearRect(0, 0, canvas.width, canvas.height);
    gameDraw();

    if (pong.pause) {
        switch(pong.state) {
            case 0:
                showTitle('Poruszanie: Gracz1: A,Z Gracz2: K,M, Spacja: Start/Pauza');
                break;
            case 2:
                resetGame();
                showTitle('Punkt!');
                break;
            case 3:
                resetGame();
                showTitle(pong.players[pong.winner].name + ' wygrywa!');
                break;
            default:
                showTitle('Pauza');    
        }
    } else {
        switch(pong.state) {
            case 2:
                pong.state = 1;
                break;
            case 3:
                initGame();
                break;
            default:
                gameTransform();
        }
    }

    requestAnimationFrame(play);
}

initGame();
play();