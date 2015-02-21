'hello'.printl;
/*
class...
*/
$makeBoard = {
    [[this]*first]*second
};
$printOnBoard = {
    $board = this;
    first.each{
        $color = this['color'];
        $sq = this['sq'];
        sq.each{
            board[this[1]][this[0]] = color
        }
    };
    board
};
$addVec = {
    [this[0]+first[0], this[1]+first[1]]
};
$moveSnike = {
    $directions = #[
        'w', [0, -1],
        'a', [-1, 0],
        's', [0, 1],
        'd', [1, 0]
    ];
    $new = [];
    $di = first;
    new << addVec(this[0], directions[first]);
    new += this;
    new >> $pop;
    new
};
$generApple = {
    [(rand*this).floor, (rand*first).floor]
};
$W = 20;
$H = 12;
$apple;
$snike;
$direction;
$info;
$eatApple = {
    'eat'.printl;
    snike << apple;
    apple = generApple(W,H)
};
$checkApple = {
    snike[0] == apple
};
$doApple = {
    {checkApple()}.if{eatApple()}
};
$checkIsOut = {
    $head = snike[0];
    head[0]<0 | head[0]>=W | head[1]<0 | head[1]>=H
};
$checkEatSelf = {
    $res = false;
    (1..(snike.len)).each{
        $i = snike[this];
        {i == snike[0]}.if{
            res = true
        }
    };
    res
};
$reset = {
    direction = 'd';
    apple = generApple(W,H);
    snike = [[2,0], [1,0], [0,0]];
};
$doReset = {
    {checkIsOut() | checkEatSelf()}.if{
        reset()
    }
};

$squares = makeBoard('y',W,H);
$squares = printOnBoard(squares, [#['color', 'r', 'sq', [apple]], #['color', 'b', 'sq', $snike]]);
'squares'.dumpl;
$w = #[
    'w', 830,
    'h', 470,
    'time', 500,
    'ontime', {
        squares = makeBoard('y',W,H);
        snike = moveSnike(snike, direction);
        doReset();
        doApple();
        squares = printOnBoard(squares, [#['color', 'r', 'sq', [apple]], #['color', 'b', 'sq', snike]]);
        info = 'result '+snike.len
    },
    'keypress', {
        {'wasd'.contains(this)}.if{
            direction = this
        }
    },
    'squares', &&squares,
    'info', [
        #[
            'text', &&info,
            'color', 'b',
            'size', 16
        ]
    ]
    ].window;
w.show